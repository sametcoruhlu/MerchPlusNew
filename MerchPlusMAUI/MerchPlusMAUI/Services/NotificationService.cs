using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchPlusMAUI.Services
{
    public class NotificationService
    {
        // Singleton pattern için instance
        private static NotificationService _instance;
        public static NotificationService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NotificationService();
                }
                return _instance;
            }
        }
        
        // Bildirimler listesi
        private readonly List<Notification> _notifications;
        public ReadOnlyCollection<Notification> Notifications => _notifications.AsReadOnly();
        
        // Okunmamış bildirim sayısı
        public int UnreadCount => _notifications.Count(n => !n.IsRead);
        
        // Olaylar
        public event EventHandler UnreadCountChanged;
        
        // Private constructor for singleton
        private NotificationService()
        {
            _notifications = new List<Notification>();
            
            // Start with empty notifications - real notifications will be added from service
            // Demo notifications removed to use only real data
        }
        
        // Bildirim ekle
        public void AddNotification(Notification notification)
        {
            if (notification.Id <= 0)
            {
                // Otomatik ID atama
                notification.Id = _notifications.Count > 0 ? _notifications.Max(n => n.Id) + 1 : 1;
            }
            
            _notifications.Add(notification);
            OnUnreadCountChanged();
        }
        
        // Bildirim ekle (başlık ve mesaj ile)
        public void AddNotification(string title, string message)
        {
            AddNotification(new Notification
            {
                Title = title,
                Message = message,
                CreatedAt = DateTime.Now,
                IsRead = false
            });
        }
        
        // Tüm bildirimleri okundu olarak işaretle
        public void MarkAllAsRead()
        {
            bool anyUnread = _notifications.Any(n => !n.IsRead);
            
            if (anyUnread)
            {
                foreach (var notification in _notifications)
                {
                    notification.IsRead = true;
                }
                
                OnUnreadCountChanged();
            }
        }
        
        /// <summary>
        /// Bildirimi okundu olarak işaretler
        /// </summary>
        public void MarkAsRead(int notificationId)
        {
            var notification = _notifications.FirstOrDefault(n => n.Id == notificationId);
            if (notification != null && !notification.IsRead)
            {
                notification.IsRead = true;
                OnUnreadCountChanged();
            }
        }
        
        // Okunmamış sayısı değiştiğinde event tetikle
        private void OnUnreadCountChanged()
        {
            UnreadCountChanged?.Invoke(this, EventArgs.Empty);
        }
        
        // TODO: Bu servisi gerçek bir API veya veritabanı ile entegre et
        // Şu anda dummy veri kullanılıyor, gerçek implementasyon için:
        // - Bildirimleri veritabanından veya API'den yükle
        // - Push notification servislerine entegrasyon sağla
        // - Kullanıcının bildirim tercihlerini göz önünde bulundur
    }
    
    // Bildirim modeli
    public class Notification
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }
    }
} 