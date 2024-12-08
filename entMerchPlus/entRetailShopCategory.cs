using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace entMerchPlus
{
    /// <summary>
    /// Reflection of a row of database table [RetailShopCategory]
    /// </summary>
    [Serializable()]
    [JsonObject(MemberSerialization.OptOut)]
    public class entRetailShopCategory
    {
        #region MEMBERS
        /// <summary>
        /// [Id] column that will hold value from database table [RetailShopCategory]
        /// </summary>
        private int memId;

        /// <summary>
        /// [RetailId] column that will hold value from database table [RetailShopCategory]
        /// </summary>
        private int? memRetailId;

        /// <summary>
        /// [ParentRetailCategoryId] column that will hold value from database table [RetailShopCategory]
        /// </summary>
        private int? memParentRetailCategoryId;

        /// <summary>
        /// [Name] column that will hold value from database table [RetailShopCategory]
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
        /// [memRetailId] value for Set/Get operations
        /// </summary>
        public int? RetailId
        {
            get { return memRetailId; }
            set { memRetailId = value; }
        }

        /// <summary>
        /// [memParentRetailCategoryId] value for Set/Get operations
        /// </summary>
        public int? ParentRetailCategoryId
        {
            get { return memParentRetailCategoryId; }
            set { memParentRetailCategoryId = value; }
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
        /// entRetailShopCategory class constructor
        /// </summary>
        /// <param name="parRetailId">RetailId is set/get by this property.</param>
        /// <param name="parParentRetailCategoryId">ParentRetailCategoryId is set/get by this property.</param>
        /// <param name="parName">Name is set/get by this property.</param>
        public entRetailShopCategory(int? parRetailId, int? parParentRetailCategoryId, string parName)
        {
            this.memRetailId = parRetailId;
            this.memParentRetailCategoryId = parParentRetailCategoryId;
            this.memName = parName;
        }

        /// <summary>
        /// entRetailShopCategory class constructor
        /// </summary>
        /// <param name="parId">Id is set/get by this property.</param>
        /// <param name="parRetailId">RetailId is set/get by this property.</param>
        /// <param name="parParentRetailCategoryId">ParentRetailCategoryId is set/get by this property.</param>
        /// <param name="parName">Name is set/get by this property.</param>
        public entRetailShopCategory(int parId, int? parRetailId, int? parParentRetailCategoryId, string parName)
        {
            this.memId = parId;
            this.memRetailId = parRetailId;
            this.memParentRetailCategoryId = parParentRetailCategoryId;
            this.memName = parName;
        }

        /// <summary>
        /// entRetailShopCategory class constructor
        /// </summary>
        public entRetailShopCategory()
        {
        }

        #endregion
    }
}
