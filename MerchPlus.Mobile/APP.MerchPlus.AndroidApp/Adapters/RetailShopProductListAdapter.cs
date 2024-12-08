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
    public class RetailShopProductListAdapter : BaseAdapter<entMemberRouteDetail>
    {

        public List<entMemberRouteDetail> mList = new List<entMemberRouteDetail>();
        private Context mContext;

        public entMemberRouteDetail GetItemEntityByPosition(int position)
        {
            return mList[position];
        }

        public RetailShopProductListAdapter(Context context, List<entMemberRouteDetail> items)
        {
            mContext = context;
            mList = items;
        }

        public override entMemberRouteDetail this[int position]
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
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.RetailShopProductListRow, null, false);
            }

            #region Product Image
            using (var db = new SQLiteConnection(GlobalVariables.DatabasePath))
            {
                try
                {
                    var currentCustomerProduct = mList[position].CustomerProductId;
                    var currentCustomerProductLogoByteString = db.Query<entCustomerProductPicture>("SELECT * FROM entCustomerProductPicture WHERE CustomerProductId=?", currentCustomerProduct);
                    byte[] imageArray = Convert.FromBase64String(currentCustomerProductLogoByteString[0].CustomerProductPictureBase64);
                    Bitmap bitmap1 = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
                    ImageView imgLogo = row.FindViewById<ImageView>(Resource.Id.RetailShopProductList_imgProfilePicturePath);
                    imgLogo.SetImageBitmap(bitmap1);
                }
                catch (Exception ex)
                {
                    
                }
                
            }
            #endregion

            #region Product Name
            using (var db = new SQLiteConnection(GlobalVariables.DatabasePath))
            {
                var currentCustomerProduct = mList[position].CustomerProductId;

                var customerProduct = db.Query<entCustomerProduct>("SELECT * FROM entCustomerProduct WHERE Id=?", currentCustomerProduct);
                TextView txtCustomerProductName = row.FindViewById<TextView>(Resource.Id.RetailShopProductList_txtCustomerProductName);
                txtCustomerProductName.Text = customerProduct[0].Name;
                TextView txtCustomerProductCategory = row.FindViewById<TextView>(Resource.Id.RetailShopProductList_txtCustomerProductCategoryName);

                //var customerProductCategory = db.Query<entCustomerBrandCategory>("SELECT * FROM entCustomerBrandCategory WHERE Id=?", customerProduct[0].CustomerBrandCategoryId);
                txtCustomerProductCategory.Text = "Bilgi giriþi için týklayýn."; // customerProductCategory[0].Name;
            }
            #endregion

            return row;

        }
    }
}