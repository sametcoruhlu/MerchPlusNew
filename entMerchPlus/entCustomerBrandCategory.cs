using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace entMerchPlus
{
    /// <summary>
    /// Reflection of a row of database table [CustomerBrandCategory]
    /// </summary>
    [Serializable()]
    [JsonObject(MemberSerialization.OptOut)]
    public class entCustomerBrandCategory
    {
        #region MEMBERS
        /// <summary>
        /// [Id] column that will hold value from database table [CustomerBrandCategory]
        /// </summary>
        private int memId;

        /// <summary>
        /// [CustomerBrandId] column that will hold value from database table [CustomerBrandCategory]
        /// </summary>
        private int? memCustomerBrandId;

        /// <summary>
        /// [ParentId] column that will hold value from database table [CustomerBrandCategory]
        /// </summary>
        private int? memParentId;

        /// <summary>
        /// [Name] column that will hold value from database table [CustomerBrandCategory]
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
        /// [memCustomerBrandId] value for Set/Get operations
        /// </summary>
        public int? CustomerBrandId
        {
            get { return memCustomerBrandId; }
            set { memCustomerBrandId = value; }
        }

        /// <summary>
        /// [memParentId] value for Set/Get operations
        /// </summary>
        public int? ParentId
        {
            get { return memParentId; }
            set { memParentId = value; }
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
        /// entCustomerBrandCategory class constructor
        /// </summary>
        /// <param name="parCustomerBrandId">CustomerBrandId is set/get by this property.</param>
        /// <param name="parParentId">ParentId is set/get by this property.</param>
        /// <param name="parName">Name is set/get by this property.</param>
        public entCustomerBrandCategory(int? parCustomerBrandId, int? parParentId, string parName)
        {
            this.memCustomerBrandId = parCustomerBrandId;
            this.memParentId = parParentId;
            this.memName = parName;
        }

        /// <summary>
        /// entCustomerBrandCategory class constructor
        /// </summary>
        /// <param name="parId">Id is set/get by this property.</param>
        /// <param name="parCustomerBrandId">CustomerBrandId is set/get by this property.</param>
        /// <param name="parParentId">ParentId is set/get by this property.</param>
        /// <param name="parName">Name is set/get by this property.</param>
        public entCustomerBrandCategory(int parId, int? parCustomerBrandId, int? parParentId, string parName)
        {
            this.memId = parId;
            this.memCustomerBrandId = parCustomerBrandId;
            this.memParentId = parParentId;
            this.memName = parName;
        }

        /// <summary>
        /// entCustomerBrandCategory class constructor
        /// </summary>
        public entCustomerBrandCategory()
        {
        }

        #endregion
    }
}