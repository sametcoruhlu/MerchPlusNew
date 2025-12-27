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
    [QueryProperty(nameof(IsCompleted), "isCompleted")]
    public partial class MerchDataEntryPage : ContentPage, INotifyPropertyChanged
    {
        private readonly ILogger<MerchDataEntryPage>? _logger;
        private readonly IFieldVisibilityService _fieldVisibilityService;
        private readonly string _dbPath;
        private int _currentId = 0;
        private entMemberRouteDetail? _currentMemberRouteDetail;
        private FieldVisibilityModel? _fieldVisibility;

        private string _entMemberRouteDetailId = string.Empty;
        private string _isCompleted = "false";
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

        public string IsCompleted
        {
            get => _isCompleted;
            set
            {
                _isCompleted = value ?? "false";
                OnPropertyChanged();
                UpdateSaveButtonText();
            }
        }

        // Parameterless constructor for Shell navigation
        public MerchDataEntryPage() : this(null, null)
        {
        }

        public MerchDataEntryPage(ILogger<MerchDataEntryPage>? logger, IFieldVisibilityService? fieldVisibilityService)
        {
            InitializeComponent();
            _logger = logger;
            _fieldVisibilityService = fieldVisibilityService ?? new FieldVisibilityService(new HttpClient());
            
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
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            // Sayfa başlangıçta görünür olsun (siyah ekranı önlemek için)
            this.Opacity = 1;
            
            try
            {
                // XAML elementlerini initialize et
                _loadingOverlay = FindByName("LoadingOverlay") as Grid;
                _mainContent = FindByName("MainContent") as ScrollView;
                
                // Loading overlay'i hemen göster (sayfa render edilmeden önce)
                if (_loadingOverlay != null)
                {
                    _loadingOverlay.IsVisible = true;
                }
                
                // Veri yükleme işlemini başlat
                await LoadProductDataAsync();
                
                // Buton metnini güncelle (parametreler set edildikten sonra)
                UpdateSaveButtonText();
            }
            catch (Exception ex)
            {
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
                
                // Buton metnini güncelle
                UpdateSaveButtonText();
            });
        }

        private void UpdateSaveButtonText()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                var saveButton = FindByName("SaveButton") as Button;
                if (saveButton != null)
                {
                    // isCompleted parametresini kontrol et
                    bool isCompleted = bool.TryParse(_isCompleted, out bool completed) && completed;
                    saveButton.Text = isCompleted ? "GÜNCELLE" : "KAYDET";
                    System.Diagnostics.Debug.WriteLine($"SaveButton text updated to: {saveButton.Text} (isCompleted: {isCompleted})");
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

        private async Task LoadFieldVisibilityAsync(int? customerId = null)
        {
            try
            {
                if (_fieldVisibilityService == null)
                {
                    ShowAllFields();
                    HideLoadingOverlay();
                    return;
                }

                // CustomerId belirtilmemişse al
                int finalCustomerId = customerId ?? await GetCustomerIdAsync();
                
                if (finalCustomerId <= 0)
                {
                    ShowAllFields();
                    HideLoadingOverlay();
                    return;
                }

                // OPTİMİZE: Field visibility'yi background'da yükle, UI thread'i bloklama
                _fieldVisibility = await Task.Run(async () => 
                    await _fieldVisibilityService.GetFieldVisibilityAsync(finalCustomerId));
                
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    ApplyFieldVisibility();
                    HideLoadingOverlay();
                });
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error loading field visibility");
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    ShowAllFields();
                    HideLoadingOverlay();
                });
            }
        }

        private async Task<int> GetCustomerIdAsync()
        {
            try
            {
                // 1. Önce GlobalVariables.CurrentCustomerId'yi kontrol et (en yaygın ve hızlı yöntem)
                if (!string.IsNullOrEmpty(GlobalVariables.CurrentCustomerId))
                {
                    if (int.TryParse(GlobalVariables.CurrentCustomerId, out int globalCustomerId))
                    {
                        return globalCustomerId;
                    }
                }

                // 2. OPTİMİZE: Route'dan CustomerId al (zaten LoadProductDataAsync'de yüklenmiş)
                // Reflection kullanmak yerine direkt property erişimi (daha hızlı)
                // Eğer route yüklenmişse ondan al, yoksa GlobalVariables'dan
                if (GlobalVariables.CurrentMember != null && !string.IsNullOrEmpty(GlobalVariables.CurrentMember.Id))
                {
                    if (int.TryParse(GlobalVariables.CurrentMember.Id, out int memberCustomerId))
                    {
                        return memberCustomerId;
                    }
                }

                // 3. UserProfileService'den al (cache'lenmiş)
                var userProfileService = UserProfileService.Instance;
                var currentUser = userProfileService?.GetCurrentUser();
                if (currentUser != null && !string.IsNullOrEmpty(currentUser.Id))
                {
                    if (int.TryParse(currentUser.Id, out int userCustomerId))
                    {
                        return userCustomerId;
                    }
                }

                // Son çare: Test amaçlı 1
                return 1;
            }
            catch (Exception ex)
            {
                _logger?.LogWarning(ex, "Error getting CustomerId");
                return 1;
            }
        }

        private void ApplyFieldVisibility()
        {
            // Eğer servisten boş cevap gelirse (null) tüm alanları göster
            if (_fieldVisibility == null)
            {
                ShowAllFields();
                return;
            }
            
            // Servisten gelen veriye göre alanları göster/gizle
            FaceCountGrid.IsVisible = _fieldVisibility.FaceCount;
            ShelfIndexGrid.IsVisible = _fieldVisibility.ShelfIndex;
            ShelfStockGrid.IsVisible = _fieldVisibility.ShelfStock;
            PriceGrid.IsVisible = _fieldVisibility.Price;
            WarehouseStockGrid.IsVisible = _fieldVisibility.WarehouseStock;
            ActivityTypeGrid.IsVisible = _fieldVisibility.ActivityType;
            DiscountPriceGrid.IsVisible = _fieldVisibility.DiscountPrice;
            NotesGrid.IsVisible = _fieldVisibility.Notes;
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



        private async Task LoadProductDataAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(_entMemberRouteDetailId)) return;

                _currentId = Convert.ToInt32(_entMemberRouteDetailId);

                // OPTİMİZE: Tüm veritabanı işlemlerini background thread'de yap
                var (routeDetail, route, retailShop, product, customerId) = await Task.Run(() =>
                {
                    using var db = new SQLiteConnection(_dbPath);
                    
                    // 1. Route detail'i yükle
                    var routeDetailList = db.Query<entMemberRouteDetail>("SELECT * FROM entMemberRouteDetail WHERE Id=?", _currentId);
                    
                    if (routeDetailList.Count == 0)
                    {
                        return (null, null, null, null, 0);
                    }
                    
                    var routeDetailItem = routeDetailList[0];
                    
                    // 2. Route'u yükle
                    entMemberRoute? routeItem = null;
                    if (routeDetailItem.MemberRouteId.HasValue)
                    {
                        var routeList = db.Query<entMemberRoute>("SELECT * FROM entMemberRoute WHERE Id=?", routeDetailItem.MemberRouteId.Value);
                        if (routeList.Count > 0)
                        {
                            routeItem = routeList[0];
                        }
                    }
                    
                    // 3. Retail shop'u yükle
                    entRetailShop? retailShopItem = null;
                    if (routeItem?.RetailShopId.HasValue == true)
                    {
                        var retailShopList = db.Query<entRetailShop>("SELECT * FROM entRetailShop WHERE Id=?", routeItem.RetailShopId.Value);
                        if (retailShopList.Count > 0)
                        {
                            retailShopItem = retailShopList[0];
                        }
                    }
                    
                    // 4. Product'u yükle
                    entCustomerProduct? productItem = null;
                    if (routeDetailItem.CustomerProductId.HasValue)
                    {
                        var productList = db.Query<entCustomerProduct>("SELECT * FROM entCustomerProduct WHERE Id=?", routeDetailItem.CustomerProductId.Value);
                        if (productList.Count > 0)
                        {
                            productItem = productList[0];
                        }
                    }
                    
                    // 5. CustomerId'yi belirle (önce route'dan, sonra GlobalVariables'dan)
                    int customerIdValue = 0;
                    if (routeItem?.CustomerId.HasValue == true)
                    {
                        customerIdValue = routeItem.CustomerId.Value;
                    }
                    else if (!string.IsNullOrEmpty(GlobalVariables.CurrentCustomerId) && int.TryParse(GlobalVariables.CurrentCustomerId, out int globalCustomerId))
                    {
                        customerIdValue = globalCustomerId;
                    }
                    else if (GlobalVariables.CurrentMember != null && !string.IsNullOrEmpty(GlobalVariables.CurrentMember.Id) && int.TryParse(GlobalVariables.CurrentMember.Id, out int memberCustomerId))
                    {
                        customerIdValue = memberCustomerId;
                    }
                    
                    return (routeDetailItem, routeItem, retailShopItem, productItem, customerIdValue);
                }).ConfigureAwait(false);
                
                // Veri kontrolü
                if (routeDetail == null)
                {
                    await DisplayAlert("Hata", "Ürün bilgisi bulunamadı.", "Tamam");
                    await Shell.Current.GoToAsync("..");
                    return;
                }
                
                if (route == null)
                {
                    await DisplayAlert("Hata", "Mağaza rotası bulunamadı.", "Tamam");
                    await Shell.Current.GoToAsync("..");
                    return;
                }

                _currentMemberRouteDetail = routeDetail;
                _currentMemberRouteId = routeDetail.MemberRouteId ?? 0;
                
                // UI güncellemelerini main thread'de yap
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    if (retailShop != null)
                    {
                        RetailShopNameLabel.Text = retailShop.Name;
                        _currentStoreName = retailShop.Name ?? string.Empty;
                    }
                    
                    if (product != null)
                    {
                        CustomerProductNameLabel.Text = product.Name;
                    }
                });

                // Load existing data - using the actual properties from entMemberRouteDetail
                // Note: These properties might have different names in the actual entity
                // We'll use the properties that exist in the entMemberRouteDetail class
                
                // Initialize all fields as empty so placeholders are shown
                // Only set values if existing data is found
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
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

                    // Load existing data from entMemberRouteDetail
                    try
                    {
                        if (_currentMemberRouteDetail.FaceCount.HasValue)
                        {
                            FaceCountEntry.Text = _currentMemberRouteDetail.FaceCount.Value.ToString();
                        }
                        
                        if (_currentMemberRouteDetail.ShelfIndex.HasValue)
                        {
                            ShelfIndexEntry.Text = _currentMemberRouteDetail.ShelfIndex.Value.ToString();
                        }
                        
                        if (_currentMemberRouteDetail.ShelfStock.HasValue)
                        {
                            ShelfStockEntry.Text = _currentMemberRouteDetail.ShelfStock.Value.ToString();
                        }
                        
                        if (_currentMemberRouteDetail.ShelfPrice.HasValue)
                        {
                            var priceValue = _currentMemberRouteDetail.ShelfPrice.Value;
                            ShelfPrice1Entry.Text = Convert.ToString(Math.Truncate(priceValue));
                            ShelfPrice2Entry.Text = Convert.ToInt32((priceValue - Math.Truncate(priceValue)) * 100).ToString("00");
                        }
                        
                        if (_currentMemberRouteDetail.WarehouseStock.HasValue)
                        {
                            WarehouseStockEntry.Text = _currentMemberRouteDetail.WarehouseStock.Value.ToString();
                        }
                        
                        if (!string.IsNullOrEmpty(_currentMemberRouteDetail.Notes))
                        {
                            NotesEditor.Text = _currentMemberRouteDetail.Notes;
                        }
                        
                        if (!string.IsNullOrEmpty(_currentMemberRouteDetail.ActivityTypeName))
                        {
                            var activityTypes = ActivityTypePicker.ItemsSource as List<string>;
                            var index = activityTypes?.IndexOf(_currentMemberRouteDetail.ActivityTypeName) ?? -1;
                            if (index >= 0)
                            {
                                ActivityTypePicker.SelectedIndex = index;
                            }
                        }
                        
                        if (_currentMemberRouteDetail.DiscountedPrice.HasValue)
                        {
                            var discountValue = _currentMemberRouteDetail.DiscountedPrice.Value;
                            DiscountPrice1Entry.Text = Convert.ToString(Math.Truncate(discountValue));
                            DiscountPrice2Entry.Text = Convert.ToInt32((discountValue - Math.Truncate(discountValue)) * 100).ToString("00");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger?.LogWarning(ex, "Some properties might not exist in entMemberRouteDetail");
                    }
                });

                // Product data yüklendikten sonra field visibility'yi yükle
                await LoadFieldVisibilityAsync(customerId > 0 ? customerId : null);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error loading product data");
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    await DisplayAlert("Hata", "Ürün bilgileri yüklenirken hata oluştu.", "Tamam");
                    HideLoadingOverlay();
                });
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

        // Placeholder değerlerini gerçek değerler olarak kullanmak için helper metod
        private string GetValueOrPlaceholder(Entry entry, string placeholder)
        {
            if (string.IsNullOrWhiteSpace(entry.Text))
            {
                System.Diagnostics.Debug.WriteLine($"GetValueOrPlaceholder: {entry.GetType().Name} boş, placeholder kullanılıyor: {placeholder}");
                return placeholder;
            }
            return entry.Text;
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
                var faceCountValue = GetValueOrPlaceholder(FaceCountEntry, "0");
                if (Convert.ToInt32(faceCountValue) < 0)
                {
                    errorMessage += "* Önyüz sıfırdan küçük olamaz.\r\n";
                    System.Diagnostics.Debug.WriteLine("OnSaveClicked: FaceCount validasyon hatası - negatif");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"OnSaveClicked: FaceCount değeri: {faceCountValue}");
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
                var shelfIndexValue = GetValueOrPlaceholder(ShelfIndexEntry, "0");
                if (Convert.ToInt32(shelfIndexValue) < 0)
                {
                    errorMessage += "* Raf Sırası sıfırdan küçük olamaz.\r\n";
                    System.Diagnostics.Debug.WriteLine("OnSaveClicked: ShelfIndex validasyon hatası - negatif");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"OnSaveClicked: ShelfIndex değeri: {shelfIndexValue}");
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
                var shelfStockValue = GetValueOrPlaceholder(ShelfStockEntry, "0");
                if (Convert.ToInt32(shelfStockValue) < 0)
                {
                    errorMessage += "* Raf Stoğu sıfırdan küçük olamaz.\r\n";
                    System.Diagnostics.Debug.WriteLine("OnSaveClicked: ShelfStock validasyon hatası - negatif");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"OnSaveClicked: ShelfStock değeri: {shelfStockValue}");
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
                var price1Value = GetValueOrPlaceholder(ShelfPrice1Entry, "0");
                if (Convert.ToDecimal(price1Value) < 0)
                {
                    errorMessage += "* Fiyat 1. Kısım sıfırdan küçük olamaz.\r\n";
                    System.Diagnostics.Debug.WriteLine("OnSaveClicked: Price1 validasyon hatası - negatif");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"OnSaveClicked: Price1 değeri: {price1Value}");
                }

                var price2Value = GetValueOrPlaceholder(ShelfPrice2Entry, "00");
                System.Diagnostics.Debug.WriteLine($"OnSaveClicked: Price2 değeri: {price2Value}");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("OnSaveClicked: Price alanı gizli, atlanıyor");
            }

            // Warehouse Stock alanı görünürse kontrol et
            if (WarehouseStockGrid.IsVisible)
            {
                System.Diagnostics.Debug.WriteLine("OnSaveClicked: WarehouseStock alanı görünür, kontrol ediliyor...");
                var warehouseStockValue = GetValueOrPlaceholder(WarehouseStockEntry, "0");
                if (Convert.ToInt32(warehouseStockValue) < 0)
                {
                    errorMessage += "* Depo Stoğu sıfırdan küçük olamaz.\r\n";
                    System.Diagnostics.Debug.WriteLine("OnSaveClicked: WarehouseStock validasyon hatası - negatif");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"OnSaveClicked: WarehouseStock değeri: {warehouseStockValue}");
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
                    var discountPrice1Value = GetValueOrPlaceholder(DiscountPrice1Entry, "0");
                    if (Convert.ToDecimal(discountPrice1Value) < 0)
                    {
                        errorMessage += "* İndirimli Fiyat 1. Kısım sıfırdan küçük olamaz.\r\n";
                        System.Diagnostics.Debug.WriteLine("OnSaveClicked: DiscountPrice1 validasyon hatası - negatif");
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"OnSaveClicked: DiscountPrice1 değeri: {discountPrice1Value}");
                    }

                    var discountPrice2Value = GetValueOrPlaceholder(DiscountPrice2Entry, "00");
                    System.Diagnostics.Debug.WriteLine($"OnSaveClicked: DiscountPrice2 değeri: {discountPrice2Value}");
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

                // Sadece görünen alanlardan fiyat hesapla (placeholder değerlerini kullan)
                if (PriceGrid.IsVisible)
                {
                    var price1Value = GetValueOrPlaceholder(ShelfPrice1Entry, "0");
                    var price2Value = GetValueOrPlaceholder(ShelfPrice2Entry, "00");
                    priceConcat = Convert.ToDecimal(price1Value) + (Convert.ToDecimal(price2Value) / 100);
                    System.Diagnostics.Debug.WriteLine($"OnSaveClicked: Fiyat hesaplandı: {priceConcat} (Price1: {price1Value}, Price2: {price2Value})");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("OnSaveClicked: Price alanı gizli, fiyat hesaplaması atlandı");
                }

                if (DiscountPriceGrid.IsVisible && selectedActivity == "FIYAT INDIRIMI")
                {
                    var discountPrice1Value = GetValueOrPlaceholder(DiscountPrice1Entry, "0");
                    var discountPrice2Value = GetValueOrPlaceholder(DiscountPrice2Entry, "00");
                    priceConcat2 = Convert.ToDecimal(discountPrice1Value) + (Convert.ToDecimal(discountPrice2Value) / 100);
                    System.Diagnostics.Debug.WriteLine($"OnSaveClicked: İndirimli fiyat hesaplandı: {priceConcat2} (DiscountPrice1: {discountPrice1Value}, DiscountPrice2: {discountPrice2Value})");
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
                
                // Sadece görünen alanları güncelle (placeholder değerlerini kullan)
                if (ShelfIndexGrid.IsVisible)
                {
                    var shelfIndexValue = GetValueOrPlaceholder(ShelfIndexEntry, "0");
                    updateFields.Add("ShelfIndex=?");
                    parameters.Add(Convert.ToInt32(shelfIndexValue));
                    System.Diagnostics.Debug.WriteLine($"OnSaveClicked: ShelfIndex eklendi: {shelfIndexValue}");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("OnSaveClicked: ShelfIndex alanı gizli, güncelleme atlandı");
                }
                
                if (ShelfStockGrid.IsVisible)
                {
                    var shelfStockValue = GetValueOrPlaceholder(ShelfStockEntry, "0");
                    updateFields.Add("ShelfStock=?");
                    parameters.Add(Convert.ToInt32(shelfStockValue));
                    System.Diagnostics.Debug.WriteLine($"OnSaveClicked: ShelfStock eklendi: {shelfStockValue}");
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
                    var faceCountValue = GetValueOrPlaceholder(FaceCountEntry, "0");
                    updateFields.Add("FaceCount=?");
                    parameters.Add(Convert.ToInt32(faceCountValue));
                    System.Diagnostics.Debug.WriteLine($"OnSaveClicked: FaceCount eklendi: {faceCountValue}");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("OnSaveClicked: FaceCount alanı gizli, güncelleme atlandı");
                }
                
                if (WarehouseStockGrid.IsVisible)
                {
                    var warehouseStockValue = GetValueOrPlaceholder(WarehouseStockEntry, "0");
                    updateFields.Add("WarehouseStock=?");
                    parameters.Add(Convert.ToInt32(warehouseStockValue));
                    System.Diagnostics.Debug.WriteLine($"OnSaveClicked: WarehouseStock eklendi: {warehouseStockValue}");
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

                HideLoadingOverlay();
                await DisplayAlert("Başarılı", "Ürün verileri kaydedildi.", "Tamam");
                System.Diagnostics.Debug.WriteLine("OnSaveClicked: Başarı mesajı gösterildi");
                
                // CommonFooter badge'ini güncelle (eğer AboutMePage görünürse)
                Controls.CommonFooter.RefreshBadge();
                
                // Tamam butonuna tıklandığında ürünler sayfasına geri dön
                await NavigateBackToStoreProductsPage();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error saving data");
                HideLoadingOverlay();
                await DisplayAlert("Hata", "Veriler kaydedilirken hata oluştu.", "Tamam");
            }
        }

        private async Task NavigateBackToStoreProductsPage()
        {
            // Loading overlay göster (hemen görünsün - metodun en başında, tüm işlemlerden önce)
            ShowLoadingOverlay();
            
            // Overlay'in render edilmesi için kısa bir delay
            await Task.Delay(100); // 100ms - overlay'in kesinlikle görünmesi için
            
            try
            {
                System.Diagnostics.Debug.WriteLine("NavigateBackToStoreProductsPage: Ürünler sayfasına geri dönülüyor");
                
                // Navigation flag'ini set et
                _isNavigatingAway = true;
                
                // Navigation stack'i kontrol et
                System.Diagnostics.Debug.WriteLine($"NavigateBackToStoreProductsPage: Current navigation stack count: {Shell.Current.Navigation.NavigationStack.Count}");
                
                // MemberRouteId ve StoreName'i kontrol et
                if (!_currentMemberRouteId.HasValue)
                {
                    System.Diagnostics.Debug.WriteLine("NavigateBackToStoreProductsPage: WARNING - _currentMemberRouteId is null, trying to get from _currentMemberRouteDetail");
                    
                    // Eğer _currentMemberRouteId set edilmemişse, _currentMemberRouteDetail'den al
                    if (_currentMemberRouteDetail != null)
                    {
                        _currentMemberRouteId = _currentMemberRouteDetail.MemberRouteId;
                        System.Diagnostics.Debug.WriteLine($"NavigateBackToStoreProductsPage: Got MemberRouteId from _currentMemberRouteDetail: {_currentMemberRouteId}");
                        
                        // StoreName'i de veritabanından al (async olarak, UI thread'i bloklamamak için)
                        // Overlay görünür kalacak çünkü Task.Run background thread'de çalışıyor
                        if (string.IsNullOrEmpty(_currentStoreName))
                        {
                            System.Diagnostics.Debug.WriteLine("NavigateBackToStoreProductsPage: Fetching StoreName from database (overlay should remain visible)...");
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
                                        System.Diagnostics.Debug.WriteLine($"NavigateBackToStoreProductsPage: Got StoreName from DB: {_currentStoreName}");
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
                    
                    System.Diagnostics.Debug.WriteLine($"NavigateBackToStoreProductsPage: Navigating to StoreProductsPage with parameters:");
                    System.Diagnostics.Debug.WriteLine($"    entMemberRouteId: {parameters["entMemberRouteId"]}");
                    System.Diagnostics.Debug.WriteLine($"    StoreName: {parameters["StoreName"]}");
                    
                    await Shell.Current.GoToAsync("///StoreProductsPage", parameters);
                    // Overlay'i gizleme - StoreProductsPage açılana kadar görünür kalacak
                    // StoreProductsPage açıldığında kendi overlay'ini gösterecek ve bu sayfa arka planda kalacak
                    System.Diagnostics.Debug.WriteLine("NavigateBackToStoreProductsPage: Navigation completed, overlay will remain visible until StoreProductsPage loads");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("NavigateBackToStoreProductsPage: WARNING - Cannot get MemberRouteId, navigating without parameters");
                    // Fallback: parametre olmadan git (eski davranış)
                    await Shell.Current.GoToAsync("//StoreProductsPage");
                    // Overlay'i gizleme - StoreProductsPage açılana kadar görünür kalacak
                    System.Diagnostics.Debug.WriteLine("NavigateBackToStoreProductsPage: Navigation completed, overlay will remain visible until StoreProductsPage loads");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"NavigateBackToStoreProductsPage: Genel hata: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"NavigateBackToStoreProductsPage: Stack trace: {ex.StackTrace}");
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

        private async void OnExitClicked(object sender, EventArgs e)
        {
            // Bu metod artık kullanılmıyor, çıkış butonu kaldırıldı
            // NavigateBackToStoreProductsPage metodunu kullanıyoruz
            await NavigateBackToStoreProductsPage();
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