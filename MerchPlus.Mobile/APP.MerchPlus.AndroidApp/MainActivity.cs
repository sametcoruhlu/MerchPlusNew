using Android.App;
using Android.Content;
using Android.OS;
using Android.Telephony;
using Android.Views;
using Android.Widget;
using APP.MerchPlus.Entities;
using entMerchPlus;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SQLite;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Android.Views.ViewGroup;

namespace APP.MerchPlus
{
    [Activity(Label = "MerchPlus", MainLauncher = true, Icon = "@drawable/ic_launcher", ClearTaskOnLaunch = true, MultiProcess = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class MainActivity : Activity
    {
        private PowerManager _powerManager;
        private PowerManager.WakeLock _wakeLock;
        MyBootReceiver insReciever = new MyBootReceiver();
        long downloadId;
        String destination = string.Empty;
        Activity currentActivity;

        private static readonly object _dbLock = new object(); // Veritabanı kilidi için nesne


        protected override void OnCreate(Bundle bundle)
        {
            #region Hide Title
            RequestWindowFeature(WindowFeatures.NoTitle);
            #endregion

            base.OnCreate(bundle);
            SetContentView(Resource.Layout.SplashScreen);

            _powerManager = (PowerManager)GetSystemService(PowerService);
            _wakeLock = _powerManager.NewWakeLock(WakeLockFlags.Full, "MerchPlusApp");
            currentActivity = this;

            #region Permission Control
            if (!PermissionManager.ArePermissionsGranted(this))
            {
                if (!PermissionsHelper.ArePermissionsGranted(this))
                {
                    PermissionsHelper.RequestPermissionsIfNecessary(this);
                }
                else
                {
                    PermissionManager.SetPermissionsGranted(this, true);
                }
            }
            #endregion
        }

        protected async override void OnResume()
        {
            base.OnResume();
            _wakeLock.Acquire();
            ClearStatusMessages();
            await GetUser();

            if (GlobalVariables.CurrentMember.Id != null)
            {
                await CheckDatabase();
                await CreateTablesForFirstUse();
                await GetDataForToday();

                #region Start Coordinate Agent
                if (!GlobalVariables.MemberLocationAgentStarted)
                {
                    GlobalVariables.MemberLocationAgentStarted = true;
                    StartService(new Intent(this, typeof(LocationAgentService)));
                }
                #endregion

                #region Start Database Sync Service
                if (!GlobalVariables.DatabaseSyncStarted)
                {
                    GlobalVariables.DatabaseSyncStarted = true;
                    StartService(new Intent(this, typeof(DatabaseSyncService)));
                }
                #endregion

                Intent intent = new Intent(this.ApplicationContext, typeof(MainMenu));
                StartActivity(intent);
            }
        }

        private void ClearStatusMessages()
        {
            LinearLayout layout = FindViewById<LinearLayout>(Resource.Id.pnlStatus);
            layout.RemoveAllViews();
        }

        string dId = string.Empty;
        public string DeviceId
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(dId))
                    return dId;
                try
                {

                    var telephonyManager = (TelephonyManager)GetSystemService(TelephonyService);
                    dId = telephonyManager.DeviceId;
                }
                catch (Exception exc)
                {
                    dId = Android.OS.Build.Serial;
                    if (string.IsNullOrWhiteSpace(dId) || dId == Build.Unknown || dId == "0")
                    {
                        try
                        {
                            var context = Android.App.Application.Context;
                            dId = Android.Provider.Settings.Secure.GetString(context.ContentResolver, Android.Provider.Settings.Secure.AndroidId);
                            //AddToListUI("Cihazı kaydetmek için bu seri numarasını kullanın : " + dId);
                        }
                        catch (Exception ex)
                        {
                            Android.Util.Log.Warn("DeviceInfo", "Unable to get id: " + ex.ToString());
                        }
                    }
                }

                return dId;
            }
        }

        public async Task GetUser()
        {

            if (GlobalVariables.CurrentMember.Id != null)
            {
                AddToListUI("Tekrar merhaba " + GlobalVariables.CurrentMember.NameSurname);
                return;
            }

            var telephonyManager = (TelephonyManager)GetSystemService(TelephonyService);
            var id = DeviceId;

            AddToListUI("Kullanıcı girişi yapılıyor");

            await Task.Factory.StartNew(() =>
            {
                var responseStringIMEI = string.Empty;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(GlobalVariables.APIPath);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    entMember insEntMember = new entMember { IMEINumber = id };
                    var request = new StringContent(JsonConvert.SerializeObject(insEntMember), Encoding.UTF8, "application/json");
                    try
                    {
                        HttpResponseMessage response = client.PostAsync("AndroidTestController/SelectMemberByIMEINumber", request).Result;
                        responseStringIMEI = response.Content.ReadAsStringAsync().Result;
                    }
                    catch (Exception ex)
                    {
                        AddToListUI("Hata MainActivity.cs line:269 " + ex.Message);
                    }
                }

                JObject returnObjectIMEI = (JObject)JsonConvert.DeserializeObject(responseStringIMEI);
                if (Convert.ToString(returnObjectIMEI["Result"]) != "NOK")
                {
                    GlobalVariables.CurrentMember = ((JObject)returnObjectIMEI["Content"]).ToObject<entMember>();
                }
            });

            if (GlobalVariables.CurrentMember.Id == null)
            {
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("Sizi Tanıyamadım...");
                alert.SetMessage("Cihazınıza ait " + DeviceId + " numarası hiçbir personel ile eşleştirilmemiş.");
                alert.SetPositiveButton("TAMAM", (senderAlert, args) =>
                {
                    Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
                });

                alert.Show();
            }
            else
            {
                AddToListUI("Hoşgeldiniz " + GlobalVariables.CurrentMember.NameSurname);
            }
        }

        public async Task CheckDatabase()
        {
            AddToListUI("Lokal veritabanı kontrol ediliyor");
            string errorMessage = string.Empty;
            await Task.Factory.StartNew(() =>
            {
                lock (_dbLock)
                {
                    try
                    {
                        var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "merchplus.db3");
                        if (!File.Exists(path))
                        {
                            SQLiteConnection db = new SQLiteConnection(path);
                            db.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        errorMessage = ex.ToString();
                    }
                }
            });
            if (errorMessage != string.Empty)
            {
                AddToListUI(errorMessage);
            }
            
        }
        public async Task CreateTablesForFirstUse()
        {
            SQLiteConnection db = new SQLiteConnection(GlobalVariables.DatabasePath);
            object dbLock = new object();

            #region MemberRoute
            SQLiteCommand insCommand = new SQLiteCommand(db);
            lock (dbLock)
            {
                insCommand.CommandText = "SELECT COUNT(name) FROM sqlite_master WHERE name = 'entMemberRoute'";

                var result = insCommand.ExecuteScalar<int>();
                if (result == 0)
                {
                    AddToListUI("Ziyaret Planı tablosu oluşturuluyor");
                    db.CreateTable<entMemberRoute>();
                }
            }
            #endregion

            #region MemberRouteDetail
            insCommand = new SQLiteCommand(db);
            lock (dbLock)
            {
                insCommand.CommandText = "SELECT COUNT(name) FROM sqlite_master WHERE name = 'entMemberRouteDetail'";
                var result = insCommand.ExecuteScalar<int>();
                if (result == 0)
                {
                    AddToListUI("Ziyaret Planı Detay tablosu oluşturuluyor");
                    db.CreateTable<entMemberRouteDetail>();
                }
            }
            #endregion

            #region MemberLocation
            insCommand = new SQLiteCommand(db);
            lock (dbLock)
            {
                insCommand.CommandText = "SELECT COUNT(name) FROM sqlite_master WHERE name = 'entMemberCoordinate'";
                var result = insCommand.ExecuteScalar<int>();
                if (result == 0)
                {
                    AddToListUI("Koordinat tablosu oluşturuluyor");
                    db.CreateTable<entMemberCoordinate>();
                }
            }
            #endregion

            #region Retail
            insCommand = new SQLiteCommand(db);
            lock (dbLock)
            {
                insCommand.CommandText = "SELECT COUNT(name) FROM sqlite_master WHERE name = 'entRetailProfilePicture'";
                var result = insCommand.ExecuteScalar<int>();
                if (result == 0)
                {
                    db.CreateTable<entRetailProfilePicture>();
                }
            }
            #endregion

            #region CustomerProductPicture
            insCommand = new SQLiteCommand(db);
            lock (dbLock)
            {
                insCommand.CommandText = "SELECT COUNT(name) FROM sqlite_master WHERE name = 'entCustomerProductPicture'";
                var result = insCommand.ExecuteScalar<int>();
                if (result == 0)
                {
                    db.CreateTable<entCustomerProductPicture>();
                }
            }
            #endregion

            #region RetailShop
            insCommand = new SQLiteCommand(db);
            lock (dbLock)
            {
                insCommand.CommandText = "SELECT COUNT(name) FROM sqlite_master WHERE name = 'entRetailShop'";
                var result = insCommand.ExecuteScalar<int>();
                if (result == 0)
                {
                    AddToListUI("Mağaza tablosu oluşturuluyor");
                    db.CreateTable<entRetailShop>();
                }
            }
            #endregion

            #region Retail
            insCommand = new SQLiteCommand(db);
            lock (dbLock)
            {
                insCommand.CommandText = "SELECT COUNT(name) FROM sqlite_master WHERE name = 'entRetail'";
                var result = insCommand.ExecuteScalar<int>();
                if (result == 0)
                {
                    db.CreateTable<entRetail>();
                }
            }
            #endregion

            #region CustomerProduct
            insCommand = new SQLiteCommand(db);
            lock (dbLock)
            {
                insCommand.CommandText = "SELECT COUNT(name) FROM sqlite_master WHERE name = 'entCustomerProduct'";
                var result = insCommand.ExecuteScalar<int>();
                if (result == 0)
                {
                    db.CreateTable<entCustomerProduct>();
                }
            }
            #endregion

            #region CustomerBrandCategory
            insCommand = new SQLiteCommand(db);
            lock (dbLock)
            {
                insCommand.CommandText = "SELECT COUNT(name) FROM sqlite_master WHERE name = 'entCustomerBrandCategory'";
                var result = insCommand.ExecuteScalar<int>();
                if (result == 0)
                {
                    db.CreateTable<entCustomerBrandCategory>();
                }
            }
            #endregion

            #region SalesOrder
            insCommand = new SQLiteCommand(db);
            lock (dbLock)
            {
                insCommand.CommandText = "SELECT COUNT(name) FROM sqlite_master WHERE name = 'entSalesOrder'";
                var result = insCommand.ExecuteScalar<int>();
                if (result == 0)
                {
                    db.CreateTable<entSalesOrder>();
                }
            }
            #endregion

            #region SalesOrderDetail
            insCommand = new SQLiteCommand(db);
            lock (dbLock)
            {
                insCommand.CommandText = "SELECT COUNT(name) FROM sqlite_master WHERE name = 'entSalesOrderDetail'";
                var result = insCommand.ExecuteScalar<int>();
                if (result == 0)
                {
                    db.CreateTable<entSalesOrderDetail>();
                }
            }
            #endregion

            #region SalesOrderPackageType
            insCommand = new SQLiteCommand(db);
            lock (dbLock)
            {
                insCommand.CommandText = "SELECT COUNT(name) FROM sqlite_master WHERE name = 'entSalesOrderPackageType'";
                var result = insCommand.ExecuteScalar<int>();
                if (result == 0)
                {
                    db.CreateTable<entSalesOrderPackageType>();
                }
            }
            #endregion

            #region MemberRoutePhoto
            insCommand = new SQLiteCommand(db);
            lock (dbLock)
            {
                insCommand.CommandText = "SELECT COUNT(name) FROM sqlite_master WHERE name = 'entMemberRoutePhoto'";
                var result = insCommand.ExecuteScalar<int>();
                if (result == 0)
                {
                    db.CreateTable<entMemberRoutePhoto>();

                    GlobalVariables.MemberRoutePhotoDataTable = new System.Data.DataTable();
                    GlobalVariables.MemberRoutePhotoDataTable.Columns.Add("Data", typeof(entMemberRoutePhoto));
                    GlobalVariables.MemberRoutePhotoDataTable.AcceptChanges();
                }
            }
            #endregion

            #region ProductExpiration
            insCommand = new SQLiteCommand(db);
            lock (dbLock)
            {
                insCommand.CommandText = "SELECT COUNT(name) FROM sqlite_master WHERE name = 'entProductExpiration'";
                var result = insCommand.ExecuteScalar<int>();
                if (result == 0)
                {
                    db.CreateTable<entProductExpiration>();
                }
            }
            #endregion

            #region Promotion Type
            insCommand = new SQLiteCommand(db);
            lock (dbLock)
            {
                insCommand.CommandText = "SELECT COUNT(name) FROM sqlite_master WHERE name = 'entPromotionType'";
                var result = insCommand.ExecuteScalar<int>();
                if (result == 0)
                {
                    db.CreateTable<entPromotionType>();
                }
            }
            #endregion

            #region Member Display Photos
            insCommand = new SQLiteCommand(db);
            lock (dbLock)
            {
                insCommand.CommandText = "SELECT COUNT(name) FROM sqlite_master WHERE name = 'entMemberDisplayPhotos'";
                var result = insCommand.ExecuteScalar<int>();
                if (result == 0)
                {
                    db.CreateTable<entMemberDisplayPhotos>();
                }
            }
            #endregion

            #region Configuration
            insCommand = new SQLiteCommand(db);
            lock (dbLock)
            {
                insCommand.CommandText = "SELECT COUNT(name) FROM sqlite_master WHERE name = 'entConfiguration'";
                var result = insCommand.ExecuteScalar<int>();

                if (result == 0)
                {
                    db.CreateTable<entConfiguration>();

                    entConfiguration insEntConfiguration = new entConfiguration();
                    insEntConfiguration.Name = "RetailDataDateTime";
                    insEntConfiguration.Value = DateTime.Now.AddDays(1).ToString("yyyyMMdd");
                    db.Insert(insEntConfiguration);

                    insEntConfiguration = new entConfiguration();
                    insEntConfiguration.Name = "RetailShopDataDateTime";
                    insEntConfiguration.Value = DateTime.Now.AddDays(1).ToString("yyyyMMdd");
                    db.Insert(insEntConfiguration);

                    insEntConfiguration = new entConfiguration();
                    insEntConfiguration.Name = "MemberRouteDetailDataDateTime";
                    insEntConfiguration.Value = DateTime.Now.AddDays(1).ToString("yyyyMMdd");
                    db.Insert(insEntConfiguration);

                    insEntConfiguration = new entConfiguration();
                    insEntConfiguration.Name = "MemberRouteDataDateTime";
                    insEntConfiguration.Value = DateTime.Now.AddDays(1).ToString("yyyyMMdd");
                    db.Insert(insEntConfiguration);

                    insEntConfiguration = new entConfiguration();
                    insEntConfiguration.Name = "CustomerProductDataDateTime";
                    insEntConfiguration.Value = DateTime.Now.AddDays(1).ToString("yyyyMMdd");
                    db.Insert(insEntConfiguration);

                    insEntConfiguration = new entConfiguration();
                    insEntConfiguration.Name = "CustomerBrandCategoryDataDateTime";
                    insEntConfiguration.Value = DateTime.Now.AddDays(1).ToString("yyyyMMdd");
                    db.Insert(insEntConfiguration);

                    insEntConfiguration = new entConfiguration();
                    insEntConfiguration.Name = "CustomerProductDataDateTime";
                    insEntConfiguration.Value = DateTime.Now.AddDays(1).ToString("yyyyMMdd");
                    db.Insert(insEntConfiguration);

                    insEntConfiguration = new entConfiguration();
                    insEntConfiguration.Name = "SalesOrderDataDateTime";
                    insEntConfiguration.Value = DateTime.Now.AddDays(1).ToString("yyyyMMdd");
                    db.Insert(insEntConfiguration);

                    insEntConfiguration = new entConfiguration();
                    insEntConfiguration.Name = "SalesOrderDetailDataDateTime";
                    insEntConfiguration.Value = DateTime.Now.AddDays(1).ToString("yyyyMMdd");
                    db.Insert(insEntConfiguration);

                    insEntConfiguration = new entConfiguration();
                    insEntConfiguration.Name = "SalesOrderPackageTypeDataDateTime";
                    insEntConfiguration.Value = DateTime.Now.AddDays(1).ToString("yyyyMMdd");
                    db.Insert(insEntConfiguration);

                    insEntConfiguration = new entConfiguration();
                    insEntConfiguration.Name = "PromotionTypeDataDateTime";
                    insEntConfiguration.Value = DateTime.Now.AddDays(1).ToString("yyyyMMdd");
                    db.Insert(insEntConfiguration);

                    insEntConfiguration = new entConfiguration();
                    insEntConfiguration.Name = "MemberDisplayPhotosDataDateTime";
                    insEntConfiguration.Value = DateTime.Now.AddDays(1).ToString("yyyyMMdd");
                    db.Insert(insEntConfiguration);

                }
            }
            #endregion


            lock (dbLock)
            {
                db.Execute("DELETE FROM entCustomerProduct WHERE Id=?", 208);
                db.Execute("DELETE FROM entMemberRouteDetail WHERE CustomerProductId=?", 208);
            }
            db.Close();
        }

        public async Task GetDataForToday()
        {

            object dbLock = new object();
            using (var db = new SQLiteConnection(GlobalVariables.DatabasePath))
            {
                try
                {
                    #region Retail
                    var retailConfig = db.Table<entConfiguration>()
                                         .FirstOrDefault(x => x.Name == "RetailDataDateTime");

                    var retailCount = db.ExecuteScalar<int>("SELECT COUNT(*) FROM entRetail");
                    if (retailCount == 0 || DateTime.ParseExact(DateTime.Now.ToString("yyyyMMdd"), "yyyyMMdd", CultureInfo.InvariantCulture) > DateTime.ParseExact(retailConfig.Value.Replace("-", ""), "yyyyMMdd", CultureInfo.InvariantCulture))
                    {
                        //AddToListUI("Fetching Retail Data...");

                        lock (dbLock)
                        {
                            db.Execute("DELETE FROM entRetail");
                        }

                        string responseString;
                        using (var client = new HttpClient { BaseAddress = new Uri(GlobalVariables.APIPath) })
                        {
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                            var request = new StringContent(string.Empty, Encoding.UTF8, "application/json");
                            var response = await client.PostAsync("RetailController/SelectRetail", request);
                            responseString = await response.Content.ReadAsStringAsync();
                        }

                        JObject returnObject = JObject.Parse(responseString);
                        if (returnObject["Result"]?.ToString() != "NOK")
                        {
                            var retailList = JArray.Parse(returnObject["Content"].ToString()).ToObject<List<entRetail>>();
                            GlobalVariables.listRetail = retailList;

                            db.BeginTransaction();

                            foreach (var retail in retailList)
                            {
                                lock (dbLock)
                                {
                                    db.Insert(retail, typeof(entRetail));
                                }

                                try
                                {
                                    entRetailProfilePicture profilePicture = new entRetailProfilePicture
                                    {
                                        RetailId = retail.Id
                                    };

                                    using (var webClient = new WebClient())
                                    {
                                        byte[] imageBytes = await webClient.DownloadDataTaskAsync(GlobalVariables.WebsitePath + retail.ProfilePicturePath);
                                        profilePicture.ProfilePictureBase64 = Convert.ToBase64String(imageBytes);
                                    }

                                    lock (dbLock)
                                    {
                                        db.Insert(profilePicture, typeof(entRetailProfilePicture));
                                    }
                                }
                                catch (Exception ex)
                                {
                                    // Log exception
                                }

                                lock (dbLock)
                                {
                                    db.Execute("UPDATE entConfiguration SET Value=? WHERE Name=?", DateTime.Now.ToString("yyyy-MM-dd"), "RetailDataDateTime");
                                }
                            }
                            db.Commit();

                        }

                        EditListUI($"(Total: {GlobalVariables.listRetail.Count:N0})");
                    }
                    #endregion

                    #region RetailShop
                    var retailShopConfig = db.Table<entConfiguration>()
                                              .FirstOrDefault(x => x.Name == "RetailShopDataDateTime");

                    var retailShopCount = db.ExecuteScalar<int>("SELECT COUNT(*) FROM entRetailShop");
                    if (retailShopCount == 0 || DateTime.ParseExact(DateTime.Now.ToString("yyyyMMdd"), "yyyyMMdd", CultureInfo.InvariantCulture) > DateTime.ParseExact(retailShopConfig.Value.Replace("-", ""), "yyyyMMdd", CultureInfo.InvariantCulture))
                    {
                        AddToListUI("Mağaza bilgileri alınıyor. Bu işlem zaman alabilir, lütfen uygulamayı kapatmayınız.");

                        lock (dbLock)
                        {
                            db.Execute("DELETE FROM entRetailShop");
                        }


                        string responseString;
                        using (var client = new HttpClient { BaseAddress = new Uri(GlobalVariables.APIPath) })
                        {
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                            var memberRoute = new entMemberRoute
                            {
                                EffectiveDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")),
                                MemberId = GlobalVariables.CurrentMember.Id
                            };

                            var request = new StringContent(JsonConvert.SerializeObject(memberRoute), Encoding.UTF8, "application/json");
                            var response = await client.PostAsync("RetailShopController/SelectRetailShopByMemberIdMemberRouteDate", request);
                            responseString = await response.Content.ReadAsStringAsync();
                        }

                        JObject returnObject = JObject.Parse(responseString);
                        if (returnObject["Result"]?.ToString() != "NOK")
                        {
                            var retailShopList = JArray.Parse(returnObject["Content"].ToString()).ToObject<List<entRetailShop>>();
                            GlobalVariables.listRetailShop = retailShopList;

                            db.BeginTransaction();

                            foreach (var retailShop in retailShopList)
                            {
                                lock (dbLock)
                                {
                                    db.Insert(retailShop, typeof(entRetailShop));
                                }
                            }

                            lock (dbLock)
                            {
                                db.Execute("UPDATE entConfiguration SET Value=? WHERE Name=?", DateTime.Now.ToString("yyyy-MM-dd"), "RetailShopDataDateTime");
                            }
                            db.Commit();

                        }

                        EditListUI($"(Total: {GlobalVariables.listRetailShop.Count:N0})");
                    }
                    #endregion

                    #region CustomerBrandCategory
                    var customerBrandCategoryDataDateTime = db.Table<entConfiguration>()
                        .Where(x => x.Name == "CustomerBrandCategoryDataDateTime")
                        .FirstOrDefault();

                    var result = db.ExecuteScalar<int>("SELECT COUNT(*) FROM entCustomerBrandCategory");

                    if (result == 0 || DateTime.ParseExact(DateTime.Now.ToString("yyyyMMdd"), "yyyyMMdd", CultureInfo.InvariantCulture) > DateTime.ParseExact(customerBrandCategoryDataDateTime.Value.Replace("-", ""), "yyyyMMdd", CultureInfo.InvariantCulture))
                    {
                        AddToListUI("Ürün Ağacı alınıyor...");

                        db.BeginTransaction();

                        lock (dbLock)
                        {
                            db.Execute("DELETE FROM entCustomerBrandCategory");
                        }

                        try
                        {
                            string responseString;

                            using (var client = new HttpClient())
                            {
                                client.BaseAddress = new Uri(GlobalVariables.APIPath);
                                client.DefaultRequestHeaders.Accept.Clear();
                                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                                var request = new StringContent(string.Empty, Encoding.UTF8, "application/json");
                                HttpResponseMessage response = client.PostAsync("CustomerBrandCategoryController/SelectCustomerBrandCategory", request).Result;
                                responseString = response.Content.ReadAsStringAsync().Result;
                            }

                            JObject returnObject = JObject.Parse(responseString);
                            if (returnObject["Result"].ToString() != "NOK")
                            {
                                var hede = JArray.Parse(returnObject["Content"].ToString());
                                List<entCustomerBrandCategory> listCustomerBrandCategory = hede.ToObject<List<entCustomerBrandCategory>>();
                                GlobalVariables.listCustomerBrandCategory = listCustomerBrandCategory;

                                foreach (var insEntCustomerBrandCategory in listCustomerBrandCategory)
                                {
                                    lock (dbLock)
                                    {
                                        db.Insert(insEntCustomerBrandCategory, typeof(entCustomerBrandCategory));
                                    }
                                }

                                lock (dbLock)
                                {
                                    db.Execute("UPDATE entConfiguration SET Value=? WHERE Name='CustomerBrandCategoryDataDateTime'", DateTime.Now.ToString("yyyy-MM-dd"));
                                }

                                db.Commit(); // Transaction başarılıysa commit yap
                            }
                        }
                        catch (Exception ex)
                        {
                            db.Rollback(); // Hata durumunda işlemi geri al
                            AddToListUI($"Hata oluştu: {ex.Message}");
                        }


                        EditListUI($" (Toplam: {GlobalVariables.listCustomerBrandCategory.Count:N0})");
                    }
                    #endregion

                    #region CustomerProduct
                    var customerProductDataDateTime = db.Table<entConfiguration>()
                                                       .Where(x => x.Name == "CustomerProductDataDateTime")
                                                       .FirstOrDefault();
                    result = db.ExecuteScalar<int>("SELECT COUNT(*) FROM entCustomerProduct");

                    if (result == 0 || DateTime.ParseExact(DateTime.Now.ToString("yyyyMMdd"), "yyyyMMdd", CultureInfo.InvariantCulture) > DateTime.ParseExact(customerProductDataDateTime.Value.Replace("-", ""), "yyyyMMdd", CultureInfo.InvariantCulture))
                    {
                        AddToListUI("Ürün detayları alınıyor. Bu işlem biraz zaman alabilir...");

                        db.BeginTransaction();

                        lock (dbLock)
                        {
                            db.Execute("DELETE FROM entCustomerProduct");
                        }

                        try
                        {
                            string responseString;

                            using (var client = new HttpClient())
                            {
                                client.BaseAddress = new Uri(GlobalVariables.APIPath);
                                client.DefaultRequestHeaders.Accept.Clear();
                                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                                var request = new StringContent(string.Empty, Encoding.UTF8, "application/json");
                                HttpResponseMessage response = client.PostAsync("CustomerProductController/SelectCustomerProduct", request).Result;
                                responseString = response.Content.ReadAsStringAsync().Result;
                            }

                            JObject returnObject = JObject.Parse(responseString);
                            if (returnObject["Result"].ToString() != "NOK")
                            {
                                var hede = JArray.Parse(returnObject["Content"].ToString());
                                List<entCustomerProduct> listCustomerProduct = hede.ToObject<List<entCustomerProduct>>();
                                GlobalVariables.listCustomerProduct = listCustomerProduct;

                                foreach (var insEntCustomerProduct in listCustomerProduct)
                                {
                                    lock (dbLock)
                                    {
                                        db.Insert(insEntCustomerProduct, typeof(entCustomerProduct));
                                    }

                                    try
                                    {
                                        entCustomerProductPicture insEntCustomerProductPicture = new entCustomerProductPicture();
                                        insEntCustomerProductPicture.CustomerProductId = insEntCustomerProduct.Id;

                                        var webClient = new WebClient();
                                        byte[] imageBytes = webClient.DownloadData(GlobalVariables.WebsitePath + insEntCustomerProduct.ProfilePicturePath);
                                        insEntCustomerProductPicture.CustomerProductPictureBase64 = Convert.ToBase64String(imageBytes);

                                        lock (dbLock)
                                        {
                                            db.Insert(insEntCustomerProductPicture, typeof(entCustomerProductPicture));
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        // Hataları yakala ve logla
                                        AddToListUI($"Ürün resmi eklenemedi: {ex.Message}");
                                    }

                                    lock (dbLock)
                                    {
                                        db.Execute("UPDATE entConfiguration SET Value=? WHERE Name='CustomerProductDataDateTime'", DateTime.Now.ToString("yyyy-MM-dd"));
                                    }
                                }

                                db.Commit(); // Başarılıysa commit yap
                            }
                        }
                        catch (Exception ex)
                        {
                            db.Rollback(); // Hata varsa rollback yap
                            AddToListUI($"Hata oluştu: {ex.Message}");
                        }


                        lock (dbLock)
                        {
                            db.Execute("DELETE FROM entCustomerProduct WHERE Id=?", 208);
                        }

                        EditListUI($" (Toplam: {GlobalVariables.listCustomerProduct.Count:N0})");
                    }
                    #endregion

                    #region MemberRoute
                    var memberRouteDataDateTime = db.Table<entConfiguration>()
                       .Where(x => x.Name == "MemberRouteDataDateTime")
                       .FirstOrDefault();
                    result = db.ExecuteScalar<int>("SELECT COUNT(*) FROM entMemberRoute");

                    if (result == 0 || DateTime.ParseExact(DateTime.Now.ToString("yyyyMMdd"), "yyyyMMdd", CultureInfo.InvariantCulture) > DateTime.ParseExact(memberRouteDataDateTime.Value.Replace("-", ""), "yyyyMMdd", CultureInfo.InvariantCulture))
                    {
                        AddToListUI("Ziyaret Planı alınıyor...");

                        db.BeginTransaction();

                        lock (dbLock)
                        {
                            db.Execute("DELETE FROM entMemberRoute");
                        }

                        try
                        {
                            string responseString;

                            using (var client = new HttpClient())
                            {
                                client.BaseAddress = new Uri(GlobalVariables.APIPath);
                                client.DefaultRequestHeaders.Accept.Clear();
                                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                                var insEntMemberRoute = new entMemberRoute
                                {
                                    EffectiveDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")),
                                    MemberId = GlobalVariables.CurrentMember.Id
                                };

                                var request = new StringContent(JsonConvert.SerializeObject(insEntMemberRoute), Encoding.UTF8, "application/json");
                                HttpResponseMessage response = client.PostAsync("MemberRouteController/SelectMemberRouteByMemberIdEffectiveDate", request).Result;
                                responseString = response.Content.ReadAsStringAsync().Result;
                            }

                            JObject returnObject = JObject.Parse(responseString);
                            if (returnObject["Result"].ToString() != "NOK")
                            {
                                var hede = JArray.Parse(returnObject["Content"].ToString());
                                List<entMemberRoute> listMemberRoute = hede.ToObject<List<entMemberRoute>>();
                                GlobalVariables.listMemberRoute = listMemberRoute;

                                foreach (var insEntMemberRoute in listMemberRoute)
                                {
                                    lock (dbLock)
                                    {
                                        db.Insert(insEntMemberRoute, typeof(entMemberRoute));
                                    }
                                }

                                lock (dbLock)
                                {
                                    db.Execute("UPDATE entConfiguration SET Value=? WHERE Name='MemberRouteDataDateTime'", DateTime.Now.ToString("yyyy-MM-dd"));
                                }

                                db.Commit(); // İşlem başarılıysa commit yap
                            }
                        }
                        catch (Exception ex)
                        {
                            db.Rollback(); // Hata durumunda rollback yap
                            AddToListUI($"Hata oluştu: {ex.Message}");
                        }


                        EditListUI($" (Toplam: {GlobalVariables.listMemberRoute.Count:N0})");
                    }
                    #endregion

                    #region MemberRouteDetail
                    var memberRouteDetailDataDateTime = db.Table<entConfiguration>()
                                                           .Where(x => x.Name == "MemberRouteDetailDataDateTime")
                                                           .FirstOrDefault();
                    result = db.ExecuteScalar<int>("SELECT COUNT(*) FROM entMemberRouteDetail");

                    if (result == 0 || DateTime.ParseExact(DateTime.Now.ToString("yyyyMMdd"), "yyyyMMdd", CultureInfo.InvariantCulture) > DateTime.ParseExact(memberRouteDetailDataDateTime.Value.Replace("-", ""), "yyyyMMdd", CultureInfo.InvariantCulture))
                    {
                        AddToListUI("Ziyaret Planı Detayı alınıyor...");

                        db.BeginTransaction();

                        lock (dbLock)
                        {
                            db.Execute("DELETE FROM entMemberRouteDetail");
                        }

                        try
                        {
                            string responseString;

                            using (var client = new HttpClient())
                            {
                                client.BaseAddress = new Uri(GlobalVariables.APIPath);
                                client.DefaultRequestHeaders.Accept.Clear();
                                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                                var insEntMemberRoute = new entMemberRoute
                                {
                                    EffectiveDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")),
                                    MemberId = GlobalVariables.CurrentMember.Id
                                };

                                var request = new StringContent(JsonConvert.SerializeObject(insEntMemberRoute), Encoding.UTF8, "application/json");
                                HttpResponseMessage response = client.PostAsync("MemberRouteDetailController/SelectMemberRouteDetailByMemberIdEffectiveDate", request).Result;
                                responseString = response.Content.ReadAsStringAsync().Result;
                            }

                            JObject returnObject = JObject.Parse(responseString);
                            if (returnObject["Result"].ToString() != "NOK")
                            {
                                var hede = JArray.Parse(returnObject["Content"].ToString());
                                List<entMemberRouteDetail> listMemberRouteDetail = hede.ToObject<List<entMemberRouteDetail>>();
                                GlobalVariables.listMemberRouteDetail = listMemberRouteDetail;

                                foreach (var insEntMemberRouteDetail in listMemberRouteDetail)
                                {
                                    lock (dbLock)
                                    {
                                        db.Insert(insEntMemberRouteDetail, typeof(entMemberRouteDetail));
                                    }
                                }

                                lock (dbLock)
                                {
                                    db.Execute("UPDATE entConfiguration SET Value=? WHERE Name='MemberRouteDetailDataDateTime'", DateTime.Now.ToString("yyyy-MM-dd"));
                                }

                                db.Commit(); // İşlem başarılıysa commit yap
                            }
                        }
                        catch (Exception ex)
                        {
                            db.Rollback(); // Hata durumunda rollback yap
                            AddToListUI($"Hata oluştu: {ex.Message}");
                        }


                        lock (dbLock)
                        {
                            db.Execute("DELETE FROM entMemberRouteDetail WHERE CustomerProductId=?", 208);
                        }

                        EditListUI($" (Toplam: {GlobalVariables.listMemberRouteDetail.Count:N0})");
                    }
                    #endregion

                    #region SalesOrderPackageType
                    var salesOrderPackageType = db.Table<entConfiguration>()
                       .Where(x => x.Name == "SalesOrderPackageTypeDataDateTime")
                       .FirstOrDefault();
                    result = db.ExecuteScalar<int>("SELECT COUNT(*) FROM entSalesOrderPackageType");

                    if (result == 0 || DateTime.ParseExact(DateTime.Now.ToString("yyyyMMdd"), "yyyyMMdd", CultureInfo.InvariantCulture) > DateTime.ParseExact(salesOrderPackageType.Value.Replace("-", ""), "yyyyMMdd", CultureInfo.InvariantCulture))
                    {
                        AddToListUI("Ürün paketleme tipleri alınıyor...");

                        db.BeginTransaction();

                        lock (dbLock)
                        {
                            db.Execute("DELETE FROM entSalesOrderPackageType");
                        }

                        try
                        {
                            string responseString;

                            using (var client = new HttpClient())
                            {
                                client.BaseAddress = new Uri(GlobalVariables.APIPath);
                                client.DefaultRequestHeaders.Accept.Clear();
                                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                                var request = new StringContent(string.Empty, Encoding.UTF8, "application/json");
                                HttpResponseMessage response = client.PostAsync("SalesOrderPackageTypeController/SelectSalesOrderPackageType", request).Result;
                                responseString = response.Content.ReadAsStringAsync().Result;
                            }

                            JObject returnObject = JObject.Parse(responseString);
                            if (returnObject["Result"].ToString() != "NOK")
                            {
                                var hede = JArray.Parse(returnObject["Content"].ToString());
                                List<entSalesOrderPackageType> listSalesOrderPackageType = hede.ToObject<List<entSalesOrderPackageType>>();
                                GlobalVariables.listSalesOrderPackageType = listSalesOrderPackageType;

                                foreach (var insEntSalesOrderPackageType in listSalesOrderPackageType)
                                {
                                    lock (dbLock)
                                    {
                                        db.Insert(insEntSalesOrderPackageType, typeof(entSalesOrderPackageType));
                                    }
                                }

                                lock (dbLock)
                                {
                                    db.Execute("UPDATE entConfiguration SET Value=? WHERE Name='SalesOrderPackageTypeDataDateTime'", DateTime.Now.ToString("yyyy-MM-dd"));
                                }

                                db.Commit(); // İşlem başarılıysa commit yap
                            }
                        }
                        catch (Exception ex)
                        {
                            db.Rollback(); // Hata durumunda rollback yap
                            AddToListUI($"Hata oluştu: {ex.Message}");
                        }


                        EditListUI($" (Toplam: {GlobalVariables.listSalesOrderPackageType.Count:N0})");
                    }
                    #endregion

                    #region PromotionType
                    var promotionType = db.Table<entConfiguration>()
                       .Where(x => x.Name == "PromotionTypeDataDateTime")
                       .FirstOrDefault();
                    result = db.ExecuteScalar<int>("SELECT COUNT(*) FROM entPromotionType");

                    if (result == 0 || DateTime.ParseExact(DateTime.Now.ToString("yyyyMMdd"), "yyyyMMdd", CultureInfo.InvariantCulture) > DateTime.ParseExact(promotionType.Value.Replace("-", ""), "yyyyMMdd", CultureInfo.InvariantCulture))
                    {
                        AddToListUI("Promosyon tipleri alınıyor...");

                        db.BeginTransaction();

                        lock (dbLock)
                        {
                            db.Execute("DELETE FROM entPromotionType");
                        }

                        try
                        {
                            string responseString;

                            using (var client = new HttpClient())
                            {
                                client.BaseAddress = new Uri(GlobalVariables.APIPath2);
                                client.DefaultRequestHeaders.Accept.Clear();
                                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                                var request = new StringContent(string.Empty, Encoding.UTF8, "application/json");
                                HttpResponseMessage response = client.PostAsync("PromotionController/SelectPromotionType", request).Result;
                                responseString = response.Content.ReadAsStringAsync().Result;
                            }

                            JObject returnObject = JObject.Parse(responseString);
                            if (returnObject["Result"].ToString() != "NOK")
                            {
                                var hede = JArray.Parse(returnObject["Content"].ToString());
                                List<entPromotionType> listPromotionType = hede.ToObject<List<entPromotionType>>();
                                GlobalVariables.listPromotionType = listPromotionType;

                                foreach (var insEntPromotionType in listPromotionType)
                                {
                                    lock (dbLock)
                                    {
                                        db.Insert(insEntPromotionType, typeof(entPromotionType));
                                    }
                                }

                                lock (dbLock)
                                {
                                    db.Execute("UPDATE entConfiguration SET Value=? WHERE Name='PromotionTypeDataDateTime'", DateTime.Now.ToString("yyyy-MM-dd"));
                                }

                                db.Commit(); // İşlem başarılıysa commit yap
                            }
                        }
                        catch (Exception ex)
                        {
                            db.Rollback(); // Hata durumunda rollback yap
                            AddToListUI($"Hata oluştu: {ex.Message}");
                        }


                        EditListUI($" (Toplam: {GlobalVariables.listPromotionType.Count:N0})");
                    }
                    #endregion


                    db.Close();


                }
                catch (SQLiteException ex)
                {
                    // Log SQLiteException
                    AddToListUI($"SQLite Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    // Log general exception
                    AddToListUI($"Error: {ex.Message}");
                }
            }

        }

        private void AddToListUI(string message)
        {
            RunOnUiThread(() =>
            {
                LinearLayout layout = FindViewById<LinearLayout>(Resource.Id.pnlStatus);

                if (layout.ChildCount == 4 )
                {
                    layout.RemoveAllViews(); // Çok fazla mesaj olursa temizle
                }

                // İlk mesaj için üst boşluk 60dp, diğerleri için 10dp
                int topMargin = layout.ChildCount == 0 ? 90 : 10;
                // Son mesaj ise alt boşluk 0dp, diğer mesajlar için 5dp
                int bottomMargin = layout.ChildCount == 4 ? 0 : 5;

                LinearLayout.LayoutParams layoutParams = new LinearLayout.LayoutParams(
                                                           LinearLayout.LayoutParams.MatchParent,
                                                           LinearLayout.LayoutParams.WrapContent
                                                       );
                layoutParams.SetMargins(20, topMargin, 20, bottomMargin); // Sol, üst, sağ, alt margin ayarları


                TextView txtStatus = new TextView(this)
                {
                    LayoutParameters = layoutParams,
                    Gravity = GravityFlags.Center,
                    Text = message,
                };
                txtStatus.SetTextColor(Android.Graphics.Color.LightGray);
                layout.AddView(txtStatus);
            });
        }
        private void EditListUI(string message)
        {
            RunOnUiThread(() =>
            {
                LinearLayout layout = FindViewById<LinearLayout>(Resource.Id.pnlStatus);
                TextView txtStatus = (TextView)layout.GetChildAt(layout.ChildCount - 1);
                txtStatus.Text = txtStatus.Text.Replace("Bu işlem biraz zaman alabilir...", " ") + message;
            });
        }

    }

    [BroadcastReceiver(Enabled = true)]
    [IntentFilter(new[] { Android.Content.Intent.ActionBootCompleted })]
    public class MyBootReceiver : BroadcastReceiver
    {
        long _downloadId = 0;
        string _destination = string.Empty;
        DownloadManager _manager;
        Activity _currentActivity;

        public MyBootReceiver()
        {

        }
        public MyBootReceiver(long downloadId, string destination, DownloadManager manager, Activity currentActivity)
        {
            _downloadId = downloadId;
            _destination = destination;
            _manager = manager;
            _currentActivity = currentActivity;
        }
        public override void OnReceive(Context context, Intent intent)
        {
            Intent install = new Intent(Intent.ActionView);
            install.SetDataAndType(Android.Net.Uri.Parse("file://" + _destination),
                    _manager.GetMimeTypeForDownloadedFile(_downloadId));

            context.StartActivity(install);
            context.UnregisterReceiver(this);
        }
    }
}