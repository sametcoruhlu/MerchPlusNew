using System;
using System.Collections.Generic;
using System.Text;
using SqlHelper;
using System.Data;
using entMerchPlus;

namespace datMerchPlus
{
    /// <summary>
    /// The class that is generated hold parameters to manage database table [CustomerProductRetailShop]
    /// </summary>
    public class datCustomerProductRetailShop
    {
        /// <summary>
        /// datCustomerProductRetailShop Constructor method used while taking an instance of this class.
        /// </summary>
        public datCustomerProductRetailShop()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Method that selects all data stored in table [CustomerProductRetailShop]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public DataTable SelectCustomerProductRetailShop(DbConnector parDbConnector)
        {
            return parDbConnector.ExecuteDataTable("SelectCustomerProductRetailShop", null);
        }

        /// <summary>
        /// Method that selects one row of data in table [CustomerProductRetailShop] filtered by the PK field of the inbound entity object
        /// </summary>
        /// <param name="parEntCustomerProductRetailShop">Entity object as parameter for tableCustomerProductRetailShop]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void SelectCustomerProductRetailShopById(entCustomerProductRetailShop parEntCustomerProductRetailShop, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntCustomerProductRetailShop.Id);
            DataTable insDataTable = new DataTable();
            insDataTable = parDbConnector.ExecuteDataTable("SelectCustomerProductRetailShopById", insDbParamCollection);
            if (insDataTable.Rows.Count > 0)
            {
                if (insDataTable.Rows[0]["Id"] != DBNull.Value)
                {
                    parEntCustomerProductRetailShop.Id = Convert.ToInt32(insDataTable.Rows[0]["Id"]);
                }
                if (insDataTable.Rows[0]["CustomerId"] != DBNull.Value)
                {
                    parEntCustomerProductRetailShop.CustomerId = Convert.ToInt32(insDataTable.Rows[0]["CustomerId"]);
                }
                if (insDataTable.Rows[0]["CustomerProductId"] != DBNull.Value)
                {
                    parEntCustomerProductRetailShop.CustomerProductId = Convert.ToInt32(insDataTable.Rows[0]["CustomerProductId"]);
                }
                if (insDataTable.Rows[0]["RetailShopId"] != DBNull.Value)
                {
                    parEntCustomerProductRetailShop.RetailShopId = Convert.ToInt32(insDataTable.Rows[0]["RetailShopId"]);
                }
                if (insDataTable.Rows[0]["CreatedOn"] != DBNull.Value)
                {
                    parEntCustomerProductRetailShop.CreatedOn = Convert.ToDateTime(insDataTable.Rows[0]["CreatedOn"]);
                }
                if (insDataTable.Rows[0]["CreatedBy"] != DBNull.Value)
                {
                    parEntCustomerProductRetailShop.CreatedBy = Convert.ToString(insDataTable.Rows[0]["CreatedBy"]);
                }
            }
        }

        /// <summary>
        /// Method that inserts one row of data into table [CustomerProductRetailShop] based on the values of input entity object
        /// </summary>
        /// <param name="parEntCustomerProductRetailShop">Entity object as parameter for tableCustomerProductRetailShop]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void InsertCustomerProductRetailShop(entCustomerProductRetailShop parEntCustomerProductRetailShop, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.AddOutput("@pId", DbType.Int32);
            insDbParamCollection.Add("@pCustomerId", parEntCustomerProductRetailShop.CustomerId);
            insDbParamCollection.Add("@pCustomerProductId", parEntCustomerProductRetailShop.CustomerProductId);
            insDbParamCollection.Add("@pRetailShopId", parEntCustomerProductRetailShop.RetailShopId);
            insDbParamCollection.Add("@pCreatedOn", parEntCustomerProductRetailShop.CreatedOn);
            insDbParamCollection.Add("@pCreatedBy", parEntCustomerProductRetailShop.CreatedBy);
            parDbConnector.ExecuteNonQuery("InsertCustomerProductRetailShop", insDbParamCollection);
            parEntCustomerProductRetailShop.Id = Convert.ToInt32(insDbParamCollection.GetOutPutParameter().Value);
        }

        /// <summary>
        /// Method that updates one row of data in table [CustomerProductRetailShop] based on the values of input entity object
        /// </summary>
        /// <param name="parEntCustomerProductRetailShop">Entity object as parameter for tableCustomerProductRetailShop]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void UpdateCustomerProductRetailShopById(entCustomerProductRetailShop parEntCustomerProductRetailShop, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntCustomerProductRetailShop.Id);
            insDbParamCollection.Add("@pCustomerId", parEntCustomerProductRetailShop.CustomerId);
            insDbParamCollection.Add("@pCustomerProductId", parEntCustomerProductRetailShop.CustomerProductId);
            insDbParamCollection.Add("@pRetailShopId", parEntCustomerProductRetailShop.RetailShopId);
            insDbParamCollection.Add("@pCreatedOn", parEntCustomerProductRetailShop.CreatedOn);
            insDbParamCollection.Add("@pCreatedBy", parEntCustomerProductRetailShop.CreatedBy);
            parDbConnector.ExecuteNonQuery("UpdateCustomerProductRetailShopById", insDbParamCollection);
        }

        /// <summary>
        /// Method that deletes all data in table [CustomerProductRetailShop]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteCustomerProductRetailShop(DbConnector parDbConnector)
        {
            parDbConnector.ExecuteNonQuery("DeleteCustomerProductRetailShop", null);
        }

        /// <summary>
        /// Method that deletes one row of data in table [CustomerProductRetailShop] based on the passed entity object
        /// </summary>
        /// <param name="parEntCustomerProductRetailShop">Gets entity object as parameter for table CustomerProductRetailShop]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteCustomerProductRetailShopById(entCustomerProductRetailShop parEntCustomerProductRetailShop, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntCustomerProductRetailShop.Id);
            parDbConnector.ExecuteNonQuery("DeleteCustomerProductRetailShopById", insDbParamCollection);
        }

        #endregion
        #region Custom Methods
        public DataTable SelectCustomerProductRetailShopByRetailShopId(entCustomerProductRetailShop insEntCustomerProductRetailShop, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pRetailShopId", insEntCustomerProductRetailShop.RetailShopId);
            return insDbConnector.ExecuteDataTable("SelectCustomerProductRetailShopByRetailShopId", insDbParamCollection);
        }

        public DataTable SelectCustomerProductRetailShopByRetailShopIdCustomerId(entCustomerProductRetailShop insEntCustomerProductRetailShop, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pRetailShopId", insEntCustomerProductRetailShop.RetailShopId);
            insDbParamCollection.Add("@pCustomerId", insEntCustomerProductRetailShop.CustomerId);
            return insDbConnector.ExecuteDataTable("SelectCustomerProductRetailShopByRetailShopIdCustomerId", insDbParamCollection);
        }

        public void SelectCustomerProductRetailShopByCustomerIdCustomerProductIdRetailShopId(entCustomerProductRetailShop parEntCustomerProductRetailShop, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pCustomerId", parEntCustomerProductRetailShop.CustomerId);
            insDbParamCollection.Add("@pCustomerProductId", parEntCustomerProductRetailShop.CustomerProductId);
            insDbParamCollection.Add("@pRetailShopId", parEntCustomerProductRetailShop.RetailShopId);
            DataTable insDataTable = new DataTable();
            insDataTable = parDbConnector.ExecuteDataTable("SelectCustomerProductRetailShopByCustomerIdCustomerProductIdRetailShopId", insDbParamCollection);
            if (insDataTable.Rows.Count > 0)
            {
                if (insDataTable.Rows[0]["Id"] != DBNull.Value)
                {
                    parEntCustomerProductRetailShop.Id = Convert.ToInt32(insDataTable.Rows[0]["Id"]);
                }
                if (insDataTable.Rows[0]["CustomerId"] != DBNull.Value)
                {
                    parEntCustomerProductRetailShop.CustomerId = Convert.ToInt32(insDataTable.Rows[0]["CustomerId"]);
                }
                if (insDataTable.Rows[0]["CustomerProductId"] != DBNull.Value)
                {
                    parEntCustomerProductRetailShop.CustomerProductId = Convert.ToInt32(insDataTable.Rows[0]["CustomerProductId"]);
                }
                if (insDataTable.Rows[0]["RetailShopId"] != DBNull.Value)
                {
                    parEntCustomerProductRetailShop.RetailShopId = Convert.ToInt32(insDataTable.Rows[0]["RetailShopId"]);
                }
                if (insDataTable.Rows[0]["CreatedOn"] != DBNull.Value)
                {
                    parEntCustomerProductRetailShop.CreatedOn = Convert.ToDateTime(insDataTable.Rows[0]["CreatedOn"]);
                }
                if (insDataTable.Rows[0]["CreatedBy"] != DBNull.Value)
                {
                    parEntCustomerProductRetailShop.CreatedBy = Convert.ToString(insDataTable.Rows[0]["CreatedBy"]);
                }
            }
        }

        public DataTable SelectCustomerProductRetailShopByRetailChannelId(int retailCategoryId, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pRetailCategoryId", retailCategoryId);
            return insDbConnector.ExecuteDataTable("SelectCustomerProductRetailShopByRetailChannelId", insDbParamCollection);
        }

        public void DeleteCustomerProductRetailShopByRetailShopIdCustomerId(entCustomerProductRetailShop insEntCustomerProductRetailShop_Delete, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pCustomerId", insEntCustomerProductRetailShop_Delete.CustomerId);
            insDbParamCollection.Add("@pRetailShopId", insEntCustomerProductRetailShop_Delete.RetailShopId);
            insDbConnector.ExecuteNonQuery("DeleteCustomerProductRetailShopByRetailShopIdCustomerId", insDbParamCollection);

        }

        public DataTable SelectCustomerProductRetailShopByRetailCategoryId(int retailCategoryId, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pRetailCategoryId", retailCategoryId);
            return insDbConnector.ExecuteDataTable("SelectCustomerProductRetailShopByRetailCategoryId", insDbParamCollection);
        }

        public void DeleteCustomerProductRetailShopByCustomerIdRetailCategoryId(int customerId, int retailCategoryId, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pCustomerId", customerId);
            insDbParamCollection.Add("@pRetailCategoryId", retailCategoryId);
            insDbConnector.ExecuteNonQuery("DeleteCustomerProductRetailShopByCustomerIdRetailCategoryId", insDbParamCollection);
        }

        public DataTable SelectCustomerProductRetailShopByRetailId(int retailId, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pRetailId", retailId);
            return insDbConnector.ExecuteDataTable("SelectCustomerProductRetailShopByRetailId", insDbParamCollection);
        }

        public void DeleteCustomerProductRetailShopByCustomerIdRetailId(int customerId, int retailId, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pCustomerId", customerId);
            insDbParamCollection.Add("@pRetailId", retailId);
            insDbConnector.ExecuteNonQuery("DeleteCustomerProductRetailShopByCustomerIdRetailId", insDbParamCollection);
        }

        public void DeleteCustomerProductRetailShopByCustomerIdRetailChannelId(int customerId, int retailCategoryId, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pCustomerId", customerId);
            insDbParamCollection.Add("@pRetailCategoryId", retailCategoryId);
            insDbConnector.ExecuteNonQuery("DeleteCustomerProductRetailShopByCustomerIdRetailChannelId", insDbParamCollection);
        }

        public DataTable SelectCustomerProductRetailShopByRetailShopCategoryIdOnlyProducts(int retailCategoryId, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pRetailCategoryId", retailCategoryId);
            return insDbConnector.ExecuteDataTable("SelectCustomerProductRetailShopByRetailShopCategoryIdOnlyProducts", insDbParamCollection);
        }
        #endregion
    }
}
