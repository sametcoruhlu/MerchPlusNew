using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace entMerchPlus
{
    /// <summary>
    /// Reflection of a row of database table [City]
    /// </summary>
    [Serializable()]
    [JsonObject(MemberSerialization.OptOut)]
    public class entCity
    {
        #region MEMBERS
        /// <summary>
        /// [Id] column that will hold value from database table [City]
        /// </summary>
        private int memId;

        /// <summary>
        /// [AreaId] column that will hold value from database table [City]
        /// </summary>
        private int? memAreaId;

        /// <summary>
        /// [Name] column that will hold value from database table [City]
        /// </summary>
        private string memName;

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
        /// [memAreaId] value for Set/Get operations
        /// </summary>
        public int? AreaId
        {
            get { return memAreaId; }
            set { memAreaId = value; }
        }

        /// <summary>
        /// [memName] value for Set/Get operations
        /// </summary>
        public string Name
        {
            get { return memName; }
            set { memName = value; }
        }

        #endregion
        #region CONSTRUCTORS
        /// <summary>
        /// entCity class constructor
        /// </summary>
        /// <param name="parAreaId">AreaId is set/get by this property.</param>
        /// <param name="parName">Name is set/get by this property.</param>
        public entCity(int? parAreaId, string parName)
        {
            this.memAreaId = parAreaId;
            this.memName = parName;
        }

        /// <summary>
        /// entCity class constructor
        /// </summary>
        /// <param name="parId">Id is set/get by this property.</param>
        /// <param name="parAreaId">AreaId is set/get by this property.</param>
        /// <param name="parName">Name is set/get by this property.</param>
        public entCity(int parId, int? parAreaId, string parName)
        {
            this.memId = parId;
            this.memAreaId = parAreaId;
            this.memName = parName;
        }

        /// <summary>
        /// entCity class constructor
        /// </summary>
        public entCity()
        {
        }

        #endregion
    }
}