using System;
using System.Collections.Generic;
using System.Text;
using SqlHelper;
using System.Data;
using entMerchPlus;

namespace datMerchPlus
{
    /// <summary>
    /// The class that is generated hold parameters to manage database table [SalesOrderPackageType]
    /// </summary>
    public class datSalesOrderPackageType
    {
        /// <summary>
        /// datSalesOrderPackageType Constructor method used while taking an instance of this class.
        /// </summary>
        public datSalesOrderPackageType()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Method that selects all data stored in table [SalesOrderPackageType]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public DataTable SelectSalesOrderPackageType(DbConnector parDbConnector)
        {
            return parDbConnector.ExecuteDataTable("SelectSalesOrderPackageType", null);
        }

        /// <summary>
        /// Method that selects one row of data in table [SalesOrderPackageType] filtered by the PK field of the inbound entity object
        /// </summary>
        /// <param name="parEntSalesOrderPackageType">Entity object as parameter for tableSalesOrderPackageType]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void SelectSalesOrderPackageTypeById(entSalesOrderPackageType parEntSalesOrderPackageType, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntSalesOrderPackageType.Id);
            DataTable insDataTable = new DataTable();
            insDataTable = parDbConnector.ExecuteDataTable("SelectSalesOrderPackageTypeById", insDbParamCollection);
            if (insDataTable.Rows.Count > 0)
            {
                if (insDataTable.Rows[0]["Id"] != DBNull.Value)
                {
                    parEntSalesOrderPackageType.Id = Convert.ToInt32(insDataTable.Rows[0]["Id"]);
                }
                if (insDataTable.Rows[0]["Name"] != DBNull.Value)
                {
                    parEntSalesOrderPackageType.Name = Convert.ToString(insDataTable.Rows[0]["Name"]);
                }
            }
        }

        /// <summary>
        /// Method that inserts one row of data into table [SalesOrderPackageType] based on the values of input entity object
        /// </summary>
        /// <param name="parEntSalesOrderPackageType">Entity object as parameter for tableSalesOrderPackageType]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void InsertSalesOrderPackageType(entSalesOrderPackageType parEntSalesOrderPackageType, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.AddOutput("@pId", DbType.Int32);
            insDbParamCollection.Add("@pName", parEntSalesOrderPackageType.Name);
            parDbConnector.ExecuteNonQuery("InsertSalesOrderPackageType", insDbParamCollection);
            parEntSalesOrderPackageType.Id = Convert.ToInt32(insDbParamCollection.GetOutPutParameter().Value);
        }

        /// <summary>
        /// Method that updates one row of data in table [SalesOrderPackageType] based on the values of input entity object
        /// </summary>
        /// <param name="parEntSalesOrderPackageType">Entity object as parameter for tableSalesOrderPackageType]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void UpdateSalesOrderPackageTypeById(entSalesOrderPackageType parEntSalesOrderPackageType, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntSalesOrderPackageType.Id);
            insDbParamCollection.Add("@pName", parEntSalesOrderPackageType.Name);
            parDbConnector.ExecuteNonQuery("UpdateSalesOrderPackageTypeById", insDbParamCollection);
        }

        /// <summary>
        /// Method that deletes all data in table [SalesOrderPackageType]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteSalesOrderPackageType(DbConnector parDbConnector)
        {
            parDbConnector.ExecuteNonQuery("DeleteSalesOrderPackageType", null);
        }

        /// <summary>
        /// Method that deletes one row of data in table [SalesOrderPackageType] based on the passed entity object
        /// </summary>
        /// <param name="parEntSalesOrderPackageType">Gets entity object as parameter for table SalesOrderPackageType]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteSalesOrderPackageTypeById(entSalesOrderPackageType parEntSalesOrderPackageType, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntSalesOrderPackageType.Id);
            parDbConnector.ExecuteNonQuery("DeleteSalesOrderPackageTypeById", insDbParamCollection);
        }

        #endregion
        #region Custom Methods
        #endregion
    }
}
