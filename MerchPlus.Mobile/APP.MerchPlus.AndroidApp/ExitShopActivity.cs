using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Widget;
using entMerchPlus;
using SQLite;
using System;
using System.Linq;

namespace APP.MerchPlus
{
    [Activity(Label = "Maðazadan Çýkýþ")]
    public class ExitShopActivity : Activity
    {

        Intent intent = new Intent(MediaStore.ActionImageCapture);
        string routeId = string.Empty;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ExitShop);
            ImageButton btn = FindViewById<ImageButton>(Resource.Id.ExitShop_btnExit);
            btn.Click += Btn_Click;
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            #region Check Coordinate
            routeId = Intent.GetStringExtra("entMemberRouteId");
            entMemberRoute insEntMemberRoute = new entMemberRoute();
            using (var db = new SQLiteConnection(GlobalVariables.DatabasePath))
            {
                entMemberRoute memberRoute = db.Query<entMemberRoute>("SELECT * FROM entMemberRoute WHERE ExitOn IS NULL and Id =?", Convert.ToInt32(routeId)).FirstOrDefault();
                insEntMemberRoute = memberRoute;
            }


            #region Check if within coordinate (500 meters)

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
                    alert.SetMessage("Bu maðaza için sisteme tanýmlý koordinatýn yaklaþýk olarak " + (distance * 1000).ToString("N2") + " metre uzaðýndasýnýz. Maðazadan çýkýþ yapabilmek için maðaza yakýnýnda bulunmalýsýnýz.");
                    alert.SetPositiveButton("TAMAM", (senderAlert, args) =>
                    {

                    });
                    alert.Show();
                    return;
                }
            }
            #endregion
            #endregion

            using (var db = new SQLiteConnection(GlobalVariables.DatabasePath))
            {
                #region Check if there are at least 2 photos in this shop (1 for entry, one for raf düzeni)
                routeId = Intent.GetStringExtra("entMemberRouteId");

                var photos = db.Query<entMemberRoutePhoto>("SELECT * FROM entMemberRoutePhoto WHERE MemberRouteId=?", Convert.ToInt32(routeId));
                if (photos.Count < 1)
                {
                    AlertDialog.Builder alert = new AlertDialog.Builder(this);
                    alert.SetTitle("Raf Düzeni Fotoðrafý Eksik");
                    alert.SetMessage("Maðazaya giriþinizi fotoðraf çekerek belgelediniz fakat raf düzeninize ait fotoðraf yok.\r\n\r\nÖncelikle fotoðraf çekiniz.");
                    alert.SetPositiveButton("TAMAM", (senderAlert, args) =>
                    {
                        this.Finish();
                    });

                    //run the alert in UI thread to display in the screen
                    alert.Show();
                    return;
                }
                #endregion

                #region Check if there are unentered products

                try
                {
                    var unenteredProducts = db.Query<entMemberRouteDetail>("SELECT * FROM entMemberRouteDetail WHERE CreatedOn IS NULL AND MemberRouteId=?", Convert.ToInt32(routeId));
                    if (unenteredProducts.Count != 0)
                    {
                        AlertDialog.Builder alert = new AlertDialog.Builder(this);
                        alert.SetTitle("Eksik Veri Giriþi");
                        alert.SetMessage("Toplam " + unenteredProducts.Count.ToString() + " ürünün veri giriþini yapmadýnýz. Çýkmanýz halinde eksik ürün giriþi yaptýðýnýz yöneticinize bildirilecektir.\r\n\r\nYine de çýkmak istiyor musunuz?");
                        alert.SetPositiveButton("VAZGEÇ", (senderAlert, args) =>
                        {
                            this.Finish();
                        });
                        alert.SetNegativeButton("ÇIKIÞI ONAYLIYORUM", (senderAlert, args) =>
                        {
                            #region Update MemberRoute table
                            SQLiteConnection db1 = new SQLiteConnection(GlobalVariables.DatabasePath);
                            db1.Execute("UPDATE entMemberRoute SET ExitOn=?, IsSentToServer=0 WHERE Id=?", DateTime.Now, Convert.ToInt32(routeId));

                            var retailShopSelect = new Intent(this, typeof(MainMenu));
                            StartActivity(retailShopSelect);
                            this.Finish();
                            #endregion
                        });

                        //run the alert in UI thread to display in the screen
                        alert.Show();
                        return;
                    }
                    else
                    {
                        #region Update MemberRoute table
                        SQLiteConnection db1 = new SQLiteConnection(GlobalVariables.DatabasePath);
                        db1.Execute("UPDATE entMemberRoute SET ExitOn=?, IsSentToServer=0 WHERE Id=?", DateTime.Now, Convert.ToInt32(routeId));

                        var retailShopSelect = new Intent(this, typeof(MainMenu));
                        StartActivity(retailShopSelect);
                        this.Finish();
                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    SQLiteConnection db1 = new SQLiteConnection(GlobalVariables.DatabasePath);
                    db1.Execute("UPDATE entMemberRoute SET ExitOn=?, IsSentToServer=0 WHERE Id=?", DateTime.Now, Convert.ToInt32(routeId));

                    var retailShopSelect = new Intent(this, typeof(MainMenu));
                    StartActivity(retailShopSelect);
                    this.Finish();
                }



                #endregion
            }
        }
    }
}