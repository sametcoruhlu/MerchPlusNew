using SQLite;
using Microsoft.Extensions.Logging;
using entMerchPlus;
using System.Net.Http.Headers;
using MerchPlusMAUI.Models;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using Microsoft.Maui.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace MerchPlusMAUI.Views
{
    public partial class AboutMePage : ContentPage
    {
        private readonly ILogger<AboutMePage> _logger;
        private readonly string _dbPath;

        public AboutMePage() : this(null)
        {
            System.Diagnostics.Debug.WriteLine("AboutMePage parametresiz constructor çalıştı!");
        }

        public AboutMePage(ILogger<AboutMePage> logger)
        {
            try
            {
                InitializeComponent();
                _logger = logger;
                _dbPath = GlobalVariables.DatabasePath;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AboutMePage initialize hatası: {ex.Message}");
                _logger?.LogError(ex, "AboutMePage başlatma hatası");
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            // Sayfa başlangıçta görünür olsun (siyah ekranı önlemek için)
            this.Opacity = 1;
            
            System.Diagnostics.Debug.WriteLine("AboutMePage açıldı!");
            System.Diagnostics.Debug.WriteLine("AboutMePage Content Loaded");
            
            // Loading overlay'i göster
            var loadingOverlay = this.FindByName<Grid>("LoadingOverlay");
            if (loadingOverlay != null)
            {
                loadingOverlay.IsVisible = true;
            }
            
            // Her sayfa açılışında verileri yeniden yükle
            await LoadDataAsync();
            
            // Veriler yüklendikten sonra loading overlay'i gizle
            if (loadingOverlay != null)
            {
                loadingOverlay.IsVisible = false;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            
            // FadeTo(0) kaldırıldı - Shell navigation kendi animasyonunu yönetiyor
        }

        private async Task LoadDataAsync()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("AboutMePage: Veri yükleme başlatılıyor...");
                
                // İstatistikleri hesapla
                CalculateAndUpdateStatistics();
                
                // CommonFooter'daki badge'i güncelle
                UpdateFooterBadge();
                
                System.Diagnostics.Debug.WriteLine("AboutMePage: Veri yükleme tamamlandı.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AboutMePage: Veri yükleme hatası: {ex.Message}");
                _logger?.LogError(ex, "AboutMePage veri yükleme hatası");
            }
        }

        // CommonFooter'daki badge'i güncelle
        private void UpdateFooterBadge()
        {
            try
            {
                // CommonFooter'ı bul
                var footer = this.FindByName<Controls.CommonFooter>("CommonFooter");
                if (footer != null)
                {
                    footer.UpdateButtonText();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Footer badge güncelleme hatası: {ex.Message}");
            }
        }

        private void CalculateAndUpdateStatistics()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("AboutMePage: İstatistikler hesaplanıyor...");
                
                using var db = new SQLiteConnection(_dbPath);
                
                // Toplam planlanan mağaza sayısı (tüm routes)
                var plannedCount = db.ExecuteScalar<int>("SELECT COUNT(*) FROM entMemberRoute");
                
                // Tamamlanan mağaza sayısı (ExitOn is not null)
                var completedCount = db.ExecuteScalar<int>("SELECT COUNT(*) FROM entMemberRoute WHERE ExitOn IS NOT NULL");
                
                // Bekleyen mağaza sayısı (ExitOn IS NULL - tamamlanmamış tüm mağazalar)
                // Bu, Planlanan (henüz başlamamış) + Giriş yapılmış ama çıkış yapılmamış mağazaları içerir
                var pendingCount = db.ExecuteScalar<int>("SELECT COUNT(*) FROM entMemberRoute WHERE ExitOn IS NULL");
                
                System.Diagnostics.Debug.WriteLine($"AboutMePage: İstatistikler - Planlanan: {plannedCount}, Tamamlanan: {completedCount}, Bekleyen (Tamamlanmamış): {pendingCount}");
                
                // UI'yi güncelle
                PlannedCountLabel.Text = plannedCount.ToString();
                CompletedCountLabel.Text = completedCount.ToString();
                PendingCountLabel.Text = pendingCount.ToString();
                
                System.Diagnostics.Debug.WriteLine($"AboutMePage: UI güncellendi - Planlanan: {PlannedCountLabel.Text}, Tamamlanan: {CompletedCountLabel.Text}, Bekleyen: {PendingCountLabel.Text}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AboutMePage: İstatistik hesaplama hatası: {ex.Message}");
                _logger?.LogError(ex, "İstatistikler hesaplanırken hata oluştu");
                
                // UI'yı varsayılan değerlere ayarla
                PlannedCountLabel.Text = "0";
                CompletedCountLabel.Text = "0";
                PendingCountLabel.Text = "0";
            }
        }

        private async void OnSubmitDataClicked(object sender, EventArgs e)
        {
            try
            {
                bool answer = await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    return await DisplayAlert("Onay", 
                        "Tüm verileri sunucuya göndermek istediğinizden emin misiniz?", 
                        "Evet", "Hayır");
                });

                if (answer)
                {
                    // Trigger immediate sync
                    await SyncData();
                    
                    // CommonFooter'daki badge'i güncelle
                    UpdateFooterBadge();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error submitting data");
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    await DisplayAlert("Hata", "Veriler gönderilirken bir hata oluştu.", "Tamam");
                });
            }
        }

        private async void OnDeleteDbClicked(object sender, EventArgs e)
        {
            try
            {
                bool answer = await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    return await DisplayAlert("Dikkat!", 
                        "Veritabanını silmek istediğinizden emin misiniz? Bu işlem geri alınamaz!", 
                        "Evet", "Hayır");
                });

                if (answer)
                {
                    File.Delete(_dbPath);
                    await MainThread.InvokeOnMainThreadAsync(async () =>
                    {
                        await DisplayAlert("Başarılı", "Veritabanı başarıyla silindi.", "Tamam");
                    });
                    Microsoft.Maui.Controls.Application.Current.Quit();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting database");
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    await DisplayAlert("Hata", "Veritabanı silinirken bir hata oluştu.", "Tamam");
                });
            }
        }

        private async Task SyncData()
        {
            using var db = new SQLiteConnection(_dbPath);
            
            // Show loading indicator
            IsBusy = true;
            
            try
            {
                // Sync coordinates
                var coordinates = db.Table<MerchPlusMAUI.Models.entMemberCoordinate>().ToList();
                if (coordinates.Any())
                {
                    await SyncCoordinates(coordinates);
                }

                // Sync routes
                var routes = db.Table<entMemberRoute>().Where(r => r.IsSentToServer != true).ToList();
                if (routes.Any())
                {
                    await SyncRoutes(routes, db);
                }

                // Sync photos
                var photos = db.Table<MemberDisplayPhotos>().Where(p => !p.IsSentToServer != true).ToList();
                if (photos.Any())
                {
                    await SyncPhotos(photos, db);
                }

                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    await DisplayAlert("Başarılı", "Tüm veriler başarıyla gönderildi.", "Tamam");
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during manual sync");
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    await DisplayAlert("Hata", "Senkronizasyon sırasında bir hata oluştu.", "Tamam");
                });
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task SyncCoordinates(List<MerchPlusMAUI.Models.entMemberCoordinate> coordinates)
        {
            using var client = new HttpClient { BaseAddress = new Uri(GlobalVariables.APIPath) };
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var jsonString = System.Text.Json.JsonSerializer.Serialize(coordinates);
            var content = new StringContent(jsonString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            
            var response = await client.PostAsync("MemberCoordinateController/InsertMemberCoordinateFromAndroidArray", content);
            
            if (response.IsSuccessStatusCode)
            {
                // Silmeden önce son koordinat bilgisini GlobalVariables'a kaydet
                var lastCoordinate = coordinates.OrderByDescending(c => c.LastUpdateTime ?? c.CreatedOn ?? DateTime.MinValue).FirstOrDefault();
                if (lastCoordinate != null && lastCoordinate.Latitude.HasValue && lastCoordinate.Longitude.HasValue)
                {
                    GlobalVariables.MemberLastCoordinate = new MerchPlusMAUI.Location
                    {
                        Latitude = lastCoordinate.Latitude.Value,
                        Longitude = lastCoordinate.Longitude.Value
                    };
                    Console.WriteLine($"[AboutMePage] Son koordinat GlobalVariables'a kaydedildi: Lat={lastCoordinate.Latitude}, Lon={lastCoordinate.Longitude}");
                }
                
                using var db = new SQLiteConnection(_dbPath);
                db.DeleteAll<MerchPlusMAUI.Models.entMemberCoordinate>();
            }
        }

        private async Task SyncRoutes(List<entMemberRoute> routes, SQLiteConnection db)
        {
            using var client = new HttpClient { BaseAddress = new Uri(GlobalVariables.APIPath) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(0, 10, 0); // 10 dakika timeout (eski projede olduğu gibi)

            foreach (var route in routes)
            {
                // Eski projede olduğu gibi tüm verileri topla
                // Eski projede entMemberRouteDetail, entSalesOrder, entSalesOrderDetail, entProductExpiration kullanılıyor
                // Veritabanından raw SQL ile okuyup entMerchPlus entity'lerine map ediyoruz
                // Tam namespace belirtiyoruz çünkü Models namespace'inde de aynı isimde class'lar var
                var detailRows = db.Query<entMerchPlus.entMemberRouteDetail>(
                    "SELECT * FROM entMemberRouteDetail WHERE MemberRouteId=?", 
                    route.Id).ToList();

                // Raf fotoğrafları - sadece gönderilmemiş olanları al (IsSentToServer=false)
                var photoRows = db.Query<entMerchPlus.entMemberRoutePhoto>(
                    "SELECT * FROM entMemberRoutePhoto WHERE MemberRouteId=? AND IsSentToServer=?", 
                    route.Id, false).ToList();
                
                System.Diagnostics.Debug.WriteLine($"[AboutMePage] Route {route.Id} için {photoRows.Count} adet gönderilmemiş raf fotoğrafı bulundu");

                var salesOrder = db.Query<entMerchPlus.entSalesOrder>(
                    "SELECT * FROM entSalesOrder WHERE MemberRouteId=?", 
                    route.Id).ToList();

                List<entMerchPlus.entSalesOrderDetail> salesOrderDetail = new List<entMerchPlus.entSalesOrderDetail>();
                if (salesOrder.Any())
                {
                    var salesOrderIds = salesOrder.Select(s => s.Id).ToList();
                    foreach (var soId in salesOrderIds)
                    {
                        var details = db.Query<entMerchPlus.entSalesOrderDetail>(
                            "SELECT * FROM entSalesOrderDetail WHERE SalesOrderId=?", 
                            soId).ToList();
                        salesOrderDetail.AddRange(details);
                    }
                }

                var productExpiration = db.Query<entMerchPlus.entProductExpiration>(
                    "SELECT * FROM entProductExpiration WHERE MemberRouteId=?", 
                    route.Id).ToList();

                // MemberRouteSyncData oluştur (eski projede olduğu gibi)
                // Eski projede property'ler Add() ile dolduruluyor, aynısını yapıyoruz
                MemberRouteSyncData syncData = new MemberRouteSyncData();
                syncData.MemberRoute = route;

                foreach (entMerchPlus.entMemberRouteDetail memberRouteDetail in detailRows)
                {
                    syncData.MemberRouteDetail.Add(memberRouteDetail);
                }

                foreach (entMerchPlus.entMemberRoutePhoto memberRoutePhoto in photoRows)
                {
                    syncData.MemberRoutePhoto.Add(memberRoutePhoto);
                }

                if (salesOrder.Any())
                {
                    foreach (entMerchPlus.entSalesOrder salesOrderEntity in salesOrder)
                    {
                        syncData.SalesOrder.Add(salesOrderEntity);
                    }

                    foreach (entMerchPlus.entSalesOrderDetail salesOrderDetailEntity in salesOrderDetail)
                    {
                        syncData.SalesOrderDetail.Add(salesOrderDetailEntity);
                    }
                }

                if (productExpiration.Any())
                {
                    foreach (entMerchPlus.entProductExpiration productExpirationEntity in productExpiration)
                    {
                        syncData.ProductExpiration.Add(productExpirationEntity);
                    }
                }

                // DateTime alanlarını kontrol et ve null olanları düzelt (sunucu null DateTime kabul etmiyor)
                FixDateTimeFields(syncData);

                // Endpoint belirle (eski projede olduğu gibi)
                var endpoint = salesOrder.Any()
                    ? "MemberRouteController/CompleteMemberRouteWiselyFromJsonArrayWithSalesOrder"
                    : "MemberRouteController/CompleteMemberRouteWiselyFromJsonArray";

                // JSON serialize (eski projede Newtonsoft.Json kullanılıyor, aynısını kullanıyoruz)
                // Newtonsoft.Json default olarak PascalCase serialize eder, sunucu bunu bekliyor
                var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(syncData);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                
                var response = await client.PostAsync(endpoint, content);
                var responseString = await response.Content.ReadAsStringAsync();

                // Response kontrolü (eski projede olduğu gibi - Result != "NOK")
                // Eski projede Newtonsoft.Json kullanılıyor, aynısını kullanıyoruz
                if (response.IsSuccessStatusCode)
                {
                    // JSON response'u parse et (eski projede olduğu gibi - Newtonsoft.Json)
                    try
                    {
                        Newtonsoft.Json.Linq.JObject returnObject = (Newtonsoft.Json.Linq.JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(responseString);
                        if (Convert.ToString(returnObject["Result"]) != "NOK")
                        {
                            // Başarılı - flag'leri güncelle (eski projede olduğu gibi)
                            db.Execute("UPDATE entMemberRoute SET IsSentToServer=? WHERE Id=?", true, route.Id);
                            db.Execute("UPDATE entMemberRouteDetail SET IsSentToServer=? WHERE MemberRouteId=?", true, route.Id);
                            db.Execute("UPDATE entProductExpiration SET IsSentToServer=? WHERE MemberRouteId=?", true, route.Id);
                            
                            // Raf fotoğrafları flag'lerini de güncelle (IsSentToServer=false olanları)
                            if (photoRows.Any())
                            {
                                foreach (var photo in photoRows)
                                {
                                    db.Execute("UPDATE entMemberRoutePhoto SET IsSentToServer=? WHERE Id=?", true, photo.Id);
                                    System.Diagnostics.Debug.WriteLine($"[AboutMePage] Raf fotoğrafı Id: {photo.Id} için IsSentToServer=true yapıldı");
                                }
                            }
                            
                            // SalesOrder ve SalesOrderDetail flag'lerini de güncelle
                            if (salesOrder.Any())
                            {
                                foreach (var so in salesOrder)
                                {
                                    db.Execute("UPDATE entSalesOrder SET IsSentToServer=? WHERE Id=?", true, so.Id);
                                }
                                foreach (var sod in salesOrderDetail)
                                {
                                    db.Execute("UPDATE entSalesOrderDetail SET IsSentToServer=? WHERE Id=?", true, sod.Id);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // JSON parse hatası, ama HTTP başarılı
                        System.Diagnostics.Debug.WriteLine($"Response parse hatası: {ex.Message}");
                        db.Execute("UPDATE entMemberRoute SET IsSentToServer=? WHERE Id=?", true, route.Id);
                        db.Execute("UPDATE entMemberRouteDetail SET IsSentToServer=? WHERE MemberRouteId=?", true, route.Id);
                        db.Execute("UPDATE entProductExpiration SET IsSentToServer=? WHERE MemberRouteId=?", true, route.Id);
                        
                        // Raf fotoğrafları flag'lerini de güncelle
                        if (photoRows.Any())
                        {
                            foreach (var photo in photoRows)
                            {
                                db.Execute("UPDATE entMemberRoutePhoto SET IsSentToServer=? WHERE Id=?", true, photo.Id);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Sunucuya gönderilmeden önce DateTime alanlarını kontrol eder ve null olanları düzeltir
        /// </summary>
        private void FixDateTimeFields(MemberRouteSyncData syncData)
        {
            var now = DateTime.Now;

            // MemberRoute DateTime alanlarını kontrol et
            if (syncData.MemberRoute != null)
            {
                var routeType = typeof(entMerchPlus.entMemberRoute);
                var createdOnProp = routeType.GetProperty("CreatedOn");
                if (createdOnProp != null)
                {
                    var value = createdOnProp.GetValue(syncData.MemberRoute);
                    if (value == null || (value is DateTime dt && dt == default(DateTime)))
                    {
                        createdOnProp.SetValue(syncData.MemberRoute, now);
                        System.Diagnostics.Debug.WriteLine($"[AboutMePage] MemberRoute.CreatedOn null/default, {now} olarak set edildi");
                    }
                }
            }

            // MemberRouteDetail DateTime alanlarını kontrol et
            foreach (var detail in syncData.MemberRouteDetail)
            {
                var detailType = typeof(entMerchPlus.entMemberRouteDetail);
                var createdOnProp = detailType.GetProperty("CreatedOn");
                if (createdOnProp != null)
                {
                    var value = createdOnProp.GetValue(detail);
                    if (value == null || (value is DateTime dt && dt == default(DateTime)))
                    {
                        createdOnProp.SetValue(detail, now);
                        System.Diagnostics.Debug.WriteLine($"[AboutMePage] MemberRouteDetail[{detail.Id}].CreatedOn null/default, {now} olarak set edildi");
                    }
                }
            }

            // MemberRoutePhoto DateTime alanlarını kontrol et
            foreach (var photo in syncData.MemberRoutePhoto)
            {
                var photoType = typeof(entMerchPlus.entMemberRoutePhoto);
                var createdOnProp = photoType.GetProperty("CreatedOn");
                if (createdOnProp != null)
                {
                    var value = createdOnProp.GetValue(photo);
                    if (value == null || (value is DateTime dt && dt == default(DateTime)))
                    {
                        createdOnProp.SetValue(photo, now);
                        System.Diagnostics.Debug.WriteLine($"[AboutMePage] MemberRoutePhoto[{photo.Id}].CreatedOn null/default, {now} olarak set edildi");
                    }
                }
            }

            // ProductExpiration DateTime alanlarını kontrol et
            foreach (var expiration in syncData.ProductExpiration)
            {
                var expirationType = typeof(entMerchPlus.entProductExpiration);
                var createdOnProp = expirationType.GetProperty("CreatedOn");
                var expirationDateProp = expirationType.GetProperty("ExpirationDate");
                
                if (createdOnProp != null)
                {
                    var value = createdOnProp.GetValue(expiration);
                    if (value == null || (value is DateTime dt && dt == default(DateTime)))
                    {
                        createdOnProp.SetValue(expiration, now);
                        System.Diagnostics.Debug.WriteLine($"[AboutMePage] ProductExpiration[{expiration.Id}].CreatedOn null/default, {now} olarak set edildi");
                    }
                }
                
                if (expirationDateProp != null)
                {
                    var value = expirationDateProp.GetValue(expiration);
                    if (value == null || (value is DateTime dt && dt == default(DateTime)))
                    {
                        expirationDateProp.SetValue(expiration, now);
                        System.Diagnostics.Debug.WriteLine($"[AboutMePage] ProductExpiration[{expiration.Id}].ExpirationDate null/default, {now} olarak set edildi");
                    }
                }
            }

            // SalesOrder DateTime alanlarını kontrol et
            foreach (var salesOrder in syncData.SalesOrder)
            {
                var salesOrderType = typeof(entMerchPlus.entSalesOrder);
                var createdOnProp = salesOrderType.GetProperty("CreatedOn");
                var orderDateProp = salesOrderType.GetProperty("OrderDate");
                
                if (createdOnProp != null)
                {
                    var value = createdOnProp.GetValue(salesOrder);
                    if (value == null || (value is DateTime dt && dt == default(DateTime)))
                    {
                        createdOnProp.SetValue(salesOrder, now);
                        System.Diagnostics.Debug.WriteLine($"[AboutMePage] SalesOrder[{salesOrder.Id}].CreatedOn null/default, {now} olarak set edildi");
                    }
                }
                
                if (orderDateProp != null)
                {
                    var value = orderDateProp.GetValue(salesOrder);
                    if (value == null || (value is DateTime dt && dt == default(DateTime)))
                    {
                        orderDateProp.SetValue(salesOrder, now);
                        System.Diagnostics.Debug.WriteLine($"[AboutMePage] SalesOrder[{salesOrder.Id}].OrderDate null/default, {now} olarak set edildi");
                    }
                }
            }

            // SalesOrderDetail DateTime alanlarını kontrol et
            foreach (var detail in syncData.SalesOrderDetail)
            {
                var detailType = typeof(entMerchPlus.entSalesOrderDetail);
                var createdOnProp = detailType.GetProperty("CreatedOn");
                
                if (createdOnProp != null)
                {
                    var value = createdOnProp.GetValue(detail);
                    if (value == null || (value is DateTime dt && dt == default(DateTime)))
                    {
                        createdOnProp.SetValue(detail, now);
                        System.Diagnostics.Debug.WriteLine($"[AboutMePage] SalesOrderDetail[{detail.Id}].CreatedOn null/default, {now} olarak set edildi");
                    }
                }
            }
        }

        private async Task SyncPhotos(List<MemberDisplayPhotos> photos, SQLiteConnection db)
        {
            try
            {
                Console.WriteLine("========================================");
                Console.WriteLine("[AboutMePage - TEŞHİR FOTOĞRAFI SENKRONIZASYON] Başlatılıyor...");
                Console.WriteLine($"Zaman: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                Console.WriteLine($"[AboutMePage] {photos.Count} adet fotoğraf gönderilecek");

                using var client = new HttpClient { BaseAddress = new Uri(GlobalVariables.APIPath2) };
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                foreach (var photo in photos)
                {
                    try
                    {
                        // DisplayPicturePath null/boş kontrolü
                        if (string.IsNullOrEmpty(photo.DisplayPicturePath))
                        {
                            Console.WriteLine($"[AboutMePage] ❌ UYARI: Fotoğraf Id: {photo.Id} için DisplayPicturePath null veya boş, atlanıyor");
                            _logger.LogWarning($"Display photo Id: {photo.Id} has null or empty DisplayPicturePath, skipping");
                            continue; // Bu fotoğrafı atla, bir sonrakine geç
                        }
                        
                        Console.WriteLine($"[AboutMePage] Fotoğraf gönderiliyor - Id: {photo.Id}, PromotionTypeId: {photo.PromotionTypeId}, CustomerProductId: {photo.CustomerProductId}, MemberRouteId: {photo.MemberRouteId}, DisplayPicturePath uzunluğu: {photo.DisplayPicturePath?.Length ?? 0}");
                        
                        var jsonString = System.Text.Json.JsonSerializer.Serialize(photo);
                        var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                        
                        var response = await client.PostAsync("MemberDisplayPhotosController/InsertMemberDisplayPhotos", content);
                        var responseString = await response.Content.ReadAsStringAsync();
                        
                        Console.WriteLine($"[AboutMePage] HTTP Status: {response.StatusCode}, Response: {responseString}");

                        // Response kontrolü - DatabaseSyncService'deki gibi
                        bool isSuccess = false;
                        if (response.IsSuccessStatusCode)
                        {
                            // Önce hata kontrolü yap (ExceptionMessage, "An error has occurred" vb.)
                            if (responseString.Contains("ExceptionMessage", StringComparison.OrdinalIgnoreCase) ||
                                responseString.Contains("An error has occurred", StringComparison.OrdinalIgnoreCase) ||
                                responseString.Contains("ArgumentNullException", StringComparison.OrdinalIgnoreCase))
                            {
                                Console.WriteLine($"[AboutMePage] ❌ Response'da hata mesajı bulundu - başarısız olarak işaretleniyor");
                                isSuccess = false;
                            }
                            else
                            {
                                try
                                {
                                    var responseJson = System.Text.Json.JsonSerializer.Deserialize<System.Text.Json.JsonElement>(responseString);
                                    if (responseJson.TryGetProperty("Result", out var resultProperty))
                                    {
                                        var result = resultProperty.GetString();
                                        Console.WriteLine($"[AboutMePage] Response Result: {result}");
                                        isSuccess = result != "NOK";
                                    }
                                    else
                                    {
                                        // Result key'i yoksa ve hata mesajı da yoksa, başarılı say (eski API davranışı)
                                        Console.WriteLine($"[AboutMePage] UYARI: Response'da 'Result' key'i bulunamadı, hata mesajı da yok - başarılı sayılıyor");
                                        isSuccess = true;
                                    }
                                }
                                catch
                                {
                                    // JSON parse hatası, ama HTTP başarılı ve hata mesajı yok
                                    isSuccess = true;
                                }
                            }
                        }

                        if (isSuccess)
                        {
                            // Başarılı - IsSentToServer flag'ini güncelle (Id ile, CustomerProductId ile değil!)
                            db.Execute("UPDATE entMemberDisplayPhotos SET IsSentToServer=? WHERE Id=?", 
                                true, photo.Id);
                            Console.WriteLine($"[AboutMePage] ✅ Başarılı - Fotoğraf Id: {photo.Id} sunucuya gönderildi ve IsSentToServer=true yapıldı");
                        }
                        else
                        {
                            Console.WriteLine($"[AboutMePage] ❌ Başarısız - Fotoğraf Id: {photo.Id} sunucuya gönderilemedi");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[AboutMePage] ❌ HATA (Fotoğraf Id: {photo.Id}): {ex.Message}");
                        _logger.LogError(ex, $"Error syncing display photo - Id: {photo.Id}");
                    }
                }
                
                Console.WriteLine($"[AboutMePage - TEŞHİR FOTOĞRAFI SENKRONIZASYON] Tamamlandı - {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                Console.WriteLine("========================================");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AboutMePage] ❌ GENEL HATA: {ex.Message}");
                _logger.LogError(ex, "Error occurred while syncing display photos");
            }
        }

        // Info butonuna tıklandığında
        private async void OnInfoButtonClicked(object sender, EventArgs e)
        {
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                await DisplayAlert("Bilgi", 
                    "Mağazalar Bilgileri:\n\n" +
                    "• Planlanan: Ziyaret planınızdaki toplam mağaza sayısı\n" +
                    "• Tamamlanan: Ziyaretini tamamladığınız mağaza sayısı\n" +
                    "• Bekleyen: Henüz ziyaret etmediğiniz mağaza sayısı\n\n" +
                    "Kategorilere tıklayarak ilgili mağaza listesini görüntüleyebilirsiniz.\n\n" +
                    "Veri gönderme ve veritabanı temizleme işlemleri için alt menüdeki 'Güncelle' ve 'Temizle' butonlarını kullanabilirsiniz.", 
                    "Tamam");
            });
        }

        // Planlanan tab'ına tıklandığında
        private async void OnPlannedTapped(object sender, EventArgs e)
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
                
                // Planlanan mağazaları göster - TÜM mağazalar listelenecek
                await Shell.Current.GoToAsync($"///StoresPage?StoreType=All");
                
                // Navigation tamamlandı, loading overlay'i gizle (hedef sayfa kendi overlay'ini gösterecek)
                if (loadingOverlay != null)
                {
                    loadingOverlay.IsVisible = false;
                    loadingOverlay.InputTransparent = true;
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Tüm mağazalar sayfasına yönlendirme hatası");
                var loadingOverlay = this.FindByName<Grid>("LoadingOverlay");
                if (loadingOverlay != null)
                {
                    loadingOverlay.IsVisible = false;
                    loadingOverlay.InputTransparent = true;
                }
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    await DisplayAlert("Hata", "Mağaza listesi yüklenirken bir hata oluştu.", "Tamam");
                });
            }
        }
        
        // Tamamlanan tab'ına tıklandığında
        private async void OnCompletedTapped(object sender, EventArgs e)
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
                
                // Tamamlanan mağazaları göster (giriş yapılmış VE çıkış yapılmış)
                await Shell.Current.GoToAsync($"///StoresPage?StoreType=Completed");
                
                // Navigation tamamlandı, loading overlay'i gizle (hedef sayfa kendi overlay'ini gösterecek)
                if (loadingOverlay != null)
                {
                    loadingOverlay.IsVisible = false;
                    loadingOverlay.InputTransparent = true;
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Tamamlanan mağazalar sayfasına yönlendirme hatası");
                var loadingOverlay = this.FindByName<Grid>("LoadingOverlay");
                if (loadingOverlay != null)
                {
                    loadingOverlay.IsVisible = false;
                    loadingOverlay.InputTransparent = true;
                }
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    await DisplayAlert("Hata", "Mağaza listesi yüklenirken bir hata oluştu.", "Tamam");
                });
            }
        }
        
        // Bekleyen butonuna tıklandığında
        private async void OnPendingTapped(object sender, EventArgs e)
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
                
                // Bekleyen mağazaları göster - tamamlanmamış tüm mağazalar (ExitOn IS NULL)
                await Shell.Current.GoToAsync($"///StoresPage?StoreType=Pending");
                
                // Navigation tamamlandı, loading overlay'i gizle (hedef sayfa kendi overlay'ini gösterecek)
                if (loadingOverlay != null)
                {
                    loadingOverlay.IsVisible = false;
                    loadingOverlay.InputTransparent = true;
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Bekleyen mağazalar sayfasına yönlendirme hatası");
                var loadingOverlay = this.FindByName<Grid>("LoadingOverlay");
                if (loadingOverlay != null)
                {
                    loadingOverlay.IsVisible = false;
                    loadingOverlay.InputTransparent = true;
                }
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    await DisplayAlert("Hata", "Mağaza listesi yüklenirken bir hata oluştu.", "Tamam");
                });
            }
        }
    }
    
    // Görev modeli
    public class ReportItem
    {
        public string Name { get; set; }
        public int CompletionPercentage { get; set; }
    }
} 