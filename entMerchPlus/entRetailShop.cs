using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace entMerchPlus
{
    /// <summary>
    /// Reflection of a row of database table [RetailShop]
    /// </summary>
    [Serializable()]
    [JsonObject(MemberSerialization.OptOut)]
    public class entRetailShop
    {
        #region MEMBERS
        /// <summary>
        /// [Id] column that will hold value from database table [RetailShop]
        /// </summary>
        private int memId;

        /// <summary>
        /// [RetailId] column that will hold value from database table [RetailShop]
        /// </summary>
        private int? memRetailId;

        /// <summary>
        /// [RetailShopCategoryId] column that will hold value from database table [RetailShop]
        /// </summary>
        private int? memRetailShopCategoryId;

        /// <summary>
        /// [Name] column that will hold value from database table [RetailShop]
        /// </summary>
        private string memName;

        /// <summary>
        /// [Address] column that will hold value from database table [RetailShop]
        /// </summary>
        private string memAddress;

        /// <summary>
        /// [CityId] column that will hold value from database table [RetailShop]
        /// </summary>
        private int? memCityId;

        /// <summary>
        /// [DistrictId] column that will hold value from database table [RetailShop]
        /// </summary>
        private int? memDistrictId;

        /// <summary>
        /// [RetailDatabaseId] column that will hold value from database table [RetailShop]
        /// </summary>
        private string memRetailDatabaseId;

        /// <summary>
        /// [CoordinateX] column that will hold value from database table [RetailShop]
        /// </summary>
        private decimal? memCoordinateX;

        /// <summary>
        /// [CoordinateY] column that will hold value from database table [RetailShop]
        /// </summary>
        private decimal? memCoordinateY;

        /// <summary>
        /// [IsActive] column that will hold value from database table [RetailShop]
        /// </summary>
        private bool? memIsActive;

        /// <summary>
        /// [CreatedBy] column that will hold value from database table [RetailShop]
        /// </summary>
        private string memCreatedBy;

        /// <summary>
        /// [CreatedOn] column that will hold value from database table [RetailShop]
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
        /// [memRetailId] value for Set/Get operations
        /// </summary>
        public int? RetailId
        {
            get { return memRetailId; }
            set { memRetailId = value; }
        }

        /// <summary>
        /// [memRetailShopCategoryId] value for Set/Get operations
        /// </summary>
        public int? RetailShopCategoryId
        {
            get { return memRetailShopCategoryId; }
            set { memRetailShopCategoryId = value; }
        }

        /// <summary>
        /// [memName] value for Set/Get operations
        /// </summary>
        public string Name
        {
            get { return memName; }
            set { memName = value; }
        }

        /// <summary>
        /// [memAddress] value for Set/Get operations
        /// </summary>
        public string Address
        {
            get { return memAddress; }
            set { memAddress = value; }
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
        /// [memDistrictId] value for Set/Get operations
        /// </summary>
        public int? DistrictId
        {
            get { return memDistrictId; }
            set { memDistrictId = value; }
        }

        /// <summary>
        /// [memRetailDatabaseId] value for Set/Get operations
        /// </summary>
        public string RetailDatabaseId
        {
            get { return memRetailDatabaseId; }
            set { memRetailDatabaseId = value; }
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
        /// [memIsActive] value for Set/Get operations
        /// </summary>
        public bool? IsActive
        {
            get { return memIsActive; }
            set { memIsActive = value; }
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
        /// entRetailShop class constructor
        /// </summary>
        /// <param name="parRetailId">RetailId is set/get by this property.</param>
        /// <param name="parRetailShopCategoryId">RetailShopCategoryId is set/get by this property.</param>
        /// <param name="parName">Name is set/get by this property.</param>
        /// <param name="parAddress">Address is set/get by this property.</param>
        /// <param name="parCityId">CityId is set/get by this property.</param>
        /// <param name="parDistrictId">DistrictId is set/get by this property.</param>
        /// <param name="parRetailDatabaseId">RetailDatabaseId is set/get by this property.</param>
        /// <param name="parCoordinateX">CoordinateX is set/get by this property.</param>
        /// <param name="parCoordinateY">CoordinateY is set/get by this property.</param>
        /// <param name="parIsActive">IsActive is set/get by this property.</param>
        /// <param name="parCreatedBy">CreatedBy is set/get by this property.</param>
        /// <param name="parCreatedOn">CreatedOn is set/get by this property.</param>
        public entRetailShop(int? parRetailId, int? parRetailShopCategoryId, string parName, string parAddress, int? parCityId, int? parDistrictId, string parRetailDatabaseId, decimal? parCoordinateX, decimal? parCoordinateY, bool? parIsActive, string parCreatedBy, DateTime? parCreatedOn)
        {
            this.memRetailId = parRetailId;
            this.memRetailShopCategoryId = parRetailShopCategoryId;
            this.memName = parName;
            this.memAddress = parAddress;
            this.memCityId = parCityId;
            this.memDistrictId = parDistrictId;
            this.memRetailDatabaseId = parRetailDatabaseId;
            this.memCoordinateX = parCoordinateX;
            this.memCoordinateY = parCoordinateY;
            this.memIsActive = parIsActive;
            this.memCreatedBy = parCreatedBy;
            this.memCreatedOn = parCreatedOn;
        }

        /// <summary>
        /// entRetailShop class constructor
        /// </summary>
        /// <param name="parId">Id is set/get by this property.</param>
        /// <param name="parRetailId">RetailId is set/get by this property.</param>
        /// <param name="parRetailShopCategoryId">RetailShopCategoryId is set/get by this property.</param>
        /// <param name="parName">Name is set/get by this property.</param>
        /// <param name="parAddress">Address is set/get by this property.</param>
        /// <param name="parCityId">CityId is set/get by this property.</param>
        /// <param name="parDistrictId">DistrictId is set/get by this property.</param>
        /// <param name="parRetailDatabaseId">RetailDatabaseId is set/get by this property.</param>
        /// <param name="parCoordinateX">CoordinateX is set/get by this property.</param>
        /// <param name="parCoordinateY">CoordinateY is set/get by this property.</param>
        /// <param name="parIsActive">IsActive is set/get by this property.</param>
        /// <param name="parCreatedBy">CreatedBy is set/get by this property.</param>
        /// <param name="parCreatedOn">CreatedOn is set/get by this property.</param>
        public entRetailShop(int parId, int? parRetailId, int? parRetailShopCategoryId, string parName, string parAddress, int? parCityId, int? parDistrictId, string parRetailDatabaseId, decimal? parCoordinateX, decimal? parCoordinateY, bool? parIsActive, string parCreatedBy, DateTime? parCreatedOn)
        {
            this.memId = parId;
            this.memRetailId = parRetailId;
            this.memRetailShopCategoryId = parRetailShopCategoryId;
            this.memName = parName;
            this.memAddress = parAddress;
            this.memCityId = parCityId;
            this.memDistrictId = parDistrictId;
            this.memRetailDatabaseId = parRetailDatabaseId;
            this.memCoordinateX = parCoordinateX;
            this.memCoordinateY = parCoordinateY;
            this.memIsActive = parIsActive;
            this.memCreatedBy = parCreatedBy;
            this.memCreatedOn = parCreatedOn;
        }

        /// <summary>
        /// entRetailShop class constructor
        /// </summary>
        public entRetailShop()
        {
        }

        #endregion
    }
}