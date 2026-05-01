using Microsoft.Maui.Controls;

namespace MerchPlusMAUI.Views
{
    public partial class BlockingOverlayPage : ContentPage
    {
        public BlockingOverlayPage(string message = "İşlem yapılıyor...")
        {
            InitializeComponent();
            SetMessage(message);
        }

        public void SetMessage(string message)
        {
            if (MessageLabel == null)
                return;

            MessageLabel.Text = string.IsNullOrWhiteSpace(message) ? "İşlem yapılıyor..." : message;
        }

        protected override bool OnBackButtonPressed()
        {
            // İşlem sırasında geri tuşunu engelle
            return true;
        }
    }
}

