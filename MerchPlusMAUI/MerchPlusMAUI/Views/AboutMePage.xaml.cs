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
            System.Diagnostics.Debug.WriteLine("AboutMePage açıldı!");
            System.Diagnostics.Debug.WriteLine("AboutMePage Content Loaded");
            
            // Her sayfa açılışında verileri yeniden yükle
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("AboutMePage: Veri yükleme başlatılıyor...");
                
                // İstatistikleri hesapla
                CalculateAndUpdateStatistics();
                
                
                // Mağaza listesini yükleme (en son güncel verilerle)
                await LoadStoreList();
                
                System.Diagnostics.Debug.WriteLine("AboutMePage: Veri yükleme tamamlandı.");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AboutMePage: Veri yükleme hatası: {ex.Message}");
                _logger?.LogError(ex, "AboutMePage veri yükleme hatası");
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
                
                // Bekleyen mağaza sayısı (EntryOn is not null but ExitOn is null) - currently in progress
                var pendingInProgressCount = db.ExecuteScalar<int>("SELECT COUNT(*) FROM entMemberRoute WHERE EntryOn IS NOT NULL AND ExitOn IS NULL");
                
                // Henüz başlanmamış mağaza sayısı (EntryOn is null and ExitOn is null) - not started yet
                var notStartedCount = db.ExecuteScalar<int>("SELECT COUNT(*) FROM entMemberRoute WHERE EntryOn IS NULL AND ExitOn IS NULL");
                
                // Bekleyen = devam eden + henüz başlanmamış
                var pendingCount = pendingInProgressCount + notStartedCount;
                
                System.Diagnostics.Debug.WriteLine($"AboutMePage: İstatistikler - Planlanan: {plannedCount}, Tamamlanan: {completedCount}, Bekleyen: {pendingCount} (Devam Eden: {pendingInProgressCount}, Başlanmamış: {notStartedCount})");
                
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

                var photoRows = db.Query<entMerchPlus.entMemberRoutePhoto>(
                    "SELECT * FROM entMemberRoutePhoto WHERE MemberRouteId=?", 
                    route.Id).ToList();

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
                    }
                }
            }
        }

        private async Task SyncPhotos(List<MemberDisplayPhotos> photos, SQLiteConnection db)
        {
            using var client = new HttpClient { BaseAddress = new Uri(GlobalVariables.APIPath2) };
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            foreach (var photo in photos)
            {
                var jsonString = System.Text.Json.JsonSerializer.Serialize(photo);
                var content = new StringContent(jsonString);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                
                var response = await client.PostAsync("MemberDisplayPhotosController/InsertMemberDisplayPhotos", content);

                if (response.IsSuccessStatusCode)
                {
                    db.Execute("UPDATE entMemberDisplayPhotos SET IsSentToServer=? WHERE CustomerProductId=?", 
                        true, photo.CustomerProductId);
                }
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
        

        private async Task LoadStoreList()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("AboutMePage: Mağaza listesi yükleniyor...");
                using var db = new SQLiteConnection(_dbPath);
                
                // Tüm mağaza rotalarını al
                var memberRoutes = db.Table<entMemberRoute>().ToList();
                System.Diagnostics.Debug.WriteLine($"AboutMePage: {memberRoutes.Count} mağaza rotası bulundu");
                
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    if (memberRoutes.Any())
                    {
                        foreach (var route in memberRoutes)
                        {
                            // Mağaza adını al
                            string storeName = "Bilinmeyen Mağaza";
                            try
                            {
                                if (route.RetailShopId.HasValue)
                                {
                                    var retailShop = db.Table<entRetailShop>()
                                        .FirstOrDefault(rs => rs.Id == route.RetailShopId.Value);
                                    
                                    if (retailShop != null)
                                    {
                                        // entRetailShop'da Name property'si var mı kontrol et
                                        var nameProperty = typeof(entRetailShop).GetProperty("Name");
                                        if (nameProperty != null)
                                        {
                                            var nameValue = nameProperty.GetValue(retailShop);
                                            storeName = nameValue?.ToString() ?? "Bilinmeyen Mağaza";
                                        }
                                        else
                                        {
                                            // Name property'si yoksa CustomerId kullan
                                            storeName = route.CustomerId?.ToString() ?? "Bilinmeyen Mağaza";
                                        }
                                    }
                                    else
                                    {
                                        storeName = route.CustomerId?.ToString() ?? "Bilinmeyen Mağaza";
                                    }
                                }
                                else
                                {
                                    storeName = route.CustomerId?.ToString() ?? "Bilinmeyen Mağaza";
                                }
                            }
                            catch (Exception ex)
                            {
                                _logger?.LogError(ex, "Mağaza adı alınırken hata oluştu");
                                storeName = route.CustomerId?.ToString() ?? "Bilinmeyen Mağaza";
                            }
                            
                            // Mağaza için tamamlanma yüzdesini hesapla
                            var completionPercentage = CalculateStoreCompletionPercentage(route.Id, db);
                            
                            // Mağaza kartını oluştur
                            var storeCard = CreateStoreCard(route, storeName, completionPercentage);
                            ReportContainer.Children.Add(storeCard);
                        }
                    }
                    else
                    {
                        // Mağaza yoksa bilgi mesajı göster
                        var noStoresLabel = new Label
                        {
                            Text = "Henüz mağaza rotası bulunmamaktadır.",
                            FontSize = 16,
                            TextColor = Colors.Gray,
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.Center
                        };
                        ReportContainer.Children.Add(noStoresLabel);
                    }
                });
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Mağaza listesi yüklenirken hata oluştu");
            }
        }

        private int CalculateStoreCompletionPercentage(int memberRouteId, SQLiteConnection db)
        {
            try
            {
                // Bu mağaza için ürün detaylarını al
                var routeDetails = db.Table<entMemberRouteDetail>()
                    .Where(d => d.MemberRouteId == memberRouteId)
                    .ToList();

                System.Diagnostics.Debug.WriteLine($"AboutMePage: Mağaza {memberRouteId} için {routeDetails.Count} ürün detayı bulundu");

                if (!routeDetails.Any())
                {
                    System.Diagnostics.Debug.WriteLine($"AboutMePage: Mağaza {memberRouteId} için ürün detayı bulunamadı");
                    return 0;
                }

                int completedFields = 0;
                int totalFields = 0;

                foreach (var detail in routeDetails)
                {
                    // Her ürün için tamamlanan alanları say
                    if (detail.FaceCount.HasValue && detail.FaceCount.Value > 0)
                        completedFields++;
                    totalFields++;

                    if (detail.ShelfPrice.HasValue && detail.ShelfPrice.Value > 0)
                        completedFields++;
                    totalFields++;

                    if (detail.ShelfStock.HasValue && detail.ShelfStock.Value >= 0)
                        completedFields++;
                    totalFields++;

                    if (!string.IsNullOrEmpty(detail.Notes))
                        completedFields++;
                    totalFields++;

                    if (detail.PromotionTypeId.HasValue)
                        completedFields++;
                    totalFields++;
                }

                int percentage = totalFields > 0 ? (int)((double)completedFields / totalFields * 100) : 0;
                System.Diagnostics.Debug.WriteLine($"AboutMePage: Mağaza {memberRouteId} tamamlanma yüzdesi: {percentage}% ({completedFields}/{totalFields})");
                
                return percentage;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AboutMePage: Mağaza {memberRouteId} tamamlanma yüzdesi hesaplama hatası: {ex.Message}");
                _logger?.LogError(ex, $"Mağaza {memberRouteId} için tamamlanma yüzdesi hesaplanırken hata oluştu");
                return 0;
            }
        }

        private Frame CreateStoreCard(entMemberRoute route, string storeName, int completionPercentage)
        {
            var card = new Frame
            {
                BackgroundColor = Colors.White,
                CornerRadius = 8,
                Padding = 10,
                Margin = new Thickness(0, 3),
                HasShadow = true
            };

            var grid = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(60, GridUnitType.Absolute) },
                    new ColumnDefinition { Width = new GridLength(50, GridUnitType.Absolute) }
                },
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto }
                }
            };

            // Mağaza adı
            var storeNameLabel = new Label
            {
                Text = storeName,
                FontSize = 14,
                FontAttributes = FontAttributes.Bold,
                TextColor = Colors.Black,
                LineBreakMode = LineBreakMode.TailTruncation
            };
            grid.Add(storeNameLabel, 0, 0);

            // Tamamlanma yüzdesi
            var percentageColor = completionPercentage >= 80 ? Colors.Green : 
                                 completionPercentage >= 50 ? Colors.Orange : Colors.Red;

            var percentageLabel = new Label
            {
                Text = $"%{completionPercentage}",
                FontSize = 14,
                FontAttributes = FontAttributes.Bold,
                TextColor = percentageColor,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
            grid.Add(percentageLabel, 1, 0);

            // Detay butonu
            var detailButton = new Button
            {
                Text = "Detay",
                FontSize = 10,
                BackgroundColor = Colors.Blue,
                TextColor = Colors.White,
                CornerRadius = 4,
                Padding = new Thickness(6, 3),
                HeightRequest = 28
            };
            
            detailButton.Clicked += async (s, e) => await OnStoreDetailClicked(route, storeName);
            grid.Add(detailButton, 2, 0);

            // Progress bar
            var progressBar = new ProgressBar
            {
                Progress = completionPercentage / 100.0,
                ProgressColor = percentageColor,
                HeightRequest = 6,
                Margin = new Thickness(0, 3, 0, 0)
            };
            grid.Add(progressBar, 0, 1);
            Grid.SetColumnSpan(progressBar, 3);

            card.Content = grid;
            return card;
        }

        private async Task OnStoreDetailClicked(entMemberRoute route, string storeName)
        {
            try
            {
                // StoreReportPage'e git
                var navigationParameters = new Dictionary<string, object>
                {
                    ["entMemberRouteId"] = route.Id.ToString(),
                    ["StoreName"] = storeName
                };

                await Shell.Current.GoToAsync("///StoreReportPage", navigationParameters);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Store detail sayfasına yönlendirilirken hata oluştu");
                await DisplayAlert("Hata", "Mağaza detay sayfasına yönlendirilirken hata oluştu.", "Tamam");
            }
        }

        // Planlanan tab'ına tıklandığında
        private async void OnPlannedTapped(object sender, EventArgs e)
        {
            try
            {
                // Tüm mağazaları göster (planlanan yerine "All" parametresi gönder)
                await Shell.Current.GoToAsync($"///StoresPage?StoreType=All");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Tüm mağazalar sayfasına yönlendirme hatası");
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
                // Tamamlanan mağazaları göster (yeşil renklendirme ile)
                await Shell.Current.GoToAsync($"///StoresPage?StoreType=Completed");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Tamamlanan mağazalar sayfasına yönlendirme hatası");
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    await DisplayAlert("Hata", "Mağaza listesi yüklenirken bir hata oluştu.", "Tamam");
                });
            }
        }
        
        // Bekleyen tab'ına tıklandığında
        private async void OnPendingTapped(object sender, EventArgs e)
        {
            try
            {
                // Giriş yapılan ama çıkış yapılmayan mağazaları göster
                await Shell.Current.GoToAsync($"///StoresPage?StoreType=InProgress");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Devam eden mağazalar sayfasına yönlendirme hatası");
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