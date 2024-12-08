using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Util;
using APP.MerchPlus.Entities;
using entMerchPlus;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;

namespace APP.MerchPlus
{

    [Service]
    public class LocationAgentService : Service, ILocationListener
    {
        static readonly string TAG = "X:" + "Coordinate Agent Service";
        LocationManager _locationManager;
        string _locationProvider = string.Empty;

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            int WaitTime = 5000;

            #region Create Manager and Start Service
            _locationManager = (LocationManager)GetSystemService(LocationService);
            Criteria criteriaForLocationService = new Criteria
            {
                Accuracy = Accuracy.Coarse
            };
            IList<string> acceptableLocationProviders = _locationManager.GetProviders(criteriaForLocationService, true);

            if (acceptableLocationProviders.Any())
            {
                for (int i = 0; i < acceptableLocationProviders.Count; i++)
                {
                    _locationProvider = acceptableLocationProviders.ElementAt(i);
                    _locationManager.RequestLocationUpdates(_locationProvider, WaitTime, 0, this);
                }
            }
            else
            {
                _locationProvider = string.Empty;
            }
            Log.Debug(TAG, "Using " + _locationProvider + ".");
            #endregion



            return StartCommandResult.NotSticky;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        public override IBinder OnBind(Intent intent)
        {
            // This example isn't of a bound service, so we just return NULL.
            return null;
        }

        public void OnLocationChanged(Location location)
        {
            var db = new SQLiteConnection(GlobalVariables.DatabasePath);
            entMemberCoordinate insEntMemberCoordinate = new entMemberCoordinate();
            insEntMemberCoordinate.MemberId = GlobalVariables.CurrentMember.Id;
            insEntMemberCoordinate.CreatedOn = DateTime.Now;
            insEntMemberCoordinate.CoordinateX = Convert.ToDecimal(location.Latitude);
            insEntMemberCoordinate.CoordinateY = Convert.ToDecimal(location.Longitude);
            db.Insert(insEntMemberCoordinate);

            GlobalVariables.MemberLastCoordinate.Latitude = Convert.ToDouble(location.Latitude);
            GlobalVariables.MemberLastCoordinate.Longitude = Convert.ToDouble(location.Longitude);

            Log.Debug(TAG, " New coordinate arrived :  " + JsonConvert.SerializeObject(insEntMemberCoordinate));

        }

        public void OnProviderDisabled(string provider)
        {

        }

        public void OnProviderEnabled(string provider)
        {

        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {

        }
    }

    [Service]
    public class DatabaseSyncService : Service
    {

        static readonly int TimerWait = 300000;
        Timer _timer;

        static readonly int TimerWait_MemberRoute = 360000;
        Timer _timerMemberRoute;

        static readonly int TimerWait_MemberDisplayPhoto = 360000;
        Timer _timerMemberDisplayPhoto;

        Timer _timerDatabaseCheck;

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            _timer = new Timer(o =>
            {
                #region User Coordinates to Server
                try
                {
                    var db = new SQLiteConnection(GlobalVariables.DatabasePath);
                    var rows = db.Query<entMemberCoordinate>("SELECT * from entMemberCoordinate");

                    if (rows.Count > 0)
                    {
                        var responseString = string.Empty;
                        using (var client = new HttpClient())
                        {
                            //setup client
                            client.BaseAddress = new System.Uri(GlobalVariables.APIPath);
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                            //make request
                            var request = new StringContent(JsonConvert.SerializeObject(rows), Encoding.UTF8, "application/json");
                            HttpResponseMessage response = client.PostAsync("MemberCoordinateController/InsertMemberCoordinateFromAndroidArray", request).Result;
                            responseString = response.Content.ReadAsStringAsync().Result;
                        }

                        JObject returnObject = (JObject)JsonConvert.DeserializeObject(responseString);
                        if (Convert.ToString(returnObject["Result"]) != "NOK")
                        {
                            #region Corrdinates sent to server, delete from internal database
                            db.DeleteAll<entMemberCoordinate>();
                            #endregion
                        }
                        else
                        {
                            Console.WriteLine("Database Sync Failed > Member Coordinate upload");
                        }
                    }
                }
                catch (Exception ex)
                {

                }
                #endregion
            }, null, 0, TimerWait);

            _timerMemberRoute = new Timer(o =>
            {

                Console.WriteLine("Syncronizing data with Server > MemerRoute!");

                #region MemberRoute to Server
                try
                {
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
                        using (var client = new HttpClient())
                        {
                            //setup client
                            client.BaseAddress = new System.Uri(GlobalVariables.APIPath);
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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

                        JObject returnObject = (JObject)JsonConvert.DeserializeObject(responseString);
                        if (Convert.ToString(returnObject["Result"]) != "NOK")
                        {
                            #region All information sent to server. Mark as complete
                            db.Execute("UPDATE entMemberRoute SET IsSentToServer=? WHERE Id=?", true, insSyncData.MemberRoute.Id);
                            db.Execute("UPDATE entMemberRouteDetail SET IsSentToServer=? WHERE MemberRouteId=?", true, insSyncData.MemberRoute.Id);
                            db.Execute("UPDATE entProductExpiration SET IsSentToServer=? WHERE MemberRouteId=?", true, insSyncData.MemberRoute.Id);
                            #endregion
                        }
                        else
                        {
                            Console.WriteLine("Database Sync Failed > MemberRoute");
                        }
                    }
                }
                catch (Exception ex)
                {

                }
                #endregion
            }, null, 0, TimerWait_MemberRoute);

            _timerMemberDisplayPhoto = new Timer(o =>
            {
                Console.WriteLine("Syncronizing data with Server > MemerDisplayPhoto!");

                #region Member Display Photo to Server
                try
                {
                    var db = new SQLiteConnection(GlobalVariables.DatabasePath);
                    var rows = db.Query<entMemberDisplayPhotos>("SELECT * from entMemberDisplayPhotos WHERE IsSentToServer=false");

                    foreach (var memberDisplayPhoto in rows)
                    {
                        #region Prepare Data
                        entMemberDisplayPhotos memberDisplayPhotos = new entMemberDisplayPhotos();
                        memberDisplayPhotos.DisplayPicturePath = memberDisplayPhoto.DisplayPicturePath;
                        memberDisplayPhotos.CustomerProductId = memberDisplayPhoto.CustomerProductId;
                        memberDisplayPhotos.MemberId = memberDisplayPhoto.MemberId;
                        memberDisplayPhotos.MemberRouteId = memberDisplayPhoto.MemberRouteId;
                        memberDisplayPhotos.CreatedOn = memberDisplayPhoto.CreatedOn;
                        memberDisplayPhotos.IsSentToServer = true;
                        memberDisplayPhotos.PromotionTypeId = memberDisplayPhoto.PromotionTypeId;
                        #endregion

                        var responseString = string.Empty;
                        using (var client = new HttpClient())
                        {
                            //setup client
                            client.BaseAddress = new System.Uri(GlobalVariables.APIPath2);
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                            //make request
                            var request = new StringContent(JsonConvert.SerializeObject(memberDisplayPhotos), Encoding.UTF8, "application/json");
                            HttpResponseMessage response = client.PostAsync("MemberDisplayPhotosController/InsertMemberDisplayPhotos", request).Result;
                            responseString = response.Content.ReadAsStringAsync().Result;
                        }

                        JObject returnObject = (JObject)JsonConvert.DeserializeObject(responseString);
                        if (Convert.ToString(returnObject["Result"]) != "NOK")
                        {
                            #region All information sent to server. Mark as complete
                            db.Execute("UPDATE entMemberDisplayPhotos SET IsSentToServer=? WHERE CustomerProductId=?", true, memberDisplayPhotos.CustomerProductId);
                            #endregion
                        }
                        else
                        {
                            Console.WriteLine("Database Sync Failed > MemberDisplayPhoto");
                        }
                    }

                }
                catch (Exception)
                {

                    throw;
                }

                #endregion

            }, null, 0, TimerWait_MemberDisplayPhoto);

            _timerDatabaseCheck = new Timer(o =>
            {

                Console.WriteLine("Syncronizing data with Server > MemerRoute!");

                #region MemberRoute to Server
                try
                {
                    var db = new SQLiteConnection(GlobalVariables.DatabasePath);
                    var rows = db.Query<entConfiguration>("SELECT * from entConfiguration WHERE Name='RetailDataDateTime'");

                    if (Convert.ToDateTime(rows[0].Value) < new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day))
                    {
                        FileInfo insFileInfo = new FileInfo(GlobalVariables.DatabasePath);
                        insFileInfo.Delete();
                        Process.KillProcess(Process.MyPid());
                    }
                }
                catch (Exception ex)
                {

                }
                #endregion
            }, null, 0, 15000);

            return StartCommandResult.Sticky;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        public override IBinder OnBind(Intent intent)
        {
            // This example isn't of a bound service, so we just return NULL.
            return null;
        }
    }

    [Serializable()]
    [JsonObject(MemberSerialization.OptOut)]
    public class MemberRouteSyncData
    {
        public MemberRouteSyncData()
        {

        }

        private entMemberRoute memberRoute = new entMemberRoute();
        public entMemberRoute MemberRoute
        {
            get { return memberRoute; }
            set { memberRoute = value; }
        }

        private List<entMemberRouteDetail> memberRouteDetail = new List<entMemberRouteDetail>();
        public List<entMemberRouteDetail> MemberRouteDetail
        {
            get { return memberRouteDetail; }
            set { memberRouteDetail = value; }
        }

        private List<entMemberRoutePhoto> memberRoutePhoto = new List<entMemberRoutePhoto>();
        public List<entMemberRoutePhoto> MemberRoutePhoto
        {
            get { return memberRoutePhoto; }
            set { memberRoutePhoto = value; }
        }

        private List<entSalesOrder> salesOrder = new List<entSalesOrder>();
        public List<entSalesOrder> SalesOrder
        {
            get { return salesOrder; }
            set { salesOrder = value; }
        }

        private List<entSalesOrderDetail> salesOrderDetail = new List<entSalesOrderDetail>();
        public List<entSalesOrderDetail> SalesOrderDetail
        {
            get { return salesOrderDetail; }
            set { salesOrderDetail = value; }
        }

        private List<entProductExpiration> productExpiration = new List<entProductExpiration>();
        public List<entProductExpiration> ProductExpiration
        {
            get { return productExpiration; }
            set { productExpiration = value; }
        }
    }
}