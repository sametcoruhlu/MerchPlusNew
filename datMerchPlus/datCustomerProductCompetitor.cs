using System;
using System.Collections.Generic;
using System.Text;
using SqlHelper;
using System.Data;
using entMerchPlus;

namespace datMerchPlus
{
    /// <summary>
    /// The class that is generated hold parameters to manage database table [CustomerProductCompetitor]
    /// </summary>
    public class datCustomerProductCompetitor
    {
        /// <summary>
        /// datCustomerProductCompetitor Constructor method used while taking an instance of this class.
        /// </summary>
        public datCustomerProductCompetitor()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Method that selects all data stored in table [CustomerProductCompetitor]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public DataTable SelectCustomerProductCompetitor(DbConnector parDbConnector)
        {
            return parDbConnector.ExecuteDataTable("SelectCustomerProductCompetitor", null);
        }

        /// <summary>
        /// Method that selects one row of data in table [CustomerProductCompetitor] filtered by the PK field of the inbound entity object
        /// </summary>
        /// <param name="parEntCustomerProductCompetitor">Entity object as parameter for tableCustomerProductCompetitor]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void SelectCustomerProductCompetitorById(entCustomerProductCompetitor parEntCustomerProductCompetitor, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntCustomerProductCompetitor.Id);
            DataTable insDataTable = new DataTable();
            insDataTable = parDbConnector.ExecuteDataTable("SelectCustomerProductCompetitorById", insDbParamCollection);
            if (insDataTable.Rows.Count > 0)
            {
                if (insDataTable.Rows[0]["Id"] != DBNull.Value)
                {
                    parEntCustomerProductCompetitor.Id = Convert.ToInt32(insDataTable.Rows[0]["Id"]);
                }
                if (insDataTable.Rows[0]["CustomerProductId"] != DBNull.Value)
                {
                    parEntCustomerProductCompetitor.CustomerProductId = Convert.ToInt32(insDataTable.Rows[0]["CustomerProductId"]);
                }
                if (insDataTable.Rows[0]["CompetitorCustomerProductId"] != DBNull.Value)
                {
                    parEntCustomerProductCompetitor.CompetitorCustomerProductId = Convert.ToInt32(insDataTable.Rows[0]["CompetitorCustomerProductId"]);
                }
            }
        }

        /// <summary>
        /// Method that inserts one row of data into table [CustomerProductCompetitor] based on the values of input entity object
        /// </summary>
        /// <param name="parEntCustomerProductCompetitor">Entity object as parameter for tableCustomerProductCompetitor]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void InsertCustomerProductCompetitor(entCustomerProductCompetitor parEntCustomerProductCompetitor, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.AddOutput("@pId", DbType.Int32);
            insDbParamCollection.Add("@pCustomerProductId", parEntCustomerProductCompetitor.CustomerProductId);
            insDbParamCollection.Add("@pCompetitorCustomerProductId", parEntCustomerProductCompetitor.CompetitorCustomerProductId);
            parDbConnector.ExecuteNonQuery("InsertCustomerProductCompetitor", insDbParamCollection);
            parEntCustomerProductCompetitor.Id = Convert.ToInt32(insDbParamCollection.GetOutPutParameter().Value);
        }

        /// <summary>
        /// Method that updates one row of data in table [CustomerProductCompetitor] based on the values of input entity object
        /// </summary>
        /// <param name="parEntCustomerProductCompetitor">Entity object as parameter for tableCustomerProductCompetitor]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void UpdateCustomerProductCompetitorById(entCustomerProductCompetitor parEntCustomerProductCompetitor, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntCustomerProductCompetitor.Id);
            insDbParamCollection.Add("@pCustomerProductId", parEntCustomerProductCompetitor.CustomerProductId);
            insDbParamCollection.Add("@pCompetitorCustomerProductId", parEntCustomerProductCompetitor.CompetitorCustomerProductId);
            parDbConnector.ExecuteNonQuery("UpdateCustomerProductCompetitorById", insDbParamCollection);
        }

        /// <summary>
        /// Method that deletes all data in table [CustomerProductCompetitor]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteCustomerProductCompetitor(DbConnector parDbConnector)
        {
            parDbConnector.ExecuteNonQuery("DeleteCustomerProductCompetitor", null);
        }

        /// <summary>
        /// Method that deletes one row of data in table [CustomerProductCompetitor] based on the passed entity object
        /// </summary>
        /// <param name="parEntCustomerProductCompetitor">Gets entity object as parameter for table CustomerProductCompetitor]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteCustomerProductCompetitorById(entCustomerProductCompetitor parEntCustomerProductCompetitor, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntCustomerProductCompetitor.Id);
            parDbConnector.ExecuteNonQuery("DeleteCustomerProductCompetitorById", insDbParamCollection);
        }

        #endregion
        #region Custom Methods
        public DataTable SelectCustomerProductCompetitorByCustomerProductId(entCustomerProductCompetitor insEntCustomerProductCompetitor, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pCustomerProductId", insEntCustomerProductCompetitor.CustomerProductId);
            return insDbConnector.ExecuteDataTable("SelectCustomerProductCompetitorByCustomerProductId", insDbParamCollection);
        }

        public DataTable SelectCustomerProductCompetitorByCustomerIdGridData(int customerId, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pCustomerId", customerId);
            return insDbConnector.ExecuteDataTable("SelectCustomerProductCompetitorByCustomerIdGridData", insDbParamCollection);
        }

        public DataTable SelectCustomerProductCompetitorByCustomerId(int customerId, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pCustomerId", customerId);
            return insDbConnector.ExecuteDataTable("SelectCustomerProductCompetitorByCustomerId", insDbParamCollection);
        }
        #endregion
    }
}