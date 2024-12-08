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
    public class SalesOrderPreviewAdapter : BaseAdapter<entSalesOrderPreview>
    {

        public List<entSalesOrderPreview> mList = new List<entSalesOrderPreview>();
        private Context mContext;
        public EventHandler ListChanged;

        public SalesOrderPreviewAdapter(Context context, List<entSalesOrderPreview> items)
        {
            mContext = context;
            mList = items;
            this.ListChanged += ListChangedEvent;
        }

        public void ListChangedEvent(object sender, EventArgs e) {

        }

        public override entSalesOrderPreview this[int position]
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
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.SalesOrderPreviewRow, null, false);
            }

            TextView txtName = row.FindViewById<TextView>(Resource.Id.SalesOrderPreview_txtName);
            txtName.Text = mList[position].ProductName;

            TextView txtQuantity = row.FindViewById<TextView>(Resource.Id.SalesOrderPreview_txtQuantity);
            txtQuantity.Text = mList[position].PackageTypeName;

            Button btnDelete = row.FindViewById<Button>(Resource.Id.SalesOrderPreview_btnDelete);
            btnDelete.Tag = mList[position].Id;
            btnDelete.Click += BtnDelete_Click;

            return row;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            Log.Debug("Sales Order Item Deleted > ", Convert.ToString("Delete : " + ((Button)sender).Tag));
            using (var db = new SQLiteConnection(GlobalVariables.DatabasePath))
            {
                db.Execute("DELETE FROM entSalesOrderDetail WHERE Id=?", Convert.ToInt32(((Button)sender).Tag));
                this.ListChanged(this, new EventArgs() { });
            }
        }
    }
}