using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Windowing;
using Microsoft.UI;
using WinRT.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MerchPlusMAUI.WinUI
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : MauiWinUIApplication
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
        
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            base.OnLaunched(args);

            // Pencereyi bul ve title bar'ı normal haline getir
            var window = Microsoft.UI.Xaml.Window.Current;
            if (window != null)
            {
                // Title bar'ı normal göster ve uygulama adını ayarla
                var appWindow = GetAppWindowForCurrentWindow(window);
                
                if (appWindow != null)
                {
                    // Title bar'ı normal haline getir
                    appWindow.TitleBar.ExtendsContentIntoTitleBar = false;
                    appWindow.Title = "MerchPlus";
                    
                    // Title bar renklerini ayarla
                    appWindow.TitleBar.ButtonBackgroundColor = Microsoft.UI.Colors.Black;
                    appWindow.TitleBar.ButtonForegroundColor = Microsoft.UI.Colors.White;
                    appWindow.TitleBar.BackgroundColor = Microsoft.UI.Colors.Black;
                    appWindow.TitleBar.ForegroundColor = Microsoft.UI.Colors.White;
                }
            }
        }

        private AppWindow GetAppWindowForCurrentWindow(Microsoft.UI.Xaml.Window window)
        {
            try
            {
                IntPtr hWnd = WindowNative.GetWindowHandle(window);
                WindowId windowId = Win32Interop.GetWindowIdFromWindow(hWnd);
                return AppWindow.GetFromWindowId(windowId);
            }
            catch
            {
                return null;
            }
        }
    }
}
