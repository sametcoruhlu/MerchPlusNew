using SQLite;
using Microsoft.Extensions.Logging;
using entMerchPlus;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Graphics;
using System.Collections.Generic;

namespace MerchPlusMAUI.Views
{
    [QueryProperty(nameof(StoreType), "StoreType")]
    public partial class StoresPage : ContentPage
    {
        private readonly ILogger<StoresPage> _logger;
        private readonly string _dbPath;
        private bool _isLoading = false;

        private string _storeType;
        public string StoreType
        {
            get => _storeType;
            set
            {
                System.Diagnostics.Debug.WriteLine($"StoresPage: StoreType property set to: {value}");
                _storeType = value;
                UpdatePageTitle();
                
                // Sadece loading durumunda değilse yükle
                if (!_isLoading)
                {
                    LoadStoresAsync();
                }
            }
        }

        // ViewModel benzeri özellikler
        public string PageTitle { get; private set; }
        public ObservableCollection<StoreViewModel> Stores { get; private set; } = new ObservableCollection<StoreViewModel>();
        public int ItemCount => Stores?.Count ?? 0;
        public bool IsRefreshing { get; set; }
        public ICommand RefreshCommand { get; private set; }

        public StoresPage(ILogger<StoresPage> logger)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("StoresPage: Constructor called");
                InitializeComponent();
                _logger = logger;
                _dbPath = GlobalVariables.DatabasePath;

                RefreshCommand = new Command(async () => 
                {
                    if (!_isLoading)
                    {
                        await LoadStoresAsync();
                    }
                });
                BindingContext = this;
                System.Diagnostics.Debug.WriteLine("StoresPage: Constructor completed");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"StoresPage initialize hatası: {ex.Message}");
                _logger?.LogError(ex, "StoresPage başlatma hatası");
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            System.Diagnostics.Debug.WriteLine($"StoresPage: OnAppearing called, StoreType = {StoreType}");
            UpdatePageTitle();
            
            // Floating back button'u göster
            FloatingBack?.Show();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            
            // Floating back button'u gizle
            FloatingBack?.Hide();
        }

        private void UpdatePageTitle()
        {
            PageTitle = StoreType switch
            {
                "Planned" => "Planlanan Mağazalar",
                "Completed" => "Tamamlanan Mağazalar",
                "Pending" => "Bekleyen Mağazalar",
                "InProgress" => "Devam Eden Mağazalar",
                "All" => "Tüm Mağazalar",
                _ => "Tüm Mağazalar"
            };

            System.Diagnostics.Debug.WriteLine($"StoresPage: Page title updated to: {PageTitle}");
            OnPropertyChanged(nameof(PageTitle));
        }

        private async Task LoadStoresAsync()
        {
            if (_isLoading)
            {
                System.Diagnostics.Debug.WriteLine("StoresPage: LoadStoresAsync already in progress, skipping");
                return;
            }

            try
            {
                _isLoading = true;
                System.Diagnostics.Debug.WriteLine($"StoresPage: Starting LoadStoresAsync with StoreType: {StoreType}");
                IsRefreshing = true;
                OnPropertyChanged(nameof(IsRefreshing));

                Stores.Clear();

                await Task.Run(() =>
                {
                    using var db = new SQLiteConnection(_dbPath);
                    System.Diagnostics.Debug.WriteLine($"StoresPage: Database connection opened");

                    var routes = StoreType switch
                    {
                        "Completed" => db.Table<entMemberRoute>()
                            .Where(r => r.ExitOn != null)
                            .ToList(),

                        "Pending" => db.Table<entMemberRoute>()
                            .Where(r => r.EntryOn != null && r.ExitOn == null)
                            .ToList(),

                        "InProgress" => db.Table<entMemberRoute>()
                            .Where(r => r.EntryOn != null && r.ExitOn == null)
                            .ToList(),

                        "Planned" => db.Table<entMemberRoute>()
                            .Where(r => r.EntryOn == null && r.ExitOn == null)
                            .ToList(),

                        "All" => db.Table<entMemberRoute>().ToList(),

                        _ => db.Table<entMemberRoute>().ToList()
                    };

                    System.Diagnostics.Debug.WriteLine($"StoresPage: Found {routes.Count} routes for {StoreType}");

                    // Mağazaları al
                    var stores = new List<StoreViewModel>();

                    foreach (var route in routes)
                    {
                        // Mağaza detaylarını al
                        var store = db.Table<entRetailShop>()
                            .FirstOrDefault(s => s.Id == route.RetailShopId);

                        if (store != null)
                        {
                            // Phone özelliği için reflection ile kontrol yapılıyor
                            string phoneNumber = "Telefon bilgisi yok";
                            try
                            {
                                // entRetailShop sınıfında Phone özelliği var mı kontrol et
                                var phoneProperty = typeof(entRetailShop).GetProperty("Phone");
                                if (phoneProperty != null)
                                {
                                    // Phone özelliği varsa değerini al
                                    var value = phoneProperty.GetValue(store);
                                    phoneNumber = value?.ToString() ?? "Telefon bilgisi yok";
                                }
                            }
                            catch (Exception ex)
                            {
                                _logger?.LogError(ex, "Phone özelliği okunurken hata oluştu");
                            }

                            var storeViewModel = new StoreViewModel
                            {
                                Id = store.Id,
                                StoreId = store.Id,
                                StoreName = store.Name,
                                StoreAddress = store.Address ?? "Adres bilgisi yok",
                                Phone = phoneNumber,
                                RouteId = route.Id,
                                MemberRouteId = route.Id,
                                StoreType = GetStoreStatusText(route),
                                StatusColor = GetStatusColor(route),
                                LastVisit = route.ExitOn?.ToString("dd.MM.yyyy HH:mm") ?? "",
                                HasLastVisit = route.ExitOn != null
                            };

                            stores.Add(storeViewModel);
                        }
                    }

                    // UI güncellemesi ana thread'de yapılmalı
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        foreach (var store in stores)
                        {
                            Stores.Add(store);
                        }

                        OnPropertyChanged(nameof(Stores));
                        OnPropertyChanged(nameof(ItemCount));
                    });
                });
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Mağazalar yüklenirken hata oluştu");
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    await DisplayAlert("Hata", "Mağaza listesi yüklenirken bir hata oluştu.", "Tamam");
                });
            }
            finally
            {
                _isLoading = false;
                IsRefreshing = false;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        private string GetStoreStatusText(entMemberRoute route)
        {
            // ExitOn varsa tamamlandı
            if (route.ExitOn != null)
                return "✅ Tamamlandı";
            // EntryOn varsa ama ExitOn yoksa devam ediyor
            else if (route.EntryOn != null && route.ExitOn == null)
            {
                // InProgress sayfasındaysak giriş saatini göster
                if (StoreType == "InProgress" && route.EntryOn.HasValue)
                {
                    var entryTime = route.EntryOn.Value.ToString("HH:mm");
                    return $"🔄 Devam Ediyor (Giriş: {entryTime})";
                }
                else
                    return "🔄 Devam Ediyor";
            }
            // EntryOn da ExitOn da yoksa henüz başlanmamış
            else
                return "⏳ Henüz Başlanmamış";
        }

        private string GetStatusColor(entMemberRoute route)
        {
            // ExitOn varsa yeşil (tamamlandı)
            if (route.ExitOn != null)
                return "#4CAF50"; // Yeşil - tamamlandı
            // EntryOn varsa ama ExitOn yoksa (devam ediyor)
            else if (route.EntryOn != null && route.ExitOn == null)
            {
                // InProgress sayfasındaysak turuncu, diğer durumlarda sarı
                if (StoreType == "InProgress")
                    return "#FF9800"; // Turuncu - aktif olarak devam ediyor
                else
                    return "#FFC107"; // Sarı - devam ediyor/bekleyen
            }
            // EntryOn da ExitOn da yoksa sarı (planlandı/giriş yapılmamış)
            else
                return "#FFC107"; // Sarı - planlandı/giriş yapılmamış
        }

        // TapGestureRecognizer için alternatif metod
        private async void OnStoreTapped(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("OnStoreTapped called");
                Console.WriteLine("=== OnStoreTapped called ===");
                _logger?.LogInformation("OnStoreTapped method called");

                if (sender is Frame frame && frame.BindingContext is StoreViewModel selectedStore)
                {
                    Console.WriteLine($"Store tapped: {selectedStore.StoreName} (MemberRouteId: {selectedStore.MemberRouteId})");
                    System.Diagnostics.Debug.WriteLine($"Store tapped: {selectedStore.StoreName} (MemberRouteId: {selectedStore.MemberRouteId})");
                    _logger?.LogInformation($"Store tapped: {selectedStore.StoreName} (MemberRouteId: {selectedStore.MemberRouteId})");

                    // Visual feedback
                    await frame.ScaleTo(0.95, 50);
                    await frame.ScaleTo(1.0, 50);

                    // StoreReportPage'e git
                    var navigationParameters = new Dictionary<string, object>
                    {
                        ["entMemberRouteId"] = selectedStore.MemberRouteId.ToString(),
                        ["StoreName"] = selectedStore.StoreName ?? "Bilinmeyen Mağaza"
                    };

                    Console.WriteLine($"Navigation parameters (Tap): entMemberRouteId={selectedStore.MemberRouteId}, StoreName={selectedStore.StoreName}");
                    System.Diagnostics.Debug.WriteLine($"Navigation parameters (Tap): entMemberRouteId={selectedStore.MemberRouteId}, StoreName={selectedStore.StoreName}");
                    _logger?.LogInformation($"Navigating to StoreReportPage via tap with parameters: entMemberRouteId={selectedStore.MemberRouteId}, StoreName={selectedStore.StoreName}");

                    Console.WriteLine("Starting navigation...");
                    await Shell.Current.GoToAsync("///StoreReportPage", navigationParameters);
                    Console.WriteLine("Navigation completed (Tap)");
                    System.Diagnostics.Debug.WriteLine("Navigation completed (Tap)");
                }
                else
                {
                    Console.WriteLine("OnStoreTapped: Invalid sender or binding context");
                    System.Diagnostics.Debug.WriteLine("OnStoreTapped: Invalid sender or binding context");
                    _logger?.LogWarning("OnStoreTapped called but sender is not Frame or BindingContext is not StoreViewModel");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR in OnStoreTapped: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                _logger?.LogError(ex, "OnStoreTapped sırasında hata oluştu");
                System.Diagnostics.Debug.WriteLine($"Error in OnStoreTapped: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    await DisplayAlert("Hata", $"Mağaza sayfasına geçiş sırasında bir hata oluştu: {ex.Message}", "Tamam");
                });
            }
        }

        // Mağaza seçildiğinde
        private async void OnStoreSelected(object sender, SelectionChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("OnStoreSelected called");
            _logger?.LogInformation("OnStoreSelected method called");
            
            if (e.CurrentSelection.FirstOrDefault() is StoreViewModel selectedStore)
            {
                try
                {
                    System.Diagnostics.Debug.WriteLine($"Store selected: {selectedStore.StoreName} (MemberRouteId: {selectedStore.MemberRouteId})");
                    _logger?.LogInformation($"Store selected: {selectedStore.StoreName} (MemberRouteId: {selectedStore.MemberRouteId})");

                    // Mağaza seçimi sıfırla
                    CollectionView storesCollectionView = null;
                    try
                    {
                        storesCollectionView = this.FindByName<CollectionView>("StoresCollectionView");
                        if (storesCollectionView != null)
                        {
                            storesCollectionView.SelectedItem = null;
                            System.Diagnostics.Debug.WriteLine("Selection cleared");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger?.LogError(ex, "StoresCollectionView bulunamadı");
                        System.Diagnostics.Debug.WriteLine($"Error finding StoresCollectionView: {ex.Message}");
                    }

                    // StoreReportPage'e git - Android koduna uygun olarak MemberRouteId kullan
                    var navigationParameters = new Dictionary<string, object>
                    {
                        ["entMemberRouteId"] = selectedStore.MemberRouteId.ToString(),
                        ["StoreName"] = selectedStore.StoreName ?? "Bilinmeyen Mağaza"
                    };

                    System.Diagnostics.Debug.WriteLine($"Navigation parameters: entMemberRouteId={selectedStore.MemberRouteId}, StoreName={selectedStore.StoreName}");
                    _logger?.LogInformation($"Navigating to StoreReportPage with parameters: entMemberRouteId={selectedStore.MemberRouteId}, StoreName={selectedStore.StoreName}");

                    // Navigation yap
                    await Shell.Current.GoToAsync("///StoreReportPage", navigationParameters);
                    System.Diagnostics.Debug.WriteLine("Navigation completed");
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, "Mağaza seçimi sırasında hata oluştu");
                    System.Diagnostics.Debug.WriteLine($"Error in OnStoreSelected: {ex.Message}");
                    System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                    
                    await MainThread.InvokeOnMainThreadAsync(async () =>
                    {
                        await DisplayAlert("Hata", $"Mağaza sayfasına geçiş sırasında bir hata oluştu: {ex.Message}", "Tamam");
                    });
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("No store selected or selection is null");
                _logger?.LogWarning("OnStoreSelected called but no store was selected");
            }
        }
    }

    // Mağaza görünüm modeli
    public class StoreViewModel
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int RouteId { get; set; }
        public string StoreName { get; set; }
        public string StoreAddress { get; set; }
        public string Phone { get; set; }
        public string StoreType { get; set; }
        public string StatusColor { get; set; }
        public string LastVisit { get; set; }
        public bool HasLastVisit { get; set; }
        public int MemberRouteId { get; set; }
    }
}