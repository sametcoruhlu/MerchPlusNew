using Foundation;
using UIKit;

namespace MerchPlusMAUI.Platforms.iOS
{
    /// <summary>
    /// Status bar stilini kontrol etmek için özel view controller
    /// </summary>
    public class CustomViewController : UIViewController
    {
        public override UIStatusBarStyle PreferredStatusBarStyle()
        {
            return UIStatusBarStyle.LightContent;
        }
        
        public override bool PrefersStatusBarHidden()
        {
            return false;
        }
        
        public override UIStatusBarAnimation PreferredStatusBarUpdateAnimation
        {
            get { return UIStatusBarAnimation.None; }
        }
        
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
            // Status bar güncellemesini tetikle
            SetNeedsStatusBarAppearanceUpdate();
        }
        
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            
            // Her görünümde status bar'ı güncelle
            SetNeedsStatusBarAppearanceUpdate();
        }
    }
} 