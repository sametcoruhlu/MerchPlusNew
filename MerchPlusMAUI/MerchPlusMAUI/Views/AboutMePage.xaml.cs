using SQLite;
using Microsoft.Extensions.Logging;
using entMerchPlus;
using System.Net.Http.Headers;
using MerchPlusMAUI.Models;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using Microsoft.Maui.Graphics;

namespace MerchPlusMAUI.Views
{
    public partial class AboutMePage : ContentPage
    {
        private readonly ILogger<AboutMePage> _logger;
        private readonly string _dbPath;
        private readonly List<ReportItem> _reports = new List<ReportItem>();

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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            System.Diagnostics.Debug.WriteLine("AboutMePage açıldı!");
            System.Diagnostics.Debug.WriteLine("AboutMePage Content Loaded");
            
            LoadData();
        }

        private async void LoadData()
        {
            // İstatistikleri HomePage'den al
            UpdateStatisticsFromHomePage();
            
            // Rapor verilerini yükleme
            LoadReportData();
        }

        private void UpdateStatisticsFromHomePage()
        {
            try
            {
                // HomePage'den hesaplanan istatistikleri kullan
                PlannedCountLabel.Text = HomePage.PlannedStoresCount.ToString();
                CompletedCountLabel.Text = HomePage.CompletedStoresCount.ToString();
                PendingCountLabel.Text = HomePage.PendingStoresCount.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "İstatistikler yüklenirken hata oluştu");
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
                var photos = db.Table<entMemberDisplayPhotos>().Where(p => !p.IsSentToServer != true).ToList();
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
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            foreach (var route in routes)
            {
                var syncData = new MemberRouteSyncData
                {
                    MemberRoute = route,
                    MemberRouteDetail = db.Table<entMemberRouteDetail>()
                        .Where(d => d.MemberRouteId == route.Id)
                        .ToList(),
                    MemberRoutePhoto = db.Table<entMemberRoutePhoto>()
                        .Where(p => p.MemberRouteId == route.Id)
                        .ToList(),
                    SalesOrder = db.Table<entSalesOrder>()
                        .Where(s => s.MemberRouteId == route.Id)
                        .ToList(),
                    ProductExpiration = db.Table<entProductExpiration>()
                        .Where(p => p.MemberRouteId == route.Id)
                        .ToList()
                };

                if (syncData.SalesOrder.Any())
                {
                    var salesOrderIds = syncData.SalesOrder.Select(s => s.Id).ToList();
                    syncData.SalesOrderDetail = db.Table<entSalesOrderDetail>()
                        .Where(d => d.SalesOrderId.HasValue && salesOrderIds.Contains(d.SalesOrderId.Value))
                        .ToList();
                }

                var endpoint = syncData.SalesOrder.Any()
                    ? "MemberRouteController/CompleteMemberRouteWiselyFromJsonArrayWithSalesOrder"
                    : "MemberRouteController/CompleteMemberRouteWiselyFromJsonArray";

                var jsonString = System.Text.Json.JsonSerializer.Serialize(syncData);
                var content = new StringContent(jsonString);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                
                var response = await client.PostAsync(endpoint, content);

                if (response.IsSuccessStatusCode)
                {
                    db.Execute("UPDATE entMemberRoute SET IsSentToServer=? WHERE Id=?", true, route.Id);
                    db.Execute("UPDATE entMemberRouteDetail SET IsSentToServer=? WHERE MemberRouteId=?", true, route.Id);
                    db.Execute("UPDATE entProductExpiration SET IsSentToServer=? WHERE MemberRouteId=?", true, route.Id);
                }
            }
        }

        private async Task SyncPhotos(List<entMemberDisplayPhotos> photos, SQLiteConnection db)
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
        
        // Rapor verilerini yükleme
        private void LoadReportData()
        {
            try
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    ReportContainer.Children.Clear();

                    // Rapor verilerini oluştur
                    _reports.Clear();
                    _reports.Add(new ReportItem { Name = "Raf Fiyatlar", CompletionPercentage = 100 });
                    _reports.Add(new ReportItem { Name = "Önyüz Adedi", CompletionPercentage = 65 });
                    _reports.Add(new ReportItem { Name = "Var-Yok", CompletionPercentage = 83 });
                    _reports.Add(new ReportItem { Name = "Aktivite", CompletionPercentage = 45 });
                    _reports.Add(new ReportItem { Name = "Raf Stok", CompletionPercentage = 100 });
                    _reports.Add(new ReportItem { Name = "Sipariş", CompletionPercentage = 22 });
                    _reports.Add(new ReportItem { Name = "Son Kullanma Tarihi", CompletionPercentage = 100 });
                    
                    // Rapor öğelerini arayüze ekle
                    foreach (var report in _reports)
                    {
                        Microsoft.Maui.Graphics.Color progressColor = report.CompletionPercentage == 100 
                            ? Microsoft.Maui.Graphics.Color.FromArgb("#4CAF50") // Yeşil
                            : Microsoft.Maui.Graphics.Color.FromArgb("#F44336"); // Kırmızı
                        
                        var grid = new Grid
                        {
                            ColumnDefinitions =
                            {
                                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                                new ColumnDefinition { Width = new GridLength(100, GridUnitType.Absolute) }
                            }
                        };
                        
                        grid.Add(new Label
                        {
                            Text = report.Name,
                            FontSize = 16,
                            VerticalOptions = LayoutOptions.Center
                        }, 0, 0);
                        
                        var progressStack = new VerticalStackLayout
                        {
                            Spacing = 2
                        };
                        
                        var percentLabel = new Label
                        {
                            Text = $"%{report.CompletionPercentage}",
                            FontSize = 14,
                            TextColor = progressColor,
                            FontAttributes = FontAttributes.Bold,
                            HorizontalOptions = LayoutOptions.End
                        };
                        
                        var progressBar = new ProgressBar
                        {
                            Progress = report.CompletionPercentage / 100.0,
                            ProgressColor = progressColor,
                            HeightRequest = 8
                        };
                        
                        progressStack.Add(percentLabel);
                        progressStack.Add(progressBar);
                        
                        grid.Add(progressStack, 1, 0);
                        
                        ReportContainer.Children.Add(grid);
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Görevler yüklenirken hata oluştu");
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