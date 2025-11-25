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
using System.Windows.Input;
using MerchPlusMAUI.Models;
using Microsoft.Maui.ApplicationModel;

namespace MerchPlusMAUI.Views
{
    [QueryProperty(nameof(EntMemberRouteId), "entMemberRouteId")]
    public partial class ExpirationDatePage : ContentPage, INotifyPropertyChanged, IQueryAttributable
    {
        #region Fields
        private readonly ILogger<ExpirationDatePage> _logger;
        private readonly string _dbPath;
        private bool _isInitialized;
        private string _entMemberRouteId = string.Empty;
        private string _storeName = "SKT Raporlama";
        private string _dayText = string.Empty;
        private string _monthText = string.Empty;
        private string _yearText = string.Empty;
        private string _quantityText = string.Empty;
        private bool _isBusy;
        private entCustomerProduct? _selectedProduct;
        #endregion

        #region Properties
        public string EntMemberRouteId
        {
            get => _entMemberRouteId;
            set
            {
                if (_entMemberRouteId != value)
                {
                    Console.WriteLine($"[ExpirationDatePage] EntMemberRouteId Property Set - Old: '{_entMemberRouteId}', New: '{value}'");
                    _entMemberRouteId = value;
                    OnPropertyChanged();
                    
                    // Eğer sayfa zaten görünürse ve henüz initialize edilmemişse, initialize et
                    if (!string.IsNullOrEmpty(value) && !_isInitialized)
                    {
                        Console.WriteLine("[ExpirationDatePage] EntMemberRouteId set, triggering initialization...");
                        _ = Task.Run(async () =>
                        {
                            await Task.Delay(100); // Kısa bir gecikme
                            await MainThread.InvokeOnMainThreadAsync(async () =>
                            {
                                if (!_isInitialized && !string.IsNullOrEmpty(_entMemberRouteId))
                                {
                                    await InitializePageAsync();
                                    _isInitialized = true;
                                }
                            });
                        });
                    }
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
                    _storeName = string.IsNullOrWhiteSpace(value) ? "SKT Raporlama" : value;
                    OnPropertyChanged();
                }
            }
        }

        public string DayText
        {
            get => _dayText;
            set
            {
                if (_dayText != value)
                {
                    _dayText = value;
                    OnPropertyChanged();
                    // Otomatik focus geçişi
                    if (value.Length == 2 && !string.IsNullOrEmpty(value))
                    {
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            MonthEntry?.Focus();
                        });
                    }
                }
            }
        }

        public string MonthText
        {
            get => _monthText;
            set
            {
                if (_monthText != value)
                {
                    _monthText = value;
                    OnPropertyChanged();
                    // Otomatik focus geçişi
                    if (value.Length == 2 && !string.IsNullOrEmpty(value))
                    {
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            YearEntry?.Focus();
                        });
                    }
                }
            }
        }

        public string YearText
        {
            get => _yearText;
            set
            {
                if (_yearText != value)
                {
                    _yearText = value;
                    OnPropertyChanged();
                    // Otomatik focus geçişi
                    if (value.Length == 4 && !string.IsNullOrEmpty(value))
                    {
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            QuantityEntry?.Focus();
                        });
                    }
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

        public entCustomerProduct? SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                if (_selectedProduct != value)
                {
                    _selectedProduct = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<entCustomerProduct> CustomerProducts { get; private set; } = new();
        public ObservableCollection<ExpirationItemViewModel> ExpirationItems { get; private set; } = new();

        public ICommand AddCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        #endregion

        #region Constructor
        public ExpirationDatePage(ILogger<ExpirationDatePage> logger)
        {
            InitializeComponent();
            _logger = logger;
            _dbPath = GlobalVariables.DatabasePath;

            AddCommand = new Command(async () => await OnAddClickedAsync());
            DeleteCommand = new Command<ExpirationItemViewModel>(async (item) => await OnDeleteClickedAsync(item));

            BindingContext = this;
        }
        #endregion

        #region IQueryAttributable
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Console.WriteLine($"[ExpirationDatePage] ApplyQueryAttributes called with {query.Count} parameters");
            foreach (var kvp in query)
            {
                Console.WriteLine($"    {kvp.Key} = '{kvp.Value}'");
            }
            
            // Eğer sayfa zaten initialize edilmişse ve aynı parametreler geliyorsa, tekrar initialize etme
            if (_isInitialized && query.TryGetValue("entMemberRouteId", out var existingRouteId))
            {
                var newRouteId = existingRouteId?.ToString() ?? string.Empty;
                if (newRouteId == EntMemberRouteId)
                {
                    Console.WriteLine($"[ExpirationDatePage] ApplyQueryAttributes: Page already initialized with same parameters, skipping");
                    return;
                }
            }
            
            if (query.TryGetValue("entMemberRouteId", out var routeId))
            {
                EntMemberRouteId = routeId?.ToString() ?? string.Empty;
                Console.WriteLine($"[ExpirationDatePage] Navigation parameters received (EntMemberRouteId='{EntMemberRouteId}')");
            }
            else
            {
                Console.WriteLine("[ExpirationDatePage] WARNING: entMemberRouteId not found in query parameters!");
            }
        }
        #endregion

        #region Lifecycle
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            // Sayfa başlangıçta görünür olsun (siyah ekranı önlemek için)
            this.Opacity = 1;
            
            Console.WriteLine("=== ExpirationDatePage OnAppearing ===");
            System.Diagnostics.Debug.WriteLine("=== ExpirationDatePage OnAppearing ===");
            
            if (!_isInitialized)
            {
                Console.WriteLine("Waiting for navigation parameters...");
                
                // Navigation parametrelerinin set edilmesini bekle
                int attempts = 0;
                while (string.IsNullOrEmpty(EntMemberRouteId) && attempts < 10)
                {
                    await Task.Delay(50);
                    attempts++;
                    Console.WriteLine($"Waiting attempt {attempts}/10 - EntMemberRouteId: '{EntMemberRouteId}'");
                }
                
                if (!string.IsNullOrEmpty(EntMemberRouteId))
                {
                    Console.WriteLine($"Parameters received - EntMemberRouteId: {EntMemberRouteId}");
                    Console.WriteLine("Initializing ExpirationDatePage...");
                    
                    await InitializePageAsync();
                    _isInitialized = true;
                }
                else
                {
                    Console.WriteLine("ERROR: Navigation parameters not received after waiting!");
                    await DisplayAlert("Hata", "Mağaza bilgileri alınamadı. Lütfen tekrar deneyin.", "Tamam");
                    await Shell.Current.GoToAsync("..");
                }
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            
            // Sayfa kapanırken fade-out
            _ = this.FadeTo(0, 150, Easing.CubicIn);
        }
        #endregion

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
                    Console.WriteLine($"[ExpirationDatePage] InitializePageAsync DB path: {_dbPath}");

                    var routeId = Convert.ToInt32(EntMemberRouteId);
                    Console.WriteLine($"[ExpirationDatePage] Parsed routeId: {routeId}");

                    var route = db.Table<entMemberRoute>().FirstOrDefault(r => r.Id == routeId);
                    if (route == null)
                    {
                        Console.WriteLine($"[ExpirationDatePage] Route not found for Id={routeId}");
                        throw new InvalidOperationException("Mağaza rotası bulunamadı.");
                    }
                    Console.WriteLine($"[ExpirationDatePage] Route loaded: CustomerId={route.CustomerId}, RetailShopId={route.RetailShopId}");

                    string resolvedStoreName = StoreName;
                    if (route.RetailShopId.HasValue)
                    {
                        var shop = db.Table<entRetailShop>().FirstOrDefault(x => x.Id == route.RetailShopId.Value);
                        Console.WriteLine($"[ExpirationDatePage] Retail shop lookup by id {route.RetailShopId.Value} returned '{shop?.Name}'");
                        if (!string.IsNullOrWhiteSpace(shop?.Name))
                        {
                            resolvedStoreName = shop.Name!;
                        }
                    }

                    Console.WriteLine($"[ExpirationDatePage] Querying products for MemberRouteId={routeId}, CustomerId={route.CustomerId}");
                    
                    // Eski projede: db.Query<entCustomerProduct>("SELECT * from entCustomerProduct WHERE Id IN ( SELECT CustomerProductId FROM entMemberRouteDetail WHERE entMemberRouteDetail.MemberRouteId = " + Convert.ToString(routeId) + " ) AND entCustomerProduct.CustomerId = " + Convert.ToString(currentMemberRoute[0].CustomerId) + " ORDER BY entCustomerProduct.Name");
                    List<entCustomerProduct> products;
                    
                    if (route.CustomerId.HasValue)
                    {
                        products = db.Query<entCustomerProduct>(
                            "SELECT * FROM entCustomerProduct WHERE Id IN (" +
                            "SELECT CustomerProductId FROM entMemberRouteDetail WHERE MemberRouteId = ?) " +
                            "AND entCustomerProduct.CustomerId = ? ORDER BY entCustomerProduct.Name",
                            routeId,
                            route.CustomerId.Value).ToList();
                    }
                    else
                    {
                        // CustomerId null ise sadece MemberRouteDetail'den ürünleri al
                        products = db.Query<entCustomerProduct>(
                            "SELECT * FROM entCustomerProduct WHERE Id IN (" +
                            "SELECT CustomerProductId FROM entMemberRouteDetail WHERE MemberRouteId = ?) " +
                            "ORDER BY entCustomerProduct.Name",
                            routeId).ToList();
                    }
                    
                    Console.WriteLine($"[ExpirationDatePage] Found {products.Count} products before adding empty product");

                    _logger.LogInformation("ExpirationDatePage: Loaded {Count} products for MemberRouteId={RouteId}, CustomerId={CustomerId}",
                        products.Count, routeId, route.CustomerId?.ToString() ?? "null");
                    Console.WriteLine($"[ExpirationDatePage] Products loaded: {products.Count} (route={routeId}, customer={route.CustomerId?.ToString() ?? "null"})");

                    // Boş ürün ekle (Seçiniz)
                    var emptyProduct = new entCustomerProduct
                    {
                        Id = -1,
                        Name = "Seçiniz"
                    };
                    products.Insert(0, emptyProduct);

                    return (StoreName: resolvedStoreName, Products: products);
                });

                Console.WriteLine("[ExpirationDatePage] Applying initialization data to UI");
                StoreName = initData.StoreName;

                // UI thread'de collection'ı güncelle
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    CustomerProducts.Clear();
                    Console.WriteLine($"[ExpirationDatePage] Populating CustomerProducts collection with {initData.Products.Count} rows");
                    foreach (var product in initData.Products)
                    {
                        CustomerProducts.Add(product);
                    }
                    SelectedProduct = CustomerProducts.FirstOrDefault();
                    Console.WriteLine($"[ExpirationDatePage] SelectedProduct.Id after init: {SelectedProduct?.Id}");
                    Console.WriteLine($"[ExpirationDatePage] CustomerProducts.Count: {CustomerProducts.Count}");
                });

                await RefreshExpirationListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ExpirationDatePage] ERROR in InitializePageAsync: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                _logger.LogError(ex, "Error initializing ExpirationDatePage");
                await DisplayAlert("Hata", $"Sayfa yüklenirken hata oluştu: {ex.Message}", "Tamam");
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion

        #region Commands
        private async Task OnAddClickedAsync()
        {
            try
            {
                _logger.LogInformation("Add expiration date clicked");

                // Validasyon
                string errorMessage = string.Empty;

                if (SelectedProduct == null || SelectedProduct.Id == -1)
                {
                    errorMessage += "* Ürün seçiniz!\r\n\r\n";
                }

                try
                {
                    if (string.IsNullOrWhiteSpace(DayText) || string.IsNullOrWhiteSpace(MonthText) || string.IsNullOrWhiteSpace(YearText))
                    {
                        errorMessage += "* Tarihi gün.ay.yıl formatında giriniz. Örnek : 31.12.2017\r\n\r\n";
                    }
                    else
                    {
                        var expirationDate = new DateTime(Convert.ToInt32(YearText), Convert.ToInt32(MonthText), Convert.ToInt32(DayText));
                        if (expirationDate.Year < DateTime.Now.Year - 1)
                        {
                            errorMessage += "* Ürün SKT bir yıldan daha eski olamaz!\r\n\r\n";
                        }
                    }
                }
                catch (Exception ex)
                {
                    errorMessage += "* Tarihi gün.ay.yıl formatında giriniz. Örnek : 31.12.2017\r\n\r\n";
                }

                if (string.IsNullOrWhiteSpace(QuantityText))
                {
                    errorMessage += "* Adet boş bırakılamaz!";
                }
                else
                {
                    if (Convert.ToInt32(QuantityText) == 0)
                    {
                        errorMessage += "* Ürün adedi 0 olamaz!\r\n\r\n";
                    }
                }

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    await DisplayAlert("Eksikleriniz var...",
                        "Aşağıda listelenen sorunları düzelttikten sonra tekrar deneyin.\r\n\r\n" + errorMessage,
                        "TAMAM");
                    return;
                }

                // Ekleme işlemi
                IsBusy = true;

                await Task.Run(() =>
                {
                    using var db = new SQLiteConnection(_dbPath);
                    var routeId = Convert.ToInt32(EntMemberRouteId);
                    var route = db.Table<entMemberRoute>().FirstOrDefault(r => r.Id == routeId);

                    if (route == null)
                    {
                        throw new InvalidOperationException("Mağaza rotası bulunamadı.");
                    }

                    // Eski projede: entProductExpiration tablosu şu kolonlara sahip:
                    // CreatedOn, CustomerId, CustomerProductId, IsSentToServer, MemberId, MemberRouteId, Quantity, ExpirationDate
                    // Veritabanı tablosu eski yapıya göre oluşturulmuş, bu yüzden doğrudan SQL ile insert yapıyoruz
                    var expirationDate = new DateTime(Convert.ToInt32(YearText), Convert.ToInt32(MonthText), Convert.ToInt32(DayText));
                    var quantity = Convert.ToInt32(QuantityText);
                    var customerProductId = SelectedProduct!.Id;
                    var customerId = route.CustomerId ?? 0; // Eğer null ise 0 kullan
                    var memberId = GlobalVariables.CurrentMember?.Id ?? GlobalVariables.CurrentMemberId ?? string.Empty;
                    var createdOn = DateTime.Now;

                    // SQL ile insert yap (eski tablo yapısına uygun)
                    db.Execute(
                        "INSERT INTO entProductExpiration (CreatedOn, CustomerId, CustomerProductId, IsSentToServer, MemberId, MemberRouteId, Quantity, ExpirationDate) " +
                        "VALUES (?, ?, ?, ?, ?, ?, ?, ?)",
                        createdOn, customerId, customerProductId, false, memberId, routeId, quantity, expirationDate);

                    _logger.LogInformation("ExpirationDatePage: Added expiration date - CustomerProductId={CustomerProductId}, ExpirationDate={ExpirationDate}, Quantity={Quantity}",
                        customerProductId, expirationDate, quantity);
                });

                // Formu temizle
                DayText = string.Empty;
                MonthText = string.Empty;
                YearText = string.Empty;
                QuantityText = string.Empty;
                SelectedProduct = CustomerProducts.FirstOrDefault();

                await RefreshExpirationListAsync();
                await DisplayAlert("Başarılı", "SKT kaydı eklendi.", "Tamam");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ExpirationDatePage] ERROR in OnAddClickedAsync: {ex.Message}");
                _logger.LogError(ex, "Error adding expiration date");
                await DisplayAlert("Hata", $"SKT kaydı eklenirken hata oluştu: {ex.Message}", "Tamam");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task OnDeleteClickedAsync(ExpirationItemViewModel item)
        {
            try
            {
                Console.WriteLine($"[ExpirationDatePage] OnDeleteClickedAsync - Item Id: {item?.Id}, ProductName: {item?.ProductName}, ProductId: {item?.ProductId}, Quantity: {item?.Quantity}, ExpirationDate: {item?.ExpirationDate}");
                
                if (item == null)
                {
                    await DisplayAlert("Hata", "Silinecek kayıt bulunamadı.", "Tamam");
                    return;
                }

                var result = await DisplayAlert("Sil",
                    $"{item.ProductName} ürününün SKT kaydını silmek istediğinize emin misiniz?",
                    "Evet", "Hayır");

                if (!result) return;

                IsBusy = true;

                var deletedRows = await Task.Run(() =>
                {
                    using var db = new SQLiteConnection(_dbPath);
                    var routeId = Convert.ToInt32(EntMemberRouteId);
                    
                    // Eski projede: db.Execute("DELETE FROM entProductExpiration WHERE MemberRouteId=? AND CustomerProductId=? AND Quantity=? AND ExpirationDate=?", Convert.ToInt32(insPrev.MemberRouteId), Convert.ToInt32(insPrev.ProductId), Convert.ToInt32(insPrev.Quantity), Convert.ToDateTime(insPrev.ExpirationDate));
                    // Eski projede Id kullanılmıyor, unique kombinasyon kullanılıyor
                    // Bu yöntem daha güvenli çünkü Id mapping sorunlarından etkilenmiyor
                    var rowsAffected = db.Execute(
                        "DELETE FROM entProductExpiration WHERE MemberRouteId=? AND CustomerProductId=? AND Quantity=? AND ExpirationDate=?",
                        routeId,
                        item.ProductId,
                        item.Quantity,
                        item.ExpirationDate);
                    
                    Console.WriteLine($"[ExpirationDatePage] DELETE query executed - MemberRouteId: {routeId}, ProductId: {item.ProductId}, Quantity: {item.Quantity}, ExpirationDate: {item.ExpirationDate}, Rows affected: {rowsAffected}");
                    _logger.LogInformation("ExpirationDatePage: Deleted expiration date - MemberRouteId={MemberRouteId}, ProductId={ProductId}, Quantity={Quantity}, ExpirationDate={ExpirationDate}, RowsAffected={RowsAffected}",
                        routeId, item.ProductId, item.Quantity, item.ExpirationDate, rowsAffected);
                    return rowsAffected;
                });

                if (deletedRows == 0)
                {
                    Console.WriteLine($"[ExpirationDatePage] WARNING: No rows deleted");
                    await DisplayAlert("Uyarı", "Kayıt silinemedi. Lütfen sayfayı yenileyin ve tekrar deneyin.", "Tamam");
                }
                else
                {
                    Console.WriteLine($"[ExpirationDatePage] Successfully deleted {deletedRows} row(s)");
                }

                await RefreshExpirationListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ExpirationDatePage] ERROR in OnDeleteClickedAsync: {ex.Message}");
                _logger.LogError(ex, "Error deleting expiration date");
                await DisplayAlert("Hata", $"SKT kaydı silinirken hata oluştu: {ex.Message}", "Tamam");
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion

        #region Refresh List
        private async Task RefreshExpirationListAsync()
        {
            try
            {
                var items = await Task.Run(() =>
                {
                    using var db = new SQLiteConnection(_dbPath);
                    var routeId = Convert.ToInt32(EntMemberRouteId);

                    // Eski projede: db.Query<entExpirationDatePreview>("SELECT entProductExpiration.Id, entCustomerProduct.Name as ProductName, entCustomerProduct.Id as ProductId, entProductExpiration.MemberRouteId as MemberRouteId, entProductExpiration.Quantity as [Quantity], entProductExpiration.ExpirationDate FROM entProductExpiration INNER JOIN entCustomerProduct ON entCustomerProduct.Id = entProductExpiration.CustomerProductId WHERE entProductExpiration.MemberRouteId = ?", Convert.ToInt32(routeId));
                    // SQLite JOIN'li sorgularda property mapping sorunları var, bu yüzden iki ayrı sorgu yapıyoruz
                    // 1. Önce tüm expiration kayıtlarını al (Id ile birlikte) - JOIN olmadan
                    var expirationRecords = db.Query<ExpirationRecordRaw>(
                        "SELECT Id, CustomerProductId, MemberRouteId, Quantity, ExpirationDate " +
                        "FROM entProductExpiration " +
                        "WHERE MemberRouteId = ?",
                        routeId).ToList();
                    
                    Console.WriteLine($"[ExpirationDatePage] Found {expirationRecords.Count} expiration records");
                    
                    // İlk kaydı log'la (debug için)
                    if (expirationRecords.Count > 0)
                    {
                        Console.WriteLine($"[ExpirationDatePage] First record - Id: {expirationRecords[0].Id}, CustomerProductId: {expirationRecords[0].CustomerProductId}");
                    }

                    var previewList = new List<ExpirationItemViewModel>();

                    // 2. Her expiration kaydı için product bilgisini al
                    foreach (var expiration in expirationRecords)
                    {
                        var product = db.Table<entCustomerProduct>()
                            .FirstOrDefault(p => p.Id == expiration.CustomerProductId);

                        var productName = product?.Name ?? "Ürün Adı Yok";
                        var productId = expiration.CustomerProductId;

                        Console.WriteLine($"[ExpirationDatePage] Processing item - Id: {expiration.Id}, ProductName: {productName}, ProductId: {productId}");

                        previewList.Add(new ExpirationItemViewModel
                        {
                            Id = expiration.Id,
                            ProductName = productName,
                            ProductId = productId,
                            Quantity = expiration.Quantity,
                            ExpirationDate = expiration.ExpirationDate,
                            MemberRouteId = expiration.MemberRouteId
                        });
                    }
                    
                    Console.WriteLine($"[ExpirationDatePage] Created {previewList.Count} ExpirationItemViewModel items");

                    return previewList;
                });

                ExpirationItems.Clear();
                foreach (var item in items)
                {
                    ExpirationItems.Add(item);
                }

                _logger.LogInformation("ExpirationDatePage: Refreshed expiration list - {Count} items", items.Count);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ExpirationDatePage] ERROR in RefreshExpirationListAsync: {ex.Message}");
                _logger.LogError(ex, "Error refreshing expiration list");
            }
        }
        #endregion

        #region Helper Methods
        private int GetIntValue(Dictionary<string, object> dict, params string[] keys)
        {
            foreach (var key in keys)
            {
                if (dict.ContainsKey(key))
                {
                    var value = dict[key];
                    if (value != null && value != DBNull.Value)
                    {
                        return Convert.ToInt32(value);
                    }
                }
            }
            
            // Case-insensitive arama
            var foundKey = dict.Keys.FirstOrDefault(k => keys.Any(key => k.Equals(key, StringComparison.OrdinalIgnoreCase)));
            if (foundKey != null)
            {
                var value = dict[foundKey];
                if (value != null && value != DBNull.Value)
                {
                    return Convert.ToInt32(value);
                }
            }
            
            return 0;
        }

        private DateTime GetDateTimeValue(Dictionary<string, object> dict, params string[] keys)
        {
            foreach (var key in keys)
            {
                if (dict.ContainsKey(key))
                {
                    var value = dict[key];
                    if (value != null && value != DBNull.Value)
                    {
                        return Convert.ToDateTime(value);
                    }
                }
            }
            
            // Case-insensitive arama
            var foundKey = dict.Keys.FirstOrDefault(k => keys.Any(key => k.Equals(key, StringComparison.OrdinalIgnoreCase)));
            if (foundKey != null)
            {
                var value = dict[foundKey];
                if (value != null && value != DBNull.Value)
                {
                    return Convert.ToDateTime(value);
                }
            }
            
            return DateTime.MinValue;
        }

        private T GetValueFromDictionary<T>(Dictionary<string, object> dict, params string[] keys)
        {
            foreach (var key in keys)
            {
                if (dict.ContainsKey(key))
                {
                    var value = dict[key];
                    if (value != null && value != DBNull.Value)
                    {
                        if (typeof(T) == typeof(string))
                        {
                            return (T)(object)(value.ToString() ?? string.Empty);
                        }
                        return (T)Convert.ChangeType(value, typeof(T));
                    }
                }
            }
            
            // Case-insensitive arama
            var foundKey = dict.Keys.FirstOrDefault(k => keys.Any(key => k.Equals(key, StringComparison.OrdinalIgnoreCase)));
            if (foundKey != null)
            {
                var value = dict[foundKey];
                if (value != null && value != DBNull.Value)
                {
                    if (typeof(T) == typeof(string))
                    {
                        return (T)(object)(value.ToString() ?? string.Empty);
                    }
                    return (T)Convert.ChangeType(value, typeof(T));
                }
            }
            
            if (typeof(T) == typeof(string))
            {
                return (T)(object)string.Empty;
            }
            return default(T)!;
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

    #region ViewModel
    public class ExpirationItemViewModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int MemberRouteId { get; set; }
    }

    // SQL query sonucu için helper class
    public class ExpirationDatePreviewResult
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int MemberRouteId { get; set; }
    }

    // Expiration kaydı için helper class (JOIN yapmadan direkt tablodan almak için)
    public class ExpirationRecord
    {
        public int Id { get; set; }
        public int CustomerProductId { get; set; }
        public int MemberRouteId { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }
    }

    // SQL query sonucu için raw class (property mapping için)
    public class ExpirationRecordRaw
    {
        [SQLite.Column("Id")]
        public int Id { get; set; }
        
        [SQLite.Column("CustomerProductId")]
        public int CustomerProductId { get; set; }
        
        [SQLite.Column("MemberRouteId")]
        public int MemberRouteId { get; set; }
        
        [SQLite.Column("Quantity")]
        public int Quantity { get; set; }
        
        [SQLite.Column("ExpirationDate")]
        public DateTime ExpirationDate { get; set; }
    }
    #endregion
}

