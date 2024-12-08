using System;
using System.Collections.Generic;
using System.Text;
using SqlHelper;
using System.Data;
using entMerchPlus;

namespace datMerchPlus
{
    /// <summary>
    /// The class that is generated hold parameters to manage database table [Retail]
    /// </summary>
    public class datRetail
    {
        /// <summary>
        /// datRetail Constructor method used while taking an instance of this class.
        /// </summary>
        public datRetail()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Method that selects all data stored in table [Retail]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public DataTable SelectRetail(DbConnector parDbConnector)
        {
            return parDbConnector.ExecuteDataTable("SelectRetail", null);
        }

        /// <summary>
        /// Method that selects one row of data in table [Retail] filtered by the PK field of the inbound entity object
        /// </summary>
        /// <param name="parEntRetail">Entity object as parameter for tableRetail]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void SelectRetailById(entRetail parEntRetail, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntRetail.Id);
            DataTable insDataTable = new DataTable();
            insDataTable = parDbConnector.ExecuteDataTable("SelectRetailById", insDbParamCollection);
            if (insDataTable.Rows.Count > 0)
            {
                if (insDataTable.Rows[0]["Id"] != DBNull.Value)
                {
                    parEntRetail.Id = Convert.ToInt32(insDataTable.Rows[0]["Id"]);
                }
                if (insDataTable.Rows[0]["Name"] != DBNull.Value)
                {
                    parEntRetail.Name = Convert.ToString(insDataTable.Rows[0]["Name"]);
                }
                if (insDataTable.Rows[0]["ProfilePicturePath"] != DBNull.Value)
                {
                    parEntRetail.ProfilePicturePath = Convert.ToString(insDataTable.Rows[0]["ProfilePicturePath"]);
                }
                if (insDataTable.Rows[0]["RetailCategoryId"] != DBNull.Value)
                {
                    parEntRetail.RetailCategoryId = Convert.ToInt32(insDataTable.Rows[0]["RetailCategoryId"]);
                }
            }
        }

        /// <summary>
        /// Method that inserts one row of data into table [Retail] based on the values of input entity object
        /// </summary>
        /// <param name="parEntRetail">Entity object as parameter for tableRetail]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void InsertRetail(entRetail parEntRetail, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.AddOutput("@pId", DbType.Int32);
            insDbParamCollection.Add("@pName", parEntRetail.Name);
            insDbParamCollection.Add("@pProfilePicturePath", parEntRetail.ProfilePicturePath);
            insDbParamCollection.Add("@pRetailCategoryId", parEntRetail.RetailCategoryId);
            parDbConnector.ExecuteNonQuery("InsertRetail", insDbParamCollection);
            parEntRetail.Id = Convert.ToInt32(insDbParamCollection.GetOutPutParameter().Value);
        }

        /// <summary>
        /// Method that updates one row of data in table [Retail] based on the values of input entity object
        /// </summary>
        /// <param name="parEntRetail">Entity object as parameter for tableRetail]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void UpdateRetailById(entRetail parEntRetail, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntRetail.Id);
            insDbParamCollection.Add("@pName", parEntRetail.Name);
            insDbParamCollection.Add("@pProfilePicturePath", parEntRetail.ProfilePicturePath);
            insDbParamCollection.Add("@pRetailCategoryId", parEntRetail.RetailCategoryId);
            parDbConnector.ExecuteNonQuery("UpdateRetailById", insDbParamCollection);
        }

        /// <summary>
        /// Method that deletes all data in table [Retail]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteRetail(DbConnector parDbConnector)
        {
            parDbConnector.ExecuteNonQuery("DeleteRetail", null);
        }

        /// <summary>
        /// Method that deletes one row of data in table [Retail] based on the passed entity object
        /// </summary>
        /// <param name="parEntRetail">Gets entity object as parameter for table Retail]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteRetailById(entRetail parEntRetail, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntRetail.Id);
            parDbConnector.ExecuteNonQuery("DeleteRetailById", insDbParamCollection);
        }

        #endregion
        #region Custom Methods
        public DataTable SelectRetailByRetailCategoryId(entRetail insEntRetail, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pRetailCategoryId", insEntRetail.RetailCategoryId);
            return insDbConnector.ExecuteDataTable("SelectRetailByRetailCategoryId", insDbParamCollection);
        }

        public DataTable SelectRetailGridData(DbConnector insDbConnector)
        {
            return insDbConnector.ExecuteDataTable("SelectRetailGridData", null);
        }
        #endregion
    }
}