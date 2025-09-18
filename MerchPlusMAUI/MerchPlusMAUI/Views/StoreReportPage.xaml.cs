using SQLite;
using Microsoft.Extensions.Logging;
using entMerchPlus;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Graphics;

namespace MerchPlusMAUI.Views
{
    [QueryProperty(nameof(EntMemberRouteId), "entMemberRouteId")]
    [QueryProperty(nameof(StoreName), "StoreName")]
    public partial class StoreReportPage : ContentPage, INotifyPropertyChanged
    {
        #region Fields
        private readonly ILogger<StoreReportPage> _logger;
        private readonly string _dbPath;
        private bool _isLoading = false;
        private bool _isInitialized = false;
        #endregion

        #region Properties
        private string _entMemberRouteId = string.Empty;
        public string EntMemberRouteId
        {
            get => _entMemberRouteId;
            set
            {
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
        public StoreReportPage(ILogger<StoreReportPage> logger)
        {
            InitializeComponent();
            _logger = logger;
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
            
            this.Opacity = 0;
            
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
                    
                    _ = Task.Run(async () =>
                    {
                        await Task.Delay(100);
                        await MainThread.InvokeOnMainThreadAsync(async () =>
                        {
                            await this.FadeTo(1, 300, Easing.CubicOut);
                        });
                    });
                    
                    await InitializeReportAsync();
                    _isInitialized = true;
                }
                else
                {
                    Console.WriteLine("ERROR: Navigation parameters not received!");
                    await this.FadeTo(1, 200);
                    await DisplayAlert("Hata", "Mağaza bilgileri alınamadı.", "Tamam");
                }
            }
            else
            {
                await this.FadeTo(1, 200, Easing.CubicOut);
            }
        }

        private async Task InitializeReportAsync()
        {
            try
            {
                Console.WriteLine($"=== InitializeReportAsync Started ===");
                
                IsLoading = true;
                
                await LoadReportDataAsync();
                
                // Buton durumlarını güncelle
                UpdateButtonVisibility();
                
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
        #endregion

        #region Report Data Loading
        private async Task LoadReportDataAsync()
        {
            try
            {
                Console.WriteLine("=== Loading Report Data ===");
                
                var reportData = await Task.Run(() =>
                {
                    using var db = new SQLiteConnection(_dbPath);
                    
                    var memberRouteId = Convert.ToInt32(EntMemberRouteId);
                    Console.WriteLine($"Loading report for MemberRouteId: {memberRouteId}");
                    
                    var memberRouteDetails = db.Query<entMemberRouteDetail>(
                        "SELECT * FROM entMemberRouteDetail WHERE MemberRouteId=? AND CreatedOn IS NULL", 
                        memberRouteId);

                    Console.WriteLine($"Found {memberRouteDetails?.Count ?? 0} products");

                    var reportItems = new List<StoreReportItem>();
                    
                    if (memberRouteDetails != null && memberRouteDetails.Count > 0)
                    {
                        // Rapor kategorileri
                        var categories = new[]
                        {
                            new { Name = "Önyüz", Field = "FacingQuantity" },
                            new { Name = "Raf Sırası", Field = "ShelfOrder" },
                            new { Name = "Raf Stoğu", Field = "ShelfStock" },
                            new { Name = "Fiyat", Field = "Price" },
                            new { Name = "Depo Stok", Field = "WarehouseStock" },
                            new { Name = "Aktivite", Field = "Activity" }
                        };

                        foreach (var category in categories)
                        {
                            var completedCount = 0;
                            var totalCount = memberRouteDetails.Count;
                            
                            // Her kategori için tamamlanma durumunu kontrol et
                            foreach (var detail in memberRouteDetails)
                            {
                                // Burada gerçek alanları kontrol etmek gerekir
                                // Şimdilik dummy veri kullanıyoruz
                                var random = new Random();
                                if (random.Next(0, 100) < 70) // %70 tamamlanma oranı
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
                });
                
                Console.WriteLine("=== Report Data Loading Completed ===");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR loading report data: {ex.Message}");
                _logger.LogError(ex, "Error loading report data");
                await DisplayAlert("Hata", "Rapor verileri yüklenirken hata oluştu.", "Tamam");
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
        private async void OnEntryButtonClicked(object sender, EventArgs e)
        {
            try
            {
                _logger.LogInformation($"Entry button clicked for store: {StoreName}");
                
                // Mağazaya giriş yap (EntryOn alanını güncelle)
                await UpdateStoreEntryStatus(true);
                
                // Buton durumlarını güncelle
                UpdateButtonVisibility();
                
                // StoreProductsPage'e git
                var parameters = new Dictionary<string, object>
                {
                    ["entMemberRouteId"] = EntMemberRouteId,
                    ["StoreName"] = StoreName
                };
                
                await Shell.Current.GoToAsync("///StoreProductsPage", parameters);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in entry button click");
                await DisplayAlert("Hata", "Mağazaya giriş yapılırken hata oluştu.", "Tamam");
            }
        }

        private async void OnContinueButtonClicked(object sender, EventArgs e)
        {
            try
            {
                _logger.LogInformation($"Continue button clicked for store: {StoreName}");
                
                // Direkt StoreProductsPage'e git
                var parameters = new Dictionary<string, object>
                {
                    ["entMemberRouteId"] = EntMemberRouteId,
                    ["StoreName"] = StoreName
                };
                
                await Shell.Current.GoToAsync("///StoreProductsPage", parameters);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in continue button click");
                await DisplayAlert("Hata", "Devam işlemi sırasında hata oluştu.", "Tamam");
            }
        }

        private async void OnExitButtonClicked(object sender, EventArgs e)
        {
            try
            {
                _logger.LogInformation($"Exit button clicked for store: {StoreName}");
                
                // Onay al
                bool result = await DisplayAlert("Mağazadan Çıkış", 
                    $"{StoreName} mağazasından çıkış yapmak istediğinizden emin misiniz?", 
                    "Evet", "Hayır");
                
                if (result)
                {
                    // Mağazadan çıkış yap (ExitOn alanını güncelle)
                    await UpdateStoreEntryStatus(false);
                    
                    // Buton durumlarını güncelle
                    UpdateButtonVisibility();
                    
                    // Başarı mesajı
                    await DisplayAlert("Başarılı", 
                        $"{StoreName} mağazasından başarıyla çıkış yaptınız.", 
                        "Tamam");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in exit button click");
                await DisplayAlert("Hata", "Mağazadan çıkış yapılırken hata oluştu.", "Tamam");
            }
        }

        private async void OnBackToProductsClicked(object sender, EventArgs e)
        {
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    ["entMemberRouteId"] = EntMemberRouteId,
                    ["StoreName"] = StoreName
                };
                
                await Shell.Current.GoToAsync("///StoreProductsPage", parameters);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error navigating to products page");
                await DisplayAlert("Hata", "Ürünler sayfasına geçiş yapılırken hata oluştu.", "Tamam");
            }
        }

        private async void OnBackToStoreClicked(object sender, EventArgs e)
        {
            try
            {
                await Shell.Current.GoToAsync("///StoresPage");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error navigating to stores page");
                await DisplayAlert("Hata", "Mağazalar sayfasına geçiş yapılırken hata oluştu.", "Tamam");
            }
        }
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

        private void UpdateButtonVisibility()
        {
            try
            {
                if (string.IsNullOrEmpty(EntMemberRouteId))
                {
                    _logger.LogWarning("EntMemberRouteId is empty in UpdateButtonVisibility");
                    return;
                }

                if (!int.TryParse(EntMemberRouteId, out int memberRouteId))
                {
                    _logger.LogWarning($"Invalid EntMemberRouteId in UpdateButtonVisibility: {EntMemberRouteId}");
                    return;
                }

                using var db = new SQLiteConnection(_dbPath);
                
                var memberRoute = db.Table<entMemberRoute>().FirstOrDefault(x => x.Id == memberRouteId);
                if (memberRoute != null)
                {
                    bool hasEntry = memberRoute.EntryOn != null;
                    bool hasExit = memberRoute.ExitOn != null;

                    // Butonları ana thread'de güncelle
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        if (hasExit)
                        {
                            // Çıkış yapılmış - hiçbir buton gösterme
                            EntryButton.IsVisible = false;
                            ContinueButton.IsVisible = false;
                            ExitButton.IsVisible = false;
                        }
                        else if (hasEntry)
                        {
                            // Giriş yapılmış ama çıkış yapılmamış - devam et ve çıkış butonları
                            EntryButton.IsVisible = false;
                            ContinueButton.IsVisible = true;
                            ExitButton.IsVisible = true;
                        }
                        else
                        {
                            // Hiç giriş yapılmamış - giriş butonu
                            EntryButton.IsVisible = true;
                            ContinueButton.IsVisible = false;
                            ExitButton.IsVisible = false;
                        }
                    });

                    _logger.LogInformation($"Button visibility updated - Entry: {hasEntry}, Exit: {hasExit}");
                }
                else
                {
                    _logger.LogWarning($"MemberRoute not found for id: {memberRouteId}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating button visibility");
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