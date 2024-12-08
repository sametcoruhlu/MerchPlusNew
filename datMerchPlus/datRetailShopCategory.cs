using System;
using System.Collections.Generic;
using System.Text;
using SqlHelper;
using System.Data;
using entMerchPlus;

namespace datMerchPlus
{
    /// <summary>
    /// The class that is generated hold parameters to manage database table [RetailShopCategory]
    /// </summary>
    public class datRetailShopCategory
    {
        /// <summary>
        /// datRetailShopCategory Constructor method used while taking an instance of this class.
        /// </summary>
        public datRetailShopCategory()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Method that selects all data stored in table [RetailShopCategory]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public DataTable SelectRetailShopCategory(DbConnector parDbConnector)
        {
            return parDbConnector.ExecuteDataTable("SelectRetailShopCategory", null);
        }

        /// <summary>
        /// Method that selects one row of data in table [RetailShopCategory] filtered by the PK field of the inbound entity object
        /// </summary>
        /// <param name="parEntRetailShopCategory">Entity object as parameter for tableRetailShopCategory]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void SelectRetailShopCategoryById(entRetailShopCategory parEntRetailShopCategory, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntRetailShopCategory.Id);
            DataTable insDataTable = new DataTable();
            insDataTable = parDbConnector.ExecuteDataTable("SelectRetailShopCategoryById", insDbParamCollection);
            if (insDataTable.Rows.Count > 0)
            {
                if (insDataTable.Rows[0]["Id"] != DBNull.Value)
                {
                    parEntRetailShopCategory.Id = Convert.ToInt32(insDataTable.Rows[0]["Id"]);
                }
                if (insDataTable.Rows[0]["RetailId"] != DBNull.Value)
                {
                    parEntRetailShopCategory.RetailId = Convert.ToInt32(insDataTable.Rows[0]["RetailId"]);
                }
                if (insDataTable.Rows[0]["ParentRetailCategoryId"] != DBNull.Value)
                {
                    parEntRetailShopCategory.ParentRetailCategoryId = Convert.ToInt32(insDataTable.Rows[0]["ParentRetailCategoryId"]);
                }
                if (insDataTable.Rows[0]["Name"] != DBNull.Value)
                {
                    parEntRetailShopCategory.Name = Convert.ToString(insDataTable.Rows[0]["Name"]);
                }
            }
        }

        /// <summary>
        /// Method that inserts one row of data into table [RetailShopCategory] based on the values of input entity object
        /// </summary>
        /// <param name="parEntRetailShopCategory">Entity object as parameter for tableRetailShopCategory]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void InsertRetailShopCategory(entRetailShopCategory parEntRetailShopCategory, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.AddOutput("@pId", DbType.Int32);
            insDbParamCollection.Add("@pRetailId", parEntRetailShopCategory.RetailId);
            insDbParamCollection.Add("@pParentRetailCategoryId", parEntRetailShopCategory.ParentRetailCategoryId);
            insDbParamCollection.Add("@pName", parEntRetailShopCategory.Name);
            parDbConnector.ExecuteNonQuery("InsertRetailShopCategory", insDbParamCollection);
            parEntRetailShopCategory.Id = Convert.ToInt32(insDbParamCollection.GetOutPutParameter().Value);
        }

        /// <summary>
        /// Method that updates one row of data in table [RetailShopCategory] based on the values of input entity object
        /// </summary>
        /// <param name="parEntRetailShopCategory">Entity object as parameter for tableRetailShopCategory]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void UpdateRetailShopCategoryById(entRetailShopCategory parEntRetailShopCategory, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntRetailShopCategory.Id);
            insDbParamCollection.Add("@pRetailId", parEntRetailShopCategory.RetailId);
            insDbParamCollection.Add("@pParentRetailCategoryId", parEntRetailShopCategory.ParentRetailCategoryId);
            insDbParamCollection.Add("@pName", parEntRetailShopCategory.Name);
            parDbConnector.ExecuteNonQuery("UpdateRetailShopCategoryById", insDbParamCollection);
        }

        /// <summary>
        /// Method that deletes all data in table [RetailShopCategory]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteRetailShopCategory(DbConnector parDbConnector)
        {
            parDbConnector.ExecuteNonQuery("DeleteRetailShopCategory", null);
        }

        /// <summary>
        /// Method that deletes one row of data in table [RetailShopCategory] based on the passed entity object
        /// </summary>
        /// <param name="parEntRetailShopCategory">Gets entity object as parameter for table RetailShopCategory]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteRetailShopCategoryById(entRetailShopCategory parEntRetailShopCategory, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntRetailShopCategory.Id);
            parDbConnector.ExecuteNonQuery("DeleteRetailShopCategoryById", insDbParamCollection);
        }

        #endregion
        #region Custom Methods
        public DataTable SelectRetailShopCategoryByRetailId(entRetailShopCategory insEntRetailShopCategory, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pRetailId", insEntRetailShopCategory.RetailId);
            return insDbConnector.ExecuteDataTable("SelectRetailShopCategoryByRetailId", insDbParamCollection);
        }

        public DataTable SelectRetailShopCategoryGridData(DbConnector insDbConnector)
        {
            return insDbConnector.ExecuteDataTable("SelectRetailShopCategoryGridData", null);
        }
        #endregion
    }
}
