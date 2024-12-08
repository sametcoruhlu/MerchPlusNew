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
    [Activity(Label = "Ma�azadan ��k��")]
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
                alert.SetTitle("Koordinat�n�z Al�namad�");
                alert.SetMessage("MerchPlus uygulamas�n� �al��t�rd���n�zdan bu yana koordinat�n�z elde edilemedi. L�tfen g�ky�z�n� g�ren a��k bir alana ��karak bir s�re bekleyiniz ve tekrar giri� yapmay� deneyiniz.");
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
                    alert.SetMessage("Bu ma�aza i�in sisteme tan�ml� koordinat�n yakla��k olarak " + (distance * 1000).ToString("N2") + " metre uza��ndas�n�z. Ma�azadan ��k�� yapabilmek i�in ma�aza yak�n�nda bulunmal�s�n�z.");
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
                #region Check if there are at least 2 photos in this shop (1 for entry, one for raf d�zeni)
                routeId = Intent.GetStringExtra("entMemberRouteId");

                var photos = db.Query<entMemberRoutePhoto>("SELECT * FROM entMemberRoutePhoto WHERE MemberRouteId=?", Convert.ToInt32(routeId));
                if (photos.Count < 1)
                {
                    AlertDialog.Builder alert = new AlertDialog.Builder(this);
                    alert.SetTitle("Raf D�zeni Foto�raf� Eksik");
                    alert.SetMessage("Ma�azaya giri�inizi foto�raf �ekerek belgelediniz fakat raf d�zeninize ait foto�raf yok.\r\n\r\n�ncelikle foto�raf �ekiniz.");
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
                        alert.SetTitle("Eksik Veri Giri�i");
                        alert.SetMessage("Toplam " + unenteredProducts.Count.ToString() + " �r�n�n veri giri�ini yapmad�n�z. ��kman�z halinde eksik �r�n giri�i yapt���n�z y�neticinize bildirilecektir.\r\n\r\nYine de ��kmak istiyor musunuz?");
                        alert.SetPositiveButton("VAZGE�", (senderAlert, args) =>
                        {
                            this.Finish();
                        });
                        alert.SetNegativeButton("�IKI�I ONAYLIYORUM", (senderAlert, args) =>
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