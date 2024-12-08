using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace entMerchPlus
{
    /// <summary>
    /// Reflection of a row of database table [MemberCustomer]
    /// </summary>
    [Serializable()]
    [JsonObject(MemberSerialization.OptOut)]
    public class entMemberCustomer
    {
        #region MEMBERS
        /// <summary>
        /// [Id] column that will hold value from database table [MemberCustomer]
        /// </summary>
        private int memId;

        /// <summary>
        /// [MemberId] column that will hold value from database table [MemberCustomer]
        /// </summary>
        private string memMemberId;

        /// <summary>
        /// [CustomerId] column that will hold value from database table [MemberCustomer]
        /// </summary>
        private int? memCustomerId;

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

        #endregion
        #region CONSTRUCTORS
        /// <summary>
        /// entMemberCustomer class constructor
        /// </summary>
        /// <param name="parMemberId">MemberId is set/get by this property.</param>
        /// <param name="parCustomerId">CustomerId is set/get by this property.</param>
        public entMemberCustomer(string parMemberId, int? parCustomerId)
        {
            this.memMemberId = parMemberId;
            this.memCustomerId = parCustomerId;
        }

        /// <summary>
        /// entMemberCustomer class constructor
        /// </summary>
        /// <param name="parId">Id is set/get by this property.</param>
        /// <param name="parMemberId">MemberId is set/get by this property.</param>
        /// <param name="parCustomerId">CustomerId is set/get by this property.</param>
        public entMemberCustomer(int parId, string parMemberId, int? parCustomerId)
        {
            this.memId = parId;
            this.memMemberId = parMemberId;
            this.memCustomerId = parCustomerId;
        }

        /// <summary>
        /// entMemberCustomer class constructor
        /// </summary>
        public entMemberCustomer()
        {
        }

        #endregion
    }
}