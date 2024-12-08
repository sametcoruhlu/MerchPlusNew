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
using APP.MerchPlus.Adapters;
using SQLite;
using entMerchPlus;

namespace APP.MerchPlus
{
    [Activity(Label = "Ürün Listesi")]
    public class CustomerProductList : Activity
    {
        private ListView mListView;
        private RetailShopProductListAdapter insAdapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        private void BtnTakePhoto_Click(object sender, EventArgs e)
        {
            var shopEntryVitrineCameraActivity = new Intent(this, typeof(ShopEntryVitrineCameraActivity));
            shopEntryVitrineCameraActivity.PutExtra("entMemberRouteId", Intent.GetStringExtra("entMemberRouteId"));
            StartActivity(shopEntryVitrineCameraActivity);
        }

        protected override void OnResume()
        {
            base.OnResume();
            SetContentView(Resource.Layout.CustomerProductList);

            ImageButton btnTakePhoto = FindViewById<ImageButton>(Resource.Id.ShopEntryCamera_btnTakePhotoVitrine);
            btnTakePhoto.Click += BtnTakePhoto_Click;

            ImageButton btnExitShop = FindViewById<ImageButton>(Resource.Id.ShopEntryCamera_btnExitShop);
            btnExitShop.Click += BtnExitShop_Click;

            ImageButton btnSalesOrderEntry = FindViewById<ImageButton>(Resource.Id.ShopEntryCamera_btnNewSalesOrder);
            btnSalesOrderEntry.Click += BtnSalesOrderEntry_Click;

            ImageButton btnExpirationDate = FindViewById<ImageButton>(Resource.Id.ShopEntryCamera_btnNewExpirationDate);
            btnExpirationDate.Click += BtnExpirationDate_Click;

            using (var db = new SQLiteConnection(GlobalVariables.DatabasePath))
            {
                mListView = FindViewById<ListView>(Resource.Id.CustomerProductListView);
                var listOfMemberRouteDetail = db.Query<entMemberRouteDetail>("SELECT * FROM entMemberRouteDetail WHERE MemberRouteId=? AND CreatedOn IS NULL", Convert.ToInt32(Intent.GetStringExtra("entMemberRouteId")));
                insAdapter = new RetailShopProductListAdapter(this, listOfMemberRouteDetail);
                mListView.Adapter = insAdapter;

                mListView.ItemClick += MListView_ItemClick;
            }
        }

        private void BtnSalesOrderEntry_Click(object sender, EventArgs e)
        {
            var salesOrderActivity = new Intent(this, typeof(SalesOrderActivity));
            salesOrderActivity.PutExtra("entMemberRouteId", Intent.GetStringExtra("entMemberRouteId"));
            StartActivity(salesOrderActivity);
        }

        private void BtnExpirationDate_Click(object sender, EventArgs e)
        {
            var expirationDateActivity = new Intent(this, typeof(ExpirationDateActivity));
            expirationDateActivity.PutExtra("entMemberRouteId", Intent.GetStringExtra("entMemberRouteId"));
            StartActivity(expirationDateActivity);
        }

        private void BtnExitShop_Click(object sender, EventArgs e)
        {
            var exitShopActivity = new Intent(this, typeof(ExitShopActivity));
            exitShopActivity.PutExtra("entMemberRouteId", Intent.GetStringExtra("entMemberRouteId"));
            StartActivity(exitShopActivity);
        }

        private void MListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            entMemberRouteDetail insEntMemberRouteDetail = new entMemberRouteDetail();
            insEntMemberRouteDetail = insAdapter.GetItemEntityByPosition(e.Position);

            var merchDataEntryActivity = new Intent(this, typeof(MerchDataEntryActivity));
            merchDataEntryActivity.PutExtra("entMemberRouteDetailId", Convert.ToString(insEntMemberRouteDetail.Id));
            StartActivity(merchDataEntryActivity);
        }
    }
}