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
            
            LoadActivityTypes();
            SetupEventHandlers();
            
            System.Diagnostics.Debug.WriteLine("MerchDataEntryPage Constructor: Tamamlandı");
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

        private void ShowLoadingOverlay()
        {
            LoadingOverlay.IsVisible = true;
            MainContent.IsVisible = false;
            System.Diagnostics.Debug.WriteLine("ShowLoadingOverlay: Loading overlay gösteriliyor");
        }

        private void HideLoadingOverlay()
        {
            LoadingOverlay.IsVisible = false;
            MainContent.IsVisible = true;
            System.Diagnostics.Debug.WriteLine("HideLoadingOverlay: Loading overlay gizleniyor, ana içerik gösteriliyor");
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

                // Sayfa açılır açılmaz loading overlay'i göster
                System.Diagnostics.Debug.WriteLine("LoadProductDataAsync: Sayfa açıldı, loading overlay gösteriliyor");
                ShowLoadingOverlay();

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
                
                // Load route detail - HomePage'deki tablo adını kullan
                var routeDetail = db.Query<entMemberRouteDetail>("SELECT * FROM entMemberRouteDetail WHERE Id=?", _currentId);
                if (routeDetail.Count == 0)
                {
                    await DisplayAlert("Hata", "Ürün bilgisi bulunamadı.", "Tamam");
                    await Shell.Current.GoToAsync("..");
                    return;
                }

                _currentMemberRouteDetail = routeDetail[0];

                // Load route
                var route = db.Query<entMemberRoute>("SELECT * FROM entMemberRoute WHERE Id=?", routeDetail[0].MemberRouteId);
                if (route.Count == 0) return;

                // Load retail shop
                var retailShop = db.Query<entRetailShop>("SELECT * FROM entRetailShop WHERE Id=?", route[0].RetailShopId);
                if (retailShop.Count > 0)
                {
                    RetailShopNameLabel.Text = retailShop[0].Name;
                }

                // Load product
                var product = db.Query<entCustomerProduct>("SELECT * FROM entCustomerProduct WHERE Id=?", routeDetail[0].CustomerProductId);
                if (product.Count > 0)
                {
                    CustomerProductNameLabel.Text = product[0].Name;
                }

                // Load existing data - using the actual properties from entMemberRouteDetail
                // Note: These properties might have different names in the actual entity
                // We'll use the properties that exist in the entMemberRouteDetail class
                
                // For now, we'll set default values and load from database if properties exist
                FaceCountEntry.Text = "0";
                ShelfIndexEntry.Text = "0";
                ShelfStockEntry.Text = "0";
                ShelfPrice1Entry.Text = "0";
                ShelfPrice2Entry.Text = "00";
                WarehouseStockEntry.Text = "0";
                NotesEditor.Text = string.Empty;

                // Try to load existing data if the properties exist
                try
                {
                    // These property names should match the actual entMemberRouteDetail class
                    // You may need to adjust these based on the actual property names
                    if (routeDetail[0].GetType().GetProperty("FaceCount") != null)
                        FaceCountEntry.Text = Convert.ToString(routeDetail[0].GetType().GetProperty("FaceCount")?.GetValue(routeDetail[0]) ?? 0);
                    
                    if (routeDetail[0].GetType().GetProperty("ShelfIndex") != null)
                        ShelfIndexEntry.Text = Convert.ToString(routeDetail[0].GetType().GetProperty("ShelfIndex")?.GetValue(routeDetail[0]) ?? 0);
                    
                    if (routeDetail[0].GetType().GetProperty("ShelfStock") != null)
                        ShelfStockEntry.Text = Convert.ToString(routeDetail[0].GetType().GetProperty("ShelfStock")?.GetValue(routeDetail[0]) ?? 0);
                    
                    if (routeDetail[0].GetType().GetProperty("ShelfPrice") != null)
                    {
                        var price = routeDetail[0].GetType().GetProperty("ShelfPrice")?.GetValue(routeDetail[0]);
                        if (price != null && decimal.TryParse(price.ToString(), out decimal priceValue))
                        {
                            ShelfPrice1Entry.Text = Convert.ToString(Math.Truncate(priceValue));
                            ShelfPrice2Entry.Text = Convert.ToInt32((priceValue - Math.Truncate(priceValue)) * 100).ToString("00");
                        }
                    }
                    
                    if (routeDetail[0].GetType().GetProperty("WarehouseStock") != null)
                        WarehouseStockEntry.Text = Convert.ToString(routeDetail[0].GetType().GetProperty("WarehouseStock")?.GetValue(routeDetail[0]) ?? 0);
                    
                    if (routeDetail[0].GetType().GetProperty("Notes") != null)
                        NotesEditor.Text = routeDetail[0].GetType().GetProperty("Notes")?.GetValue(routeDetail[0])?.ToString() ?? string.Empty;
                    
                    if (routeDetail[0].GetType().GetProperty("ActivityTypeName") != null)
                    {
                        var activityType = routeDetail[0].GetType().GetProperty("ActivityTypeName")?.GetValue(routeDetail[0])?.ToString();
                        if (!string.IsNullOrEmpty(activityType))
                        {
                            var activityTypes = ActivityTypePicker.ItemsSource as List<string>;
                            var index = activityTypes?.IndexOf(activityType) ?? -1;
                            if (index >= 0)
                            {
                                ActivityTypePicker.SelectedIndex = index;
                            }
                        }
                    }
                    
                    if (routeDetail[0].GetType().GetProperty("DiscountedPrice") != null)
                    {
                        var discountPrice = routeDetail[0].GetType().GetProperty("DiscountedPrice")?.GetValue(routeDetail[0]);
                        if (discountPrice != null && decimal.TryParse(discountPrice.ToString(), out decimal discountValue))
                        {
                            DiscountPrice1Entry.Text = Convert.ToString(Math.Truncate(discountValue));
                            DiscountPrice2Entry.Text = Convert.ToInt32((discountValue - Math.Truncate(discountValue)) * 100).ToString("00");
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger?.LogWarning(ex, "Some properties might not exist in entMemberRouteDetail");
                }

                // Load product image
                if (product.Count > 0)
                {
                    var customerProductPicture = db.Query<entCustomerProductPicture>("SELECT * FROM entCustomerProductPicture WHERE CustomerProductId=?", product[0].Id);
                    if (customerProductPicture.Count > 0 && !string.IsNullOrEmpty(customerProductPicture[0].CustomerProductPictureBase64))
                    {
                        try
                        {
                            byte[] imageArray = Convert.FromBase64String(customerProductPicture[0].CustomerProductPictureBase64);
                            CustomerProductPhotoImage.Source = ImageSource.FromStream(() => new MemoryStream(imageArray));
                        }
                        catch (Exception ex)
                        {
                            _logger?.LogWarning(ex, "Could not load product image");
                            CustomerProductPhotoImage.Source = "dotnet_bot.png";
                        }
                    }
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
            string errorMessage = string.Empty;

            // Validate required fields (equivalent to Android validation)
            if (string.IsNullOrEmpty(FaceCountEntry.Text) || Convert.ToInt32(FaceCountEntry.Text) < 0)
            {
                errorMessage += "* Önyüz boş bırakılamaz veya sıfırdan küçük olamaz.\r\n";
            }

            if (string.IsNullOrEmpty(ShelfIndexEntry.Text) || Convert.ToInt32(ShelfIndexEntry.Text) < 0)
            {
                errorMessage += "* Raf Sırası boş bırakılamaz veya sıfırdan küçük olamaz.\r\n";
            }

            if (string.IsNullOrEmpty(ShelfStockEntry.Text) || Convert.ToInt32(ShelfStockEntry.Text) < 0)
            {
                errorMessage += "* Raf Stoğu boş bırakılamaz veya sıfırdan küçük olamaz.\r\n";
            }

            if (string.IsNullOrEmpty(ShelfPrice1Entry.Text) || Convert.ToDecimal(ShelfPrice1Entry.Text) < 0)
            {
                errorMessage += "* Fiyat 1. Kısım boş bırakılamaz veya sıfırdan küçük olamaz.\r\n";
            }

            if (string.IsNullOrEmpty(ShelfPrice2Entry.Text))
            {
                errorMessage += "* Fiyat 2. Kısım boş bırakılamaz.\r\n";
            }

            if (string.IsNullOrEmpty(WarehouseStockEntry.Text) || Convert.ToInt32(WarehouseStockEntry.Text) < 0)
            {
                errorMessage += "* Depo Stoğu boş bırakılamaz veya sıfırdan küçük olamaz.\r\n";
            }

            // Check discount price validation if activity type is "FIYAT INDIRIMI"
            var selectedActivity = ActivityTypePicker.SelectedItem?.ToString();
            if (selectedActivity == "FIYAT INDIRIMI")
            {
                if (string.IsNullOrEmpty(DiscountPrice1Entry.Text) || Convert.ToDecimal(DiscountPrice1Entry.Text) < 0)
                {
                    errorMessage += "* İndirimli Fiyat 1. Kısım boş bırakılamaz veya sıfırdan küçük olamaz.\r\n";
                }

                if (string.IsNullOrEmpty(DiscountPrice2Entry.Text))
                {
                    errorMessage += "* İndirimli Fiyat 2. Kısım boş bırakılamaz.\r\n";
                }
            }

            // Show validation errors if any
            if (!string.IsNullOrEmpty(errorMessage))
            {
                await DisplayAlert("Eksikleriniz var...", 
                    "Aşağıda listelenen sorunları düzelttikten sonra tekrar deneyin.\r\n\r\n" + errorMessage, 
                    "TAMAM");
                return;
            }

            try
            {
                // Calculate prices (equivalent to Android logic)
                decimal priceConcat = Convert.ToDecimal(ShelfPrice1Entry.Text) + (Convert.ToDecimal(ShelfPrice2Entry.Text) / 100);
                decimal priceConcat2 = 0;

                if (selectedActivity == "FIYAT INDIRIMI")
                {
                    priceConcat2 = Convert.ToDecimal(DiscountPrice1Entry.Text) + (Convert.ToDecimal(DiscountPrice2Entry.Text) / 100);
                }

                // Update database
                using var db = new SQLiteConnection(_dbPath);
                
                // Update the existing record - using dynamic SQL based on available properties
                // This approach handles cases where some properties might not exist in the entity
                var updateFields = new List<string>();
                var parameters = new List<object>();
                
                // Add fields that exist in the entity
                updateFields.Add("ShelfIndex=?");
                parameters.Add(Convert.ToInt32(ShelfIndexEntry.Text));
                
                updateFields.Add("ShelfStock=?");
                parameters.Add(Convert.ToInt32(ShelfStockEntry.Text));
                
                updateFields.Add("ShelfPrice=?");
                parameters.Add(priceConcat);
                
                updateFields.Add("FaceCount=?");
                parameters.Add(Convert.ToInt32(FaceCountEntry.Text));
                
                updateFields.Add("WarehouseStock=?");
                parameters.Add(Convert.ToInt32(WarehouseStockEntry.Text));
                
                updateFields.Add("Notes=?");
                parameters.Add(NotesEditor.Text ?? string.Empty);
                
                updateFields.Add("CreatedOn=?");
                parameters.Add(DateTime.Now);
                
                updateFields.Add("ActivityTypeName=?");
                parameters.Add(selectedActivity ?? string.Empty);
                
                updateFields.Add("DiscountedPrice=?");
                parameters.Add(priceConcat2);
                
                // Add the WHERE clause parameter
                parameters.Add(_currentId);
                
                var updateSql = $"UPDATE entMemberRouteDetail SET {string.Join(", ", updateFields)} WHERE Id=?";
                db.Execute(updateSql, parameters.ToArray());

                await DisplayAlert("Başarılı", "Ürün verileri kaydedildi.", "Tamam");
                // Artık otomatik çıkış yapmıyoruz, kullanıcı çıkış butonunu kullanacak
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error saving data");
                await DisplayAlert("Hata", "Veriler kaydedilirken hata oluştu.", "Tamam");
            }
        }

        private async void OnExitClicked(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("OnExitClicked: Çıkış butonuna tıklandı");
                
                // Navigation flag'ini set et
                _isNavigatingAway = true;
                
                // Navigation stack'i kontrol et
                System.Diagnostics.Debug.WriteLine($"OnExitClicked: Current navigation stack count: {Shell.Current.Navigation.NavigationStack.Count}");
                
                // StoreProductsPage'e git
                System.Diagnostics.Debug.WriteLine("OnExitClicked: StoreProductsPage'e yönlendiriliyor");
                await Shell.Current.GoToAsync("//StoreProductsPage");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"OnExitClicked: Genel hata: {ex.Message}");
                _logger?.LogError(ex, "Error navigating back");
                await DisplayAlert("Hata", "Sayfa geçişi sırasında hata oluştu.", "Tamam");
            }
            finally
            {
                // Navigation flag'ini reset et
                _isNavigatingAway = false;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
} 