using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace entMerchPlus
{
    /// <summary>
    /// Reflection of a row of database table [SalesOrder]
    /// </summary>
    [Serializable()]
    [JsonObject(MemberSerialization.OptOut)]
    public class entSalesOrder
    {
        #region MEMBERS
        /// <summary>
        /// [Id] column that will hold value from database table [SalesOrder]
        /// </summary>
        private int memId;

        /// <summary>
        /// [CustomerId] column that will hold value from database table [SalesOrder]
        /// </summary>
        private int? memCustomerId;

        /// <summary>
        /// [MemberRouteId] column that will hold value from database table [SalesOrder]
        /// </summary>
        private int? memMemberRouteId;

        /// <summary>
        /// [SalesOrderStatusId] column that will hold value from database table [SalesOrder]
        /// </summary>
        private int? memSalesOrderStatusId;

        /// <summary>
        /// [CreatedBy] column that will hold value from database table [SalesOrder]
        /// </summary>
        private string memCreatedBy;

        /// <summary>
        /// [CreatedOn] column that will hold value from database table [SalesOrder]
        /// </summary>
        private DateTime? memCreatedOn;

        /// <summary>
        /// [ShippedOn] column that will hold value from database table [SalesOrder]
        /// </summary>
        private DateTime? memShippedOn;

        /// <summary>
        /// [ShippedBy] column that will hold value from database table [SalesOrder]
        /// </summary>
        private string memShippedBy;

        /// <summary>
        /// [WaybillNumber] column that will hold value from database table [SalesOrder]
        /// </summary>
        private string memWaybillNumber;

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
        /// [memMemberRouteId] value for Set/Get operations
        /// </summary>
        public int? MemberRouteId
        {
            get { return memMemberRouteId; }
            set { memMemberRouteId = value; }
        }

        /// <summary>
        /// [memSalesOrderStatusId] value for Set/Get operations
        /// </summary>
        public int? SalesOrderStatusId
        {
            get { return memSalesOrderStatusId; }
            set { memSalesOrderStatusId = value; }
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
        /// [memShippedOn] value for Set/Get operations
        /// </summary>
        public DateTime? ShippedOn
        {
            get { return memShippedOn; }
            set { memShippedOn = value; }
        }

        /// <summary>
        /// [memShippedBy] value for Set/Get operations
        /// </summary>
        public string ShippedBy
        {
            get { return memShippedBy; }
            set { memShippedBy = value; }
        }

        /// <summary>
        /// [memWaybillNumber] value for Set/Get operations
        /// </summary>
        public string WaybillNumber
        {
            get { return memWaybillNumber; }
            set { memWaybillNumber = value; }
        }

        #endregion
        #region CONSTRUCTORS
        /// <summary>
        /// entSalesOrder class constructor
        /// </summary>
        /// <param name="parCustomerId">CustomerId is set/get by this property.</param>
        /// <param name="parMemberRouteId">MemberRouteId is set/get by this property.</param>
        /// <param name="parSalesOrderStatusId">SalesOrderStatusId is set/get by this property.</param>
        /// <param name="parCreatedBy">CreatedBy is set/get by this property.</param>
        /// <param name="parCreatedOn">CreatedOn is set/get by this property.</param>
        /// <param name="parShippedOn">ShippedOn is set/get by this property.</param>
        /// <param name="parShippedBy">ShippedBy is set/get by this property.</param>
        /// <param name="parWaybillNumber">WaybillNumber is set/get by this property.</param>
        public entSalesOrder(int? parCustomerId, int? parMemberRouteId, int? parSalesOrderStatusId, string parCreatedBy, DateTime? parCreatedOn, DateTime? parShippedOn, string parShippedBy, string parWaybillNumber)
        {
            this.memCustomerId = parCustomerId;
            this.memMemberRouteId = parMemberRouteId;
            this.memSalesOrderStatusId = parSalesOrderStatusId;
            this.memCreatedBy = parCreatedBy;
            this.memCreatedOn = parCreatedOn;
            this.memShippedOn = parShippedOn;
            this.memShippedBy = parShippedBy;
            this.memWaybillNumber = parWaybillNumber;
        }

        /// <summary>
        /// entSalesOrder class constructor
        /// </summary>
        /// <param name="parId">Id is set/get by this property.</param>
        /// <param name="parCustomerId">CustomerId is set/get by this property.</param>
        /// <param name="parMemberRouteId">MemberRouteId is set/get by this property.</param>
        /// <param name="parSalesOrderStatusId">SalesOrderStatusId is set/get by this property.</param>
        /// <param name="parCreatedBy">CreatedBy is set/get by this property.</param>
        /// <param name="parCreatedOn">CreatedOn is set/get by this property.</param>
        /// <param name="parShippedOn">ShippedOn is set/get by this property.</param>
        /// <param name="parShippedBy">ShippedBy is set/get by this property.</param>
        /// <param name="parWaybillNumber">WaybillNumber is set/get by this property.</param>
        public entSalesOrder(int parId, int? parCustomerId, int? parMemberRouteId, int? parSalesOrderStatusId, string parCreatedBy, DateTime? parCreatedOn, DateTime? parShippedOn, string parShippedBy, string parWaybillNumber)
        {
            this.memId = parId;
            this.memCustomerId = parCustomerId;
            this.memMemberRouteId = parMemberRouteId;
            this.memSalesOrderStatusId = parSalesOrderStatusId;
            this.memCreatedBy = parCreatedBy;
            this.memCreatedOn = parCreatedOn;
            this.memShippedOn = parShippedOn;
            this.memShippedBy = parShippedBy;
            this.memWaybillNumber = parWaybillNumber;
        }

        /// <summary>
        /// entSalesOrder class constructor
        /// </summary>
        public entSalesOrder()
        {
        }

        #endregion
    }
}