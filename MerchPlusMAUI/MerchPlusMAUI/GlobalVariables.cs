using entMerchPlus;
using Microsoft.Maui.Devices.Sensors;
using System.Data;
using MerchPlusMAUI.Entities;
using IOPath = System.IO.Path;

namespace MerchPlusMAUI
{
    public static class GlobalVariables
    {
        public static string APIPath { get; set; } = "http://merchplus.net/api/api/";
        public static string APIPath2 { get; set; } = "http://77.245.158.32:8080/api/";
        public static string WebsitePath { get; set; } = "http://merchplus.net/";

        public static string CurrentMemberId { get; set; }
        public static string CurrentUserId { get; set; }
        public static string CurrentMemberName { get; set; }
        public static string CurrentMemberSurname { get; set; }
        public static string CurrentUsername { get; set; }
        public static string CurrentProvinceId { get; set; }
        public static string CurrentProvinceName { get; set; }
        public static string CurrentUserCode { get; set; }
        public static string CurrentCustomerId { get; set; }
        public static string CurrentCustomerAddress { get; set; }
        public static string CurrentCustomerBarCode { get; set; }
        public static string CurrentCustomerCode { get; set; }
        public static string CurrentCustomerName { get; set; }
        public static string CurrentCustomerRouteDay { get; set; }
        public static string CurrentCustomerTel { get; set; }
        public static string CurrentDistrictName { get; set; }
        public static string CurrentRegistrationId { get; set; }
        public static string CurrentApplicationVersion { get; set; } = "2.0";

        public static string DatabasePath { get; set; } = IOPath.Combine(FileSystem.AppDataDirectory, "merchplus.db3");

#if DEBUG
        public static bool IsDebug { get; } = true;
#endif

        // Static Lists for Caching
        public static List<entRetail> ListRetail { get; set; } = new();
        public static List<entRetailShop> ListRetailShop { get; set; } = new();
        public static List<entCustomerBrandCategory> ListCustomerBrandCategory { get; set; } = new();
        public static List<entMemberRoute> ListMemberRoute { get; set; } = new();
        public static List<entMemberRouteDetail> ListMemberRouteDetail { get; set; } = new();
        public static List<entCustomerProduct> ListCustomerProduct { get; set; } = new();
        public static List<entSalesOrderPackageType> ListSalesOrderPackageType { get; set; } = new();
        public static List<entPromotionType> ListPromotionType { get; set; } = new();
        public static List<entCustomerFormFieldVisibility> ListCustomerFormFieldVisibility { get; set; } = new();

        // Flags and Status
        public static bool MemberLocationAgentStarted { get; set; }
        public static bool DatabaseSyncStarted { get; set; }

        // Member Related
        public static byte[] MemberProfilePictureByteArray { get; set; } = Array.Empty<byte>();
        private static entMember _currentMember = new();
        public static entMember CurrentMember
        {
            get => _currentMember;
            set => _currentMember = value;
        }

        // Location
        public static Location MemberLastCoordinate { get; set; } = new();

        // Data Tables
        public static DataTable MemberRoutePhotoDataTable { get; set; } = new();

        // Current Transaction
        public static int CurrentSalesOrderId { get; set; }

        // List definitions for data synchronization
        public static List<entRetail> listRetail { get; set; } = new();
        public static List<entRetailShop> listRetailShop { get; set; } = new();
        public static List<entCustomerBrandCategory> listCustomerBrandCategory { get; set; } = new();
        public static List<entCustomerProduct> listCustomerProduct { get; set; } = new();
        public static List<entMemberRoute> listMemberRoute { get; set; } = new();
        public static List<entMemberRouteDetail> listMemberRouteDetail { get; set; } = new();
        public static List<entSalesOrderPackageType> listSalesOrderPackageType { get; set; } = new();
        public static List<entPromotionType> listPromotionType { get; set; } = new();
        public static List<entCustomerFormFieldVisibility> listCustomerFormFieldVisibility { get; set; } = new();
    }

    // If Location class is not defined in entMerchPlus, we'll keep this one
    public class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
} 