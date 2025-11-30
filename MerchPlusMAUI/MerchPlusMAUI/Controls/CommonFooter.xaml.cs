using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.Net.Http;
using System.Net.Http.Headers;
using entMerchPlus;
using MerchPlusMAUI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;
// Çakışmayı önlemek için alias tanımlıyoruz
using MauiControls = Microsoft.Maui.Controls;
using MauiShell = Microsoft.Maui.Controls.Shell;

namespace MerchPlusMAUI.Controls
{
    public partial class CommonFooter : MauiControls.ContentView
    {
        // Tıklama olayları için event handler tanımları
        public event EventHandler HomeClicked;
        public event EventHandler MapClicked;
        public event EventHandler TasksClicked;
        public event EventHandler UpdateDbClicked;
        public event EventHandler DeleteDbClicked;
        
        // Servislere erişim için
        private MerchPlusMAUI.Services.DatabaseService _databaseService;
        private readonly string _dbPath;
        private bool _isBusy = false;

        public CommonFooter()
        {
            InitializeComponent();
            
            // Database path
            _dbPath = GlobalVariables.DatabasePath;
            
            // Servisleri elde et
            try
            {
                _databaseService = MerchPlusMAUI.Services.DatabaseService.Instance;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Veritabanı servisi alınamadı: {ex.Message}");
            }
            
            // Buton metnini güncelle (eski projede olduğu gibi)
            UpdateButtonText();
        }

        // Buton metnini güncelle ve badge göster (eski projede olduğu gibi - badge ile sayı göster)
        public void UpdateButtonText()
        {
            try
            {
                using var db = new SQLiteConnection(_dbPath);
                
                // Gönderilmemiş route sayısı
                var remainingRoutes = db.Table<entMemberRoute>().Where(r => r.IsSentToServer != true).Count();
                
                // Gönderilmemiş route photo sayısı
                var remainingPhotos = db.Table<entMemberRoutePhoto>().Where(p => p.IsSentToServer != true).Count();
                
                // Gönderilmemiş display photo sayısı
                var remainingDisplayPhotos = db.Table<MemberDisplayPhotos>().Where(p => !p.IsSentToServer).Count();
                
                // Gönderilmemiş coordinate sayısı
                // Not: entMemberCoordinate'da IsSentToServer property'si yok, gönderilenler silindiği için tüm koordinatlar gönderilmemiş sayılır
                var remainingCoordinates = db.Table<MerchPlusMAUI.Models.entMemberCoordinate>().Count();
                
                // Toplam güncelleme sayısı
                int totalUpdates = remainingRoutes + remainingPhotos + remainingDisplayPhotos + remainingCoordinates;
                
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    if (UpdateButtonLabel != null)
                    {
                        UpdateButtonLabel.Text = "Güncelle";
                    }
                    
                    // Badge'i göster/gizle
                    if (UpdateBadge != null && UpdateBadgeLabel != null)
                    {
                        if (totalUpdates > 0)
                        {
                            UpdateBadge.IsVisible = true;
                            UpdateBadgeLabel.Text = totalUpdates > 99 ? "99+" : totalUpdates.ToString();
                            
                            // Badge boyutunu sayıya göre ayarla
                            if (totalUpdates > 9)
                            {
                                UpdateBadge.WidthRequest = 24;
                            }
                            else
                            {
                                UpdateBadge.WidthRequest = 20;
                            }
                        }
                        else
                        {
                            UpdateBadge.IsVisible = false;
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Buton metni güncelleme hatası: {ex.Message}");
            }
        }

        // Ana sayfa butonuna tıklandığında
        private void OnHomeClicked(object sender, EventArgs e)
        {
            HomeClicked?.Invoke(this, EventArgs.Empty);
            
            // Ana sayfaya yönlendirme
            if (MauiShell.Current != null)
            {
                MauiShell.Current.GoToAsync("//AboutMePage");
            }
        }

        // Harita butonuna tıklandığında
        private void OnMapClicked(object sender, EventArgs e)
        {
            MapClicked?.Invoke(this, EventArgs.Empty);
            
            // Harita sayfasına yönlendirme
            if (MauiShell.Current != null)
            {
                MauiShell.Current.GoToAsync("//LocationsPage");
            }
        }

        // Görevler butonuna tıklandığında
        private void OnTasksClicked(object sender, EventArgs e)
        {
            TasksClicked?.Invoke(this, EventArgs.Empty);
            
            // Görevler sayfasına yönlendirme
            if (MauiShell.Current != null)
            {
                MauiShell.Current.GoToAsync("//VisitPage");
            }
        }

        // Veritabanı güncelleme butonuna tıklandığında
        private async void OnUpdateDbClicked(object sender, EventArgs e)
        {
            if (_isBusy)
            {
                return; // İşlem zaten devam ediyor, engelle
            }
            
            UpdateDbClicked?.Invoke(this, EventArgs.Empty);
            
            // Veri gönderme işlemi
            if (MauiShell.Current != null)
            {
                bool answer = await MauiShell.Current.DisplayAlert("Onay", 
                    "Tüm verileri sunucuya göndermek istediğinizden emin misiniz?", 
                    "Evet", "Hayır");

                if (answer)
                {
                    try
                    {
                        // Başlangıç mesajı göster
                        await MainThread.InvokeOnMainThreadAsync(async () =>
                        {
                            await MauiShell.Current.DisplayAlert("Bilgi", 
                                "Veriler sunucuya gönderiliyor. Lütfen bekleyin...", 
                                "Tamam");
                        });

                        // Buton durumunu güncelle (eski projede olduğu gibi)
                        await MainThread.InvokeOnMainThreadAsync(() =>
                        {
                            if (UpdateButtonLabel != null)
                            {
                                UpdateButtonLabel.Text = "Gönderiliyor...";
                            }
                            if (UpdateButtonGrid != null)
                            {
                                UpdateButtonGrid.IsEnabled = false;
                            }
                        });

                        // Senkronizasyon işlemini başlat
                        _isBusy = true;
                        string resultMessage = await SyncData();
                        _isBusy = false;

                        // Buton durumunu geri yükle ve badge'i güncelle
                        await MainThread.InvokeOnMainThreadAsync(async () =>
                        {
                            if (UpdateButtonLabel != null)
                            {
                                UpdateButtonLabel.Text = "Güncelle";
                            }
                            if (UpdateButtonGrid != null)
                            {
                                UpdateButtonGrid.IsEnabled = true;
                            }

                            // Badge'i güncelle (güncellenecek bir şey kalmamışsa badge gizlenecek)
                            UpdateButtonText();

                            // Sonuç mesajını göster - daha kullanıcı dostu
                            string displayMessage = resultMessage;
                            if (resultMessage.Contains("Gönderilecek veri bulunamadı"))
                            {
                                displayMessage = "Güncellenecek veri bulunamadı. Tüm veriler zaten sunucuya gönderilmiş.";
                            }
                            else
                            {
                                displayMessage = "✅ Güncelleme tamamlandı!\n\n" + resultMessage;
                            }
                            
                            await MauiShell.Current.DisplayAlert("Güncelleme Tamamlandı", displayMessage, "Tamam");
                        });
                    }
                    catch (Exception ex)
                    {
                        _isBusy = false;
                        System.Diagnostics.Debug.WriteLine($"Veri senkronizasyon hatası: {ex.Message}");
                        
                        // Buton durumunu geri yükle ve badge'i güncelle
                        await MainThread.InvokeOnMainThreadAsync(async () =>
                        {
                            if (UpdateButtonLabel != null)
                            {
                                UpdateButtonLabel.Text = "Güncelle";
                            }
                            if (UpdateButtonGrid != null)
                            {
                                UpdateButtonGrid.IsEnabled = true;
                            }

                            // Badge'i güncelle
                            UpdateButtonText();

                            await MauiShell.Current.DisplayAlert("Hata", 
                                $"Veriler gönderilirken bir hata oluştu: {ex.Message}", 
                                "Tamam");
                        });
                    }
                }
            }
        }

        // Veritabanı silme butonuna tıklandığında
        private async void OnDeleteDbClicked(object sender, EventArgs e)
        {
            DeleteDbClicked?.Invoke(this, EventArgs.Empty);
            
            // Veritabanı silme onayı isteme
            if (MauiShell.Current != null)
            {
                bool result = await MauiShell.Current.DisplayAlert(
                    "Veritabanı Temizleme", 
                    "Veritabanını temizlemek istediğinize emin misiniz? Bu işlem geri alınamaz.", 
                    "Evet, Temizle", 
                    "İptal");
                
                if (result)
                {
                    try
                    {
                        // Doğrudan veritabanı dosyasını siliyoruz
                        if (File.Exists(_dbPath))
                        {
                            File.Delete(_dbPath);
                            await MauiShell.Current.DisplayAlert("Başarılı", 
                                "Veritabanı başarıyla silindi.", 
                                "Tamam");
                                
                            // Uygulamayı yeniden başlatmak için kapatıyoruz
                            MauiControls.Application.Current.Quit();
                        }
                        else
                        {
                            await MauiShell.Current.DisplayAlert("Bilgi", 
                                "Veritabanı dosyası bulunamadı.", 
                                "Tamam");
                        }
                    }
                    catch (Exception ex)
                    {
                        await MauiShell.Current.DisplayAlert(
                            "Hata", 
                            $"Veritabanı temizlenirken bir hata oluştu: {ex.Message}", 
                            "Tamam");
                    }
                }
            }
        }
        
        // Veri senkronizasyon metodu (eski projede olduğu gibi)
        public async Task<string> SyncData()
        {
            string message = string.Empty;
            using var db = new SQLiteConnection(_dbPath);
            
            try
            {
                // Eski projede sadece MemberRoute gönderiliyor, coordinates ve photos ayrı gönderiliyor
                // Ama yeni projede coordinates ve photos da var, onları da gönderelim
                
                // Sync coordinates (yeni projede ekstra)
                var coordinates = db.Table<MerchPlusMAUI.Models.entMemberCoordinate>().ToList();
                if (coordinates.Any())
                {
                    try
                    {
                        await SyncCoordinates(coordinates);
                        message += $"Koordinatlar gönderildi ({coordinates.Count} adet)\r\n";
                    }
                    catch (Exception ex)
                    {
                        message += $"Koordinatlar gönderilemedi: {ex.Message}\r\n";
                    }
                }

                // Sync routes (eski projede ana işlem)
                var routes = db.Table<entMemberRoute>().Where(r => r.IsSentToServer != true).ToList();
                int totalRoutes = routes.Count;
                int counter = 0;

                if (routes.Any())
                {
                    foreach (var route in routes)
                    {
                        counter++;
                        
                        // Progress göster (eski projede olduğu gibi)
                        await MainThread.InvokeOnMainThreadAsync(() =>
                        {
                            if (UpdateButtonLabel != null)
                            {
                                UpdateButtonLabel.Text = $"Gönderiliyor {counter}/{totalRoutes}";
                            }
                        });

                        try
                        {
                            var result = await SyncSingleRoute(route, db);
                            if (result.Success)
                            {
                                message += $"Gönderildi {route.Id}\r\n";
                                
                                await MainThread.InvokeOnMainThreadAsync(() =>
                                {
                                    if (UpdateButtonLabel != null)
                                    {
                                        UpdateButtonLabel.Text = $"Güncellendi {counter}/{totalRoutes}";
                                    }
                                });
                            }
                            else
                            {
                                message += $"Gönderilemedi {route.Id}\r\n{result.ErrorMessage}\r\n";
                            }
                        }
                        catch (Exception ex)
                        {
                            message += $"Hata oluştu {route.Id}: {ex.Message}\r\n";
                        }
                    }

                    // Buton metnini güncelle (eski projede olduğu gibi)
                    var remainingRoutes = db.Table<entMemberRoute>().Where(r => r.IsSentToServer != true).ToList();
                    await MainThread.InvokeOnMainThreadAsync(() =>
                    {
                        if (UpdateButtonLabel != null)
                        {
                            UpdateButtonLabel.Text = remainingRoutes.Count > 0 
                                ? $"Güncelle ({remainingRoutes.Count} mağaza)" 
                                : "Güncelle";
                        }
                    });
                }

                // Sync photos (yeni projede ekstra) - IsSentToServer=false olanları gönder
                var photos = db.Table<MemberDisplayPhotos>().Where(p => p.IsSentToServer == false).ToList();
                Console.WriteLine($"[CommonFooter] Gönderilmemiş fotoğraf sayısı: {photos.Count}");
                if (photos.Any())
                {
                    try
                    {
                        await SyncPhotos(photos, db);
                        message += $"Fotoğraflar gönderildi ({photos.Count} adet)\r\n";
                    }
                    catch (Exception ex)
                    {
                        message += $"Fotoğraflar gönderilemedi: {ex.Message}\r\n";
                    }
                }

                if (string.IsNullOrEmpty(message))
                {
                    message = "Gönderilecek veri bulunamadı.";
                }
                else
                {
                    // Mesajı daha okunabilir hale getir
                    message = "✅ " + message.TrimEnd('\r', '\n');
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Veri senkronizasyon hatası: {ex.Message}");
                message += $"Hata oluştu: {ex.ToString()}\r\n";
            }

            return message;
        }

        private async Task SyncCoordinates(List<MerchPlusMAUI.Models.entMemberCoordinate> coordinates)
        {
            using var client = new HttpClient { BaseAddress = new Uri(GlobalVariables.APIPath) };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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
                    GlobalVariables.MemberLastCoordinate = new Location
                    {
                        Latitude = lastCoordinate.Latitude.Value,
                        Longitude = lastCoordinate.Longitude.Value
                    };
                    Console.WriteLine($"[CommonFooter] Son koordinat GlobalVariables'a kaydedildi: Lat={lastCoordinate.Latitude}, Lon={lastCoordinate.Longitude}");
                }
                
                using var db = new SQLiteConnection(_dbPath);
                db.DeleteAll<MerchPlusMAUI.Models.entMemberCoordinate>();
            }
        }

        // Tek bir route'u senkronize et (eski projede olduğu gibi)
        private async Task<(bool Success, string ErrorMessage)> SyncSingleRoute(entMemberRoute route, SQLiteConnection db)
        {
            try
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
                
                Console.WriteLine($"[CommonFooter] Route {route.Id} için {photoRows.Count} adet gönderilmemiş raf fotoğrafı bulundu");

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

                // HTTP client oluştur (eski projede olduğu gibi - 10 dakika timeout)
                using var client = new HttpClient { BaseAddress = new Uri(GlobalVariables.APIPath) };
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = new TimeSpan(0, 10, 0); // 10 dakika timeout (eski projede olduğu gibi)

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
                        JObject returnObject = (JObject)JsonConvert.DeserializeObject(responseString);
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
                                    Console.WriteLine($"[CommonFooter] Raf fotoğrafı Id: {photo.Id} için IsSentToServer=true yapıldı");
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
                            
                            return (true, string.Empty);
                        }
                        else
                        {
                            // Sunucu NOK döndü
                            return (false, responseString);
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
                        
                        return (true, string.Empty);
                    }
                }
                else
                {
                    // HTTP hatası
                    return (false, $"HTTP {response.StatusCode}: {responseString}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Route sync hatası: {ex.Message}");
                return (false, ex.ToString());
            }
        }

        private async Task SyncPhotos(List<MemberDisplayPhotos> photos, SQLiteConnection db)
        {
            try
            {
                Console.WriteLine("========================================");
                Console.WriteLine("[CommonFooter - TEŞHİR FOTOĞRAFI SENKRONIZASYON] Başlatılıyor...");
                Console.WriteLine($"Zaman: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                Console.WriteLine($"[CommonFooter] {photos.Count} adet fotoğraf gönderilecek");

                using var client = new HttpClient { BaseAddress = new Uri(GlobalVariables.APIPath2) };
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                foreach (var photo in photos)
                {
                    try
                    {
                        // DisplayPicturePath null/boş kontrolü
                        if (string.IsNullOrEmpty(photo.DisplayPicturePath))
                        {
                            Console.WriteLine($"[CommonFooter] ❌ UYARI: Fotoğraf Id: {photo.Id} için DisplayPicturePath null veya boş, atlanıyor");
                            System.Diagnostics.Debug.WriteLine($"CommonFooter: Display photo Id: {photo.Id} has null or empty DisplayPicturePath, skipping");
                            continue; // Bu fotoğrafı atla, bir sonrakine geç
                        }
                        
                        Console.WriteLine($"[CommonFooter] Fotoğraf gönderiliyor - Id: {photo.Id}, PromotionTypeId: {photo.PromotionTypeId}, CustomerProductId: {photo.CustomerProductId}, MemberRouteId: {photo.MemberRouteId}, DisplayPicturePath uzunluğu: {photo.DisplayPicturePath?.Length ?? 0}");
                        
                        var jsonString = System.Text.Json.JsonSerializer.Serialize(photo);
                        var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                        
                        var response = await client.PostAsync("MemberDisplayPhotosController/InsertMemberDisplayPhotos", content);
                        var responseString = await response.Content.ReadAsStringAsync();
                        
                        Console.WriteLine($"[CommonFooter] HTTP Status: {response.StatusCode}, Response: {responseString}");

                        // Response kontrolü - DatabaseSyncService'deki gibi
                        bool isSuccess = false;
                        if (response.IsSuccessStatusCode)
                        {
                            // Önce hata kontrolü yap (ExceptionMessage, "An error has occurred" vb.)
                            if (responseString.Contains("ExceptionMessage", StringComparison.OrdinalIgnoreCase) ||
                                responseString.Contains("An error has occurred", StringComparison.OrdinalIgnoreCase) ||
                                responseString.Contains("ArgumentNullException", StringComparison.OrdinalIgnoreCase))
                            {
                                Console.WriteLine($"[CommonFooter] ❌ Response'da hata mesajı bulundu - başarısız olarak işaretleniyor");
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
                                        Console.WriteLine($"[CommonFooter] Response Result: {result}");
                                        isSuccess = result != "NOK";
                                    }
                                    else
                                    {
                                        // Result key'i yoksa ve hata mesajı da yoksa, başarılı say (eski API davranışı)
                                        Console.WriteLine($"[CommonFooter] UYARI: Response'da 'Result' key'i bulunamadı, hata mesajı da yok - başarılı sayılıyor");
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
                            // Başarılı - IsSentToServer flag'ini güncelle (Id ile)
                            db.Execute("UPDATE entMemberDisplayPhotos SET IsSentToServer=? WHERE Id=?", true, photo.Id);
                            Console.WriteLine($"[CommonFooter] ✅ Başarılı - Fotoğraf Id: {photo.Id} sunucuya gönderildi ve IsSentToServer=true yapıldı");
                        }
                        else
                        {
                            Console.WriteLine($"[CommonFooter] ❌ Başarısız - Fotoğraf Id: {photo.Id} sunucuya gönderilemedi");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[CommonFooter] ❌ HATA (Fotoğraf Id: {photo.Id}): {ex.Message}");
                        System.Diagnostics.Debug.WriteLine($"CommonFooter: Error syncing display photo - Id: {photo.Id}, Error: {ex.Message}");
                    }
                }
                
                Console.WriteLine($"[CommonFooter - TEŞHİR FOTOĞRAFI SENKRONIZASYON] Tamamlandı - {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                Console.WriteLine("========================================");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CommonFooter] ❌ GENEL HATA: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"CommonFooter: Error occurred while syncing display photos: {ex.Message}");
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
                        Console.WriteLine($"[CommonFooter] {entityName}{idStr}.{prop.Name} null/default, {now:yyyy-MM-dd HH:mm:ss} olarak set edildi");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[CommonFooter] {entityName}.{prop.Name} kontrol edilirken hata: {ex.Message}");
                }
            }
        }
    }
} 