using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace entMerchPlus
{
    /// <summary>
    /// Reflection of a row of database table [MemberDistrict]
    /// </summary>
    [Serializable()]
    [JsonObject(MemberSerialization.OptOut)]
    public class entMemberDistrict
    {
        #region MEMBERS
        /// <summary>
        /// [Id] column that will hold value from database table [MemberDistrict]
        /// </summary>
        private int memId;

        /// <summary>
        /// [MemberId] column that will hold value from database table [MemberDistrict]
        /// </summary>
        private string memMemberId;

        /// <summary>
        /// [DistrictId] column that will hold value from database table [MemberDistrict]
        /// </summary>
        private int? memDistrictId;

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
        /// [memDistrictId] value for Set/Get operations
        /// </summary>
        public int? DistrictId
        {
            get { return memDistrictId; }
            set { memDistrictId = value; }
        }

        #endregion
        #region CONSTRUCTORS
        /// <summary>
        /// entMemberDistrict class constructor
        /// </summary>
        /// <param name="parMemberId">MemberId is set/get by this property.</param>
        /// <param name="parDistrictId">DistrictId is set/get by this property.</param>
        public entMemberDistrict(string parMemberId, int? parDistrictId)
        {
            this.memMemberId = parMemberId;
            this.memDistrictId = parDistrictId;
        }

        /// <summary>
        /// entMemberDistrict class constructor
        /// </summary>
        /// <param name="parId">Id is set/get by this property.</param>
        /// <param name="parMemberId">MemberId is set/get by this property.</param>
        /// <param name="parDistrictId">DistrictId is set/get by this property.</param>
        public entMemberDistrict(int parId, string parMemberId, int? parDistrictId)
        {
            this.memId = parId;
            this.memMemberId = parMemberId;
            this.memDistrictId = parDistrictId;
        }

        /// <summary>
        /// entMemberDistrict class constructor
        /// </summary>
        public entMemberDistrict()
        {
        }

        #endregion
    }
}
