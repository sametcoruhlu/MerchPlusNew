using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace entMerchPlus
{
    /// <summary>
    /// Reflection of a row of database table [Retail]
    /// </summary>
    [Serializable()]
    [JsonObject(MemberSerialization.OptOut)]
    public class entRetail
    {
        #region MEMBERS
        /// <summary>
        /// [Id] column that will hold value from database table [Retail]
        /// </summary>
        private int memId;

        /// <summary>
        /// [Name] column that will hold value from database table [Retail]
        /// </summary>
        private string memName;

        /// <summary>
        /// [ProfilePicturePath] column that will hold value from database table [Retail]
        /// </summary>
        private string memProfilePicturePath;

        /// <summary>
        /// [RetailCategoryId] column that will hold value from database table [Retail]
        /// </summary>
        private int? memRetailCategoryId;

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
        /// [memName] value for Set/Get operations
        /// </summary>
        public string Name
        {
            get { return memName; }
            set { memName = value; }
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
        /// [memRetailCategoryId] value for Set/Get operations
        /// </summary>
        public int? RetailCategoryId
        {
            get { return memRetailCategoryId; }
            set { memRetailCategoryId = value; }
        }

        #endregion
        #region CONSTRUCTORS
        /// <summary>
        /// entRetail class constructor
        /// </summary>
        /// <param name="parName">Name is set/get by this property.</param>
        /// <param name="parProfilePicturePath">ProfilePicturePath is set/get by this property.</param>
        /// <param name="parRetailCategoryId">RetailCategoryId is set/get by this property.</param>
        public entRetail(string parName, string parProfilePicturePath, int? parRetailCategoryId)
        {
            this.memName = parName;
            this.memProfilePicturePath = parProfilePicturePath;
            this.memRetailCategoryId = parRetailCategoryId;
        }

        /// <summary>
        /// entRetail class constructor
        /// </summary>
        /// <param name="parId">Id is set/get by this property.</param>
        /// <param name="parName">Name is set/get by this property.</param>
        /// <param name="parProfilePicturePath">ProfilePicturePath is set/get by this property.</param>
        /// <param name="parRetailCategoryId">RetailCategoryId is set/get by this property.</param>
        public entRetail(int parId, string parName, string parProfilePicturePath, int? parRetailCategoryId)
        {
            this.memId = parId;
            this.memName = parName;
            this.memProfilePicturePath = parProfilePicturePath;
            this.memRetailCategoryId = parRetailCategoryId;
        }

        /// <summary>
        /// entRetail class constructor
        /// </summary>
        public entRetail()
        {
        }

        #endregion
    }
}