using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace entMerchPlus
{
    /// <summary>
    /// Reflection of a row of database table [SalesOrderDetail]
    /// </summary>
    [Serializable()]
    [JsonObject(MemberSerialization.OptOut)]
    public class entSalesOrderDetail
    {
        #region MEMBERS
        /// <summary>
        /// [Id] column that will hold value from database table [SalesOrderDetail]
        /// </summary>
        private int memId;

        /// <summary>
        /// [SalesOrderId] column that will hold value from database table [SalesOrderDetail]
        /// </summary>
        private int? memSalesOrderId;

        /// <summary>
        /// [CustomerProductId] column that will hold value from database table [SalesOrderDetail]
        /// </summary>
        private int? memCustomerProductId;

        /// <summary>
        /// [Quantity] column that will hold value from database table [SalesOrderDetail]
        /// </summary>
        private int? memQuantity;

        /// <summary>
        /// [SalesOrderPackageTypeId] column that will hold value from database table [SalesOrderDetail]
        /// </summary>
        private int? memSalesOrderPackageTypeId;

        /// <summary>
        /// [Description] column that will hold value from database table [SalesOrderDetail]
        /// </summary>
        private string memDescription;

        /// <summary>
        /// [CreatedOn] column that will hold value from database table [SalesOrderDetail]
        /// </summary>
        private DateTime? memCreatedOn;

        /// <summary>
        /// [CreatedBy] column that will hold value from database table [SalesOrderDetail]
        /// </summary>
        private string memCreatedBy;

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
        /// [memSalesOrderId] value for Set/Get operations
        /// </summary>
        public int? SalesOrderId
        {
            get { return memSalesOrderId; }
            set { memSalesOrderId = value; }
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
        /// [memQuantity] value for Set/Get operations
        /// </summary>
        public int? Quantity
        {
            get { return memQuantity; }
            set { memQuantity = value; }
        }

        /// <summary>
        /// [memSalesOrderPackageTypeId] value for Set/Get operations
        /// </summary>
        public int? SalesOrderPackageTypeId
        {
            get { return memSalesOrderPackageTypeId; }
            set { memSalesOrderPackageTypeId = value; }
        }

        /// <summary>
        /// [memDescription] value for Set/Get operations
        /// </summary>
        public string Description
        {
            get { return memDescription; }
            set { memDescription = value; }
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
        /// [memCreatedBy] value for Set/Get operations
        /// </summary>
        public string CreatedBy
        {
            get { return memCreatedBy; }
            set { memCreatedBy = value; }
        }

        #endregion
        #region CONSTRUCTORS
        /// <summary>
        /// entSalesOrderDetail class constructor
        /// </summary>
        /// <param name="parSalesOrderId">SalesOrderId is set/get by this property.</param>
        /// <param name="parCustomerProductId">CustomerProductId is set/get by this property.</param>
        /// <param name="parQuantity">Quantity is set/get by this property.</param>
        /// <param name="parSalesOrderPackageTypeId">SalesOrderPackageTypeId is set/get by this property.</param>
        /// <param name="parDescription">Description is set/get by this property.</param>
        /// <param name="parCreatedOn">CreatedOn is set/get by this property.</param>
        /// <param name="parCreatedBy">CreatedBy is set/get by this property.</param>
        public entSalesOrderDetail(int? parSalesOrderId, int? parCustomerProductId, int? parQuantity, int? parSalesOrderPackageTypeId, string parDescription, DateTime? parCreatedOn, string parCreatedBy)
        {
            this.memSalesOrderId = parSalesOrderId;
            this.memCustomerProductId = parCustomerProductId;
            this.memQuantity = parQuantity;
            this.memSalesOrderPackageTypeId = parSalesOrderPackageTypeId;
            this.memDescription = parDescription;
            this.memCreatedOn = parCreatedOn;
            this.memCreatedBy = parCreatedBy;
        }

        /// <summary>
        /// entSalesOrderDetail class constructor
        /// </summary>
        /// <param name="parId">Id is set/get by this property.</param>
        /// <param name="parSalesOrderId">SalesOrderId is set/get by this property.</param>
        /// <param name="parCustomerProductId">CustomerProductId is set/get by this property.</param>
        /// <param name="parQuantity">Quantity is set/get by this property.</param>
        /// <param name="parSalesOrderPackageTypeId">SalesOrderPackageTypeId is set/get by this property.</param>
        /// <param name="parDescription">Description is set/get by this property.</param>
        /// <param name="parCreatedOn">CreatedOn is set/get by this property.</param>
        /// <param name="parCreatedBy">CreatedBy is set/get by this property.</param>
        public entSalesOrderDetail(int parId, int? parSalesOrderId, int? parCustomerProductId, int? parQuantity, int? parSalesOrderPackageTypeId, string parDescription, DateTime? parCreatedOn, string parCreatedBy)
        {
            this.memId = parId;
            this.memSalesOrderId = parSalesOrderId;
            this.memCustomerProductId = parCustomerProductId;
            this.memQuantity = parQuantity;
            this.memSalesOrderPackageTypeId = parSalesOrderPackageTypeId;
            this.memDescription = parDescription;
            this.memCreatedOn = parCreatedOn;
            this.memCreatedBy = parCreatedBy;
        }

        /// <summary>
        /// entSalesOrderDetail class constructor
        /// </summary>
        public entSalesOrderDetail()
        {
        }

        #endregion
    }
}