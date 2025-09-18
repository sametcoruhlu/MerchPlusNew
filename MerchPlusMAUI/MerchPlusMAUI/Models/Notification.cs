using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchPlusMAUI.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }
        public NotificationType Type { get; set; }
        
        // İlgili veri için referans (ör. sipariş ID'si, ürün ID'si)
        public string ReferenceId { get; set; }
    }
    
    public enum NotificationType
    {
        Info,
        Warning,
        Success,
        Error,
        System
    }
} 