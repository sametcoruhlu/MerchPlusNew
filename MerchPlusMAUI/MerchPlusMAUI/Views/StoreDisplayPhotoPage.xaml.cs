using Microsoft.Maui.Controls;
using Microsoft.Maui.ApplicationModel;
using SQLite;
using System.Diagnostics;
using System.IO;
using entMerchPlus;
using Microsoft.Extensions.Logging;
using MerchPlusMAUI.Models;
using System.Collections.ObjectModel;
using MerchPlusMAUI.Services;
using System;
#if ANDROID
using Android.Graphics;
#endif

namespace MerchPlusMAUI.Views
{
    public partial class StoreDisplayPhotoPage : ContentPage
    {
        private readonly ILogger<StoreDisplayPhotoPage> _logger;
        private readonly IPhotoGalleryService _photoGalleryService;
        private string _entMemberRouteId;
        private readonly string _dbPath;
        
        private List<entPromotionType> _listPromotionType = new();
        private entPromotionType _entPromotionTypeSelected = new();
        private List<entCustomerProduct> _listCustomerProduct = new();
        private List<entCustomerProduct> _selectedProducts = new();
        private ObservableCollection<DisplayPhotoItem> _displayPhotoList = new();
        
        private entMemberRoute _currentMemberRoute;
        private bool _isTakingPhoto;
        private bool _isSavingPhotos;

        public StoreDisplayPhotoPage(ILogger<StoreDisplayPhotoPage> logger)
        {
            InitializeComponent();
            _logger = logger;
            _photoGalleryService = new PhotoGalleryService();
            _dbPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "merchplus.db3");
            
            // ObservableCollection'ın CollectionChanged event'ini dinle
            _displayPhotoList.CollectionChanged += (sender, e) =>
            {
                System.Diagnostics.Debug.WriteLine($"CollectionChanged: {e.Action}, Count: {_displayPhotoList.Count}");
            };
            
            PhotoListView.ItemsSource = _displayPhotoList;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            // Sayfa başlangıçta görünür olsun (siyah ekranı önlemek için)
            this.Opacity = 1;

            // Loading overlay'i göster
            var loadingOverlay = this.FindByName<Grid>("LoadingOverlay");
            if (loadingOverlay != null)
            {
                loadingOverlay.IsVisible = true;
            }

            try
            {
                // GlobalVariables'dan MemberRouteId'yi al
                if (GlobalVariables.CurrentMemberRoute != null)
                {
                    _entMemberRouteId = GlobalVariables.CurrentMemberRoute.Id.ToString();
                    System.Diagnostics.Debug.WriteLine($"StoreDisplayPhotoPage: MemberRouteId from GlobalVariables: {_entMemberRouteId}");
                }
                else
                {
                    // Eğer GlobalVariables'da yoksa veritabanından aktif route'u al
                    using (var db = new SQLiteConnection(_dbPath))
                    {
                        var activeRoute = db.Query<entMemberRoute>("SELECT * FROM entMemberRoute WHERE IsActive = 1 LIMIT 1");
                        if (activeRoute != null && activeRoute.Count > 0)
                        {
                            _entMemberRouteId = activeRoute[0].Id.ToString();
                            System.Diagnostics.Debug.WriteLine($"StoreDisplayPhotoPage: Active MemberRouteId from DB: {_entMemberRouteId}");
                        }
                    }
                }

                await LoadDataAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"StoreDisplayPhotoPage: Parameter loading error: {ex.Message}");
                _logger?.LogError(ex, "Error loading navigation parameters");
            }
            finally
            {
                // Veriler yüklendikten sonra loading overlay'i gizle
                if (loadingOverlay != null)
                {
                    loadingOverlay.IsVisible = false;
                }
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            
            // FadeTo(0) kaldırıldı - Shell navigation kendi animasyonunu yönetiyor
        }

        // Geri butonu için override
        protected override bool OnBackButtonPressed()
        {
            System.Diagnostics.Debug.WriteLine("=== StoreDisplayPhotoPage Hardware Back Button Pressed ===");
            
            // Loading overlay göster (hemen görünsün)
            var loadingOverlay = this.FindByName<Grid>("LoadingOverlay");
            if (loadingOverlay != null)
            {
                loadingOverlay.IsVisible = true;
                loadingOverlay.InputTransparent = false;
            }
            
            // Bir önceki sayfaya geri dön
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    // Kısa bir delay ekle ki overlay render edilsin
                    await Task.Delay(50);
                    
                    await NavigateBackToPreviousPage();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"StoreDisplayPhotoPage: Geri gitme hatası: {ex.Message}");
                    _logger?.LogError(ex, "Error navigating back");
                    if (loadingOverlay != null)
                    {
                        loadingOverlay.IsVisible = false;
                        loadingOverlay.InputTransparent = true;
                    }
                }
            });
            
            return true; // Event'i handle et
        }

        private async Task NavigateBackToPreviousPage()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("StoreDisplayPhotoPage: Bir önceki sayfaya (StoreVitrinePhotoPage) geri dönülüyor");
                
                // StoreVitrinePhotoPage'e explicit navigation (MainActivity'deki kod da bu path'i kullanıyor)
                await Shell.Current.GoToAsync("///StoreVitrinePhotoPage");
                System.Diagnostics.Debug.WriteLine("StoreDisplayPhotoPage: Navigation to StoreVitrinePhotoPage completed");
                
                // Navigation tamamlandı, loading overlay'i gizle (hedef sayfa kendi overlay'ini gösterecek)
                var loadingOverlay = this.FindByName<Grid>("LoadingOverlay");
                if (loadingOverlay != null)
                {
                    loadingOverlay.IsVisible = false;
                    loadingOverlay.InputTransparent = true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"StoreDisplayPhotoPage: NavigateBackToPreviousPage hatası: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"StoreDisplayPhotoPage: Stack trace: {ex.StackTrace}");
                _logger?.LogError(ex, "Error navigating back to previous page");
                
                var loadingOverlay = this.FindByName<Grid>("LoadingOverlay");
                if (loadingOverlay != null)
                {
                    loadingOverlay.IsVisible = false;
                    loadingOverlay.InputTransparent = true;
                }
                await DisplayAlert("Hata", "Sayfa geçişi sırasında hata oluştu.", "Tamam");
            }
        }

        private async Task LoadDataAsync()
        {
            try
            {
                using (var db = new SQLiteConnection(_dbPath))
                {
                    // Teşhir tiplerini yükle
                    var promotionTypes = db.Query<entPromotionType>("SELECT * FROM entPromotionType");
                    var placeholderItem = new entPromotionType { Id = -1, Name = "Seçiniz" };
                    promotionTypes.Insert(0, placeholderItem);
                    _listPromotionType = promotionTypes;

                    // MemberRoute bilgilerini al
                    var memberRoutes = db.Query<entMemberRoute>("SELECT * FROM entMemberRoute WHERE Id = ?", Convert.ToInt32(_entMemberRouteId));
                    if (memberRoutes.Count > 0)
                    {
                        _currentMemberRoute = memberRoutes[0];
                        
                        System.Diagnostics.Debug.WriteLine($"StoreDisplayPhotoPage: MemberRoute CustomerId: {_currentMemberRoute.CustomerId}");
                        
                        // Ürünleri yükle - CustomerId tip uyumsuzluğunu düzelt
                        var products = db.Query<entCustomerProduct>("SELECT * FROM entCustomerProduct WHERE Id IN (SELECT CustomerProductId FROM entMemberRouteDetail WHERE MemberRouteId=?) AND CustomerId=?", 
                            _entMemberRouteId, _currentMemberRoute.CustomerId?.ToString());
                        
                        System.Diagnostics.Debug.WriteLine($"StoreDisplayPhotoPage: Found {products.Count} products for CustomerId: {_currentMemberRoute.CustomerId}");
                        _listCustomerProduct = products;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"StoreDisplayPhotoPage: LoadDataAsync error: {ex.Message}");
                _logger?.LogError(ex, "Error loading data");
            }
        }

        private async void OnPromotionTypeClicked(object sender, EventArgs e)
        {
            try
            {
                var promotionNames = _listPromotionType.Select(p => p.Name).ToArray();
                var selectedIndex = _listPromotionType.IndexOf(_entPromotionTypeSelected);
                
                var action = await DisplayActionSheet("Teşhir Tipi Seçiniz", "İptal", null, promotionNames);
                
                if (action != null && action != "İptal")
                {
                    var selectedPromotion = _listPromotionType.FirstOrDefault(p => p.Name == action);
                    if (selectedPromotion != null && selectedPromotion.Id != -1)
                    {
                        _entPromotionTypeSelected = selectedPromotion;
                        SelectedPromotionLabel.Text = $"Seçilen Teşhir Tipi: {selectedPromotion.Name}";
                        SelectedPromotionLabel.IsVisible = true;
                        PromotionTypeLabel.IsVisible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"StoreDisplayPhotoPage: OnPromotionTypeClicked error: {ex.Message}");
                _logger?.LogError(ex, "Error selecting promotion type");
            }
        }

        private async void OnProductSelectionClicked(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"StoreDisplayPhotoPage: Ürün seçimi başlatılıyor. Mevcut ürün sayısı: {_listCustomerProduct.Count}");
                
                if (_listCustomerProduct.Count == 0)
                {
                    await DisplayAlert("Uyarı", "Bu mağaza için ürün bulunamadı! Lütfen önce ürün girişi yapın.", "Tamam");
                    return;
                }

                System.Diagnostics.Debug.WriteLine($"StoreDisplayPhotoPage: ProductMultiSelectionPage açılıyor...");
                // Çoklu seçim için yeni bir sayfa oluşturalım
                await Navigation.PushModalAsync(new ProductMultiSelectionPage(_listCustomerProduct, _selectedProducts, OnProductsSelected));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"StoreDisplayPhotoPage: OnProductSelectionClicked error: {ex.Message}");
                _logger?.LogError(ex, "Error selecting products");
            }
        }

        private void OnProductsSelected(List<entCustomerProduct> selectedProducts)
        {
            _selectedProducts = selectedProducts;
            
            if (_selectedProducts.Count > 0)
            {
                SelectedProductsLabel.Text = $"Seçilen Ürünler: {string.Join(", ", _selectedProducts.Select(p => p.Name))}";
                SelectedProductsLabel.IsVisible = true;
                ProductSelectionLabel.IsVisible = false;
            }
            else
            {
                SelectedProductsLabel.IsVisible = false;
                ProductSelectionLabel.IsVisible = true;
            }
            
            System.Diagnostics.Debug.WriteLine($"StoreDisplayPhotoPage: {_selectedProducts.Count} ürün seçildi");
        }

        private async void OnTakePhotoClicked(object sender, EventArgs e)
        {
            if (_isTakingPhoto)
            {
                System.Diagnostics.Debug.WriteLine("StoreDisplayPhotoPage: Fotoğraf çekme işlemi zaten devam ediyor, ikinci tetikleme engellendi");
                return;
            }

            try
            {
                _isTakingPhoto = true;
                if (TakePhotoButton != null)
                {
                    TakePhotoButton.IsEnabled = false;
                }

                if (_entPromotionTypeSelected.Id == -1)
                {
                    await DisplayAlert("Uyarı", "Lütfen bir teşhir tipi seçiniz!", "Tamam");
                    return;
                }

                if (_selectedProducts.Count == 0)
                {
                    await DisplayAlert("Uyarı", "Lütfen en az bir ürün seçiniz!", "Tamam");
                    return;
                }

                System.Diagnostics.Debug.WriteLine("StoreDisplayPhotoPage: Teşhir fotoğrafı çekme başlatılıyor...");

                // Kamera izni kontrol et
                var status = await Microsoft.Maui.ApplicationModel.Permissions.CheckStatusAsync<Microsoft.Maui.ApplicationModel.Permissions.Camera>();
                if (status != Microsoft.Maui.ApplicationModel.PermissionStatus.Granted)
                {
                    status = await Microsoft.Maui.ApplicationModel.Permissions.RequestAsync<Microsoft.Maui.ApplicationModel.Permissions.Camera>();
                    if (status != Microsoft.Maui.ApplicationModel.PermissionStatus.Granted)
                    {
                        await DisplayAlert("İzin Gerekli", "Kamera izni gerekli. Lütfen ayarlardan izin verin.", "Tamam");
                        return;
                    }
                }

                // MAUI MediaPicker ile fotoğraf çekme
                var photo = await Microsoft.Maui.Media.MediaPicker.Default.CapturePhotoAsync();
                if (photo != null)
                {
                    System.Diagnostics.Debug.WriteLine($"StoreDisplayPhotoPage: Teşhir fotoğrafı çekildi: {photo.FileName}");

                    // Fotoğrafı byte array olarak oku
                    byte[] photoBytes;
                    using (var stream = await photo.OpenReadAsync())
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await stream.CopyToAsync(memoryStream);
                            photoBytes = memoryStream.ToArray();
                        }
                    }

                    // Seçilen ürünlerin her biri için fotoğraf ekliyoruz
                    var newItems = new List<DisplayPhotoItem>();
                    
                    foreach (var product in _selectedProducts)
                    {
                        // UI listesinde tam boy görsel yerine thumbnail kullan
                        var photoSource = CreatePhotoPreviewSource(photoBytes);
                        
                        var newItem = new DisplayPhotoItem
                        {
                            PromotionTypeId = _entPromotionTypeSelected.Id,
                            PromotionType = _entPromotionTypeSelected.Name,
                            ProductId = product.Id,
                            ProductName = product.Name,
                            PhotoBytes = photoBytes,
                            PhotoSource = photoSource
                        };

                        newItems.Add(newItem);
                        System.Diagnostics.Debug.WriteLine($"StoreDisplayPhotoPage: Fotoğraf hazırlandı - Teşhir Tipi: {_entPromotionTypeSelected.Name}, Ürün: {product.Name}, PromotionTypeId: {_entPromotionTypeSelected.Id}");
                    }

                    // Fotoğrafı galeriye kaydet
                    try
                    {
                        var fileName = $"MerchPlus_Teşhir_{_entPromotionTypeSelected.Name}_{DateTime.Now:yyyyMMdd_HHmmss}.jpg";
                        var savedToGallery = await _photoGalleryService.SavePhotoToGalleryAsync(photoBytes, fileName);
                        if (savedToGallery)
                        {
                            System.Diagnostics.Debug.WriteLine($"StoreDisplayPhotoPage: Teşhir fotoğrafı galeriye kaydedildi: {fileName}");
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("StoreDisplayPhotoPage: Teşhir fotoğrafı galeriye kaydedilemedi");
                        }
                    }
                    catch (Exception galleryEx)
                    {
                        System.Diagnostics.Debug.WriteLine($"StoreDisplayPhotoPage: Galeri kaydetme hatası: {galleryEx.Message}");
                        _logger?.LogError(galleryEx, "Error saving photo to gallery");
                        // Galeri kaydetme hatası kullanıcıyı rahatsız etmesin, sadece log'la
                    }

                    // Ana thread'de tüm öğeleri tek seferde ekle - await ile bekle
                    await MainThread.InvokeOnMainThreadAsync(() =>
                    {
                        foreach (var item in newItems)
                        {
                            _displayPhotoList.Add(item);
                        }
                        
                        System.Diagnostics.Debug.WriteLine($"StoreDisplayPhotoPage: {newItems.Count} fotoğraf listeye eklendi");
                        System.Diagnostics.Debug.WriteLine($"StoreDisplayPhotoPage: Toplam fotoğraf sayısı: {_displayPhotoList.Count}");
                    });

                    // Alert'i göster - await ile bekle
                    await DisplayAlert("Başarılı", $"Teşhir fotoğrafı başarıyla çekildi ve {_selectedProducts.Count} ürün için listeye eklendi.\nTeşhir Tipi: {_entPromotionTypeSelected.Name}", "Tamam");
                    
                    // Seçimler temizlenmiyor - kullanıcı aynı teşhir tipi ve ürünlerle tekrar fotoğraf çekebilir
                    System.Diagnostics.Debug.WriteLine("StoreDisplayPhotoPage: Fotoğraf çekildi, seçimler korunuyor");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("StoreDisplayPhotoPage: Teşhir fotoğrafı çekme iptal edildi");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"StoreDisplayPhotoPage: Teşhir fotoğrafı çekme hatası: {ex.Message}");
                _logger?.LogError(ex, "Error taking display photo");
                await DisplayAlert("Hata", "Teşhir fotoğrafı çekilirken hata oluştu. Lütfen tekrar deneyin.", "Tamam");
            }
            finally
            {
                _isTakingPhoto = false;
                if (TakePhotoButton != null)
                {
                    TakePhotoButton.IsEnabled = true;
                }
            }
        }

        private void OnDeletePhotoClicked(object sender, EventArgs e)
        {
            try
            {
                if (sender is Button button && button.BindingContext is DisplayPhotoItem item)
                {
                    // Ana thread'de ObservableCollection'dan kaldır
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        _displayPhotoList.Remove(item);
                        
                        System.Diagnostics.Debug.WriteLine($"StoreDisplayPhotoPage: Fotoğraf silindi: {item.ProductName}");
                        System.Diagnostics.Debug.WriteLine($"StoreDisplayPhotoPage: Kalan fotoğraf sayısı: {_displayPhotoList.Count}");
                    });
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"StoreDisplayPhotoPage: OnDeletePhotoClicked error: {ex.Message}");
                _logger?.LogError(ex, "Error deleting photo");
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (_isSavingPhotos)
            {
                System.Diagnostics.Debug.WriteLine("StoreDisplayPhotoPage: Kaydet işlemi zaten çalışıyor, ikinci tetikleme engellendi");
                return;
            }

            try
            {
                _isSavingPhotos = true;
                if (SaveButton != null)
                {
                    SaveButton.IsEnabled = false;
                    SaveButton.Text = "KAYDEDILIYOR...";
                }

                if (_displayPhotoList.Count == 0)
                {
                    await DisplayAlert("Uyarı", "Kaydedilecek fotoğraf bulunamadı.", "Tamam");
                    return;
                }

                System.Diagnostics.Debug.WriteLine($"StoreDisplayPhotoPage: {_displayPhotoList.Count} fotoğraf kaydediliyor...");

                using (var db = new SQLiteConnection(_dbPath))
                {
                    foreach (var photoItem in _displayPhotoList)
                    {
                        // Fotoğraf byte array kontrolü
                        if (photoItem.PhotoBytes == null || photoItem.PhotoBytes.Length == 0)
                        {
                            System.Diagnostics.Debug.WriteLine($"StoreDisplayPhotoPage: UYARI - Fotoğraf byte array null veya boş, atlanıyor - Ürün: {photoItem.ProductName}");
                            await DisplayAlert("Uyarı", $"Ürün '{photoItem.ProductName}' için fotoğraf verisi bulunamadı, atlanıyor.", "Tamam");
                            continue;
                        }
                        
                        // Fotoğrafı Base64 string'e çevir
                        string base64Photo = Convert.ToBase64String(photoItem.PhotoBytes);
                        
                        if (string.IsNullOrEmpty(base64Photo))
                        {
                            System.Diagnostics.Debug.WriteLine($"StoreDisplayPhotoPage: UYARI - Base64 string boş, atlanıyor - Ürün: {photoItem.ProductName}");
                            await DisplayAlert("Uyarı", $"Ürün '{photoItem.ProductName}' için fotoğraf Base64'e çevrilemedi, atlanıyor.", "Tamam");
                            continue;
                        }
                        
                        // entMemberDisplayPhotos tablosuna kaydet
                        var photoEntity = new entMemberDisplayPhotos
                        {
                            PromotionTypeId = photoItem.PromotionTypeId,
                            CustomerProductId = photoItem.ProductId,
                            DisplayPicturePath = base64Photo,
                            IsSentToServer = false,
                            MemberId = _currentMemberRoute.MemberId,
                            MemberRouteId = _currentMemberRoute.Id,
                            CreatedOn = DateTime.Now
                        };

                        db.Insert(photoEntity);
                        
                        System.Diagnostics.Debug.WriteLine($"StoreDisplayPhotoPage: Fotoğraf kaydedildi - Id: {photoEntity.Id}, PromotionTypeId: {photoItem.PromotionTypeId}, PromotionType: {photoItem.PromotionType}, ProductId: {photoItem.ProductId}, ProductName: {photoItem.ProductName}, DisplayPicturePath uzunluğu: {base64Photo.Length}");
                    }
                }

                System.Diagnostics.Debug.WriteLine("StoreDisplayPhotoPage: Tüm fotoğraflar veritabanına kaydedildi");
                
                // CommonFooter badge'ini güncelle (eğer AboutMePage görünürse)
                Controls.CommonFooter.RefreshBadge();
                
                // Sayfayı temizle - yeni giriş için
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    _displayPhotoList.Clear();
                    
                    // Seçimleri de temizle
                    _entPromotionTypeSelected = new entPromotionType { Id = -1, Name = "Seçiniz" };
                    _selectedProducts.Clear();
                    
                    // UI'ı resetle
                    SelectedPromotionLabel.IsVisible = false;
                    PromotionTypeLabel.IsVisible = true;
                    SelectedProductsLabel.IsVisible = false;
                    ProductSelectionLabel.IsVisible = true;
                    
                    System.Diagnostics.Debug.WriteLine("StoreDisplayPhotoPage: Sayfa temizlendi, yeni giriş için hazır");
                });
                
                // Kaydedildi mesajını göster
                await DisplayAlert("Başarılı", "Fotoğraflar kaydedildi.", "Tamam");
                
                // CommonFooter badge'ini güncelle (eğer AboutMePage görünürse)
                Controls.CommonFooter.RefreshBadge();
                
                // Tamam butonuna tıklandığında bir önceki sayfaya (StoreVitrinePhotoPage) geri dön
                await NavigateBackToPreviousPage();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"StoreDisplayPhotoPage: OnSaveClicked error: {ex.Message}");
                _logger?.LogError(ex, "Error saving photos");
                var loadingOverlay = this.FindByName<Grid>("LoadingOverlay");
                if (loadingOverlay != null) loadingOverlay.IsVisible = false;
                await DisplayAlert("Hata", "Fotoğraflar kaydedilirken hata oluştu.", "Tamam");
            }
            finally
            {
                _isSavingPhotos = false;
                if (SaveButton != null)
                {
                    SaveButton.IsEnabled = true;
                    SaveButton.Text = "KAYDET";
                }
            }
        }

        private ImageSource CreatePhotoPreviewSource(byte[] photoBytes)
        {
            var thumbnailBytes = CreateThumbnailBytes(photoBytes, 240);
            return ImageSource.FromStream(() => new MemoryStream(thumbnailBytes));
        }

        private static byte[] CreateThumbnailBytes(byte[] originalBytes, int maxEdge)
        {
            if (originalBytes == null || originalBytes.Length == 0)
                return Array.Empty<byte>();

#if ANDROID
            try
            {
                var boundsOptions = new BitmapFactory.Options { InJustDecodeBounds = true };
                BitmapFactory.DecodeByteArray(originalBytes, 0, originalBytes.Length, boundsOptions);

                int sampleSize = 1;
                while ((boundsOptions.OutHeight / sampleSize) > maxEdge || (boundsOptions.OutWidth / sampleSize) > maxEdge)
                {
                    sampleSize *= 2;
                }

                var decodeOptions = new BitmapFactory.Options
                {
                    InSampleSize = Math.Max(1, sampleSize),
                    InPreferredConfig = Bitmap.Config.Rgb565
                };

                using var bitmap = BitmapFactory.DecodeByteArray(originalBytes, 0, originalBytes.Length, decodeOptions);
                if (bitmap == null)
                    return originalBytes;

                using var output = new MemoryStream();
                bitmap.Compress(Bitmap.CompressFormat.Jpeg, 80, output);
                return output.ToArray();
            }
            catch
            {
                return originalBytes;
            }
#else
            return originalBytes;
#endif
        }

    }
}
