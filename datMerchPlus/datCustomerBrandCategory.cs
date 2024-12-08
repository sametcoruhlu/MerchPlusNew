using System;
using System.Collections.Generic;
using System.Text;
using SqlHelper;
using System.Data;
using entMerchPlus;

namespace datMerchPlus
{
    /// <summary>
    /// The class that is generated hold parameters to manage database table [CustomerBrandCategory]
    /// </summary>
    public class datCustomerBrandCategory
    {
        /// <summary>
        /// datCustomerBrandCategory Constructor method used while taking an instance of this class.
        /// </summary>
        public datCustomerBrandCategory()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Method that selects all data stored in table [CustomerBrandCategory]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public DataTable SelectCustomerBrandCategory(DbConnector parDbConnector)
        {
            return parDbConnector.ExecuteDataTable("SelectCustomerBrandCategory", null);
        }

        /// <summary>
        /// Method that selects one row of data in table [CustomerBrandCategory] filtered by the PK field of the inbound entity object
        /// </summary>
        /// <param name="parEntCustomerBrandCategory">Entity object as parameter for tableCustomerBrandCategory]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void SelectCustomerBrandCategoryById(entCustomerBrandCategory parEntCustomerBrandCategory, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntCustomerBrandCategory.Id);
            DataTable insDataTable = new DataTable();
            insDataTable = parDbConnector.ExecuteDataTable("SelectCustomerBrandCategoryById", insDbParamCollection);
            if (insDataTable.Rows.Count > 0)
            {
                if (insDataTable.Rows[0]["Id"] != DBNull.Value)
                {
                    parEntCustomerBrandCategory.Id = Convert.ToInt32(insDataTable.Rows[0]["Id"]);
                }
                if (insDataTable.Rows[0]["CustomerBrandId"] != DBNull.Value)
                {
                    parEntCustomerBrandCategory.CustomerBrandId = Convert.ToInt32(insDataTable.Rows[0]["CustomerBrandId"]);
                }
                if (insDataTable.Rows[0]["ParentId"] != DBNull.Value)
                {
                    parEntCustomerBrandCategory.ParentId = Convert.ToInt32(insDataTable.Rows[0]["ParentId"]);
                }
                if (insDataTable.Rows[0]["Name"] != DBNull.Value)
                {
                    parEntCustomerBrandCategory.Name = Convert.ToString(insDataTable.Rows[0]["Name"]);
                }
            }
        }

        /// <summary>
        /// Method that inserts one row of data into table [CustomerBrandCategory] based on the values of input entity object
        /// </summary>
        /// <param name="parEntCustomerBrandCategory">Entity object as parameter for tableCustomerBrandCategory]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void InsertCustomerBrandCategory(entCustomerBrandCategory parEntCustomerBrandCategory, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.AddOutput("@pId", DbType.Int32);
            insDbParamCollection.Add("@pCustomerBrandId", parEntCustomerBrandCategory.CustomerBrandId);
            insDbParamCollection.Add("@pParentId", parEntCustomerBrandCategory.ParentId);
            insDbParamCollection.Add("@pName", parEntCustomerBrandCategory.Name);
            parDbConnector.ExecuteNonQuery("InsertCustomerBrandCategory", insDbParamCollection);
            parEntCustomerBrandCategory.Id = Convert.ToInt32(insDbParamCollection.GetOutPutParameter().Value);
        }

        /// <summary>
        /// Method that updates one row of data in table [CustomerBrandCategory] based on the values of input entity object
        /// </summary>
        /// <param name="parEntCustomerBrandCategory">Entity object as parameter for tableCustomerBrandCategory]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void UpdateCustomerBrandCategoryById(entCustomerBrandCategory parEntCustomerBrandCategory, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntCustomerBrandCategory.Id);
            insDbParamCollection.Add("@pCustomerBrandId", parEntCustomerBrandCategory.CustomerBrandId);
            insDbParamCollection.Add("@pParentId", parEntCustomerBrandCategory.ParentId);
            insDbParamCollection.Add("@pName", parEntCustomerBrandCategory.Name);
            parDbConnector.ExecuteNonQuery("UpdateCustomerBrandCategoryById", insDbParamCollection);
        }

        /// <summary>
        /// Method that deletes all data in table [CustomerBrandCategory]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteCustomerBrandCategory(DbConnector parDbConnector)
        {
            parDbConnector.ExecuteNonQuery("DeleteCustomerBrandCategory", null);
        }

        /// <summary>
        /// Method that deletes one row of data in table [CustomerBrandCategory] based on the passed entity object
        /// </summary>
        /// <param name="parEntCustomerBrandCategory">Gets entity object as parameter for table CustomerBrandCategory]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteCustomerBrandCategoryById(entCustomerBrandCategory parEntCustomerBrandCategory, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntCustomerBrandCategory.Id);
            parDbConnector.ExecuteNonQuery("DeleteCustomerBrandCategoryById", insDbParamCollection);
        }
        #endregion
        #region Custom Methods
        public DataTable SelectCustomerBrandCategoryByCustomerBrandId(entCustomerBrandCategory insEntCustomerBrandCategory, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pCustomerBrandId", insEntCustomerBrandCategory.CustomerBrandId);
            return insDbConnector.ExecuteDataTable("SelectCustomerBrandCategoryByCustomerBrandId", insDbParamCollection);
        }

        public DataTable SelectCustomerBrandCategoryByParentId(entCustomerBrandCategory insEnt, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pParentId", insEnt.ParentId);
            return insDbConnector.ExecuteDataTable("SelectCustomerBrandCategoryByParentId", insDbParamCollection);
        }

        public DataTable SelectCustomerBrandCategoryByCustomerBrandIdParentIdNull(entCustomerBrandCategory insEntCustomerBrandCategory, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pCustomerBrandId", insEntCustomerBrandCategory.CustomerBrandId);
            return insDbConnector.ExecuteDataTable("SelectCustomerBrandCategoryByCustomerBrandIdParentIdNull", insDbParamCollection);
        }
        #endregion
    }
}