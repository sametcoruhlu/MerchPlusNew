using SQLite;
using Microsoft.Extensions.Logging;
using entMerchPlus;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Graphics;
using MerchPlusMAUI.Services;
using MerchPlusMAUI.Models;
using System.Linq;

namespace MerchPlusMAUI.Views
{
    [QueryProperty(nameof(EntMemberRouteId), "entMemberRouteId")]
    [QueryProperty(nameof(StoreName), "StoreName")]
    public partial class StoreReportPage : ContentPage, INotifyPropertyChanged
    {
        #region Fields
        private readonly ILogger<StoreReportPage> _logger;
        private readonly IFieldVisibilityService _fieldVisibilityService;
        private readonly string _dbPath;
        private bool _isLoading = false;
        private bool _isInitialized = false;
        private FieldVisibilityModel? _fieldVisibility;
        #endregion

        #region Properties
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
                PageTitle = $"{_storeName} - Rapor";
            }
        }

        private string _pageTitle = "Mağaza Raporu";
        public string PageTitle
        {
            get => _pageTitle;
            set
            {
                _pageTitle = value;
                OnPropertyChanged();
            }
        }

        private string _reportSummary = string.Empty;
        public string ReportSummary
        {
            get => _reportSummary;
            set
            {
                _reportSummary = value;
                OnPropertyChanged();
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
        #endregion

        #region Collections
        public ObservableCollection<StoreReportItem> ReportItems { get; private set; } = new();
        #endregion

        #region Constructor
        public StoreReportPage(ILogger<StoreReportPage> logger, IFieldVisibilityService? fieldVisibilityService)
        {
            InitializeComponent();
            _logger = logger;
            _fieldVisibilityService = fieldVisibilityService ?? new FieldVisibilityService(new HttpClient());
            _dbPath = GlobalVariables.DatabasePath;
            
            BindingContext = this;
            
            Console.WriteLine("StoreReportPage constructor completed");
            _logger.LogInformation("StoreReportPage initialized");
        }
        #endregion

        #region Lifecycle Methods
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            // Sayfa başlangıçta görünür olsun (siyah ekranı önlemek için)
            this.Opacity = 1;
            
            Console.WriteLine("=== StoreReportPage OnAppearing ===");
            
            if (!_isInitialized)
            {
                Console.WriteLine("Waiting for navigation parameters...");
                
                int attempts = 0;
                while (string.IsNullOrEmpty(EntMemberRouteId) && attempts < 10)
                {
                    await Task.Delay(50);
                    attempts++;
                    Console.WriteLine($"Waiting attempt {attempts}/10 - EntMemberRouteId: '{EntMemberRouteId}'");
                }
                
                if (!string.IsNullOrEmpty(EntMemberRouteId))
                {
                    Console.WriteLine($"Parameters received - EntMemberRouteId: {EntMemberRouteId}, StoreName: {StoreName}");
                    
                    await InitializeReportAsync();
                    _isInitialized = true;
                }
                else
                {
                    Console.WriteLine("ERROR: Navigation parameters not received!");
                    await DisplayAlert("Hata", "Mağaza bilgileri alınamadı.", "Tamam");
                }
            }
            else
            {
                // Sayfa zaten initialize edilmiş, verileri yenile
                Console.WriteLine("Page already initialized, refreshing data...");
                await RefreshReportDataAsync();
            }
        }

        private async Task InitializeReportAsync()
        {
            try
            {
                Console.WriteLine($"=== InitializeReportAsync Started ===");
                
                IsLoading = true;
                
                await LoadReportDataAsync();
                
                Console.WriteLine("=== InitializeReportAsync Completed ===");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR in InitializeReportAsync: {ex.Message}");
                _logger.LogError(ex, "Error initializing StoreReportPage");
                await DisplayAlert("Hata", $"Rapor yüklenirken hata oluştu: {ex.Message}", "Tamam");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task RefreshReportDataAsync()
        {
            try
            {
                Console.WriteLine($"=== RefreshReportDataAsync Started ===");
                
                // EntMemberRouteId boşsa navigation parametrelerini yeniden almaya çalış
                if (string.IsNullOrEmpty(EntMemberRouteId))
                {
                    Console.WriteLine("EntMemberRouteId is empty, trying to get from GlobalVariables...");
                    
                    // GlobalVariables'dan MemberRouteId'yi al
                    if (GlobalVariables.CurrentMemberRoute != null)
                    {
                        EntMemberRouteId = GlobalVariables.CurrentMemberRoute.Id.ToString();
                        Console.WriteLine($"EntMemberRouteId set from GlobalVariables: {EntMemberRouteId}");
                    }
                    else
                    {
                        // Eğer GlobalVariables'da yoksa veritabanından aktif route'u al
                        using var db = new SQLiteConnection(_dbPath);
                        var activeRoute = db.Query<entMemberRoute>("SELECT * FROM entMemberRoute WHERE IsActive = 1 LIMIT 1");
                        if (activeRoute != null && activeRoute.Count > 0)
                        {
                            EntMemberRouteId = activeRoute[0].Id.ToString();
                            Console.WriteLine($"EntMemberRouteId set from DB: {EntMemberRouteId}");
                        }
                    }
                }
                
                // Hala boşsa hata ver
                if (string.IsNullOrEmpty(EntMemberRouteId))
                {
                    Console.WriteLine("ERROR: EntMemberRouteId is still empty after trying to get from GlobalVariables and DB");
                    await DisplayAlert("Hata", "Mağaza bilgileri alınamadı.", "Tamam");
                    return;
                }
                
                // Loading overlay'i göster
                ShowLoadingOverlay();
                
                // Sadece rapor verilerini yenile, field visibility'yi tekrar yükleme
                var reportData = await Task.Run(() =>
                {
                    using var db = new SQLiteConnection(_dbPath);
                    
                    Console.WriteLine($"EntMemberRouteId value before conversion: '{EntMemberRouteId}'");
                    var memberRouteId = Convert.ToInt32(EntMemberRouteId);
                    Console.WriteLine($"Refreshing report for MemberRouteId: {memberRouteId}");
                    
                    // Tüm ürünleri al (hem tamamlanmış hem de tamamlanmamış)
                    var memberRouteDetails = db.Query<entMemberRouteDetail>(
                        "SELECT * FROM entMemberRouteDetail WHERE MemberRouteId=?", 
                        memberRouteId);

                    Console.WriteLine($"Found {memberRouteDetails?.Count ?? 0} products");

                    var reportItems = new List<StoreReportItem>();
                    
                    if (memberRouteDetails != null && memberRouteDetails.Count > 0)
                    {
                        // MerchDataEntryPage ile aynı alanlar - servisten gelen görünürlük ayarlarına göre
                        var categories = new List<dynamic>();
                        
                        // Servisten gelen field visibility'ye göre kategorileri belirle
                        if (_fieldVisibility?.FaceCount == true)
                        {
                            categories.Add(new { Name = "Önyüz", Field = "FaceCount" });
                        }
                        
                        if (_fieldVisibility?.ShelfIndex == true)
                        {
                            categories.Add(new { Name = "Raf Sırası", Field = "ShelfIndex" });
                        }
                        
                        if (_fieldVisibility?.ShelfStock == true)
                        {
                            categories.Add(new { Name = "Raf Stoğu", Field = "ShelfStock" });
                        }
                        
                        if (_fieldVisibility?.Price == true)
                        {
                            categories.Add(new { Name = "Fiyat", Field = "ShelfPrice" });
                        }
                        
                        if (_fieldVisibility?.WarehouseStock == true)
                        {
                            categories.Add(new { Name = "Depo Stok", Field = "WarehouseStock" });
                        }
                        
                        if (_fieldVisibility?.ActivityType == true)
                        {
                            categories.Add(new { Name = "Aktivite", Field = "ActivityTypeName" });
                        }

                        foreach (var category in categories)
                        {
                            var completedCount = 0;
                            var totalCount = memberRouteDetails.Count;
                            
                            // Her kategori için gerçek tamamlanma durumunu kontrol et
                            foreach (var detail in memberRouteDetails)
                            {
                                bool isCompleted = false;
                                
                                switch (category.Field)
                                {
                                    case "FaceCount":
                                        // FaceCount alanının dolu olup olmadığını kontrol et
                                        isCompleted = detail.FaceCount.HasValue && detail.FaceCount.Value > 0;
                                        break;
                                        
                                    case "ShelfIndex":
                                        isCompleted = detail.ShelfIndex.HasValue && detail.ShelfIndex.Value >= 0;
                                        break;
                                        
                                    case "ShelfStock":
                                        isCompleted = detail.ShelfStock.HasValue && detail.ShelfStock.Value >= 0;
                                        break;
                                        
                                    case "ShelfPrice":
                                        isCompleted = detail.ShelfPrice.HasValue && detail.ShelfPrice.Value > 0;
                                        break;
                                        
                                    case "WarehouseStock":
                                        isCompleted = detail.WarehouseStock.HasValue && detail.WarehouseStock.Value >= 0;
                                        break;
                                        
                                    case "ActivityTypeName":
                                        isCompleted = !string.IsNullOrEmpty(detail.ActivityTypeName);
                                        break;
                                }
                                
                                if (isCompleted)
                                {
                                    completedCount++;
                                }
                            }

                            var completionPercentage = totalCount > 0 ? (completedCount * 100) / totalCount : 0;
                            
                            reportItems.Add(new StoreReportItem
                            {
                                Name = category.Name,
                                CompletedCount = completedCount,
                                TotalCount = totalCount,
                                CompletionPercentage = completionPercentage
                            });
                        }
                    }
                    
                    return new { ReportItems = reportItems, TotalProducts = memberRouteDetails?.Count ?? 0 };
                });

                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    UpdateReportUI(reportData.ReportItems, reportData.TotalProducts);
                    // Loading overlay'i gizle
                    HideLoadingOverlay();
                });
                
                Console.WriteLine("=== RefreshReportDataAsync Completed ===");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR refreshing report data: {ex.Message}");
                _logger.LogError(ex, "Error refreshing report data");
                await DisplayAlert("Hata", "Rapor verileri yenilenirken hata oluştu.", "Tamam");
                
                // Hata durumunda da loading overlay'i gizle
                HideLoadingOverlay();
            }
        }
        #endregion

        #region Field Visibility
        private async Task LoadFieldVisibilityAsync()
        {
            try
            {
                Console.WriteLine("LoadFieldVisibilityAsync: Başladı");
                
                if (_fieldVisibilityService == null)
                {
                    Console.WriteLine("LoadFieldVisibilityAsync: _fieldVisibilityService null!");
                    ShowAllFields();
                    return;
                }

                // CustomerId'yi al
                int customerId = await GetCustomerIdAsync();
                
                if (customerId <= 0)
                {
                    Console.WriteLine("LoadFieldVisibilityAsync: CustomerId bulunamadı, tüm alanlar gösteriliyor");
                    ShowAllFields();
                    return;
                }

                Console.WriteLine($"LoadFieldVisibilityAsync: CustomerId: {customerId}, Servis çağrısı yapılıyor...");
                _fieldVisibility = await _fieldVisibilityService.GetFieldVisibilityAsync(customerId);
                
                Console.WriteLine($"LoadFieldVisibilityAsync: Servis cevabı alındı: {_fieldVisibility != null}");
                
                if (_fieldVisibility == null)
                {
                    Console.WriteLine("LoadFieldVisibilityAsync: Servis cevap vermedi, tüm alanlar gösteriliyor");
                    ShowAllFields();
                }
                
                Console.WriteLine("LoadFieldVisibilityAsync: Tamamlandı");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"LoadFieldVisibilityAsync: Hata: {ex.Message}");
                _logger.LogError(ex, "Error loading field visibility");
                // Hata durumunda tüm alanları göster
                ShowAllFields();
            }
        }

        private async Task<int> GetCustomerIdAsync()
        {
            try
            {
                // 1. Önce GlobalVariables.CurrentCustomerId'yi kontrol et
                if (!string.IsNullOrEmpty(GlobalVariables.CurrentCustomerId))
                {
                    if (int.TryParse(GlobalVariables.CurrentCustomerId, out int globalCustomerId))
                    {
                        Console.WriteLine($"GetCustomerIdAsync: GlobalVariables.CurrentCustomerId: {globalCustomerId}");
                        return globalCustomerId;
                    }
                }

                // 2. Route'dan customer bilgisini almaya çalış
                if (!string.IsNullOrEmpty(EntMemberRouteId) && int.TryParse(EntMemberRouteId, out int memberRouteId))
                {
                    using var db = new SQLiteConnection(_dbPath);
                    var route = db.Query<entMemberRoute>("SELECT * FROM entMemberRoute WHERE Id=?", memberRouteId);
                    
                    if (route.Count > 0)
                    {
                        // Route'dan CustomerId al
                        if (route[0].CustomerId != null)
                        {
                            Console.WriteLine($"GetCustomerIdAsync: Route'dan CustomerId: {route[0].CustomerId}");
                            return (int)route[0].CustomerId;
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
                        Console.WriteLine($"GetCustomerIdAsync: UserProfile'den CustomerId: {userCustomerId}");
                        return userCustomerId;
                    }
                }

                // 4. GlobalVariables.CurrentMember'dan al
                if (GlobalVariables.CurrentMember != null && !string.IsNullOrEmpty(GlobalVariables.CurrentMember.Id))
                {
                    if (int.TryParse(GlobalVariables.CurrentMember.Id, out int memberCustomerId))
                    {
                        Console.WriteLine($"GetCustomerIdAsync: CurrentMember'den CustomerId: {memberCustomerId}");
                        return memberCustomerId;
                    }
                }

                Console.WriteLine("GetCustomerIdAsync: CustomerId bulunamadı, test amaçlı 1 kullanılıyor");
                return 1; // Test amaçlı sabit CustomerId
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetCustomerIdAsync: Hata: {ex.Message}");
                return 1; // Hata durumunda da test amaçlı 1 döndür
            }
        }

        private void ShowAllFields()
        {
            Console.WriteLine("ShowAllFields: Tüm alanlar gösteriliyor");
            
            // Tüm alanları göster
            _fieldVisibility = new FieldVisibilityModel
            {
                FaceCount = true,
                ShelfIndex = true,
                ShelfStock = true,
                Price = true,
                WarehouseStock = true,
                ActivityType = true,
                DiscountPrice = true,
                Notes = true
            };
            
            Console.WriteLine("ShowAllFields: Tamamlandı");
        }

        private void ShowLoadingOverlay()
        {
            LoadingOverlay.IsVisible = true;
            Console.WriteLine("ShowLoadingOverlay: Loading overlay gösteriliyor");
        }

        private void HideLoadingOverlay()
        {
            LoadingOverlay.IsVisible = false;
            Console.WriteLine("HideLoadingOverlay: Loading overlay gizleniyor");
        }
        #endregion

        #region Report Data Loading
        private async Task LoadReportDataAsync()
        {
            try
            {
                Console.WriteLine("=== Loading Report Data ===");
                
                // EntMemberRouteId boşsa navigation parametrelerini yeniden almaya çalış
                if (string.IsNullOrEmpty(EntMemberRouteId))
                {
                    Console.WriteLine("EntMemberRouteId is empty, trying to get from GlobalVariables...");
                    
                    // GlobalVariables'dan MemberRouteId'yi al
                    if (GlobalVariables.CurrentMemberRoute != null)
                    {
                        EntMemberRouteId = GlobalVariables.CurrentMemberRoute.Id.ToString();
                        Console.WriteLine($"EntMemberRouteId set from GlobalVariables: {EntMemberRouteId}");
                    }
                    else
                    {
                        // Eğer GlobalVariables'da yoksa veritabanından aktif route'u al
                        using var db = new SQLiteConnection(_dbPath);
                        var activeRoute = db.Query<entMemberRoute>("SELECT * FROM entMemberRoute WHERE IsActive = 1 LIMIT 1");
                        if (activeRoute != null && activeRoute.Count > 0)
                        {
                            EntMemberRouteId = activeRoute[0].Id.ToString();
                            Console.WriteLine($"EntMemberRouteId set from DB: {EntMemberRouteId}");
                        }
                    }
                }
                
                // Hala boşsa hata ver
                if (string.IsNullOrEmpty(EntMemberRouteId))
                {
                    Console.WriteLine("ERROR: EntMemberRouteId is still empty after trying to get from GlobalVariables and DB");
                    await DisplayAlert("Hata", "Mağaza bilgileri alınamadı.", "Tamam");
                    return;
                }
                
                // Loading overlay'i göster
                ShowLoadingOverlay();
                
                // Önce field visibility'yi yükle
                await LoadFieldVisibilityAsync();
                
                var reportData = await Task.Run(() =>
                {
                    using var db = new SQLiteConnection(_dbPath);
                    
                    Console.WriteLine($"EntMemberRouteId value before conversion: '{EntMemberRouteId}'");
                    var memberRouteId = Convert.ToInt32(EntMemberRouteId);
                    Console.WriteLine($"Loading report for MemberRouteId: {memberRouteId}");
                    
                    // Tüm ürünleri al (hem tamamlanmış hem de tamamlanmamış)
                    var memberRouteDetails = db.Query<entMemberRouteDetail>(
                        "SELECT * FROM entMemberRouteDetail WHERE MemberRouteId=?", 
                        memberRouteId);

                    Console.WriteLine($"Found {memberRouteDetails?.Count ?? 0} products");

                    var reportItems = new List<StoreReportItem>();
                    
                    if (memberRouteDetails != null && memberRouteDetails.Count > 0)
                    {
                        // MerchDataEntryPage ile aynı alanlar - servisten gelen görünürlük ayarlarına göre
                        var categories = new List<dynamic>();
                        
                        // Servisten gelen field visibility'ye göre kategorileri belirle
                        if (_fieldVisibility?.FaceCount == true)
                        {
                            categories.Add(new { Name = "Önyüz", Field = "FaceCount" });
                        }
                        
                        if (_fieldVisibility?.ShelfIndex == true)
                        {
                            categories.Add(new { Name = "Raf Sırası", Field = "ShelfIndex" });
                        }
                        
                        if (_fieldVisibility?.ShelfStock == true)
                        {
                            categories.Add(new { Name = "Raf Stoğu", Field = "ShelfStock" });
                        }
                        
                        if (_fieldVisibility?.Price == true)
                        {
                            categories.Add(new { Name = "Fiyat", Field = "ShelfPrice" });
                        }
                        
                        if (_fieldVisibility?.WarehouseStock == true)
                        {
                            categories.Add(new { Name = "Depo Stok", Field = "WarehouseStock" });
                        }
                        
                        if (_fieldVisibility?.ActivityType == true)
                        {
                            categories.Add(new { Name = "Aktivite", Field = "ActivityTypeName" });
                        }

                        foreach (var category in categories)
                        {
                            var completedCount = 0;
                            var totalCount = memberRouteDetails.Count;
                            
                            // Her kategori için gerçek tamamlanma durumunu kontrol et
                            foreach (var detail in memberRouteDetails)
                            {
                                bool isCompleted = false;
                                
                                switch (category.Field)
                                {
                                    case "FaceCount":
                                        // FaceCount alanının dolu olup olmadığını kontrol et
                                        isCompleted = detail.FaceCount.HasValue && detail.FaceCount.Value > 0;
                                        break;
                                        
                                    case "ShelfIndex":
                                        isCompleted = detail.ShelfIndex.HasValue && detail.ShelfIndex.Value >= 0;
                                        break;
                                        
                                    case "ShelfStock":
                                        isCompleted = detail.ShelfStock.HasValue && detail.ShelfStock.Value >= 0;
                                        break;
                                        
                                    case "ShelfPrice":
                                        isCompleted = detail.ShelfPrice.HasValue && detail.ShelfPrice.Value > 0;
                                        break;
                                        
                                    case "WarehouseStock":
                                        isCompleted = detail.WarehouseStock.HasValue && detail.WarehouseStock.Value >= 0;
                                        break;
                                        
                                    case "ActivityTypeName":
                                        isCompleted = !string.IsNullOrEmpty(detail.ActivityTypeName);
                                        break;
                                }
                                
                                if (isCompleted)
                                {
                                    completedCount++;
                                }
                            }

                            var completionPercentage = totalCount > 0 ? (completedCount * 100) / totalCount : 0;
                            
                            reportItems.Add(new StoreReportItem
                            {
                                Name = category.Name,
                                CompletedCount = completedCount,
                                TotalCount = totalCount,
                                CompletionPercentage = completionPercentage
                            });
                        }
                    }
                    
                    return new { ReportItems = reportItems, TotalProducts = memberRouteDetails?.Count ?? 0 };
                });

                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    UpdateReportUI(reportData.ReportItems, reportData.TotalProducts);
                    // Loading overlay'i gizle
                    HideLoadingOverlay();
                });
                
                Console.WriteLine("=== Report Data Loading Completed ===");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR loading report data: {ex.Message}");
                _logger.LogError(ex, "Error loading report data");
                await DisplayAlert("Hata", "Rapor verileri yüklenirken hata oluştu.", "Tamam");
                
                // Hata durumunda da loading overlay'i gizle
                HideLoadingOverlay();
            }
        }

        private void UpdateReportUI(List<StoreReportItem> reportItems, int totalProducts)
        {
            try
            {
                Console.WriteLine($"=== UpdateReportUI Started ===");
                
                ReportItemsContainer.Children.Clear();
                ReportItems.Clear();
                
                ReportSummary = $"{totalProducts} ürün için rapor";
                
                foreach (var item in reportItems)
                {
                    ReportItems.Add(item);
                    
                    var reportCard = CreateReportCard(item);
                    ReportItemsContainer.Children.Add(reportCard);
                }
                
                Console.WriteLine($"Updated UI with {reportItems.Count} report items");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR in UpdateReportUI: {ex.Message}");
                _logger.LogError(ex, "Error updating report UI");
            }
        }

        private Frame CreateReportCard(StoreReportItem item)
        {
            var progressColor = item.CompletionPercentage == 100 
                ? Microsoft.Maui.Graphics.Color.FromArgb("#4CAF50") // Yeşil
                : item.CompletionPercentage >= 70 
                    ? Microsoft.Maui.Graphics.Color.FromArgb("#FF9800") // Turuncu
                    : Microsoft.Maui.Graphics.Color.FromArgb("#F44336"); // Kırmızı

            var card = new Frame
            {
                BackgroundColor = Microsoft.Maui.Graphics.Color.FromArgb("#FFFFFF"),
                CornerRadius = 10,
                HasShadow = true,
                Padding = 15,
                Margin = new Thickness(0, 5)
            };

            var grid = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(120, GridUnitType.Absolute) }
                },
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto }
                }
            };

            // Başlık
            var titleLabel = new Label
            {
                Text = item.Name,
                FontSize = 16,
                FontAttributes = FontAttributes.Bold,
                TextColor = Microsoft.Maui.Graphics.Color.FromArgb("#333333"),
                VerticalOptions = LayoutOptions.Center
            };
            grid.Add(titleLabel, 0, 0);

            // Detay
            var detailLabel = new Label
            {
                Text = $"{item.CompletedCount}/{item.TotalCount} ürün tamamlandı",
                FontSize = 14,
                TextColor = Microsoft.Maui.Graphics.Color.FromArgb("#666666"),
                VerticalOptions = LayoutOptions.Center
            };
            grid.Add(detailLabel, 0, 1);

            // Progress alanı
            var progressStack = new VerticalStackLayout
            {
                Spacing = 5,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center
            };

            var percentLabel = new Label
            {
                Text = $"%{item.CompletionPercentage}",
                FontSize = 16,
                TextColor = progressColor,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.End
            };

            var progressBar = new ProgressBar
            {
                Progress = item.CompletionPercentage / 100.0,
                ProgressColor = progressColor,
                HeightRequest = 8,
                WidthRequest = 100
            };

            progressStack.Add(percentLabel);
            progressStack.Add(progressBar);

            grid.Add(progressStack, 1, 0);
            Grid.SetRowSpan(progressStack, 2);

            card.Content = grid;
            return card;
        }
        #endregion

        #region Event Handlers
        #endregion

        #region Helper Methods
        private async Task UpdateStoreEntryStatus(bool isEntry)
        {
            try
            {
                if (string.IsNullOrEmpty(EntMemberRouteId))
                {
                    _logger.LogWarning("EntMemberRouteId is empty");
                    return;
                }

                if (!int.TryParse(EntMemberRouteId, out int memberRouteId))
                {
                    _logger.LogWarning($"Invalid EntMemberRouteId: {EntMemberRouteId}");
                    return;
                }

                using var db = new SQLiteConnection(_dbPath);
                
                var memberRoute = db.Table<entMemberRoute>().FirstOrDefault(x => x.Id == memberRouteId);
                if (memberRoute != null)
                {
                    if (isEntry)
                    {
                        // Giriş yap
                        memberRoute.EntryOn = DateTime.Now;
                        _logger.LogInformation($"Store entry updated: {memberRoute.EntryOn}");
                    }
                    else
                    {
                        // Çıkış yap
                        memberRoute.ExitOn = DateTime.Now;
                        _logger.LogInformation($"Store exit updated: {memberRoute.ExitOn}");
                    }

                    db.Update(memberRoute);
                    _logger.LogInformation($"Store status updated successfully for MemberRouteId: {memberRouteId}");
                }
                else
                {
                    _logger.LogWarning($"MemberRoute not found for id: {memberRouteId}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating store entry status");
                throw;
            }
        }


        private async void OnHomeButtonClicked(object sender, EventArgs e)
        {
            try
            {
                _logger.LogInformation("Home button clicked from StoreReportPage");
                
                // Ana sayfaya git
                await Shell.Current.GoToAsync("///AboutMePage");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error navigating to home page");
                await DisplayAlert("Hata", "Ana sayfaya yönlendirilirken hata oluştu.", "Tamam");
            }
        }

        private async void OnRefreshButtonClicked(object sender, EventArgs e)
        {
            try
            {
                _logger.LogInformation("Refresh button clicked from StoreReportPage");
                
                // Sayfayı yenile
                await RefreshReportDataAsync();
                
                // Başarı mesajı göster
                await DisplayAlert("Başarılı", "Rapor verileri güncellendi.", "Tamam");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error refreshing report data");
                await DisplayAlert("Hata", "Rapor verileri güncellenirken hata oluştu.", "Tamam");
            }
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

    #region StoreReportItem Model
    public class StoreReportItem
    {
        public string Name { get; set; } = string.Empty;
        public int CompletedCount { get; set; }
        public int TotalCount { get; set; }
        public int CompletionPercentage { get; set; }
    }
    #endregion
} 