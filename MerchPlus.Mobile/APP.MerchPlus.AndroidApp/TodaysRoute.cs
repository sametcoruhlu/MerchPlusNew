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
using SQLite;
using entMerchPlus;
using APP.MerchPlus.Adapters;
using Android.Provider;

namespace APP.MerchPlus
{
    [Activity(Label = "Ziyaret Planý")]
    public class TodaysRoute : Activity
    {

        private ListView mListView;
        private TodaysRouteAdapter insAdapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        protected override void OnResume()
        {
            base.OnResume();
            SetContentView(Resource.Layout.TodaysRoute);

            using (var db = new SQLiteConnection(GlobalVariables.DatabasePath))
            {
                mListView = FindViewById<ListView>(Resource.Id.TodaysRouteListView);
                var listOfMemberRoute = db.Query<entMemberRoute>("SELECT * FROM entMemberRoute WHERE ExitOn IS NULL");
                insAdapter = new TodaysRouteAdapter(this, listOfMemberRoute);
                mListView.Adapter = insAdapter;

                mListView.ItemClick += MListView_ItemClick;
            }
        }

        private void MListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            entMemberRoute insEntMemberRoute = new entMemberRoute();
            insEntMemberRoute = insAdapter.GetItemEntityByPosition(e.Position);

            bool isLocal = false;

            using (var db = new SQLiteConnection(GlobalVariables.DatabasePath))
            {
                entRetailShop insEntRetailShop = db.Query<entRetailShop>("SELECT * FROM entRetailShop WHERE Id=?", insEntMemberRoute.RetailShopId)[0];
                entRetail insEntRetail = db.Query<entRetail>("SELECT * FROM entRetail WHERE Id=?", insEntRetailShop.RetailId)[0];

                if (Convert.ToInt32(insEntRetail.RetailCategoryId) != 1)
                    isLocal = true;
            }

            bool enteredBefore = false;
            #region Check if entered here before
            if (insEntMemberRoute.EntryOn != null)
                enteredBefore = true;
            #endregion

            #region Check if within coordinate (500 meters)
            if (!enteredBefore)
            {

                if (GlobalVariables.MemberLastCoordinate.Latitude == 0 && GlobalVariables.MemberLastCoordinate.Longitude == 0)
                {
                    AlertDialog.Builder alert = new AlertDialog.Builder(this);
                    alert.SetTitle("Koordinatýnýz Alýnamadý");
                    alert.SetMessage("MerchPlus uygulamasýný çalýþtýrdýðýnýzdan bu yana koordinatýnýz elde edilemedi. Lütfen gökyüzünü gören açýk bir alana çýkarak bir süre bekleyiniz ve tekrar giriþ yapmayý deneyiniz.");
                    alert.SetPositiveButton("TAMAM", (senderAlert, args) =>
                    {

                    });
                    alert.Show();
                    return;
                }

                using (var db = new SQLiteConnection(GlobalVariables.DatabasePath))
                {

                    var retailShop = db.Query<entRetailShop>("SELECT * FROM entRetailShop WHERE Id=?", Convert.ToInt32(insEntMemberRoute.RetailShopId));

                    HaversineUtility insGPSUtility = new HaversineUtility();
                    Position insPosRetailShop = new Position();
                    insPosRetailShop.Latitude = Convert.ToDouble(retailShop[0].CoordinateX);
                    insPosRetailShop.Longitude = Convert.ToDouble(retailShop[0].CoordinateY);
                    var distance = insGPSUtility.Distance(insPosRetailShop, GlobalVariables.MemberLastCoordinate, DistanceType.Kilometers);

                    if (distance * 1000 > 500)
                    {
                        AlertDialog.Builder alert = new AlertDialog.Builder(this);
                        alert.SetTitle("UZAKTASINIZ");
                        alert.SetMessage("Bu maðaza için sisteme tanýmlý koordinatýn yaklaþýk olarak " + (distance * 1000).ToString("N2") + " metre uzaðýndasýnýz. Eðer giriþ iþlemini maðazanýn içinde yapýyorsanýz gökyüzünü gören bir alana çýkarak tekrar deneyiniz.");
                        alert.SetPositiveButton("TAMAM", (senderAlert, args) =>
                        {
                            
                        });
                        alert.Show();
                        return;
                    }

                    insEntMemberRoute.EntryOn = DateTime.Now;
                    insEntMemberRoute.EntryCoordinateX = Convert.ToDecimal(retailShop[0].CoordinateX);
                    insEntMemberRoute.EntryCoordinateY = Convert.ToDecimal(retailShop[0].CoordinateY);

                    db.Execute("UPDATE entMemberRoute SET EntryOn=?, EntryCoordinateX=?, EntryCoordinateY=? WHERE Id=?", DateTime.Now, Convert.ToDecimal(retailShop[0].CoordinateX), Convert.ToDecimal(retailShop[0].CoordinateY), insEntMemberRoute.Id);

                    #region There is entry picture, just forward to product selection
                    var customerProductListActivity = new Intent(this, typeof(CustomerProductList));
                    customerProductListActivity.PutExtra("entMemberRouteId", Convert.ToString(insEntMemberRoute.Id));
                    StartActivity(customerProductListActivity);
                    return;
                    #endregion

                    //if (insEntMemberRoute.EntryImageData != null)
                    //{
                    //    #region There is entry picture, just forward to product selection
                    //    var customerProductListActivity = new Intent(this, typeof(CustomerProductList));
                    //    customerProductListActivity.PutExtra("entMemberRouteId", Convert.ToString(insEntMemberRoute.Id));
                    //    StartActivity(customerProductListActivity);
                    //    return;
                    //    #endregion
                    //}
                    //else
                    //{
                    //    #region There is no entry photo, redirect to photo screen
                    //    var shopEntryCameraActivity = new Intent(this, typeof(ShopEntryCameraActivity));
                    //    shopEntryCameraActivity.PutExtra("entMemberRouteId", Convert.ToString(insEntMemberRoute.Id));
                    //    StartActivity(shopEntryCameraActivity);
                    //    return;
                    //    #endregion
                    //}
                }
            }
            #endregion

            #region Already entered before, just take to product listing
            var customerProductListActivity1 = new Intent(this, typeof(CustomerProductList));
            customerProductListActivity1.PutExtra("entMemberRouteId", Convert.ToString(insEntMemberRoute.Id));
            StartActivity(customerProductListActivity1);
            return;
            #endregion


        }

    }
}