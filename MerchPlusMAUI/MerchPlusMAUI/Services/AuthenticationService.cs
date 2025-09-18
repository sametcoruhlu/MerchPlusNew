using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Logging;
using entMerchPlus;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Maui.Devices;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Networking;

namespace MerchPlusMAUI.Services
{
    public interface IAuthenticationService
    {
        Task<bool> InitializeUserAsync();
        Task<entMember> GetCurrentUserAsync();
        Task<entMember> AuthenticateByDeviceIdAsync(string deviceId);
        Task<bool> LogoutAsync();
        string GetDeviceId();
        event EventHandler<string> StatusMessageChanged;
        event EventHandler<bool> AuthenticationCompleted;
    }

    public class AuthenticationService : IAuthenticationService
    {
        private readonly ILogger<AuthenticationService> _logger;
        private readonly HttpClient _httpClient;
        private readonly IConnectivity _connectivity;
        private readonly IDeviceInfo _deviceInfo;
        
        private entMember _currentUser;
        private string _deviceId;

        public event EventHandler<string> StatusMessageChanged;
        public event EventHandler<bool> AuthenticationCompleted;

        public AuthenticationService(ILogger<AuthenticationService> logger, IConnectivity connectivity, IDeviceInfo deviceInfo)
        {
            _logger = logger;
            _connectivity = connectivity;
            _deviceInfo = deviceInfo;
            
            try
            {
                // API yolu kontrolü
                string apiPath = GlobalVariables.APIPath ?? "https://api.example.com/"; // Varsayılan değer ekledim
                
                // HttpClient yapılandırması
                _httpClient = new HttpClient
                {
                    BaseAddress = new Uri(apiPath),
                    Timeout = TimeSpan.FromSeconds(30)
                };
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "HttpClient kurulumu sırasında hata oluştu");
                _httpClient = new HttpClient(); // Yine de bir httpClient örneği oluştur
            }
        }

        /// <summary>
        /// Uygulama başladığında kullanıcı bilgilerini başlatır.
        /// </summary>
        public async Task<bool> InitializeUserAsync()
        {
            try
            {
                // Eğer kullanıcı zaten varsa, tekrar yüklemeye gerek yok
                if (GlobalVariables.CurrentMember?.Id != null)
                {
                    _currentUser = GlobalVariables.CurrentMember;
                    RaiseStatusMessage($"Tekrar merhaba {_currentUser.NameSurname}");
                    RaiseAuthenticationCompleted(true);
                    return true;
                }

                // İnternet bağlantısı kontrolü
                if (_connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    RaiseStatusMessage("İnternet bağlantısı bulunamadı. Lütfen bağlantınızı kontrol edin.");
                    RaiseAuthenticationCompleted(false);
                    return false;
                }

                // Cihaz ID'si alınır
                var deviceId = GetDeviceId();
                if (string.IsNullOrEmpty(deviceId))
                {
                    RaiseStatusMessage("Cihaz tanımlayıcı alınamadı.");
                    RaiseAuthenticationCompleted(false);
                    return false;
                }

                RaiseStatusMessage("Kullanıcı girişi yapılıyor...");
                
                // Kullanıcı kimlik doğrulama
                var user = await AuthenticateByDeviceIdAsync(deviceId);
                
                if (user?.Id == null)
                {
                    RaiseStatusMessage($"Cihaz tanımlama hatası: {deviceId} numarası ile ilişkili kullanıcı bulunamadı.");
                    RaiseAuthenticationCompleted(false);
                    return false;
                }

                // Kullanıcı bilgilerini tamamlayıcı işlemler
                await InitializeUserProfileAsync(user);
                
                _currentUser = user;
                GlobalVariables.CurrentMember = user;
                GlobalVariables.CurrentMemberId = user.Id.ToString();
                GlobalVariables.CurrentUserId = user.Id.ToString();
                
                RaiseStatusMessage($"Hoşgeldiniz {user.NameSurname}");
                RaiseAuthenticationCompleted(true);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcı başlatma hatası");
                RaiseStatusMessage($"Giriş sırasında bir hata oluştu: {ex.Message}");
                RaiseAuthenticationCompleted(false);
                return false;
            }
        }

        /// <summary>
        /// Mevcut oturum açmış kullanıcıyı döndürür
        /// </summary>
        public Task<entMember> GetCurrentUserAsync()
        {
            return Task.FromResult(_currentUser ?? GlobalVariables.CurrentMember);
        }

        /// <summary>
        /// Cihaz ID'sine göre kullanıcıyı doğrular
        /// </summary>
        public async Task<entMember> AuthenticateByDeviceIdAsync(string deviceId)
        {
            try
            {
                // API yolu kontrolü
                if (_httpClient.BaseAddress == null || string.IsNullOrEmpty(_httpClient.BaseAddress.ToString()))
                {
                    string apiPath = GlobalVariables.APIPath ?? "https://api.example.com/";
                    _httpClient.BaseAddress = new Uri(apiPath);
                }
                
                var member = new entMember { IMEINumber = deviceId };
                var jsonContent = JsonConvert.SerializeObject(member);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // API isteği gönderme
                var response = await _httpClient.PostAsync("AndroidTestController/SelectMemberByIMEINumber", content);
                response.EnsureSuccessStatusCode();
                
                var responseString = await response.Content.ReadAsStringAsync();
                var returnObject = JObject.Parse(responseString);

                if (returnObject["Result"]?.ToString() != "NOK")
                {
                    return returnObject["Content"]?.ToObject<entMember>();
                }
                
                return null;
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "HTTP hatası, sunucu yanıt vermiyor veya istek başarısız");
                throw new Exception("Sunucu ile iletişim kurulamadı. Lütfen internet bağlantınızı kontrol edin.", httpEx);
            }
            catch (TaskCanceledException tcEx)
            {
                _logger.LogError(tcEx, "İstek zaman aşımına uğradı");
                throw new Exception("İstek zaman aşımına uğradı. Lütfen daha sonra tekrar deneyin.", tcEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kimlik doğrulama hatası");
                throw new Exception("Giriş yapılırken bir hata oluştu", ex);
            }
        }

        /// <summary>
        /// Kullanıcı oturumunu kapatır
        /// </summary>
        public Task<bool> LogoutAsync()
        {
            try
            {
                _currentUser = null;
                GlobalVariables.CurrentMember = null;
                GlobalVariables.CurrentMemberId = null;
                GlobalVariables.CurrentUserId = null;
                
                // Cache temizleme ve diğer logout işlemleri buraya eklenebilir
                
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Çıkış yapılırken hata oluştu");
                return Task.FromResult(false);
            }
        }

        /// <summary>
        /// Cihaz benzersiz kimliğini alır
        /// </summary>
        public string GetDeviceId()
        {
            if (!string.IsNullOrEmpty(_deviceId))
                return _deviceId;

            try
            {
                // MAUI'nin DeviceInfo özelliğini kullanarak modern yöntemle cihaz kimliği alma
                // GetIdForVendor yerine daha genel bir yöntem kullanılıyor
                _deviceId = _deviceInfo.Model + "_" + _deviceInfo.Name + "_" + _deviceInfo.Manufacturer;

                // Android'de ekstra bir kontrol
#if ANDROID
                if (string.IsNullOrEmpty(_deviceId) && DeviceInfo.Platform == DevicePlatform.Android)
                {
                    try
                    {
                        var context = Android.App.Application.Context;
                        _deviceId = Android.Provider.Settings.Secure.GetString(
                            context.ContentResolver,
                            Android.Provider.Settings.Secure.AndroidId);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Android cihaz ID alma hatası");
                    }
                }
#endif

                // Yine de ID alınamadıysa, benzersiz ID oluştur ve sakla
                if (string.IsNullOrEmpty(_deviceId))
                {
                    _deviceId = Guid.NewGuid().ToString();
                    Preferences.Set("device_id", _deviceId);
                    _logger.LogInformation("Benzersiz cihaz ID'si oluşturuldu: {DeviceId}", _deviceId);
                }
                else
                {
                    // Gelecekteki kullanımlar için kaydet
                    Preferences.Set("device_id", _deviceId);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cihaz ID alınırken hata oluştu");
                
                // Hata durumunda daha önce kaydedilmiş bir ID var mı kontrol et
                _deviceId = Preferences.Get("device_id", string.Empty);
                
                // Yoksa yeni oluştur
                if (string.IsNullOrEmpty(_deviceId))
                {
                    _deviceId = Guid.NewGuid().ToString();
                    Preferences.Set("device_id", _deviceId);
                    _logger.LogInformation("Hata sonrası yeni cihaz ID'si oluşturuldu: {DeviceId}", _deviceId);
                }
            }

            return _deviceId;
        }

        /// <summary>
        /// Kullanıcı profil bilgilerini tamamlar (profil resmi gibi)
        /// </summary>
        private async Task InitializeUserProfileAsync(entMember user)
        {
            if (user == null) return;

            try
            {
                // Profil fotoğrafı boşsa varsayılan ekle
                if (string.IsNullOrEmpty(user.ProfilePicturePath))
                {
                    try
                    {
                        // Resourcesden varsayılan profil fotoğrafını yükle
                        using var stream = await FileSystem.OpenAppPackageFileAsync("defaultprofilphoto.png");
                        if (stream != null)
                        {
                            using var memoryStream = new MemoryStream();
                            await stream.CopyToAsync(memoryStream);
                            var imageBytes = memoryStream.ToArray();
                            user.ProfilePicturePath = Convert.ToBase64String(imageBytes);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "Varsayılan profil fotoğrafı yüklenemedi");
                    }
                }

                // Ek kullanıcı bilgisi işlemleri burada yapılabilir
                // Örneğin: Yetkilendirme, rol bilgileri vs.
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcı profili tamamlanamadı");
            }
        }

        // Event helpers
        private void RaiseStatusMessage(string message)
        {
            try
            {
                StatusMessageChanged?.Invoke(this, message);
                _logger.LogInformation("Auth Status: {Message}", message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Durum mesajı bildirimi sırasında hata oluştu");
            }
        }

        private void RaiseAuthenticationCompleted(bool success)
        {
            try
            {
                AuthenticationCompleted?.Invoke(this, success);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kimlik doğrulama tamamlandı bildirimi sırasında hata oluştu");
            }
        }
    }
} 