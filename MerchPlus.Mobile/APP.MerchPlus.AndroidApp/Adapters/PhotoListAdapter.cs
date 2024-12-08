using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using APP.MerchPlus.Entities;
using System.Collections.Generic;

namespace APP.MerchPlus.Adapters
{
    public class PhotoListAdapter : BaseAdapter<DisplayPhotoItem>
    {
        private readonly Context _context;
        private readonly List<DisplayPhotoItem> _items;

        public PhotoListAdapter(Context context, List<DisplayPhotoItem> items)
        {
            _context = context;
            _items = items;
        }

        public override DisplayPhotoItem this[int position] => _items[position];

        public override int Count => _items.Count;

        public override long GetItemId(int position) => position;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? LayoutInflater.From(_context).Inflate(Resource.Layout.PhotoListItem, parent, false);

            // Görünüm elemanlarını bağla
            var promotionTypeTextView = view.FindViewById<TextView>(Resource.Id.photo_list_item_promotion_type);
            var promotionTypeIdTextView = view.FindViewById<TextView>(Resource.Id.photo_list_item_promotion_type_id);
            var productNameTextView = view.FindViewById<TextView>(Resource.Id.photo_list_item_product_name);
            var productIdTextView = view.FindViewById<TextView>(Resource.Id.photo_list_item_product_id);
            var photoImageView = view.FindViewById<ImageView>(Resource.Id.photo_list_item_image);

            // Mevcut öğeyi al
            var item = _items[position];

            // Verileri ata
            promotionTypeTextView.Text = $"Promosyon: {item.PromotionType}";
            promotionTypeIdTextView.Text = $"Promosyon ID: {item.PromotionTypeId}";
            productNameTextView.Text = $"Ürün: {item.ProductName}";
            productIdTextView.Text = $"Ürün ID: {item.ProductId}";

            if (item.PhotoBitmap != null)
            {
                photoImageView.SetImageBitmap(item.PhotoBitmap);
            }
            else
            {
                photoImageView.SetImageResource(Android.Resource.Drawable.IcMenuCamera);
            }

            return view;
        }
    }
}
