using System;
using System.Collections.Generic;
using System.Text;
using SqlHelper;
using System.Data;
using entMerchPlus;

namespace datMerchPlus
{
    /// <summary>
    /// The class that is generated hold parameters to manage database table [CustomerBrandPool]
    /// </summary>
    public class datCustomerBrandPool
    {
        /// <summary>
        /// datCustomerBrandPool Constructor method used while taking an instance of this class.
        /// </summary>
        public datCustomerBrandPool()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Method that selects all data stored in table [CustomerBrandPool]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public DataTable SelectCustomerBrandPool(DbConnector parDbConnector)
        {
            return parDbConnector.ExecuteDataTable("SelectCustomerBrandPool", null);
        }

        /// <summary>
        /// Method that selects one row of data in table [CustomerBrandPool] filtered by the PK field of the inbound entity object
        /// </summary>
        /// <param name="parEntCustomerBrandPool">Entity object as parameter for tableCustomerBrandPool]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void SelectCustomerBrandPoolById(entCustomerBrandPool parEntCustomerBrandPool, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntCustomerBrandPool.Id);
            DataTable insDataTable = new DataTable();
            insDataTable = parDbConnector.ExecuteDataTable("SelectCustomerBrandPoolById", insDbParamCollection);
            if (insDataTable.Rows.Count > 0)
            {
                if (insDataTable.Rows[0]["Id"] != DBNull.Value)
                {
                    parEntCustomerBrandPool.Id = Convert.ToInt32(insDataTable.Rows[0]["Id"]);
                }
                if (insDataTable.Rows[0]["CustomerId"] != DBNull.Value)
                {
                    parEntCustomerBrandPool.CustomerId = Convert.ToInt32(insDataTable.Rows[0]["CustomerId"]);
                }
                if (insDataTable.Rows[0]["CustomerBrandId"] != DBNull.Value)
                {
                    parEntCustomerBrandPool.CustomerBrandId = Convert.ToInt32(insDataTable.Rows[0]["CustomerBrandId"]);
                }
            }
        }

        /// <summary>
        /// Method that inserts one row of data into table [CustomerBrandPool] based on the values of input entity object
        /// </summary>
        /// <param name="parEntCustomerBrandPool">Entity object as parameter for tableCustomerBrandPool]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void InsertCustomerBrandPool(entCustomerBrandPool parEntCustomerBrandPool, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.AddOutput("@pId", DbType.Int32);
            insDbParamCollection.Add("@pCustomerId", parEntCustomerBrandPool.CustomerId);
            insDbParamCollection.Add("@pCustomerBrandId", parEntCustomerBrandPool.CustomerBrandId);
            parDbConnector.ExecuteNonQuery("InsertCustomerBrandPool", insDbParamCollection);
            parEntCustomerBrandPool.Id = Convert.ToInt32(insDbParamCollection.GetOutPutParameter().Value);
        }

        /// <summary>
        /// Method that updates one row of data in table [CustomerBrandPool] based on the values of input entity object
        /// </summary>
        /// <param name="parEntCustomerBrandPool">Entity object as parameter for tableCustomerBrandPool]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void UpdateCustomerBrandPoolById(entCustomerBrandPool parEntCustomerBrandPool, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntCustomerBrandPool.Id);
            insDbParamCollection.Add("@pCustomerId", parEntCustomerBrandPool.CustomerId);
            insDbParamCollection.Add("@pCustomerBrandId", parEntCustomerBrandPool.CustomerBrandId);
            parDbConnector.ExecuteNonQuery("UpdateCustomerBrandPoolById", insDbParamCollection);
        }

        /// <summary>
        /// Method that deletes all data in table [CustomerBrandPool]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteCustomerBrandPool(DbConnector parDbConnector)
        {
            parDbConnector.ExecuteNonQuery("DeleteCustomerBrandPool", null);
        }

        /// <summary>
        /// Method that deletes one row of data in table [CustomerBrandPool] based on the passed entity object
        /// </summary>
        /// <param name="parEntCustomerBrandPool">Gets entity object as parameter for table CustomerBrandPool]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteCustomerBrandPoolById(entCustomerBrandPool parEntCustomerBrandPool, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntCustomerBrandPool.Id);
            parDbConnector.ExecuteNonQuery("DeleteCustomerBrandPoolById", insDbParamCollection);
        }

        #endregion
        #region Custom Methods
        #endregion
    }
}
