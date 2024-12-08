using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace entMerchPlus
{
    /// <summary>
    /// Reflection of a row of database table [CustomerProduct]
    /// </summary>
    [Serializable()]
    [JsonObject(MemberSerialization.OptOut)]
    public class entCustomerProduct
    {
        #region MEMBERS
        /// <summary>
        /// [Id] column that will hold value from database table [CustomerProduct]
        /// </summary>
        private int memId;

        /// <summary>
        /// [CustomerId] column that will hold value from database table [CustomerProduct]
        /// </summary>
        private int? memCustomerId;

        /// <summary>
        /// [CustomerBrandId] column that will hold value from database table [CustomerProduct]
        /// </summary>
        private int? memCustomerBrandId;

        /// <summary>
        /// [CustomerBrandCategoryId] column that will hold value from database table [CustomerProduct]
        /// </summary>
        private int? memCustomerBrandCategoryId;

        /// <summary>
        /// [ProductBarcode] column that will hold value from database table [CustomerProduct]
        /// </summary>
        private string memProductBarcode;

        /// <summary>
        /// [SuggestedListPrice] column that will hold value from database table [CustomerProduct]
        /// </summary>
        private decimal? memSuggestedListPrice;

        /// <summary>
        /// [SuggestedActivityPrice] column that will hold value from database table [CustomerProduct]
        /// </summary>
        private decimal? memSuggestedActivityPrice;

        /// <summary>
        /// [PackagingSize] column that will hold value from database table [CustomerProduct]
        /// </summary>
        private decimal? memPackagingSize;

        /// <summary>
        /// [PackagingTypeId] column that will hold value from database table [CustomerProduct]
        /// </summary>
        private int? memPackagingTypeId;

        /// <summary>
        /// [Name] column that will hold value from database table [CustomerProduct]
        /// </summary>
        private string memName;

        /// <summary>
        /// [ProfilePicturePath] column that will hold value from database table [CustomerProduct]
        /// </summary>
        private string memProfilePicturePath;

        /// <summary>
        /// [ProductCode] column that will hold value from database table [CustomerProduct]
        /// </summary>
        private string memProductCode;

        /// <summary>
        /// [CaseBarcode] column that will hold value from database table [CustomerProduct]
        /// </summary>
        private string memCaseBarcode;

        /// <summary>
        /// [BoxBarcode] column that will hold value from database table [CustomerProduct]
        /// </summary>
        private string memBoxBarcode;

        /// <summary>
        /// [CaseEach] column that will hold value from database table [CustomerProduct]
        /// </summary>
        private int? memCaseEach;

        /// <summary>
        /// [CreatedBy] column that will hold value from database table [CustomerProduct]
        /// </summary>
        private string memCreatedBy;

        /// <summary>
        /// [CreatedOn] column that will hold value from database table [CustomerProduct]
        /// </summary>
        private DateTime? memCreatedOn;

        /// <summary>
        /// [UpdatedBy] column that will hold value from database table [CustomerProduct]
        /// </summary>
        private string memUpdatedBy;

        /// <summary>
        /// [UpdatedOn] column that will hold value from database table [CustomerProduct]
        /// </summary>
        private DateTime? memUpdatedOn;

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
        /// [memCustomerId] value for Set/Get operations
        /// </summary>
        public int? CustomerId
        {
            get { return memCustomerId; }
            set { memCustomerId = value; }
        }

        /// <summary>
        /// [memCustomerBrandId] value for Set/Get operations
        /// </summary>
        public int? CustomerBrandId
        {
            get { return memCustomerBrandId; }
            set { memCustomerBrandId = value; }
        }

        /// <summary>
        /// [memCustomerBrandCategoryId] value for Set/Get operations
        /// </summary>
        public int? CustomerBrandCategoryId
        {
            get { return memCustomerBrandCategoryId; }
            set { memCustomerBrandCategoryId = value; }
        }

        /// <summary>
        /// [memProductBarcode] value for Set/Get operations
        /// </summary>
        public string ProductBarcode
        {
            get { return memProductBarcode; }
            set { memProductBarcode = value; }
        }

        /// <summary>
        /// [memSuggestedListPrice] value for Set/Get operations
        /// </summary>
        public decimal? SuggestedListPrice
        {
            get { return memSuggestedListPrice; }
            set { memSuggestedListPrice = value; }
        }

        /// <summary>
        /// [memSuggestedActivityPrice] value for Set/Get operations
        /// </summary>
        public decimal? SuggestedActivityPrice
        {
            get { return memSuggestedActivityPrice; }
            set { memSuggestedActivityPrice = value; }
        }

        /// <summary>
        /// [memPackagingSize] value for Set/Get operations
        /// </summary>
        public decimal? PackagingSize
        {
            get { return memPackagingSize; }
            set { memPackagingSize = value; }
        }

        /// <summary>
        /// [memPackagingTypeId] value for Set/Get operations
        /// </summary>
        public int? PackagingTypeId
        {
            get { return memPackagingTypeId; }
            set { memPackagingTypeId = value; }
        }

        /// <summary>
        /// [memName] value for Set/Get operations
        /// </summary>
        public string Name
        {
            get { return memName; }
            set { memName = value; }
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
        /// [memProductCode] value for Set/Get operations
        /// </summary>
        public string ProductCode
        {
            get { return memProductCode; }
            set { memProductCode = value; }
        }

        /// <summary>
        /// [memCaseBarcode] value for Set/Get operations
        /// </summary>
        public string CaseBarcode
        {
            get { return memCaseBarcode; }
            set { memCaseBarcode = value; }
        }

        /// <summary>
        /// [memBoxBarcode] value for Set/Get operations
        /// </summary>
        public string BoxBarcode
        {
            get { return memBoxBarcode; }
            set { memBoxBarcode = value; }
        }

        /// <summary>
        /// [memCaseEach] value for Set/Get operations
        /// </summary>
        public int? CaseEach
        {
            get { return memCaseEach; }
            set { memCaseEach = value; }
        }

        /// <summary>
        /// [memCreatedBy] value for Set/Get operations
        /// </summary>
        public string CreatedBy
        {
            get { return memCreatedBy; }
            set { memCreatedBy = value; }
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
        /// [memUpdatedBy] value for Set/Get operations
        /// </summary>
        public string UpdatedBy
        {
            get { return memUpdatedBy; }
            set { memUpdatedBy = value; }
        }

        /// <summary>
        /// [memUpdatedOn] value for Set/Get operations
        /// </summary>
        public DateTime? UpdatedOn
        {
            get { return memUpdatedOn; }
            set { memUpdatedOn = value; }
        }

        #endregion
        #region CONSTRUCTORS
        /// <summary>
        /// entCustomerProduct class constructor
        /// </summary>
        /// <param name="parCustomerId">CustomerId is set/get by this property.</param>
        /// <param name="parCustomerBrandId">CustomerBrandId is set/get by this property.</param>
        /// <param name="parCustomerBrandCategoryId">CustomerBrandCategoryId is set/get by this property.</param>
        /// <param name="parProductBarcode">ProductBarcode is set/get by this property.</param>
        /// <param name="parSuggestedListPrice">SuggestedListPrice is set/get by this property.</param>
        /// <param name="parSuggestedActivityPrice">SuggestedActivityPrice is set/get by this property.</param>
        /// <param name="parPackagingSize">PackagingSize is set/get by this property.</param>
        /// <param name="parPackagingTypeId">PackagingTypeId is set/get by this property.</param>
        /// <param name="parName">Name is set/get by this property.</param>
        /// <param name="parProfilePicturePath">ProfilePicturePath is set/get by this property.</param>
        /// <param name="parProductCode">ProductCode is set/get by this property.</param>
        /// <param name="parCaseBarcode">CaseBarcode is set/get by this property.</param>
        /// <param name="parBoxBarcode">BoxBarcode is set/get by this property.</param>
        /// <param name="parCaseEach">CaseEach is set/get by this property.</param>
        /// <param name="parCreatedBy">CreatedBy is set/get by this property.</param>
        /// <param name="parCreatedOn">CreatedOn is set/get by this property.</param>
        /// <param name="parUpdatedBy">UpdatedBy is set/get by this property.</param>
        /// <param name="parUpdatedOn">UpdatedOn is set/get by this property.</param>
        public entCustomerProduct(int? parCustomerId, int? parCustomerBrandId, int? parCustomerBrandCategoryId, string parProductBarcode, decimal? parSuggestedListPrice, decimal? parSuggestedActivityPrice, decimal? parPackagingSize, int? parPackagingTypeId, string parName, string parProfilePicturePath, string parProductCode, string parCaseBarcode, string parBoxBarcode, int? parCaseEach, string parCreatedBy, DateTime? parCreatedOn, string parUpdatedBy, DateTime? parUpdatedOn)
        {
            this.memCustomerId = parCustomerId;
            this.memCustomerBrandId = parCustomerBrandId;
            this.memCustomerBrandCategoryId = parCustomerBrandCategoryId;
            this.memProductBarcode = parProductBarcode;
            this.memSuggestedListPrice = parSuggestedListPrice;
            this.memSuggestedActivityPrice = parSuggestedActivityPrice;
            this.memPackagingSize = parPackagingSize;
            this.memPackagingTypeId = parPackagingTypeId;
            this.memName = parName;
            this.memProfilePicturePath = parProfilePicturePath;
            this.memProductCode = parProductCode;
            this.memCaseBarcode = parCaseBarcode;
            this.memBoxBarcode = parBoxBarcode;
            this.memCaseEach = parCaseEach;
            this.memCreatedBy = parCreatedBy;
            this.memCreatedOn = parCreatedOn;
            this.memUpdatedBy = parUpdatedBy;
            this.memUpdatedOn = parUpdatedOn;
        }

        /// <summary>
        /// entCustomerProduct class constructor
        /// </summary>
        /// <param name="parId">Id is set/get by this property.</param>
        /// <param name="parCustomerId">CustomerId is set/get by this property.</param>
        /// <param name="parCustomerBrandId">CustomerBrandId is set/get by this property.</param>
        /// <param name="parCustomerBrandCategoryId">CustomerBrandCategoryId is set/get by this property.</param>
        /// <param name="parProductBarcode">ProductBarcode is set/get by this property.</param>
        /// <param name="parSuggestedListPrice">SuggestedListPrice is set/get by this property.</param>
        /// <param name="parSuggestedActivityPrice">SuggestedActivityPrice is set/get by this property.</param>
        /// <param name="parPackagingSize">PackagingSize is set/get by this property.</param>
        /// <param name="parPackagingTypeId">PackagingTypeId is set/get by this property.</param>
        /// <param name="parName">Name is set/get by this property.</param>
        /// <param name="parProfilePicturePath">ProfilePicturePath is set/get by this property.</param>
        /// <param name="parProductCode">ProductCode is set/get by this property.</param>
        /// <param name="parCaseBarcode">CaseBarcode is set/get by this property.</param>
        /// <param name="parBoxBarcode">BoxBarcode is set/get by this property.</param>
        /// <param name="parCaseEach">CaseEach is set/get by this property.</param>
        /// <param name="parCreatedBy">CreatedBy is set/get by this property.</param>
        /// <param name="parCreatedOn">CreatedOn is set/get by this property.</param>
        /// <param name="parUpdatedBy">UpdatedBy is set/get by this property.</param>
        /// <param name="parUpdatedOn">UpdatedOn is set/get by this property.</param>
        public entCustomerProduct(int parId, int? parCustomerId, int? parCustomerBrandId, int? parCustomerBrandCategoryId, string parProductBarcode, decimal? parSuggestedListPrice, decimal? parSuggestedActivityPrice, decimal? parPackagingSize, int? parPackagingTypeId, string parName, string parProfilePicturePath, string parProductCode, string parCaseBarcode, string parBoxBarcode, int? parCaseEach, string parCreatedBy, DateTime? parCreatedOn, string parUpdatedBy, DateTime? parUpdatedOn)
        {
            this.memId = parId;
            this.memCustomerId = parCustomerId;
            this.memCustomerBrandId = parCustomerBrandId;
            this.memCustomerBrandCategoryId = parCustomerBrandCategoryId;
            this.memProductBarcode = parProductBarcode;
            this.memSuggestedListPrice = parSuggestedListPrice;
            this.memSuggestedActivityPrice = parSuggestedActivityPrice;
            this.memPackagingSize = parPackagingSize;
            this.memPackagingTypeId = parPackagingTypeId;
            this.memName = parName;
            this.memProfilePicturePath = parProfilePicturePath;
            this.memProductCode = parProductCode;
            this.memCaseBarcode = parCaseBarcode;
            this.memBoxBarcode = parBoxBarcode;
            this.memCaseEach = parCaseEach;
            this.memCreatedBy = parCreatedBy;
            this.memCreatedOn = parCreatedOn;
            this.memUpdatedBy = parUpdatedBy;
            this.memUpdatedOn = parUpdatedOn;
        }

        /// <summary>
        /// entCustomerProduct class constructor
        /// </summary>
        public entCustomerProduct()
        {
        }

        #endregion
    }
}
