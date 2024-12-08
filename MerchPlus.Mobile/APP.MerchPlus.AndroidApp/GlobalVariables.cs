using entMerchPlus;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace APP.MerchPlus
{
    public static class GlobalVariables
    {

        public static string CurrentApplicationVersion = "2.0";

       
        public static string DatabasePath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), "merchplus.db3");

#if DEBUG
        //public static string ApplicationVersionUpdate = "https://merchplusapi.azurewebsites.net/mp.apk";
        public static string APIPath = "http://merchplus.net/api/api/";
        public static string APIPath2 = "http://77.245.158.32:8080/api/";
        //public static string APIPath = "http://localhost:54166/api/";

        public static string WebsitePath = "http://merchplus.net/";
#else
        public static string WebsitePath = "http://merchplus.net/";
        public static string APIPath = "http://merchplus.net/api/api/";
        public static string APIPath2 = "http://77.245.158.32:8080/api/";
#endif

        public static List<entRetail> listRetail = new List<entRetail>();
        public static List<entRetailShop> listRetailShop = new List<entRetailShop>();
        public static List<entCustomerBrandCategory> listCustomerBrandCategory = new List<entCustomerBrandCategory>();
        public static List<entMemberRoute> listMemberRoute = new List<entMemberRoute>();
        public static List<entMemberRouteDetail> listMemberRouteDetail = new List<entMemberRouteDetail>();
        public static List<entCustomerProduct> listCustomerProduct = new List<entCustomerProduct>();
        public static List<entSalesOrderPackageType> listSalesOrderPackageType = new List<entSalesOrderPackageType>();
        public static List<entPromotionType> listPromotionType = new List<entPromotionType>();
        public static bool MemberLocationAgentStarted = false;
        public static bool DatabaseSyncStarted = false;
        public static byte[] MemberProfilePictureByteArray = new byte[] { };
        public static Position MemberLastCoordinate = new Position();
        public static DataTable MemberRoutePhotoDataTable = new DataTable();
        public static int currentSalesOrderId = 0;

        private static entMember currentMember = new entMember();
        public static entMember CurrentMember
        {
            get { return currentMember; }
            set { currentMember = value; }
        }

    }
}