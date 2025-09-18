using Microsoft.Maui.Controls;
using System;
using entMerchPlus;

namespace MerchPlusMAUI.Controls
{
    public partial class CommonHeader : ContentView
    {
        private int _notificationCount = 0;

        public CommonHeader()
        {
            System.Diagnostics.Debug.WriteLine("CommonHeader: Constructor called");
            InitializeComponent();
            System.Diagnostics.Debug.WriteLine("CommonHeader: InitializeComponent completed");
            LoadUserInfo();
            LoadNotificationCount();
            System.Diagnostics.Debug.WriteLine("CommonHeader: Constructor completed");
        }

        private void LoadUserInfo()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("CommonHeader: LoadUserInfo called");
                // Kullanıcı bilgilerini yükle
                if (GlobalVariables.CurrentMember != null)
                {
                    UserNameLabel.Text = GlobalVariables.CurrentMember.NameSurname ?? "Kullanıcı";
                    System.Diagnostics.Debug.WriteLine($"CommonHeader: User name set to: {UserNameLabel.Text}");
                    LoadProfilePhoto();
                }
                else
                {
                    UserNameLabel.Text = "Kullanıcı";
                    // Profil fotoğrafı yok, ikonu göster
                    DefaultPersonIcon.IsVisible = true;
                    ProfileImage.IsVisible = false;
                    System.Diagnostics.Debug.WriteLine("CommonHeader: No current member, using defaults");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Kullanıcı bilgisi yükleme hatası: {ex.Message}");
                UserNameLabel.Text = "Kullanıcı";
                // Hata durumunda ikonu göster
                DefaultPersonIcon.IsVisible = true;
                ProfileImage.IsVisible = false;
            }
        }

        private void LoadProfilePhoto()
        {
            try
            {
                if (GlobalVariables.CurrentMember != null && 
                    !string.IsNullOrEmpty(GlobalVariables.CurrentMember.ProfilePicturePath))
                {
                    // Base64 string ise
                    if (GlobalVariables.CurrentMember.ProfilePicturePath.Contains("base64") || 
                        GlobalVariables.CurrentMember.ProfilePicturePath.Length > 100)
                    {
                        // Base64 string'i ImageSource'a çevir
                        var base64String = GlobalVariables.CurrentMember.ProfilePicturePath;
                        if (base64String.Contains(","))
                        {
                            base64String = base64String.Split(',')[1];
                        }
                        
                        var imageBytes = Convert.FromBase64String(base64String);
                        ProfileImage.Source = ImageSource.FromStream(() => new MemoryStream(imageBytes));
                        
                        // Profil fotoğrafı var, ikonu gizle, fotoğrafı göster
                        DefaultPersonIcon.IsVisible = false;
                        ProfileImage.IsVisible = true;
                    }
                    else
                    {
                        // URL ise
                        ProfileImage.Source = ImageSource.FromUri(new Uri(GlobalVariables.WebsitePath + GlobalVariables.CurrentMember.ProfilePicturePath));
                        
                        // Profil fotoğrafı var, ikonu gizle, fotoğrafı göster
                        DefaultPersonIcon.IsVisible = false;
                        ProfileImage.IsVisible = true;
                    }
                }
                else
                {
                    // Profil fotoğrafı yok, ikonu göster, fotoğrafı gizle
                    DefaultPersonIcon.IsVisible = true;
                    ProfileImage.IsVisible = false;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Profil fotoğrafı yükleme hatası: {ex.Message}");
                // Hata durumunda ikonu göster
                DefaultPersonIcon.IsVisible = true;
                ProfileImage.IsVisible = false;
            }
        }

        private void LoadNotificationCount()
        {
            try
            {
                // Şimdilik dummy data - gelecekte servisten alınacak
                _notificationCount = GetDummyNotificationCount();
                UpdateNotificationBadge();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Bildirim sayısı yükleme hatası: {ex.Message}");
                _notificationCount = 0;
                UpdateNotificationBadge();
            }
        }

        private int GetDummyNotificationCount()
        {
            // Dummy data - gerçek uygulamada servisten alınacak
            var random = new Random();
            return random.Next(0, 10);
        }

        private void UpdateNotificationBadge()
        {
            if (_notificationCount > 0)
            {
                NotificationBadge.IsVisible = true;
                NotificationCountLabel.Text = _notificationCount > 99 ? "99+" : _notificationCount.ToString();
            }
            else
            {
                NotificationBadge.IsVisible = false;
            }
        }

        private async void OnNotificationClicked(object sender, EventArgs e)
        {
            try
            {
                // Bildirimler sayfasına yönlendir
                if (Shell.Current != null)
                {
                    await Shell.Current.GoToAsync("//NotificationsPage");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Shell.Current is null, cannot navigate to notifications");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Bildirimler sayfasına gitme hatası: {ex.Message}");
                // Fallback: Alert göster
                if (Application.Current?.MainPage != null)
                {
                    await Application.Current.MainPage.DisplayAlert("Bildirimler", "Bildirimler sayfası açılamadı.", "Tamam");
                }
            }
        }

        // Kullanıcı adını güncelle
        public void UpdateUserName(string userName)
        {
            UserNameLabel.Text = userName ?? "Kullanıcı";
        }

        // Bildirim sayısını güncelle - dışarıdan çağrılabilir
        public void UpdateNotificationCount(int count)
        {
            _notificationCount = count;
            UpdateNotificationBadge();
        }

        // Kullanıcı bilgilerini yenile - dışarıdan çağrılabilir
        public void RefreshUserInfo()
        {
            LoadUserInfo();
            LoadNotificationCount();
        }

        // ContentView için Loaded event'ini kullan
        protected override void OnHandlerChanged()
        {
            base.OnHandlerChanged();
            
            // Handler değiştiğinde kullanıcı bilgisini yenile
            if (Handler != null)
            {
                LoadUserInfo();
                LoadNotificationCount();
            }
        }
    }
} 