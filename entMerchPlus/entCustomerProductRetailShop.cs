using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace entMerchPlus
{
    /// <summary>
    /// Reflection of a row of database table [CustomerProductRetailShop]
    /// </summary>
    [Serializable()]
    [JsonObject(MemberSerialization.OptOut)]
    public class entCustomerProductRetailShop
    {
        #region MEMBERS
        /// <summary>
        /// [Id] column that will hold value from database table [CustomerProductRetailShop]
        /// </summary>
        private int memId;

        /// <summary>
        /// [CustomerId] column that will hold value from database table [CustomerProductRetailShop]
        /// </summary>
        private int? memCustomerId;

        /// <summary>
        /// [CustomerProductId] column that will hold value from database table [CustomerProductRetailShop]
        /// </summary>
        private int? memCustomerProductId;

        /// <summary>
        /// [RetailShopId] column that will hold value from database table [CustomerProductRetailShop]
        /// </summary>
        private int? memRetailShopId;

        /// <summary>
        /// [CreatedOn] column that will hold value from database table [CustomerProductRetailShop]
        /// </summary>
        private DateTime? memCreatedOn;

        /// <summary>
        /// [CreatedBy] column that will hold value from database table [CustomerProductRetailShop]
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
        /// [memCustomerId] value for Set/Get operations
        /// </summary>
        public int? CustomerId
        {
            get { return memCustomerId; }
            set { memCustomerId = value; }
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
        /// [memRetailShopId] value for Set/Get operations
        /// </summary>
        public int? RetailShopId
        {
            get { return memRetailShopId; }
            set { memRetailShopId = value; }
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
        /// entCustomerProductRetailShop class constructor
        /// </summary>
        /// <param name="parCustomerId">CustomerId is set/get by this property.</param>
        /// <param name="parCustomerProductId">CustomerProductId is set/get by this property.</param>
        /// <param name="parRetailShopId">RetailShopId is set/get by this property.</param>
        /// <param name="parCreatedOn">CreatedOn is set/get by this property.</param>
        /// <param name="parCreatedBy">CreatedBy is set/get by this property.</param>
        public entCustomerProductRetailShop(int? parCustomerId, int? parCustomerProductId, int? parRetailShopId, DateTime? parCreatedOn, string parCreatedBy)
        {
            this.memCustomerId = parCustomerId;
            this.memCustomerProductId = parCustomerProductId;
            this.memRetailShopId = parRetailShopId;
            this.memCreatedOn = parCreatedOn;
            this.memCreatedBy = parCreatedBy;
        }

        /// <summary>
        /// entCustomerProductRetailShop class constructor
        /// </summary>
        /// <param name="parId">Id is set/get by this property.</param>
        /// <param name="parCustomerId">CustomerId is set/get by this property.</param>
        /// <param name="parCustomerProductId">CustomerProductId is set/get by this property.</param>
        /// <param name="parRetailShopId">RetailShopId is set/get by this property.</param>
        /// <param name="parCreatedOn">CreatedOn is set/get by this property.</param>
        /// <param name="parCreatedBy">CreatedBy is set/get by this property.</param>
        public entCustomerProductRetailShop(int parId, int? parCustomerId, int? parCustomerProductId, int? parRetailShopId, DateTime? parCreatedOn, string parCreatedBy)
        {
            this.memId = parId;
            this.memCustomerId = parCustomerId;
            this.memCustomerProductId = parCustomerProductId;
            this.memRetailShopId = parRetailShopId;
            this.memCreatedOn = parCreatedOn;
            this.memCreatedBy = parCreatedBy;
        }

        /// <summary>
        /// entCustomerProductRetailShop class constructor
        /// </summary>
        public entCustomerProductRetailShop()
        {
        }

        #endregion
    }
}
