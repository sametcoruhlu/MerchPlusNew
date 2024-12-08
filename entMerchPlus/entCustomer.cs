using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace entMerchPlus
{
    /// <summary>
    /// Reflection of a row of database table [Customer]
    /// </summary>
    [Serializable()]
    [JsonObject(MemberSerialization.OptOut)]
    public class entCustomer
    {
        #region MEMBERS
        /// <summary>
        /// [Id] column that will hold value from database table [Customer]
        /// </summary>
        private int memId;

        /// <summary>
        /// [Name] column that will hold value from database table [Customer]
        /// </summary>
        private string memName;

        /// <summary>
        /// [ProfilePicturePath] column that will hold value from database table [Customer]
        /// </summary>
        private string memProfilePicturePath;

        /// <summary>
        /// [IsActive] column that will hold value from database table [Customer]
        /// </summary>
        private bool? memIsActive;

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
        /// [memIsActive] value for Set/Get operations
        /// </summary>
        public bool? IsActive
        {
            get { return memIsActive; }
            set { memIsActive = value; }
        }

        #endregion
        #region CONSTRUCTORS
        /// <summary>
        /// entCustomer class constructor
        /// </summary>
        /// <param name="parName">Name is set/get by this property.</param>
        /// <param name="parProfilePicturePath">ProfilePicturePath is set/get by this property.</param>
        /// <param name="parIsActive">IsActive is set/get by this property.</param>
        public entCustomer(string parName, string parProfilePicturePath, bool? parIsActive)
        {
            this.memName = parName;
            this.memProfilePicturePath = parProfilePicturePath;
            this.memIsActive = parIsActive;
        }

        /// <summary>
        /// entCustomer class constructor
        /// </summary>
        /// <param name="parId">Id is set/get by this property.</param>
        /// <param name="parName">Name is set/get by this property.</param>
        /// <param name="parProfilePicturePath">ProfilePicturePath is set/get by this property.</param>
        /// <param name="parIsActive">IsActive is set/get by this property.</param>
        public entCustomer(int parId, string parName, string parProfilePicturePath, bool? parIsActive)
        {
            this.memId = parId;
            this.memName = parName;
            this.memProfilePicturePath = parProfilePicturePath;
            this.memIsActive = parIsActive;
        }

        /// <summary>
        /// entCustomer class constructor
        /// </summary>
        public entCustomer()
        {
        }

        #endregion
    }
}
