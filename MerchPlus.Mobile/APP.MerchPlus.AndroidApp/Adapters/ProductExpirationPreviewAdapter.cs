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
using APP.MerchPlus.Entities;
using Android.Util;
using SQLite;

namespace APP.MerchPlus.Adapters
{
    public class ProductExpirationPreviewAdapter : BaseAdapter<entExpirationDatePreview>
    {

        public List<entExpirationDatePreview> mList = new List<entExpirationDatePreview>();
        private Context mContext;
        public EventHandler ListChanged;

        public ProductExpirationPreviewAdapter(Context context, List<entExpirationDatePreview> items)
        {
            mContext = context;
            mList = items;
            this.ListChanged += ListChangedEvent;
        }

        public void ListChangedEvent(object sender, EventArgs e) {

        }

        public override entExpirationDatePreview this[int position]
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
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.ProductExpirationPreviewRow, null, false);
            }

            TextView txtName = row.FindViewById<TextView>(Resource.Id.ProductExpirationPreview_txtName);
            txtName.Text = mList[position].ProductName;

            TextView txtQuantity = row.FindViewById<TextView>(Resource.Id.ProductExpirationPreview_txtQuantity);
            txtQuantity.Text = Convert.ToString(mList[position].Quantity) + " adet \r\nSKT: " + Convert.ToString(mList[position].ExpirationDate.Value.ToString("dd.MM.yyyy"));

            Button btnDelete = row.FindViewById<Button>(Resource.Id.ProductExpirationPreview_btnDelete);
            entExpirationDatePreview myObj = mList[position];
            btnDelete.Tag = new JavaObjectWrapper<entExpirationDatePreview>() { Obj = myObj };
            btnDelete.Click += BtnDelete_Click;

            return row;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            Log.Debug("Expiration Date Item Deleted > ", Convert.ToString("Delete : " + ((Button)sender).Tag));
            using (var db = new SQLiteConnection(GlobalVariables.DatabasePath))
            {
                dynamic clickedObject = ((Button)sender).Tag;
                entExpirationDatePreview insPrev = clickedObject.Obj;
                db.Execute("DELETE FROM entProductExpiration WHERE MemberRouteId=? AND CustomerProductId=? AND Quantity=? AND ExpirationDate=?", Convert.ToInt32(insPrev.MemberRouteId), Convert.ToInt32(insPrev.ProductId), Convert.ToInt32(insPrev.Quantity), Convert.ToDateTime(insPrev.ExpirationDate));
                this.ListChanged(this, new EventArgs() { });
            }
        }
    }
}