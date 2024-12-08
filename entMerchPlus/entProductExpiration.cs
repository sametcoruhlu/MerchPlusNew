using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace entMerchPlus
{
    /// <summary>
    /// Reflection of a row of database table [ProductExpiration]
    /// </summary>
    [Serializable()]
    [JsonObject(MemberSerialization.OptOut)]
    public class entProductExpiration
    {
        #region MEMBERS
        /// <summary>
        /// [Id] column that will hold value from database table [ProductExpiration]
        /// </summary>
        private int memId;

        /// <summary>
        /// [MemberId] column that will hold value from database table [ProductExpiration]
        /// </summary>
        private string memMemberId;

        /// <summary>
        /// [CustomerId] column that will hold value from database table [ProductExpiration]
        /// </summary>
        private int? memCustomerId;

        /// <summary>
        /// [MemberRouteId] column that will hold value from database table [ProductExpiration]
        /// </summary>
        private int? memMemberRouteId;

        /// <summary>
        /// [CustomerProductId] column that will hold value from database table [ProductExpiration]
        /// </summary>
        private int? memCustomerProductId;

        /// <summary>
        /// [Quantity] column that will hold value from database table [ProductExpiration]
        /// </summary>
        private int? memQuantity;

        /// <summary>
        /// [ExpirationDate] column that will hold value from database table [ProductExpiration]
        /// </summary>
        private DateTime? memExpirationDate;

        /// <summary>
        /// [CreatedOn] column that will hold value from database table [ProductExpiration]
        /// </summary>
        private DateTime? memCreatedOn;

        /// <summary>
        /// [IsSentToServer] column that will hold value from database table [ProductExpiration]
        /// </summary>
        private bool? memIsSentToServer;

        /// <summary>
        /// [SentToServerOn] column that will hold value from database table [ProductExpiration]
        /// </summary>
        private DateTime? memSentToServerOn;

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
        /// [memMemberId] value for Set/Get operations
        /// </summary>
        public string MemberId
        {
            get { return memMemberId; }
            set { memMemberId = value; }
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
        /// [memExpirationDate] value for Set/Get operations
        /// </summary>
        public DateTime? ExpirationDate
        {
            get { return memExpirationDate; }
            set { memExpirationDate = value; }
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

        #endregion
        #region CONSTRUCTORS
        /// <summary>
        /// entProductExpiration class constructor
        /// </summary>
        /// <param name="parMemberId">MemberId is set/get by this property.</param>
        /// <param name="parCustomerId">CustomerId is set/get by this property.</param>
        /// <param name="parMemberRouteId">MemberRouteId is set/get by this property.</param>
        /// <param name="parCustomerProductId">CustomerProductId is set/get by this property.</param>
        /// <param name="parQuantity">Quantity is set/get by this property.</param>
        /// <param name="parExpirationDate">ExpirationDate is set/get by this property.</param>
        /// <param name="parCreatedOn">CreatedOn is set/get by this property.</param>
        /// <param name="parIsSentToServer">IsSentToServer is set/get by this property.</param>
        /// <param name="parSentToServerOn">SentToServerOn is set/get by this property.</param>
        public entProductExpiration(string parMemberId, int? parCustomerId, int? parMemberRouteId, int? parCustomerProductId, int? parQuantity, DateTime? parExpirationDate, DateTime? parCreatedOn, bool? parIsSentToServer, DateTime? parSentToServerOn)
        {
            this.memMemberId = parMemberId;
            this.memCustomerId = parCustomerId;
            this.memMemberRouteId = parMemberRouteId;
            this.memCustomerProductId = parCustomerProductId;
            this.memQuantity = parQuantity;
            this.memExpirationDate = parExpirationDate;
            this.memCreatedOn = parCreatedOn;
            this.memIsSentToServer = parIsSentToServer;
            this.memSentToServerOn = parSentToServerOn;
        }

        /// <summary>
        /// entProductExpiration class constructor
        /// </summary>
        /// <param name="parId">Id is set/get by this property.</param>
        /// <param name="parMemberId">MemberId is set/get by this property.</param>
        /// <param name="parCustomerId">CustomerId is set/get by this property.</param>
        /// <param name="parMemberRouteId">MemberRouteId is set/get by this property.</param>
        /// <param name="parCustomerProductId">CustomerProductId is set/get by this property.</param>
        /// <param name="parQuantity">Quantity is set/get by this property.</param>
        /// <param name="parExpirationDate">ExpirationDate is set/get by this property.</param>
        /// <param name="parCreatedOn">CreatedOn is set/get by this property.</param>
        /// <param name="parIsSentToServer">IsSentToServer is set/get by this property.</param>
        /// <param name="parSentToServerOn">SentToServerOn is set/get by this property.</param>
        public entProductExpiration(int parId, string parMemberId, int? parCustomerId, int? parMemberRouteId, int? parCustomerProductId, int? parQuantity, DateTime? parExpirationDate, DateTime? parCreatedOn, bool? parIsSentToServer, DateTime? parSentToServerOn)
        {
            this.memId = parId;
            this.memMemberId = parMemberId;
            this.memCustomerId = parCustomerId;
            this.memMemberRouteId = parMemberRouteId;
            this.memCustomerProductId = parCustomerProductId;
            this.memQuantity = parQuantity;
            this.memExpirationDate = parExpirationDate;
            this.memCreatedOn = parCreatedOn;
            this.memIsSentToServer = parIsSentToServer;
            this.memSentToServerOn = parSentToServerOn;
        }

        /// <summary>
        /// entProductExpiration class constructor
        /// </summary>
        public entProductExpiration()
        {
        }

        #endregion
    }
}
