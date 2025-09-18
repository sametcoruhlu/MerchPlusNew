using System.Diagnostics;
using Microsoft.Extensions.Logging;
using entMerchPlus;
using MerchPlusMAUI.Models;
using System.Net.Http.Json;
using MerchPlusMAUI.Services;
using System.Net.Http.Headers;
using Microsoft.Maui.Networking;
using SQLite;
using IOPath = System.IO.Path;

namespace MerchPlusMAUI.Views
{
    public partial class SplashScreen : ContentPage
    {
        // Manuel olarak UI elemanlarını tanımlayalım
        private Label? _statusLabel;
        private ProgressBar? _loadingProgressBar;

        private readonly ILogger<SplashScreen> _logger;
        private readonly string _dbPath;
        private readonly IAuthenticationService _authService;
        private readonly IConnectivity _connectivity;
        private CancellationTokenSource _cancellationTokenSource;

        public SplashScreen(ILogger<SplashScreen> logger, IAuthenticationService authService, IConnectivity connectivity)
        {
            try
            {
                InitializeComponent();
                
                // XAML'dan kontrolleri al
                _statusLabel = this.FindByName<Label>("StatusLabel");
                _loadingProgressBar = this.FindByName<ProgressBar>("LoadingProgressBar");
                
                _logger = logger;
                _connectivity = connectivity;
                _dbPath = GlobalVariables.DatabasePath;
                _authService = authService;
                _cancellationTokenSource = new CancellationTokenSource();

                // Durum mesajı olaylarını dinle
                _authService.StatusMessageChanged += (sender, message) => {
                    MainThread.BeginInvokeOnMainThread(() => {
                        if (_statusLabel != null)
                            _statusLabel.Text = message;
                    });
                };

                // Kimlik doğrulama tamamlandığında bildirimi dinle
                _authService.AuthenticationCompleted += (sender, success) => {
                    if (!success)
                    {
                        MainThread.BeginInvokeOnMainThread(async () => {
                            await DisplayAlert("Kullanıcı Doğrulama Hatası", 
                                "Kullanıcı bilgileri doğrulanamadı. Uygulama kapatılacak.", "Tamam");
                            Application.Current.Quit();
                        });
                    }
                };
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SplashScreen initialize hatası: {ex.Message}");
                _logger?.LogError(ex, "SplashScreen başlatma hatası");
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await InitializeAppAsync();
        }
        
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }

        private async Task InitializeAppAsync()
        {
            try
            {
                // Başlangıç durumu
                UpdateStatus("Uygulama başlatılıyor...", 0.1);
                await Task.Delay(500); // Kullanıcının splash ekranını görmesi için kısa bir gecikme

                // Kullanıcı kimlik doğrulama
                UpdateStatus("Kullanıcı doğrulanıyor...", 0.2);
                bool userAuthenticated = await _authService.InitializeUserAsync();
                
                if (!userAuthenticated)
                {
                    UpdateStatus("Kullanıcı doğrulanamadı!", 0.3);
                    await Task.Delay(2000); // Hata mesajını göster
                    Application.Current.Quit();
                    return;
                }

                // Veritabanı kontrolü
                UpdateStatus("Veritabanı kontrol ediliyor...", 0.4);
                await EnsureDatabaseAsync();
                
                // Mağaza bilgilerini çek
                UpdateStatus("Mağaza bilgileri yükleniyor...", 0.6);
                bool storesLoaded = await LoadStoresAsync();
                
                UpdateStatus("Uygulama başlatılıyor...", 0.9);
                await Task.Delay(500);
                UpdateStatus("Tamamlandı", 1.0);
                
                // Kısa bir gecikme sonrası ana sayfaya yönlendir
                await Task.Delay(500);
                
                // Ana sayfaya yönlendir - Modern yöntem (MAUI 9)
                if (Application.Current != null && Application.Current.Windows.Count > 0)
                {
                    Application.Current.Windows[0].Page = new AppShell();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Splash ekranında beklenmeyen hata");
                await DisplayAlert("Hata", "Uygulama başlatılırken bir hata oluştu.", "Tamam");
            }
        }

        private void UpdateStatus(string message, double progress)
        {
            try
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    if (_statusLabel != null)
                        _statusLabel.Text = message;
                    
                    if (_loadingProgressBar != null)
                        _loadingProgressBar.Progress = progress;
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Durum güncellenirken hata oluştu: {Message}", message);
            }
        }

        private async Task EnsureDatabaseAsync()
        {
            try
            {
                // Veritabanı yolu kontrolü
                if (string.IsNullOrEmpty(_dbPath))
                {
                    throw new InvalidOperationException("Veritabanı yolu tanımlı değil");
                }
                
                // Veritabanı dizininin var olduğundan emin ol
                string dbDirectory = IOPath.GetDirectoryName(_dbPath);
                if (!string.IsNullOrEmpty(dbDirectory) && !Directory.Exists(dbDirectory))
                {
                    Directory.CreateDirectory(dbDirectory);
                }
                
                if (!File.Exists(_dbPath))
                {
                    // Veritabanı dosyası yoksa oluştur
                    try
                    {
                        using var db = new SQLiteConnection(_dbPath);
                        
                        // Tabloları oluştur
                        db.CreateTable<MerchPlusMAUI.Models.UserProfile>();
                        db.CreateTable<entMemberRoute>();
                        db.CreateTable<entMemberRouteDetail>();
                        db.CreateTable<entMemberRoutePhoto>();
                        db.CreateTable<entRetailShop>();
                        db.CreateTable<MerchPlusMAUI.Models.entMemberCoordinate>();
                        db.CreateTable<entSalesOrder>();
                        db.CreateTable<entSalesOrderDetail>();
                        db.CreateTable<entProductExpiration>();
                        db.CreateTable<entMemberDisplayPhotos>();
                        
                        UpdateStatus("Veritabanı oluşturuldu", 0.5);
                    }
                    catch (SQLiteException sqlEx)
                    {
                        _logger.LogError(sqlEx, "SQLite veritabanı oluşturma hatası");
                        throw new Exception("Veritabanı oluşturulamadı", sqlEx);
                    }
                }
                else
                {
                    UpdateStatus("Veritabanı kontrol edildi", 0.5);
                }
                
                await Task.Delay(300);
                return;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Veritabanı hazırlanırken hata oluştu");
                throw;
            }
        }

        private async Task<bool> LoadStoresAsync()
        {
            try
            {
                // İnternet bağlantısı kontrolü
                if (_connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    UpdateStatus("İnternet bağlantısı yok, çevrimdışı mağazalar kullanılacak", 0.65);
                    return false;
                }
                
                // Mağaza verilerini sunucudan çekelim
                if (string.IsNullOrEmpty(GlobalVariables.CurrentMemberId))
                {
                    UpdateStatus("Üye bilgisi eksik, mağazalar yüklenemedi", 0.7);
                    await Task.Delay(500);
                    return false;
                }

                UpdateStatus("Mağaza verileri çekiliyor...", 0.7);

                using var client = new HttpClient();
                try
                {
                    // API yolu kontrolü
                    string apiPath = GlobalVariables.APIPath ?? "https://api.example.com/";
                    client.BaseAddress = new Uri(apiPath);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(30); // Timeout ekledim
                    
                    // İstek iptali için cancellation token kullanımı
                    _cancellationTokenSource?.Cancel();
                    _cancellationTokenSource = new CancellationTokenSource();
                    _cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(30));

                    // Önce entRetailShop verilerini çekelim
                    var responseShops = await client.GetAsync(
                        $"RetailShopController/GetRetailShops?memberId={GlobalVariables.CurrentMemberId}", 
                        _cancellationTokenSource.Token);
                    
                    if (responseShops.IsSuccessStatusCode)
                    {
                        var shops = await responseShops.Content.ReadFromJsonAsync<List<entRetailShop>>(
                            cancellationToken: _cancellationTokenSource.Token);
                        
                        if (shops != null && shops.Count > 0)
                        {
                            try
                            {
                                using var db = new SQLiteConnection(_dbPath);
                                db.DeleteAll<entRetailShop>(); // Eski kayıtları temizle
                                
                                foreach (var shop in shops)
                                {
                                    db.Insert(shop);
                                }
                                
                                UpdateStatus("Mağaza bilgileri güncellendi", 0.75);
                                
                                // Şimdi ilişkili rotaları alalım
                                await LoadRoutesAsync();
                                
                                return true;
                            }
                            catch (SQLiteException sqlEx)
                            {
                                _logger.LogError(sqlEx, "Mağaza bilgileri veritabanına kaydedilirken hata");
                                UpdateStatus("Mağaza verilerini kaydetme hatası", 0.75);
                            }
                        }
                    }
                    
                    UpdateStatus("Mağaza bilgileri güncellenemedi", 0.8);
                    await Task.Delay(300);
                    return false;
                }
                catch (TaskCanceledException)
                {
                    _logger.LogWarning("Mağaza bilgileri çekme işlemi zaman aşımına uğradı");
                    UpdateStatus("İstek zaman aşımına uğradı", 0.75);
                    return false;
                }
                catch (HttpRequestException httpEx)
                {
                    _logger.LogError(httpEx, "Mağaza bilgileri HTTP isteği sırasında hata");
                    UpdateStatus("Sunucuya erişim sırasında hata oluştu", 0.75);
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Mağaza bilgileri yüklenirken hata oluştu");
                UpdateStatus("Mağaza bilgileri yüklenemedi", 0.8);
                await Task.Delay(300);
                return false;
            }
        }

        private async Task<bool> LoadRoutesAsync()
        {
            try
            {
                UpdateStatus("Rota bilgileri çekiliyor...", 0.8);

                // İnternet bağlantısı kontrolü
                if (_connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    UpdateStatus("İnternet bağlantısı yok, çevrimdışı rotalar kullanılacak", 0.85);
                    return false;
                }
                
                using var client = new HttpClient();
                try
                {
                    // API yolu kontrolü
                    string apiPath = GlobalVariables.APIPath ?? "https://api.example.com/";
                    client.BaseAddress = new Uri(apiPath);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(30);
                    
                    // İstek iptali için cancellation token kullanımı
                    _cancellationTokenSource?.Cancel();
                    _cancellationTokenSource = new CancellationTokenSource();
                    _cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(30));

                    // API'den rota bilgilerini çek
                    var response = await client.GetAsync(
                        $"MemberRouteController/GetMemberRoutes?memberId={GlobalVariables.CurrentMemberId}",
                        _cancellationTokenSource.Token);
                    
                    if (response.IsSuccessStatusCode)
                    {
                        var routes = await response.Content.ReadFromJsonAsync<List<entMemberRoute>>(
                            cancellationToken: _cancellationTokenSource.Token);
                        
                        if (routes != null && routes.Count > 0)
                        {
                            try
                            {
                                using var db = new SQLiteConnection(_dbPath);
                                db.DeleteAll<entMemberRoute>(); // Eski kayıtları temizle
                                
                                foreach (var route in routes)
                                {
                                    db.Insert(route);
                                }
                                
                                UpdateStatus("Rota bilgileri güncellendi", 0.85);
                                await Task.Delay(300);
                                return true;
                            }
                            catch (SQLiteException sqlEx)
                            {
                                _logger.LogError(sqlEx, "Rota bilgileri veritabanına kaydedilirken hata");
                                UpdateStatus("Rota verilerini kaydetme hatası", 0.85);
                            }
                        }
                    }
                    
                    UpdateStatus("Rota bilgileri güncellenemedi", 0.85);
                    await Task.Delay(300);
                    return false;
                }
                catch (TaskCanceledException)
                {
                    _logger.LogWarning("Rota bilgileri çekme işlemi zaman aşımına uğradı");
                    UpdateStatus("İstek zaman aşımına uğradı", 0.85);
                    return false;
                }
                catch (HttpRequestException httpEx)
                {
                    _logger.LogError(httpEx, "Rota bilgileri HTTP isteği sırasında hata");
                    UpdateStatus("Sunucuya erişim sırasında hata oluştu", 0.85);
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Rota bilgileri yüklenirken hata oluştu");
                return false;
            }
        }
    }
} 