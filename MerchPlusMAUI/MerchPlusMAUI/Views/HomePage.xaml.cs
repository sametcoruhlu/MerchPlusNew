using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using entMerchPlus;
using MerchPlusMAUI.Entities;
using MerchPlusMAUI.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SQLite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MerchPlusMAUI.Models;
using Microsoft.Maui.Networking;
using System.Linq;

namespace MerchPlusMAUI.Views
{
    public partial class HomePage : ContentPage
    {
        private readonly ILogger<HomePage> _logger;
        private readonly LocationAgentService? _locationService;
        private readonly DatabaseSyncService? _databaseService;
        private readonly HttpClient _httpClient;
        private string? _deviceId;
        private static readonly object _dbLock = new object();
        private Func<Task>? _retryAction;

        // Data to pass to AboutMe page
        public static entMember? CurrentUser { get; private set; }
        public static int PlannedStoresCount { get; private set; }
        public static int CompletedStoresCount { get; private set; }
        public static int PendingStoresCount { get; private set; }

        public HomePage(ILogger<HomePage> logger)
        {
            InitializeComponent();
            _logger = logger;

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(GlobalVariables.APIPath)
            };
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Get services
            var services = IPlatformApplication.Current.Services;
            _locationService = services.GetService<LocationAgentService>();
            _databaseService = services.GetService<DatabaseSyncService>();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await StartInitializationAsync();
        }

        private string DeviceId
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_deviceId))
                    return _deviceId;

                try
                {
                    _logger.LogInformation("Attempting to get device ID");

#if ANDROID
                    try
                    {
                        // Modern way to get device ID in Android using Platform specific context
                        var contentResolver = Platform.CurrentActivity?.ContentResolver ?? Android.App.Application.Context.ContentResolver;
                        if (contentResolver != null)
                        {
                            _deviceId = Android.Provider.Settings.Secure.GetString(contentResolver, Android.Provider.Settings.Secure.AndroidId);
                            _logger.LogInformation($"Android ID retrieved: {_deviceId}");
                        }
                        else
                        {
                            _logger.LogWarning("ContentResolver is null, cannot get Android ID");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error getting Android ID");
                    }

                    if (string.IsNullOrWhiteSpace(_deviceId))
                    {
                        _logger.LogInformation("Android ID is empty, trying serial number");
                        try
                        {
                            // Fallback to serial number if available
                            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
                            {
                                _deviceId = Android.OS.Build.GetSerial();
                                _logger.LogInformation($"Serial number (API 26+) retrieved: {_deviceId}");
                            }
                            else
                            {
                                _deviceId = Android.OS.Build.Serial;
                                _logger.LogInformation($"Serial number (legacy) retrieved: {_deviceId}");
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Error getting device serial number");
                        }

                        // If still no valid ID, use a generated one
                        if (string.IsNullOrWhiteSpace(_deviceId) || _deviceId == Android.OS.Build.Unknown || _deviceId == "0")
                        {
                            _deviceId = Guid.NewGuid().ToString();
                            _logger.LogWarning($"Generated fallback device ID: {_deviceId}");
                        }
                    }
#else
                    _deviceId = Guid.NewGuid().ToString();
                    _logger.LogInformation($"Non-Android platform, generated device ID: {_deviceId}");
#endif

                    if (string.IsNullOrWhiteSpace(_deviceId))
                    {
                        _deviceId = Guid.NewGuid().ToString();
                        _logger.LogWarning($"All device ID methods failed, using fallback GUID: {_deviceId}");
                    }

                    _logger.LogInformation($"Final device ID: {_deviceId}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Critical error getting device ID, using fallback");
                    _deviceId = Guid.NewGuid().ToString();
                }

                return _deviceId;
            }
        }

        private async Task StartInitializationAsync()
        {
            try
            {
                ErrorContainer.IsVisible = false;
                ProgressContainer.IsVisible = true;
                SubtitleLabel.Text = "Uygulama Başlatılıyor...";

                _retryAction = StartInitializationAsync;
                await InitializeAppAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during initialization");
                await ShowError(ex);
            }
        }

        private async Task InitializeAppAsync()
        {
            try
            {
                UpdateProgress(0, "Uygulama Başlatılıyor...");
                await Task.Delay(500); // Give time for UI to update

                _logger.LogInformation("Starting app initialization");

                UpdateProgress(10, "Kullanıcı bilgileri alınıyor...");
                try
                {
                    await GetUser();
                    _logger.LogInformation("User authentication completed successfully");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "User authentication failed");
                    throw new Exception($"Kullanıcı kimlik doğrulama hatası: {ex.Message}", ex);
                }

                if (GlobalVariables.CurrentMember?.Id != null)
                {
                    CurrentUser = GlobalVariables.CurrentMember;
                    _logger.LogInformation($"Current user set: {CurrentUser.NameSurname}");

                    UpdateProgress(25, "Veritabanı kontrol ediliyor...");
                    try
                    {
                        await CheckDatabase();
                        _logger.LogInformation("Database check completed successfully");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Database check failed");
                        throw new Exception($"Veritabanı kontrol hatası: {ex.Message}", ex);
                    }

                    UpdateProgress(40, "Tablolar oluşturuluyor...");
                    try
                    {
                        await CreateTablesForFirstUse();
                        _logger.LogInformation("Database tables created successfully");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Table creation failed");
                        throw new Exception($"Veritabanı tablo oluşturma hatası: {ex.Message}", ex);
                    }

                    UpdateProgress(60, "Veriler güncelleniyor...");
                    try
                    {
                        await GetDataForToday();
                        _logger.LogInformation("Data synchronization completed successfully");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Data synchronization failed");
                        throw new Exception($"Veri senkronizasyon hatası: {ex.Message}", ex);
                    }

                    UpdateProgress(75, "Konum servisi başlatılıyor...");
                    try
                    {
                        if (!GlobalVariables.MemberLocationAgentStarted)
                        {
                            GlobalVariables.MemberLocationAgentStarted = true;
                            if (_locationService != null)
                            {
                                await _locationService.StartLocationTracking();
                                _logger.LogInformation("Location service started successfully");
                            }
                            else
                            {
                                _logger.LogWarning("Location service is null, skipping location tracking");
                            }
                        }
                        else
                        {
                            _logger.LogInformation("Location service already started");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Location service start failed");
                        // Don't throw here, location service failure shouldn't stop the app
                        _logger.LogWarning("Continuing without location service");
                    }

                    UpdateProgress(85, "Veritabanı senkronizasyonu başlatılıyor...");
                    try
                    {
                        if (!GlobalVariables.DatabaseSyncStarted)
                        {
                            GlobalVariables.DatabaseSyncStarted = true;
                            if (_databaseService != null)
                            {
                                // Start the service in background without awaiting
                                _ = Task.Run(async () => await _databaseService.StartSync());
                                _logger.LogInformation("Database sync service started successfully");
                            }
                            else
                            {
                                _logger.LogWarning("Database sync service is null, skipping sync");
                            }
                        }
                        else
                        {
                            _logger.LogInformation("Database sync service already started");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Database sync service start failed");
                        // Don't throw here, sync service failure shouldn't stop the app
                        _logger.LogWarning("Continuing without database sync service");
                    }

                    UpdateProgress(95, "İstatistikler hesaplanıyor...");
                    try
                    {
                        await CalculateStatistics();
                        _logger.LogInformation($"Statistics calculated - Total: {PlannedStoresCount}, Completed: {CompletedStoresCount}, Pending: {PendingStoresCount} (In Progress: {PlannedStoresCount - CompletedStoresCount}, Not Started: {CompletedStoresCount})");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Statistics calculation failed");
                        // Don't throw here, statistics failure shouldn't stop the app
                        _logger.LogWarning("Continuing with default statistics");
                    }

                    UpdateProgress(100, "Tamamlandı! Ana sayfaya yönlendiriliyor...");
                    await Task.Delay(1000); // Show completion message

                    _logger.LogInformation("App initialization completed successfully, navigating to AboutMePage");

                    try
                    {
                        // Basit ve güvenilir navigation
                        _logger.LogInformation("Attempting navigation to AboutMePage");
                        await Shell.Current.GoToAsync("//AboutMePage");
                        _logger.LogInformation("Navigation to AboutMePage completed successfully");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Navigation to AboutMePage failed");
                        await MainThread.InvokeOnMainThreadAsync(async () =>
                        {
                            await DisplayAlert("Hata", 
                                $"Sayfa geçişi başarısız oldu: {ex.Message}", 
                                "Tamam");
                        });
                    }
                }
                else
                {
                    throw new Exception("Kullanıcı kimlik doğrulaması başarısız - CurrentMember is null");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Critical error in InitializeAppAsync");
                throw;
            }
        }

        private async Task<bool> CheckInternetConnectivity()
        {
            try
            {
                _logger.LogInformation("Starting simplified internet connectivity check");
                
                // 1. İlk olarak sistem seviyesinde network durumunu kontrol et
                var connectivity = Connectivity.Current;
                var networkAccess = connectivity.NetworkAccess;
                
                _logger.LogInformation($"System network access status: {networkAccess}");
                
                // Eğer sistem internet yok diyorsa, offline moda geç
                if (networkAccess != NetworkAccess.Internet)
                {
                    _logger.LogWarning("System reports no internet access - switching to offline mode");
                    return false;
                }

                // 2. Sadece en hızlı testleri yap (daha az agresif)
                var connectivityTests = new List<Func<Task<bool>>>
                {
                    // Test 1: DNS çözümleme testi (en hızlı)
                    () => TestDnsResolution(),
                    
                    // Test 2: Google DNS (daha güvenilir)
                    () => TestConnectivityEndpoint("https://8.8.8.8", 5),
                    
                    // Test 3: Kendi API'mizi test et (daha uzun timeout)
                    () => TestApiConnectivity()
                };

                // Sadece ilk başarılı testi bekle (daha hızlı)
                foreach (var test in connectivityTests)
                {
                    try
                    {
                        if (await RunTestSafely(test))
                        {
                            _logger.LogInformation("Internet connectivity confirmed");
                            return true;
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogDebug($"Test failed: {ex.Message}");
                    }
                }

                _logger.LogWarning("Connectivity tests failed - switching to offline mode");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CheckInternetConnectivity - switching to offline mode");
                return false;
            }
        }

        private async Task<bool> RunTestSafely(Func<Task<bool>> test)
        {
            try
            {
                return await test();
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"Test failed safely: {ex.Message}");
                return false;
            }
        }

        private async Task<bool> TestDnsResolution()
        {
            try
            {
                _logger.LogDebug("Testing DNS resolution");
                
                // DNS çözümleme testi - en hızlı yöntem
                var hostEntry = await Dns.GetHostEntryAsync("google.com");
                
                if (hostEntry.AddressList.Length > 0)
                {
                    _logger.LogDebug("DNS resolution successful");
                    return true;
                }
                
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"DNS resolution failed: {ex.Message}");
                return false;
            }
        }

        private async Task<bool> TestConnectivityEndpoint(string url, int timeoutSeconds)
        {
            try
            {
                _logger.LogDebug($"Testing connectivity to: {url}");
                
                using var client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(timeoutSeconds);
                
                // User-Agent ekle - bazı siteler bot trafiğini engeller
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Android; Mobile; rv:13.0) Gecko/13.0 Firefox/13.0");
                
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(timeoutSeconds));
                
                HttpResponseMessage response;
                try
                {
                    // GET request ile test et
                    response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, cts.Token);
                }
                catch (TaskCanceledException) when (cts.Token.IsCancellationRequested)
                {
                    _logger.LogDebug($"Request timeout for {url}");
                    return false;
                }
                catch (HttpRequestException ex)
                {
                    _logger.LogDebug($"HTTP request failed for {url}: {ex.Message}");
                    return false;
                }
                catch (Exception ex)
                {
                    _logger.LogDebug($"Unexpected error for {url}: {ex.Message}");
                    return false;
                }
                
                // Başarılı response kodlarını kontrol et
                var statusCode = (int)response.StatusCode;
                var success = statusCode >= 200 && statusCode < 400;
                
                if (success)
                {
                    _logger.LogDebug($"Connectivity test successful for {url} - Status: {response.StatusCode}");
                }
                else
                {
                    _logger.LogDebug($"Connectivity test failed for {url} - Status: {response.StatusCode}");
                }
                
                return success;
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"Connectivity test exception for {url}: {ex.Message}");
                return false;
            }
        }

        private async Task<bool> TestApiConnectivity()
        {
            try
            {
                _logger.LogDebug("Testing API connectivity");
                
                using var client = new HttpClient();
                client.Timeout = TimeSpan.FromSeconds(10); // Daha uzun timeout
                client.DefaultRequestHeaders.Add("User-Agent", "MerchPlus-Mobile-App");
                
                // Kendi API endpoint'imizi test et
                var baseUrl = GlobalVariables.APIPath?.TrimEnd('/');
                if (string.IsNullOrEmpty(baseUrl))
                {
                    _logger.LogDebug("API base URL is not configured");
                    return false;
                }
                
                var testUrl = $"{baseUrl}/AndroidTestController/SelectMemberByIMEINumber";
                
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(8)); // Daha uzun timeout
                
                try
                {
                    // HEAD request ile API'nin erişilebilir olup olmadığını test et (daha hızlı)
                    var request = new HttpRequestMessage(HttpMethod.Head, testUrl);
                    var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cts.Token);
                    
                    // 200-499 arası tüm kodlar API'nin erişilebilir olduğunu gösterir
                    var statusCode = (int)response.StatusCode;
                    var isAccessible = statusCode < 500;
                    
                    if (isAccessible)
                    {
                        _logger.LogDebug($"API connectivity confirmed - Status: {response.StatusCode}");
                        return true;
                    }
                    else
                    {
                        _logger.LogDebug($"API returned server error - Status: {response.StatusCode}");
                        return false;
                    }
                }
                catch (TaskCanceledException) when (cts.Token.IsCancellationRequested)
                {
                    _logger.LogDebug("API connectivity test timed out");
                    return false;
                }
                catch (HttpRequestException ex)
                {
                    _logger.LogDebug($"API HTTP error: {ex.Message}");
                    
                    // Daha fazla HTTP hatası API'nin erişilebilir olduğunu gösterir
                    if (ex.Message.Contains("405") || ex.Message.Contains("404") || ex.Message.Contains("401") || 
                        ex.Message.Contains("403") || ex.Message.Contains("400"))
                    {
                        _logger.LogDebug("API is accessible but returned client error (expected)");
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    _logger.LogDebug($"API connectivity test failed: {ex.Message}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"API connectivity test exception: {ex.Message}");
                return false;
            }
        }

        private async Task<NetworkConnectivityStatus> GetDetailedNetworkStatus()
        {
            try
            {
                var connectivity = Connectivity.Current;
                var networkAccess = connectivity.NetworkAccess;
                var profiles = connectivity.ConnectionProfiles;
                
                var status = new NetworkConnectivityStatus
                {
                    NetworkAccess = networkAccess,
                    ConnectionProfiles = profiles.ToList(),
                    HasWiFi = profiles.Contains(ConnectionProfile.WiFi),
                    HasCellular = profiles.Contains(ConnectionProfile.Cellular),
                    HasEthernet = profiles.Contains(ConnectionProfile.Ethernet),
                    IsRoaming = false // MAUI'de roaming bilgisi direkt yok
                };

                _logger.LogInformation($"Network Status - Access: {networkAccess}, WiFi: {status.HasWiFi}, Cellular: {status.HasCellular}");
                
                return status;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting detailed network status");
                return new NetworkConnectivityStatus
                {
                    NetworkAccess = NetworkAccess.None,
                    ConnectionProfiles = new List<ConnectionProfile>()
                };
            }
        }

        private async Task<bool> IsNetworkAvailable()
        {
            try
            {
                _logger.LogInformation("Checking network availability");
                
                // Hızlı sistem kontrolü
                var connectivity = Connectivity.Current;
                var networkAccess = connectivity.NetworkAccess;
                
                _logger.LogInformation($"System network access: {networkAccess}");
                
                // Eğer sistem internet yok diyorsa
                if (networkAccess != NetworkAccess.Internet)
                {
                    _logger.LogWarning("System reports no internet access");
                    return false;
                }
                
                // Sistem internet var diyor, gerçek bağlantıyı test et
                _logger.LogInformation("System reports internet access, testing actual connectivity");
                
                // Geliştirilmiş connectivity check kullan
                var hasRealInternet = await CheckInternetConnectivity();
                
                if (hasRealInternet)
                {
                    _logger.LogInformation("Real internet connectivity confirmed");
                    return true;
                }
                else
                {
                    _logger.LogWarning("System reports internet but actual connectivity test failed");
                    
                    // Detaylı network bilgilerini al
                    var networkStatus = await GetDetailedNetworkStatus();
                    _logger.LogInformation($"Detailed network status: {networkStatus}");
                    
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking network availability");
                return false;
            }
        }

        private async Task GetUser()
        {
            try
            {
                if (GlobalVariables.CurrentMember?.Id != null)
                {
                    _logger.LogInformation("User already exists, loading profile photo if needed");
                    // Load default profile photo if needed
                    if (string.IsNullOrEmpty(GlobalVariables.CurrentMember.ProfilePicturePath))
                    {
                        await LoadDefaultProfilePhoto();
                    }
                    return;
                }

                // Önce internet bağlantısı kontrol et
                _logger.LogInformation("Checking network connectivity for user authentication");
                
                // Hızlı sistem kontrolü
                var connectivity = Connectivity.Current;
                var networkAccess = connectivity.NetworkAccess;
                
                _logger.LogInformation($"System network access: {networkAccess}");
                
                if (networkAccess != NetworkAccess.Internet)
                {
                    _logger.LogWarning("System reports no internet access, trying cached user");
                    
                    // Sistem internet yok diyor, cached user dene
                    await TryLoadCachedUser();
                    if (GlobalVariables.CurrentMember?.Id != null)
                    {
                        _logger.LogInformation("Using cached user data (no system internet)");
                        await MainThread.InvokeOnMainThreadAsync(async () =>
                        {
                            await DisplayAlert("Offline Mod", 
                                "İnternet bağlantısı bulunamadı. Kayıtlı kullanıcı bilgileri ile devam ediliyor.", 
                                "Tamam");
                        });
                        return;
                    }
                    
                    throw new Exception("İnternet bağlantısı bulunamadı ve kayıtlı kullanıcı bilgisi yok.\n\n" +
                                      "Lütfen internet bağlantınızı kontrol edin ve tekrar deneyin.");
                }

                // Sistem internet var diyor, gerçek bağlantıyı test et
                _logger.LogInformation("System reports internet access, testing actual connectivity");
                bool hasInternet = false;
                
                try
                {
                    // Geliştirilmiş connectivity check - makul timeout
                    using var connectivityCts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
                    var connectivityTask = CheckInternetConnectivity();
                    
                    hasInternet = await connectivityTask.WaitAsync(connectivityCts.Token);
                    
                    if (hasInternet)
                    {
                        _logger.LogInformation("Real internet connectivity confirmed - proceeding with online authentication");
                    }
                    else
                    {
                        _logger.LogWarning("Real internet connectivity test failed, will try cached user");
                    }
                }
                catch (TimeoutException)
                {
                    _logger.LogWarning("Internet connectivity check timed out, will try cached user");
                    hasInternet = false;
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Error during internet connectivity check, will try cached user");
                    hasInternet = false;
                }

                if (!hasInternet)
                {
                    _logger.LogWarning("Internet connectivity check failed, attempting offline fallback");
                    
                    // Son bir kez cached user'ı dene
                    await TryLoadCachedUser();
                    if (GlobalVariables.CurrentMember?.Id != null)
                    {
                        _logger.LogInformation("Successfully using cached user data after connectivity failure");
                        await MainThread.InvokeOnMainThreadAsync(async () =>
                        {
                            // Bağlantı türünü belirlemek için ConnectionProfile kullan
                            var connectionProfiles = connectivity.ConnectionProfiles;
                            var hasWiFi = connectionProfiles.Contains(ConnectionProfile.WiFi);
                            var hasCellular = connectionProfiles.Contains(ConnectionProfile.Cellular);
                            
                            var connectionInfo = hasWiFi ? "Wi-Fi bağlantısı mevcut ancak internet erişimi yok" :
                                               hasCellular ? "Mobil veri bağlantısı mevcut ancak internet erişimi yok" :
                                               "İnternet bağlantısı tespit edilemedi";
                            
                            await DisplayAlert("Bağlantı Sorunu", 
                                $"{connectionInfo}.\n\nKayıtlı kullanıcı bilgileri ile devam ediliyor.", 
                                "Tamam");
                        });
                        return;
                    }
                    
                    // Offline mode da mümkün değilse, kullanıcıya detaylı bilgi ver
                    var connectionProfiles = connectivity.ConnectionProfiles;
                    var hasWiFi = connectionProfiles.Contains(ConnectionProfile.WiFi);
                    var hasCellular = connectionProfiles.Contains(ConnectionProfile.Cellular);
                    
                    var troubleshootingInfo = hasWiFi ? 
                        "Wi-Fi bağlantınız var ancak internet erişimi yok. Modem/router'ınızı kontrol edin." :
                        hasCellular ? 
                        "Mobil veri bağlantınız var ancak internet erişimi yok. Operatör ayarlarınızı kontrol edin." :
                        "Hiçbir ağ bağlantısı tespit edilemedi. Wi-Fi veya mobil veri bağlantınızı açın.";
                    
                    throw new Exception($"İnternet bağlantısı bulunamadı ve kayıtlı kullanıcı bilgisi yok.\n\n" +
                                      $"{troubleshootingInfo}\n\n" +
                                      "Tekrar denemek için uygulamayı yeniden başlatın.");
                }

                // İnternet bağlantısı var, online authentication yap
                _logger.LogInformation("Getting device ID for user authentication");
                var id = DeviceId;
                _logger.LogInformation($"Device ID retrieved: {id}");

                if (string.IsNullOrEmpty(id))
                {
                    throw new Exception("Cihaz kimliği alınamadı");
                }

                var insEntMember = new entMember { IMEINumber = id };
                var jsonContent = JsonConvert.SerializeObject(insEntMember);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                _logger.LogInformation($"Making API request to: {_httpClient.BaseAddress}AndroidTestController/SelectMemberByIMEINumber");
                _logger.LogInformation($"Request payload: {jsonContent}");

                HttpResponseMessage response;
                try
                {
                    // API request için makul bir timeout
                    using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(20));
                    response = await _httpClient.PostAsync("AndroidTestController/SelectMemberByIMEINumber", content, cts.Token);
                }
                catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
                {
                    _logger.LogError(ex, "API request timed out");
                    
                    // Timeout durumunda offline mode'a geç
                    await TryLoadCachedUser();
                    if (GlobalVariables.CurrentMember?.Id != null)
                    {
                        _logger.LogInformation("Using cached user data after API timeout");
                        await MainThread.InvokeOnMainThreadAsync(async () =>
                        {
                            await DisplayAlert("Sunucu Bağlantı Sorunu", 
                                "Sunucu yanıt vermiyor. Kayıtlı kullanıcı bilgileri ile devam ediliyor.", 
                                "Tamam");
                        });
                        return;
                    }
                    
                    throw new Exception("Sunucu bağlantısı zaman aşımına uğradı ve kayıtlı kullanıcı bilgisi yok. Lütfen daha sonra tekrar deneyin.");
                }
                catch (HttpRequestException ex)
                {
                    _logger.LogError(ex, "HTTP request failed");
                    
                    // HTTP error durumunda offline mode'a geç
                    await TryLoadCachedUser();
                    if (GlobalVariables.CurrentMember?.Id != null)
                    {
                        _logger.LogInformation("Using cached user data after HTTP error");
                        await MainThread.InvokeOnMainThreadAsync(async () =>
                        {
                            await DisplayAlert("Sunucu Hatası", 
                                "Sunucu ile iletişim kurulamadı. Kayıtlı kullanıcı bilgileri ile devam ediliyor.", 
                                "Tamam");
                        });
                        return;
                    }
                    
                    throw new Exception($"Sunucu bağlantısı başarısız: {ex.Message}");
                }

                _logger.LogInformation($"API response status: {response.StatusCode}");

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"API returned error status: {response.StatusCode}, Content: {errorContent}");
                    throw new Exception($"Sunucu hatası: {response.StatusCode} - {response.ReasonPhrase}");
                }

                var responseString = await response.Content.ReadAsStringAsync();
                _logger.LogInformation($"API response content: {responseString}");

                if (string.IsNullOrEmpty(responseString))
                {
                    throw new Exception("Sunucudan boş yanıt alındı");
                }

                JObject returnObject;
                try
                {
                    returnObject = JObject.Parse(responseString);
                }
                catch (JsonException ex)
                {
                    _logger.LogError(ex, $"Failed to parse JSON response: {responseString}");
                    throw new Exception("Sunucu yanıtı geçersiz format");
                }

                var result = returnObject["Result"]?.ToString();
                _logger.LogInformation($"API result: {result}");

                if (result == "NOK")
                {
                    var errorMessage = returnObject["Message"]?.ToString() ?? "Bilinmeyen hata";
                    _logger.LogWarning($"API returned NOK result: {errorMessage}");
                    throw new Exception($"Kullanıcı doğrulama başarısız: {errorMessage}");
                }

                if (result == "OK")
                {
                    try
                    {
                        GlobalVariables.CurrentMember = returnObject["Content"]?.ToObject<entMember>();
                        _logger.LogInformation($"User found: {GlobalVariables.CurrentMember?.NameSurname}");
                        
                        // Cache user data for offline use
                        await CacheUserData(GlobalVariables.CurrentMember);
                    }
                    catch (JsonException ex)
                    {
                        _logger.LogError(ex, "Failed to deserialize user data");
                        throw new Exception("Kullanıcı verisi işlenirken hata oluştu");
                    }

                    // Load default profile photo for new user
                    if (GlobalVariables.CurrentMember != null && string.IsNullOrEmpty(GlobalVariables.CurrentMember.ProfilePicturePath))
                    {
                        await LoadDefaultProfilePhoto();
                    }
                }
                else if (result != "NOK")
                {
                    // Handle case where Result is neither OK nor NOK (legacy API response format)
                    try
                    {
                        GlobalVariables.CurrentMember = returnObject["Content"]?.ToObject<entMember>();
                        _logger.LogInformation($"User found (legacy format): {GlobalVariables.CurrentMember?.NameSurname}");
                        
                        // Cache user data for offline use
                        await CacheUserData(GlobalVariables.CurrentMember);
                    }
                    catch (JsonException ex)
                    {
                        _logger.LogError(ex, "Failed to deserialize user data (legacy format)");
                        throw new Exception("Kullanıcı verisi işlenirken hata oluştu");
                    }

                    // Load default profile photo for new user
                    if (GlobalVariables.CurrentMember != null && string.IsNullOrEmpty(GlobalVariables.CurrentMember.ProfilePicturePath))
                    {
                        await LoadDefaultProfilePhoto();
                    }
                }

                if (GlobalVariables.CurrentMember?.Id == null)
                {
                    _logger.LogWarning($"No user found for device ID: {id}");
                    throw new Exception($"Cihazınıza ait {id} numarası hiçbir personel ile eşleştirilmemiş. Lütfen sistem yöneticinizle iletişime geçin.");
                }

                _logger.LogInformation("User authentication successful");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetUser method");
                
                // Check if it's a network connectivity issue
                if (ex is HttpRequestException || ex is TaskCanceledException)
                {
                    throw new Exception($"Ağ bağlantısı hatası: {ex.Message}\n\nLütfen internet bağlantınızı kontrol edin ve tekrar deneyin.", ex);
                }
                
                // Re-throw with more context if it's not already a user-friendly message
                if (ex.Message.Contains("Kullanıcı bilgileri alınırken hata oluştu"))
                {
                    throw;
                }
                
                throw new Exception($"Kullanıcı bilgileri alınırken hata oluştu: {ex.Message}", ex);
            }
        }

        private async Task TryLoadCachedUser()
        {
            try
            {
                using var db = new SQLiteConnection(GlobalVariables.DatabasePath);
                
                // Check if entMember table exists and has data
                var tableExists = db.ExecuteScalar<int>("SELECT COUNT(name) FROM sqlite_master WHERE type='table' AND name='entMember'");
                if (tableExists == 0)
                {
                    _logger.LogInformation("entMember table doesn't exist, cannot load cached user");
                    return;
                }

                var deviceId = DeviceId;
                if (string.IsNullOrEmpty(deviceId))
                {
                    _logger.LogWarning("Cannot load cached user without device ID");
                    return;
                }

                // Try to find cached user by IMEI
                var cachedUser = db.Table<entMember>().FirstOrDefault(m => m.IMEINumber == deviceId);
                if (cachedUser != null)
                {
                    GlobalVariables.CurrentMember = cachedUser;
                    _logger.LogInformation($"Loaded cached user: {cachedUser.NameSurname}");
                    
                    // Load default profile photo if needed
                    if (string.IsNullOrEmpty(cachedUser.ProfilePicturePath))
                    {
                        await LoadDefaultProfilePhoto();
                    }
                }
                else
                {
                    _logger.LogInformation("No cached user found for this device");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading cached user data");
            }
        }

        private async Task CacheUserData(entMember user)
        {
            try
            {
                if (user == null) return;

                using var db = new SQLiteConnection(GlobalVariables.DatabasePath);
                
                // Create entMember table if it doesn't exist
                db.CreateTable<entMember>();
                
                // Delete existing user data for this device
                if (!string.IsNullOrEmpty(user.IMEINumber))
                {
                    db.Execute("DELETE FROM entMember WHERE IMEINumber = ?", user.IMEINumber);
                }
                
                // Insert new user data
                db.Insert(user);
                _logger.LogInformation($"Cached user data for: {user.NameSurname}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error caching user data");
            }
        }

        private async Task LoadDefaultProfilePhoto()
        {
            try
            {
                var defaultPhotoPath = "defaultprofilphoto.png";
                var assembly = typeof(HomePage).Assembly;
                using var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.Resources.Images.{defaultPhotoPath}");
                if (stream != null)
                {
                    using var memoryStream = new MemoryStream();
                    await stream.CopyToAsync(memoryStream);
                    var imageBytes = memoryStream.ToArray();
                    if (GlobalVariables.CurrentMember != null)
                    {
                        GlobalVariables.CurrentMember.ProfilePicturePath = Convert.ToBase64String(imageBytes);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading default profile photo");
            }
        }

        private async Task CheckDatabase()
        {
            try
            {
                _logger.LogInformation("Starting database check");
                var startTime = DateTime.Now;

                await Task.Run(() =>
                {
                    try
                    {
                        var dbFile = new FileInfo(GlobalVariables.DatabasePath);
                        if (!dbFile.Exists)
                        {
                            _logger.LogInformation("Database file doesn't exist, will be created");
                            using var db = new SQLiteConnection(GlobalVariables.DatabasePath);
                            // Database will be created automatically
                            _logger.LogInformation("Database file created successfully");
                        }
                        else
                        {
                            _logger.LogInformation($"Database file exists, size: {dbFile.Length} bytes");
                            // Quick connection test
                            using var db = new SQLiteConnection(GlobalVariables.DatabasePath);
                            var result = db.ExecuteScalar<int>("SELECT 1");
                            if (result == 1)
                            {
                                _logger.LogInformation("Database connection test successful");
                            }
                        }
                    }
                    catch (SQLite.SQLiteException sqlEx)
                    {
                        _logger.LogError(sqlEx, "SQLite Error during database check");
                        throw new Exception($"Veritabanı bağlantı hatası: {sqlEx.Message}", sqlEx);
                    }
                    catch (UnauthorizedAccessException accessEx)
                    {
                        _logger.LogError(accessEx, "Database Access Error");
                        throw new Exception($"Veritabanı erişim hatası: {accessEx.Message}", accessEx);
                    }
                    catch (System.IO.IOException ioEx)
                    {
                        _logger.LogError(ioEx, "Database IO Error");
                        throw new Exception($"Veritabanı dosya hatası: {ioEx.Message}", ioEx);
                    }
                });

                var duration = DateTime.Now - startTime;
                _logger.LogInformation($"Database check completed in {duration.TotalMilliseconds}ms");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CheckDatabase");
                throw new Exception($"Veritabanı kontrol hatası: {ex.Message}", ex);
            }
        }

        private async Task CreateTablesForFirstUse()
        {
            try
            {
                _logger.LogInformation("Starting database table creation");
                var startTime = DateTime.Now;

                await Task.Run(() =>
                {
                    using var db = new SQLiteConnection(GlobalVariables.DatabasePath);
                    
                    // Use safer PRAGMA settings that don't cause issues
                    try
                    {
                        db.Execute("PRAGMA cache_size=10000");
                        db.Execute("PRAGMA temp_store=MEMORY");
                        _logger.LogDebug("Database PRAGMA settings applied successfully");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "Some PRAGMA settings failed, continuing with defaults");
                    }

                    #region MemberRoute
                    try
                    {
                        lock (_dbLock)
                        {
                            var result = db.ExecuteScalar<int>("SELECT COUNT(name) FROM sqlite_master WHERE name = 'entMemberRoute'");
                            if (result == 0)
                            {
                                MainThread.BeginInvokeOnMainThread(() => UpdateProgress(42, "Ziyaret Planı tablosu oluşturuluyor"));
                                db.CreateTable<entMemberRoute>();
                                _logger.LogDebug("entMemberRoute table created");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error creating entMemberRoute table");
                        throw;
                    }
                    #endregion

                    #region MemberRouteDetail
                    try
                    {
                        lock (_dbLock)
                        {
                            var result = db.ExecuteScalar<int>("SELECT COUNT(name) FROM sqlite_master WHERE name = 'entMemberRouteDetail'");
                            if (result == 0)
                            {
                                MainThread.BeginInvokeOnMainThread(() => UpdateProgress(44, "Ziyaret Planı Detay tablosu oluşturuluyor"));
                                db.CreateTable<entMemberRouteDetail>();
                                _logger.LogDebug("entMemberRouteDetail table created");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error creating entMemberRouteDetail table");
                        throw;
                    }
                    #endregion

                    #region MemberLocation
                    try
                    {
                        lock (_dbLock)
                        {
                            var result = db.ExecuteScalar<int>("SELECT COUNT(name) FROM sqlite_master WHERE name = 'entMemberCoordinate'");
                            if (result == 0)
                            {
                                MainThread.BeginInvokeOnMainThread(() => UpdateProgress(46, "Koordinat tablosu oluşturuluyor"));
                                db.CreateTable<MerchPlusMAUI.Models.entMemberCoordinate>();
                                _logger.LogDebug("entMemberCoordinate table created");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error creating entMemberCoordinate table");
                        throw;
                    }
                    #endregion

                    #region RetailProfilePicture
                    try
                    {
                        lock (_dbLock)
                        {
                            var result = db.ExecuteScalar<int>("SELECT COUNT(name) FROM sqlite_master WHERE name = 'entRetailProfilePicture'");
                            if (result == 0)
                            {
                                db.CreateTable<MerchPlusMAUI.Models.entRetailProfilePicture>();
                                _logger.LogDebug("entRetailProfilePicture table created");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error creating entRetailProfilePicture table");
                        throw;
                    }
                    #endregion

                    #region CustomerProductPicture
                    try
                    {
                        lock (_dbLock)
                        {
                            var result = db.ExecuteScalar<int>("SELECT COUNT(name) FROM sqlite_master WHERE name = 'entCustomerProductPicture'");
                            if (result == 0)
                            {
                                db.CreateTable<entCustomerProductPicture>();
                                _logger.LogDebug("entCustomerProductPicture table created");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error creating entCustomerProductPicture table");
                        throw;
                    }
                    #endregion

                    #region RetailShop
                    try
                    {
                        lock (_dbLock)
                        {
                            var result = db.ExecuteScalar<int>("SELECT COUNT(name) FROM sqlite_master WHERE name = 'entRetailShop'");
                            if (result == 0)
                            {
                                MainThread.BeginInvokeOnMainThread(() => UpdateProgress(48, "Mağaza tablosu oluşturuluyor"));
                                db.CreateTable<entRetailShop>();
                                _logger.LogDebug("entRetailShop table created");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error creating entRetailShop table");
                        throw;
                    }
                    #endregion

                    #region Retail
                    try
                    {
                        lock (_dbLock)
                        {
                            var result = db.ExecuteScalar<int>("SELECT COUNT(name) FROM sqlite_master WHERE name = 'entRetail'");
                            if (result == 0)
                            {
                                db.CreateTable<entRetail>();
                                _logger.LogDebug("entRetail table created");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error creating entRetail table");
                        throw;
                    }
                    #endregion

                    #region CustomerProduct
                    try
                    {
                        lock (_dbLock)
                        {
                            var result = db.ExecuteScalar<int>("SELECT COUNT(name) FROM sqlite_master WHERE name = 'entCustomerProduct'");
                            if (result == 0)
                            {
                                db.CreateTable<entCustomerProduct>();
                                _logger.LogDebug("entCustomerProduct table created");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error creating entCustomerProduct table");
                        throw;
                    }
                    #endregion

                    #region CustomerBrandCategory
                    try
                    {
                        lock (_dbLock)
                        {
                            var result = db.ExecuteScalar<int>("SELECT COUNT(name) FROM sqlite_master WHERE name = 'entCustomerBrandCategory'");
                            if (result == 0)
                            {
                                db.CreateTable<entCustomerBrandCategory>();
                                _logger.LogDebug("entCustomerBrandCategory table created");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error creating entCustomerBrandCategory table");
                        throw;
                    }
                    #endregion

                    #region SalesOrder
                    try
                    {
                        lock (_dbLock)
                        {
                            var result = db.ExecuteScalar<int>("SELECT COUNT(name) FROM sqlite_master WHERE name = 'entSalesOrder'");
                            if (result == 0)
                            {
                                db.CreateTable<entSalesOrder>();
                                _logger.LogDebug("entSalesOrder table created");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error creating entSalesOrder table");
                        throw;
                    }
                    #endregion

                    #region SalesOrderDetail
                    try
                    {
                        lock (_dbLock)
                        {
                            var result = db.ExecuteScalar<int>("SELECT COUNT(name) FROM sqlite_master WHERE name = 'entSalesOrderDetail'");
                            if (result == 0)
                            {
                                db.CreateTable<entSalesOrderDetail>();
                                _logger.LogDebug("entSalesOrderDetail table created");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error creating entSalesOrderDetail table");
                        throw;
                    }
                    #endregion

                    #region SalesOrderPackageType
                    try
                    {
                        lock (_dbLock)
                        {
                            var result = db.ExecuteScalar<int>("SELECT COUNT(name) FROM sqlite_master WHERE name = 'entSalesOrderPackageType'");
                            if (result == 0)
                            {
                                db.CreateTable<entSalesOrderPackageType>();
                                _logger.LogDebug("entSalesOrderPackageType table created");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error creating entSalesOrderPackageType table");
                        throw;
                    }
                    #endregion

                    #region MemberRoutePhoto
                    try
                    {
                        lock (_dbLock)
                        {
                            var result = db.ExecuteScalar<int>("SELECT COUNT(name) FROM sqlite_master WHERE name = 'entMemberRoutePhoto'");
                            if (result == 0)
                            {
                                db.CreateTable<entMemberRoutePhoto>();
                                
                                if (GlobalVariables.MemberRoutePhotoDataTable == null)
                                {
                                    GlobalVariables.MemberRoutePhotoDataTable = new System.Data.DataTable();
                                    GlobalVariables.MemberRoutePhotoDataTable.Columns.Add("Data", typeof(entMemberRoutePhoto));
                                    GlobalVariables.MemberRoutePhotoDataTable.AcceptChanges();
                                }
                                _logger.LogDebug("entMemberRoutePhoto table and DataTable created");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error creating entMemberRoutePhoto table");
                        throw;
                    }
                    #endregion

                    #region ProductExpiration
                    try
                    {
                        lock (_dbLock)
                        {
                            var result = db.ExecuteScalar<int>("SELECT COUNT(name) FROM sqlite_master WHERE name = 'entProductExpiration'");
                            if (result == 0)
                            {
                                db.CreateTable<entProductExpiration>();
                                _logger.LogDebug("entProductExpiration table created");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error creating entProductExpiration table");
                        throw;
                    }
                    #endregion

                    #region PromotionType
                    try
                    {
                        lock (_dbLock)
                        {
                            var result = db.ExecuteScalar<int>("SELECT COUNT(name) FROM sqlite_master WHERE name = 'entPromotionType'");
                            if (result == 0)
                            {
                                db.CreateTable<entPromotionType>();
                                _logger.LogDebug("entPromotionType table created");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error creating entPromotionType table");
                        throw;
                    }
                    #endregion

                    #region MemberDisplayPhotos
                    try
                    {
                        lock (_dbLock)
                        {
                            var result = db.ExecuteScalar<int>("SELECT COUNT(name) FROM sqlite_master WHERE name = 'entMemberDisplayPhotos'");
                            if (result == 0)
                            {
                                db.CreateTable<entMemberDisplayPhotos>();
                                _logger.LogDebug("entMemberDisplayPhotos table created");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error creating entMemberDisplayPhotos table");
                        throw;
                    }
                    #endregion

                    #region Configuration
                    try
                    {
                        lock (_dbLock)
                        {
                            var result = db.ExecuteScalar<int>("SELECT COUNT(name) FROM sqlite_master WHERE name = 'entConfiguration'");
                            if (result == 0)
                            {
                                db.CreateTable<MerchPlusMAUI.Models.entConfiguration>();

                                var tomorrow = DateTime.Now.AddDays(1).ToString("yyyyMMdd");
                                var configurations = new[]
                                {
                                    "RetailDataDateTime",
                                    "RetailShopDataDateTime",
                                    "MemberRouteDetailDataDateTime",
                                    "MemberRouteDataDateTime",
                                    "CustomerProductDataDateTime",
                                    "CustomerBrandCategoryDataDateTime",
                                    "SalesOrderDataDateTime",
                                    "SalesOrderDetailDataDateTime",
                                    "SalesOrderPackageTypeDataDateTime",
                                    "PromotionTypeDataDateTime",
                                    "MemberDisplayPhotosDataDateTime"
                                };

                                foreach (var configName in configurations)
                                {
                                    var config = new MerchPlusMAUI.Models.entConfiguration
                                    {
                                        Name = configName,
                                        Value = tomorrow
                                    };
                                    db.Insert(config);
                                }
                                _logger.LogDebug($"entConfiguration table created with {configurations.Length} records");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error creating entConfiguration table");
                        throw;
                    }
                    #endregion

                    // Clean up specific records
                    try
                    {
                        lock (_dbLock)
                        {
                            var deletedProducts = db.Execute("DELETE FROM entCustomerProduct WHERE Id=?", 208);
                            var deletedRouteDetails = db.Execute("DELETE FROM entMemberRouteDetail WHERE CustomerProductId=?", 208);
                            _logger.LogDebug($"Cleanup completed - deleted {deletedProducts} products, {deletedRouteDetails} route details");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "Error during cleanup operations");
                    }
                });

                var duration = DateTime.Now - startTime;
                _logger.LogInformation($"Database table creation completed in {duration.TotalMilliseconds}ms");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in CreateTablesForFirstUse");
                throw new Exception($"Tablo oluşturma hatası: {ex.Message}", ex);
            }
        }

        private async Task GetDataForToday()
        {
            try
            {
                _logger.LogInformation("Starting optimized data synchronization");
                var startTime = DateTime.Now;

                await Task.Run(async () =>
                {
                    using var db = new SQLiteConnection(GlobalVariables.DatabasePath);
                    
                    // Apply database optimizations once
                    try
                    {
                        db.Execute("PRAGMA journal_mode=WAL");
                        db.Execute("PRAGMA synchronous=NORMAL");
                        db.Execute("PRAGMA cache_size=10000");
                        db.Execute("PRAGMA temp_store=MEMORY");
                        _logger.LogDebug("Database performance settings applied");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "Performance settings failed, continuing with defaults");
                    }

                    try
                    {
                        // Create all data fetching tasks in parallel
                        var dataFetchTasks = new List<Task<(string DataType, object Data, List<string> ImageUrls)>>();

                        // Check which data needs to be updated
                        var dataTypesToUpdate = await GetDataTypesToUpdate(db);
                        
                        if (dataTypesToUpdate.Contains("Retail"))
                        {
                            dataFetchTasks.Add(FetchRetailDataAsync());
                        }
                        
                        if (dataTypesToUpdate.Contains("RetailShop"))
                        {
                            dataFetchTasks.Add(FetchRetailShopDataAsync());
                        }
                        
                        if (dataTypesToUpdate.Contains("MemberRoute"))
                        {
                            dataFetchTasks.Add(FetchMemberRouteDataAsync());
                        }
                        
                        if (dataTypesToUpdate.Contains("MemberRouteDetail"))
                        {
                            dataFetchTasks.Add(FetchMemberRouteDetailDataAsync());
                        }
                        
                        if (dataTypesToUpdate.Contains("CustomerBrandCategory"))
                        {
                            dataFetchTasks.Add(FetchCustomerBrandCategoryDataAsync());
                        }
                        
                        if (dataTypesToUpdate.Contains("CustomerProduct"))
                        {
                            dataFetchTasks.Add(FetchCustomerProductDataAsync());
                        }
                        
                        if (dataTypesToUpdate.Contains("SalesOrderPackageType"))
                        {
                            dataFetchTasks.Add(FetchSalesOrderPackageTypeDataAsync());
                        }
                        
                        if (dataTypesToUpdate.Contains("PromotionType"))
                        {
                            dataFetchTasks.Add(FetchPromotionTypeDataAsync());
                        }

                        if (dataFetchTasks.Count == 0)
                        {
                            _logger.LogInformation("All data is up to date, skipping synchronization");
                            return;
                        }

                        MainThread.BeginInvokeOnMainThread(() => UpdateProgress(65, "Veriler paralel olarak alınıyor..."));

                        // Execute all API calls in parallel
                        var results = await Task.WhenAll(dataFetchTasks);

                        MainThread.BeginInvokeOnMainThread(() => UpdateProgress(75, "Veriler veritabanına yazılıyor..."));

                        // Process all results in a single transaction for better performance
                        await ProcessDataResults(db, results);

                        MainThread.BeginInvokeOnMainThread(() => UpdateProgress(85, "Resimler indiriliyor..."));

                        // Download images in parallel batches
                        await DownloadImagesInParallel(results);

                        _logger.LogInformation("Optimized data sync completed");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error during optimized data synchronization");
                        throw;
                    }
                });

                var duration = DateTime.Now - startTime;
                _logger.LogInformation($"Optimized data synchronization completed in {duration.TotalMilliseconds}ms");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in optimized GetDataForToday");
                // Don't throw here - data sync issues shouldn't prevent app startup
                _logger.LogWarning("Continuing without full data sync - background service will handle remaining data");
            }
        }

        private async Task<List<string>> GetDataTypesToUpdate(SQLiteConnection db)
        {
            var dataTypesToUpdate = new List<string>();
            var today = DateTime.Now.ToString("yyyyMMdd");

            var dataTypes = new Dictionary<string, string>
            {
                ["Retail"] = "RetailDataDateTime",
                ["RetailShop"] = "RetailShopDataDateTime", 
                ["MemberRoute"] = "MemberRouteDataDateTime",
                ["MemberRouteDetail"] = "MemberRouteDetailDataDateTime",
                ["CustomerBrandCategory"] = "CustomerBrandCategoryDataDateTime",
                ["CustomerProduct"] = "CustomerProductDataDateTime",
                ["SalesOrderPackageType"] = "SalesOrderPackageTypeDataDateTime",
                ["PromotionType"] = "PromotionTypeDataDateTime"
            };

            foreach (var dataType in dataTypes)
            {
                var config = db.Table<MerchPlusMAUI.Models.entConfiguration>()
                               .FirstOrDefault(x => x.Name == dataType.Value);
                
                var count = db.ExecuteScalar<int>($"SELECT COUNT(*) FROM ent{dataType.Key}");
                
                if (count == 0 || (config != null && 
                    DateTime.ParseExact(today, "yyyyMMdd", CultureInfo.InvariantCulture) > 
                    DateTime.ParseExact(config.Value.Replace("-", ""), "yyyyMMdd", CultureInfo.InvariantCulture)))
                {
                    dataTypesToUpdate.Add(dataType.Key);
                }
            }

            return dataTypesToUpdate;
        }

        private async Task<(string DataType, object Data, List<string> ImageUrls)> FetchRetailDataAsync()
        {
            using var client = new HttpClient { BaseAddress = new Uri(GlobalVariables.APIPath) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var request = new StringContent(string.Empty, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("RetailController/SelectRetail", request);
            var responseString = await response.Content.ReadAsStringAsync();

            var returnObject = JObject.Parse(responseString);
            if (returnObject["Result"]?.ToString() != "NOK")
            {
                var retailList = JArray.Parse(returnObject["Content"].ToString()).ToObject<List<entRetail>>();
                var imageUrls = retailList.Where(r => !string.IsNullOrEmpty(r.ProfilePicturePath))
                                         .Select(r => GlobalVariables.WebsitePath + r.ProfilePicturePath)
                                         .ToList();
                return ("Retail", retailList, imageUrls);
            }
            return ("Retail", new List<entRetail>(), new List<string>());
        }

        private async Task<(string DataType, object Data, List<string> ImageUrls)> FetchRetailShopDataAsync()
        {
            using var client = new HttpClient { BaseAddress = new Uri(GlobalVariables.APIPath) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var memberRoute = new entMemberRoute
            {
                EffectiveDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")),
                MemberId = GlobalVariables.CurrentMember.Id
            };

            var request = new StringContent(JsonConvert.SerializeObject(memberRoute), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("RetailShopController/SelectRetailShopByMemberIdMemberRouteDate", request);
            var responseString = await response.Content.ReadAsStringAsync();

            var returnObject = JObject.Parse(responseString);
            if (returnObject["Result"]?.ToString() != "NOK")
            {
                var retailShopList = JArray.Parse(returnObject["Content"].ToString()).ToObject<List<entRetailShop>>();
                return ("RetailShop", retailShopList, new List<string>());
            }
            return ("RetailShop", new List<entRetailShop>(), new List<string>());
        }

        private async Task<(string DataType, object Data, List<string> ImageUrls)> FetchMemberRouteDataAsync()
        {
            using var client = new HttpClient { BaseAddress = new Uri(GlobalVariables.APIPath) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var memberRoute = new entMemberRoute
            {
                EffectiveDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")),
                MemberId = GlobalVariables.CurrentMember.Id
            };

            var request = new StringContent(JsonConvert.SerializeObject(memberRoute), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("MemberRouteController/SelectMemberRouteByMemberIdEffectiveDate", request);
            var responseString = await response.Content.ReadAsStringAsync();

            var returnObject = JObject.Parse(responseString);
            if (returnObject["Result"]?.ToString() != "NOK")
            {
                var memberRouteList = JArray.Parse(returnObject["Content"].ToString()).ToObject<List<entMemberRoute>>();
                return ("MemberRoute", memberRouteList, new List<string>());
            }
            return ("MemberRoute", new List<entMemberRoute>(), new List<string>());
        }

        private async Task<(string DataType, object Data, List<string> ImageUrls)> FetchMemberRouteDetailDataAsync()
        {
            using var client = new HttpClient { BaseAddress = new Uri(GlobalVariables.APIPath) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var memberRoute = new entMemberRoute
            {
                EffectiveDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")),
                MemberId = GlobalVariables.CurrentMember.Id
            };

            var request = new StringContent(JsonConvert.SerializeObject(memberRoute), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("MemberRouteDetailController/SelectMemberRouteDetailByMemberIdEffectiveDate", request);
            var responseString = await response.Content.ReadAsStringAsync();

            var returnObject = JObject.Parse(responseString);
            if (returnObject["Result"]?.ToString() != "NOK")
            {
                var memberRouteDetailList = JArray.Parse(returnObject["Content"].ToString()).ToObject<List<entMemberRouteDetail>>();
                return ("MemberRouteDetail", memberRouteDetailList, new List<string>());
            }
            return ("MemberRouteDetail", new List<entMemberRouteDetail>(), new List<string>());
        }

        private async Task<(string DataType, object Data, List<string> ImageUrls)> FetchCustomerBrandCategoryDataAsync()
        {
            using var client = new HttpClient { BaseAddress = new Uri(GlobalVariables.APIPath) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var request = new StringContent(string.Empty, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("CustomerBrandCategoryController/SelectCustomerBrandCategory", request);
            var responseString = await response.Content.ReadAsStringAsync();

            var returnObject = JObject.Parse(responseString);
            if (returnObject["Result"]?.ToString() != "NOK")
            {
                var customerBrandCategoryList = JArray.Parse(returnObject["Content"].ToString()).ToObject<List<entCustomerBrandCategory>>();
                return ("CustomerBrandCategory", customerBrandCategoryList, new List<string>());
            }
            return ("CustomerBrandCategory", new List<entCustomerBrandCategory>(), new List<string>());
        }

        private async Task<(string DataType, object Data, List<string> ImageUrls)> FetchCustomerProductDataAsync()
        {
            using var client = new HttpClient { BaseAddress = new Uri(GlobalVariables.APIPath) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var request = new StringContent(string.Empty, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("CustomerProductController/SelectCustomerProduct", request);
            var responseString = await response.Content.ReadAsStringAsync();

            var returnObject = JObject.Parse(responseString);
            if (returnObject["Result"]?.ToString() != "NOK")
            {
                var customerProductList = JArray.Parse(returnObject["Content"].ToString()).ToObject<List<entCustomerProduct>>();
                var imageUrls = customerProductList.Where(p => !string.IsNullOrEmpty(p.ProfilePicturePath))
                                                  .Select(p => GlobalVariables.WebsitePath + p.ProfilePicturePath)
                                                  .ToList();
                return ("CustomerProduct", customerProductList, imageUrls);
            }
            return ("CustomerProduct", new List<entCustomerProduct>(), new List<string>());
        }

        private async Task<(string DataType, object Data, List<string> ImageUrls)> FetchSalesOrderPackageTypeDataAsync()
        {
            using var client = new HttpClient { BaseAddress = new Uri(GlobalVariables.APIPath) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var request = new StringContent(string.Empty, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("SalesOrderPackageTypeController/SelectSalesOrderPackageType", request);
            var responseString = await response.Content.ReadAsStringAsync();

            var returnObject = JObject.Parse(responseString);
            if (returnObject["Result"]?.ToString() != "NOK")
            {
                var salesOrderPackageTypeList = JArray.Parse(returnObject["Content"].ToString()).ToObject<List<entSalesOrderPackageType>>();
                return ("SalesOrderPackageType", salesOrderPackageTypeList, new List<string>());
            }
            return ("SalesOrderPackageType", new List<entSalesOrderPackageType>(), new List<string>());
        }

        private async Task<(string DataType, object Data, List<string> ImageUrls)> FetchPromotionTypeDataAsync()
        {
            using var client = new HttpClient { BaseAddress = new Uri(GlobalVariables.APIPath2) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var request = new StringContent(string.Empty, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("PromotionController/SelectPromotionType", request);
            var responseString = await response.Content.ReadAsStringAsync();

            var returnObject = JObject.Parse(responseString);
            if (returnObject["Result"]?.ToString() != "NOK")
            {
                var promotionTypeList = JArray.Parse(returnObject["Content"].ToString()).ToObject<List<entPromotionType>>();
                return ("PromotionType", promotionTypeList, new List<string>());
            }
            return ("PromotionType", new List<entPromotionType>(), new List<string>());
        }

        private async Task ProcessDataResults(SQLiteConnection db, (string DataType, object Data, List<string> ImageUrls)[] results)
        {
            // Process all data in a single transaction for maximum performance
            db.BeginTransaction();
            try
            {
                foreach (var (dataType, data, imageUrls) in results)
                {
                    switch (dataType)
                    {
                        case "Retail":
                            var retailList = (List<entRetail>)data;
                            db.Execute("DELETE FROM entRetail");
                            db.InsertAll(retailList);
                            GlobalVariables.listRetail = retailList;
                            db.Execute("UPDATE entConfiguration SET Value=? WHERE Name=?", DateTime.Now.ToString("yyyy-MM-dd"), "RetailDataDateTime");
                            _logger.LogInformation($"Retail data processed - Total: {retailList.Count}");
                            break;

                        case "RetailShop":
                            var retailShopList = (List<entRetailShop>)data;
                            db.Execute("DELETE FROM entRetailShop");
                            db.InsertAll(retailShopList);
                            GlobalVariables.listRetailShop = retailShopList;
                            db.Execute("UPDATE entConfiguration SET Value=? WHERE Name=?", DateTime.Now.ToString("yyyy-MM-dd"), "RetailShopDataDateTime");
                            _logger.LogInformation($"RetailShop data processed - Total: {retailShopList.Count}");
                            break;

                        case "MemberRoute":
                            var memberRouteList = (List<entMemberRoute>)data;
                            db.Execute("DELETE FROM entMemberRoute");
                            db.InsertAll(memberRouteList);
                            GlobalVariables.listMemberRoute = memberRouteList;
                            db.Execute("UPDATE entConfiguration SET Value=? WHERE Name=?", DateTime.Now.ToString("yyyy-MM-dd"), "MemberRouteDataDateTime");
                            _logger.LogInformation($"MemberRoute data processed - Total: {memberRouteList.Count}");
                            break;

                        case "MemberRouteDetail":
                            var memberRouteDetailList = (List<entMemberRouteDetail>)data;
                            db.Execute("DELETE FROM entMemberRouteDetail");
                            db.InsertAll(memberRouteDetailList);
                            db.Execute("DELETE FROM entMemberRouteDetail WHERE CustomerProductId=?", 208);
                            GlobalVariables.listMemberRouteDetail = memberRouteDetailList;
                            db.Execute("UPDATE entConfiguration SET Value=? WHERE Name=?", DateTime.Now.ToString("yyyy-MM-dd"), "MemberRouteDetailDataDateTime");
                            _logger.LogInformation($"MemberRouteDetail data processed - Total: {memberRouteDetailList.Count}");
                            break;

                        case "CustomerBrandCategory":
                            var customerBrandCategoryList = (List<entCustomerBrandCategory>)data;
                            db.Execute("DELETE FROM entCustomerBrandCategory");
                            db.InsertAll(customerBrandCategoryList);
                            GlobalVariables.listCustomerBrandCategory = customerBrandCategoryList;
                            db.Execute("UPDATE entConfiguration SET Value=? WHERE Name=?", DateTime.Now.ToString("yyyy-MM-dd"), "CustomerBrandCategoryDataDateTime");
                            _logger.LogInformation($"CustomerBrandCategory data processed - Total: {customerBrandCategoryList.Count}");
                            break;

                        case "CustomerProduct":
                            var customerProductList = (List<entCustomerProduct>)data;
                            db.Execute("DELETE FROM entCustomerProduct");
                            db.InsertAll(customerProductList);
                            db.Execute("DELETE FROM entCustomerProduct WHERE Id=?", 208);
                            GlobalVariables.listCustomerProduct = customerProductList;
                            db.Execute("UPDATE entConfiguration SET Value=? WHERE Name=?", DateTime.Now.ToString("yyyy-MM-dd"), "CustomerProductDataDateTime");
                            _logger.LogInformation($"CustomerProduct data processed - Total: {customerProductList.Count}");
                            break;

                        case "SalesOrderPackageType":
                            var salesOrderPackageTypeList = (List<entSalesOrderPackageType>)data;
                            db.Execute("DELETE FROM entSalesOrderPackageType");
                            db.InsertAll(salesOrderPackageTypeList);
                            GlobalVariables.listSalesOrderPackageType = salesOrderPackageTypeList;
                            db.Execute("UPDATE entConfiguration SET Value=? WHERE Name=?", DateTime.Now.ToString("yyyy-MM-dd"), "SalesOrderPackageTypeDataDateTime");
                            _logger.LogInformation($"SalesOrderPackageType data processed - Total: {salesOrderPackageTypeList.Count}");
                            break;

                        case "PromotionType":
                            var promotionTypeList = (List<entPromotionType>)data;
                            db.Execute("DELETE FROM entPromotionType");
                            db.InsertAll(promotionTypeList);
                            GlobalVariables.listPromotionType = promotionTypeList;
                            db.Execute("UPDATE entConfiguration SET Value=? WHERE Name=?", DateTime.Now.ToString("yyyy-MM-dd"), "PromotionTypeDataDateTime");
                            _logger.LogInformation($"PromotionType data processed - Total: {promotionTypeList.Count}");
                            break;
                    }
                }

                db.Commit();
                _logger.LogInformation("All data processed successfully in single transaction");
            }
            catch (Exception ex)
            {
                db.Rollback();
                _logger.LogError(ex, "Error processing data results, transaction rolled back");
                throw;
            }
        }

        private async Task DownloadImagesInParallel((string DataType, object Data, List<string> ImageUrls)[] results)
        {
            var allImageTasks = new List<Task>();
            
            foreach (var (dataType, data, imageUrls) in results)
            {
                if (imageUrls.Count == 0) continue;

                if (dataType == "Retail")
                {
                    var retailList = (List<entRetail>)data;
                    var retailImageTasks = retailList.Where(r => !string.IsNullOrEmpty(r.ProfilePicturePath))
                                                    .Select(retail => DownloadRetailImage(retail))
                                                    .ToList();
                    allImageTasks.AddRange(retailImageTasks);
                }
                else if (dataType == "CustomerProduct")
                {
                    var productList = (List<entCustomerProduct>)data;
                    var productImageTasks = productList.Where(p => !string.IsNullOrEmpty(p.ProfilePicturePath))
                                                      .Select(product => DownloadProductImage(product))
                                                      .ToList();
                    allImageTasks.AddRange(productImageTasks);
                }
            }

            if (allImageTasks.Count > 0)
            {
                // Process images in batches of 10 to avoid overwhelming the server
                const int batchSize = 10;
                for (int i = 0; i < allImageTasks.Count; i += batchSize)
                {
                    var batch = allImageTasks.Skip(i).Take(batchSize);
                    await Task.WhenAll(batch);
                    
                    var progress = 85 + (int)((double)(i + batchSize) / allImageTasks.Count * 10);
                    MainThread.BeginInvokeOnMainThread(() => UpdateProgress(Math.Min(progress, 95), $"Resimler indiriliyor... ({Math.Min(i + batchSize, allImageTasks.Count)}/{allImageTasks.Count})"));
                }
            }

            _logger.LogInformation($"Downloaded {allImageTasks.Count} images in parallel batches");
        }

        private async Task DownloadRetailImage(entRetail retail)
        {
            try
            {
                using var db = new SQLiteConnection(GlobalVariables.DatabasePath);
                using var webClient = new HttpClient();
                
                byte[] imageBytes = await webClient.GetByteArrayAsync(GlobalVariables.WebsitePath + retail.ProfilePicturePath);
                var profilePicture = new MerchPlusMAUI.Models.entRetailProfilePicture
                {
                    RetailId = retail.Id,
                    ProfilePictureBase64 = Convert.ToBase64String(imageBytes)
                };

                lock (_dbLock)
                {
                    db.Insert(profilePicture);
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, $"Error downloading profile picture for retail {retail.Id}");
            }
        }

        private async Task DownloadProductImage(entCustomerProduct product)
        {
            try
            {
                using var db = new SQLiteConnection(GlobalVariables.DatabasePath);
                using var webClient = new HttpClient();
                
                byte[] imageBytes = await webClient.GetByteArrayAsync(GlobalVariables.WebsitePath + product.ProfilePicturePath);
                var productPicture = new entCustomerProductPicture
                {
                    CustomerProductId = product.Id,
                    CustomerProductPictureBase64 = Convert.ToBase64String(imageBytes)
                };

                lock (_dbLock)
                {
                    db.Insert(productPicture);
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, $"Error downloading product picture for product {product.Id}");
            }
        }

        private async Task CalculateStatistics()
        {
            try
            {
                _logger.LogInformation("Starting statistics calculation");
                var startTime = DateTime.Now;

                await Task.Run(() =>
                {
                    using var db = new SQLiteConnection(GlobalVariables.DatabasePath);
                    
                    try
                    {
                        // Use efficient SQL queries with proper logic for route statuses
                        // Total planned routes (all routes)
                        PlannedStoresCount = db.ExecuteScalar<int>("SELECT COUNT(*) FROM entMemberRoute");
                        
                        // Completed routes (ExitOn is not null)
                        CompletedStoresCount = db.ExecuteScalar<int>("SELECT COUNT(*) FROM entMemberRoute WHERE ExitOn IS NOT NULL");
                        
                        // Pending routes (EntryOn is not null but ExitOn is null) - currently in progress
                        var pendingStoresCount = db.ExecuteScalar<int>("SELECT COUNT(*) FROM entMemberRoute WHERE EntryOn IS NOT NULL AND ExitOn IS NULL");
                        
                        // Remaining planned routes (EntryOn is null and ExitOn is null) - not started yet
                        var notStartedStoresCount = db.ExecuteScalar<int>("SELECT COUNT(*) FROM entMemberRoute WHERE EntryOn IS NULL AND ExitOn IS NULL");
                        
                        // PendingStoresCount includes both in-progress and not-started routes
                        PendingStoresCount = pendingStoresCount + notStartedStoresCount;

                        _logger.LogInformation($"Statistics calculated - Total: {PlannedStoresCount}, Completed: {CompletedStoresCount}, Pending: {PendingStoresCount} (In Progress: {pendingStoresCount}, Not Started: {notStartedStoresCount})");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "Error calculating statistics from database, using defaults");
                        // Set default values on error
                        PlannedStoresCount = 0;
                        CompletedStoresCount = 0;
                        PendingStoresCount = 0;
                    }
                });

                var duration = DateTime.Now - startTime;
                _logger.LogInformation($"Statistics calculation completed in {duration.TotalMilliseconds}ms");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calculating statistics");
                // Set default values on error
                PlannedStoresCount = 0;
                CompletedStoresCount = 0;
                PendingStoresCount = 0;
            }
        }

        private void UpdateProgress(int progress, string message)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                InitProgressBar.Progress = progress / 100.0;
                ProgressPercentLabel.Text = $"{progress}%";
                StatusLabel.Text = message;
            });
        }

        private async Task ShowError(Exception exception)
        {
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                ProgressContainer.IsVisible = false;
                ErrorContainer.IsVisible = true;
                SubtitleLabel.Text = "Uygulama Başlatılamadı";
                ErrorMessageLabel.Text = exception.Message;
            });
        }

        private async void OnRetryClicked(object sender, EventArgs e)
        {
            if (_retryAction != null)
            {
                await _retryAction();
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _httpClient?.Dispose();
        }
    }

    // Network connectivity status model
    public class NetworkConnectivityStatus
    {
        public NetworkAccess NetworkAccess { get; set; }
        public List<ConnectionProfile> ConnectionProfiles { get; set; } = new();
        public bool HasWiFi { get; set; }
        public bool HasCellular { get; set; }
        public bool HasEthernet { get; set; }
        public bool IsRoaming { get; set; }
        
        public override string ToString()
        {
            return $"NetworkAccess: {NetworkAccess}, WiFi: {HasWiFi}, Cellular: {HasCellular}, Ethernet: {HasEthernet}, Roaming: {IsRoaming}";
        }
    }
} 