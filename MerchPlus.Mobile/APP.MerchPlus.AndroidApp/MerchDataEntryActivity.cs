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
using APP.MerchPlus.Entities;
using Android.Graphics;

namespace APP.MerchPlus
{
    [Activity(Label = "Veri Giriþi Yapýnýz")]
    public class MerchDataEntryActivity : Activity
    {
        int currentId = 0;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MerchDataEntry);

            Spinner spinner = FindViewById<Spinner>(Resource.Id.MerchDataEntry_wlePromotionTypeName);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.promotiontypearray_array, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            Spinner spinner2 = FindViewById<Spinner>(Resource.Id.MerchDataEntry_wleActivityTypeName);
            var adapter2 = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.activitytypearray_array, Android.Resource.Layout.SimpleSpinnerItem);
            adapter2.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner2.Adapter = adapter2;
            spinner2.ItemSelected += Spinner2_ItemSelected;
        }

        private void Spinner2_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner2 = FindViewById<Spinner>(Resource.Id.MerchDataEntry_wleActivityTypeName);
            if (Convert.ToString(spinner2.SelectedItem) == "FIYAT INDIRIMI")
            {
                LinearLayout insPnl = FindViewById<LinearLayout>(Resource.Id.MerchDataEntry_pnlActivityPrice);
                insPnl.Visibility = ViewStates.Visible;
            }
            else
            {
                LinearLayout insPnl = FindViewById<LinearLayout>(Resource.Id.MerchDataEntry_pnlActivityPrice);
                insPnl.Visibility = ViewStates.Gone;
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            using (var db = new SQLiteConnection(GlobalVariables.DatabasePath))
            {
                currentId = Convert.ToInt32(Intent.GetStringExtra("entMemberRouteDetailId"));
                var routeDetail = db.Query<entMemberRouteDetail>("SELECT * FROM entMemberRouteDetail WHERE Id=?", currentId);
                var route = db.Query<entMemberRoute>("SELECT * FROM entMemberRoute WHERE Id=?", routeDetail[0].MemberRouteId);
                var retailShop = db.Query<entRetailShop>("SELECT * FROM entRetailShop WHERE Id=?", route[0].RetailShopId);
                var product = db.Query<entRetailShop>("SELECT * FROM entCustomerProduct WHERE Id=?", routeDetail[0].CustomerProductId);

                TextView txtRetailShopName = FindViewById<TextView>(Resource.Id.MerchDataEntry_lblRetailShopName);
                TextView txtRetailProductName = FindViewById<TextView>(Resource.Id.MerchDataEntry_lblCustomerProductName);
                ImageView pctPicture = FindViewById<ImageView>(Resource.Id.MerchDataEntry_pctCustomerProductPhoto);

                EditText txtFaceCount = FindViewById<EditText>(Resource.Id.MerchDataEntry_txtFaceCount);
                EditText txtShelfIndex = FindViewById<EditText>(Resource.Id.MerchDataEntry_txtShelfIndex);
                EditText txtShelfStock = FindViewById<EditText>(Resource.Id.MerchDataEntry_txtShelfStok);
                EditText txtShelfPrice1 = FindViewById<EditText>(Resource.Id.MerchDataEntry_txtShelfPrice1);
                EditText txtShelfPrice2 = FindViewById<EditText>(Resource.Id.MerchDataEntry_txtShelfPrice2);
                EditText txtWarehouseStock = FindViewById<EditText>(Resource.Id.MerchDataEntry_txtWarehouseStock);

                txtRetailShopName.Text = retailShop[0].Name;
                txtRetailProductName.Text = product[0].Name;
                txtFaceCount.Text = Convert.ToString((routeDetail[0].FaceCount == null ? 0 : routeDetail[0].FaceCount.Value));
                txtShelfIndex.Text = Convert.ToString((routeDetail[0].ShelfIndex == null ? 0 : routeDetail[0].ShelfIndex.Value));
                txtShelfStock.Text = Convert.ToString((routeDetail[0].ShelfStock == null ? 0 : routeDetail[0].ShelfStock.Value));
                txtShelfPrice1.Text = Convert.ToString((routeDetail[0].ShelfPrice == null ? 0 : Math.Truncate(routeDetail[0].ShelfPrice.Value)));
                txtShelfPrice2.Text = Convert.ToString((routeDetail[0].ShelfPrice == null ? "00" : Convert.ToInt32((routeDetail[0].ShelfPrice.Value - Math.Truncate(routeDetail[0].ShelfPrice.Value)) * 100).ToString()));
                txtWarehouseStock.Text = Convert.ToString((routeDetail[0].WarehouseStock == null ? 0 : routeDetail[0].WarehouseStock.Value));

                var currentCustomerProductLogoByteString = db.Query<entCustomerProductPicture>("SELECT * FROM entCustomerProductPicture WHERE CustomerProductId=?", product[0].Id);
                byte[] imageArray = Convert.FromBase64String(currentCustomerProductLogoByteString[0].CustomerProductPictureBase64);
                Bitmap bitmap1 = BitmapFactory.DecodeByteArray(imageArray, 0, imageArray.Length);
                pctPicture.SetImageBitmap(bitmap1);
            }

            Button btnSave = FindViewById<Button>(Resource.Id.MerchDataEntry_btnSave);
            btnSave.Click += BtnSave_Click;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string errorMessage = string.Empty;
            #region Find Controls
            EditText txtFaceCount = FindViewById<EditText>(Resource.Id.MerchDataEntry_txtFaceCount);
            EditText txtShelfIndex = FindViewById<EditText>(Resource.Id.MerchDataEntry_txtShelfIndex);
            EditText txtShelfStock = FindViewById<EditText>(Resource.Id.MerchDataEntry_txtShelfStok);
            EditText txtShelfPrice1 = FindViewById<EditText>(Resource.Id.MerchDataEntry_txtShelfPrice1);
            EditText txtShelfPrice2 = FindViewById<EditText>(Resource.Id.MerchDataEntry_txtShelfPrice2);
            Spinner wlePromotionTypeName = FindViewById<Spinner>(Resource.Id.MerchDataEntry_wlePromotionTypeName);
            Spinner wleActivityTypeName = FindViewById<Spinner>(Resource.Id.MerchDataEntry_wleActivityTypeName);
            EditText txtNotes = FindViewById<EditText>(Resource.Id.MerchDataEntry_txtNotes);
            EditText txtWarehouseStock = FindViewById<EditText>(Resource.Id.MerchDataEntry_txtWarehouseStock);
            EditText txtShelfPrice3 = FindViewById<EditText>(Resource.Id.MerchDataEntry_txtDiscountPrice1);
            EditText txtShelfPrice4 = FindViewById<EditText>(Resource.Id.MerchDataEntry_txtDiscountPrice2);
            #endregion

            #region Validate Save
            if (txtFaceCount.Text == "" || Convert.ToInt32(txtFaceCount.Text) < 0)
            {
                errorMessage += "* Önyüz boþ býrakýlamaz veya sýfýrdan küçük olamaz.\r\n";
            }
            if (txtShelfIndex.Text == "" || Convert.ToInt32(txtShelfIndex.Text) < 0)
            {
                errorMessage += "* Raf Sýrasý boþ býrakýlamaz veya sýfýrdan küçük olamaz.\r\n";
            }
            if (txtShelfStock.Text == "" || Convert.ToInt32(txtShelfStock.Text) < 0)
            {
                errorMessage += "* Raf Stoðu boþ býrakýlamaz veya sýfýrdan küçük olamaz.\r\n";
            }
            if (txtShelfPrice1.Text == "" || Convert.ToDecimal(txtShelfPrice1.Text) < 0)
            {
                errorMessage += "* Fiyat 1. Kýsým boþ býrakýlamaz veya sýfýrdan küçük olamaz.\r\n";
            }
            if (txtShelfPrice2.Text == "")
            {
                errorMessage += "* Fiyat 2. Kýsým boþ býrakýlamaz.\r\n";
            }
            if (txtWarehouseStock.Text == "" || Convert.ToInt32(txtWarehouseStock.Text) < 0)
            {
                errorMessage += "* Depo Stoðu boþ býrakýlamaz veya sýfýrdan küçük olamaz.\r\n";
            }
            if (Convert.ToString(wleActivityTypeName.SelectedItem) == "FIYAT INDIRIMI")
            {
                if (txtShelfPrice3.Text == "" || Convert.ToDecimal(txtShelfPrice3.Text) < 0)
                {
                    errorMessage += "* Ýndirimli Fiyat 1. Kýsým boþ býrakýlamaz veya sýfýrdan küçük olamaz.\r\n";
                }
                if (txtShelfPrice4.Text == "")
                {
                    errorMessage += "* Ýndirimli Fiyat 2. Kýsým boþ býrakýlamaz.\r\n";
                }
            }
            if (errorMessage != string.Empty)
            {
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("Ekikleriniz var...");
                alert.SetMessage("Aþaðýda listelenen sorunlarý düzelttikten sonra tekrar deneyin.\r\n\r\n" + errorMessage);
                alert.SetPositiveButton("TAMAM", (senderAlert, args) =>
                {
                    
                });
                alert.Show();
                return;
            }
            #endregion
            #region Calculate Price
            decimal priceConcat = Convert.ToDecimal(txtShelfPrice1.Text) + (Convert.ToDecimal(txtShelfPrice2.Text) / Convert.ToDecimal(100));
            decimal priceConcat2 = 0;
            if (Convert.ToString(wleActivityTypeName.SelectedItem) == "FIYAT INDIRIMI")
            {
                priceConcat2 = Convert.ToDecimal(txtShelfPrice3.Text) + (Convert.ToDecimal(txtShelfPrice4.Text) / Convert.ToDecimal(100));
            }
            #endregion
            #region Calculate Promotion Type Id
            int promotionTypeId = wlePromotionTypeName.SelectedItemPosition + 1;
            #endregion

            using (var db = new SQLiteConnection(GlobalVariables.DatabasePath))
            {
                currentId = Convert.ToInt32(Intent.GetStringExtra("entMemberRouteDetailId"));
                var routeDetail = db.Query<entMemberRouteDetail>("SELECT * FROM entMemberRouteDetail WHERE Id=?", currentId);
                var route = db.Query<entMemberRoute>("SELECT * FROM entMemberRoute WHERE Id=?", routeDetail[0].MemberRouteId);
                var retailShop = db.Query<entRetailShop>("SELECT * FROM entRetailShop WHERE Id=?", route[0].RetailShopId);
                var product = db.Query<entRetailShop>("SELECT * FROM entCustomerProduct WHERE Id=?", routeDetail[0].CustomerProductId);

                entMemberRouteDetail insEntMemberRouteDetail = new entMemberRouteDetail();
                insEntMemberRouteDetail.CreatedOn = DateTime.Now;
                insEntMemberRouteDetail.CustomerProductId = product[0].Id;
                insEntMemberRouteDetail.FaceCount = Convert.ToInt32(txtFaceCount.Text);
                insEntMemberRouteDetail.IsSentToServer = false;
                insEntMemberRouteDetail.MemberRouteId = route[0].Id;
                insEntMemberRouteDetail.Notes = Convert.ToString(txtNotes.Text);
                insEntMemberRouteDetail.PromotionTypeId = promotionTypeId;
                insEntMemberRouteDetail.ShelfIndex = Convert.ToInt32(txtShelfIndex.Text);
                insEntMemberRouteDetail.ShelfPrice = priceConcat;
                insEntMemberRouteDetail.ShelfStock = Convert.ToInt32(txtShelfStock.Text);
                insEntMemberRouteDetail.WarehouseStock = Convert.ToInt32(txtWarehouseStock.Text);
                insEntMemberRouteDetail.ActivityTypeName = Convert.ToString(wleActivityTypeName.SelectedItem);
                insEntMemberRouteDetail.DiscountedPrice = priceConcat2;
                insEntMemberRouteDetail.Id = currentId;

                db.Execute("UPDATE entMemberRouteDetail SET ShelfIndex=?, ShelfStock=?, ShelfPrice=?, FaceCount=?, PromotionTypeId=?, WarehouseStock=?, Notes=?, CreatedOn=?, ActivityTypeName=?, DiscountedPrice=? WHERE Id=?",
                    insEntMemberRouteDetail.ShelfIndex, insEntMemberRouteDetail.ShelfStock, insEntMemberRouteDetail.ShelfPrice, insEntMemberRouteDetail.FaceCount, insEntMemberRouteDetail.PromotionTypeId, insEntMemberRouteDetail.WarehouseStock, insEntMemberRouteDetail.Notes, insEntMemberRouteDetail.CreatedOn, insEntMemberRouteDetail.ActivityTypeName, insEntMemberRouteDetail.DiscountedPrice, insEntMemberRouteDetail.Id);

                this.Finish();
            }
        }
    }
}