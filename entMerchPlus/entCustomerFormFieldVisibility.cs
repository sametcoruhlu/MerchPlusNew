using Newtonsoft.Json;
using System;

namespace entMerchPlus
{
    /// <summary>
    /// Müşteri bazında MerchDataEntry form alanlarının görünürlüğünü kontrol eden entity
    /// Supervisor web sayfasından tanımlanır
    /// </summary>
    [Serializable()]
    [JsonObject(MemberSerialization.OptOut)]
    public class entCustomerFormFieldVisibility
    {
        #region MEMBERS
        /// <summary>
        /// [Id] column that will hold value from database table [CustomerFormFieldVisibility]
        /// </summary>
        private int memId;

        /// <summary>
        /// [CustomerId] column that will hold value from database table [CustomerFormFieldVisibility]
        /// </summary>
        private int memCustomerId;

        /// <summary>
        /// [FieldName] column that will hold value from database table [CustomerFormFieldVisibility]
        /// </summary>
        private string memFieldName;

        /// <summary>
        /// [IsVisible] column that will hold value from database table [CustomerFormFieldVisibility]
        /// </summary>
        private bool memIsVisible;

        /// <summary>
        /// [IsRequired] column that will hold value from database table [CustomerFormFieldVisibility]
        /// </summary>
        private bool memIsRequired;

        /// <summary>
        /// [DisplayOrder] column that will hold value from database table [CustomerFormFieldVisibility]
        /// </summary>
        private int memDisplayOrder;

        /// <summary>
        /// [CreatedBy] column that will hold value from database table [CustomerFormFieldVisibility]
        /// </summary>
        private string memCreatedBy;

        /// <summary>
        /// [CreatedOn] column that will hold value from database table [CustomerFormFieldVisibility]
        /// </summary>
        private DateTime memCreatedOn;

        /// <summary>
        /// [ModifiedBy] column that will hold value from database table [CustomerFormFieldVisibility]
        /// </summary>
        private string memModifiedBy;

        /// <summary>
        /// [ModifiedOn] column that will hold value from database table [CustomerFormFieldVisibility]
        /// </summary>
        private DateTime? memModifiedOn;

        /// <summary>
        /// [IsActive] column that will hold value from database table [CustomerFormFieldVisibility]
        /// </summary>
        private bool memIsActive;

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
        public int CustomerId
        {
            get { return memCustomerId; }
            set { memCustomerId = value; }
        }

        /// <summary>
        /// [memFieldName] value for Set/Get operations
        /// </summary>
        public string FieldName
        {
            get { return memFieldName; }
            set { memFieldName = value; }
        }

        /// <summary>
        /// [memIsVisible] value for Set/Get operations
        /// </summary>
        public bool IsVisible
        {
            get { return memIsVisible; }
            set { memIsVisible = value; }
        }

        /// <summary>
        /// [memIsRequired] value for Set/Get operations
        /// </summary>
        public bool IsRequired
        {
            get { return memIsRequired; }
            set { memIsRequired = value; }
        }

        /// <summary>
        /// [memDisplayOrder] value for Set/Get operations
        /// </summary>
        public int DisplayOrder
        {
            get { return memDisplayOrder; }
            set { memDisplayOrder = value; }
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
        public DateTime CreatedOn
        {
            get { return memCreatedOn; }
            set { memCreatedOn = value; }
        }

        /// <summary>
        /// [memModifiedBy] value for Set/Get operations
        /// </summary>
        public string ModifiedBy
        {
            get { return memModifiedBy; }
            set { memModifiedBy = value; }
        }

        /// <summary>
        /// [memModifiedOn] value for Set/Get operations
        /// </summary>
        public DateTime? ModifiedOn
        {
            get { return memModifiedOn; }
            set { memModifiedOn = value; }
        }

        /// <summary>
        /// [memIsActive] value for Set/Get operations
        /// </summary>
        public bool IsActive
        {
            get { return memIsActive; }
            set { memIsActive = value; }
        }

        #endregion

        #region CONSTRUCTORS
        /// <summary>
        /// entCustomerFormFieldVisibility class constructor
        /// </summary>
        public entCustomerFormFieldVisibility()
        {
            memCreatedOn = DateTime.UtcNow;
            memIsActive = true;
        }

        /// <summary>
        /// entCustomerFormFieldVisibility class constructor
        /// </summary>
        /// <param name="parCustomerId">CustomerId is set/get by this property.</param>
        /// <param name="parFieldName">FieldName is set/get by this property.</param>
        /// <param name="parIsVisible">IsVisible is set/get by this property.</param>
        /// <param name="parIsRequired">IsRequired is set/get by this property.</param>
        /// <param name="parDisplayOrder">DisplayOrder is set/get by this property.</param>
        /// <param name="parCreatedBy">CreatedBy is set/get by this property.</param>
        public entCustomerFormFieldVisibility(int parCustomerId, string parFieldName, bool parIsVisible, bool parIsRequired, int parDisplayOrder, string parCreatedBy)
        {
            this.memCustomerId = parCustomerId;
            this.memFieldName = parFieldName;
            this.memIsVisible = parIsVisible;
            this.memIsRequired = parIsRequired;
            this.memDisplayOrder = parDisplayOrder;
            this.memCreatedBy = parCreatedBy;
            this.memCreatedOn = DateTime.UtcNow;
            this.memIsActive = true;
        }

        /// <summary>
        /// entCustomerFormFieldVisibility class constructor
        /// </summary>
        /// <param name="parId">Id is set/get by this property.</param>
        /// <param name="parCustomerId">CustomerId is set/get by this property.</param>
        /// <param name="parFieldName">FieldName is set/get by this property.</param>
        /// <param name="parIsVisible">IsVisible is set/get by this property.</param>
        /// <param name="parIsRequired">IsRequired is set/get by this property.</param>
        /// <param name="parDisplayOrder">DisplayOrder is set/get by this property.</param>
        /// <param name="parCreatedBy">CreatedBy is set/get by this property.</param>
        /// <param name="parCreatedOn">CreatedOn is set/get by this property.</param>
        /// <param name="parModifiedBy">ModifiedBy is set/get by this property.</param>
        /// <param name="parModifiedOn">ModifiedOn is set/get by this property.</param>
        /// <param name="parIsActive">IsActive is set/get by this property.</param>
        public entCustomerFormFieldVisibility(int parId, int parCustomerId, string parFieldName, bool parIsVisible, bool parIsRequired, int parDisplayOrder, string parCreatedBy, DateTime parCreatedOn, string parModifiedBy, DateTime? parModifiedOn, bool parIsActive)
        {
            this.memId = parId;
            this.memCustomerId = parCustomerId;
            this.memFieldName = parFieldName;
            this.memIsVisible = parIsVisible;
            this.memIsRequired = parIsRequired;
            this.memDisplayOrder = parDisplayOrder;
            this.memCreatedBy = parCreatedBy;
            this.memCreatedOn = parCreatedOn;
            this.memModifiedBy = parModifiedBy;
            this.memModifiedOn = parModifiedOn;
            this.memIsActive = true;
        }

        #endregion

        #region CONSTANTS
        /// <summary>
        /// Form alan adları için sabitler
        /// </summary>
        public static class FieldNames
        {
            public const string CustomerName = "CustomerName";
            public const string CustomerAddress = "CustomerAddress";
            public const string CustomerPhone = "CustomerPhone";
            public const string CustomerEmail = "CustomerEmail";
            public const string ContactPerson = "ContactPerson";
            public const string TaxNumber = "TaxNumber";
            public const string TaxOffice = "TaxOffice";
            public const string PaymentTerms = "PaymentTerms";
            public const string CreditLimit = "CreditLimit";
            public const string Notes = "Notes";
            public const string FaceCount = "FaceCount";
            public const string ShelfIndex = "ShelfIndex";
            public const string ShelfStock = "ShelfStock";
            public const string Price = "Price";
            public const string WarehouseStock = "WarehouseStock";
            public const string ActivityType = "ActivityType";
            public const string DiscountPrice = "DiscountPrice";
        }
        #endregion
    }
}
