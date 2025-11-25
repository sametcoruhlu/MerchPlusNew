using SQLite;
using Microsoft.Extensions.Logging;
using entMerchPlus;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Graphics;
using System.Collections.Generic;
using MerchPlusMAUI.Services;

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

                // Sayfa başlangıçta görünür olsun (siyah ekranı önlemek için)
                this.Opacity = 1;

                RefreshCommand = new Command(async () => 
                {
                    if (!_isLoading)
                    {
                        await LoadStoresAsync();
                    }
                });
                BindingContext = this;
                
                // Loading overlay'i hemen göster (sayfa render edilmeden önce)
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    var loadingOverlay = this.FindByName<Grid>("LoadingOverlay");
                    if (loadingOverlay != null)
                    {
                        loadingOverlay.IsVisible = true;
                    }
                });
                
                System.Diagnostics.Debug.WriteLine("StoresPage: Constructor completed");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"StoresPage initialize hatası: {ex.Message}");
                _logger?.LogError(ex, "StoresPage başlatma hatası");
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            // Sayfa başlangıçta görünür olsun (siyah ekranı önlemek için)
            this.Opacity = 1;
            
            System.Diagnostics.Debug.WriteLine($"StoresPage: OnAppearing called, StoreType = {StoreType}");
            UpdatePageTitle();
            
            // Loading overlay'i hemen göster (sayfa render edilmeden önce)
            var loadingOverlay = this.FindByName<Grid>("LoadingOverlay");
            if (loadingOverlay != null)
            {
                loadingOverlay.IsVisible = true;
            }
            
            // Floating back button'u göster
            FloatingBack?.Show();
            
            // Verileri yükle
            await LoadStoresAsync();
            
            // Veriler yüklendikten sonra loading overlay'i gizle
            if (loadingOverlay != null)
            {
                loadingOverlay.IsVisible = false;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            
            // Sayfa kapanırken fade-out
            _ = this.FadeTo(0, 150, Easing.CubicIn);
            
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

                            // Uzaklık hesapla
                            double? distanceMeters = null;
                            if (store.CoordinateX.HasValue && store.CoordinateY.HasValue && 
                                GlobalVariables.MemberLastCoordinate.Latitude != 0 && GlobalVariables.MemberLastCoordinate.Longitude != 0)
                            {
                                var haversine = new HaversineUtility();
                                var storePosition = new Position
                                {
                                    Latitude = Convert.ToDouble(store.CoordinateX.Value), // eski projede Convert.ToDouble kullanılıyor
                                    Longitude = Convert.ToDouble(store.CoordinateY.Value)  // eski projede Convert.ToDouble kullanılıyor
                                };

                                var currentPosition = new Position
                                {
                                    Latitude = GlobalVariables.MemberLastCoordinate.Latitude,
                                    Longitude = GlobalVariables.MemberLastCoordinate.Longitude
                                };

                                distanceMeters = haversine.GetDistanceInMeters(currentPosition, storePosition);
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
                                HasLastVisit = route.ExitOn != null,
                                DistanceMeters = distanceMeters,
                                DistanceText = distanceMeters.HasValue ? 
                                    (distanceMeters.Value < 1000 ? $"{distanceMeters.Value:F0}m" : $"{distanceMeters.Value / 1000:F1}km") : 
                                    "Konum bilgisi yok"
                            };

                            stores.Add(storeViewModel);
                        }
                    }

                    // Uzaklığa göre sırala (yakın olanlar önce)
                    stores = stores.OrderBy(s => s.DistanceMeters ?? double.MaxValue).ToList();

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
                
                // Loading overlay'i gizle
                var loadingOverlay = this.FindByName<Grid>("LoadingOverlay");
                if (loadingOverlay != null)
                {
                    loadingOverlay.IsVisible = false;
                }
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

                    // Konum durumunu kontrol et
                    _logger?.LogInformation($"=== MAĞAZA SEÇİMİ KONUM KONTROLÜ ===");
                    _logger?.LogInformation($"GlobalVariables.MemberLastCoordinate: Lat={GlobalVariables.MemberLastCoordinate.Latitude}, Lon={GlobalVariables.MemberLastCoordinate.Longitude}");
                    _logger?.LogInformation($"Store DistanceMeters: {selectedStore.DistanceMeters}");
                    _logger?.LogInformation($"Store DistanceText: {selectedStore.DistanceText}");

                    // Visual feedback
                    await frame.ScaleTo(0.95, 50);
                    await frame.ScaleTo(1.0, 50);

                    // Uzaklık kontrolü yap
                    var canProceed = await CheckStoreDistanceAndProceed(selectedStore);
                    if (!canProceed)
                    {
                        _logger?.LogWarning($"Mağaza seçimi başarısız, uzaklık kontrolü geçemedi");
                        return; // Uzaklık kontrolü başarısız, işlemi durdur
                    }
                    
                    _logger?.LogInformation($"Mağaza seçimi başarılı, StoreProductsPage'e yönlendiriliyor");

                    // StoreProductsPage'e git
                    var navigationParameters = new Dictionary<string, object>
                    {
                        ["entMemberRouteId"] = selectedStore.MemberRouteId.ToString(),
                        ["StoreName"] = selectedStore.StoreName ?? "Bilinmeyen Mağaza"
                    };

                    Console.WriteLine($"Navigation parameters (Tap): entMemberRouteId={selectedStore.MemberRouteId}, StoreName={selectedStore.StoreName}");
                    System.Diagnostics.Debug.WriteLine($"Navigation parameters (Tap): entMemberRouteId={selectedStore.MemberRouteId}, StoreName={selectedStore.StoreName}");
                    _logger?.LogInformation($"Navigating to StoreProductsPage via tap with parameters: entMemberRouteId={selectedStore.MemberRouteId}, StoreName={selectedStore.StoreName}");

                    Console.WriteLine("Starting navigation...");
                    await Shell.Current.GoToAsync("///StoreProductsPage", navigationParameters);
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

                    // Konum durumunu kontrol et
                    _logger?.LogInformation($"=== MAĞAZA SEÇİMİ KONUM KONTROLÜ (Selection) ===");
                    _logger?.LogInformation($"GlobalVariables.MemberLastCoordinate: Lat={GlobalVariables.MemberLastCoordinate.Latitude}, Lon={GlobalVariables.MemberLastCoordinate.Longitude}");
                    _logger?.LogInformation($"Store DistanceMeters: {selectedStore.DistanceMeters}");
                    _logger?.LogInformation($"Store DistanceText: {selectedStore.DistanceText}");

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

                    // Uzaklık kontrolü yap
                    var canProceed = await CheckStoreDistanceAndProceed(selectedStore);
                    if (!canProceed)
                    {
                        _logger?.LogWarning($"Mağaza seçimi başarısız, uzaklık kontrolü geçemedi (Selection)");
                        return; // Uzaklık kontrolü başarısız, işlemi durdur
                    }
                    
                    _logger?.LogInformation($"Mağaza seçimi başarılı, StoreProductsPage'e yönlendiriliyor (Selection)");

                    // StoreProductsPage'e git - Android koduna uygun olarak MemberRouteId kullan
                    var navigationParameters = new Dictionary<string, object>
                    {
                        ["entMemberRouteId"] = selectedStore.MemberRouteId.ToString(),
                        ["StoreName"] = selectedStore.StoreName ?? "Bilinmeyen Mağaza"
                    };

                    System.Diagnostics.Debug.WriteLine($"Navigation parameters: entMemberRouteId={selectedStore.MemberRouteId}, StoreName={selectedStore.StoreName}");
                    _logger?.LogInformation($"Navigating to StoreProductsPage with parameters: entMemberRouteId={selectedStore.MemberRouteId}, StoreName={selectedStore.StoreName}");

                    // Navigation yap
                    await Shell.Current.GoToAsync("///StoreProductsPage", navigationParameters);
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

        /// <summary>
        /// Mağaza uzaklık kontrolü yapar ve gerekirse giriş işlemini başlatır
        /// </summary>
        /// <param name="selectedStore">Seçilen mağaza</param>
        /// <returns>İşleme devam edilebilir mi?</returns>
        private async Task<bool> CheckStoreDistanceAndProceed(StoreViewModel selectedStore)
        {
            try
            {
                // Koordinat kontrolü - Detaylı log ekle
                _logger?.LogInformation($"=== KONUM KONTROLÜ BAŞLADI ===");
                _logger?.LogInformation($"GlobalVariables.MemberLastCoordinate.Latitude: {GlobalVariables.MemberLastCoordinate.Latitude}");
                _logger?.LogInformation($"GlobalVariables.MemberLastCoordinate.Longitude: {GlobalVariables.MemberLastCoordinate.Longitude}");
                
                // Veritabanından son konum bilgisini kontrol et
                // LocationAgentService'deki gibi Id'ye göre sıralama yap (AutoIncrement olduğu için en son eklenen en yüksek Id'ye sahip)
                using var dbCheck = new SQLiteConnection(_dbPath);
                dbCheck.CreateTable<MerchPlusMAUI.Models.entMemberCoordinate>();
                
                // entMemberCoordinate tablosundan son kaydı al (Id'ye göre - en son eklenen)
                var lastCoordinate = dbCheck.Query<MerchPlusMAUI.Models.entMemberCoordinate>(
                    "SELECT * FROM entMemberCoordinate ORDER BY Id DESC LIMIT 1").FirstOrDefault();
                
                if (lastCoordinate != null)
                {
                    var timeInfo = lastCoordinate.LastUpdateTime ?? lastCoordinate.CreatedOn;
                    _logger?.LogInformation($"Veritabanından son konum - Latitude: {lastCoordinate.Latitude}, Longitude: {lastCoordinate.Longitude}, Time: {timeInfo}");
                }
                else
                {
                    _logger?.LogWarning("Veritabanında hiç konum bilgisi bulunamadı!");
                }
                
                if (GlobalVariables.MemberLastCoordinate.Latitude == 0 && GlobalVariables.MemberLastCoordinate.Longitude == 0)
                {
                    _logger?.LogError("GlobalVariables.MemberLastCoordinate sıfır değerlerde! Konum servisi çalışmıyor olabilir.");
                    
                    await MainThread.InvokeOnMainThreadAsync(async () =>
                    {
                        await DisplayAlert("Koordinatınız Alınamadı", 
                            "MerchPlus uygulamasını çalıştırdığınızdan bu yana koordinatınız elde edilemedi. Lütfen gökyüzünü gören açık bir alana çıkarak bir süre bekleyiniz ve tekrar giriş yapmayı deneyiniz.", 
                            "Tamam");
                    });
                    return false;
                }
                
                _logger?.LogInformation($"=== KONUM KONTROLÜ BAŞARILI ===");

                using var db = new SQLiteConnection(_dbPath);
                
                // Mağaza bilgilerini al
                var route = db.Table<entMemberRoute>()
                    .FirstOrDefault(r => r.Id == selectedStore.MemberRouteId);
                
                _logger?.LogInformation($"Route bulundu - ID: {route?.Id}, Type: {route?.GetType().Name}");
                
                // Route'un Primary Key özelliklerini kontrol et
                if (route != null)
                {
                    var routeType = route.GetType();
                    var idProperty = routeType.GetProperty("Id");
                    _logger?.LogInformation($"Route.Id property: {idProperty?.Name}, Value: {route.Id}");
                    
                    // SQLite Table attribute'unu kontrol et
                    var tableAttributes = routeType.GetCustomAttributes(typeof(SQLite.TableAttribute), false);
                    if (tableAttributes.Length > 0)
                    {
                        _logger?.LogInformation($"Route Table attribute bulundu: {tableAttributes[0]}");
                    }
                    else
                    {
                        _logger?.LogWarning("Route Table attribute bulunamadı!");
                    }
                }
                
                if (route?.RetailShopId == null)
                {
                    await MainThread.InvokeOnMainThreadAsync(async () =>
                    {
                        await DisplayAlert("Hata", "Mağaza bilgileri bulunamadı.", "Tamam");
                    });
                    return false;
                }

                var retailShop = db.Table<entRetailShop>()
.FirstOrDefault(s => s.Id == route.RetailShopId.Value);

                _logger?.LogInformation($"Mağaza bilgileri - ID: {retailShop?.Id}, Name: {retailShop?.Name}");
                _logger?.LogInformation($"Mağaza koordinatları - CoordinateX: {retailShop?.CoordinateX}, CoordinateY: {retailShop?.CoordinateY}");

                if (retailShop?.CoordinateX == null || retailShop?.CoordinateY == null)
                {
                    _logger?.LogWarning($"Mağaza {retailShop?.Name} için koordinat bilgisi eksik!");
                    
                    await MainThread.InvokeOnMainThreadAsync(async () =>
                    {
                        await DisplayAlert("Uyarı", "Bu mağaza için koordinat bilgisi bulunmuyor. Devam etmek istiyor musunuz?", "Evet", "Hayır");
                    });
                    return true; // Koordinat yoksa devam et
                }

                // Uzaklık hesapla
                var haversine = new HaversineUtility();
                var storePosition = new Position
                {
                    Latitude = Convert.ToDouble(retailShop.CoordinateX.Value), // eski projede Convert.ToDouble kullanılıyor
                    Longitude = Convert.ToDouble(retailShop.CoordinateY.Value)  // eski projede Convert.ToDouble kullanılıyor
                };

                var currentPosition = new Position
                {
                    Latitude = GlobalVariables.MemberLastCoordinate.Latitude,
                    Longitude = GlobalVariables.MemberLastCoordinate.Longitude
                };

                _logger?.LogInformation($"Uzaklık hesaplama - Mağaza: Lat={storePosition.Latitude}, Lon={storePosition.Longitude}");
                _logger?.LogInformation($"Uzaklık hesaplama - Cihaz: Lat={currentPosition.Latitude}, Lon={currentPosition.Longitude}");

                var distanceMeters = haversine.GetDistanceInMeters(currentPosition, storePosition);
                _logger?.LogInformation($"Hesaplanan uzaklık: {distanceMeters:F2} metre");

                // 500 metre kontrolü
                if (distanceMeters > 500)
                {
                    await MainThread.InvokeOnMainThreadAsync(async () =>
                    {
                        await DisplayAlert("UZAKTASINIZ", 
                            $"Bu mağaza için sisteme tanımlı koordinatın yaklaşık olarak {distanceMeters:F2} metre uzağındasınız. Eğer giriş işlemini mağazanın içinde yapıyorsanız gökyüzünü gören bir alana çıkarak tekrar deneyiniz.", 
                            "Tamam");
                    });
                    return false;
                }

                // Mağaza durumu kontrolü - eski projeye göre tekrar giriş mantığı
                if (route.EntryOn == null)
                {
                    // İlk giriş - giriş tarihini kaydet
                    route.EntryOn = DateTime.Now;
                    route.EntryCoordinateX = (decimal)retailShop.CoordinateX.Value; // eski projede decimal türünde
                    route.EntryCoordinateY = (decimal)retailShop.CoordinateY.Value; // eski projede decimal türünde
                    
                    _logger?.LogInformation($"İlk giriş - Route güncelleniyor - ID: {route.Id}, EntryOn: {route.EntryOn}");
                    
                    try
                    {
                        db.Update(route);
                        _logger?.LogInformation($"Store entry recorded for {selectedStore.StoreName} at {DateTime.Now}");
                    }
                    catch (Exception updateEx)
                    {
                        _logger?.LogError(updateEx, $"Route güncelleme hatası - ID: {route.Id}");
                        
                        // Alternatif: SQL komutu ile güncelle
                        try
                        {
                            db.Execute("UPDATE entMemberRoute SET EntryOn = ?, EntryCoordinateX = ?, EntryCoordinateY = ? WHERE Id = ?",
                                route.EntryOn, route.EntryCoordinateX, route.EntryCoordinateY, route.Id);
                            _logger?.LogInformation($"Route SQL ile güncellendi - ID: {route.Id}");
                        }
                        catch (Exception sqlEx)
                        {
                            _logger?.LogError(sqlEx, $"SQL güncelleme hatası - ID: {route.Id}");
                            throw; // Hata fırlat
                        }
                    }
                }
                else if (route.ExitOn != null)
                {
                    // Daha önce çıkış yapılmış - tekrar giriş için onay iste
                    _logger?.LogInformation($"Mağaza daha önce tamamlanmış - EntryOn: {route.EntryOn}, ExitOn: {route.ExitOn}");
                    
                    var result = await MainThread.InvokeOnMainThreadAsync(async () =>
                    {
                        return await DisplayAlert("Mağaza Daha Önce Tamamlandı", 
                            $"Bu mağaza daha önce ziyaret edilmiş ve tamamlanmış.\r\n\r\n" +
                            $"Giriş: {route.EntryOn?.ToString("dd.MM.yyyy HH:mm")}\r\n" +
                            $"Çıkış: {route.ExitOn?.ToString("dd.MM.yyyy HH:mm")}\r\n\r\n" +
                            $"Tekrar giriş yapmak istiyor musunuz?", 
                            "EVET, TEKRAR GİRİŞ YAP", "VAZGEÇ");
                    });
                    
                    if (result)
                    {
                        // Tekrar giriş - yeni giriş tarihini kaydet, çıkış tarihini sıfırla
                        route.EntryOn = DateTime.Now;
                        route.ExitOn = null; // Çıkış tarihini sıfırla
                        route.EntryCoordinateX = (decimal)retailShop.CoordinateX.Value;
                        route.EntryCoordinateY = (decimal)retailShop.CoordinateY.Value;
                        route.IsSentToServer = false; // Server'a gönderilmemiş olarak işaretle
                        
                        _logger?.LogInformation($"Tekrar giriş - Route güncelleniyor - ID: {route.Id}, EntryOn: {route.EntryOn}, ExitOn: null");
                        
                        try
                        {
                            db.Update(route);
                            _logger?.LogInformation($"Store re-entry recorded for {selectedStore.StoreName} at {DateTime.Now}");
                        }
                        catch (Exception updateEx)
                        {
                            _logger?.LogError(updateEx, $"Route güncelleme hatası - ID: {route.Id}");
                            
                            // Alternatif: SQL komutu ile güncelle
                            try
                            {
                                db.Execute("UPDATE entMemberRoute SET EntryOn = ?, ExitOn = ?, EntryCoordinateX = ?, EntryCoordinateY = ?, IsSentToServer = ? WHERE Id = ?",
                                    route.EntryOn, route.ExitOn, route.EntryCoordinateX, route.EntryCoordinateY, route.IsSentToServer, route.Id);
                                _logger?.LogInformation($"Route SQL ile güncellendi - ID: {route.Id}");
                            }
                            catch (Exception sqlEx)
                            {
                                _logger?.LogError(sqlEx, $"SQL güncelleme hatası - ID: {route.Id}");
                                throw; // Hata fırlat
                            }
                        }
                    }
                    else
                    {
                        _logger?.LogInformation($"Kullanıcı tekrar giriş yapmayı reddetti - Mağaza: {selectedStore.StoreName}");
                        return false; // Tekrar giriş reddedildi
                    }
                }
                else
                {
                    // Giriş yapılmış ama çıkış yapılmamış - devam ediyor
                    _logger?.LogInformation($"Mağaza zaten aktif - EntryOn: {route.EntryOn}, ExitOn: null");
                    
                    await MainThread.InvokeOnMainThreadAsync(async () =>
                    {
                        await DisplayAlert("Mağaza Zaten Aktif", 
                            $"Bu mağaza zaten aktif olarak ziyaret ediliyor.\r\n\r\n" +
                            $"Giriş: {route.EntryOn?.ToString("dd.MM.yyyy HH:mm")}\r\n\r\n" +
                            $"Mağaza sayfasına yönlendiriliyorsunuz.", 
                            "Tamam");
                    });
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Uzaklık kontrolü sırasında hata oluştu");
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    await DisplayAlert("Hata", $"Uzaklık kontrolü sırasında bir hata oluştu: {ex.Message}", "Tamam");
                });
                return false;
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
        public double? DistanceMeters { get; set; }
        public string DistanceText { get; set; } = string.Empty;
    }
}