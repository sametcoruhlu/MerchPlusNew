using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace entMerchPlus
{
    /// <summary>
    /// Reflection of a row of database table [MemberRoute]
    /// </summary>
    [Serializable()]
    [JsonObject(MemberSerialization.OptOut)]
    public class entMemberRoute
    {
        #region MEMBERS
        /// <summary>
        /// [Id] column that will hold value from database table [MemberRoute]
        /// </summary>
        private int memId;

        /// <summary>
        /// [MemberId] column that will hold value from database table [MemberRoute]
        /// </summary>
        private string memMemberId;

        /// <summary>
        /// [CustomerId] column that will hold value from database table [MemberRoute]
        /// </summary>
        private int? memCustomerId;

        /// <summary>
        /// [EffectiveDate] column that will hold value from database table [MemberRoute]
        /// </summary>
        private DateTime? memEffectiveDate;

        /// <summary>
        /// [RetailShopId] column that will hold value from database table [MemberRoute]
        /// </summary>
        private int? memRetailShopId;

        /// <summary>
        /// [EntryOn] column that will hold value from database table [MemberRoute]
        /// </summary>
        private DateTime? memEntryOn;

        /// <summary>
        /// [ExitOn] column that will hold value from database table [MemberRoute]
        /// </summary>
        private DateTime? memExitOn;

        /// <summary>
        /// [EntryCoordinateX] column that will hold value from database table [MemberRoute]
        /// </summary>
        private decimal? memEntryCoordinateX;

        /// <summary>
        /// [EntryCoordinateY] column that will hold value from database table [MemberRoute]
        /// </summary>
        private decimal? memEntryCoordinateY;

        /// <summary>
        /// [ExitCoordinateX] column that will hold value from database table [MemberRoute]
        /// </summary>
        private decimal? memExitCoordinateX;

        /// <summary>
        /// [ExitCoordinateY] column that will hold value from database table [MemberRoute]
        /// </summary>
        private decimal? memExitCoordinateY;

        /// <summary>
        /// [EntryImageData] column that will hold value from database table [MemberRoute]
        /// </summary>
        private string memEntryImageData;

        /// <summary>
        /// [TotalTimeSpent] column that will hold value from database table [MemberRoute]
        /// </summary>
        private int? memTotalTimeSpent;

        /// <summary>
        /// [IsSentToServer] column that will hold value from database table [MemberRoute]
        /// </summary>
        private bool? memIsSentToServer;

        /// <summary>
        /// [SentToServerOn] column that will hold value from database table [MemberRoute]
        /// </summary>
        private DateTime? memSentToServerOn;

        /// <summary>
        /// [OrderIndex] column that will hold value from database table [MemberRoute]
        /// </summary>
        private int? memOrderIndex;

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
        /// [memEffectiveDate] value for Set/Get operations
        /// </summary>
        public DateTime? EffectiveDate
        {
            get { return memEffectiveDate; }
            set { memEffectiveDate = value; }
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
        /// [memEntryOn] value for Set/Get operations
        /// </summary>
        public DateTime? EntryOn
        {
            get { return memEntryOn; }
            set { memEntryOn = value; }
        }

        /// <summary>
        /// [memExitOn] value for Set/Get operations
        /// </summary>
        public DateTime? ExitOn
        {
            get { return memExitOn; }
            set { memExitOn = value; }
        }

        /// <summary>
        /// [memEntryCoordinateX] value for Set/Get operations
        /// </summary>
        public decimal? EntryCoordinateX
        {
            get { return memEntryCoordinateX; }
            set { memEntryCoordinateX = value; }
        }

        /// <summary>
        /// [memEntryCoordinateY] value for Set/Get operations
        /// </summary>
        public decimal? EntryCoordinateY
        {
            get { return memEntryCoordinateY; }
            set { memEntryCoordinateY = value; }
        }

        /// <summary>
        /// [memExitCoordinateX] value for Set/Get operations
        /// </summary>
        public decimal? ExitCoordinateX
        {
            get { return memExitCoordinateX; }
            set { memExitCoordinateX = value; }
        }

        /// <summary>
        /// [memExitCoordinateY] value for Set/Get operations
        /// </summary>
        public decimal? ExitCoordinateY
        {
            get { return memExitCoordinateY; }
            set { memExitCoordinateY = value; }
        }

        /// <summary>
        /// [memEntryImageData] value for Set/Get operations
        /// </summary>
        public string EntryImageData
        {
            get { return memEntryImageData; }
            set { memEntryImageData = value; }
        }

        /// <summary>
        /// [memTotalTimeSpent] value for Set/Get operations
        /// </summary>
        public int? TotalTimeSpent
        {
            get { return memTotalTimeSpent; }
            set { memTotalTimeSpent = value; }
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

        /// <summary>
        /// [memOrderIndex] value for Set/Get operations
        /// </summary>
        public int? OrderIndex
        {
            get { return memOrderIndex; }
            set { memOrderIndex = value; }
        }

        #endregion
        #region CONSTRUCTORS
        /// <summary>
        /// entMemberRoute class constructor
        /// </summary>
        /// <param name="parMemberId">MemberId is set/get by this property.</param>
        /// <param name="parCustomerId">CustomerId is set/get by this property.</param>
        /// <param name="parEffectiveDate">EffectiveDate is set/get by this property.</param>
        /// <param name="parRetailShopId">RetailShopId is set/get by this property.</param>
        /// <param name="parEntryOn">EntryOn is set/get by this property.</param>
        /// <param name="parExitOn">ExitOn is set/get by this property.</param>
        /// <param name="parEntryCoordinateX">EntryCoordinateX is set/get by this property.</param>
        /// <param name="parEntryCoordinateY">EntryCoordinateY is set/get by this property.</param>
        /// <param name="parExitCoordinateX">ExitCoordinateX is set/get by this property.</param>
        /// <param name="parExitCoordinateY">ExitCoordinateY is set/get by this property.</param>
        /// <param name="parEntryImageData">EntryImageData is set/get by this property.</param>
        /// <param name="parTotalTimeSpent">TotalTimeSpent is set/get by this property.</param>
        /// <param name="parIsSentToServer">IsSentToServer is set/get by this property.</param>
        /// <param name="parSentToServerOn">SentToServerOn is set/get by this property.</param>
        /// <param name="parOrderIndex">OrderIndex is set/get by this property.</param>
        public entMemberRoute(string parMemberId, int? parCustomerId, DateTime? parEffectiveDate, int? parRetailShopId, DateTime? parEntryOn, DateTime? parExitOn, decimal? parEntryCoordinateX, decimal? parEntryCoordinateY, decimal? parExitCoordinateX, decimal? parExitCoordinateY, string parEntryImageData, int? parTotalTimeSpent, bool? parIsSentToServer, DateTime? parSentToServerOn, int? parOrderIndex)
        {
            this.memMemberId = parMemberId;
            this.memCustomerId = parCustomerId;
            this.memEffectiveDate = parEffectiveDate;
            this.memRetailShopId = parRetailShopId;
            this.memEntryOn = parEntryOn;
            this.memExitOn = parExitOn;
            this.memEntryCoordinateX = parEntryCoordinateX;
            this.memEntryCoordinateY = parEntryCoordinateY;
            this.memExitCoordinateX = parExitCoordinateX;
            this.memExitCoordinateY = parExitCoordinateY;
            this.memEntryImageData = parEntryImageData;
            this.memTotalTimeSpent = parTotalTimeSpent;
            this.memIsSentToServer = parIsSentToServer;
            this.memSentToServerOn = parSentToServerOn;
            this.memOrderIndex = parOrderIndex;
        }

        /// <summary>
        /// entMemberRoute class constructor
        /// </summary>
        /// <param name="parId">Id is set/get by this property.</param>
        /// <param name="parMemberId">MemberId is set/get by this property.</param>
        /// <param name="parCustomerId">CustomerId is set/get by this property.</param>
        /// <param name="parEffectiveDate">EffectiveDate is set/get by this property.</param>
        /// <param name="parRetailShopId">RetailShopId is set/get by this property.</param>
        /// <param name="parEntryOn">EntryOn is set/get by this property.</param>
        /// <param name="parExitOn">ExitOn is set/get by this property.</param>
        /// <param name="parEntryCoordinateX">EntryCoordinateX is set/get by this property.</param>
        /// <param name="parEntryCoordinateY">EntryCoordinateY is set/get by this property.</param>
        /// <param name="parExitCoordinateX">ExitCoordinateX is set/get by this property.</param>
        /// <param name="parExitCoordinateY">ExitCoordinateY is set/get by this property.</param>
        /// <param name="parEntryImageData">EntryImageData is set/get by this property.</param>
        /// <param name="parTotalTimeSpent">TotalTimeSpent is set/get by this property.</param>
        /// <param name="parIsSentToServer">IsSentToServer is set/get by this property.</param>
        /// <param name="parSentToServerOn">SentToServerOn is set/get by this property.</param>
        /// <param name="parOrderIndex">OrderIndex is set/get by this property.</param>
        public entMemberRoute(int parId, string parMemberId, int? parCustomerId, DateTime? parEffectiveDate, int? parRetailShopId, DateTime? parEntryOn, DateTime? parExitOn, decimal? parEntryCoordinateX, decimal? parEntryCoordinateY, decimal? parExitCoordinateX, decimal? parExitCoordinateY, string parEntryImageData, int? parTotalTimeSpent, bool? parIsSentToServer, DateTime? parSentToServerOn, int? parOrderIndex)
        {
            this.memId = parId;
            this.memMemberId = parMemberId;
            this.memCustomerId = parCustomerId;
            this.memEffectiveDate = parEffectiveDate;
            this.memRetailShopId = parRetailShopId;
            this.memEntryOn = parEntryOn;
            this.memExitOn = parExitOn;
            this.memEntryCoordinateX = parEntryCoordinateX;
            this.memEntryCoordinateY = parEntryCoordinateY;
            this.memExitCoordinateX = parExitCoordinateX;
            this.memExitCoordinateY = parExitCoordinateY;
            this.memEntryImageData = parEntryImageData;
            this.memTotalTimeSpent = parTotalTimeSpent;
            this.memIsSentToServer = parIsSentToServer;
            this.memSentToServerOn = parSentToServerOn;
            this.memOrderIndex = parOrderIndex;
        }

        /// <summary>
        /// entMemberRoute class constructor
        /// </summary>
        public entMemberRoute()
        {
        }

        #endregion
    }
}
