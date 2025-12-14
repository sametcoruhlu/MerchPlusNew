using System;
using SQLite;
using entMerchPlus;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using Microsoft.Extensions.Logging;
using MerchPlusMAUI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace MerchPlusMAUI.Views
{
    [QueryProperty(nameof(EntMemberRouteId), "entMemberRouteId")]
    [QueryProperty(nameof(StoreName), "StoreName")]
    public partial class SalesOrderPage : ContentPage, INotifyPropertyChanged, IQueryAttributable
    {
        #region Fields
        private readonly ILogger<SalesOrderPage> _logger;
        private readonly string _dbPath;
        private bool _isInitialized;
        private int _currentSalesOrderId;
        private string _entMemberRouteId = string.Empty;
        private string _storeName = "Sipariş";
        private string _quantityText = string.Empty;
        private bool _isBusy;
        private string _orderSummaryText = "Henüz sipariş kalemi eklenmedi.";
        private entCustomerProduct? _selectedProduct;
        private entSalesOrderPackageType? _selectedPackageType;
        #endregion

        #region Properties
        public string EntMemberRouteId
        {
            get => _entMemberRouteId;
            set
            {
                if (_entMemberRouteId != value)
                {
                    _entMemberRouteId = value;
                    OnPropertyChanged();
                }
            }
        }

        public string StoreName
        {
            get => _storeName;
            set
            {
                if (_storeName != value)
                {
                    _storeName = string.IsNullOrWhiteSpace(value) ? "Sipariş" : value;
                    OnPropertyChanged();
                }
            }
        }

        public string QuantityText
        {
            get => _quantityText;
            set
            {
                if (_quantityText != value)
                {
                    _quantityText = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    OnPropertyChanged();
                }
            }
        }

        public string OrderSummaryText
        {
            get => _orderSummaryText;
            set
            {
                if (_orderSummaryText != value)
                {
                    _orderSummaryText = value;
                    OnPropertyChanged();
                }
            }
        }

        public entCustomerProduct? SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                if (_selectedProduct != value)
                {
                    _selectedProduct = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(HasSelectedProduct));
                }
            }
        }

        public entSalesOrderPackageType? SelectedPackageType
        {
            get => _selectedPackageType;
            set
            {
                if (_selectedPackageType != value)
                {
                    _selectedPackageType = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool HasSelectedProduct => SelectedProduct != null;

        public ObservableCollection<entCustomerProduct> CustomerProducts { get; } = new();
        public ObservableCollection<entSalesOrderPackageType> PackageTypes { get; } = new();
        public ObservableCollection<entSalesOrderPreview> OrderPreviewItems { get; } = new();
        #endregion

        #region Constructor
        public SalesOrderPage(ILogger<SalesOrderPage> logger)
        {
            InitializeComponent();
            _logger = logger ?? Microsoft.Extensions.Logging.Abstractions.NullLogger<SalesOrderPage>.Instance;
            _dbPath = GlobalVariables.DatabasePath;
            BindingContext = this;
            Console.WriteLine("[SalesOrderPage] Constructor completed");
        }
        #endregion

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Console.WriteLine("[SalesOrderPage] ApplyQueryAttributes invoked");

            if (query.TryGetValue("entMemberRouteId", out var routeObj))
            {
                var value = routeObj?.ToString() ?? string.Empty;
                Console.WriteLine($"[SalesOrderPage] ApplyQueryAttributes entMemberRouteId='{value}'");
                EntMemberRouteId = value;
            }

            if (query.TryGetValue("StoreName", out var storeObj))
            {
                var value = storeObj?.ToString() ?? string.Empty;
                Console.WriteLine($"[SalesOrderPage] ApplyQueryAttributes StoreName='{value}'");
                StoreName = value;
            }
        }

        #region Lifecycle
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            // Sayfa başlangıçta görünür olsun (siyah ekranı önlemek için)
            this.Opacity = 1;
            
            Console.WriteLine("[SalesOrderPage] OnAppearing called");

            if (!_isInitialized)
            {
                Console.WriteLine($"[SalesOrderPage] First-time initialization (EntMemberRouteId='{EntMemberRouteId}', StoreName='{StoreName}')");
                await EnsureNavigationParametersAsync();
                await InitializePageAsync();
                _isInitialized = true;
            }
            else
            {
                Console.WriteLine("[SalesOrderPage] Reappearing - refreshing preview");
                await RefreshOrderPreviewAsync();
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            
            // FadeTo(0) kaldırıldı - Shell navigation kendi animasyonunu yönetiyor
        }
        #endregion

        private async Task EnsureNavigationParametersAsync()
        {
            int attempts = 0;
            while (string.IsNullOrWhiteSpace(EntMemberRouteId) && attempts < 10)
            {
                attempts++;
                Console.WriteLine($"[SalesOrderPage] Waiting for navigation parameters attempt {attempts}/10");
                await Task.Delay(100);
            }

            if (string.IsNullOrWhiteSpace(EntMemberRouteId))
            {
                Console.WriteLine("[SalesOrderPage] Navigation parameters still missing after wait");
            }
            else
            {
                Console.WriteLine($"[SalesOrderPage] Navigation parameters received (EntMemberRouteId='{EntMemberRouteId}', StoreName='{StoreName}')");
            }
        }

        #region Initialization
        private async Task InitializePageAsync()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(EntMemberRouteId))
                {
                    await DisplayAlert("Hata", "Mağaza bilgisi alınamadı.", "Tamam");
                    await Shell.Current.GoToAsync("..");
                    return;
                }

                IsBusy = true;

                var initData = await Task.Run(() =>
                {
                    using var db = new SQLiteConnection(_dbPath);
                    Console.WriteLine($"[SalesOrderPage] InitializePageAsync DB path: {_dbPath}");

                    var rawRouteId = EntMemberRouteId;
                    Console.WriteLine($"[SalesOrderPage] Raw EntMemberRouteId value before parse: '{rawRouteId}'");

                    var routeId = Convert.ToInt32(rawRouteId);
                    Console.WriteLine($"[SalesOrderPage] Parsed routeId: {routeId}");

                    var route = db.Table<entMemberRoute>().FirstOrDefault(r => r.Id == routeId);
                    if (route == null)
                    {
                        Console.WriteLine($"[SalesOrderPage] Route not found for Id={routeId}");
                        throw new InvalidOperationException("Mağaza rotası bulunamadı.");
                    }
                    Console.WriteLine($"[SalesOrderPage] Route loaded: CustomerId={route.CustomerId}, RetailShopId={route.RetailShopId}");

                    string resolvedStoreName = StoreName;
                    if (route.RetailShopId.HasValue)
                    {
                        var shop = db.Table<entRetailShop>().FirstOrDefault(x => x.Id == route.RetailShopId.Value);
                        Console.WriteLine($"[SalesOrderPage] Retail shop lookup by id {route.RetailShopId.Value} returned '{shop?.Name}'");
                        if (!string.IsNullOrWhiteSpace(shop?.Name))
                        {
                            resolvedStoreName = shop.Name!;
                        }
                    }

                    Console.WriteLine($"[SalesOrderPage] Querying products for MemberRouteId={routeId}, CustomerId={route.CustomerId ?? 0}");
                    var products = db.Query<entCustomerProduct>(
                        "SELECT * FROM entCustomerProduct WHERE Id IN (" +
                        "SELECT CustomerProductId FROM entMemberRouteDetail WHERE MemberRouteId = ?) " +
                        "AND entCustomerProduct.CustomerId = ? ORDER BY entCustomerProduct.Name",
                        routeId,
                        route.CustomerId ?? 0).ToList();
                    _logger.LogInformation("SalesOrderPage: Loaded {Count} products for MemberRouteId={RouteId}, CustomerId={CustomerId}",
                        products.Count, routeId, route.CustomerId ?? 0);
                    Console.WriteLine($"[SalesOrderPage] Products loaded: {products.Count} (route={routeId}, customer={route.CustomerId ?? 0})");

                    Console.WriteLine("[SalesOrderPage] Querying package types");
                    var packageTypes = db.Query<entSalesOrderPackageType>(
                        "SELECT * FROM entSalesOrderPackageType ORDER BY Id").ToList();
                    _logger.LogInformation("SalesOrderPage: Loaded {Count} package types from table", packageTypes.Count);
                    Console.WriteLine($"[SalesOrderPage] Package types from table: {packageTypes.Count}");

                    if (packageTypes.Count == 0)
                    {
                        packageTypes = GetDefaultPackageTypes();
                        _logger.LogWarning("SalesOrderPage: Package type table empty, using default list (count={Count})", packageTypes.Count);
                        Console.WriteLine("[SalesOrderPage] Package type table empty, using defaults");
                    }

                    Console.WriteLine($"[SalesOrderPage] Querying existing sales order for routeId={routeId}");
                    var salesOrder = db.Query<entSalesOrder>(
                        "SELECT * FROM entSalesOrder WHERE MemberRouteId=? ORDER BY CreatedOn DESC",
                        routeId).FirstOrDefault();
                    _logger.LogInformation("SalesOrderPage: Current sales order id for route {RouteId}: {OrderId}", routeId, salesOrder?.Id);
                    Console.WriteLine($"[SalesOrderPage] Current sales order id: {salesOrder?.Id ?? 0}");

                    var lastId = db.ExecuteScalar<int>("SELECT IFNULL(MAX(Id), 0) FROM entSalesOrder");
                    _logger.LogDebug("SalesOrderPage: Last SalesOrder Id in table: {LastId}", lastId);
                    Console.WriteLine($"[SalesOrderPage] Last sales order id in table: {lastId}");

                    return (StoreName: resolvedStoreName, Products: products, PackageTypes: packageTypes, CurrentOrderId: salesOrder?.Id ?? 0, LastOrderId: lastId);
                });

                Console.WriteLine("[SalesOrderPage] Applying initialization data to UI");
                StoreName = initData.StoreName;

                CustomerProducts.Clear();
                Console.WriteLine($"[SalesOrderPage] Populating CustomerProducts collection with {initData.Products.Count} rows");
                foreach (var product in initData.Products)
                {
                    CustomerProducts.Add(product);
                }
                // Ürün seçimi başlangıçta null olsun - placeholder görünsün
                SelectedProduct = null;
                Console.WriteLine($"[SalesOrderPage] SelectedProduct set to null - placeholder will be shown");

                PackageTypes.Clear();
                Console.WriteLine($"[SalesOrderPage] Populating PackageTypes collection with {initData.PackageTypes.Count} rows");
                foreach (var packageType in initData.PackageTypes)
                {
                    PackageTypes.Add(packageType);
                }
                // Paket tipi seçimi başlangıçta null olsun - placeholder görünsün
                SelectedPackageType = null;
                Console.WriteLine($"[SalesOrderPage] SelectedPackageType set to null - placeholder will be shown");

                _currentSalesOrderId = initData.CurrentOrderId;
                GlobalVariables.CurrentSalesOrderId = initData.CurrentOrderId != 0 ? initData.CurrentOrderId : initData.LastOrderId;

                await RefreshOrderPreviewAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SalesOrderPage initialize error");
                Console.WriteLine($"[SalesOrderPage] ERROR during initialization: {ex.Message}");
                await DisplayAlert("Hata", $"Sayfa yüklenirken hata oluştu.\n{ex.Message}", "Tamam");
                await Shell.Current.GoToAsync("..");
            }
            finally
            {
                IsBusy = false;
                Console.WriteLine("[SalesOrderPage] InitializePageAsync finished");
            }
        }
        #endregion

        #region UI Event Handlers
        private async void OnAddItemClicked(object sender, EventArgs e)
        {
            await AddOrderLineAsync();
        }

        private async void OnDeleteItemClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is int detailId)
            {
                await DeleteOrderLineAsync(detailId);
            }
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            try
            {
                // Loading overlay göster (hemen görünsün)
                IsBusy = true; // Loading overlay göster (IsBusy binding ile)
                
                // Kısa bir delay ekle ki overlay render edilsin
                await Task.Delay(50);
                
                Console.WriteLine($"[SalesOrderPage] Back button tapped. EntMemberRouteId='{EntMemberRouteId}', StoreName='{StoreName}'");
                Console.WriteLine($"[SalesOrderPage] NavigationStack={Shell.Current.Navigation.NavigationStack.Count}, ModalStack={Shell.Current.Navigation.ModalStack.Count}");

                // StoreProductsPage'e geri dön - parametrelerle birlikte
                if (!string.IsNullOrEmpty(EntMemberRouteId))
                {
                    var parameters = new Dictionary<string, object>
                    {
                        ["entMemberRouteId"] = EntMemberRouteId,
                        ["StoreName"] = StoreName ?? "Mağaza"
                    };
                    
                    Console.WriteLine($"[SalesOrderPage] Navigating to StoreProductsPage with parameters:");
                    Console.WriteLine($"[SalesOrderPage]     entMemberRouteId: {EntMemberRouteId}");
                    Console.WriteLine($"[SalesOrderPage]     StoreName: {StoreName}");
                    
                    await Shell.Current.GoToAsync("///StoreProductsPage", parameters);
                    Console.WriteLine("[SalesOrderPage] Shell GoToAsync('///StoreProductsPage') with parameters executed");
                    // Navigation tamamlandı, loading overlay'i gizle (hedef sayfa kendi overlay'ini gösterecek)
                    IsBusy = false;
                }
                else
                {
                    Console.WriteLine("[SalesOrderPage] WARNING - EntMemberRouteId is empty, trying navigation stack");
                    
                    if (Shell.Current.Navigation.NavigationStack.Count > 1)
                    {
                        await Shell.Current.Navigation.PopAsync();
                        Console.WriteLine("[SalesOrderPage] PopAsync executed");
                        IsBusy = false;
                        return;
                    }

                    if (Shell.Current.Navigation.ModalStack.Count > 0)
                    {
                        await Shell.Current.Navigation.PopModalAsync();
                        Console.WriteLine("[SalesOrderPage] PopModalAsync executed");
                        IsBusy = false;
                        return;
                    }

                    Console.WriteLine("[SalesOrderPage] No entries in stack and no parameters, navigating to StoreProductsPage without parameters");
                    await Shell.Current.GoToAsync("///StoreProductsPage");
                    Console.WriteLine("[SalesOrderPage] Shell GoToAsync('///StoreProductsPage') without parameters executed");
                    // Navigation tamamlandı, loading overlay'i gizle (hedef sayfa kendi overlay'ini gösterecek)
                    IsBusy = false;
                }
            }
            catch (Exception ex)
            {
                IsBusy = false;
                _logger.LogError(ex, "SalesOrderPage back navigation error");
                Console.WriteLine($"[SalesOrderPage] ERROR during back navigation: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"[SalesOrderPage] ERROR during back navigation: {ex.Message}");
                
                // Fallback: StoresPage'e git
                try
                {
                    await Shell.Current.GoToAsync("///StoresPage?StoreType=All");
                }
                catch (Exception fallbackEx)
                {
                    Console.WriteLine($"[SalesOrderPage] Fallback navigation error: {fallbackEx.Message}");
                }
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            try
            {
                // Loading overlay göster (hemen görünsün)
                IsBusy = true; // Loading overlay göster
                
                // Kısa bir delay ekle ki overlay render edilsin
                await Task.Delay(50);
                
                // Ürün seçimi kontrolü
                if (SelectedProduct == null)
                {
                    IsBusy = false;
                    await DisplayAlert("Uyarı", "Lütfen bir ürün seçiniz.", "Tamam");
                    return;
                }
                
                if (OrderPreviewItems.Count == 0)
                {
                    IsBusy = false;
                    await DisplayAlert("Bilgi", "Kaydedilecek sipariş kalemi bulunmuyor. Lütfen önce ürünleri listeye ekleyiniz.", "Tamam");
                    return;
                }

                await DisplayAlert("Sipariş Kaydedildi", "Sipariş kalemleri başarıyla kaydedildi.", "Tamam");
                
                // CommonFooter badge'ini güncelle (eğer AboutMePage görünürse)
                Controls.CommonFooter.RefreshBadge();
                
                await Shell.Current.GoToAsync("..");
                // Navigation tamamlandı, loading overlay'i gizle (hedef sayfa kendi overlay'ini gösterecek)
                IsBusy = false;
            }
            catch (Exception ex)
            {
                IsBusy = false;
                _logger?.LogError(ex, "Error in OnSaveClicked");
                await DisplayAlert("Hata", "Sipariş kaydedilirken hata oluştu.", "Tamam");
            }
        }
        #endregion

        #region Business Logic
        private async Task AddOrderLineAsync()
        {
            try
            {
                if (SelectedProduct == null)
                {
                    await DisplayAlert("Uyarı", "Lütfen bir ürün seçiniz.", "Tamam");
                    return;
                }

                if (SelectedPackageType == null)
                {
                    await DisplayAlert("Uyarı", "Lütfen paket tipini seçiniz.", "Tamam");
                    return;
                }

                if (!int.TryParse(QuantityText, out var quantity) || quantity <= 0)
                {
                    await DisplayAlert("Uyarı", "Geçerli bir miktar giriniz.", "Tamam");
                    return;
                }

                IsBusy = true;
                var selectedProductId = SelectedProduct.Id;
                var selectedPackageTypeId = SelectedPackageType.Id;
                Console.WriteLine($"[SalesOrderPage] AddOrderLineAsync starting (product={selectedProductId}, packageType={selectedPackageTypeId}, quantity={QuantityText})");

                await Task.Run(() =>
                {
                    using var db = new SQLiteConnection(_dbPath);
                    var routeId = Convert.ToInt32(EntMemberRouteId);
                    var now = DateTime.Now;
                    Console.WriteLine($"[SalesOrderPage] AddOrderLineAsync DB work (route={routeId})");

                    // Sipariş başlığı kontrolü
                    var currentOrder = _currentSalesOrderId > 0
                        ? db.Table<entSalesOrder>().FirstOrDefault(x => x.Id == _currentSalesOrderId)
                        : null;

                    if (currentOrder == null)
                    {
                        var existingOrder = db.Query<entSalesOrder>(
                            "SELECT * FROM entSalesOrder WHERE MemberRouteId=? ORDER BY CreatedOn DESC",
                            routeId).FirstOrDefault();

                        if (existingOrder != null)
                        {
                            currentOrder = existingOrder;
                            _currentSalesOrderId = existingOrder.Id;
                            GlobalVariables.CurrentSalesOrderId = existingOrder.Id;
                            _logger.LogDebug("SalesOrderPage: Reusing existing order Id={OrderId} for route {RouteId}", existingOrder.Id, routeId);
                            Console.WriteLine($"[SalesOrderPage] Reusing existing order Id={existingOrder.Id} for route {routeId}");
                        }
                    }

                    if (currentOrder == null)
                    {
                        var nextId = db.ExecuteScalar<int>("SELECT IFNULL(MAX(Id), 0) FROM entSalesOrder") + 1;
                        currentOrder = new entSalesOrder
                        {
                            Id = nextId,
                            CreatedBy = GlobalVariables.CurrentMember?.Id ?? string.Empty,
                            CreatedOn = now,
                            CustomerId = db.Table<entMemberRoute>().FirstOrDefault(r => r.Id == routeId)?.CustomerId,
                            MemberRouteId = routeId,
                            SalesOrderStatusId = 1
                        };
                        db.Insert(currentOrder);
                        _currentSalesOrderId = nextId;
                        GlobalVariables.CurrentSalesOrderId = nextId;
                        _logger.LogInformation("SalesOrderPage: Created new sales order Id={OrderId} for route {RouteId}", nextId, routeId);
                        Console.WriteLine($"[SalesOrderPage] Created new sales order Id={nextId} for route {routeId}");
                    }

                    // Detay kaydı
                    var nextDetailId = db.ExecuteScalar<int>("SELECT IFNULL(MAX(Id), 0) FROM entSalesOrderDetail") + 1;
                    var detail = new entSalesOrderDetail
                    {
                        Id = nextDetailId,
                        SalesOrderId = _currentSalesOrderId,
                        CustomerProductId = selectedProductId,
                        Quantity = quantity,
                        SalesOrderPackageTypeId = selectedPackageTypeId,
                        CreatedBy = currentOrder.CreatedBy ?? GlobalVariables.CurrentMember?.Id,
                        CreatedOn = now
                    };

                    db.Insert(detail);
                    _logger.LogInformation(
                        "SalesOrderPage: Added detail Id={DetailId}, ProductId={ProductId}, Quantity={Quantity}, PackageTypeId={PackageTypeId} to OrderId={OrderId}",
                        detail.Id, detail.CustomerProductId, detail.Quantity, detail.SalesOrderPackageTypeId, detail.SalesOrderId);
                    Console.WriteLine($"[SalesOrderPage] Inserted detail Id={detail.Id}, Product={detail.CustomerProductId}, Qty={detail.Quantity}, PackageType={detail.SalesOrderPackageTypeId}");
                });

                QuantityText = string.Empty;
                await RefreshOrderPreviewAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sales order add line error");
                Console.WriteLine($"[SalesOrderPage] ERROR adding order line: {ex.Message}");
                await DisplayAlert("Hata", $"Kayıt yapılırken hata oluştu.\n{ex.Message}", "Tamam");
            }
            finally
            {
                IsBusy = false;
                Console.WriteLine("[SalesOrderPage] AddOrderLineAsync finished");
            }
        }

        private async Task DeleteOrderLineAsync(int detailId)
        {
            bool confirm = await DisplayAlert("Onay", "Seçili kalemi silmek istediğinize emin misiniz?", "Evet", "Hayır");
            if (!confirm)
            {
                return;
            }

            try
            {
                IsBusy = true;
                Console.WriteLine($"[SalesOrderPage] DeleteOrderLineAsync starting (detailId={detailId})");
                await Task.Run(() =>
                {
                    using var db = new SQLiteConnection(_dbPath);
                    db.Execute("DELETE FROM entSalesOrderDetail WHERE Id=?", detailId);
                    _logger.LogWarning("SalesOrderPage: Deleted detail Id={DetailId}", detailId);
                    Console.WriteLine($"[SalesOrderPage] Deleted detail Id={detailId}");
                });

                await RefreshOrderPreviewAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sales order delete line error");
                Console.WriteLine($"[SalesOrderPage] ERROR deleting detail {detailId}: {ex.Message}");
                await DisplayAlert("Hata", $"Kayıt silinirken hata oluştu.\n{ex.Message}", "Tamam");
            }
            finally
            {
                IsBusy = false;
                Console.WriteLine("[SalesOrderPage] DeleteOrderLineAsync finished");
            }
        }

        private async Task RefreshOrderPreviewAsync()
        {
            try
            {
                IsBusy = true;
                Console.WriteLine("[SalesOrderPage] RefreshOrderPreviewAsync starting");

                var previewItems = new List<entSalesOrderPreview>();
                int totalQuantity = 0;

                await Task.Run(() =>
                {
                    using var db = new SQLiteConnection(_dbPath);
                    var routeId = Convert.ToInt32(EntMemberRouteId);

                    var currentOrder = db.Query<entSalesOrder>(
                        "SELECT * FROM entSalesOrder WHERE MemberRouteId=? ORDER BY CreatedOn DESC",
                        routeId).FirstOrDefault();

                    if (currentOrder == null)
                    {
                        _currentSalesOrderId = 0;
                        _logger.LogInformation("SalesOrderPage: No sales order found for preview (route {RouteId})", routeId);
                        Console.WriteLine($"[SalesOrderPage] No order found for route {routeId}");
                        return;
                    }

                    _currentSalesOrderId = currentOrder.Id;
                    GlobalVariables.CurrentSalesOrderId = currentOrder.Id;
                    Console.WriteLine($"[SalesOrderPage] Refreshing preview for order {currentOrder.Id}");

                    previewItems = db.Query<entSalesOrderPreview>(
                        "SELECT " +
                        "entSalesOrderDetail.Id AS Id, " +
                        "entCustomerProduct.Name AS ProductName, " +
                        "entSalesOrderDetail.Quantity AS Quantity, " +
                        "entSalesOrderDetail.Quantity || ' ' || entSalesOrderPackageType.Name AS PackageTypeName " +
                        "FROM entSalesOrderDetail " +
                        "INNER JOIN entCustomerProduct ON entCustomerProduct.Id = entSalesOrderDetail.CustomerProductId " +
                        "INNER JOIN entSalesOrderPackageType ON entSalesOrderPackageType.Id = entSalesOrderDetail.SalesOrderPackageTypeId " +
                        "WHERE entSalesOrderDetail.SalesOrderId = ? " +
                        "ORDER BY entSalesOrderDetail.Id DESC",
                        currentOrder.Id);

                    totalQuantity = previewItems.Sum(x => x.Quantity);
                    _logger.LogInformation("SalesOrderPage: Preview list count={Count}, totalQuantity={TotalQuantity}", previewItems.Count, totalQuantity);
                    Console.WriteLine($"[SalesOrderPage] Preview items={previewItems.Count}, totalQty={totalQuantity}");
                });

                OrderPreviewItems.Clear();
                foreach (var item in previewItems)
                {
                    OrderPreviewItems.Add(item);
                }

                if (OrderPreviewItems.Count == 0)
                {
                    OrderSummaryText = "Henüz sipariş kalemi eklenmedi.";
                    Console.WriteLine("[SalesOrderPage] No preview items to display");
                }
                else
                {
                    OrderSummaryText = $"{OrderPreviewItems.Count} kalem, toplam {totalQuantity} adet.";
                    Console.WriteLine($"[SalesOrderPage] Summary updated: {OrderPreviewItems.Count} items / {totalQuantity} qty");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sales order preview refresh error");
                Console.WriteLine($"[SalesOrderPage] ERROR refreshing preview: {ex.Message}");
                await DisplayAlert("Hata", $"Sipariş listesi yenilenirken hata oluştu.\n{ex.Message}", "Tamam");
            }
            finally
            {
                IsBusy = false;
                Console.WriteLine("[SalesOrderPage] RefreshOrderPreviewAsync finished");
            }
        }
        #endregion

        #region Helpers
        private static List<entSalesOrderPackageType> GetDefaultPackageTypes()
        {
            return new List<entSalesOrderPackageType>
            {
                new entSalesOrderPackageType { Id = 1, Name = "Adet" },
                new entSalesOrderPackageType { Id = 2, Name = "Paket" },
                new entSalesOrderPackageType { Id = 3, Name = "Koli" },
                new entSalesOrderPackageType { Id = 4, Name = "Palet" }
            };
        }
        #endregion

        #region INotifyPropertyChanged
        public new event PropertyChangedEventHandler? PropertyChanged;

        protected new virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        // Geri butonu için override
        protected override bool OnBackButtonPressed()
        {
            System.Diagnostics.Debug.WriteLine("=== SalesOrderPage Hardware Back Button Pressed ===");
            Console.WriteLine("=== SalesOrderPage Hardware Back Button Pressed ===");
            
            // StoreProductsPage'e geri dön - parametrelerle birlikte
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    if (!string.IsNullOrEmpty(EntMemberRouteId))
                    {
                        var parameters = new Dictionary<string, object>
                        {
                            ["entMemberRouteId"] = EntMemberRouteId,
                            ["StoreName"] = StoreName ?? "Mağaza"
                        };
                        
                        System.Diagnostics.Debug.WriteLine($"OnBackButtonPressed: Navigating to StoreProductsPage with parameters:");
                        System.Diagnostics.Debug.WriteLine($"    entMemberRouteId: {EntMemberRouteId}");
                        System.Diagnostics.Debug.WriteLine($"    StoreName: {StoreName}");
                        
                        await Shell.Current.GoToAsync("///StoreProductsPage", parameters);
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("OnBackButtonPressed: WARNING - Cannot get EntMemberRouteId, navigating without parameters");
                        // Fallback: parametre olmadan git
                        await Shell.Current.GoToAsync("///StoreProductsPage");
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"OnBackButtonPressed: Error navigating back: {ex.Message}");
                    _logger?.LogError(ex, "Error navigating back from SalesOrderPage");
                    // Fallback: StoresPage'e git
                    try
                    {
                        await Shell.Current.GoToAsync("///StoresPage?StoreType=All");
                    }
                    catch (Exception fallbackEx)
                    {
                        System.Diagnostics.Debug.WriteLine($"OnBackButtonPressed: Fallback navigation error: {fallbackEx.Message}");
                    }
                }
            });
            
            return true; // Event'i handle et
        }
    }
}

