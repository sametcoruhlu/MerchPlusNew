using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace entMerchPlus
{
    /// <summary>
    /// Reflection of a row of database table [MemberTimeOff]
    /// </summary>
    [Serializable()]
    [JsonObject(MemberSerialization.OptOut)]
    public class entMemberTimeOff
    {
        #region MEMBERS
        /// <summary>
        /// [Id] column that will hold value from database table [MemberTimeOff]
        /// </summary>
        private int memId;

        /// <summary>
        /// [MemberId] column that will hold value from database table [MemberTimeOff]
        /// </summary>
        private string memMemberId;

        /// <summary>
        /// [StartDate] column that will hold value from database table [MemberTimeOff]
        /// </summary>
        private DateTime? memStartDate;

        /// <summary>
        /// [EndDate] column that will hold value from database table [MemberTimeOff]
        /// </summary>
        private DateTime? memEndDate;

        /// <summary>
        /// [IsOffRoute] column that will hold value from database table [MemberTimeOff]
        /// </summary>
        private bool? memIsOffRoute;

        /// <summary>
        /// [Description] column that will hold value from database table [MemberTimeOff]
        /// </summary>
        private string memDescription;

        /// <summary>
        /// [CreatedBy] column that will hold value from database table [MemberTimeOff]
        /// </summary>
        private string memCreatedBy;

        /// <summary>
        /// [CreatedOn] column that will hold value from database table [MemberTimeOff]
        /// </summary>
        private DateTime? memCreatedOn;

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
        /// [memStartDate] value for Set/Get operations
        /// </summary>
        public DateTime? StartDate
        {
            get { return memStartDate; }
            set { memStartDate = value; }
        }

        /// <summary>
        /// [memEndDate] value for Set/Get operations
        /// </summary>
        public DateTime? EndDate
        {
            get { return memEndDate; }
            set { memEndDate = value; }
        }

        /// <summary>
        /// [memIsOffRoute] value for Set/Get operations
        /// </summary>
        public bool? IsOffRoute
        {
            get { return memIsOffRoute; }
            set { memIsOffRoute = value; }
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

        #endregion
        #region CONSTRUCTORS
        /// <summary>
        /// entMemberTimeOff class constructor
        /// </summary>
        /// <param name="parMemberId">MemberId is set/get by this property.</param>
        /// <param name="parStartDate">StartDate is set/get by this property.</param>
        /// <param name="parEndDate">EndDate is set/get by this property.</param>
        /// <param name="parIsOffRoute">IsOffRoute is set/get by this property.</param>
        /// <param name="parDescription">Description is set/get by this property.</param>
        /// <param name="parCreatedBy">CreatedBy is set/get by this property.</param>
        /// <param name="parCreatedOn">CreatedOn is set/get by this property.</param>
        public entMemberTimeOff(string parMemberId, DateTime? parStartDate, DateTime? parEndDate, bool? parIsOffRoute, string parDescription, string parCreatedBy, DateTime? parCreatedOn)
        {
            this.memMemberId = parMemberId;
            this.memStartDate = parStartDate;
            this.memEndDate = parEndDate;
            this.memIsOffRoute = parIsOffRoute;
            this.memDescription = parDescription;
            this.memCreatedBy = parCreatedBy;
            this.memCreatedOn = parCreatedOn;
        }

        /// <summary>
        /// entMemberTimeOff class constructor
        /// </summary>
        /// <param name="parId">Id is set/get by this property.</param>
        /// <param name="parMemberId">MemberId is set/get by this property.</param>
        /// <param name="parStartDate">StartDate is set/get by this property.</param>
        /// <param name="parEndDate">EndDate is set/get by this property.</param>
        /// <param name="parIsOffRoute">IsOffRoute is set/get by this property.</param>
        /// <param name="parDescription">Description is set/get by this property.</param>
        /// <param name="parCreatedBy">CreatedBy is set/get by this property.</param>
        /// <param name="parCreatedOn">CreatedOn is set/get by this property.</param>
        public entMemberTimeOff(int parId, string parMemberId, DateTime? parStartDate, DateTime? parEndDate, bool? parIsOffRoute, string parDescription, string parCreatedBy, DateTime? parCreatedOn)
        {
            this.memId = parId;
            this.memMemberId = parMemberId;
            this.memStartDate = parStartDate;
            this.memEndDate = parEndDate;
            this.memIsOffRoute = parIsOffRoute;
            this.memDescription = parDescription;
            this.memCreatedBy = parCreatedBy;
            this.memCreatedOn = parCreatedOn;
        }

        /// <summary>
        /// entMemberTimeOff class constructor
        /// </summary>
        public entMemberTimeOff()
        {
        }

        #endregion
    }
}
