using Microsoft.Maui.Controls;
using SQLite;
using System.Diagnostics;
using System.IO;
using entMerchPlus;
using Microsoft.Extensions.Logging;

namespace MerchPlusMAUI.Views
{
    public partial class StoreVitrinePhotoPage : ContentPage
    {
        private readonly ILogger<StoreVitrinePhotoPage> _logger;
        private string _entMemberRouteId;
        private string _storeName = "Mağaza";
        private readonly string _dbPath;

        public StoreVitrinePhotoPage(ILogger<StoreVitrinePhotoPage> logger)
        {
            InitializeComponent();
            _logger = logger;
            _dbPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "merchplus.db3");
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
                    System.Diagnostics.Debug.WriteLine($"StoreVitrinePhotoPage: MemberRouteId from GlobalVariables: {_entMemberRouteId}");
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
                            System.Diagnostics.Debug.WriteLine($"StoreVitrinePhotoPage: Active MemberRouteId from DB: {_entMemberRouteId}");
                        }
                    }
                }
                
                // StoreName'i al
                if (!string.IsNullOrEmpty(_entMemberRouteId))
                {
                    using (var db = new SQLiteConnection(_dbPath))
                    {
                        // int.Parse'i lambda dışında yap (SQLite parse fonksiyonunu tanımıyor)
                        var routeId = int.Parse(_entMemberRouteId);
                        var route = db.Table<entMemberRoute>()
                            .FirstOrDefault(r => r.Id == routeId);
                        
                        if (route?.RetailShopId != null)
                        {
                            var retailShop = db.Table<entRetailShop>()
                                .FirstOrDefault(s => s.Id == route.RetailShopId.Value);
                            
                            if (retailShop != null)
                            {
                                _storeName = retailShop.Name ?? "Mağaza";
                                System.Diagnostics.Debug.WriteLine($"StoreVitrinePhotoPage: StoreName from DB: {_storeName}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"StoreVitrinePhotoPage: Parameter loading error: {ex.Message}");
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

        private async void OnShelfPhotoClicked(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("StoreVitrinePhotoPage: Raf fotoğrafı çekme başlatılıyor...");

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
                    System.Diagnostics.Debug.WriteLine($"StoreVitrinePhotoPage: Raf fotoğrafı çekildi: {photo.FileName}");

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

                    // Fotoğrafı veritabanına kaydet (Raf fotoğrafı olarak)
                    await SaveShelfPhotoToDatabase(photoBytes);

                    await DisplayAlert("Başarılı", "Raf fotoğrafı başarıyla çekildi ve kaydedildi.", "Tamam");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("StoreVitrinePhotoPage: Raf fotoğrafı çekme iptal edildi");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"StoreVitrinePhotoPage: Raf fotoğrafı çekme hatası: {ex.Message}");
                _logger?.LogError(ex, "Error taking shelf photo");
                await DisplayAlert("Hata", "Raf fotoğrafı çekilirken hata oluştu. Lütfen tekrar deneyin.", "Tamam");
            }
        }

        private async void OnDisplayPhotoClicked(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("StoreVitrinePhotoPage: Teşhir fotoğrafı sayfasına yönlendiriliyor...");
                
                // StoreDisplayPhotoPage'e navigate et
                await Shell.Current.GoToAsync("//StoreDisplayPhotoPage");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"StoreVitrinePhotoPage: OnDisplayPhotoClicked error: {ex.Message}");
                _logger?.LogError(ex, "Error navigating to StoreDisplayPhotoPage");
                await DisplayAlert("Hata", "Teşhir fotoğrafı sayfasına geçiş sırasında hata oluştu.", "Tamam");
            }
        }

        private async Task SaveShelfPhotoToDatabase(byte[] photoBytes)
        {
            try
            {
                if (string.IsNullOrEmpty(_entMemberRouteId))
                {
                    System.Diagnostics.Debug.WriteLine("StoreVitrinePhotoPage: MemberRouteId bulunamadı");
                    return;
                }

                // Fotoğrafı Base64 string'e çevir
                string base64Photo = Convert.ToBase64String(photoBytes);

                using (var db = new SQLiteConnection(_dbPath))
                {
                    // entMemberRoute tablosundan route bilgilerini al
                    var routeQuery = db.Query<entMemberRoute>("SELECT * FROM entMemberRoute WHERE Id = ?", Convert.ToInt32(_entMemberRouteId));
                    if (routeQuery != null && routeQuery.Count > 0)
                    {
                        var route = routeQuery[0];

                        // entMemberRoutePhoto tablosuna raf fotoğrafını kaydet (eski projeye göre)
                        var shelfPhoto = new entMemberRoutePhoto
                        {
                            CreatedOn = DateTime.Now,
                            IsSentToServer = false, // Eski projede IsSentToServer kullanılıyor
                            MemberId = route.MemberId, // Eski projede MemberId kullanılıyor
                            MemberRouteId = route.Id,
                            ProfilePicturePath = base64Photo // Eski projede ProfilePicturePath kullanılıyor
                        };

                        db.Insert(shelfPhoto);
                        System.Diagnostics.Debug.WriteLine("StoreVitrinePhotoPage: Raf fotoğrafı veritabanına kaydedildi");
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("StoreVitrinePhotoPage: Route bulunamadı");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"StoreVitrinePhotoPage: Raf fotoğrafı kaydetme hatası: {ex.Message}");
                _logger?.LogError(ex, "Error saving shelf photo to database");
                throw;
            }
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            try
            {
                // Loading overlay göster (hemen görünsün)
                var loadingOverlay = this.FindByName<Grid>("LoadingOverlay");
                if (loadingOverlay != null)
                {
                    loadingOverlay.IsVisible = true;
                    loadingOverlay.InputTransparent = false; // Tıklamaları engelle
                }
                
                // Kısa bir delay ekle ki overlay render edilsin
                await Task.Delay(50);
                
                System.Diagnostics.Debug.WriteLine("StoreVitrinePhotoPage: Geri butonuna tıklandı");
                System.Diagnostics.Debug.WriteLine($"StoreVitrinePhotoPage: _entMemberRouteId: {_entMemberRouteId}");
                
                // EntMemberRouteId kontrolü
                if (string.IsNullOrEmpty(_entMemberRouteId))
                {
                    System.Diagnostics.Debug.WriteLine("StoreVitrinePhotoPage: _entMemberRouteId is null or empty, trying to get from GlobalVariables");
                    
                    // GlobalVariables'dan tekrar al
                    if (GlobalVariables.CurrentMemberRoute != null)
                    {
                        _entMemberRouteId = GlobalVariables.CurrentMemberRoute.Id.ToString();
                        System.Diagnostics.Debug.WriteLine($"StoreVitrinePhotoPage: Got _entMemberRouteId from GlobalVariables: {_entMemberRouteId}");
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("StoreVitrinePhotoPage: GlobalVariables.CurrentMemberRoute is also null");
                        if (loadingOverlay != null)
                        {
                            loadingOverlay.IsVisible = false;
                            loadingOverlay.InputTransparent = true;
                        }
                        await DisplayAlert("Hata", "Mağaza bilgileri bulunamadı.", "Tamam");
                        return;
                    }
                }
                
                // StoreProductsPage'e geri dön - navigation parametreleri ile
                var navigationParameters = new Dictionary<string, object>
                {
                    ["entMemberRouteId"] = _entMemberRouteId,
                    ["StoreName"] = _storeName
                };
                
                System.Diagnostics.Debug.WriteLine($"StoreVitrinePhotoPage: Navigating to StoreProductsPage with parameters: entMemberRouteId={_entMemberRouteId}");
                await Shell.Current.GoToAsync("//StoreProductsPage", navigationParameters);
                
                // Navigation tamamlandı, loading overlay'i gizle (hedef sayfa kendi overlay'ini gösterecek)
                if (loadingOverlay != null)
                {
                    loadingOverlay.IsVisible = false;
                    loadingOverlay.InputTransparent = true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"StoreVitrinePhotoPage: Geri gitme hatası: {ex.Message}");
                _logger?.LogError(ex, "Error navigating back");
                var loadingOverlay = this.FindByName<Grid>("LoadingOverlay");
                if (loadingOverlay != null)
                {
                    loadingOverlay.IsVisible = false;
                    loadingOverlay.InputTransparent = true;
                }
                await DisplayAlert("Hata", "Sayfa geçişi sırasında hata oluştu.", "Tamam");
            }
        }

        private async void OnFinishClicked(object sender, EventArgs e)
        {
            try
            {
                // Loading overlay göster (hemen görünsün)
                var loadingOverlay = this.FindByName<Grid>("LoadingOverlay");
                if (loadingOverlay != null)
                {
                    loadingOverlay.IsVisible = true;
                    loadingOverlay.InputTransparent = false; // Tıklamaları engelle
                }
                
                // Kısa bir delay ekle ki overlay render edilsin
                await Task.Delay(50);
                
                System.Diagnostics.Debug.WriteLine("StoreVitrinePhotoPage: Tamamla butonuna tıklandı");
                System.Diagnostics.Debug.WriteLine($"StoreVitrinePhotoPage: _entMemberRouteId: {_entMemberRouteId}");
                
                // EntMemberRouteId kontrolü
                if (string.IsNullOrEmpty(_entMemberRouteId))
                {
                    System.Diagnostics.Debug.WriteLine("StoreVitrinePhotoPage: _entMemberRouteId is null or empty, trying to get from GlobalVariables");
                    
                    // GlobalVariables'dan tekrar al
                    if (GlobalVariables.CurrentMemberRoute != null)
                    {
                        _entMemberRouteId = GlobalVariables.CurrentMemberRoute.Id.ToString();
                        System.Diagnostics.Debug.WriteLine($"StoreVitrinePhotoPage: Got _entMemberRouteId from GlobalVariables: {_entMemberRouteId}");
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("StoreVitrinePhotoPage: GlobalVariables.CurrentMemberRoute is also null");
                        if (loadingOverlay != null)
                        {
                            loadingOverlay.IsVisible = false;
                            loadingOverlay.InputTransparent = true;
                        }
                        await DisplayAlert("Hata", "Mağaza bilgileri bulunamadı.", "Tamam");
                        return;
                    }
                }
                
                // StoreProductsPage'e geri dön - navigation parametreleri ile
                var navigationParameters = new Dictionary<string, object>
                {
                    ["entMemberRouteId"] = _entMemberRouteId,
                    ["StoreName"] = _storeName
                };
                
                System.Diagnostics.Debug.WriteLine($"StoreVitrinePhotoPage: Navigating to StoreProductsPage with parameters: entMemberRouteId={_entMemberRouteId}");
                await Shell.Current.GoToAsync("//StoreProductsPage", navigationParameters);
                
                // Navigation tamamlandı, loading overlay'i gizle (hedef sayfa kendi overlay'ini gösterecek)
                if (loadingOverlay != null)
                {
                    loadingOverlay.IsVisible = false;
                    loadingOverlay.InputTransparent = true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"StoreVitrinePhotoPage: Tamamla gitme hatası: {ex.Message}");
                _logger?.LogError(ex, "Error navigating to StoreProductsPage");
                var loadingOverlay = this.FindByName<Grid>("LoadingOverlay");
                if (loadingOverlay != null)
                {
                    loadingOverlay.IsVisible = false;
                    loadingOverlay.InputTransparent = true;
                }
                await DisplayAlert("Hata", "Sayfa geçişi sırasında hata oluştu.", "Tamam");
            }
        }
    }
}
