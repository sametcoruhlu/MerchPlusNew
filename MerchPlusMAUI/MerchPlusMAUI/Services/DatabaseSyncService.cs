using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Maui.ApplicationModel;
using SQLite;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MerchPlusMAUI.Models;
using MerchPlusMAUI.Services;
using entMerchPlus;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MerchPlusMAUI.Services
{
    public class DatabaseSyncService : IHostedService, IDatabaseSyncService
    {
        private readonly ILogger<DatabaseSyncService> _logger;
        private readonly HttpClient _httpClient;
        private readonly HttpClient _httpClient2;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private bool _isRunning;
        
        private const int COORDINATE_SYNC_INTERVAL = 300000; // 5 minutes
        private const int ROUTE_SYNC_INTERVAL = 360000; // 6 minutes
        private const int PHOTO_SYNC_INTERVAL = 360000; // 6 minutes
        private const int DB_CHECK_INTERVAL = 15000; // 15 seconds

        public DatabaseSyncService(ILogger<DatabaseSyncService> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient { BaseAddress = new Uri(GlobalVariables.APIPath) };
            _httpClient2 = new HttpClient { BaseAddress = new Uri(GlobalVariables.APIPath2) };
            
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            _httpClient2.DefaultRequestHeaders.Accept.Clear();
            _httpClient2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            _cancellationTokenSource = new CancellationTokenSource();
            _isRunning = false;
        }

        public async Task StartSync()
        {
            await StartAsync(CancellationToken.None);
        }

        public async Task StopSync()
        {
            await StopAsync(CancellationToken.None);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Database sync service is starting.");
            
            // Start the background task without awaiting it
            _ = Task.Run(async () => await ExecuteAsync(), cancellationToken);
            
            // Return immediately so it doesn't block the UI
            await Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Database sync service is stopping.");
            Stop();
            await Task.CompletedTask;
        }

        private async Task ExecuteAsync()
        {
            if (_isRunning)
            {
                _logger.LogInformation("Sync service is already running");
                return;
            }

            _isRunning = true;

            try
            {
                // İlk çalışmada 30 saniye bekle - LocationAgentService'in koordinat kaydetmesi için zaman tanı
                Console.WriteLine("[DatabaseSyncService] İlk senkronizasyon için 30 saniye bekleniyor (LocationAgentService'in koordinat kaydetmesi için)...");
                _logger.LogInformation("Waiting 30 seconds before first sync to allow LocationAgentService to save coordinates");
                await Task.Delay(TimeSpan.FromSeconds(30), _cancellationTokenSource.Token);
                
                while (!_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    await SyncData();
                    await Task.Delay(TimeSpan.FromMinutes(5), _cancellationTokenSource.Token);
                }
            }
            catch (System.OperationCanceledException)
            {
                _logger.LogInformation("Sync service was cancelled");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in sync service");
            }
            finally
            {
                _isRunning = false;
            }
        }

        public async Task SyncData()
        {
            try
            {
                Console.WriteLine("========================================");
                Console.WriteLine($"[VERİ SENKRONIZASYON] Başlatılıyor - {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                Console.WriteLine("========================================");
                
                // Eski projede olduğu gibi önce koordinatları senkronize et
                await SyncCoordinates();
                
                // Sonra diğer verileri senkronize et
                await SyncMemberRoutes();
                await SyncDisplayPhotos();
                
                Console.WriteLine($"[VERİ SENKRONIZASYON] Tamamlandı - {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                Console.WriteLine("========================================");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[VERİ SENKRONIZASYON] ❌ HATA: {ex.Message}");
                Console.WriteLine("========================================");
                _logger.LogError(ex, "Error occurred while syncing data");
            }
        }

        public void Stop()
        {
            _cancellationTokenSource.Cancel();
        }

        private async Task SyncCoordinates()
        {
            try
            {
                Console.WriteLine("========================================");
                Console.WriteLine("[KOORDINAT SENKRONIZASYON] Başlatılıyor...");
                Console.WriteLine($"Zaman: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                _logger.LogInformation("Synchronizing coordinates with Server > entMemberCoordinate!");
                
                using var db = new SQLiteConnection(GlobalVariables.DatabasePath);
                
                // Tablo var mı kontrol et
                var tableExists = db.ExecuteScalar<int>("SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name='entMemberCoordinate'");
                Console.WriteLine($"[KOORDINAT SENKRONIZASYON] Tablo kontrolü: entMemberCoordinate tablosu {(tableExists > 0 ? "VAR" : "YOK")}");
                
                if (tableExists == 0)
                {
                    // Tablo yoksa oluştur
                    db.CreateTable<MerchPlusMAUI.Models.entMemberCoordinate>();
                    Console.WriteLine("[KOORDINAT SENKRONIZASYON] entMemberCoordinate tablosu oluşturuldu");
                }
                
                // Toplam kayıt sayısını kontrol et
                var totalCount = db.ExecuteScalar<int>("SELECT COUNT(*) FROM entMemberCoordinate");
                Console.WriteLine($"[KOORDINAT SENKRONIZASYON] Tabloda toplam {totalCount} adet kayıt var");
                
                // Eski projede olduğu gibi entMemberCoordinate tablosundan okuyoruz
                var coordinates = db.Query<MerchPlusMAUI.Models.entMemberCoordinate>("SELECT * from entMemberCoordinate");
                Console.WriteLine($"[KOORDINAT SENKRONIZASYON] Query sonucu: {coordinates.Count()} adet koordinat bulundu");

                if (coordinates.Any())
                {
                    Console.WriteLine($"[KOORDINAT SENKRONIZASYON] Tabloda {coordinates.Count} adet koordinat bulundu");
                    _logger.LogInformation($"Found {coordinates.Count} coordinates to sync");
                    
                    // Koordinat detaylarını logla
                    Console.WriteLine("[KOORDINAT SENKRONIZASYON] Gönderilecek koordinatlar:");
                    for (int i = 0; i < coordinates.Count; i++)
                    {
                        var coord = coordinates[i];
                        Console.WriteLine($"  [{i + 1}] MemberId: '{coord.MemberId}'");
                        Console.WriteLine($"      CoordinateX (decimal): {coord.CoordinateX}, CoordinateY (decimal): {coord.CoordinateY}");
                        Console.WriteLine($"      Latitude (double): {coord.Latitude}, Longitude (double): {coord.Longitude}");
                        Console.WriteLine($"      CreatedOn: {coord.CreatedOn:yyyy-MM-dd HH:mm:ss}, LastUpdateTime: {coord.LastUpdateTime:yyyy-MM-dd HH:mm:ss}");
                    }
                    
                    // JSON önizlemesi (ilk koordinat)
                    if (coordinates.Any())
                    {
                        var firstCoord = coordinates.First();
                        var previewJson = Newtonsoft.Json.JsonConvert.SerializeObject(firstCoord);
                        Console.WriteLine($"[KOORDINAT SENKRONIZASYON] JSON önizlemesi (ilk koordinat): {previewJson}");
                    }
                    
                    // Eski projede olduğu gibi Newtonsoft.Json kullanıyoruz
                    using var client = new HttpClient { BaseAddress = new Uri(GlobalVariables.APIPath) };
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    
                    Console.WriteLine($"[KOORDINAT SENKRONIZASYON] API Endpoint: {GlobalVariables.APIPath}MemberCoordinateController/InsertMemberCoordinateFromAndroidArray");
                    
                    // Eski projede olduğu gibi JSON serialize
                    var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(coordinates);
                    Console.WriteLine($"[KOORDINAT SENKRONIZASYON] JSON hazırlandı (uzunluk: {jsonString.Length} karakter)");
                    var request = new StringContent(jsonString, Encoding.UTF8, "application/json");
                    
                    Console.WriteLine("[KOORDINAT SENKRONIZASYON] Sunucuya POST isteği gönderiliyor...");
                    var startTime = DateTime.Now;
                    var response = await client.PostAsync("MemberCoordinateController/InsertMemberCoordinateFromAndroidArray", request);
                    var endTime = DateTime.Now;
                    var duration = (endTime - startTime).TotalSeconds;
                    
                    Console.WriteLine($"[KOORDINAT SENKRONIZASYON] HTTP Response alındı (süre: {duration:F2} saniye)");
                    Console.WriteLine($"[KOORDINAT SENKRONIZASYON] HTTP Status Code: {response.StatusCode} ({(int)response.StatusCode})");
                    
                    var responseString = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"[KOORDINAT SENKRONIZASYON] Response içeriği: {responseString}");
                    
                    // Eski projede olduğu gibi response kontrolü
                    JObject returnObject = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(responseString);
                    var result = Convert.ToString(returnObject["Result"]);
                    Console.WriteLine($"[KOORDINAT SENKRONIZASYON] Response Result: {result}");
                    
                    if (result != "NOK")
                    {
                        // Başarılı - silmeden önce son koordinat bilgisini GlobalVariables'a kaydet
                        var lastCoordinate = coordinates.OrderByDescending(c => c.LastUpdateTime ?? c.CreatedOn ?? DateTime.MinValue).FirstOrDefault();
                        if (lastCoordinate != null && lastCoordinate.Latitude.HasValue && lastCoordinate.Longitude.HasValue)
                        {
                            GlobalVariables.MemberLastCoordinate = new MerchPlusMAUI.Location
                            {
                                Latitude = lastCoordinate.Latitude.Value,
                                Longitude = lastCoordinate.Longitude.Value
                            };
                            Console.WriteLine($"[KOORDINAT SENKRONIZASYON] Son koordinat GlobalVariables'a kaydedildi: Lat={lastCoordinate.Latitude}, Lon={lastCoordinate.Longitude}");
                        }
                        
                        // Başarılı - tüm kayıtları sil (eski projede olduğu gibi)
                        var deletedCount = db.DeleteAll<MerchPlusMAUI.Models.entMemberCoordinate>();
                        Console.WriteLine($"[KOORDINAT SENKRONIZASYON] ✅ BAŞARILI - {coordinates.Count} koordinat sunucuya gönderildi ve tablodan silindi");
                        Console.WriteLine("========================================");
                        _logger.LogInformation($"Successfully synced {coordinates.Count} coordinates to server");
                    }
                    else
                    {
                        Console.WriteLine($"[KOORDINAT SENKRONIZASYON] ❌ BAŞARISIZ - Sunucu NOK döndü. Koordinatlar tabloda kaldı.");
                        Console.WriteLine("========================================");
                        _logger.LogWarning("Database Sync Failed > Member Coordinate upload");
                    }
                }
                else
                {
                    Console.WriteLine("[KOORDINAT SENKRONIZASYON] Tabloda gönderilecek koordinat yok");
                    Console.WriteLine("========================================");
                    _logger.LogInformation("No coordinates to sync");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[KOORDINAT SENKRONIZASYON] ❌ HATA: {ex.Message}");
                Console.WriteLine($"[KOORDINAT SENKRONIZASYON] StackTrace: {ex.StackTrace}");
                Console.WriteLine("========================================");
                _logger.LogError(ex, "Error occurred while syncing coordinates");
            }
        }

        private async Task SyncMemberRoutes()
        {
            try
            {
                _logger.LogInformation("Synchronizing data with Server > MemberRoute!");

                using var db = new SQLiteConnection(GlobalVariables.DatabasePath);
                var routes = db.Query<entMemberRoute>("SELECT * from entMemberRoute WHERE IsSentToServer=0 or IsSentToServer is null");

                if (routes == null || !routes.Any())
                {
                    _logger.LogInformation("No MemberRoutes to sync");
                    return;
                }

                _logger.LogInformation($"Found {routes.Count()} MemberRoute(s) to sync");

                foreach (var route in routes)
                {
                    try
                    {
                        _logger.LogInformation($"Syncing MemberRoute ID: {route.Id}");

                        var syncData = new MemberRouteSyncData
                        {
                            MemberRoute = route,
                            MemberRouteDetail = db.Query<entMemberRouteDetail>("SELECT * from entMemberRouteDetail WHERE MemberRouteId=?", route.Id).ToList(),
                            MemberRoutePhoto = db.Query<entMemberRoutePhoto>("SELECT * from entMemberRoutePhoto WHERE MemberRouteId=?", route.Id).ToList(),
                            SalesOrder = db.Query<SalesOrder>("SELECT * FROM entSalesOrder WHERE MemberRouteId=?", route.Id).ToList(),
                            ProductExpiration = db.Query<ProductExpiration>("SELECT * FROM entProductExpiration WHERE MemberRouteId=?", route.Id).ToList()
                        };

                        if (syncData.SalesOrder.Any())
                        {
                            syncData.SalesOrderDetail = db.Query<SalesOrderDetail>(
                                "SELECT * FROM entSalesOrderDetail WHERE SalesOrderId=?", 
                                syncData.SalesOrder.First().Id).ToList();
                        }

                        // DateTime alanlarını kontrol et ve null olanları düzelt (sunucu null DateTime kabul etmiyor)
                        FixDateTimeFields(syncData);

                        var endpoint = syncData.SalesOrder.Any() 
                            ? "MemberRouteController/CompleteMemberRouteWiselyFromJsonArrayWithSalesOrder"
                            : "MemberRouteController/CompleteMemberRouteWiselyFromJsonArray";

                        // JSON serialize et ve sunucuya gönder
                        var json = Newtonsoft.Json.JsonConvert.SerializeObject(syncData, new Newtonsoft.Json.JsonSerializerSettings
                        {
                            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                            DateFormatString = "yyyy-MM-dd HH:mm:ss"
                        });
                        
                        var content = new StringContent(json, Encoding.UTF8, "application/json");
                        var httpResponse = await _httpClient.PostAsync($"api/{endpoint}", content);
                        
                        string responseContent = string.Empty;
                        if (httpResponse.Content != null)
                        {
                            responseContent = await httpResponse.Content.ReadAsStringAsync();
                        }

                        if (httpResponse.IsSuccessStatusCode)
                        {
                            // Response'u parse et
                            JObject responseJson = null;
                            try
                            {
                                responseJson = JObject.Parse(responseContent);
                            }
                            catch (Exception parseEx)
                            {
                                _logger.LogWarning($"Failed to parse response JSON for MemberRoute {route.Id}: {parseEx.Message}");
                                _logger.LogWarning($"Response content: {responseContent}");
                                continue;
                            }

                            // Result kontrolü
                            var result = responseJson?["Result"]?.ToString();
                            
                            if (result != "NOK")
                            {
                                // Başarılı - flag'leri güncelle
                                db.Execute("UPDATE entMemberRoute SET IsSentToServer=? WHERE Id=?", true, route.Id);
                                db.Execute("UPDATE entMemberRouteDetail SET IsSentToServer=? WHERE MemberRouteId=?", true, route.Id);
                                db.Execute("UPDATE entProductExpiration SET IsSentToServer=? WHERE MemberRouteId=?", true, route.Id);
                                
                                _logger.LogInformation($"Successfully synced MemberRoute {route.Id} to server");
                            }
                            else
                            {
                                // Sunucu NOK döndü
                                var errorMessage = responseJson?["Message"]?.ToString() ?? responseJson?["ErrorMessage"]?.ToString() ?? "Unknown error";
                                _logger.LogWarning($"Database Sync Failed > MemberRoute ID: {route.Id}, Error: {errorMessage}");
                                _logger.LogWarning($"Response content: {responseContent}");
                            }
                        }
                        else
                        {
                            // HTTP hatası
                            _logger.LogWarning($"Database Sync Failed > MemberRoute ID: {route.Id}, HTTP Status: {httpResponse.StatusCode}");
                            _logger.LogWarning($"Response content: {responseContent}");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Error syncing MemberRoute {route.Id} to server");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SyncMemberRoutes");
            }
        }

        /// <summary>
        /// Sunucuya gönderilmeden önce DateTime alanlarını kontrol eder ve null olanları düzeltir
        /// Reflection kullanarak tüm DateTime property'lerini otomatik olarak kontrol eder
        /// </summary>
        private void FixDateTimeFields(MemberRouteSyncData syncData)
        {
            var now = DateTime.Now;

            // MemberRoute DateTime alanlarını kontrol et (tüm DateTime property'leri)
            if (syncData.MemberRoute != null)
            {
                FixDateTimeFieldsForEntity(syncData.MemberRoute, "MemberRoute", syncData.MemberRoute.Id);
            }

            // MemberRouteDetail DateTime alanlarını kontrol et
            foreach (var detail in syncData.MemberRouteDetail)
            {
                FixDateTimeFieldsForEntity(detail, "MemberRouteDetail", detail.Id);
            }

            // MemberRoutePhoto DateTime alanlarını kontrol et
            foreach (var photo in syncData.MemberRoutePhoto)
            {
                FixDateTimeFieldsForEntity(photo, "MemberRoutePhoto", photo.Id);
            }

            // ProductExpiration DateTime alanlarını kontrol et
            foreach (var expiration in syncData.ProductExpiration)
            {
                FixDateTimeFieldsForEntity(expiration, "ProductExpiration", expiration.Id);
            }

            // SalesOrder DateTime alanlarını kontrol et
            foreach (var salesOrder in syncData.SalesOrder)
            {
                FixDateTimeFieldsForEntity(salesOrder, "SalesOrder", salesOrder.Id);
            }

            // SalesOrderDetail DateTime alanlarını kontrol et
            foreach (var detail in syncData.SalesOrderDetail)
            {
                FixDateTimeFieldsForEntity(detail, "SalesOrderDetail", detail.Id);
            }
        }

        /// <summary>
        /// Bir entity'nin tüm DateTime ve DateTime? property'lerini kontrol eder ve null/default olanları düzeltir
        /// </summary>
        private void FixDateTimeFieldsForEntity<T>(T entity, string entityName, int? id = null)
        {
            if (entity == null) return;

            var now = DateTime.Now;
            var entityType = typeof(T);
            
            // Tüm DateTime ve DateTime? property'lerini bul
            var dateTimeProperties = entityType.GetProperties()
                .Where(p => p.PropertyType == typeof(DateTime) || p.PropertyType == typeof(DateTime?))
                .Where(p => p.CanRead && p.CanWrite);

            foreach (var prop in dateTimeProperties)
            {
                try
                {
                    var value = prop.GetValue(entity);
                    
                    // Null veya default(DateTime) kontrolü
                    bool needsFix = false;
                    if (value == null)
                    {
                        needsFix = true;
                    }
                    else if (value is DateTime dateTimeValue)
                    {
                        // DateTime.MinValue veya default(DateTime) kontrolü
                        if (dateTimeValue == default(DateTime) || dateTimeValue == DateTime.MinValue || dateTimeValue.Year < 1900)
                        {
                            needsFix = true;
                        }
                    }
                    else if (prop.PropertyType == typeof(DateTime?) && value != null)
                    {
                        // Nullable DateTime için - önce null kontrolü yapıldı, şimdi değer kontrolü
                        if (value is DateTime nullableDateTimeValue)
                        {
                            if (nullableDateTimeValue == default(DateTime) || nullableDateTimeValue == DateTime.MinValue || nullableDateTimeValue.Year < 1900)
                            {
                                needsFix = true;
                            }
                        }
                    }
                    else if (prop.PropertyType == typeof(DateTime?) && value == null)
                    {
                        // Nullable DateTime ve null değer
                        needsFix = true;
                    }

                    if (needsFix)
                    {
                        // DateTime? için null değilse DateTime.Now, null ise DateTime.Now
                        if (prop.PropertyType == typeof(DateTime?))
                        {
                            prop.SetValue(entity, (DateTime?)now);
                        }
                        else
                        {
                            prop.SetValue(entity, now);
                        }
                        
                        string idStr = id.HasValue ? $"[ID:{id.Value}]" : "";
                        Console.WriteLine($"[DatabaseSyncService] {entityName}{idStr}.{prop.Name} null/default, {now:yyyy-MM-dd HH:mm:ss} olarak set edildi");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[DatabaseSyncService] {entityName}.{prop.Name} kontrol edilirken hata: {ex.Message}");
                }
            }
        }

        private async Task SyncDisplayPhotos()
        {
            try
            {
                Console.WriteLine("========================================");
                Console.WriteLine("[TEŞHİR FOTOĞRAFI SENKRONIZASYON] Başlatılıyor...");
                Console.WriteLine($"Zaman: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                _logger.LogInformation("Synchronizing data with Server > MemberDisplayPhoto!");

                using var db = new SQLiteConnection(GlobalVariables.DatabasePath);
                var photos = db.Query<MemberDisplayPhotos>("SELECT * from entMemberDisplayPhotos WHERE IsSentToServer=false");
                
                Console.WriteLine($"[TEŞHİR FOTOĞRAFI SENKRONIZASYON] Tabloda {photos.Count} adet gönderilmemiş fotoğraf bulundu");

                foreach (var photo in photos)
                {
                    try
                    {
                        // DisplayPicturePath null/boş kontrolü
                        if (string.IsNullOrEmpty(photo.DisplayPicturePath))
                        {
                            Console.WriteLine($"[TEŞHİR FOTOĞRAFI SENKRONIZASYON] ❌ UYARI: Fotoğraf Id: {photo.Id} için DisplayPicturePath null veya boş, atlanıyor");
                            _logger.LogWarning($"Display photo Id: {photo.Id} has null or empty DisplayPicturePath, skipping");
                            continue; // Bu fotoğrafı atla, bir sonrakine geç
                        }
                        
                        Console.WriteLine($"[TEŞHİR FOTOĞRAFI SENKRONIZASYON] Fotoğraf gönderiliyor - Id: {photo.Id}, PromotionTypeId: {photo.PromotionTypeId}, CustomerProductId: {photo.CustomerProductId}, MemberRouteId: {photo.MemberRouteId}, DisplayPicturePath uzunluğu: {photo.DisplayPicturePath?.Length ?? 0}");
                        
                        // JSON serialize et ve sunucuya gönder
                        var json = Newtonsoft.Json.JsonConvert.SerializeObject(photo, new Newtonsoft.Json.JsonSerializerSettings
                        {
                            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                            DateFormatString = "yyyy-MM-dd HH:mm:ss"
                        });
                        
                        var content = new StringContent(json, Encoding.UTF8, "application/json");
                        var httpResponse = await _httpClient2.PostAsync($"api/MemberDisplayPhotosController/InsertMemberDisplayPhotos", content);
                        
                        string responseString = string.Empty;
                        if (httpResponse.Content != null)
                        {
                            responseString = await httpResponse.Content.ReadAsStringAsync();
                        }
                        
                        Console.WriteLine($"[TEŞHİR FOTOĞRAFI SENKRONIZASYON] Response içeriği: {responseString}");
                        Console.WriteLine($"[TEŞHİR FOTOĞRAFI SENKRONIZASYON] HTTP Status: {httpResponse.StatusCode}");

                        // Response kontrolü
                        bool isSuccess = false;
                        
                        if (httpResponse.IsSuccessStatusCode)
                        {
                            // Önce hata kontrolü yap (ExceptionMessage, "An error has occurred" vb.)
                            if (responseString.Contains("ExceptionMessage", StringComparison.OrdinalIgnoreCase) ||
                                responseString.Contains("An error has occurred", StringComparison.OrdinalIgnoreCase) ||
                                responseString.Contains("ArgumentNullException", StringComparison.OrdinalIgnoreCase))
                            {
                                Console.WriteLine($"[TEŞHİR FOTOĞRAFI SENKRONIZASYON] ❌ Response'da hata mesajı bulundu - başarısız olarak işaretleniyor");
                                isSuccess = false;
                            }
                            else
                            {
                                // Response'u parse et
                                try
                                {
                                    var responseJson = JObject.Parse(responseString);
                                    var result = responseJson?["Result"]?.ToString();
                                    
                                    if (result != null)
                                    {
                                        Console.WriteLine($"[TEŞHİR FOTOĞRAFI SENKRONIZASYON] Response Result: {result}");
                                        isSuccess = result != "NOK";
                                    }
                                    else
                                    {
                                        Console.WriteLine($"[TEŞHİR FOTOĞRAFI SENKRONIZASYON] UYARI: Response'da 'Result' key'i bulunamadı");
                                        // Result key'i yoksa ve hata mesajı da yoksa, başarılı say (eski API davranışı)
                                        isSuccess = true;
                                    }
                                }
                                catch (Exception parseEx)
                                {
                                    Console.WriteLine($"[TEŞHİR FOTOĞRAFI SENKRONIZASYON] UYARI: Response parse edilemedi: {parseEx.Message}");
                                    // Parse edilemezse ama HTTP başarılıysa, başarılı say
                                    isSuccess = true;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine($"[TEŞHİR FOTOĞRAFI SENKRONIZASYON] ❌ HTTP hatası: {httpResponse.StatusCode}");
                            _logger.LogWarning($"Database Sync Failed > MemberDisplayPhoto - Id: {photo.Id}, HTTP Status: {httpResponse.StatusCode}");
                            isSuccess = false;
                        }

                        if (isSuccess)
                        {
                            // Başarılı - IsSentToServer flag'ini güncelle (Id ile güncelle, CustomerProductId ile değil)
                            db.Execute("UPDATE entMemberDisplayPhotos SET IsSentToServer=? WHERE Id=?", 
                                true, photo.Id);
                            Console.WriteLine($"[TEŞHİR FOTOĞRAFI SENKRONIZASYON] ✅ Başarılı - Fotoğraf Id: {photo.Id} sunucuya gönderildi");
                            _logger.LogInformation($"Successfully synced display photo - Id: {photo.Id}");
                        }
                        else
                        {
                            // Başarısız - IsSentToServer flag'ini geri al
                            photo.IsSentToServer = false;
                            Console.WriteLine($"[TEŞHİR FOTOĞRAFI SENKRONIZASYON] ❌ Başarısız - Fotoğraf Id: {photo.Id} sunucuya gönderilemedi");
                            _logger.LogWarning($"Database Sync Failed > MemberDisplayPhoto - Id: {photo.Id}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[TEŞHİR FOTOĞRAFI SENKRONIZASYON] ❌ HATA (Fotoğraf Id: {photo.Id}): {ex.Message}");
                        Console.WriteLine($"[TEŞHİR FOTOĞRAFI SENKRONIZASYON] StackTrace: {ex.StackTrace}");
                        _logger.LogError(ex, $"Error syncing display photo - Id: {photo.Id}");
                        // Hata durumunda IsSentToServer flag'ini geri al
                        photo.IsSentToServer = false;
                    }
                }
                
                Console.WriteLine($"[TEŞHİR FOTOĞRAFI SENKRONIZASYON] Tamamlandı - {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                Console.WriteLine("========================================");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[TEŞHİR FOTOĞRAFI SENKRONIZASYON] ❌ GENEL HATA: {ex.Message}");
                Console.WriteLine($"[TEŞHİR FOTOĞRAFI SENKRONIZASYON] StackTrace: {ex.StackTrace}");
                _logger.LogError(ex, "Error occurred while syncing display photos");
            }
        }

        private async Task CheckDatabase()
        {
            using var db = new SQLiteConnection(GlobalVariables.DatabasePath);
            var configs = db.Query<MerchPlusMAUI.Models.entConfiguration>("SELECT * from entConfiguration WHERE Name=?", "RetailDataDateTime");

            if (configs.Any() && DateTime.TryParse(configs[0].Value, out var configDate))
            {
                if (configDate.Date < DateTime.Today)
                {
                    File.Delete(GlobalVariables.DatabasePath);
                    Application.Current.Quit();
                }
            }
        }

        private async Task<JsonElement?> PostToApi(HttpClient client, string endpoint, object data)
        {
            try
            {
                // System.Text.Json.JsonSerializer kullanıyoruz (belirsizliği önlemek için tam namespace)
                var jsonString = System.Text.Json.JsonSerializer.Serialize(data, _jsonOptions);
                var content = new StringContent(jsonString);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    
                var response = await client.PostAsync(endpoint, content);
                var responseString = await response.Content.ReadAsStringAsync();
                return System.Text.Json.JsonSerializer.Deserialize<JsonElement>(responseString);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error posting to API: {Endpoint}", endpoint);
                return null;
            }
        }

        /// <summary>
        /// Server'a gönderilmemiş verileri senkronize eder
        /// </summary>
        public async Task SyncPendingDataToServer()
        {
            try
            {
                _logger.LogInformation("Starting pending data sync to server...");

                using var db = new SQLiteConnection(GlobalVariables.DatabasePath);

                // Gönderilmemiş MemberRoute'ları al
                var pendingRoutes = db.Table<entMemberRoute>()
                    .Where(r => r.IsSentToServer == false)
                    .ToList();

                foreach (var route in pendingRoutes)
                {
                    await SyncMemberRouteToServer(route, db);
                }

                _logger.LogInformation($"Completed pending data sync. Processed {pendingRoutes.Count} routes.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during pending data sync to server");
            }
        }

        /// <summary>
        /// Tek bir MemberRoute'u server'a senkronize eder
        /// </summary>
        private async Task SyncMemberRouteToServer(entMemberRoute route, SQLiteConnection db)
        {
            try
            {
				// Prepare lists first to avoid referencing the variable inside its own initializer
				var memberRouteDetails = db.Table<entMemberRouteDetail>()
					.Where(d => d.MemberRouteId == route.Id && d.IsSentToServer == false)
					.ToList();

				var memberRoutePhotos = db.Table<entMemberRoutePhoto>()
					.Where(p => p.MemberRouteId == route.Id && p.IsSentToServer == false)
					.ToList();

				var salesOrders = db.Table<SalesOrder>()
					.Where(s => s.MemberRouteId == route.Id && s.IsSentToServer == false)
					.ToList();

				var salesOrderIds = salesOrders.Select(s => s.Id).ToList();
				var salesOrderDetails = db.Table<SalesOrderDetail>()
					.Where(sd => sd.SalesOrderId != 0 && salesOrderIds.Contains(sd.SalesOrderId) && sd.IsSentToServer == false)
					.ToList();

				var productExpirations = db.Table<ProductExpiration>()
					.Where(pe => pe.MemberRouteId == route.Id && pe.IsSentToServer == false)
					.ToList();

				var displayPhotos = db.Table<MemberDisplayPhotos>()
					.Where(mdp => mdp.MemberRouteId == route.Id && mdp.IsSentToServer == false)
					.ToList();

				var syncData = new MemberRouteSyncData
				{
					MemberRoute = route,
					MemberRouteDetail = memberRouteDetails,
					MemberRoutePhoto = memberRoutePhotos,
					SalesOrder = salesOrders,
					SalesOrderDetail = salesOrderDetails,
					ProductExpiration = productExpirations,
					MemberDisplayPhotos = displayPhotos
				};

                // Server'a gönder
                // System.Text.Json.JsonSerializer kullanıyoruz (belirsizliği önlemek için tam namespace)
                var json = System.Text.Json.JsonSerializer.Serialize(syncData, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/MemberRoute/Sync", content);
                
                if (response.IsSuccessStatusCode)
                {
                    // Başarılı gönderim sonrası flag'leri güncelle
                    await MarkDataAsSentToServer(route.Id, db);
                    _logger.LogInformation($"Successfully synced MemberRoute {route.Id} to server");
                }
                else
                {
                    _logger.LogWarning($"Failed to sync MemberRoute {route.Id} to server. Status: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error syncing MemberRoute {route.Id} to server");
            }
        }

        /// <summary>
        /// Başarılı senkronizasyon sonrası flag'leri günceller
        /// </summary>
        private async Task MarkDataAsSentToServer(int memberRouteId, SQLiteConnection db)
        {
            try
            {
                var now = DateTime.UtcNow;

                // MemberRoute'u güncelle
                var route = db.Table<entMemberRoute>().FirstOrDefault(r => r.Id == memberRouteId);
                if (route != null)
                {
                    route.IsSentToServer = true;
                    route.SentToServerOn = now;
                    db.Update(route);
                }

                // İlgili detayları güncelle
                var details = db.Table<entMemberRouteDetail>()
                    .Where(d => d.MemberRouteId == memberRouteId && d.IsSentToServer == false)
                    .ToList();
                foreach (var detail in details)
                {
                    detail.IsSentToServer = true;
                    detail.SentToServerOn = now;
                    db.Update(detail);
                }

                // Fotoğrafları güncelle
                var photos = db.Table<entMemberRoutePhoto>()
                    .Where(p => p.MemberRouteId == memberRouteId && p.IsSentToServer == false)
                    .ToList();
                foreach (var photo in photos)
                {
                    photo.IsSentToServer = true;
                    // Eski projede entMemberRoutePhoto'da SentToServerOn property'si yok
                    db.Update(photo);
                }

                // Satış siparişlerini güncelle
                var salesOrders = db.Table<SalesOrder>()
                    .Where(s => s.MemberRouteId == memberRouteId && s.IsSentToServer == false)
                    .ToList();
                foreach (var order in salesOrders)
                {
                    order.IsSentToServer = true;
                    order.SentToServerOn = now;
                    db.Update(order);
                }

                // Satış sipariş detaylarını güncelle
                var salesOrderDetails = db.Table<SalesOrderDetail>()
                    .Where(sd => salesOrders.Any(s => s.Id == sd.SalesOrderId) && sd.IsSentToServer == false)
                    .ToList();
                foreach (var detail in salesOrderDetails)
                {
                    detail.IsSentToServer = true;
                    detail.SentToServerOn = now;
                    db.Update(detail);
                }

                // Ürün son kullanma tarihlerini güncelle
                var expirations = db.Table<ProductExpiration>()
                    .Where(pe => pe.MemberRouteId == memberRouteId && pe.IsSentToServer == false)
                    .ToList();
                foreach (var expiration in expirations)
                {
                    expiration.IsSentToServer = true;
                    expiration.SentToServerOn = now;
                    db.Update(expiration);
                }

                // Mağaza fotoğraflarını güncelle
                var displayPhotos = db.Table<MemberDisplayPhotos>()
                    .Where(mdp => mdp.MemberRouteId == memberRouteId && mdp.IsSentToServer == false)
                    .ToList();
                foreach (var photo in displayPhotos)
                {
                    photo.IsSentToServer = true;
                    photo.SentToServerOn = now;
                    db.Update(photo);
                }

                _logger.LogInformation($"Marked all data for MemberRoute {memberRouteId} as sent to server");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error marking data as sent to server for MemberRoute {memberRouteId}");
            }
        }
    }

    // Helper classes for sync operations
    public class MemberRouteSyncData
    {
        public entMemberRoute MemberRoute { get; set; }
        public List<entMemberRouteDetail> MemberRouteDetail { get; set; } = new List<entMemberRouteDetail>();
        public List<entMemberRoutePhoto> MemberRoutePhoto { get; set; } = new List<entMemberRoutePhoto>();
        public List<SalesOrder> SalesOrder { get; set; } = new List<SalesOrder>();
        public List<SalesOrderDetail> SalesOrderDetail { get; set; } = new List<SalesOrderDetail>();
        public List<ProductExpiration> ProductExpiration { get; set; } = new List<ProductExpiration>();
        public List<MemberDisplayPhotos> MemberDisplayPhotos { get; set; } = new List<MemberDisplayPhotos>();
    }

} 