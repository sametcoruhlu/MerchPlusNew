using Android.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.MerchPlus.Entities
{
    public class DisplayPhotoItem
    {
        // Promosyon türü adı
        public string PromotionType { get; set; }
        // Promosyon türü Id
        public int PromotionTypeId { get; set; }

        // Seçilen ürünün adı
        public string ProductName { get; set; }
        // Seçilen ürünün id si
        public int ProductId { get; set; }

        // Fotoğraf (Bitmap formatında)
        public Bitmap PhotoBitmap { get; set; }
    }
}
