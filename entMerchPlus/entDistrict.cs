using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace entMerchPlus
{
    /// <summary>
    /// Reflection of a row of database table [District]
    /// </summary>
    [Serializable()]
    [JsonObject(MemberSerialization.OptOut)]
    public class entDistrict
    {
        #region MEMBERS
        /// <summary>
        /// [Id] column that will hold value from database table [District]
        /// </summary>
        private int memId;

        /// <summary>
        /// [CityId] column that will hold value from database table [District]
        /// </summary>
        private int? memCityId;

        /// <summary>
        /// [Name] column that will hold value from database table [District]
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
        /// [memCityId] value for Set/Get operations
        /// </summary>
        public int? CityId
        {
            get { return memCityId; }
            set { memCityId = value; }
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
        /// entDistrict class constructor
        /// </summary>
        /// <param name="parCityId">CityId is set/get by this property.</param>
        /// <param name="parName">Name is set/get by this property.</param>
        public entDistrict(int? parCityId, string parName)
        {
            this.memCityId = parCityId;
            this.memName = parName;
        }

        /// <summary>
        /// entDistrict class constructor
        /// </summary>
        /// <param name="parId">Id is set/get by this property.</param>
        /// <param name="parCityId">CityId is set/get by this property.</param>
        /// <param name="parName">Name is set/get by this property.</param>
        public entDistrict(int parId, int? parCityId, string parName)
        {
            this.memId = parId;
            this.memCityId = parCityId;
            this.memName = parName;
        }

        /// <summary>
        /// entDistrict class constructor
        /// </summary>
        public entDistrict()
        {
        }

        #endregion
    }
}