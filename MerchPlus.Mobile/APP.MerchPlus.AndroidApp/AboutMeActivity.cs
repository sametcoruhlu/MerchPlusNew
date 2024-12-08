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
using Android.Graphics;
//using ImageViews.Rounded;
//using ImageViews.Rounded;
using SQLite;
using entMerchPlus;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace APP.MerchPlus
{
    [Activity(Label = "AboutMeActivity")]
    public class AboutMeActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AboutMeActivity);

            Button btnVeriGonder = FindViewById<Button>(Resource.Id.btnSubmitData);
            btnVeriGonder.Click += BtnVeriGonder_Click;

            Button btnDeleteDatabase = FindViewById<Button>(Resource.Id.btnDeleteDB);
            btnDeleteDatabase.Click += btnDeleteDatabase_Click;
        }

        private void btnDeleteDatabase_Click(object sender, EventArgs e)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle("Emin misiniz?");
            alert.SetMessage("Bu iþlem, bugün yaptýðýnýz tüm giriþlerinizi silecek ve program kapatýlacak. Sunucuya gönderilmeyen tüm verilerden veya izinsiz kullanýmdan SORUMLU TUTULURSUNUZ! Yine de devam etmek istiyor musunuz?");
            alert.SetPositiveButton("TAMAM", (senderAlert, args) =>
            {
                FileInfo insFileInfo = new FileInfo(GlobalVariables.DatabasePath);
                insFileInfo.Delete();
                Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
            });
            alert.SetNegativeButton("VAZGEÇ", (senderAlert, asrgs) => {

            });
            alert.Show();
        }

        protected override void OnResume()
        {
            base.OnResume();

            #region Show Member NameSurname
            TextView txtMemberNameSurname = FindViewById<TextView>(Resource.Id.txtMemberNameSurname);
            txtMemberNameSurname.Text = GlobalVariables.CurrentMember.NameSurname;
            #endregion
            #region Show Member Profile Photo
            //RoundedImageView pctMemberProfilePhoto1 = FindViewById<RoundedImageView>(Resource.Id.pctMemberProfilePhoto1);
            //Bitmap bitmap1 = BitmapFactory.DecodeByteArray(GlobalVariables.MemberProfilePictureByteArray, 0, GlobalVariables.MemberProfilePictureByteArray.Length);
            //pctMemberProfilePhoto1.SetImageBitmap(bitmap1);
            #endregion
            
            GetAllStoreCount();
            GetVisitedStoreCount();
            GetPendingStoreCount();
        }

        private void BtnVeriGonder_Click(object sender, EventArgs e)
        {
            ((Button)sender).Text = "Lütfen Bekleyin";
            ((Button)sender).Enabled = false;

            string message = string.Empty;

            #region MemberRoute to Server
            try
            {
                int counter = 0;
                var db = new SQLiteConnection(GlobalVariables.DatabasePath);
                var rows = db.Query<entMemberRoute>("SELECT * from entMemberRoute WHERE IsSentToServer=0");

                foreach (var memberRoute in rows)
                {
                    #region Get Detail Rows
                    var detailRows = db.Query<entMemberRouteDetail>("SELECT * from entMemberRouteDetail WHERE MemberRouteId=?", Convert.ToInt32(memberRoute.Id));
                    #endregion

                    #region Get Photos
                    var photoRows = db.Query<entMemberRoutePhoto>("SELECT * from entMemberRoutePhoto WHERE MemberRouteId=?", Convert.ToInt32(memberRoute.Id));
                    #endregion

                    #region Get Sales Orders
                    var salesOrder = db.Query<entSalesOrder>("SELECT * FROM entSalesOrder WHERE MemberRouteId=?", Convert.ToInt32(memberRoute.Id));
                    #endregion

                    #region Get Sales Order Details
                    List<entSalesOrderDetail> salesOrderDetail = new List<entSalesOrderDetail>();
                    if (salesOrder.Count > 0)
                        salesOrderDetail = db.Query<entSalesOrderDetail>("SELECT * FROM entSalesOrderDetail WHERE SalesOrderId=?", Convert.ToInt32(salesOrder[0].Id));
                    #endregion

                    #region Get ProductExpiration
                    var productExpiration = db.Query<entProductExpiration>("SELECT * FROM entProductExpiration WHERE MemberRouteId=?", Convert.ToInt32(memberRoute.Id));
                    #endregion


                    #region Prepare Data
                    MemberRouteSyncData insSyncData = new MemberRouteSyncData();
                    insSyncData.MemberRoute = memberRoute;

                    foreach (entMemberRouteDetail memberRouteDetail in detailRows)
                    {
                        insSyncData.MemberRouteDetail.Add(memberRouteDetail);
                    }

                    foreach (entMemberRoutePhoto memberRoutePhoto in photoRows)
                    {
                        insSyncData.MemberRoutePhoto.Add(memberRoutePhoto);
                    }

                    if (salesOrder.Count > 0)
                    {
                        foreach (entSalesOrder salesOrderEntity in salesOrder)
                        {
                            insSyncData.SalesOrder.Add(salesOrderEntity);
                        }

                        foreach (entSalesOrderDetail salesOrderDetailEntity in salesOrderDetail)
                        {
                            insSyncData.SalesOrderDetail.Add(salesOrderDetailEntity);
                        }
                    }

                    if (productExpiration.Count > 0)
                    {
                        foreach (entProductExpiration productExpirationEntity in productExpiration)
                        {
                            insSyncData.ProductExpiration.Add(productExpirationEntity);
                        }
                    }
                    #endregion

                    var responseString = string.Empty;
                    ((Button)sender).SetText("Gönderiliyor " + counter.ToString() + "/" + rows.Count.ToString(), TextView.BufferType.Normal);
                    using (var client = new HttpClient())
                    {
                        //setup client
                        client.BaseAddress = new System.Uri(GlobalVariables.APIPath);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        client.Timeout = new TimeSpan(0, 10, 0);

                        //make request
                        if (salesOrder.Count > 0)
                        {
                            var request = new StringContent(JsonConvert.SerializeObject(insSyncData), Encoding.UTF8, "application/json");
                            HttpResponseMessage response = client.PostAsync("MemberRouteController/CompleteMemberRouteWiselyFromJsonArrayWithSalesOrder", request).Result;
                            responseString = response.Content.ReadAsStringAsync().Result;
                        }
                        else
                        {
                            var request = new StringContent(JsonConvert.SerializeObject(insSyncData), Encoding.UTF8, "application/json");
                            HttpResponseMessage response = client.PostAsync("MemberRouteController/CompleteMemberRouteWiselyFromJsonArray", request).Result;
                            responseString = response.Content.ReadAsStringAsync().Result;
                        }
                    }
                    ((Button)sender).SetText("Sunucuya Gönderildi " + counter.ToString() + "/" + rows.Count.ToString(), TextView.BufferType.Normal);

                    JObject returnObject = (JObject)JsonConvert.DeserializeObject(responseString);
                    if (Convert.ToString(returnObject["Result"]) != "NOK")
                    {
                        #region All information sent to server. Mark as complete
                        db.Execute("UPDATE entMemberRoute SET IsSentToServer=? WHERE Id=?", true, insSyncData.MemberRoute.Id);
                        db.Execute("UPDATE entMemberRouteDetail SET IsSentToServer=? WHERE MemberRouteId=?", true, insSyncData.MemberRoute.Id);
                        db.Execute("UPDATE entProductExpiration SET IsSentToServer=? WHERE MemberRouteId=?", true, insSyncData.MemberRoute.Id);
                        #endregion
                        message += "Gönderildi " + Convert.ToString(memberRoute.Id) + "\r\n";
                        ((Button)sender).SetText("Güncellendi " + counter.ToString() + "/" + rows.Count.ToString(), TextView.BufferType.Normal);
                    }
                    else
                    {
                        Console.WriteLine("Database Sync Failed > MemberRoute");
                        message += "Gönderilemedi " + Convert.ToString(memberRoute.Id) + "\r\n" + responseString;
                    }
                }

               
                rows = db.Query<entMemberRoute>("SELECT * from entMemberRoute WHERE IsSentToServer=0");
                Button btnVeriGonder = FindViewById<Button>(Resource.Id.btnSubmitData);
                btnVeriGonder.SetText("Veri Gönder (" + rows.Count + " maðaza)", TextView.BufferType.Normal);

            }
            catch (Exception ex)
            {
                message += "Hata oluþtu " + ex.ToString() + "\r\n";
            }
            #endregion

            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle("Ýþlem Tamamlandý");
            alert.SetMessage(message);
            alert.SetPositiveButton("TAMAM", (senderAlert, args) =>
            {
                ;
            });
            alert.Show();

            ((Button)sender).Enabled = true;
            ((Button)sender).SetText("Veri Gönder", TextView.BufferType.Normal);

        }

        private void GetAllStoreCount()
        {
            var db = new SQLiteConnection(GlobalVariables.DatabasePath);
            int result = db.ExecuteScalar<int>("SELECT COUNT(*) from entMemberRoute");
            TextView txtCountOfAllStores = FindViewById<TextView>(Resource.Id.txtCountOfAllStores);
            txtCountOfAllStores.Text = result.ToString("N0");
        }

        private void GetVisitedStoreCount()
        {
            var db = new SQLiteConnection(GlobalVariables.DatabasePath);
            int result = db.ExecuteScalar<int>("SELECT COUNT(*) from entMemberRoute WHERE ExitOn IS NOT NULL");
            TextView txtCountOfVisitedStores = FindViewById<TextView>(Resource.Id.txtCountOfVisitedStores);
            txtCountOfVisitedStores.Text = result.ToString("N0");
        }

        private void GetPendingStoreCount()
        {
            var db = new SQLiteConnection(GlobalVariables.DatabasePath);
            int result = db.ExecuteScalar<int>("SELECT COUNT(*) from entMemberRoute WHERE EntryOn IS NULL");
            TextView txtCountOfPendingStores = FindViewById<TextView>(Resource.Id.txtCountOfPendingStores);
            txtCountOfPendingStores.Text = result.ToString("N0");
        }
    }
}