using MerchPlusMAUI.Views;
using System.Diagnostics;
using Microsoft.Maui.Controls;
using Microsoft.Extensions.Logging;

namespace MerchPlusMAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            
            // Shell'in normal padding'i kullan
            this.Padding = new Thickness(0);

            // Shell'in safe area ayarlarını normal kullan
            Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific.Page.SetUseSafeArea(this, true);
            
            // Shell'in varsayılan spacing'ini kullan
            this.FlyoutBehavior = FlyoutBehavior.Disabled;
            Shell.SetNavBarIsVisible(this, false);
            
            // Routes kaydı için try-catch ile önlem
            try
            {
                // Register routes for navigation
                RegisterRoutes();
                
                // MainActivity.cs'de InitializeAppAsync tamamlandıktan sonra yönlendirme yapılacak
                // Bu nedenle burada otomatik yönlendirme yapmıyoruz
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error during route registration: {ex.Message}");
            }
        }
        
        private void RegisterRoutes()
        {
            // AppShell.xaml'de tanımlanan route'lar için ek registration gerekmez
            // Sadece dinamik route'lar için registration yapılır
            
            try
            {
                // Dinamik sayfalar için route registration (eğer varsa)
                // AboutMePage, LocationsPage, VisitPage, StoresPage, ErrorPage zaten AppShell.xaml'de tanımlı
                
                System.Diagnostics.Debug.WriteLine("Route registration completed - using AppShell.xaml definitions");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error during route registration: {ex.Message}");
            }
        }
    }
}
