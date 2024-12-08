using System;
using System.Collections.Generic;
using System.Text;
using SqlHelper;
using System.Data;
using entMerchPlus;

namespace datMerchPlus
{
    /// <summary>
    /// The class that is generated hold parameters to manage database table [RetailCategory]
    /// </summary>
    public class datRetailCategory
    {
        /// <summary>
        /// datRetailCategory Constructor method used while taking an instance of this class.
        /// </summary>
        public datRetailCategory()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Method that selects all data stored in table [RetailCategory]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public DataTable SelectRetailCategory(DbConnector parDbConnector)
        {
            return parDbConnector.ExecuteDataTable("SelectRetailCategory", null);
        }

        /// <summary>
        /// Method that selects one row of data in table [RetailCategory] filtered by the PK field of the inbound entity object
        /// </summary>
        /// <param name="parEntRetailCategory">Entity object as parameter for tableRetailCategory]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void SelectRetailCategoryById(entRetailCategory parEntRetailCategory, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntRetailCategory.Id);
            DataTable insDataTable = new DataTable();
            insDataTable = parDbConnector.ExecuteDataTable("SelectRetailCategoryById", insDbParamCollection);
            if (insDataTable.Rows.Count > 0)
            {
                if (insDataTable.Rows[0]["Id"] != DBNull.Value)
                {
                    parEntRetailCategory.Id = Convert.ToInt32(insDataTable.Rows[0]["Id"]);
                }
                if (insDataTable.Rows[0]["Name"] != DBNull.Value)
                {
                    parEntRetailCategory.Name = Convert.ToString(insDataTable.Rows[0]["Name"]);
                }
            }
        }

        /// <summary>
        /// Method that inserts one row of data into table [RetailCategory] based on the values of input entity object
        /// </summary>
        /// <param name="parEntRetailCategory">Entity object as parameter for tableRetailCategory]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void InsertRetailCategory(entRetailCategory parEntRetailCategory, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.AddOutput("@pId", DbType.Int32);
            insDbParamCollection.Add("@pName", parEntRetailCategory.Name);
            parDbConnector.ExecuteNonQuery("InsertRetailCategory", insDbParamCollection);
            parEntRetailCategory.Id = Convert.ToInt32(insDbParamCollection.GetOutPutParameter().Value);
        }

        /// <summary>
        /// Method that updates one row of data in table [RetailCategory] based on the values of input entity object
        /// </summary>
        /// <param name="parEntRetailCategory">Entity object as parameter for tableRetailCategory]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void UpdateRetailCategoryById(entRetailCategory parEntRetailCategory, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntRetailCategory.Id);
            insDbParamCollection.Add("@pName", parEntRetailCategory.Name);
            parDbConnector.ExecuteNonQuery("UpdateRetailCategoryById", insDbParamCollection);
        }

        /// <summary>
        /// Method that deletes all data in table [RetailCategory]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteRetailCategory(DbConnector parDbConnector)
        {
            parDbConnector.ExecuteNonQuery("DeleteRetailCategory", null);
        }

        /// <summary>
        /// Method that deletes one row of data in table [RetailCategory] based on the passed entity object
        /// </summary>
        /// <param name="parEntRetailCategory">Gets entity object as parameter for table RetailCategory]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteRetailCategoryById(entRetailCategory parEntRetailCategory, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntRetailCategory.Id);
            parDbConnector.ExecuteNonQuery("DeleteRetailCategoryById", insDbParamCollection);
        }

        #endregion
        #region Custom Methods
        #endregion
    }
}