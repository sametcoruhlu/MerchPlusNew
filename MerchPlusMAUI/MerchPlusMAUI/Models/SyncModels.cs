using entMerchPlus;
using SQLite;
using System.Text.Json.Serialization;
using System;

namespace MerchPlusMAUI.Models
{
    [SQLite.Table("entMemberRoute")]
    public class MemberRoute
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public bool IsSentToServer { get; set; }
        public string CustomerId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string MemberId { get; set; }
        public string Notes { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletedOn { get; set; }
    }

    [SQLite.Table("entMemberRouteDetail")]
    public class MemberRouteDetail
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int MemberRouteId { get; set; }
        public bool IsSentToServer { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    [SQLite.Table("entMemberRoutePhoto")]
    public class MemberRoutePhoto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int MemberRouteId { get; set; }
        public string PhotoPath { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    [SQLite.Table("entSalesOrder")]
    public class SalesOrder
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int MemberRouteId { get; set; }
        public string CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
    }

    [SQLite.Table("entSalesOrderDetail")]
    public class SalesOrderDetail
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int SalesOrderId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }

    [SQLite.Table("entProductExpiration")]
    public class ProductExpiration
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int MemberRouteId { get; set; }
        public bool IsSentToServer { get; set; }
        public string ProductId { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Quantity { get; set; }
    }

    [SQLite.Table("entMemberDisplayPhotos")]
    public class MemberDisplayPhotos
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string DisplayPicturePath { get; set; }
        public int CustomerProductId { get; set; }
        public string MemberId { get; set; }
        public int MemberRouteId { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsSentToServer { get; set; }
        public int PromotionTypeId { get; set; }
    }

    [SQLite.Table("entConfiguration")]
    public class Configuration
    {
        [PrimaryKey]
        public string Name { get; set; }
        public string Value { get; set; }
    }

    [Serializable]
    public class MemberRouteSyncData
    {
        [JsonPropertyName("memberRoute")]
        public entMemberRoute MemberRoute { get; set; } = new();

        [JsonPropertyName("memberRouteDetail")]
        public List<entMemberRouteDetail> MemberRouteDetail { get; set; } = new();

        [JsonPropertyName("memberRoutePhoto")]
        public List<entMemberRoutePhoto> MemberRoutePhoto { get; set; } = new();

        [JsonPropertyName("salesOrder")]
        public List<entSalesOrder> SalesOrder { get; set; } = new();

        [JsonPropertyName("salesOrderDetail")]
        public List<entSalesOrderDetail> SalesOrderDetail { get; set; } = new();

        [JsonPropertyName("productExpiration")]
        public List<entProductExpiration> ProductExpiration { get; set; } = new();
    }

    public class CustomerProductPicture
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        [Column("PhotoPath")]
        public string PhotoPath { get; set; } = string.Empty;
        
        [Column("LastUpdateTime")]
        public DateTime LastUpdateTime { get; set; }
        
        [Column("IsSynced")]
        public bool IsSynced { get; set; }
    }

    public class CustomerProductPictureResponse
    {
        public required string ProductId { get; set; }
        public required string Value { get; set; }
    }

    public class CustomerProductPictureRequest
    {
        public required string CustomerId { get; set; }
        public required string MemberId { get; set; }
        public required string Notes { get; set; }
    }

    public class CustomerProductPictureSync
    {
        public required string ProductId { get; set; }
        public required string PhotoPath { get; set; }
    }

    public class RetailProfilePictureSync
    {
        public required string DisplayPicturePath { get; set; }
        public required string MemberId { get; set; }
    }

    public class ConfigurationSync
    {
        public required string Name { get; set; }
        public required string Value { get; set; }
    }

    public class CustomerVisit
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        [Column("CustomerId")]
        public string CustomerId { get; set; } = string.Empty;
        
        [Column("MemberId")]
        public string MemberId { get; set; } = string.Empty;
        
        [Column("Notes")]
        public string Notes { get; set; } = string.Empty;
        
        [Column("VisitDate")]
        public DateTime VisitDate { get; set; }
        
        [Column("IsSynced")]
        public bool IsSynced { get; set; }
    }

    public class CustomerProduct
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        [Column("ProductId")]
        public string ProductId { get; set; } = string.Empty;
        
        [Column("Quantity")]
        public int Quantity { get; set; }
        
        [Column("LastUpdateTime")]
        public DateTime LastUpdateTime { get; set; }
        
        [Column("IsSynced")]
        public bool IsSynced { get; set; }
    }

    public class CustomerOrder
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        [Column("CustomerId")]
        public string CustomerId { get; set; } = string.Empty;
        
        [Column("Status")]
        public string Status { get; set; } = string.Empty;
        
        [Column("OrderDate")]
        public DateTime OrderDate { get; set; }
        
        [Column("IsSynced")]
        public bool IsSynced { get; set; }
    }
} 