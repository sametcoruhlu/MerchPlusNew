using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace entMerchPlus
{
    /// <summary>
    /// Reflection of a row of database table [MemberRouteDetail]
    /// </summary>
    [Serializable()]
    [JsonObject(MemberSerialization.OptOut)]
    public class entMemberRouteDetail
    {
        #region MEMBERS
        /// <summary>
        /// [Id] column that will hold value from database table [MemberRouteDetail]
        /// </summary>
        private int memId;

        /// <summary>
        /// [MemberRouteId] column that will hold value from database table [MemberRouteDetail]
        /// </summary>
        private int? memMemberRouteId;

        /// <summary>
        /// [CustomerProductId] column that will hold value from database table [MemberRouteDetail]
        /// </summary>
        private int? memCustomerProductId;

        /// <summary>
        /// [ShelfIndex] column that will hold value from database table [MemberRouteDetail]
        /// </summary>
        private int? memShelfIndex;

        /// <summary>
        /// [ShelfStock] column that will hold value from database table [MemberRouteDetail]
        /// </summary>
        private int? memShelfStock;

        /// <summary>
        /// [ShelfPrice] column that will hold value from database table [MemberRouteDetail]
        /// </summary>
        private decimal? memShelfPrice;

        /// <summary>
        /// [FaceCount] column that will hold value from database table [MemberRouteDetail]
        /// </summary>
        private int? memFaceCount;

        /// <summary>
        /// [PromotionTypeId] column that will hold value from database table [MemberRouteDetail]
        /// </summary>
        private int? memPromotionTypeId;

        /// <summary>
        /// [WarehouseStock] column that will hold value from database table [MemberRouteDetail]
        /// </summary>
        private int? memWarehouseStock;

        /// <summary>
        /// [ExpirationDate] column that will hold value from database table [MemberRouteDetail]
        /// </summary>
        private DateTime? memExpirationDate;

        /// <summary>
        /// [Notes] column that will hold value from database table [MemberRouteDetail]
        /// </summary>
        private string memNotes;

        /// <summary>
        /// [ProfilePicturePath] column that will hold value from database table [MemberRouteDetail]
        /// </summary>
        private string memProfilePicturePath;

        /// <summary>
        /// [CreatedOn] column that will hold value from database table [MemberRouteDetail]
        /// </summary>
        private DateTime? memCreatedOn;

        /// <summary>
        /// [IsSentToServer] column that will hold value from database table [MemberRouteDetail]
        /// </summary>
        private bool? memIsSentToServer;

        /// <summary>
        /// [SentToServerOn] column that will hold value from database table [MemberRouteDetail]
        /// </summary>
        private DateTime? memSentToServerOn;

        /// <summary>
        /// [ActivityTypeName] column that will hold value from database table [MemberRouteDetail]
        /// </summary>
        private string memActivityTypeName;

        /// <summary>
        /// [DiscountedPrice] column that will hold value from database table [MemberRouteDetail]
        /// </summary>
        private decimal? memDiscountedPrice;

        #endregion
        #region PROPERTIES
        /// <summary>
        /// [memId] value for Set/Get operations
        /// </summary>
        public int Id
        {
            get { return memId; }
            set { memId = value; }
        }

        /// <summary>
        /// [memMemberRouteId] value for Set/Get operations
        /// </summary>
        public int? MemberRouteId
        {
            get { return memMemberRouteId; }
            set { memMemberRouteId = value; }
        }

        /// <summary>
        /// [memCustomerProductId] value for Set/Get operations
        /// </summary>
        public int? CustomerProductId
        {
            get { return memCustomerProductId; }
            set { memCustomerProductId = value; }
        }

        /// <summary>
        /// [memShelfIndex] value for Set/Get operations
        /// </summary>
        public int? ShelfIndex
        {
            get { return memShelfIndex; }
            set { memShelfIndex = value; }
        }

        /// <summary>
        /// [memShelfStock] value for Set/Get operations
        /// </summary>
        public int? ShelfStock
        {
            get { return memShelfStock; }
            set { memShelfStock = value; }
        }

        /// <summary>
        /// [memShelfPrice] value for Set/Get operations
        /// </summary>
        public decimal? ShelfPrice
        {
            get { return memShelfPrice; }
            set { memShelfPrice = value; }
        }

        /// <summary>
        /// [memFaceCount] value for Set/Get operations
        /// </summary>
        public int? FaceCount
        {
            get { return memFaceCount; }
            set { memFaceCount = value; }
        }

        /// <summary>
        /// [memPromotionTypeId] value for Set/Get operations
        /// </summary>
        public int? PromotionTypeId
        {
            get { return memPromotionTypeId; }
            set { memPromotionTypeId = value; }
        }

        /// <summary>
        /// [memWarehouseStock] value for Set/Get operations
        /// </summary>
        public int? WarehouseStock
        {
            get { return memWarehouseStock; }
            set { memWarehouseStock = value; }
        }

        /// <summary>
        /// [memExpirationDate] value for Set/Get operations
        /// </summary>
        public DateTime? ExpirationDate
        {
            get { return memExpirationDate; }
            set { memExpirationDate = value; }
        }

        /// <summary>
        /// [memNotes] value for Set/Get operations
        /// </summary>
        public string Notes
        {
            get { return memNotes; }
            set { memNotes = value; }
        }

        /// <summary>
        /// [memProfilePicturePath] value for Set/Get operations
        /// </summary>
        public string ProfilePicturePath
        {
            get { return memProfilePicturePath; }
            set { memProfilePicturePath = value; }
        }

        /// <summary>
        /// [memCreatedOn] value for Set/Get operations
        /// </summary>
        public DateTime? CreatedOn
        {
            get { return memCreatedOn; }
            set { memCreatedOn = value; }
        }

        /// <summary>
        /// [memIsSentToServer] value for Set/Get operations
        /// </summary>
        public bool? IsSentToServer
        {
            get { return memIsSentToServer; }
            set { memIsSentToServer = value; }
        }

        /// <summary>
        /// [memSentToServerOn] value for Set/Get operations
        /// </summary>
        public DateTime? SentToServerOn
        {
            get { return memSentToServerOn; }
            set { memSentToServerOn = value; }
        }

        /// <summary>
        /// [memActivityTypeName] value for Set/Get operations
        /// </summary>
        public string ActivityTypeName
        {
            get { return memActivityTypeName; }
            set { memActivityTypeName = value; }
        }

        /// <summary>
        /// [memDiscountedPrice] value for Set/Get operations
        /// </summary>
        public decimal? DiscountedPrice
        {
            get { return memDiscountedPrice; }
            set { memDiscountedPrice = value; }
        }

        #endregion
        #region CONSTRUCTORS
        /// <summary>
        /// entMemberRouteDetail class constructor
        /// </summary>
        /// <param name="parMemberRouteId">MemberRouteId is set/get by this property.</param>
        /// <param name="parCustomerProductId">CustomerProductId is set/get by this property.</param>
        /// <param name="parShelfIndex">ShelfIndex is set/get by this property.</param>
        /// <param name="parShelfStock">ShelfStock is set/get by this property.</param>
        /// <param name="parShelfPrice">ShelfPrice is set/get by this property.</param>
        /// <param name="parFaceCount">FaceCount is set/get by this property.</param>
        /// <param name="parPromotionTypeId">PromotionTypeId is set/get by this property.</param>
        /// <param name="parWarehouseStock">WarehouseStock is set/get by this property.</param>
        /// <param name="parExpirationDate">ExpirationDate is set/get by this property.</param>
        /// <param name="parNotes">Notes is set/get by this property.</param>
        /// <param name="parProfilePicturePath">ProfilePicturePath is set/get by this property.</param>
        /// <param name="parCreatedOn">CreatedOn is set/get by this property.</param>
        /// <param name="parIsSentToServer">IsSentToServer is set/get by this property.</param>
        /// <param name="parSentToServerOn">SentToServerOn is set/get by this property.</param>
        /// <param name="parActivityTypeName">ActivityTypeName is set/get by this property.</param>
        /// <param name="parDiscountedPrice">DiscountedPrice is set/get by this property.</param>
        public entMemberRouteDetail(int? parMemberRouteId, int? parCustomerProductId, int? parShelfIndex, int? parShelfStock, decimal? parShelfPrice, int? parFaceCount, int? parPromotionTypeId, int? parWarehouseStock, DateTime? parExpirationDate, string parNotes, string parProfilePicturePath, DateTime? parCreatedOn, bool? parIsSentToServer, DateTime? parSentToServerOn, string parActivityTypeName, decimal? parDiscountedPrice)
        {
            this.memMemberRouteId = parMemberRouteId;
            this.memCustomerProductId = parCustomerProductId;
            this.memShelfIndex = parShelfIndex;
            this.memShelfStock = parShelfStock;
            this.memShelfPrice = parShelfPrice;
            this.memFaceCount = parFaceCount;
            this.memPromotionTypeId = parPromotionTypeId;
            this.memWarehouseStock = parWarehouseStock;
            this.memExpirationDate = parExpirationDate;
            this.memNotes = parNotes;
            this.memProfilePicturePath = parProfilePicturePath;
            this.memCreatedOn = parCreatedOn;
            this.memIsSentToServer = parIsSentToServer;
            this.memSentToServerOn = parSentToServerOn;
            this.memActivityTypeName = parActivityTypeName;
            this.memDiscountedPrice = parDiscountedPrice;
        }

        /// <summary>
        /// entMemberRouteDetail class constructor
        /// </summary>
        /// <param name="parId">Id is set/get by this property.</param>
        /// <param name="parMemberRouteId">MemberRouteId is set/get by this property.</param>
        /// <param name="parCustomerProductId">CustomerProductId is set/get by this property.</param>
        /// <param name="parShelfIndex">ShelfIndex is set/get by this property.</param>
        /// <param name="parShelfStock">ShelfStock is set/get by this property.</param>
        /// <param name="parShelfPrice">ShelfPrice is set/get by this property.</param>
        /// <param name="parFaceCount">FaceCount is set/get by this property.</param>
        /// <param name="parPromotionTypeId">PromotionTypeId is set/get by this property.</param>
        /// <param name="parWarehouseStock">WarehouseStock is set/get by this property.</param>
        /// <param name="parExpirationDate">ExpirationDate is set/get by this property.</param>
        /// <param name="parNotes">Notes is set/get by this property.</param>
        /// <param name="parProfilePicturePath">ProfilePicturePath is set/get by this property.</param>
        /// <param name="parCreatedOn">CreatedOn is set/get by this property.</param>
        /// <param name="parIsSentToServer">IsSentToServer is set/get by this property.</param>
        /// <param name="parSentToServerOn">SentToServerOn is set/get by this property.</param>
        /// <param name="parActivityTypeName">ActivityTypeName is set/get by this property.</param>
        /// <param name="parDiscountedPrice">DiscountedPrice is set/get by this property.</param>
        public entMemberRouteDetail(int parId, int? parMemberRouteId, int? parCustomerProductId, int? parShelfIndex, int? parShelfStock, decimal? parShelfPrice, int? parFaceCount, int? parPromotionTypeId, int? parWarehouseStock, DateTime? parExpirationDate, string parNotes, string parProfilePicturePath, DateTime? parCreatedOn, bool? parIsSentToServer, DateTime? parSentToServerOn, string parActivityTypeName, decimal? parDiscountedPrice)
        {
            this.memId = parId;
            this.memMemberRouteId = parMemberRouteId;
            this.memCustomerProductId = parCustomerProductId;
            this.memShelfIndex = parShelfIndex;
            this.memShelfStock = parShelfStock;
            this.memShelfPrice = parShelfPrice;
            this.memFaceCount = parFaceCount;
            this.memPromotionTypeId = parPromotionTypeId;
            this.memWarehouseStock = parWarehouseStock;
            this.memExpirationDate = parExpirationDate;
            this.memNotes = parNotes;
            this.memProfilePicturePath = parProfilePicturePath;
            this.memCreatedOn = parCreatedOn;
            this.memIsSentToServer = parIsSentToServer;
            this.memSentToServerOn = parSentToServerOn;
            this.memActivityTypeName = parActivityTypeName;
            this.memDiscountedPrice = parDiscountedPrice;
        }

        /// <summary>
        /// entMemberRouteDetail class constructor
        /// </summary>
        public entMemberRouteDetail()
        {
        }

        #endregion
    }
}
