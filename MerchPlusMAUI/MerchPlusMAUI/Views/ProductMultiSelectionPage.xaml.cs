using Microsoft.Maui.Controls;
using SQLite;
using System.Diagnostics;
using entMerchPlus;
using Microsoft.Extensions.Logging;
using MerchPlusMAUI.Models;
using System.Collections.ObjectModel;

namespace MerchPlusMAUI.Views
{
    public partial class ProductMultiSelectionPage : ContentPage
    {
        private readonly ILogger<ProductMultiSelectionPage> _logger;
        private ObservableCollection<ProductSelectionItem> _productItems = new();
        private List<entCustomerProduct> _selectedProducts;
        private readonly Action<List<entCustomerProduct>> _onProductsSelected;

        public ProductMultiSelectionPage(List<entCustomerProduct> availableProducts, 
                                       List<entCustomerProduct> currentlySelected, 
                                       Action<List<entCustomerProduct>> onProductsSelected,
                                       ILogger<ProductMultiSelectionPage> logger = null)
        {
            InitializeComponent();
            _logger = logger;
            _selectedProducts = currentlySelected ?? new List<entCustomerProduct>();
            _onProductsSelected = onProductsSelected;

            // Sayfa başlangıçta görünür olsun (siyah ekranı önlemek için)
            this.Opacity = 1;

            LoadProducts(availableProducts);
            ProductsCollectionView.ItemsSource = _productItems;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            
            // Sayfa kapanırken fade-out
            _ = this.FadeTo(0, 150, Easing.CubicIn);
        }

        private void LoadProducts(List<entCustomerProduct> availableProducts)
        {
            try
            {
                _productItems.Clear();

                foreach (var product in availableProducts)
                {
                    var isSelected = _selectedProducts.Any(p => p.Id == product.Id);
                    var item = new ProductSelectionItem
                    {
                        Product = product,
                        IsSelected = isSelected
                    };

                    _productItems.Add(item);
                }

                System.Diagnostics.Debug.WriteLine($"ProductMultiSelectionPage: {_productItems.Count} ürün yüklendi");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ProductMultiSelectionPage: LoadProducts error: {ex.Message}");
                _logger?.LogError(ex, "Error loading products");
            }
        }

        private void OnOkClicked(object sender, EventArgs e)
        {
            try
            {
                // Seçili ürünleri topla
                var selectedProducts = _productItems
                    .Where(item => item.IsSelected)
                    .Select(item => item.Product)
                    .ToList();

                System.Diagnostics.Debug.WriteLine($"ProductMultiSelectionPage: {selectedProducts.Count} ürün seçildi");

                // Seçimleri geri döndür
                _onProductsSelected?.Invoke(selectedProducts);

                // Sayfayı kapat
                Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ProductMultiSelectionPage: OnOkClicked error: {ex.Message}");
                _logger?.LogError(ex, "Error in OnOkClicked");
            }
        }

        private void OnCancelClicked(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("ProductMultiSelectionPage: İptal edildi");
                
                // Sayfayı kapat (değişiklikleri kaydetme)
                Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ProductMultiSelectionPage: OnCancelClicked error: {ex.Message}");
                _logger?.LogError(ex, "Error in OnCancelClicked");
            }
        }
    }
}
