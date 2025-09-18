using Microsoft.Maui;
#if ANDROID
using Android.Graphics;
#endif

namespace MerchPlusMAUI.Entities
{
    public class DisplayPhotoItem
    {
        // Promosyon türü adı
        public required string PromotionType { get; set; }
        // Promosyon türü Id
        public int PromotionTypeId { get; set; }

        // Seçilen ürünün adı
        public required string ProductName { get; set; }
        // Seçilen ürünün id si
        public int ProductId { get; set; }

        // Fotoğraf
        public required string PhotoData { get; set; }

        #if ANDROID
        private Bitmap? _photoBitmap;

        public Bitmap? PhotoBitmap
        {
            get
            {
                if (_photoBitmap == null && !string.IsNullOrEmpty(PhotoData))
                {
                    try
                    {
                        byte[] bytes = Convert.FromBase64String(PhotoData);
                        _photoBitmap = BitmapFactory.DecodeByteArray(bytes, 0, bytes.Length);
                    }
                    catch
                    {
                        _photoBitmap = null;
                    }
                }
                return _photoBitmap;
            }
        }

        public byte[]? GetCompressedBytes()
        {
            if (PhotoBitmap == null) return null;

            using var stream = new MemoryStream();
            PhotoBitmap.Compress(Bitmap.CompressFormat.Jpeg!, 80, stream);
            return stream.ToArray();
        }
        #endif
    }
}
