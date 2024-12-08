using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace entMerchPlus
{
    /// <summary>
    /// Reflection of a row of database table [CustomerBrand]
    /// </summary>
    [Serializable()]
    [JsonObject(MemberSerialization.OptOut)]
    public class entCustomerBrand
    {
        #region MEMBERS
        /// <summary>
        /// [Id] column that will hold value from database table [CustomerBrand]
        /// </summary>
        private int memId;

        /// <summary>
        /// [CustomerId] column that will hold value from database table [CustomerBrand]
        /// </summary>
        private int? memCustomerId;

        /// <summary>
        /// [Name] column that will hold value from database table [CustomerBrand]
        /// </summary>
        private string memName;

        /// <summary>
        /// [Description] column that will hold value from database table [CustomerBrand]
        /// </summary>
        private string memDescription;

        /// <summary>
        /// [ProfilePicturePath] column that will hold value from database table [CustomerBrand]
        /// </summary>
        private string memProfilePicturePath;

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
        /// [memCustomerId] value for Set/Get operations
        /// </summary>
        public int? CustomerId
        {
            get { return memCustomerId; }
            set { memCustomerId = value; }
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
        /// [memDescription] value for Set/Get operations
        /// </summary>
        public string Description
        {
            get { return memDescription; }
            set { memDescription = value; }
        }

        /// <summary>
        /// [memProfilePicturePath] value for Set/Get operations
        /// </summary>
        public string ProfilePicturePath
        {
            get { return memProfilePicturePath; }
            set { memProfilePicturePath = value; }
        }

        #endregion
        #region CONSTRUCTORS
        /// <summary>
        /// entCustomerBrand class constructor
        /// </summary>
        /// <param name="parCustomerId">CustomerId is set/get by this property.</param>
        /// <param name="parName">Name is set/get by this property.</param>
        /// <param name="parDescription">Description is set/get by this property.</param>
        /// <param name="parProfilePicturePath">ProfilePicturePath is set/get by this property.</param>
        public entCustomerBrand(int? parCustomerId, string parName, string parDescription, string parProfilePicturePath)
        {
            this.memCustomerId = parCustomerId;
            this.memName = parName;
            this.memDescription = parDescription;
            this.memProfilePicturePath = parProfilePicturePath;
        }

        /// <summary>
        /// entCustomerBrand class constructor
        /// </summary>
        /// <param name="parId">Id is set/get by this property.</param>
        /// <param name="parCustomerId">CustomerId is set/get by this property.</param>
        /// <param name="parName">Name is set/get by this property.</param>
        /// <param name="parDescription">Description is set/get by this property.</param>
        /// <param name="parProfilePicturePath">ProfilePicturePath is set/get by this property.</param>
        public entCustomerBrand(int parId, int? parCustomerId, string parName, string parDescription, string parProfilePicturePath)
        {
            this.memId = parId;
            this.memCustomerId = parCustomerId;
            this.memName = parName;
            this.memDescription = parDescription;
            this.memProfilePicturePath = parProfilePicturePath;
        }

        /// <summary>
        /// entCustomerBrand class constructor
        /// </summary>
        public entCustomerBrand()
        {
        }

        #endregion
    }
}