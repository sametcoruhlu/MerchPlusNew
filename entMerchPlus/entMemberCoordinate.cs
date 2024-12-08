using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace entMerchPlus
{
    /// <summary>
    /// Reflection of a row of database table [MemberCoordinate]
    /// </summary>
    [Serializable()]
    [JsonObject(MemberSerialization.OptOut)]
    public class entMemberCoordinate
    {
        #region MEMBERS
        /// <summary>
        /// [Id] column that will hold value from database table [MemberCoordinate]
        /// </summary>
        private int memId;

        /// <summary>
        /// [MemberId] column that will hold value from database table [MemberCoordinate]
        /// </summary>
        private string memMemberId;

        /// <summary>
        /// [CoordinateX] column that will hold value from database table [MemberCoordinate]
        /// </summary>
        private decimal? memCoordinateX;

        /// <summary>
        /// [CoordinateY] column that will hold value from database table [MemberCoordinate]
        /// </summary>
        private decimal? memCoordinateY;

        /// <summary>
        /// [CreatedOn] column that will hold value from database table [MemberCoordinate]
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
        /// [memCoordinateX] value for Set/Get operations
        /// </summary>
        public decimal? CoordinateX
        {
            get { return memCoordinateX; }
            set { memCoordinateX = value; }
        }

        /// <summary>
        /// [memCoordinateY] value for Set/Get operations
        /// </summary>
        public decimal? CoordinateY
        {
            get { return memCoordinateY; }
            set { memCoordinateY = value; }
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
        /// entMemberCoordinate class constructor
        /// </summary>
        /// <param name="parMemberId">MemberId is set/get by this property.</param>
        /// <param name="parCoordinateX">CoordinateX is set/get by this property.</param>
        /// <param name="parCoordinateY">CoordinateY is set/get by this property.</param>
        /// <param name="parCreatedOn">CreatedOn is set/get by this property.</param>
        public entMemberCoordinate(string parMemberId, decimal? parCoordinateX, decimal? parCoordinateY, DateTime? parCreatedOn)
        {
            this.memMemberId = parMemberId;
            this.memCoordinateX = parCoordinateX;
            this.memCoordinateY = parCoordinateY;
            this.memCreatedOn = parCreatedOn;
        }

        /// <summary>
        /// entMemberCoordinate class constructor
        /// </summary>
        /// <param name="parId">Id is set/get by this property.</param>
        /// <param name="parMemberId">MemberId is set/get by this property.</param>
        /// <param name="parCoordinateX">CoordinateX is set/get by this property.</param>
        /// <param name="parCoordinateY">CoordinateY is set/get by this property.</param>
        /// <param name="parCreatedOn">CreatedOn is set/get by this property.</param>
        public entMemberCoordinate(int parId, string parMemberId, decimal? parCoordinateX, decimal? parCoordinateY, DateTime? parCreatedOn)
        {
            this.memId = parId;
            this.memMemberId = parMemberId;
            this.memCoordinateX = parCoordinateX;
            this.memCoordinateY = parCoordinateY;
            this.memCreatedOn = parCreatedOn;
        }

        /// <summary>
        /// entMemberCoordinate class constructor
        /// </summary>
        public entMemberCoordinate()
        {
        }

        #endregion
    }
}
