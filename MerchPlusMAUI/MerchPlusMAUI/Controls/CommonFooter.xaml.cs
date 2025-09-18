using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.Net.Http.Headers;
using entMerchPlus;
using MerchPlusMAUI.Models;
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
        }

        // Ana sayfa butonuna tıklandığında
        private void OnHomeClicked(object sender, EventArgs e)
        {
            HomeClicked?.Invoke(this, EventArgs.Empty);
            
            // Ana sayfaya yönlendirme
            if (MauiShell.Current != null)
            {
                MauiShell.Current.GoToAsync("//AboutMe");
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
                        // Senkronizasyon işlemini başlat
                        _isBusy = true;
                        await SyncData();
                        _isBusy = false;
                    }
                    catch (Exception ex)
                    {
                        _isBusy = false;
                        System.Diagnostics.Debug.WriteLine($"Veri senkronizasyon hatası: {ex.Message}");
                        await MauiShell.Current.DisplayAlert("Hata", 
                            $"Veriler gönderilirken bir hata oluştu: {ex.Message}", 
                            "Tamam");
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
        
        // Veri senkronizasyon metodu
        private async Task SyncData()
        {
            using var db = new SQLiteConnection(_dbPath);
            
            try
            {
                // Show loading indicator
                await MainThread.InvokeOnMainThreadAsync(async () => {
                    await MauiShell.Current.DisplayAlert("Bilgi", "Veri senkronizasyonu başlatıldı...", "Tamam");
                });
                
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

                await MainThread.InvokeOnMainThreadAsync(async () => {
                    await MauiShell.Current.DisplayAlert("Başarılı", "Tüm veriler başarıyla gönderildi.", "Tamam");
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Veri senkronizasyon hatası: {ex.Message}");
                await MainThread.InvokeOnMainThreadAsync(async () => {
                    await MauiShell.Current.DisplayAlert("Hata", $"Senkronizasyon sırasında bir hata oluştu: {ex.Message}", "Tamam");
                });
            }
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
                using var db = new SQLiteConnection(_dbPath);
                db.DeleteAll<MerchPlusMAUI.Models.entMemberCoordinate>();
            }
        }

        private async Task SyncRoutes(List<entMemberRoute> routes, SQLiteConnection db)
        {
            using var client = new HttpClient { BaseAddress = new Uri(GlobalVariables.APIPath) };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            foreach (var photo in photos)
            {
                var jsonString = System.Text.Json.JsonSerializer.Serialize(photo);
                var content = new StringContent(jsonString);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                
                var response = await client.PostAsync("MemberDisplayPhotosController/InsertMemberDisplayPhotos", content);

                if (response.IsSuccessStatusCode)
                {
                    db.Execute("UPDATE entMemberDisplayPhotos SET IsSentToServer=? WHERE Id=?", true, photo.Id);
                }
            }
        }
    }
} 