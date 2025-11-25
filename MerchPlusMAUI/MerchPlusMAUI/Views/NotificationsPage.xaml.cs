using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
// Android.Views.View referansı ile çakışma yaşanmaması için View için takma ad kullanıyoruz
using MauiView = Microsoft.Maui.Controls.View;

namespace MerchPlusMAUI.Views
{
    public partial class NotificationsPage : ContentPage
    {
        private readonly ILogger<NotificationsPage> _logger;
        private readonly List<NotificationItem> _notifications = new List<NotificationItem>();

        public NotificationsPage() : this(null)
        {
            System.Diagnostics.Debug.WriteLine("NotificationsPage parametresiz constructor çalıştı!");
        }

        public NotificationsPage(ILogger<NotificationsPage> logger)
        {
            try
            {
                InitializeComponent();
                _logger = logger;
                
                System.Diagnostics.Debug.WriteLine("NotificationsPage başlatıldı");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"NotificationsPage initialize hatası: {ex.Message}");
                _logger?.LogError(ex, "NotificationsPage başlatma hatası");
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            System.Diagnostics.Debug.WriteLine("NotificationsPage açıldı!");
            LoadNotifications();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private void LoadNotifications()
        {
            try
            {
                // Dummy bildirim verileri oluştur
                _notifications.Clear();
                _notifications.AddRange(GetDummyNotifications());

                // UI'yi güncelle
                UpdateNotificationsUI();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Bildirimler yüklenirken hata oluştu");
                System.Diagnostics.Debug.WriteLine($"Bildirim yükleme hatası: {ex.Message}");
            }
        }

        private List<NotificationItem> GetDummyNotifications()
        {
            return new List<NotificationItem>
            {
                new NotificationItem
                {
                    Id = 1,
                    Title = "Yeni Görev Atandı",
                    Message = "Size yeni bir mağaza ziyareti görevi atandı. Detayları kontrol edin.",
                    Date = DateTime.Now.AddHours(-2),
                    IsRead = false,
                    Type = NotificationType.Task,
                    Icon = "📋"
                },
                new NotificationItem
                {
                    Id = 2,
                    Title = "Veri Senkronizasyonu Tamamlandı",
                    Message = "Tüm verileriniz başarıyla sunucuya gönderildi.",
                    Date = DateTime.Now.AddHours(-5),
                    IsRead = false,
                    Type = NotificationType.Success,
                    Icon = "✅"
                },
                new NotificationItem
                {
                    Id = 3,
                    Title = "Haftalık Rapor Hazır",
                    Message = "Bu haftaki performans raporunuz hazırlandı ve yöneticinize iletildi.",
                    Date = DateTime.Now.AddDays(-1),
                    IsRead = true,
                    Type = NotificationType.Info,
                    Icon = "📊"
                },
                new NotificationItem
                {
                    Id = 4,
                    Title = "Sistem Güncellemesi",
                    Message = "Uygulamamız güncellendi. Yeni özellikler ve iyileştirmeler eklendi.",
                    Date = DateTime.Now.AddDays(-2),
                    IsRead = true,
                    Type = NotificationType.Update,
                    Icon = "🔄"
                },
                new NotificationItem
                {
                    Id = 5,
                    Title = "Ziyaret Hatırlatması",
                    Message = "Bugün ziyaret etmeniz gereken 3 mağaza bulunmaktadır.",
                    Date = DateTime.Now.AddDays(-3),
                    IsRead = true,
                    Type = NotificationType.Reminder,
                    Icon = "⏰"
                }
            };
        }

        private void UpdateNotificationsUI()
        {
            try
            {
                NotificationsContainer.Children.Clear();

                if (_notifications.Count == 0)
                {
                    EmptyStateContainer.IsVisible = true;
                    return;
                }

                EmptyStateContainer.IsVisible = false;

                foreach (var notification in _notifications.OrderByDescending(n => n.Date))
                {
                    var notificationView = CreateNotificationView(notification);
                    NotificationsContainer.Children.Add(notificationView);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "UI güncellenirken hata oluştu");
                System.Diagnostics.Debug.WriteLine($"UI güncelleme hatası: {ex.Message}");
            }
        }

        private MauiView CreateNotificationView(NotificationItem notification)
        {
            var mainFrame = new Frame
            {
                Margin = new Thickness(15, 5),
                Padding = new Thickness(15),
                CornerRadius = 10,
                HasShadow = true,
                BackgroundColor = notification.IsRead ? Colors.White : Colors.LightBlue.WithAlpha(0.1f),
                BorderColor = notification.IsRead ? Colors.LightGray : Colors.Blue.WithAlpha(0.3f)
            };

            var mainGrid = new Grid
            {
                ColumnDefinitions = new ColumnDefinitionCollection
                {
                    new ColumnDefinition { Width = new GridLength(50, GridUnitType.Absolute) },
                    new ColumnDefinition { Width = GridLength.Star },
                    new ColumnDefinition { Width = new GridLength(80, GridUnitType.Absolute) }
                },
                RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto }
                }
            };

            // Icon
            var iconLabel = new Label
            {
                Text = notification.Icon,
                FontSize = 24,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start
            };
            Grid.SetColumn(iconLabel, 0);
            Grid.SetRow(iconLabel, 0);
            mainGrid.Children.Add(iconLabel);

            // Title
            var titleLabel = new Label
            {
                Text = notification.Title,
                FontSize = 16,
                FontAttributes = FontAttributes.Bold,
                TextColor = Colors.Black,
                LineBreakMode = LineBreakMode.WordWrap
            };
            Grid.SetColumn(titleLabel, 1);
            Grid.SetRow(titleLabel, 0);
            mainGrid.Children.Add(titleLabel);

            // Message
            var messageLabel = new Label
            {
                Text = notification.Message,
                FontSize = 14,
                TextColor = Colors.Gray,
                LineBreakMode = LineBreakMode.WordWrap,
                Margin = new Thickness(0, 5, 0, 0)
            };
            Grid.SetColumn(messageLabel, 1);
            Grid.SetRow(messageLabel, 1);
            mainGrid.Children.Add(messageLabel);

            // Date
            var dateLabel = new Label
            {
                Text = GetRelativeTimeString(notification.Date),
                FontSize = 12,
                TextColor = Colors.Gray,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Start
            };
            Grid.SetColumn(dateLabel, 2);
            Grid.SetRow(dateLabel, 0);
            mainGrid.Children.Add(dateLabel);

            // Unread indicator
            if (!notification.IsRead)
            {
                var unreadDot = new Frame
                {
                    BackgroundColor = Colors.Red,
                    CornerRadius = 5,
                    WidthRequest = 10,
                    HeightRequest = 10,
                    Padding = 0,
                    HorizontalOptions = LayoutOptions.End,
                    VerticalOptions = LayoutOptions.Start,
                    Margin = new Thickness(0, 5, 5, 0)
                };
                Grid.SetColumn(unreadDot, 2);
                Grid.SetRow(unreadDot, 1);
                mainGrid.Children.Add(unreadDot);
            }

            mainFrame.Content = mainGrid;

            // Tıklama eventi
            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += (s, e) => OnNotificationTapped(notification);
            mainFrame.GestureRecognizers.Add(tapGesture);

            return mainFrame;
        }

        private string GetRelativeTimeString(DateTime date)
        {
            var timeSpan = DateTime.Now - date;

            if (timeSpan.TotalMinutes < 1)
                return "Az önce";
            if (timeSpan.TotalMinutes < 60)
                return $"{(int)timeSpan.TotalMinutes} dakika önce";
            if (timeSpan.TotalHours < 24)
                return $"{(int)timeSpan.TotalHours} saat önce";
            if (timeSpan.TotalDays < 7)
                return $"{(int)timeSpan.TotalDays} gün önce";
            
            return date.ToString("dd.MM.yyyy");
        }

        private async void OnNotificationTapped(NotificationItem notification)
        {
            try
            {
                // Okunmadı olarak işaretle
                if (!notification.IsRead)
                {
                    notification.IsRead = true;
                    UpdateNotificationsUI();
                }

                // Detay göster
                await DisplayAlert(notification.Title, notification.Message, "Tamam");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Bildirim tıklama hatası");
                System.Diagnostics.Debug.WriteLine($"Bildirim tıklama hatası: {ex.Message}");
            }
        }

        private async void OnMarkAllReadClicked(object sender, EventArgs e)
        {
            try
            {
                bool answer = await DisplayAlert("Onay", 
                    "Tüm bildirimleri okundu olarak işaretlemek istediğinizden emin misiniz?", 
                    "Evet", "Hayır");

                if (answer)
                {
                    foreach (var notification in _notifications)
                    {
                        notification.IsRead = true;
                    }
                    UpdateNotificationsUI();
                    await DisplayAlert("Başarılı", "Tüm bildirimler okundu olarak işaretlendi.", "Tamam");
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Tümünü okundu işaretleme hatası");
                await DisplayAlert("Hata", "İşlem sırasında bir hata oluştu.", "Tamam");
            }
        }
    }

    // Bildirim model sınıfları
    public class NotificationItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public bool IsRead { get; set; }
        public NotificationType Type { get; set; }
        public string Icon { get; set; } = "📢";
    }

    public enum NotificationType
    {
        Task,
        Success,
        Info,
        Update,
        Reminder,
        Warning,
        Error
    }
} 