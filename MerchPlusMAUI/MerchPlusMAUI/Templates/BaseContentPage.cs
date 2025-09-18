using MerchPlusMAUI.Controls;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Android.Views.View referansı ile çakışma yaşanmaması için View için takma ad kullanıyoruz
using MauiView = Microsoft.Maui.Controls.View;

namespace MerchPlusMAUI.Templates
{
    /// <summary>
    /// Tüm sayfalar için temel sınıf.
    /// Bu sınıf, her sayfada ortak olarak kullanılacak header'ı ekler.
    /// </summary>
    public class BaseContentPage : ContentPage
    {
        protected Grid MainGrid { get; private set; }
        protected CommonHeader Header { get; private set; }
        protected CommonFooter Footer { get; private set; }
        protected ContentView ContentContainer { get; private set; }
        private bool _isLayoutBuilt = false;

        public BaseContentPage()
        {
            System.Diagnostics.Debug.WriteLine($"BaseContentPage: Constructor called for {this.GetType().Name}");
            
            // Sayfa ayarlarını yap
            this.Padding = new Thickness(0);
            
            // Safe area'yı devre dışı bırak
            Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific.Page.SetUseSafeArea(this, false);
            
            // Layout'u oluştur
            BuildLayout();
        }

        private void BuildLayout()
        {
            if (_isLayoutBuilt) return;

            System.Diagnostics.Debug.WriteLine("BaseContentPage: Building layout");

            // Ana grid yapısını oluştur
            MainGrid = new Grid
            {
                BackgroundColor = Microsoft.Maui.Graphics.Colors.White,
                RowSpacing = 0,
                Padding = new Thickness(0),
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition { Height = 70 }, // Header için 70px
                    new RowDefinition { Height = GridLength.Star },  // Content için
                    new RowDefinition { Height = 60 }  // Footer için
                }
            };

            // Header oluştur
            Header = new CommonHeader
            {
                IsVisible = true,
                HeightRequest = 70,
                MinimumHeightRequest = 70,
                VerticalOptions = LayoutOptions.Start,
                Padding = new Thickness(0)
            };
            Grid.SetRow(Header, 0);
            MainGrid.Children.Add(Header);

            // Footer oluştur
            Footer = new CommonFooter
            {
                HeightRequest = 60,
                MinimumHeightRequest = 60,
                VerticalOptions = LayoutOptions.End
            };
            Grid.SetRow(Footer, 2);
            MainGrid.Children.Add(Footer);

            // Content container - ContentView kullanarak daha basit
            ContentContainer = new ContentView
            {
                BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent,
                Padding = new Thickness(0)
            };
            Grid.SetRow(ContentContainer, 1);
            MainGrid.Children.Add(ContentContainer);

            // Base content'i set et
            base.Content = MainGrid;
            _isLayoutBuilt = true;

            System.Diagnostics.Debug.WriteLine("BaseContentPage: Layout built successfully");
        }

        protected override void OnHandlerChanged()
        {
            base.OnHandlerChanged();
            
            if (Handler != null && !_isLayoutBuilt)
            {
                BuildLayout();
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            
            // XAML content'i yükle (OnBindingContextChanged sonrası Content set edilir)
            if (_isLayoutBuilt && base.Content == MainGrid && ContentContainer.Content == null)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    LoadXamlContent();
                });
            }
        }

        protected override void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);
            
            System.Diagnostics.Debug.WriteLine($"BaseContentPage: OnNavigatedTo called for {this.GetType().Name}");
            
            // Layout'un hazır olduğundan emin ol
            if (!_isLayoutBuilt)
            {
                BuildLayout();
            }
            
            // XAML content'i yükle
            LoadXamlContent();
            
            // Header'ı güncelle
            if (Header != null)
            {
                Header.IsVisible = true;
                System.Diagnostics.Debug.WriteLine("BaseContentPage: Header visibility set to true in OnNavigatedTo");
            }
        }

        private void LoadXamlContent()
        {
            try
            {
                // XAML'den yüklenen content'i bul ve ContentContainer'a taşı
                var xamlContent = FindXamlContent();
                if (xamlContent != null && ContentContainer.Content == null)
                {
                    System.Diagnostics.Debug.WriteLine($"BaseContentPage: Loading XAML content: {xamlContent.GetType().Name}");
                    
                    // Parent'tan çıkar (eğer varsa)
                    if (xamlContent.Parent is Layout parentLayout)
                    {
                        parentLayout.Children.Remove(xamlContent);
                        System.Diagnostics.Debug.WriteLine("BaseContentPage: Removed content from original parent");
                    }
                    
                    // ContentContainer'a ekle
                    ContentContainer.Content = xamlContent;
                    System.Diagnostics.Debug.WriteLine("BaseContentPage: XAML content loaded successfully");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"BaseContentPage: Error loading XAML content: {ex.Message}");
            }
        }

        private MauiView FindXamlContent()
        {
            // Bu metod derived class'larda override edilecek
            // veya reflection ile XAML content'i bulacak
            return null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            System.Diagnostics.Debug.WriteLine($"BaseContentPage.OnAppearing called for {this.GetType().Name}");
            
            // Layout'un hazır olduğundan emin ol
            if (!_isLayoutBuilt)
            {
                BuildLayout();
            }
            
            // Header ve Footer görünürlüğünü kontrol et
            if (Header != null)
            {
                Header.IsVisible = true;
                Header.RefreshUserInfo();
                System.Diagnostics.Debug.WriteLine($"Header visibility set to true: {Header.IsVisible}");
            }
            
            if (Footer != null)
            {
                Footer.IsVisible = true;
                System.Diagnostics.Debug.WriteLine($"Footer visibility set to true: {Footer.IsVisible}");
            }
            
            // Safe area ayarları
            try
            {
                this.Padding = new Thickness(0);
                Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific.Page.SetUseSafeArea(this, false);
                
                if (DeviceInfo.Platform == DevicePlatform.Android)
                {
                    this.Padding = new Thickness(0);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Safe area ayarları hatası: {ex.Message}");
            }
        }
    }
} 