using Microsoft.Maui.Controls;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using SQLite;
using entMerchPlus;

namespace MerchPlusMAUI.Views
{
    public partial class StoreEntryPhotoPage : ContentPage
    {
        private readonly ILogger<StoreEntryPhotoPage> _logger;
        private byte[] _capturedPhotoBytes;
        private string _capturedPhotoPath;
        private string _entMemberRouteId;
        private string _dbPath;

        public StoreEntryPhotoPage(ILogger<StoreEntryPhotoPage> logger)
        {
            InitializeComponent();
            _logger = logger;
            _dbPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "merchplus.db3");
            
            // Navigation parametreleri OnNavigatedTo'da alınacak
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            try
            {
                // GlobalVariables'dan MemberRouteId'yi al (Android Intent.GetStringExtra karşılığı)
                if (GlobalVariables.CurrentMemberRoute != null)
                {
                    _entMemberRouteId = GlobalVariables.CurrentMemberRoute.Id.ToString();
                    System.Diagnostics.Debug.WriteLine($"StoreEntryPhotoPage: MemberRouteId from GlobalVariables: {_entMemberRouteId}");
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
                            System.Diagnostics.Debug.WriteLine($"StoreEntryPhotoPage: Active MemberRouteId from DB: {_entMemberRouteId}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"StoreEntryPhotoPage: Parameter loading error: {ex.Message}");
                _logger?.LogError(ex, "Error loading navigation parameters");
            }
        }

        private async void OnTakePhotoClicked(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("StoreEntryPhotoPage: Fotoğraf çekme başlatılıyor...");

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
                    System.Diagnostics.Debug.WriteLine($"StoreEntryPhotoPage: Fotoğraf çekildi: {photo.FileName}");
                    
                    // Fotoğrafı byte array olarak oku
                    using (var stream = await photo.OpenReadAsync())
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await stream.CopyToAsync(memoryStream);
                            _capturedPhotoBytes = memoryStream.ToArray();
                        }
                    }

                    // Fotoğrafı göster
                    PhotoPreviewImage.Source = ImageSource.FromStream(() => photo.OpenReadAsync().Result);
                    PhotoPreviewFrame.IsVisible = true;
                    TakePhotoButton.IsVisible = false;

                    System.Diagnostics.Debug.WriteLine("StoreEntryPhotoPage: Fotoğraf başarıyla çekildi ve gösterildi");
                    
                    // Otomatik olarak kaydet
                    await SavePhotoToDatabase();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("StoreEntryPhotoPage: Fotoğraf çekme iptal edildi");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"StoreEntryPhotoPage: Fotoğraf çekme hatası: {ex.Message}");
                _logger?.LogError(ex, "Error taking photo");
                await DisplayAlert("Hata", "Fotoğraf çekilirken hata oluştu. Lütfen tekrar deneyin.", "Tamam");
            }
        }

        private async Task SavePhotoToDatabase()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("StoreEntryPhotoPage: Fotoğraf veritabanına kaydediliyor...");

                if (_capturedPhotoBytes == null || string.IsNullOrEmpty(_entMemberRouteId))
                {
                    System.Diagnostics.Debug.WriteLine("StoreEntryPhotoPage: Kaydedilecek fotoğraf veya MemberRouteId bulunamadı");
                    return;
                }

                // Fotoğrafı Base64 string'e çevir (Android kodundaki gibi)
                var base64String = Convert.ToBase64String(_capturedPhotoBytes);
                
                using (var db = new SQLiteConnection(_dbPath))
                {
                    // entMemberRoute tablosunu güncelle
                    var updateResult = db.Execute(
                        "UPDATE entMemberRoute SET EntryImageData = ? WHERE Id = ?", 
                        base64String, 
                        Convert.ToInt32(_entMemberRouteId));
                    
                    System.Diagnostics.Debug.WriteLine($"StoreEntryPhotoPage: Veritabanı güncelleme sonucu: {updateResult} satır etkilendi");
                    
                    if (updateResult > 0)
                    {
                        await DisplayAlert("Başarılı", "Mağaza giriş fotoğrafı başarıyla kaydedildi.", "Tamam");
                        System.Diagnostics.Debug.WriteLine("StoreEntryPhotoPage: Fotoğraf başarıyla veritabanına kaydedildi");
                    }
                    else
                    {
                        await DisplayAlert("Uyarı", "Fotoğraf kaydedilemedi. MemberRouteId bulunamadı.", "Tamam");
                        System.Diagnostics.Debug.WriteLine("StoreEntryPhotoPage: MemberRouteId bulunamadı veya güncelleme başarısız");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"StoreEntryPhotoPage: Veritabanı kaydetme hatası: {ex.Message}");
                _logger?.LogError(ex, "Error saving photo to database");
                await DisplayAlert("Hata", "Fotoğraf kaydedilirken hata oluştu.", "Tamam");
            }
        }

        private async void OnDeletePhotoClicked(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("StoreEntryPhotoPage: Fotoğraf siliniyor...");
                
                var result = await DisplayAlert("Fotoğraf Sil", "Çekilen fotoğrafı silmek istediğinizden emin misiniz?", "Evet", "Hayır");
                if (result)
                {
                    // Fotoğrafı temizle
                    _capturedPhotoBytes = null;
                    PhotoPreviewImage.Source = null;
                    PhotoPreviewFrame.IsVisible = false;
                    TakePhotoButton.IsVisible = true;

                    System.Diagnostics.Debug.WriteLine("StoreEntryPhotoPage: Fotoğraf silindi");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"StoreEntryPhotoPage: Fotoğraf silme hatası: {ex.Message}");
                _logger?.LogError(ex, "Error deleting photo");
                await DisplayAlert("Hata", "Fotoğraf silinirken hata oluştu.", "Tamam");
            }
        }

        private async void OnSavePhotoClicked(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("StoreEntryPhotoPage: Manuel fotoğraf kaydetme...");
                
                if (_capturedPhotoBytes == null)
                {
                    await DisplayAlert("Uyarı", "Kaydedilecek fotoğraf bulunamadı. Lütfen önce fotoğraf çekin.", "Tamam");
                    return;
                }

                await SavePhotoToDatabase();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"StoreEntryPhotoPage: Fotoğraf kaydetme hatası: {ex.Message}");
                _logger?.LogError(ex, "Error saving photo");
                await DisplayAlert("Hata", "Fotoğraf kaydedilirken hata oluştu.", "Tamam");
            }
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("StoreEntryPhotoPage: Geri butonuna tıklandı");
                
                // StoreProductsPage'e geri dön
                await Shell.Current.GoToAsync("//StoreProductsPage");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"StoreEntryPhotoPage: Geri gitme hatası: {ex.Message}");
                _logger?.LogError(ex, "Error navigating back");
                await DisplayAlert("Hata", "Sayfa geçişi sırasında hata oluştu.", "Tamam");
            }
        }

        private async void OnFinishClicked(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("StoreEntryPhotoPage: Tamamla butonuna tıklandı");

                // Android kodundaki gibi CustomerProductList sayfasına yönlendir
                // StoreProductsPage'e geri dön
                await Shell.Current.GoToAsync("//StoreProductsPage");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"StoreEntryPhotoPage: Tamamlama hatası: {ex.Message}");
                _logger?.LogError(ex, "Error finishing photo capture");
                await DisplayAlert("Hata", "İşlem tamamlanırken hata oluştu.", "Tamam");
            }
        }
    }
}
