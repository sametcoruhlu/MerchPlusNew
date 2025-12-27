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

        // Static metod: Tüm sayfalardan CommonFooter badge'ini güncellemek için
        public static void RefreshBadge()
        {
            try
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    // Tüm navigation stack'i kontrol et - AboutMePage herhangi bir yerde olabilir
                    if (MauiShell.Current?.Navigation != null)
                    {
                        // Navigation stack'ten AboutMePage'i bul
                        var navigationStack = MauiShell.Current.Navigation.NavigationStack;
                        foreach (var page in navigationStack)
                        {
                            if (page is MerchPlusMAUI.Views.AboutMePage aboutMePage)
                            {
                                // CommonFooter'ı bul
                                var footer = aboutMePage.FindByName<CommonFooter>("CommonFooter");
                                if (footer != null)
                                {
                                    footer.UpdateButtonText();
                                    System.Diagnostics.Debug.WriteLine("[CommonFooter] Badge güncellendi (RefreshBadge çağrıldı)");
                                    return; // Bulundu ve güncellendi, çık
                                }
                            }
                        }
                    }
                    
                    // Eğer navigation stack'te bulunamadıysa, mevcut sayfayı kontrol et
                    var currentPage = MauiShell.Current?.CurrentPage;
                    if (currentPage is MerchPlusMAUI.Views.AboutMePage aboutMePageCurrent)
                    {
                        var footer = aboutMePageCurrent.FindByName<CommonFooter>("CommonFooter");
                        if (footer != null)
                        {
                            footer.UpdateButtonText();
                            System.Diagnostics.Debug.WriteLine("[CommonFooter] Badge güncellendi (RefreshBadge - current page)");
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[CommonFooter] RefreshBadge hatası: {ex.Message}");
            }
        }

        // Güncellenecek veri detaylarını hesapla (onay ekranında gösterilmek için)
        private (int Routes, int Photos, int DisplayPhotos, int Coordinates, int RouteDetails, int SalesOrders, int SalesOrderDetails, int ProductExpirations, int Total) GetPendingUpdatesDetails()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"[CommonFooter] GetPendingUpdatesDetails() çağrıldı - {DateTime.Now:HH:mm:ss.fff}");
                System.Diagnostics.Debug.WriteLine($"[CommonFooter] GetPendingUpdatesDetails() - Thread: {System.Threading.Thread.CurrentThread.ManagedThreadId}, IsMainThread: {MainThread.IsMainThread}");
                System.Diagnostics.Debug.WriteLine($"[CommonFooter] GetPendingUpdatesDetails() - Database path: {_dbPath}");
                
                using var db = new SQLiteConnection(_dbPath);
                System.Diagnostics.Debug.WriteLine($"[CommonFooter] GetPendingUpdatesDetails() - SQLiteConnection açıldı");
                
                // Gönderilmemiş route sayısı - SyncData ile aynı mantık (IsSentToServer != true)
                // Sadece tamamlanmış route'ları say (ExitOn IS NOT NULL) çünkü sync sadece tamamlanmış route'ları gönderir
                var remainingRoutes = db.Table<entMemberRoute>()
                    .Where(r => r.ExitOn != null && (r.IsSentToServer != true || r.IsSentToServer == false))
                    .Count();
                System.Diagnostics.Debug.WriteLine($"[CommonFooter] remainingRoutes (ExitOn IS NOT NULL AND IsSentToServer=false): {remainingRoutes}");
                
                // Gönderilmemiş route photo sayısı - Tüm gönderilmemiş fotoğrafları say
                // (Route tamamlanmadıysa bile, kullanıcı veri eklediğinde görmeli)
                var remainingPhotos = db.ExecuteScalar<int>(
                    "SELECT COUNT(*) FROM entMemberRoutePhoto WHERE IsSentToServer = 0");
                System.Diagnostics.Debug.WriteLine($"[CommonFooter] remainingPhotos (All unsent): {remainingPhotos}");
                
                // Gönderilmemiş display photo sayısı - Tüm gönderilmemiş display fotoğraflarını say
                // (Route tamamlanmadıysa bile, kullanıcı veri eklediğinde görmeli)
                var remainingDisplayPhotos = db.Table<MemberDisplayPhotos>()
                    .Where(p => p.IsSentToServer == false)
                    .Count();
                System.Diagnostics.Debug.WriteLine($"[CommonFooter] remainingDisplayPhotos (All unsent): {remainingDisplayPhotos}");
                
                // Gönderilmemiş coordinate sayısı
                // Not: entMemberCoordinate'da IsSentToServer property'si yok, gönderilenler silindiği için tüm koordinatlar gönderilmemiş sayılır
                // SQLite LINQ nullable double property'leri desteklemediği için SQL sorgusu kullanıyoruz
                var remainingCoordinates = db.ExecuteScalar<int>(
                    "SELECT COUNT(*) FROM entMemberCoordinate WHERE Latitude IS NOT NULL AND Longitude IS NOT NULL");
                System.Diagnostics.Debug.WriteLine($"[CommonFooter] remainingCoordinates: {remainingCoordinates}");
                
                // Gönderilmemiş route detail sayısı (ürün veri girişi) - Sadece tamamlanmış route'lara ait verileri say
                // Route detail'ler route ile birlikte sync edilir, bu yüzden sadece tamamlanmış route'lara ait olanları saymalıyız
                var completedRouteIds = db.Table<entMemberRoute>()
                    .Where(r => r.ExitOn != null)
                    .Select(r => r.Id)
                    .ToList();
                var remainingRouteDetails = db.Table<entMerchPlus.entMemberRouteDetail>()
                    .Where(d => d.IsSentToServer == false)
                    .ToList()
                    .Where(d => d.MemberRouteId != null && completedRouteIds.Contains((int)d.MemberRouteId))
                    .Count();
                System.Diagnostics.Debug.WriteLine($"[CommonFooter] remainingRouteDetails (Completed routes only): {remainingRouteDetails}");
                
                // Gönderilmemiş sipariş sayısı - Sadece tamamlanmış route'lara ait siparişleri say
                // Siparişler route ile birlikte sync edilir
                var remainingSalesOrders = db.Table<SalesOrder>()
                    .Where(s => s.IsSentToServer == false && completedRouteIds.Contains(s.MemberRouteId))
                    .Count();
                System.Diagnostics.Debug.WriteLine($"[CommonFooter] remainingSalesOrders (Completed routes only): {remainingSalesOrders}");
                
                // Gönderilmemiş sipariş detay sayısı - Sadece tamamlanmış route'lara ait sipariş detaylarını say
                // Sipariş detayları route ile birlikte sync edilir
                var salesOrderIds = db.Table<SalesOrder>()
                    .Where(s => completedRouteIds.Contains(s.MemberRouteId))
                    .Select(s => s.Id)
                    .ToList();
                var remainingSalesOrderDetails = db.Table<SalesOrderDetail>()
                    .Where(sd => sd.IsSentToServer == false && salesOrderIds.Contains(sd.SalesOrderId))
                    .Count();
                System.Diagnostics.Debug.WriteLine($"[CommonFooter] remainingSalesOrderDetails (Completed routes only): {remainingSalesOrderDetails}");
                
                // Gönderilmemiş SKT (Son Kullanma Tarihi) sayısı - Sadece tamamlanmış route'lara ait SKT'leri say
                // SKT'ler route ile birlikte sync edilir
                var remainingProductExpirations = db.Table<ProductExpiration>()
                    .Where(pe => pe.IsSentToServer == false && completedRouteIds.Contains(pe.MemberRouteId))
                    .Count();
                System.Diagnostics.Debug.WriteLine($"[CommonFooter] remainingProductExpirations (Completed routes only): {remainingProductExpirations}");
                
                // Toplam güncelleme sayısı
                int totalUpdates = remainingRoutes + remainingPhotos + remainingDisplayPhotos + remainingCoordinates + 
                                   remainingRouteDetails + remainingSalesOrders + remainingSalesOrderDetails + remainingProductExpirations;
                System.Diagnostics.Debug.WriteLine($"[CommonFooter] totalUpdates: {totalUpdates}");
                
                return (remainingRoutes, remainingPhotos, remainingDisplayPhotos, remainingCoordinates, 
                        remainingRouteDetails, remainingSalesOrders, remainingSalesOrderDetails, remainingProductExpirations, totalUpdates);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Güncellenecek veri detayları hesaplama hatası: {ex.Message}");
                return (0, 0, 0, 0, 0, 0, 0, 0, 0);
            }
        }

        // Güncellenecek veri sayısını hesapla (UpdateButtonText ve OnUpdateDbClicked için ortak metod)
        private int GetPendingUpdatesCount()
        {
            System.Diagnostics.Debug.WriteLine($"[CommonFooter] GetPendingUpdatesCount() çağrıldı - {DateTime.Now:HH:mm:ss.fff}");
            var details = GetPendingUpdatesDetails();
            System.Diagnostics.Debug.WriteLine($"[CommonFooter] GetPendingUpdatesCount() - GetPendingUpdatesDetails() sonucu: Total={details.Total}");
            return details.Total;
        }

        // Buton metnini güncelle ve badge göster (eski projede olduğu gibi - badge ile sayı göster)
        public void UpdateButtonText()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"[CommonFooter] UpdateButtonText() çağrıldı - {DateTime.Now:HH:mm:ss.fff}");
                System.Diagnostics.Debug.WriteLine($"[CommonFooter] UpdateButtonText() - Thread: {System.Threading.Thread.CurrentThread.ManagedThreadId}, IsMainThread: {MainThread.IsMainThread}");
                
                int totalUpdates = GetPendingUpdatesCount();
                System.Diagnostics.Debug.WriteLine($"[CommonFooter] UpdateButtonText() - GetPendingUpdatesCount() sonucu: {totalUpdates}");
                System.Diagnostics.Debug.WriteLine($"[CommonFooter] UpdateButtonText() - MainThread.BeginInvokeOnMainThread() çağrılıyor, totalUpdates değeri: {totalUpdates}");
                
                // totalUpdates değerini closure içinde kullanmak için yerel değişkene kopyala
                int capturedTotalUpdates = totalUpdates;
                
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    System.Diagnostics.Debug.WriteLine($"[CommonFooter] UpdateButtonText() - MainThread callback çalışıyor - {DateTime.Now:HH:mm:ss.fff}");
                    System.Diagnostics.Debug.WriteLine($"[CommonFooter] UpdateButtonText() - MainThread callback içinde totalUpdates değeri: {capturedTotalUpdates}");
                    
                    if (UpdateButtonLabel != null)
                    {
                        UpdateButtonLabel.Text = "Gönder";
                    }
                    
                    // Badge'i göster/gizle
                    if (UpdateBadge != null && UpdateBadgeLabel != null)
                    {
                        if (capturedTotalUpdates > 0)
                        {
                            UpdateBadge.IsVisible = true;
                            UpdateBadgeLabel.Text = capturedTotalUpdates > 99 ? "99+" : capturedTotalUpdates.ToString();
                            System.Diagnostics.Debug.WriteLine($"[CommonFooter] UpdateButtonText() - Badge GÖSTERİLDİ: {capturedTotalUpdates} (IsVisible={UpdateBadge.IsVisible}, Text={UpdateBadgeLabel.Text})");
                            
                            // Badge boyutunu sayıya göre ayarla
                            if (capturedTotalUpdates > 9)
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
                            System.Diagnostics.Debug.WriteLine($"[CommonFooter] UpdateButtonText() - Badge GİZLENDİ (totalUpdates=0, IsVisible={UpdateBadge.IsVisible})");
                        }
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"[CommonFooter] UpdateButtonText() - UpdateBadge veya UpdateBadgeLabel null! (UpdateBadge={UpdateBadge != null}, UpdateBadgeLabel={UpdateBadgeLabel != null})");
                    }
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[CommonFooter] Buton metni güncelleme hatası: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"[CommonFooter] Stack trace: {ex.StackTrace}");
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
            System.Diagnostics.Debug.WriteLine($"[CommonFooter] OnUpdateDbClicked() çağrıldı - {DateTime.Now:HH:mm:ss.fff}");
            
            if (_isBusy)
            {
                System.Diagnostics.Debug.WriteLine($"[CommonFooter] OnUpdateDbClicked() - _isBusy=true, işlem zaten devam ediyor");
                return; // İşlem zaten devam ediyor, engelle
            }
            
            UpdateDbClicked?.Invoke(this, EventArgs.Empty);
            
            // Önce badge'i güncelle (background sync sonrası değişmiş olabilir)
            System.Diagnostics.Debug.WriteLine($"[CommonFooter] OnUpdateDbClicked() - Badge güncellemesi yapılıyor (1. çağrı)...");
            UpdateButtonText();
            
            // Sonra güncellenecek veri kontrolü yap
            System.Diagnostics.Debug.WriteLine($"[CommonFooter] OnUpdateDbClicked() - GetPendingUpdatesDetails() çağrılıyor...");
            var pendingDetails = GetPendingUpdatesDetails();
            System.Diagnostics.Debug.WriteLine($"[CommonFooter] OnUpdateDbClicked() - GetPendingUpdatesDetails() sonucu: Total={pendingDetails.Total}, Routes={pendingDetails.Routes}, Photos={pendingDetails.Photos}, DisplayPhotos={pendingDetails.DisplayPhotos}, Coordinates={pendingDetails.Coordinates}, RouteDetails={pendingDetails.RouteDetails}, SalesOrders={pendingDetails.SalesOrders}, SalesOrderDetails={pendingDetails.SalesOrderDetails}, ProductExpirations={pendingDetails.ProductExpirations}");
            
            if (pendingDetails.Total == 0)
            {
                System.Diagnostics.Debug.WriteLine($"[CommonFooter] OnUpdateDbClicked() - Total=0, güncellenecek veri yok! Badge gizleniyor...");
                // Güncellenecek bir şey yok, badge'i gizle ve uyarı göster
                UpdateButtonText(); // Badge'i gizlemek için tekrar güncelle
                if (MauiShell.Current != null)
                {
                    await MauiShell.Current.DisplayAlert("Bilgi", 
                        "Güncellenecek veri bulunmuyor. Tüm veriler zaten sunucuya gönderilmiş.", 
                        "Tamam");
                }
                return;
            }
            
            System.Diagnostics.Debug.WriteLine($"[CommonFooter] OnUpdateDbClicked() - Total={pendingDetails.Total}, güncelleme işlemi devam ediyor...");
            
            // Veri gönderme işlemi - detaylı bilgi göster
            if (MauiShell.Current != null)
            {
                // Güncellenecek veri türlerini ve sayılarını göster
                var detailsMessage = "Güncellenecek veriler:\n\n";
                if (pendingDetails.Routes > 0)
                    detailsMessage += $"• Mağaza Ziyaretleri: {pendingDetails.Routes} adet\n";
                if (pendingDetails.Photos > 0)
                    detailsMessage += $"• Raf Fotoğrafları: {pendingDetails.Photos} adet\n";
                if (pendingDetails.DisplayPhotos > 0)
                    detailsMessage += $"• Teşhir Fotoğrafları: {pendingDetails.DisplayPhotos} adet\n";
                if (pendingDetails.Coordinates > 0)
                    detailsMessage += $"• Koordinatlar: {pendingDetails.Coordinates} adet\n";
                if (pendingDetails.RouteDetails > 0)
                    detailsMessage += $"• Ürün Veri Girişleri: {pendingDetails.RouteDetails} adet\n";
                if (pendingDetails.SalesOrders > 0)
                    detailsMessage += $"• Siparişler: {pendingDetails.SalesOrders} adet\n";
                if (pendingDetails.SalesOrderDetails > 0)
                    detailsMessage += $"• Sipariş Detayları: {pendingDetails.SalesOrderDetails} adet\n";
                if (pendingDetails.ProductExpirations > 0)
                    detailsMessage += $"• Son Kullanma Tarihleri: {pendingDetails.ProductExpirations} adet\n";
                
                detailsMessage += $"\nToplam: {pendingDetails.Total} adet veri güncellenecek.";
                
                bool answer = await MauiShell.Current.DisplayAlert("Güncelleme Onayı", 
                    detailsMessage + "\n\nTüm verileri sunucuya göndermek istediğinizden emin misiniz?", 
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
                                UpdateButtonLabel.Text = "Gönder";
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
                                UpdateButtonLabel.Text = "Gönder";
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
                        
                        // Badge'i güncelle (koordinatlar gönderildikten sonra)
                        await MainThread.InvokeOnMainThreadAsync(() =>
                        {
                            UpdateButtonText();
                        });
                    }
                    catch (Exception ex)
                    {
                        message += $"Koordinatlar gönderilemedi: {ex.Message}\r\n";
                    }
                }

                // Sync routes (eski projede ana işlem)
                // Sadece tamamlanmış route'ları gönder (ExitOn IS NOT NULL) - sunucu sadece bunları kabul ediyor
                var routes = db.Table<entMemberRoute>()
                    .Where(r => r.ExitOn != null && r.IsSentToServer != true)
                    .ToList();
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
                                    
                                    // Badge'i güncelle (her route gönderildikten sonra)
                                    UpdateButtonText();
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
                                ? $"Gönder ({remainingRoutes.Count} mağaza)" 
                                : "Gönder";
                        }
                        
                        // Badge'i güncelle (route'lar bittikten sonra)
                        UpdateButtonText();
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
                        
                        // Badge'i güncelle (fotoğraflar gönderildikten sonra)
                        await MainThread.InvokeOnMainThreadAsync(() =>
                        {
                            UpdateButtonText();
                        });
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