using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using Android.Telephony;
using System.Net.Http;
using System.Net.Http.Headers;
using entMerchPlus;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;
using static Android.Views.ViewGroup;
using System.IO;
using SQLite;
using APP.MerchPlus.Entities;
using System.Collections.Generic;
using System.Net;
using Android.Graphics;
using Android.Provider;
using Android.Graphics.Drawables;
using System.Data;
using Java.Lang;
using Android.Util;
using APP.MerchPlus.Adapters;
using Android.Views.InputMethods;

namespace APP.MerchPlus
{
    [Activity(Label = "SKT Raporlama")]
    public class ExpirationDateActivity : Activity
    {
        Intent intent = new Intent(MediaStore.ActionImageCapture);
        string routeId = string.Empty;
        List<entCustomerProduct> listCustomerProduct = new List<entCustomerProduct>();
        entCustomerProduct entCustomerProductSelected = new entCustomerProduct();

        private ListView mListView;
        private ProductExpirationPreviewAdapter insAdapter;


        EditText txtYear;
        EditText txtMonth;
        EditText txtDay;
        EditText txtQuantity;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ExpirationDateEntry);

            using (var db = new SQLiteConnection(GlobalVariables.DatabasePath))
            {
                routeId = Intent.GetStringExtra("entMemberRouteId");
                var currentMemberRoute = db.Query<entMemberRoute>("SELECT * from entMemberRoute WHERE Id=?", routeId);
                var products = db.Query<entCustomerProduct>("SELECT * from entCustomerProduct WHERE Id IN ( " +
                                                            "SELECT CustomerProductId FROM entMemberRouteDetail WHERE entMemberRouteDetail.MemberRouteId = " + Convert.ToString(routeId) + " " +
                                                            ") AND entCustomerProduct.CustomerId = " + Convert.ToString(currentMemberRoute[0].CustomerId) + " ORDER BY entCustomerProduct.Name");
                listCustomerProduct = products;

                entCustomerProduct insEntCustomerProduct_Empty = new entCustomerProduct();
                insEntCustomerProduct_Empty.Name = "Seçiniz";
                insEntCustomerProduct_Empty.Id = -1;
                listCustomerProduct.Insert(0, insEntCustomerProduct_Empty);

                CustomerProductSpinnerAdapter adapter = new CustomerProductSpinnerAdapter(this, products);
                Spinner spinner = FindViewById<Spinner>(Resource.Id.ExpirationDateEntry_wleCustomerProductId);
                spinner.Adapter = adapter;
                spinner.ItemSelected += Spinner_ItemSelected;
                
            }

            Button btn = FindViewById<Button>(Resource.Id.ExpirationDateEntry_btnAdd);
            btn.Click += Btn_Click1;

            RefreshOrderPreview();

            txtYear = FindViewById<EditText>(Resource.Id.ExpirationDateEntry_txtYear);
            txtMonth = FindViewById<EditText>(Resource.Id.ExpirationDateEntry_txtMonth);
            txtDay = FindViewById<EditText>(Resource.Id.ExpirationDateEntry_txtDay);
            txtQuantity = FindViewById<EditText>(Resource.Id.ExpirationDateEntry_txtQuantity);

            txtDay.TextChanged += TxtDay_TextChanged;
            txtMonth.TextChanged += TxtMonth_TextChanged;
            txtYear.TextChanged += TxtYear_TextChanged;
        }

        private void TxtYear_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            if (txtYear.Text.Length == 4)
                txtQuantity.RequestFocus();
        }

        private void TxtDay_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            if (txtDay.Text.Length == 2)
                txtMonth.RequestFocus();
        }

        private void TxtMonth_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            if (txtMonth.Text.Length == 2)
                txtYear.RequestFocus();
        }

        private readonly string TAG = "X:" + "SpinnerChanged";

        private void Btn_Click1(object sender, EventArgs e)
        {
            Log.Debug(TAG, Convert.ToString(entCustomerProductSelected.Id));

            InputMethodManager inputManager = (InputMethodManager)GetSystemService(Context.InputMethodService);
            inputManager.HideSoftInputFromWindow(CurrentFocus.WindowToken, HideSoftInputFlags.None);


            #region Validate
            string errorMessage = string.Empty;
            if (entCustomerProductSelected.Id == -1)
            {
                errorMessage += "* Ürün seçiniz!\r\n\r\n";
            }
            try
            {
                DateTime insDateTime = new DateTime(Convert.ToInt32(txtYear.Text), Convert.ToInt32(txtMonth.Text), Convert.ToInt32(txtDay.Text));
                if (insDateTime.Year < DateTime.Now.Year - 1)
                {
                    errorMessage += "* Ürün SKT bir yýldan daha eski olamaz!\r\n\r\n";
                }
            }
            catch (System.Exception ex)
            {
                errorMessage += "* Tarihi gün.ay.yýl formatýnda giriniz. Örnek : 31.12.2017\r\n\r\n";
            }
            if (txtQuantity.Text == string.Empty)
            {
                errorMessage += "* Adet boþ býrakýlamaz!";
            }
            else
            {
                if (Convert.ToInt32(txtQuantity.Text) == 0)
                    errorMessage += "* Ürün adedi 0 olamaz!\r\n\r\n";
            }

            if (errorMessage != string.Empty)
            {
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("Eksikleriniz var...");
                alert.SetMessage("Aþaðýda listelenen sorunlarý düzelttikten sonra tekrar deneyin.\r\n\r\n" + errorMessage);
                alert.SetPositiveButton("TAMAM", (senderAlert, args) =>
                {

                });
                alert.Show();
                return;
            }
            #endregion

            #region Add Expiration Line
            List<entProductExpiration> productExpirationCount = new List<entProductExpiration>();
            using (var db = new SQLiteConnection(GlobalVariables.DatabasePath))
            {
                routeId = Intent.GetStringExtra("entMemberRouteId");
                var currentMemberRoute = db.Query<entMemberRoute>("SELECT * from entMemberRoute WHERE Id=?", routeId);
                productExpirationCount = db.Query<entProductExpiration>("SELECT * from entProductExpiration WHERE MemberRouteId=?", routeId);

                entProductExpiration insEntProductExpiration_New = new entProductExpiration();
                insEntProductExpiration_New.CreatedOn = DateTime.Now;
                insEntProductExpiration_New.CustomerId = currentMemberRoute[0].CustomerId;
                insEntProductExpiration_New.CustomerProductId = entCustomerProductSelected.Id;
                insEntProductExpiration_New.IsSentToServer = false;
                insEntProductExpiration_New.MemberId = GlobalVariables.CurrentMember.Id;
                insEntProductExpiration_New.MemberRouteId = currentMemberRoute[0].Id;
                insEntProductExpiration_New.Quantity = Convert.ToInt32(txtQuantity.Text);
                insEntProductExpiration_New.ExpirationDate = new DateTime(Convert.ToInt32(txtYear.Text), Convert.ToInt32(txtMonth.Text), Convert.ToInt32(txtDay.Text));

                db.Insert(insEntProductExpiration_New);

                RefreshOrderPreview();

            }
            #endregion
        }

        private void RefreshOrderPreview()
        {
            List<entExpirationDatePreview> list = new List<entExpirationDatePreview>();
            using (var db = new SQLiteConnection(GlobalVariables.DatabasePath))
            {
                routeId = Intent.GetStringExtra("entMemberRouteId");
                var salesOrderCount = db.Query<entProductExpiration>("SELECT * from entProductExpiration WHERE MemberRouteId=?", routeId);
                list = db.Query<entExpirationDatePreview>("SELECT " +
                                                      "entProductExpiration.Id, " +
                                                      "entCustomerProduct.Name as ProductName, " +
                                                      "entCustomerProduct.Id as ProductId, " +
                                                      "entProductExpiration.MemberRouteId as MemberRouteId, " +
                                                      "entProductExpiration.Quantity as [Quantity], " +
                                                      "entProductExpiration.ExpirationDate " +
                                                      "FROM entProductExpiration " +
                                                      "     INNER JOIN entCustomerProduct ON entCustomerProduct.Id = entProductExpiration.CustomerProductId " +
                                                      "WHERE " +
                                                      "     entProductExpiration.MemberRouteId = ?", Convert.ToInt32(routeId));

                mListView = FindViewById<ListView>(Resource.Id.ExpirationDateEntry_lstItems);
                insAdapter = new ProductExpirationPreviewAdapter(this, list);
                insAdapter.ListChanged += ListChangedEvent;
                mListView.Adapter = insAdapter;

            }
        }

        private void ListChangedEvent(object sender, EventArgs e) {
            RefreshOrderPreview();
        }

        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            entCustomerProductSelected = listCustomerProduct[e.Position];
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            //using (var db = new SQLiteConnection(GlobalVariables.DatabasePath))
            //{
            //    #region Check if there are at least 2 photos in this shop (1 for entry, one for raf düzeni)
            //    routeId = Intent.GetStringExtra("entMemberRouteId");

            //    var photos = db.Query<entMemberRoutePhoto>("SELECT * FROM entMemberRoutePhoto WHERE MemberRouteId=?", Convert.ToInt32(routeId));
            //    if (photos.Count < 1)
            //    {
            //        AlertDialog.Builder alert = new AlertDialog.Builder(this);
            //        alert.SetTitle("Raf Düzeni Fotoðrafý Eksik");
            //        alert.SetMessage("Maðazaya giriþinizi fotoðraf çekerek belgelediniz fakat raf düzeninize ait fotoðraf yok.\r\n\r\nÖncelikle fotoðraf çekiniz.");
            //        alert.SetPositiveButton("TAMAM", (senderAlert, args) =>
            //        {
            //            this.Finish();
            //        });

            //        //run the alert in UI thread to display in the screen
            //        alert.Show();
            //        return;
            //    }
            //    #endregion

            //    #region Check if there are unentered products

            //    try
            //    {
            //        var unenteredProducts = db.Query<entMemberRouteDetail>("SELECT * FROM entMemberRouteDetail WHERE CreatedOn IS NULL AND MemberRouteId=?", Convert.ToInt32(routeId));
            //        if (unenteredProducts.Count != 0)
            //        {
            //            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            //            alert.SetTitle("Eksik Veri Giriþi");
            //            alert.SetMessage("Toplam " + unenteredProducts.Count.ToString() + " ürünün veri giriþini yapmadýnýz. Çýkmanýz halinde eksik ürün giriþi yaptýðýnýz yöneticinize bildirilecektir.\r\n\r\nYine de çýkmak istiyor musunuz?");
            //            alert.SetPositiveButton("VAZGEÇ", (senderAlert, args) =>
            //            {
            //                this.Finish();
            //            });
            //            alert.SetNegativeButton("ÇIKIÞI ONAYLIYORUM", (senderAlert, args) =>
            //            {
            //                #region Update MemberRoute table
            //                SQLiteConnection db1 = new SQLiteConnection(GlobalVariables.DatabasePath);
            //                db1.Execute("UPDATE entMemberRoute SET ExitOn=?, IsSentToServer=0 WHERE Id=?", DateTime.Now, Convert.ToInt32(routeId));

            //                var retailShopSelect = new Intent(this, typeof(MainMenu));
            //                StartActivity(retailShopSelect);
            //                this.Finish();
            //                #endregion
            //            });

            //            //run the alert in UI thread to display in the screen
            //            alert.Show();
            //            return;
            //        }
            //        else
            //        {
            //            #region Update MemberRoute table
            //            SQLiteConnection db1 = new SQLiteConnection(GlobalVariables.DatabasePath);
            //            db1.Execute("UPDATE entMemberRoute SET ExitOn=?, IsSentToServer=0 WHERE Id=?", DateTime.Now, Convert.ToInt32(routeId));

            //            var retailShopSelect = new Intent(this, typeof(MainMenu));
            //            StartActivity(retailShopSelect);
            //            this.Finish();
            //            #endregion
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        SQLiteConnection db1 = new SQLiteConnection(GlobalVariables.DatabasePath);
            //        db1.Execute("UPDATE entMemberRoute SET ExitOn=?, IsSentToServer=0 WHERE Id=?", DateTime.Now, Convert.ToInt32(routeId));

            //        var retailShopSelect = new Intent(this, typeof(MainMenu));
            //        StartActivity(retailShopSelect);
            //        this.Finish();
            //    }

                

            //    #endregion
            //}
        }
    }
}