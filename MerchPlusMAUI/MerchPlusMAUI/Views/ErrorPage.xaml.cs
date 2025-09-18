using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MerchPlusMAUI.Views
{
    public partial class ErrorPage : ContentPage, IQueryAttributable
    {
        private Exception _exception;
        private Func<Task> _retryAction;

        public ErrorPage()
        {
            InitializeComponent();
        }

        public ErrorPage(Exception exception = null, Func<Task> retryAction = null)
        {
            InitializeComponent();
            _exception = exception;
            _retryAction = retryAction;
            
            SetupErrorDisplay();
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("Exception", out var exceptionObj) && exceptionObj is Exception exception)
            {
                _exception = exception;
            }

            if (query.TryGetValue("RetryAction", out var retryActionObj) && retryActionObj is Func<Task> retryAction)
            {
                _retryAction = retryAction;
            }

            SetupErrorDisplay();
        }

        private void SetupErrorDisplay()
        {
            if (_exception != null)
            {
                // Ana hata mesajını ayarla
                var errorMessage = GetUserFriendlyErrorMessage(_exception);
                ErrorMessageLabel.Text = errorMessage;

                // Detaylı hata bilgisini ayarla
                var errorDetails = GetDetailedErrorMessage(_exception);
                ErrorDetailsLabel.Text = errorDetails;
            }

            // Retry butonu sadece retry action varsa görünür olsun
            RetryButton.IsVisible = _retryAction != null;
        }

        private string GetUserFriendlyErrorMessage(Exception exception)
        {
            return exception switch
            {
                SQLite.SQLiteException => "Veritabanı bağlantı hatası oluştu.",
                System.Net.Http.HttpRequestException => "İnternet bağlantısı sorunu yaşanıyor.",
                UnauthorizedAccessException => "Yetki hatası oluştu.",
                TimeoutException => "İşlem zaman aşımına uğradı.",
                System.IO.IOException => "Dosya erişim hatası oluştu.",
                _ when exception.Message.Contains("database") || exception.Message.Contains("veritabanı") => "Veritabanı ile ilgili bir sorun oluştu.",
                _ when exception.Message.Contains("connection") || exception.Message.Contains("bağlantı") => "Bağlantı sorunu yaşanıyor.",
                _ => "Beklenmeyen bir hata oluştu."
            };
        }

        private string GetDetailedErrorMessage(Exception exception)
        {
            var details = $"Hata Türü: {exception.GetType().Name}\n";
            details += $"Mesaj: {exception.Message}\n";
            
            if (exception.InnerException != null)
            {
                details += $"İç Hata: {exception.InnerException.Message}\n";
            }

            // Stack trace'i sadece debug modunda göster
            #if DEBUG
            details += $"\nStack Trace:\n{exception.StackTrace}";
            #endif

            return details;
        }

        private async void OnRetryClicked(object sender, EventArgs e)
        {
            if (_retryAction != null)
            {
                try
                {
                    // Retry butonunu devre dışı bırak
                    RetryButton.IsEnabled = false;
                    RetryButton.Text = "Tekrar deneniyor...";

                    await _retryAction();

                    // Başarılı olursa ana sayfaya git
                    await Shell.Current.GoToAsync("///AboutMe");
                }
                catch (Exception ex)
                {
                    // Retry başarısız olursa hata mesajını güncelle
                    _exception = ex;
                    SetupErrorDisplay();
                    
                    await DisplayAlert("Hata", "Tekrar deneme başarısız oldu.", "Tamam");
                }
                finally
                {
                    // Retry butonunu tekrar etkinleştir
                    RetryButton.IsEnabled = true;
                    RetryButton.Text = "Tekrar Dene";
                }
            }
        }

        private async void OnGoToHomeClicked(object sender, EventArgs e)
        {
            try
            {
                // Ana sayfaya git (AboutMe)
                await Shell.Current.GoToAsync("///AboutMe");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", $"Ana sayfaya gidilemedi: {ex.Message}", "Tamam");
            }
        }

        private async void OnExitClicked(object sender, EventArgs e)
        {
            var result = await DisplayAlert(
                "Çıkış", 
                "Uygulamadan çıkmak istediğinize emin misiniz?", 
                "Evet", 
                "Hayır");

            if (result)
            {
                // Uygulamayı kapat
                #if ANDROID
                Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
                #else
                Application.Current?.Quit();
                #endif
            }
        }
    }
} 