﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace entMerchPlus
{
    /// <summary>
    /// Reflection of a row of database table [RetailCategory]
    /// </summary>
    [Serializable()]
    [JsonObject(MemberSerialization.OptOut)]
    public class entRetailCategory
    {
        #region MEMBERS
        /// <summary>
        /// [Id] column that will hold value from database table [RetailCategory]
        /// </summary>
        private int memId;

        /// <summary>
        /// [Name] column that will hold value from database table [RetailCategory]
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
        /// entRetailCategory class constructor
        /// </summary>
        /// <param name="parName">Name is set/get by this property.</param>
        public entRetailCategory(string parName)
        {
            this.memName = parName;
        }

        /// <summary>
        /// entRetailCategory class constructor
        /// </summary>
        /// <param name="parId">Id is set/get by this property.</param>
        /// <param name="parName">Name is set/get by this property.</param>
        public entRetailCategory(int parId, string parName)
        {
            this.memId = parId;
            this.memName = parName;
        }

        /// <summary>
        /// entRetailCategory class constructor
        /// </summary>
        public entRetailCategory()
        {
        }

        #endregion
    }
}