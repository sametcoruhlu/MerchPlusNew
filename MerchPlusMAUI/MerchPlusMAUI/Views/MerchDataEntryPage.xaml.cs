using SQLite;
using Microsoft.Extensions.Logging;
using entMerchPlus;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MerchPlusMAUI.Services;
using MerchPlusMAUI.Models;

namespace MerchPlusMAUI.Views
{
    [QueryProperty(nameof(EntMemberRouteDetailId), "entMemberRouteDetailId")]
    public partial class MerchDataEntryPage : ContentPage, INotifyPropertyChanged
    {
        private readonly ILogger<MerchDataEntryPage>? _logger;
        private readonly IFieldVisibilityService _fieldVisibilityService;
        private readonly string _dbPath;
        private int _currentId = 0;
        private entMemberRouteDetail? _currentMemberRouteDetail;
        private FieldVisibilityModel? _fieldVisibility;

        private string _entMemberRouteDetailId = string.Empty;
        private bool _isNavigatingAway = false;
        
        // Store route and store name for navigation back to StoreProductsPage
        private int? _currentMemberRouteId = null;
        private string _currentStoreName = string.Empty;
        
        // Public properties for FloatingBackButton to access
        public string EntMemberRouteId => _currentMemberRouteId?.ToString() ?? string.Empty;
        public string StoreName => _currentStoreName ?? string.Empty;
        
        // XAML elementleri için property'ler
        private Grid? _loadingOverlay;
        private ScrollView? _mainContent;
        
        public string EntMemberRouteDetailId
        {
            get => _entMemberRouteDetailId;
            set
            {
                // Eğer navigation yapılıyorsa LoadProductDataAsync çağırma
                if (_isNavigatingAway)
                {
                    System.Diagnostics.Debug.WriteLine("EntMemberRouteDetailId: Navigation yapılıyor, LoadProductDataAsync çağrılmıyor");
                    return;
                }
                
                _entMemberRouteDetailId = value;
                OnPropertyChanged();
                _ = LoadProductDataAsync();
            }
        }

        // Parameterless constructor for Shell navigation
        public MerchDataEntryPage() : this(null, null)
        {
        }

        public MerchDataEntryPage(ILogger<MerchDataEntryPage>? logger, IFieldVisibilityService? fieldVisibilityService)
        {
            System.Diagnostics.Debug.WriteLine("MerchDataEntryPage Constructor: Başladı");
            
            InitializeComponent();
            _logger = logger;
            _fieldVisibilityService = fieldVisibilityService ?? new FieldVisibilityService(new HttpClient());
            
            System.Diagnostics.Debug.WriteLine($"MerchDataEntryPage Constructor: _fieldVisibilityService null mu? {_fieldVisibilityService == null}");
            
            _dbPath = GlobalVariables.DatabasePath; // GlobalVariables'daki veritabanı yolunu kullan
            BindingContext = this;
            
            // Sayfa başlangıçta görünür olsun (siyah ekranı önlemek için)
            this.Opacity = 1;
            
            LoadActivityTypes();
            SetupEventHandlers();
            
            // Loading overlay'i hemen göster (sayfa render edilmeden önce)
            MainThread.BeginInvokeOnMainThread(() =>
            {
                var loadingOverlay = this.FindByName<Grid>("LoadingOverlay");
                if (loadingOverlay != null)
                {
                    loadingOverlay.IsVisible = true;
                }
            });
            
            System.Diagnostics.Debug.WriteLine("MerchDataEntryPage Constructor: Tamamlandı");
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            // Sayfa başlangıçta görünür olsun (siyah ekranı önlemek için)
            this.Opacity = 1;
            
            System.Diagnostics.Debug.WriteLine("=== MerchDataEntryPage OnAppearing ===");
            
            try
            {
                // XAML elementlerini initialize et
                _loadingOverlay = FindByName("LoadingOverlay") as Grid;
                _mainContent = FindByName("MainContent") as ScrollView;
                
                System.Diagnostics.Debug.WriteLine($"LoadingOverlay found: {_loadingOverlay != null}");
                System.Diagnostics.Debug.WriteLine($"MainContent found: {_mainContent != null}");
                
                // Loading overlay'i hemen göster (sayfa render edilmeden önce)
                if (_loadingOverlay != null)
                {
                    _loadingOverlay.IsVisible = true;
                }
                
                // Veri yükleme işlemini başlat
                await LoadProductDataAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"OnAppearing error: {ex.Message}");
                _logger?.LogError(ex, "Error in OnAppearing");
                HideLoadingOverlay();
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            
            // FadeTo(0) kaldırıldı - Shell navigation kendi animasyonunu yönetiyor
            HideLoadingOverlay();
        }

        private void ShowLoadingOverlay()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                _loadingOverlay = FindByName("LoadingOverlay") as Grid;
                _mainContent = FindByName("MainContent") as ScrollView;
                
                if (_loadingOverlay != null)
                {
                    _loadingOverlay.IsVisible = true;
                    _loadingOverlay.InputTransparent = false; // Tıklamaları engelle
                    _loadingOverlay.Opacity = 1; // Opacity'yi açıkça 1 yap
                    
                    // Frame ve Label'ın opacity'sini de kontrol et
                    if (_loadingOverlay.Children.Count > 0 && _loadingOverlay.Children[0] is Frame frame)
                    {
                        frame.Opacity = 1;
                        if (frame.Content is StackLayout stackLayout && stackLayout.Children.Count > 1)
                        {
                            if (stackLayout.Children[1] is Label label)
                            {
                                label.Opacity = 1;
                                // TextColor'u açıkça set et (koyu gri)
                                label.TextColor = Microsoft.Maui.Graphics.Color.FromRgb(52, 58, 64);
                            }
                        }
                    }
                    
                    System.Diagnostics.Debug.WriteLine("MerchDataEntryPage: Loading overlay shown");
                }
                
                if (_mainContent != null)
                {
                    _mainContent.IsVisible = false;
                }
            });
        }
        
        // Overlay'in görünürlüğünü kontrol et ve gerekirse tekrar göster
        private void EnsureLoadingOverlayVisible()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (_loadingOverlay != null && !_loadingOverlay.IsVisible)
                {
                    _loadingOverlay.IsVisible = true;
                    _loadingOverlay.InputTransparent = false;
                    System.Diagnostics.Debug.WriteLine("MerchDataEntryPage: Loading overlay re-shown (EnsureLoadingOverlayVisible)");
                }
            });
        }

        private void HideLoadingOverlay()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                _loadingOverlay = FindByName("LoadingOverlay") as Grid;
                _mainContent = FindByName("MainContent") as ScrollView;
                
                if (_loadingOverlay != null)
                {
                    _loadingOverlay.IsVisible = false;
                    _loadingOverlay.InputTransparent = true; // Tıklamalara izin ver
                    System.Diagnostics.Debug.WriteLine("MerchDataEntryPage: Loading overlay hidden");
                }
                
                if (_mainContent != null)
                {
                    _mainContent.IsVisible = true;
                }
            });
        }

        private void LoadActivityTypes()
        {
            // Load activity types from resources (equivalent to Android Resource.Array.activitytypearray_array)
            ActivityTypePicker.ItemsSource = new List<string>
            {
                "NORMAL SATIŞ",
                "PROMOSYON",
                "FIYAT INDIRIMI",
                "YENİ ÜRÜN",
                "SEZON SONU"
            };
        }

        private void SetupEventHandlers()
        {
            ActivityTypePicker.SelectedIndexChanged += OnActivityTypeSelectionChanged;
        }

        private async Task LoadFieldVisibilityAsync()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("LoadFieldVisibilityAsync: Başladı");
                
                if (_fieldVisibilityService == null)
                {
                    System.Diagnostics.Debug.WriteLine("LoadFieldVisibilityAsync: _fieldVisibilityService null!");
                    ShowAllFields();
                    return;
                }

                // CustomerId'yi al - önce route detail'dan, sonra kullanıcı profilinden
                int customerId = await GetCustomerIdAsync();
                
                if (customerId <= 0)
                {
                    System.Diagnostics.Debug.WriteLine("LoadFieldVisibilityAsync: CustomerId bulunamadı, tüm alanlar gösteriliyor");
                    ShowAllFields();
                    return;
                }

                System.Diagnostics.Debug.WriteLine($"LoadFieldVisibilityAsync: CustomerId: {customerId}, Servis çağrısı yapılıyor...");
                _fieldVisibility = await _fieldVisibilityService.GetFieldVisibilityAsync(customerId);
                
                System.Diagnostics.Debug.WriteLine($"LoadFieldVisibilityAsync: Servis cevabı alındı: {_fieldVisibility != null}");
                
                ApplyFieldVisibility();
                
                // Loading overlay'i gizle ve ana içeriği göster
                HideLoadingOverlay();
                
                System.Diagnostics.Debug.WriteLine("LoadFieldVisibilityAsync: Tamamlandı");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"LoadFieldVisibilityAsync: Hata: {ex.Message}");
                _logger?.LogError(ex, "Error loading field visibility");
                // Hata durumunda tüm alanları göster
                ShowAllFields();
                // Loading overlay'i gizle
                HideLoadingOverlay();
            }
        }

        private async Task<int> GetCustomerIdAsync()
        {
            try
            {
                // 1. Önce GlobalVariables.CurrentCustomerId'yi kontrol et (en yaygın yöntem)
                if (!string.IsNullOrEmpty(GlobalVariables.CurrentCustomerId))
                {
                    if (int.TryParse(GlobalVariables.CurrentCustomerId, out int globalCustomerId))
                    {
                        System.Diagnostics.Debug.WriteLine($"GetCustomerIdAsync: GlobalVariables.CurrentCustomerId: {globalCustomerId}");
                        return globalCustomerId;
                    }
                }

                // 2. Route detail'dan customer bilgisini almaya çalış
                if (_currentMemberRouteDetail != null)
                {
                    using var db = new SQLiteConnection(_dbPath);
                    var route = db.Query<entMemberRoute>("SELECT * FROM entMemberRoute WHERE Id=?", _currentMemberRouteDetail.MemberRouteId);
                    
                    if (route.Count > 0)
                    {
                        var retailShop = db.Query<entRetailShop>("SELECT * FROM entRetailShop WHERE Id=?", route[0].RetailShopId);
                        if (retailShop.Count > 0)
                        {
                            // RetailShop
                            System.Diagnostics.Debug.WriteLine($"GetCustomerIdAsync: RetailShop Name: {retailShop[0].Name}");
                            
                            // entRetailShop'da CustomerId property'si var mı kontrol et
                            var customerIdProperty = typeof(entRetailShop).GetProperty("CustomerId");
                            if (customerIdProperty != null)
                            {
                                var customerIdValue = customerIdProperty.GetValue(retailShop[0]);
                                if (customerIdValue != null && int.TryParse(customerIdValue.ToString(), out int customerId))
                                {
                                    System.Diagnostics.Debug.WriteLine($"GetCustomerIdAsync: RetailShop'dan CustomerId: {customerId}");
                                    return customerId;
                                }
                            }
                            
                            // Alternatif: Route'dan CustomerId al
                            if (route[0].CustomerId != null)
                            {
                                System.Diagnostics.Debug.WriteLine($"GetCustomerIdAsync: Route'dan CustomerId: {route[0].CustomerId}");
                                return (int)route[0].CustomerId;
                            }
                        }
                    }
                }

                // 3. UserProfileService'den customer bilgisini al
                var userProfileService = UserProfileService.Instance;
                var currentUser = userProfileService?.GetCurrentUser();
                if (currentUser != null && !string.IsNullOrEmpty(currentUser.Id))
                {
                    if (int.TryParse(currentUser.Id, out int userCustomerId))
                    {
                        System.Diagnostics.Debug.WriteLine($"GetCustomerIdAsync: UserProfile'den CustomerId: {userCustomerId}");
                        return userCustomerId;
                    }
                }

                // 4. GlobalVariables.CurrentMember'dan al
                if (GlobalVariables.CurrentMember != null && !string.IsNullOrEmpty(GlobalVariables.CurrentMember.Id))
                {
                    if (int.TryParse(GlobalVariables.CurrentMember.Id, out int memberCustomerId))
                    {
                        System.Diagnostics.Debug.WriteLine($"GetCustomerIdAsync: CurrentMember'den CustomerId: {memberCustomerId}");
                        return memberCustomerId;
                    }
                }

                System.Diagnostics.Debug.WriteLine("GetCustomerIdAsync: CustomerId bulunamadı, test amaçlı 1 kullanılıyor");
                return 1; // Test amaçlı sabit CustomerId
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetCustomerIdAsync: Hata: {ex.Message}");
                return 1; // Hata durumunda da test amaçlı 1 döndür
            }
        }

        private void ApplyFieldVisibility()
        {
            System.Diagnostics.Debug.WriteLine("ApplyFieldVisibility: Başladı");
            
            // Eğer servisten boş cevap gelirse (null) tüm alanları göster
            if (_fieldVisibility == null)
            {
                System.Diagnostics.Debug.WriteLine("ApplyFieldVisibility: _fieldVisibility null, tüm alanlar gösteriliyor (servis cevap vermedi)");
                ShowAllFields();
                return;
            }

            System.Diagnostics.Debug.WriteLine($"ApplyFieldVisibility: Alanlar ayarlanıyor - FaceCount: {_fieldVisibility.FaceCount}, ShelfIndex: {_fieldVisibility.ShelfIndex}");
            
            // Servisten gelen veriye göre alanları göster/gizle
            FaceCountGrid.IsVisible = _fieldVisibility.FaceCount;
            ShelfIndexGrid.IsVisible = _fieldVisibility.ShelfIndex;
            ShelfStockGrid.IsVisible = _fieldVisibility.ShelfStock;
            PriceGrid.IsVisible = _fieldVisibility.Price;
            WarehouseStockGrid.IsVisible = _fieldVisibility.WarehouseStock;
            ActivityTypeGrid.IsVisible = _fieldVisibility.ActivityType;
            DiscountPriceGrid.IsVisible = _fieldVisibility.DiscountPrice;
            NotesGrid.IsVisible = _fieldVisibility.Notes;
            
            System.Diagnostics.Debug.WriteLine("ApplyFieldVisibility: Tamamlandı");
        }

        private void ShowAllFields()
        {
            System.Diagnostics.Debug.WriteLine("ShowAllFields: Tüm alanlar gösteriliyor");
            
            // Tüm alanları göster
            FaceCountGrid.IsVisible = true;
            ShelfIndexGrid.IsVisible = true;
            ShelfStockGrid.IsVisible = true;
            PriceGrid.IsVisible = true;
            WarehouseStockGrid.IsVisible = true;
            ActivityTypeGrid.IsVisible = true;
            DiscountPriceGrid.IsVisible = false; // Bu alan aktivite tipine göre kontrol ediliyor
            NotesGrid.IsVisible = true;
            
            System.Diagnostics.Debug.WriteLine("ShowAllFields: Tamamlandı");
        }

        private void HideAllFields()
        {
            System.Diagnostics.Debug.WriteLine("HideAllFields: Tüm alanlar gizleniyor");
            
            // Tüm alanları gizle
            FaceCountGrid.IsVisible = false;
            ShelfIndexGrid.IsVisible = false;
            ShelfStockGrid.IsVisible = false;
            PriceGrid.IsVisible = false;
            WarehouseStockGrid.IsVisible = false;
            ActivityTypeGrid.IsVisible = false;
            DiscountPriceGrid.IsVisible = false;
            NotesGrid.IsVisible = false;
            
            System.Diagnostics.Debug.WriteLine("HideAllFields: Tamamlandı");
        }


        private async Task<bool> CheckTableExistsAsync(SQLiteConnection db, string tableName)
        {
            try
            {
                var result = await Task.Run(() => 
                {
                    var count = db.ExecuteScalar<int>("SELECT COUNT(name) FROM sqlite_master WHERE name = ?", tableName);
                    return count > 0;
                });
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"CheckTableExistsAsync: Hata - {ex.Message}");
                return false;
            }
        }

        private async Task LoadProductDataAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(_entMemberRouteDetailId)) return;

                // Loading overlay zaten OnAppearing'de gösterildi
                System.Diagnostics.Debug.WriteLine("LoadProductDataAsync: Veri yükleme başlıyor");

                _currentId = Convert.ToInt32(_entMemberRouteDetailId);

                using var db = new SQLiteConnection(_dbPath);
                
                // Önce tablo varlığını kontrol et
                System.Diagnostics.Debug.WriteLine($"LoadProductDataAsync: Veritabanı yolu: {_dbPath}");
                
                var tableExists = await CheckTableExistsAsync(db, "entMemberRouteDetail");
                System.Diagnostics.Debug.WriteLine($"LoadProductDataAsync: entMemberRouteDetail tablosu var mı: {tableExists}");
                
                if (!tableExists)
                {
                    await DisplayAlert("Hata", "Veritabanı tabloları henüz oluşturulmamış. Lütfen uygulamayı yeniden başlatın.", "Tamam");
                    await Shell.Current.GoToAsync("..");
                    return;
                }
                
                // Load route detail - Raw SQL kullanarak veri oku
                System.Diagnostics.Debug.WriteLine($"Loading route detail for Id: {_currentId}");
                
                // Load route detail
                var routeDetail = db.Query<entMemberRouteDetail>("SELECT * FROM entMemberRouteDetail WHERE Id=?", _currentId);
                System.Diagnostics.Debug.WriteLine($"Found {routeDetail.Count} route detail records");
                
                // routeDetail datasını detaylı logla
                if (routeDetail.Count > 0)
                {
                    System.Diagnostics.Debug.WriteLine("=== Raw routeDetail data from database ===");
                    System.Diagnostics.Debug.WriteLine($"routeDetail[0].Id: {routeDetail[0].Id}");
                    System.Diagnostics.Debug.WriteLine($"routeDetail[0].MemberRouteId: {routeDetail[0].MemberRouteId}");
                    System.Diagnostics.Debug.WriteLine($"routeDetail[0].CustomerProductId: {routeDetail[0].CustomerProductId}");
                    System.Diagnostics.Debug.WriteLine($"routeDetail[0].FaceCount: {routeDetail[0].FaceCount}");
                    System.Diagnostics.Debug.WriteLine($"routeDetail[0].ShelfIndex: {routeDetail[0].ShelfIndex}");
                    System.Diagnostics.Debug.WriteLine($"routeDetail[0].ShelfStock: {routeDetail[0].ShelfStock}");
                    System.Diagnostics.Debug.WriteLine($"routeDetail[0].ShelfPrice: {routeDetail[0].ShelfPrice}");
                    System.Diagnostics.Debug.WriteLine($"routeDetail[0].WarehouseStock: {routeDetail[0].WarehouseStock}");
                    System.Diagnostics.Debug.WriteLine($"routeDetail[0].Notes: '{routeDetail[0].Notes}'");
                    System.Diagnostics.Debug.WriteLine($"routeDetail[0].ActivityTypeName: '{routeDetail[0].ActivityTypeName}'");
                    System.Diagnostics.Debug.WriteLine($"routeDetail[0].DiscountedPrice: {routeDetail[0].DiscountedPrice}");
                    System.Diagnostics.Debug.WriteLine($"routeDetail[0].CreatedOn: {routeDetail[0].CreatedOn}");
                    System.Diagnostics.Debug.WriteLine($"routeDetail[0].IsSentToServer: {routeDetail[0].IsSentToServer}");
                    System.Diagnostics.Debug.WriteLine($"routeDetail[0].SentToServerOn: {routeDetail[0].SentToServerOn}");
                    System.Diagnostics.Debug.WriteLine("=== End of raw routeDetail data ===");
                }
                
                if (routeDetail.Count == 0)
                {
                    System.Diagnostics.Debug.WriteLine("No route detail found, showing error");
                    await DisplayAlert("Hata", "Ürün bilgisi bulunamadı.", "Tamam");
                    await Shell.Current.GoToAsync("..");
                    return;
                }

                _currentMemberRouteDetail = routeDetail[0];
                System.Diagnostics.Debug.WriteLine($"Route detail loaded: Id={routeDetail[0].Id}, MemberRouteId={routeDetail[0].MemberRouteId}");
                
                // Tüm property'leri debug et
                System.Diagnostics.Debug.WriteLine($"FaceCount: {routeDetail[0].FaceCount}");
                System.Diagnostics.Debug.WriteLine($"ShelfIndex: {routeDetail[0].ShelfIndex}");
                System.Diagnostics.Debug.WriteLine($"ShelfStock: {routeDetail[0].ShelfStock}");
                System.Diagnostics.Debug.WriteLine($"ShelfPrice: {routeDetail[0].ShelfPrice}");
                System.Diagnostics.Debug.WriteLine($"WarehouseStock: {routeDetail[0].WarehouseStock}");
                System.Diagnostics.Debug.WriteLine($"Notes: '{routeDetail[0].Notes}'");
                System.Diagnostics.Debug.WriteLine($"ActivityTypeName: '{routeDetail[0].ActivityTypeName}'");
                System.Diagnostics.Debug.WriteLine($"DiscountedPrice: {routeDetail[0].DiscountedPrice}");
                System.Diagnostics.Debug.WriteLine($"CreatedOn: {routeDetail[0].CreatedOn}");

                // Load route
                var route = db.Query<entMemberRoute>("SELECT * FROM entMemberRoute WHERE Id=?", _currentMemberRouteDetail.MemberRouteId);
                if (route.Count == 0) return;

                // Store MemberRouteId for navigation back
                _currentMemberRouteId = _currentMemberRouteDetail.MemberRouteId;
                System.Diagnostics.Debug.WriteLine($"MerchDataEntryPage: Stored MemberRouteId: {_currentMemberRouteId}");

                // Load retail shop
                var retailShop = db.Query<entRetailShop>("SELECT * FROM entRetailShop WHERE Id=?", route[0].RetailShopId);
                if (retailShop.Count > 0)
                {
                    RetailShopNameLabel.Text = retailShop[0].Name;
                    // Store StoreName for navigation back
                    _currentStoreName = retailShop[0].Name ?? string.Empty;
                    System.Diagnostics.Debug.WriteLine($"MerchDataEntryPage: Stored StoreName: {_currentStoreName}");
                }

                // Load product
                var product = db.Query<entCustomerProduct>("SELECT * FROM entCustomerProduct WHERE Id=?", _currentMemberRouteDetail.CustomerProductId);
                if (product.Count > 0)
                {
                    CustomerProductNameLabel.Text = product[0].Name;
                }

                // Load existing data - using the actual properties from entMemberRouteDetail
                // Note: These properties might have different names in the actual entity
                // We'll use the properties that exist in the entMemberRouteDetail class
                
                // Initialize all fields as empty so placeholders are shown
                // Only set values if existing data is found
                System.Diagnostics.Debug.WriteLine("Initializing fields with empty values (placeholders will be shown)...");
                FaceCountEntry.Text = string.Empty;
                ShelfIndexEntry.Text = string.Empty;
                ShelfStockEntry.Text = string.Empty;
                ShelfPrice1Entry.Text = string.Empty;
                ShelfPrice2Entry.Text = string.Empty;
                WarehouseStockEntry.Text = string.Empty;
                NotesEditor.Text = string.Empty;
                DiscountPrice1Entry.Text = string.Empty;
                DiscountPrice2Entry.Text = string.Empty;
                ActivityTypePicker.SelectedIndex = -1;

                // Try to load existing data if the properties exist
                try
                {
                    System.Diagnostics.Debug.WriteLine("=== Loading existing data from entMemberRouteDetail ===");
                    
                    // Load existing data from entMemberRouteDetail
                    if (_currentMemberRouteDetail.FaceCount.HasValue)
                    {
                        FaceCountEntry.Text = _currentMemberRouteDetail.FaceCount.Value.ToString();
                        System.Diagnostics.Debug.WriteLine($"FaceCount loaded: {_currentMemberRouteDetail.FaceCount.Value}");
                    }
                    else
                    {
                        FaceCountEntry.Text = string.Empty;
                        System.Diagnostics.Debug.WriteLine("FaceCount is null or empty - showing placeholder");
                    }
                    
                    if (_currentMemberRouteDetail.ShelfIndex.HasValue)
                    {
                        ShelfIndexEntry.Text = _currentMemberRouteDetail.ShelfIndex.Value.ToString();
                        System.Diagnostics.Debug.WriteLine($"ShelfIndex loaded: {_currentMemberRouteDetail.ShelfIndex.Value}");
                    }
                    else
                    {
                        ShelfIndexEntry.Text = string.Empty;
                        System.Diagnostics.Debug.WriteLine("ShelfIndex is null or empty - showing placeholder");
                    }
                    
                    if (_currentMemberRouteDetail.ShelfStock.HasValue)
                    {
                        ShelfStockEntry.Text = _currentMemberRouteDetail.ShelfStock.Value.ToString();
                        System.Diagnostics.Debug.WriteLine($"ShelfStock loaded: {_currentMemberRouteDetail.ShelfStock.Value}");
                    }
                    else
                    {
                        ShelfStockEntry.Text = string.Empty;
                        System.Diagnostics.Debug.WriteLine("ShelfStock is null or empty - showing placeholder");
                    }
                    
                    if (_currentMemberRouteDetail.ShelfPrice.HasValue)
                    {
                        var priceValue = _currentMemberRouteDetail.ShelfPrice.Value;
                        ShelfPrice1Entry.Text = Convert.ToString(Math.Truncate(priceValue));
                        ShelfPrice2Entry.Text = Convert.ToInt32((priceValue - Math.Truncate(priceValue)) * 100).ToString("00");
                        System.Diagnostics.Debug.WriteLine($"ShelfPrice loaded: {priceValue}");
                    }
                    else
                    {
                        ShelfPrice1Entry.Text = string.Empty;
                        ShelfPrice2Entry.Text = string.Empty;
                        System.Diagnostics.Debug.WriteLine("ShelfPrice is null or empty - showing placeholder");
                    }
                    
                    if (_currentMemberRouteDetail.WarehouseStock.HasValue)
                    {
                        WarehouseStockEntry.Text = _currentMemberRouteDetail.WarehouseStock.Value.ToString();
                        System.Diagnostics.Debug.WriteLine($"WarehouseStock loaded: {_currentMemberRouteDetail.WarehouseStock.Value}");
                    }
                    else
                    {
                        WarehouseStockEntry.Text = string.Empty;
                        System.Diagnostics.Debug.WriteLine("WarehouseStock is null or empty - showing placeholder");
                    }
                    
                    if (!string.IsNullOrEmpty(_currentMemberRouteDetail.Notes))
                    {
                        NotesEditor.Text = _currentMemberRouteDetail.Notes;
                        System.Diagnostics.Debug.WriteLine($"Notes loaded: {_currentMemberRouteDetail.Notes}");
                    }
                    else
                    {
                        NotesEditor.Text = string.Empty;
                        System.Diagnostics.Debug.WriteLine("Notes is null or empty - showing placeholder");
                    }
                    
                    if (!string.IsNullOrEmpty(_currentMemberRouteDetail.ActivityTypeName))
                    {
                        var activityTypes = ActivityTypePicker.ItemsSource as List<string>;
                        var index = activityTypes?.IndexOf(_currentMemberRouteDetail.ActivityTypeName) ?? -1;
                        if (index >= 0)
                        {
                            ActivityTypePicker.SelectedIndex = index;
                            System.Diagnostics.Debug.WriteLine($"ActivityTypeName loaded: {_currentMemberRouteDetail.ActivityTypeName} (index: {index})");
                        }
                        else
                        {
                            ActivityTypePicker.SelectedIndex = -1;
                            System.Diagnostics.Debug.WriteLine($"ActivityTypeName not found in picker: {_currentMemberRouteDetail.ActivityTypeName} - showing placeholder");
                        }
                    }
                    else
                    {
                        ActivityTypePicker.SelectedIndex = -1;
                        System.Diagnostics.Debug.WriteLine("ActivityTypeName is null or empty - showing placeholder");
                    }
                    
                    if (_currentMemberRouteDetail.DiscountedPrice.HasValue)
                    {
                        var discountValue = _currentMemberRouteDetail.DiscountedPrice.Value;
                        DiscountPrice1Entry.Text = Convert.ToString(Math.Truncate(discountValue));
                        DiscountPrice2Entry.Text = Convert.ToInt32((discountValue - Math.Truncate(discountValue)) * 100).ToString("00");
                        System.Diagnostics.Debug.WriteLine($"DiscountedPrice loaded: {discountValue}");
                    }
                    else
                    {
                        DiscountPrice1Entry.Text = string.Empty;
                        DiscountPrice2Entry.Text = string.Empty;
                        System.Diagnostics.Debug.WriteLine("DiscountedPrice is null or empty - showing placeholder");
                    }
                    
                    System.Diagnostics.Debug.WriteLine("=== Existing data loading completed ===");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error loading existing data: {ex.Message}");
                    _logger?.LogWarning(ex, "Some properties might not exist in entMemberRouteDetail");
                }

                // Product data yüklendikten sonra field visibility'yi yükle
                System.Diagnostics.Debug.WriteLine("LoadProductDataAsync: Field visibility yükleniyor...");
                await LoadFieldVisibilityAsync();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error loading product data");
                await DisplayAlert("Hata", "Ürün bilgileri yüklenirken hata oluştu.", "Tamam");
            }
        }

        private void OnActivityTypeSelectionChanged(object sender, EventArgs e)
        {
            if (sender is Picker picker)
            {
                var selectedActivity = picker.SelectedItem?.ToString();
                
                // Show/hide discount price based on both service visibility and activity type
                bool shouldShowDiscountPrice = false;
                
                // Önce servisten gelen görünürlük ayarını kontrol et
                if (_fieldVisibility != null)
                {
                    shouldShowDiscountPrice = _fieldVisibility.DiscountPrice;
                }
                else
                {
                    // Servisten boş cevap gelirse tüm alanları göster
                    shouldShowDiscountPrice = true;
                }
                
                // Sonra aktivite tipini kontrol et
                if (shouldShowDiscountPrice && selectedActivity == "FIYAT INDIRIMI")
                {
                    DiscountPriceGrid.IsVisible = true;
                }
                else
                {
                    DiscountPriceGrid.IsVisible = false;
                    DiscountPrice1Entry.Text = string.Empty;
                    DiscountPrice2Entry.Text = string.Empty;
                }
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            ShowLoadingOverlay();
            System.Diagnostics.Debug.WriteLine("OnSaveClicked: Kaydet butonuna tıklandı");
            string errorMessage = string.Empty;

            // Sadece görünen alanları kontrol et
            System.Diagnostics.Debug.WriteLine("OnSaveClicked: Alan görünürlük kontrolü başlıyor...");

            // Face Count alanı görünürse kontrol et
            if (FaceCountGrid.IsVisible)
            {
                System.Diagnostics.Debug.WriteLine("OnSaveClicked: FaceCount alanı görünür, kontrol ediliyor...");
                if (string.IsNullOrEmpty(FaceCountEntry.Text) || Convert.ToInt32(FaceCountEntry.Text) < 0)
                {
                    errorMessage += "* Önyüz boş bırakılamaz veya sıfırdan küçük olamaz.\r\n";
                    System.Diagnostics.Debug.WriteLine("OnSaveClicked: FaceCount validasyon hatası - boş veya negatif");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"OnSaveClicked: FaceCount değeri: {FaceCountEntry.Text}");
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("OnSaveClicked: FaceCount alanı gizli, atlanıyor");
            }

            // Shelf Index alanı görünürse kontrol et
            if (ShelfIndexGrid.IsVisible)
            {
                System.Diagnostics.Debug.WriteLine("OnSaveClicked: ShelfIndex alanı görünür, kontrol ediliyor...");
                if (string.IsNullOrEmpty(ShelfIndexEntry.Text) || Convert.ToInt32(ShelfIndexEntry.Text) < 0)
                {
                    errorMessage += "* Raf Sırası boş bırakılamaz veya sıfırdan küçük olamaz.\r\n";
                    System.Diagnostics.Debug.WriteLine("OnSaveClicked: ShelfIndex validasyon hatası - boş veya negatif");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"OnSaveClicked: ShelfIndex değeri: {ShelfIndexEntry.Text}");
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("OnSaveClicked: ShelfIndex alanı gizli, atlanıyor");
            }

            // Shelf Stock alanı görünürse kontrol et
            if (ShelfStockGrid.IsVisible)
            {
                System.Diagnostics.Debug.WriteLine("OnSaveClicked: ShelfStock alanı görünür, kontrol ediliyor...");
                if (string.IsNullOrEmpty(ShelfStockEntry.Text) || Convert.ToInt32(ShelfStockEntry.Text) < 0)
                {
                    errorMessage += "* Raf Stoğu boş bırakılamaz veya sıfırdan küçük olamaz.\r\n";
                    System.Diagnostics.Debug.WriteLine("OnSaveClicked: ShelfStock validasyon hatası - boş veya negatif");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"OnSaveClicked: ShelfStock değeri: {ShelfStockEntry.Text}");
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("OnSaveClicked: ShelfStock alanı gizli, atlanıyor");
            }

            // Price alanı görünürse kontrol et
            if (PriceGrid.IsVisible)
            {
                System.Diagnostics.Debug.WriteLine("OnSaveClicked: Price alanı görünür, kontrol ediliyor...");
                if (string.IsNullOrEmpty(ShelfPrice1Entry.Text) || Convert.ToDecimal(ShelfPrice1Entry.Text) < 0)
                {
                    errorMessage += "* Fiyat 1. Kısım boş bırakılamaz veya sıfırdan küçük olamaz.\r\n";
                    System.Diagnostics.Debug.WriteLine("OnSaveClicked: Price1 validasyon hatası - boş veya negatif");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"OnSaveClicked: Price1 değeri: {ShelfPrice1Entry.Text}");
                }

                if (string.IsNullOrEmpty(ShelfPrice2Entry.Text))
                {
                    errorMessage += "* Fiyat 2. Kısım boş bırakılamaz.\r\n";
                    System.Diagnostics.Debug.WriteLine("OnSaveClicked: Price2 validasyon hatası - boş");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"OnSaveClicked: Price2 değeri: {ShelfPrice2Entry.Text}");
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("OnSaveClicked: Price alanı gizli, atlanıyor");
            }

            // Warehouse Stock alanı görünürse kontrol et
            if (WarehouseStockGrid.IsVisible)
            {
                System.Diagnostics.Debug.WriteLine("OnSaveClicked: WarehouseStock alanı görünür, kontrol ediliyor...");
                if (string.IsNullOrEmpty(WarehouseStockEntry.Text) || Convert.ToInt32(WarehouseStockEntry.Text) < 0)
                {
                    errorMessage += "* Depo Stoğu boş bırakılamaz veya sıfırdan küçük olamaz.\r\n";
                    System.Diagnostics.Debug.WriteLine("OnSaveClicked: WarehouseStock validasyon hatası - boş veya negatif");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"OnSaveClicked: WarehouseStock değeri: {WarehouseStockEntry.Text}");
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("OnSaveClicked: WarehouseStock alanı gizli, atlanıyor");
            }

            // Activity Type alanı görünürse kontrol et
            var selectedActivity = ActivityTypePicker.SelectedItem?.ToString();
            if (ActivityTypeGrid.IsVisible)
            {
                System.Diagnostics.Debug.WriteLine("OnSaveClicked: ActivityType alanı görünür, kontrol ediliyor...");
                System.Diagnostics.Debug.WriteLine($"OnSaveClicked: Seçilen aktivite tipi: {selectedActivity}");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("OnSaveClicked: ActivityType alanı gizli, atlanıyor");
            }

            // Discount Price alanı görünürse kontrol et
            if (DiscountPriceGrid.IsVisible)
            {
                System.Diagnostics.Debug.WriteLine("OnSaveClicked: DiscountPrice alanı görünür, kontrol ediliyor...");
                if (selectedActivity == "FIYAT INDIRIMI")
                {
                    if (string.IsNullOrEmpty(DiscountPrice1Entry.Text) || Convert.ToDecimal(DiscountPrice1Entry.Text) < 0)
                    {
                        errorMessage += "* İndirimli Fiyat 1. Kısım boş bırakılamaz veya sıfırdan küçük olamaz.\r\n";
                        System.Diagnostics.Debug.WriteLine("OnSaveClicked: DiscountPrice1 validasyon hatası - boş veya negatif");
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"OnSaveClicked: DiscountPrice1 değeri: {DiscountPrice1Entry.Text}");
                    }

                    if (string.IsNullOrEmpty(DiscountPrice2Entry.Text))
                    {
                        errorMessage += "* İndirimli Fiyat 2. Kısım boş bırakılamaz.\r\n";
                        System.Diagnostics.Debug.WriteLine("OnSaveClicked: DiscountPrice2 validasyon hatası - boş");
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"OnSaveClicked: DiscountPrice2 değeri: {DiscountPrice2Entry.Text}");
                    }
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("OnSaveClicked: DiscountPrice alanı gizli, atlanıyor");
            }

            // Notes alanı görünürse kontrol et (Notes zorunlu değil, sadece log)
            if (NotesGrid.IsVisible)
            {
                System.Diagnostics.Debug.WriteLine("OnSaveClicked: Notes alanı görünür");
                System.Diagnostics.Debug.WriteLine($"OnSaveClicked: Notes değeri: {NotesEditor.Text}");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("OnSaveClicked: Notes alanı gizli, atlanıyor");
            }

            System.Diagnostics.Debug.WriteLine($"OnSaveClicked: Validasyon tamamlandı. Hata sayısı: {(string.IsNullOrEmpty(errorMessage) ? 0 : errorMessage.Split('\r').Length - 1)}");

            // Show validation errors if any
            if (!string.IsNullOrEmpty(errorMessage))
            {
                System.Diagnostics.Debug.WriteLine($"OnSaveClicked: Validasyon hataları var, kullanıcıya gösteriliyor: {errorMessage}");
                HideLoadingOverlay();
                await DisplayAlert("Eksikleriniz var...", 
                    "Aşağıda listelenen sorunları düzelttikten sonra tekrar deneyin.\r\n\r\n" + errorMessage, 
                    "TAMAM");
                return;
            }

            try
            {
                System.Diagnostics.Debug.WriteLine("OnSaveClicked: Veritabanı güncelleme işlemi başlıyor...");
                
                // Calculate prices (equivalent to Android logic)
                decimal priceConcat = 0;
                decimal priceConcat2 = 0;

                // Sadece görünen alanlardan fiyat hesapla
                if (PriceGrid.IsVisible)
                {
                    priceConcat = Convert.ToDecimal(ShelfPrice1Entry.Text) + (Convert.ToDecimal(ShelfPrice2Entry.Text) / 100);
                    System.Diagnostics.Debug.WriteLine($"OnSaveClicked: Fiyat hesaplandı: {priceConcat}");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("OnSaveClicked: Price alanı gizli, fiyat hesaplaması atlandı");
                }

                if (DiscountPriceGrid.IsVisible && selectedActivity == "FIYAT INDIRIMI")
                {
                    priceConcat2 = Convert.ToDecimal(DiscountPrice1Entry.Text) + (Convert.ToDecimal(DiscountPrice2Entry.Text) / 100);
                    System.Diagnostics.Debug.WriteLine($"OnSaveClicked: İndirimli fiyat hesaplandı: {priceConcat2}");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("OnSaveClicked: İndirimli fiyat hesaplaması atlandı");
                }

                // Update database
                using var db = new SQLiteConnection(_dbPath);
                System.Diagnostics.Debug.WriteLine($"OnSaveClicked: Veritabanı bağlantısı açıldı: {_dbPath}");
                
                // Update the existing record - using dynamic SQL based on available properties
                // This approach handles cases where some properties might not exist in the entity
                var updateFields = new List<string>();
                var parameters = new List<object>();
                
                // Sadece görünen alanları güncelle
                if (ShelfIndexGrid.IsVisible)
                {
                    updateFields.Add("ShelfIndex=?");
                    parameters.Add(Convert.ToInt32(ShelfIndexEntry.Text));
                    System.Diagnostics.Debug.WriteLine($"OnSaveClicked: ShelfIndex eklendi: {ShelfIndexEntry.Text}");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("OnSaveClicked: ShelfIndex alanı gizli, güncelleme atlandı");
                }
                
                if (ShelfStockGrid.IsVisible)
                {
                    updateFields.Add("ShelfStock=?");
                    parameters.Add(Convert.ToInt32(ShelfStockEntry.Text));
                    System.Diagnostics.Debug.WriteLine($"OnSaveClicked: ShelfStock eklendi: {ShelfStockEntry.Text}");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("OnSaveClicked: ShelfStock alanı gizli, güncelleme atlandı");
                }
                
                if (PriceGrid.IsVisible)
                {
                    updateFields.Add("ShelfPrice=?");
                    parameters.Add(priceConcat);
                    System.Diagnostics.Debug.WriteLine($"OnSaveClicked: ShelfPrice eklendi: {priceConcat}");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("OnSaveClicked: Price alanı gizli, güncelleme atlandı");
                }
                
                if (FaceCountGrid.IsVisible)
                {
                    updateFields.Add("FaceCount=?");
                    parameters.Add(Convert.ToInt32(FaceCountEntry.Text));
                    System.Diagnostics.Debug.WriteLine($"OnSaveClicked: FaceCount eklendi: {FaceCountEntry.Text}");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("OnSaveClicked: FaceCount alanı gizli, güncelleme atlandı");
                }
                
                if (WarehouseStockGrid.IsVisible)
                {
                    updateFields.Add("WarehouseStock=?");
                    parameters.Add(Convert.ToInt32(WarehouseStockEntry.Text));
                    System.Diagnostics.Debug.WriteLine($"OnSaveClicked: WarehouseStock eklendi: {WarehouseStockEntry.Text}");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("OnSaveClicked: WarehouseStock alanı gizli, güncelleme atlandı");
                }
                
                if (NotesGrid.IsVisible)
                {
                    updateFields.Add("Notes=?");
                    parameters.Add(NotesEditor.Text ?? string.Empty);
                    System.Diagnostics.Debug.WriteLine($"OnSaveClicked: Notes eklendi: {NotesEditor.Text}");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("OnSaveClicked: Notes alanı gizli, güncelleme atlandı");
                }
                
                // Her zaman güncellenecek alanlar
                updateFields.Add("CreatedOn=?");
                parameters.Add(DateTime.Now);
                System.Diagnostics.Debug.WriteLine($"OnSaveClicked: CreatedOn eklendi: {DateTime.Now}");
                
                if (ActivityTypeGrid.IsVisible)
                {
                    updateFields.Add("ActivityTypeName=?");
                    parameters.Add(selectedActivity ?? string.Empty);
                    System.Diagnostics.Debug.WriteLine($"OnSaveClicked: ActivityTypeName eklendi: {selectedActivity}");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("OnSaveClicked: ActivityType alanı gizli, güncelleme atlandı");
                }
                
                if (DiscountPriceGrid.IsVisible && selectedActivity == "FIYAT INDIRIMI")
                {
                    updateFields.Add("DiscountedPrice=?");
                    parameters.Add(priceConcat2);
                    System.Diagnostics.Debug.WriteLine($"OnSaveClicked: DiscountedPrice eklendi: {priceConcat2}");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("OnSaveClicked: DiscountedPrice güncelleme atlandı");
                }
                
                // Add the WHERE clause parameter
                parameters.Add(_currentId);
                System.Diagnostics.Debug.WriteLine($"OnSaveClicked: WHERE Id parametresi eklendi: {_currentId}");
                
                var updateSql = $"UPDATE entMemberRouteDetail SET {string.Join(", ", updateFields)} WHERE Id=?";
                System.Diagnostics.Debug.WriteLine($"OnSaveClicked: SQL sorgusu hazırlandı: {updateSql}");
                System.Diagnostics.Debug.WriteLine($"OnSaveClicked: Parametre sayısı: {parameters.Count}");
                
                int affectedRows = db.Execute(updateSql, parameters.ToArray());
                System.Diagnostics.Debug.WriteLine($"OnSaveClicked: Veritabanı güncellemesi tamamlandı. Etkilenen satır sayısı: {affectedRows}");
                
                // Güncelleme sonrası veriyi tekrar oku ve debug et
                var updatedRouteDetail = db.Query<entMemberRouteDetail>("SELECT * FROM entMemberRouteDetail WHERE Id=?", _currentId);
                if (updatedRouteDetail.Count > 0)
                {
                    System.Diagnostics.Debug.WriteLine("=== Updated data after save ===");
                    System.Diagnostics.Debug.WriteLine($"FaceCount: {updatedRouteDetail[0].FaceCount}");
                    System.Diagnostics.Debug.WriteLine($"ShelfIndex: {updatedRouteDetail[0].ShelfIndex}");
                    System.Diagnostics.Debug.WriteLine($"ShelfStock: {updatedRouteDetail[0].ShelfStock}");
                    System.Diagnostics.Debug.WriteLine($"ShelfPrice: {updatedRouteDetail[0].ShelfPrice}");
                    System.Diagnostics.Debug.WriteLine($"WarehouseStock: {updatedRouteDetail[0].WarehouseStock}");
                    System.Diagnostics.Debug.WriteLine($"Notes: '{updatedRouteDetail[0].Notes}'");
                    System.Diagnostics.Debug.WriteLine($"ActivityTypeName: '{updatedRouteDetail[0].ActivityTypeName}'");
                    System.Diagnostics.Debug.WriteLine($"DiscountedPrice: {updatedRouteDetail[0].DiscountedPrice}");
                    System.Diagnostics.Debug.WriteLine($"CreatedOn: {updatedRouteDetail[0].CreatedOn}");
                }

                await DisplayAlert("Başarılı", "Ürün verileri kaydedildi.", "Tamam");
                System.Diagnostics.Debug.WriteLine("OnSaveClicked: Başarı mesajı gösterildi");
                // Artık otomatik çıkış yapmıyoruz, kullanıcı çıkış butonunu kullanacak
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error saving data");
                await DisplayAlert("Hata", "Veriler kaydedilirken hata oluştu.", "Tamam");
            }
            finally
            {
                HideLoadingOverlay();
            }
        }

        private async void OnExitClicked(object sender, EventArgs e)
        {
            // Loading overlay göster (hemen görünsün - metodun en başında, tüm işlemlerden önce)
            ShowLoadingOverlay();
            
            // Overlay'in render edilmesi için kısa bir delay
            await Task.Delay(100); // 100ms - overlay'in kesinlikle görünmesi için
            
            try
            {
                System.Diagnostics.Debug.WriteLine("OnExitClicked: Çıkış butonuna tıklandı");
                
                // Navigation flag'ini set et
                _isNavigatingAway = true;
                
                // Navigation stack'i kontrol et
                System.Diagnostics.Debug.WriteLine($"OnExitClicked: Current navigation stack count: {Shell.Current.Navigation.NavigationStack.Count}");
                
                // MemberRouteId ve StoreName'i kontrol et
                if (!_currentMemberRouteId.HasValue)
                {
                    System.Diagnostics.Debug.WriteLine("OnExitClicked: WARNING - _currentMemberRouteId is null, trying to get from _currentMemberRouteDetail");
                    
                    // Eğer _currentMemberRouteId set edilmemişse, _currentMemberRouteDetail'den al
                    if (_currentMemberRouteDetail != null)
                    {
                        _currentMemberRouteId = _currentMemberRouteDetail.MemberRouteId;
                        System.Diagnostics.Debug.WriteLine($"OnExitClicked: Got MemberRouteId from _currentMemberRouteDetail: {_currentMemberRouteId}");
                        
                        // StoreName'i de veritabanından al (async olarak, UI thread'i bloklamamak için)
                        // Overlay görünür kalacak çünkü Task.Run background thread'de çalışıyor
                        if (string.IsNullOrEmpty(_currentStoreName))
                        {
                            System.Diagnostics.Debug.WriteLine("OnExitClicked: Fetching StoreName from database (overlay should remain visible)...");
                            await Task.Run(() =>
                            {
                                using var db = new SQLiteConnection(_dbPath);
                                var route = db.Query<entMemberRoute>("SELECT * FROM entMemberRoute WHERE Id=?", _currentMemberRouteId.Value);
                                if (route.Count > 0 && route[0].RetailShopId.HasValue)
                                {
                                    var retailShop = db.Query<entRetailShop>("SELECT * FROM entRetailShop WHERE Id=?", route[0].RetailShopId.Value);
                                    if (retailShop.Count > 0)
                                    {
                                        _currentStoreName = retailShop[0].Name ?? string.Empty;
                                        System.Diagnostics.Debug.WriteLine($"OnExitClicked: Got StoreName from DB: {_currentStoreName}");
                                    }
                                }
                            });
                        }
                    }
                }
                
                // StoreProductsPage'e git - parametrelerle birlikte
                if (_currentMemberRouteId.HasValue)
                {
                    var parameters = new Dictionary<string, object>
                    {
                        ["entMemberRouteId"] = _currentMemberRouteId.Value.ToString(),
                        ["StoreName"] = _currentStoreName ?? "Mağaza"
                    };
                    
                    System.Diagnostics.Debug.WriteLine($"OnExitClicked: Navigating to StoreProductsPage with parameters:");
                    System.Diagnostics.Debug.WriteLine($"    entMemberRouteId: {parameters["entMemberRouteId"]}");
                    System.Diagnostics.Debug.WriteLine($"    StoreName: {parameters["StoreName"]}");
                    
                    await Shell.Current.GoToAsync("///StoreProductsPage", parameters);
                    // Overlay'i gizleme - StoreProductsPage açılana kadar görünür kalacak
                    // StoreProductsPage açıldığında kendi overlay'ini gösterecek ve bu sayfa arka planda kalacak
                    System.Diagnostics.Debug.WriteLine("OnExitClicked: Navigation completed, overlay will remain visible until StoreProductsPage loads");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("OnExitClicked: WARNING - Cannot get MemberRouteId, navigating without parameters");
                    // Fallback: parametre olmadan git (eski davranış)
                    await Shell.Current.GoToAsync("//StoreProductsPage");
                    // Overlay'i gizleme - StoreProductsPage açılana kadar görünür kalacak
                    System.Diagnostics.Debug.WriteLine("OnExitClicked: Navigation completed, overlay will remain visible until StoreProductsPage loads");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"OnExitClicked: Genel hata: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"OnExitClicked: Stack trace: {ex.StackTrace}");
                _logger?.LogError(ex, "Error navigating back");
                HideLoadingOverlay();
                await DisplayAlert("Hata", "Sayfa geçişi sırasında hata oluştu.", "Tamam");
            }
            finally
            {
                // Navigation flag'ini reset et
                _isNavigatingAway = false;
            }
        }

        // Geri butonu için override
        protected override bool OnBackButtonPressed()
        {
            System.Diagnostics.Debug.WriteLine("=== MerchDataEntryPage Hardware Back Button Pressed ===");
            
            // Loading overlay göster (hemen görünsün)
            ShowLoadingOverlay();
            
            // StoreProductsPage'e geri dön - parametrelerle birlikte
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    // Kısa bir delay ekle ki overlay render edilsin
                    await Task.Delay(50);
                    
                    if (_currentMemberRouteId.HasValue)
                    {
                        var parameters = new Dictionary<string, object>
                        {
                            ["entMemberRouteId"] = _currentMemberRouteId.Value.ToString(),
                            ["StoreName"] = _currentStoreName ?? "Mağaza"
                        };
                        
                        System.Diagnostics.Debug.WriteLine($"OnBackButtonPressed: Navigating to StoreProductsPage with parameters:");
                        System.Diagnostics.Debug.WriteLine($"    entMemberRouteId: {parameters["entMemberRouteId"]}");
                        System.Diagnostics.Debug.WriteLine($"    StoreName: {parameters["StoreName"]}");
                        
                        await Shell.Current.GoToAsync("///StoreProductsPage", parameters);
                        // Navigation tamamlandı, loading overlay'i gizle (hedef sayfa kendi overlay'ini gösterecek)
                        HideLoadingOverlay();
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("OnBackButtonPressed: WARNING - Cannot get MemberRouteId, navigating without parameters");
                        // Fallback: parametre olmadan git
                        await Shell.Current.GoToAsync("///StoreProductsPage");
                        // Navigation tamamlandı, loading overlay'i gizle (hedef sayfa kendi overlay'ini gösterecek)
                        HideLoadingOverlay();
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"OnBackButtonPressed: Error navigating back: {ex.Message}");
                    _logger?.LogError(ex, "Error navigating back from MerchDataEntryPage");
                    HideLoadingOverlay();
                    // Fallback: StoresPage'e git
                    try
                    {
                        await Shell.Current.GoToAsync("///StoresPage");
                    }
                    catch (Exception fallbackEx)
                    {
                        System.Diagnostics.Debug.WriteLine($"OnBackButtonPressed: Fallback navigation error: {fallbackEx.Message}");
                    }
                }
            });
            
            return true; // Event'i handle et
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
} 