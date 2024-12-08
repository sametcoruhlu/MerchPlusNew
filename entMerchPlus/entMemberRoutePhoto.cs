using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace entMerchPlus
{
    /// <summary>
    /// Reflection of a row of database table [MemberRoutePhoto]
    /// </summary>
    [Serializable()]
    [JsonObject(MemberSerialization.OptOut)]
    public class entMemberRoutePhoto
    {
        #region MEMBERS
        /// <summary>
        /// [Id] column that will hold value from database table [MemberRoutePhoto]
        /// </summary>
        private int memId;

        /// <summary>
        /// [MemberId] column that will hold value from database table [MemberRoutePhoto]
        /// </summary>
        private string memMemberId;

        /// <summary>
        /// [MemberRouteId] column that will hold value from database table [MemberRoutePhoto]
        /// </summary>
        private int? memMemberRouteId;

        /// <summary>
        /// [ProfilePicturePath] column that will hold value from database table [MemberRoutePhoto]
        /// </summary>
        private string memProfilePicturePath;

        /// <summary>
        /// [IsSentToServer] column that will hold value from database table [MemberRoutePhoto]
        /// </summary>
        private bool? memIsSentToServer;

        /// <summary>
        /// [CreatedOn] column that will hold value from database table [MemberRoutePhoto]
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
        /// [memMemberRouteId] value for Set/Get operations
        /// </summary>
        public int? MemberRouteId
        {
            get { return memMemberRouteId; }
            set { memMemberRouteId = value; }
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
        /// [memIsSentToServer] value for Set/Get operations
        /// </summary>
        public bool? IsSentToServer
        {
            get { return memIsSentToServer; }
            set { memIsSentToServer = value; }
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
        /// entMemberRoutePhoto class constructor
        /// </summary>
        /// <param name="parMemberId">MemberId is set/get by this property.</param>
        /// <param name="parMemberRouteId">MemberRouteId is set/get by this property.</param>
        /// <param name="parProfilePicturePath">ProfilePicturePath is set/get by this property.</param>
        /// <param name="parIsSentToServer">IsSentToServer is set/get by this property.</param>
        /// <param name="parCreatedOn">CreatedOn is set/get by this property.</param>
        public entMemberRoutePhoto(string parMemberId, int? parMemberRouteId, string parProfilePicturePath, bool? parIsSentToServer, DateTime? parCreatedOn)
        {
            this.memMemberId = parMemberId;
            this.memMemberRouteId = parMemberRouteId;
            this.memProfilePicturePath = parProfilePicturePath;
            this.memIsSentToServer = parIsSentToServer;
            this.memCreatedOn = parCreatedOn;
        }

        /// <summary>
        /// entMemberRoutePhoto class constructor
        /// </summary>
        /// <param name="parId">Id is set/get by this property.</param>
        /// <param name="parMemberId">MemberId is set/get by this property.</param>
        /// <param name="parMemberRouteId">MemberRouteId is set/get by this property.</param>
        /// <param name="parProfilePicturePath">ProfilePicturePath is set/get by this property.</param>
        /// <param name="parIsSentToServer">IsSentToServer is set/get by this property.</param>
        /// <param name="parCreatedOn">CreatedOn is set/get by this property.</param>
        public entMemberRoutePhoto(int parId, string parMemberId, int? parMemberRouteId, string parProfilePicturePath, bool? parIsSentToServer, DateTime? parCreatedOn)
        {
            this.memId = parId;
            this.memMemberId = parMemberId;
            this.memMemberRouteId = parMemberRouteId;
            this.memProfilePicturePath = parProfilePicturePath;
            this.memIsSentToServer = parIsSentToServer;
            this.memCreatedOn = parCreatedOn;
        }

        /// <summary>
        /// entMemberRoutePhoto class constructor
        /// </summary>
        public entMemberRoutePhoto()
        {
        }

        #endregion
    }
}
