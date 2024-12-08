using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace entMerchPlus
{
    /// <summary>
    /// Reflection of a row of database table [CustomerBrandPool]
    /// </summary>
    [Serializable()]
    [JsonObject(MemberSerialization.OptOut)]
    public class entCustomerBrandPool
    {
        #region MEMBERS
        /// <summary>
        /// [Id] column that will hold value from database table [CustomerBrandPool]
        /// </summary>
        private int memId;

        /// <summary>
        /// [CustomerId] column that will hold value from database table [CustomerBrandPool]
        /// </summary>
        private int? memCustomerId;

        /// <summary>
        /// [CustomerBrandId] column that will hold value from database table [CustomerBrandPool]
        /// </summary>
        private int? memCustomerBrandId;

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

        #endregion
        #region CONSTRUCTORS
        /// <summary>
        /// entCustomerBrandPool class constructor
        /// </summary>
        /// <param name="parCustomerId">CustomerId is set/get by this property.</param>
        /// <param name="parCustomerBrandId">CustomerBrandId is set/get by this property.</param>
        public entCustomerBrandPool(int? parCustomerId, int? parCustomerBrandId)
        {
            this.memCustomerId = parCustomerId;
            this.memCustomerBrandId = parCustomerBrandId;
        }

        /// <summary>
        /// entCustomerBrandPool class constructor
        /// </summary>
        /// <param name="parId">Id is set/get by this property.</param>
        /// <param name="parCustomerId">CustomerId is set/get by this property.</param>
        /// <param name="parCustomerBrandId">CustomerBrandId is set/get by this property.</param>
        public entCustomerBrandPool(int parId, int? parCustomerId, int? parCustomerBrandId)
        {
            this.memId = parId;
            this.memCustomerId = parCustomerId;
            this.memCustomerBrandId = parCustomerBrandId;
        }

        /// <summary>
        /// entCustomerBrandPool class constructor
        /// </summary>
        public entCustomerBrandPool()
        {
        }

        #endregion
    }
}