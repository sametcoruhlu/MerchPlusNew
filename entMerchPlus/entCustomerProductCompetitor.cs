using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace entMerchPlus
{
    /// <summary>
    /// Reflection of a row of database table [CustomerProductCompetitor]
    /// </summary>
    [Serializable()]
    [JsonObject(MemberSerialization.OptOut)]
    public class entCustomerProductCompetitor
    {
        #region MEMBERS
        /// <summary>
        /// [Id] column that will hold value from database table [CustomerProductCompetitor]
        /// </summary>
        private int memId;

        /// <summary>
        /// [CustomerProductId] column that will hold value from database table [CustomerProductCompetitor]
        /// </summary>
        private int? memCustomerProductId;

        /// <summary>
        /// [CompetitorCustomerProductId] column that will hold value from database table [CustomerProductCompetitor]
        /// </summary>
        private int? memCompetitorCustomerProductId;

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
        /// [memCustomerProductId] value for Set/Get operations
        /// </summary>
        public int? CustomerProductId
        {
            get { return memCustomerProductId; }
            set { memCustomerProductId = value; }
        }

        /// <summary>
        /// [memCompetitorCustomerProductId] value for Set/Get operations
        /// </summary>
        public int? CompetitorCustomerProductId
        {
            get { return memCompetitorCustomerProductId; }
            set { memCompetitorCustomerProductId = value; }
        }

        #endregion
        #region CONSTRUCTORS
        /// <summary>
        /// entCustomerProductCompetitor class constructor
        /// </summary>
        /// <param name="parCustomerProductId">CustomerProductId is set/get by this property.</param>
        /// <param name="parCompetitorCustomerProductId">CompetitorCustomerProductId is set/get by this property.</param>
        public entCustomerProductCompetitor(int? parCustomerProductId, int? parCompetitorCustomerProductId)
        {
            this.memCustomerProductId = parCustomerProductId;
            this.memCompetitorCustomerProductId = parCompetitorCustomerProductId;
        }

        /// <summary>
        /// entCustomerProductCompetitor class constructor
        /// </summary>
        /// <param name="parId">Id is set/get by this property.</param>
        /// <param name="parCustomerProductId">CustomerProductId is set/get by this property.</param>
        /// <param name="parCompetitorCustomerProductId">CompetitorCustomerProductId is set/get by this property.</param>
        public entCustomerProductCompetitor(int parId, int? parCustomerProductId, int? parCompetitorCustomerProductId)
        {
            this.memId = parId;
            this.memCustomerProductId = parCustomerProductId;
            this.memCompetitorCustomerProductId = parCompetitorCustomerProductId;
        }

        /// <summary>
        /// entCustomerProductCompetitor class constructor
        /// </summary>
        public entCustomerProductCompetitor()
        {
        }

        #endregion
    }
}
