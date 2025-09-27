using SQLite;
using Microsoft.Extensions.Logging;
using entMerchPlus;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Graphics;
using System.Linq;

namespace MerchPlusMAUI.Views
{
    [QueryProperty(nameof(EntMemberRouteId), "entMemberRouteId")]
    [QueryProperty(nameof(StoreName), "StoreName")]
    public partial class StoreProductsPage : ContentPage, INotifyPropertyChanged
    {
        #region Fields
        private readonly ILogger<StoreProductsPage> _logger;
        private readonly string _dbPath;
        private bool _isLoading = false;
        private bool _isInitialized = false;
        #endregion

        #region Properties - Android Intent.GetStringExtra karşılıkları
        private string _entMemberRouteId = string.Empty;
        public string EntMemberRouteId
        {
            get => _entMemberRouteId;
            set
            {
                Console.WriteLine($"=== EntMemberRouteId Property Set ===");
                Console.WriteLine($"Old value: '{_entMemberRouteId}'");
                Console.WriteLine($"New value: '{value}'");
                _entMemberRouteId = value;
                OnPropertyChanged();
            }
        }

        private string _storeName = string.Empty;
        public string StoreName
        {
            get => _storeName;
            set
            {
                Console.WriteLine($"=== StoreName Property Set ===");
                Console.WriteLine($"Old value: '{_storeName}'");
                Console.WriteLine($"New value: '{value}'");
                _storeName = value;
                OnPropertyChanged();
                Title = $"{_storeName} - Ürün Listesi";
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        private string _productCountText = string.Empty;
        public string ProductCountText
        {
            get => _productCountText;
            set
            {
                _productCountText = value;
                OnPropertyChanged();
            }
        }

        private bool _hasProducts = false;
        public bool HasProducts
        {
            get => _hasProducts;
            set
            {
                _hasProducts = value;
                OnPropertyChanged();
            }
        }

        private bool _isSearchActive = false;
        public bool IsSearchActive
        {
            get => _isSearchActive;
            set
            {
                _isSearchActive = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Collections - Android ListView ve Adapter karşılıkları
        public ObservableCollection<entMemberRouteDetail> MemberRouteDetails { get; private set; } = new();
        public ObservableCollection<ProductViewModel> Products { get; private set; } = new();
        public ObservableCollection<ProductViewModel> FilteredProducts { get; private set; } = new();
        #endregion

        #region Commands
        public ICommand SearchCommand { get; private set; }
        public ICommand TakePhotoCommand { get; private set; }
        public ICommand SalesOrderCommand { get; private set; }
        public ICommand ExpirationDateCommand { get; private set; }
        public ICommand ExitShopCommand { get; private set; }
        public ICommand ProductSelectedCommand { get; private set; }
        #endregion

        #region Constructor - Android OnCreate karşılığı
        public StoreProductsPage(ILogger<StoreProductsPage> logger)
        {
            InitializeComponent();
            _logger = logger;
            _dbPath = GlobalVariables.DatabasePath;
            
            InitializeCommands();
            BindingContext = this;
            
            Console.WriteLine("StoreProductsPage constructor completed");
            _logger.LogInformation("StoreProductsPage initialized");
        }

        // Alternative constructor for direct parameter passing
        public StoreProductsPage(ILogger<StoreProductsPage> logger, string entMemberRouteId, string storeName) 
            : this(logger)
        {
            Console.WriteLine($"Alternative constructor called with EntMemberRouteId: {entMemberRouteId}, StoreName: {storeName}");
            EntMemberRouteId = entMemberRouteId;
            StoreName = storeName;
        }

        private void InitializeCommands()
        {
            SearchCommand = new Command<string>(OnSearchExecuted);
            TakePhotoCommand = new Command(async () => await OnTakePhotoClickedAsync());
            SalesOrderCommand = new Command(async () => await OnSalesOrderClickedAsync());
            ExpirationDateCommand = new Command(async () => await OnExpirationDateClickedAsync());
            ExitShopCommand = new Command(async () => await OnExitShopClickedAsync());
            ProductSelectedCommand = new Command<ProductViewModel>(async (product) => await OnProductSelected(product));
        }
        #endregion

        #region Lifecycle Methods - Android OnResume karşılığı
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            // Sayfa animasyonu için başlangıçta görünmez yap
            this.Opacity = 0;
            
            Console.WriteLine("=== StoreProductsPage OnAppearing ===");
            System.Diagnostics.Debug.WriteLine("=== StoreProductsPage OnAppearing ===");
            
            if (!_isInitialized)
            {
                Console.WriteLine("Waiting for navigation parameters...");
                
                // Navigation parametrelerinin set edilmesini bekle
                int attempts = 0;
                while (string.IsNullOrEmpty(EntMemberRouteId) && attempts < 10)
                {
                    await Task.Delay(50); // 50ms'ye düşür (daha hızlı)
                    attempts++;
                    Console.WriteLine($"Waiting attempt {attempts}/10 - EntMemberRouteId: '{EntMemberRouteId}'");
                }
                
                if (!string.IsNullOrEmpty(EntMemberRouteId))
                {
                    Console.WriteLine($"Parameters received - EntMemberRouteId: {EntMemberRouteId}, StoreName: {StoreName}");
                    Console.WriteLine("Initializing StoreProductsPage...");
                    
                    // Güzel bir fade-in animasyonu
                    _ = Task.Run(async () =>
                    {
                        await Task.Delay(100); // Kısa bir gecikme
                        await MainThread.InvokeOnMainThreadAsync(async () =>
                        {
                            await this.FadeTo(1, 300, Easing.CubicOut); // Smooth fade-in
                        });
                    });
                    
                    await InitializePageAsync();
                    _isInitialized = true;
                }
                else
                {
                    Console.WriteLine("ERROR: Navigation parameters not received after waiting!");
                    await this.FadeTo(1, 200); // Hata durumunda da görünür yap
                    await ShowErrorAndNavigateBack("Mağaza bilgileri alınamadı. Lütfen tekrar deneyin.");
                }
            }
            else
            {
                // Zaten initialize edilmiş, sadece fade-in yap
                await this.FadeTo(1, 200, Easing.CubicOut);
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Console.WriteLine("=== StoreProductsPage OnDisappearing ===");
            System.Diagnostics.Debug.WriteLine("=== StoreProductsPage OnDisappearing ===");
            
            // Sayfa kapanırken fade-out
            _ = this.FadeTo(0, 150, Easing.CubicIn);
        }

        // Geri butonu için override
        protected override bool OnBackButtonPressed()
        {
            Console.WriteLine("=== Hardware Back Button Pressed ===");
            System.Diagnostics.Debug.WriteLine("=== Hardware Back Button Pressed ===");
            
            // StoreReportPage'e geri dön
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    // StoreReportPage'e absolute routing ile git
                    var parameters = new Dictionary<string, object>
                    {
                        ["entMemberRouteId"] = EntMemberRouteId,
                        ["StoreName"] = StoreName
                    };
                    
                    await Shell.Current.GoToAsync("///StoreReportPage", parameters);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in back navigation: {ex.Message}");
                    // Fallback: StoresPage'e git
                    try
                    {
                        await Shell.Current.GoToAsync("///StoresPage");
                    }
                    catch (Exception fallbackEx)
                    {
                        Console.WriteLine($"Fallback navigation error: {fallbackEx.Message}");
                    }
                }
            });
            
            return true; // Event'i handle et
        }

        private async Task InitializePageAsync()
        {
            try
            {
                Console.WriteLine($"=== InitializePageAsync Started ===");
                Console.WriteLine($"EntMemberRouteId: '{EntMemberRouteId}'");
                Console.WriteLine($"StoreName: '{StoreName}'");
                
                _logger.LogInformation($"Initializing StoreProductsPage - EntMemberRouteId: {EntMemberRouteId}");

                // GlobalVariables.CurrentMemberRoute'u set et (sadece gerekirse)
                if (!string.IsNullOrEmpty(EntMemberRouteId) && int.TryParse(EntMemberRouteId, out int memberRouteId))
                {
                    if (GlobalVariables.CurrentMemberRoute == null)
                    {
                        GlobalVariables.CurrentMemberRoute = new entMemberRoute { Id = memberRouteId };
                        Console.WriteLine($"GlobalVariables.CurrentMemberRoute created with Id: {memberRouteId}");
                    }
                    else if (GlobalVariables.CurrentMemberRoute.Id != memberRouteId)
                    {
                        GlobalVariables.CurrentMemberRoute.Id = memberRouteId;
                        Console.WriteLine($"GlobalVariables.CurrentMemberRoute.Id updated to: {memberRouteId}");
                    }
                }

                Console.WriteLine("Starting database operations...");
                // Android OnResume içindeki database işlemleri
                await LoadProductsFromDatabaseAsync();
                Console.WriteLine("=== InitializePageAsync Completed ===");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR in InitializePageAsync: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                _logger.LogError(ex, "Error initializing StoreProductsPage");
                await ShowErrorAndNavigateBack($"Sayfa yüklenirken hata oluştu: {ex.Message}");
            }
        }
        #endregion

        #region Database Operations - Android OnResume database logic
        private async Task LoadProductsFromDatabaseAsync()
        {
            try
            {
                Console.WriteLine("=== Loading Products ===");
                IsLoading = true;
                
                // Database işlemlerini background thread'de yap ama daha verimli
                var memberRouteDetails = await Task.Run(async () =>
                {
                    using var db = new SQLiteConnection(_dbPath);
                    
                    var memberRouteId = Convert.ToInt32(EntMemberRouteId);
                    Console.WriteLine($"Querying products for MemberRouteId: {memberRouteId}");
                    
                    // Orijinal yöntemle veri oku (daha önce çalışıyordu)
                    var listOfMemberRouteDetail = db.Query<entMemberRouteDetail>(
                        "SELECT * FROM entMemberRouteDetail WHERE MemberRouteId=? AND CreatedOn IS NULL", 
                        memberRouteId);
                    
                    Console.WriteLine($"Found {listOfMemberRouteDetail?.Count ?? 0} products");
                    
                    if (listOfMemberRouteDetail == null || listOfMemberRouteDetail.Count == 0)
                    {
                        // Sadece sorun varsa debug bilgisi göster
                        try
                        {
                            var totalRecords = db.ExecuteScalar<int>("SELECT COUNT(*) FROM entMemberRouteDetail");
                            Console.WriteLine($"Debug: Total records in table: {totalRecords}");
                        }
                        catch (Exception debugEx)
                        {
                            Console.WriteLine($"Debug query error: {debugEx.Message}");
                        }
                    }

                    return listOfMemberRouteDetail ?? new List<entMemberRouteDetail>();
                }).ConfigureAwait(false);

                _logger.LogInformation($"Found {memberRouteDetails.Count} products for MemberRouteId: {EntMemberRouteId}");

                // UI güncellemesini main thread'de yap
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    UpdateProductCollections(memberRouteDetails);
                });
                
                // Başarılı yükleme toast'ı
                if (memberRouteDetails.Count > 0)
                {
                    await ShowToast($"✅ {memberRouteDetails.Count} ürün yüklendi", 2000);
                }
                else
                {
                    await ShowToast("ℹ️ Bu mağaza için ürün bulunamadı", 3000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR loading products: {ex.Message}");
                _logger.LogError(ex, "Error loading products from database");
                await ShowToast("❌ Ürünler yüklenirken hata oluştu", 3000);
            }
            finally
            {
                IsLoading = false;
                Console.WriteLine("=== Product Loading Completed ===");
            }
        }

        private void UpdateProductCollections(List<entMemberRouteDetail> memberRouteDetails)
        {
            try
            {
                Console.WriteLine($"=== UpdateProductCollections Started ===");
                Console.WriteLine($"Input memberRouteDetails count: {memberRouteDetails?.Count ?? 0}");
                
                // Android Adapter pattern'ini ObservableCollection ile simüle et
                MemberRouteDetails.Clear();
                Products.Clear();
                FilteredProducts.Clear();

                if (memberRouteDetails == null || memberRouteDetails.Count == 0)
                {
                    Console.WriteLine("No member route details to process");
                    HasProducts = false;
                    ProductCountText = "0 ürün listelendi";
                    return;
                }

                int successCount = 0;
                int failCount = 0;

                foreach (var detail in memberRouteDetails)
                {
                    Console.WriteLine($"Processing RouteDetail Id: {detail.Id}, CustomerProductId: {detail.CustomerProductId}");
                    
                    MemberRouteDetails.Add(detail);
                    
                    var productViewModel = CreateProductViewModel(detail);
                    if (productViewModel != null)
                    {
                        Console.WriteLine($"  ✓ ProductViewModel created: {productViewModel.ProductName}");
                        Products.Add(productViewModel);
                        FilteredProducts.Add(productViewModel);
                        successCount++;
                    }
                    else
                    {
                        Console.WriteLine($"  ✗ Failed to create ProductViewModel");
                        failCount++;
                    }
                }

                HasProducts = Products.Count > 0;
                ProductCountText = $"{Products.Count} ürün listelendi";
                
                Console.WriteLine($"UpdateProductCollections completed:");
                Console.WriteLine($"  - Processed: {memberRouteDetails.Count} route details");
                Console.WriteLine($"  - Success: {successCount} products");
                Console.WriteLine($"  - Failed: {failCount} products");
                Console.WriteLine($"  - Total products: {Products.Count}");
                Console.WriteLine($"  - HasProducts: {HasProducts}");
                Console.WriteLine($"  - ProductCountText: {ProductCountText}");
                
                _logger.LogInformation($"Updated collections with {Products.Count} products");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR in UpdateProductCollections: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                _logger.LogError(ex, "Error updating product collections");
            }
        }

        private ProductViewModel? CreateProductViewModel(entMemberRouteDetail routeDetail)
        {
            try
            {
                Console.WriteLine($"    Creating ProductViewModel for RouteDetail {routeDetail.Id}");
                
                using var db = new SQLiteConnection(_dbPath);
                
                // CustomerProductId null check
                if (routeDetail.CustomerProductId == null)
                {
                    Console.WriteLine($"    ✗ CustomerProductId is null for RouteDetail {routeDetail.Id}");
                    _logger.LogWarning($"CustomerProductId is null for RouteDetail {routeDetail.Id}");
                    return null;
                }

                var customerProductId = routeDetail.CustomerProductId.Value;
                Console.WriteLine($"    CustomerProductId: {customerProductId}");
                
                // Android: db.Query<entCustomerProduct>("SELECT * FROM entCustomerProduct WHERE Id=?", currentCustomerProduct);
                var products = db.Query<entCustomerProduct>(
                    "SELECT * FROM entCustomerProduct WHERE Id=?", 
                    customerProductId);

                Console.WriteLine($"    Found {products?.Count ?? 0} products for CustomerProductId {customerProductId}");

                if (products == null || products.Count == 0) 
                {
                    Console.WriteLine($"    ✗ No product found for CustomerProductId {customerProductId}");
                    return null;
                }

                var product = products[0];
                Console.WriteLine($"    ✓ Product found: {product.Name}");

                // Android koduna uygun olarak entCustomerProductPicture tablosundan resmi al
                Console.WriteLine($"    Loading product image...");
                var productImageSource = GetProductImageFromCustomerProductPicture(routeDetail.CustomerProductId);

                var viewModel = new ProductViewModel
                {
                    Id = product.Id,
                    RouteDetailId = routeDetail.Id,
                    ProductName = product.Name ?? "Ürün Adı Yok",
                    ProductCode = product.Id.ToString(),
                    ProductCategory = "Bilgi girişi için tıklayın.", // Android kodundaki gibi
                    ProductImage = productImageSource,
                    MemberRouteDetail = routeDetail
                };

                Console.WriteLine($"    ✓ ProductViewModel created successfully: {viewModel.ProductName}");
                return viewModel;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"    ✗ ERROR creating ProductViewModel for RouteDetail {routeDetail.Id}: {ex.Message}");
                _logger.LogError(ex, $"Error creating ProductViewModel for RouteDetail {routeDetail.Id}");
                return null;
            }
        }
        #endregion

        #region XAML Event Handlers - Sync wrappers for async methods
        private async void OnSalesOrderClicked(object sender, EventArgs e)
        {
            await OnSalesOrderClickedAsync();
        }

        private async void OnExpirationClicked(object sender, EventArgs e)
        {
            await OnExpirationDateClickedAsync();
        }

        private async void OnReportClicked(object sender, EventArgs e)
        {
            await OnReportClickedAsync();
        }

        private async void OnExitShopClicked(object sender, EventArgs e)
        {
            await OnExitShopClickedAsync();
        }
        #endregion

        #region Button Event Handlers - Android button click karşılıkları
        // Android: BtnTakePhoto_Click
        private async void OnTakePhotoClicked(object sender, EventArgs e)
        {
            await OnTakePhotoClickedAsync();
        }

        private async Task OnTakePhotoClickedAsync()
        {
            try
            {
                _logger.LogInformation($"Take photo clicked - EntMemberRouteId: {EntMemberRouteId}");
                
                // StoreVitrinePhotoPage'e navigate et (Android kodundaki gibi entMemberRouteId parametresi ile)
                // GlobalVariables'da MemberRouteId'yi sakla
                if (GlobalVariables.CurrentMemberRoute == null)
                {
                    GlobalVariables.CurrentMemberRoute = new entMemberRoute { Id = Convert.ToInt32(EntMemberRouteId) };
                }
                else
                {
                    GlobalVariables.CurrentMemberRoute.Id = Convert.ToInt32(EntMemberRouteId);
                }
                
                await Shell.Current.GoToAsync("//StoreVitrinePhotoPage");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in OnTakePhotoClicked");
                await DisplayAlert("Hata", "Fotoğraf çekme işleminde hata oluştu.", "Tamam");
            }
        }

        // Android: BtnSalesOrderEntry_Click
        private async Task OnSalesOrderClickedAsync()
        {
            try
            {
                _logger.LogInformation($"Sales order clicked - EntMemberRouteId: {EntMemberRouteId}");
                
                // Android: salesOrderActivity.PutExtra("entMemberRouteId", Intent.GetStringExtra("entMemberRouteId"));
                var parameters = new Dictionary<string, object>
                {
                    ["entMemberRouteId"] = EntMemberRouteId
                };

                // TODO: SalesOrderPage'e navigate et
                await DisplayAlert("Sipariş Girişi", 
                    "Sipariş giriş sayfası yakında eklenecek.\n" +
                    $"MemberRouteId: {EntMemberRouteId}", 
                    "Tamam");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in OnSalesOrderClicked");
                await DisplayAlert("Hata", "Sipariş işleminde hata oluştu.", "Tamam");
            }
        }

        // Android: BtnExpirationDate_Click
        private async Task OnExpirationDateClickedAsync()
        {
            try
            {
                _logger.LogInformation($"Expiration date clicked - EntMemberRouteId: {EntMemberRouteId}");
                
                // Android: expirationDateActivity.PutExtra("entMemberRouteId", Intent.GetStringExtra("entMemberRouteId"));
                var parameters = new Dictionary<string, object>
                {
                    ["entMemberRouteId"] = EntMemberRouteId
                };

                // TODO: ExpirationDatePage'e navigate et
                await DisplayAlert("Son Kullanma Tarihi", 
                    "SKT kontrol sayfası yakında eklenecek.\n" +
                    $"MemberRouteId: {EntMemberRouteId}", 
                    "Tamam");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in OnExpirationDateClicked");
                await DisplayAlert("Hata", "SKT işleminde hata oluştu.", "Tamam");
            }
        }

        // Mağaza Raporu
        private async Task OnReportClickedAsync()
        {
            try
            {
                _logger.LogInformation($"Report clicked - EntMemberRouteId: {EntMemberRouteId}, StoreName: {StoreName}");
                
                var parameters = new Dictionary<string, object>
                {
                    ["entMemberRouteId"] = EntMemberRouteId,
                    ["StoreName"] = StoreName
                };

                await Shell.Current.GoToAsync("///StoreReportPage", parameters);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in OnReportClicked");
                await DisplayAlert("Hata", "Rapor sayfasına geçiş yapılırken hata oluştu.", "Tamam");
            }
        }

        // Android: BtnExitShop_Click
        private async Task OnExitShopClickedAsync()
        {
            try
            {
                _logger.LogInformation($"Exit shop clicked - EntMemberRouteId: {EntMemberRouteId}");
                
                // Android: exitShopActivity.PutExtra("entMemberRouteId", Intent.GetStringExtra("entMemberRouteId"));
                var parameters = new Dictionary<string, object>
                {
                    ["entMemberRouteId"] = EntMemberRouteId
                };

                // TODO: ExitShopPage'e navigate et
                await DisplayAlert("Mağazadan Çık", 
                    "Mağazadan çıkış sayfası yakında eklenecek.\n" +
                    $"MemberRouteId: {EntMemberRouteId}", 
                    "Tamam");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in OnExitShopClicked");
                await DisplayAlert("Hata", "Çıkış işleminde hata oluştu.", "Tamam");
            }
        }
        #endregion

        #region Product Selection - Android MListView_ItemClick karşılığı
        private async Task OnProductSelected(ProductViewModel selectedProduct)
        {
            try
            {
                if (selectedProduct?.MemberRouteDetail == null) return;

                _logger.LogInformation($"Product selected: {selectedProduct.ProductName} (RouteDetailId: {selectedProduct.RouteDetailId})");
                System.Diagnostics.Debug.WriteLine($"Product selected: {selectedProduct.ProductName} (RouteDetailId: {selectedProduct.RouteDetailId})");

                // Android: insEntMemberRouteDetail = insAdapter.GetItemEntityByPosition(e.Position);
                var memberRouteDetail = selectedProduct.MemberRouteDetail;

                System.Diagnostics.Debug.WriteLine($"MemberRouteDetail: Id={memberRouteDetail.Id}, CustomerProductId={memberRouteDetail.CustomerProductId}, MemberRouteId={memberRouteDetail.MemberRouteId}");

                // Android: merchDataEntryActivity.PutExtra("entMemberRouteDetailId", Convert.ToString(insEntMemberRouteDetail.Id));
                var parameters = new Dictionary<string, object>
                {
                    ["entMemberRouteDetailId"] = memberRouteDetail.Id.ToString()
                };

                System.Diagnostics.Debug.WriteLine($"Navigation parameters: entMemberRouteDetailId={memberRouteDetail.Id}");
                _logger.LogInformation($"Navigating to MerchDataEntryPage with entMemberRouteDetailId={memberRouteDetail.Id}");

                // MerchDataEntryPage'e navigate et
                await Shell.Current.GoToAsync("///MerchDataEntryPage", parameters);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in OnProductSelected");
                System.Diagnostics.Debug.WriteLine($"Error in OnProductSelected: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                await DisplayAlert("Hata", "Ürün seçimi işleminde hata oluştu.", "Tamam");
            }
        }

        // XAML'den çağrılacak event handler
        private async void OnProductSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Console.WriteLine("OnProductSelectionChanged called");
            
            if (e.CurrentSelection.FirstOrDefault() is ProductViewModel selectedProduct)
            {
                Console.WriteLine($"Product selection changed: {selectedProduct.ProductName}");
                
                // Selection'ı temizle
                if (sender is CollectionView collectionView)
                {
                    collectionView.SelectedItem = null;
                    Console.WriteLine("Selection cleared in OnProductSelectionChanged");
                }

                // Güzel bir ripple effect simülasyonu
                await ShowToast($"📦 {selectedProduct.ProductName} seçildi", 2000);
                
                await OnProductSelected(selectedProduct);
            }
        }

        // Yeni: Ürün kartlarına dokunma animasyonu için
        private async void OnProductCardTapped(object sender, EventArgs e)
        {
            if (sender is Frame frame)
            {
                // Dokunma animasyonu
                await frame.ScaleTo(0.95, 50, Easing.CubicOut);
                await frame.ScaleTo(1.0, 50, Easing.CubicOut);
                
                // Ürün seçimi işlemi
                if (frame.BindingContext is ProductViewModel product)
                {
                    await OnProductSelected(product);
                }
            }
        }
        #endregion

        #region Search Functionality
        private void OnSearchExecuted(string searchText)
        {
            try
            {
                IsSearchActive = !string.IsNullOrWhiteSpace(searchText);
                
                FilteredProducts.Clear();

                var itemsToShow = string.IsNullOrWhiteSpace(searchText) 
                    ? Products 
                    : Products.Where(p => 
                        p.ProductName.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                        p.ProductCode.Contains(searchText, StringComparison.OrdinalIgnoreCase));

                foreach (var product in itemsToShow)
                {
                    FilteredProducts.Add(product);
                }

                ProductCountText = IsSearchActive 
                    ? $"{FilteredProducts.Count} ürün bulundu" 
                    : $"{Products.Count} ürün listelendi";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in search");
            }
        }
        #endregion

        #region Helper Methods
        private ImageSource? GetProductImageFromCustomerProductPicture(int? customerProductId)
        {
            try
            {
                if (customerProductId == null || customerProductId.Value <= 0)
                {
                    return ImageSource.FromFile("product_placeholder.png");
                }

                using var db = new SQLiteConnection(_dbPath);
                
                // Android: db.Query<entCustomerProductPicture>("SELECT * FROM entCustomerProductPicture WHERE CustomerProductId=?", currentCustomerProduct);
                var productPictures = db.Query<entCustomerProductPicture>(
                    "SELECT * FROM entCustomerProductPicture WHERE CustomerProductId=?", 
                    customerProductId.Value);

                if (productPictures == null || productPictures.Count == 0)
                {
                    return ImageSource.FromFile("product_placeholder.png");
                }

                var productPicture = productPictures[0];
                
                // Android: Convert.FromBase64String(currentCustomerProductLogoByteString[0].CustomerProductPictureBase64);
                if (!string.IsNullOrEmpty(productPicture.CustomerProductPictureBase64))
                {
                    try
                    {
                        var imageBytes = Convert.FromBase64String(productPicture.CustomerProductPictureBase64);
                        return ImageSource.FromStream(() => new MemoryStream(imageBytes));
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, $"Error converting base64 to image for CustomerProductId: {customerProductId}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, $"Error loading product picture for CustomerProductId: {customerProductId}");
            }

            // Hata durumunda varsayılan resim
            return ImageSource.FromFile("product_placeholder.png");
        }

        private async Task ShowErrorAndNavigateBack(string message)
        {
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                // Daha kullanıcı dostu hata mesajı
                var result = await DisplayAlert("Bir sorun oluştu", 
                    message + "\n\nTekrar denemek ister misiniz?", 
                    "Tekrar Dene", "Geri Dön");
                
                if (result)
                {
                    // Tekrar deneme
                    _isInitialized = false;
                    await Task.Delay(500);
                    await InitializePageAsync();
                }
                else
                {
                    // Geri dön
                    await Shell.Current.GoToAsync("..");
                }
            });
        }

        // Yeni: Toast benzeri bildirim metodu
        private async Task ShowToast(string message, int durationMs = 3000)
        {
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                var toastFrame = new Frame
                {
                    BackgroundColor = Microsoft.Maui.Graphics.Color.FromArgb("#333333"),
                    CornerRadius = 25,
                    Padding = new Thickness(20, 10),
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.End,
                    Margin = new Thickness(20, 0, 20, 100),
                    Opacity = 0,
                    HasShadow = false,
                    Content = new Label
                    {
                        Text = message,
                        TextColor = Colors.White,
                        FontSize = 14,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center
                    }
                };

                // Toast'ı sayfaya ekle
                if (this.Content is Grid mainGrid)
                {
                    mainGrid.Children.Add(toastFrame);
                    
                    // Animasyon
                    await toastFrame.FadeTo(0.9, 250, Easing.CubicOut);
                    await Task.Delay(durationMs);
                    await toastFrame.FadeTo(0, 250, Easing.CubicIn);
                    
                    // Temizle
                    mainGrid.Children.Remove(toastFrame);
                }
            });
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

    #region ProductViewModel - Android Adapter item karşılığı
    public class ProductViewModel
    {
        public int Id { get; set; }
        public int RouteDetailId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductCode { get; set; } = string.Empty;
        public string ProductCategory { get; set; } = string.Empty;
        public ImageSource? ProductImage { get; set; }
        public entMemberRouteDetail? MemberRouteDetail { get; set; } // Android GetItemEntityByPosition için
    }
    #endregion
} 