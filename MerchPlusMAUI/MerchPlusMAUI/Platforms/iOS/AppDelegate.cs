using Foundation;
using UIKit;

namespace MerchPlusMAUI
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
        
        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            // iOS'ta status bar'ı normal haline getir
            
            // Window arka plan rengini normal yap
            if (UIApplication.SharedApplication.KeyWindow != null)
            {
                UIApplication.SharedApplication.KeyWindow.BackgroundColor = UIColor.White;
            }
            
            // Status bar stilini normal yap
            try
            {
                // Normal status bar stili (siyah yazı, beyaz arka plan)
                UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.Default;
                UIApplication.SharedApplication.StatusBarHidden = false;
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Status bar ayarlama hatası: {ex.Message}");
            }
            
            // Normal başlatma işlemi devam eder
            return base.FinishedLaunching(application, launchOptions);
        }
        
        // Application lifecycle metodları
        public override void OnActivated(UIApplication application)
        {
            base.OnActivated(application);
            
            // Uygulama aktif olduğunda status bar ayarlarını tekrar kontrol et
            try
            {
                UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.Default;
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"OnActivated status bar hatası: {ex.Message}");
            }
        }
    }
}
