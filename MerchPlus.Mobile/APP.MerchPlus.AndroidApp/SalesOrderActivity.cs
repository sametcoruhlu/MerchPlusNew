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
    [Activity(Label = "Yeni Sipariþ")]
    public class SalesOrderActivity : Activity
    {
        Intent intent = new Intent(MediaStore.ActionImageCapture);
        string routeId = string.Empty;
        List<entCustomerProduct> listCustomerProduct = new List<entCustomerProduct>();
        entCustomerProduct entCustomerProductSelected = new entCustomerProduct();

        private ListView mListView;
        private SalesOrderPreviewAdapter insAdapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SalesOrderEntry);

            using (var db = new SQLiteConnection(GlobalVariables.DatabasePath))
            {
                routeId = Intent.GetStringExtra("entMemberRouteId");
                var currentMemberRoute = db.Query<entMemberRoute>("SELECT * from entMemberRoute WHERE Id=?", routeId);
                var products = db.Query<entCustomerProduct>("SELECT * from entCustomerProduct WHERE Id IN ( " +
                                                            "SELECT CustomerProductId FROM entMemberRouteDetail WHERE entMemberRouteDetail.MemberRouteId = " + Convert.ToString(routeId) + " " +
                                                            ") AND entCustomerProduct.CustomerId = " + Convert.ToString(currentMemberRoute[0].CustomerId) + " ORDER BY entCustomerProduct.Name");
                listCustomerProduct = products;
                CustomerProductSpinnerAdapter adapter = new CustomerProductSpinnerAdapter(this, products);
                Spinner spinner = FindViewById<Spinner>(Resource.Id.SalesOrderEntry_wleCustomerProductId);
                spinner.Adapter = adapter;
                spinner.ItemSelected += Spinner_ItemSelected;

                Spinner spinnerPackage = FindViewById<Spinner>(Resource.Id.SalesOrderEntry_wleSalesOrderPackageTypeId);
                var adapterPackage = ArrayAdapter.CreateFromResource(
                        this, Resource.Array.salesorderpackagetypearray_array, Android.Resource.Layout.SimpleSpinnerItem);
                adapterPackage.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                spinnerPackage.Adapter = adapterPackage;

            }

            Button btn = FindViewById<Button>(Resource.Id.SalesOrderEntry_btnAdd);
            btn.Click += Btn_Click1;
        }

        private readonly string TAG = "X:" + "SpinnerChanged";

        private void Btn_Click1(object sender, EventArgs e)
        {
            EditText txtQuantity = FindViewById<EditText>(Resource.Id.SalesOrderEntry_txtQuantity);
            Spinner wlePackageType = FindViewById<Spinner>(Resource.Id.SalesOrderEntry_wleSalesOrderPackageTypeId);
            Log.Debug(TAG, Convert.ToString(entCustomerProductSelected.Id));
            Log.Debug(TAG, Convert.ToString(txtQuantity.Text) + " " + Convert.ToString(wlePackageType.SelectedItem.ToString()));

            InputMethodManager inputManager = (InputMethodManager)GetSystemService(Context.InputMethodService);
            inputManager.HideSoftInputFromWindow(CurrentFocus.WindowToken, HideSoftInputFlags.None);

            #region Add Sales Order Line
            List<entSalesOrder> salesOrderCount = new List<entSalesOrder>();
            using (var db = new SQLiteConnection(GlobalVariables.DatabasePath))
            {
                routeId = Intent.GetStringExtra("entMemberRouteId");
                var currentMemberRoute = db.Query<entMemberRoute>("SELECT * from entMemberRoute WHERE Id=?", routeId);
                salesOrderCount = db.Query<entSalesOrder>("SELECT * from entSalesOrder WHERE MemberRouteId=?", routeId);

                if (salesOrderCount.Count == 0)
                {
                    GlobalVariables.currentSalesOrderId = GlobalVariables.currentSalesOrderId + 1;
                    entSalesOrder insEntSalesOrder = new entSalesOrder();
                    insEntSalesOrder.CreatedBy = GlobalVariables.CurrentMember.Id;
                    insEntSalesOrder.CreatedOn = DateTime.Now;
                    insEntSalesOrder.CustomerId = currentMemberRoute[0].CustomerId;
                    insEntSalesOrder.MemberRouteId = Convert.ToInt32(routeId);
                    insEntSalesOrder.SalesOrderStatusId = 1; //Sipriþ alýndý
                    insEntSalesOrder.Id = GlobalVariables.currentSalesOrderId;
                    db.Insert(insEntSalesOrder);
                }

                salesOrderCount = db.Query<entSalesOrder>("SELECT * from entSalesOrder WHERE MemberRouteId=?", routeId);
                var salesOrderDetailCount = db.Query<entSalesOrderDetail>("SELECT * from entSalesOrderDetail WHERE SalesOrderId=?", GlobalVariables.currentSalesOrderId);
                
                entSalesOrderDetail insEntSalesOrderDetail = new entSalesOrderDetail();
                insEntSalesOrderDetail.CreatedBy = salesOrderCount[0].CreatedBy;
                insEntSalesOrderDetail.CreatedOn = salesOrderCount[0].CreatedOn;
                insEntSalesOrderDetail.CustomerProductId = entCustomerProductSelected.Id;
                insEntSalesOrderDetail.Quantity = Convert.ToInt32(txtQuantity.Text);
                insEntSalesOrderDetail.SalesOrderId = GlobalVariables.currentSalesOrderId;
                insEntSalesOrderDetail.Id = salesOrderDetailCount.Count + 1;

                if (wlePackageType.SelectedItem.ToString() == "Adet")
                    insEntSalesOrderDetail.SalesOrderPackageTypeId = 1;
                else if (wlePackageType.SelectedItem.ToString() == "Paket")
                    insEntSalesOrderDetail.SalesOrderPackageTypeId = 2;
                else if (wlePackageType.SelectedItem.ToString() == "Koli")
                    insEntSalesOrderDetail.SalesOrderPackageTypeId = 3;
                else if (wlePackageType.SelectedItem.ToString() == "Palet")
                    insEntSalesOrderDetail.SalesOrderPackageTypeId = 4;

                db.Insert(insEntSalesOrderDetail);

                RefreshOrderPreview();

            }
            #endregion
        }

        private void RefreshOrderPreview()
        {
            List<entSalesOrderPreview> list = new List<entSalesOrderPreview>();
            using (var db = new SQLiteConnection(GlobalVariables.DatabasePath))
            {
                routeId = Intent.GetStringExtra("entMemberRouteId");
                var salesOrderCount = db.Query<entSalesOrder>("SELECT * from entSalesOrder WHERE MemberRouteId=?", routeId);
                list = db.Query<entSalesOrderPreview>("SELECT " +
                                                      "entSalesOrderDetail.Id, " +
                                                      "entCustomerProduct.Name as ProductName, " +
                                                      "entSalesOrderDetail.Quantity || ' ' || entSalesOrderPackageType.Name as PackageTypeName, " +
                                                      "entSalesOrderDetail.Quantity " +
                                                      "FROM entSalesOrderDetail " +
                                                      "     INNER JOIN entCustomerProduct ON entCustomerProduct.Id = entSalesOrderDetail.CustomerProductId " +
                                                      "     INNER JOIN entSalesOrderPackageType ON entSalesOrderPackageType.Id = entSalesOrderDetail.SalesOrderPackageTypeId " +
                                                      "WHERE " +
                                                      "     entSalesOrderDetail.SalesOrderId = ?", salesOrderCount[0].Id);

                mListView = FindViewById<ListView>(Resource.Id.SalesOrderEntry_lstItems);
                insAdapter = new SalesOrderPreviewAdapter(this, list);
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

    #region Custom Spinner
    public class CustomerProductSpinnerAdapter : BaseAdapter
    {
        List<entCustomerProduct> _customerProduct;
        Activity _activity;

        public CustomerProductSpinnerAdapter(Activity activity, List<entCustomerProduct> incomingData)
        {
            _customerProduct = incomingData;
            _activity = activity;
        }

        public override int Count
        {
            get
            {
                return _customerProduct.Count;
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return _customerProduct[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? _activity.LayoutInflater.Inflate(Resource.Layout.CustomerProductListItem, parent, false);
            var productName = view.FindViewById<TextView>(Resource.Id.CustomerProductListItem_txtName);
            productName.Text = _customerProduct[position].Name;
            var barcode = view.FindViewById<TextView>(Resource.Id.CustomerProductListItem_txtBarcode);
            barcode.Text = _customerProduct[position].ProductBarcode;
            return view;
        }
    }
    #endregion
}