using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using entMerchPlus;
using SQLite;
using Android.Graphics;
using APP.MerchPlus.Entities;
using System.Globalization;

namespace APP.MerchPlus.Adapters
{
    public class TodaysRouteAdapter : BaseAdapter<entMemberRoute>
    {

        public List<entMemberRoute> mList = new List<entMemberRoute>();
        private Context mContext;

        public entMemberRoute GetItemEntityByPosition(int position)
        {
            return mList[position];
        }

        public TodaysRouteAdapter(Context context, List<entMemberRoute> items)
        {
            mContext = context;
            mList = items;
        }

        public override entMemberRoute this[int position]
        {
            get
            {
                return mList[position];
            }
        }

        public override int Count
        {
            get
            {
                return mList.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;
            if (row == null)
            {
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.TodaysRouteRow, null, false);
            }

            #region Retail Shop Name
            using (var db = new SQLiteConnection(GlobalVariables.DatabasePath))
            {
                var currentRetailShop = db.Query<entRetailShop>("SELECT * FROM entRetailShop WHERE Id=?", mList[position].RetailShopId);

                TextView txtRetailShopName = row.FindViewById<TextView>(Resource.Id.TodaysRoute_txtRetailShopName);
                txtRetailShopName.Text = currentRetailShop[0].Name;

                TextView txtRetailShopAddress = row.FindViewById<TextView>(Resource.Id.TodaysRoute_txtRetailShopAddress);
                txtRetailShopAddress.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(currentRetailShop[0].Address.ToLowerInvariant());

                var currentRetailLogoByteString = db.Query<entRetailProfilePicture>("SELECT * FROM entRetailProfilePicture WHERE RetailId=?", currentRetailShop[0].RetailId);
                byte[] imageArray = Convert.FromBase64String(currentRetailLogoByteString[0].ProfilePictureBase64);
                Bitmap bitmap1 = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
                ImageView imgLogo = row.FindViewById<ImageView>(Resource.Id.TodaysRoute_imgProfilePicturePath);
                imgLogo.SetImageBitmap(bitmap1);
            }
            #endregion

            return row;

        }
    }
}