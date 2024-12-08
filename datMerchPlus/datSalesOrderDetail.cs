using System;
using System.Collections.Generic;
using System.Text;
using SqlHelper;
using System.Data;
using entMerchPlus;

namespace datMerchPlus
{
    /// <summary>
    /// The class that is generated hold parameters to manage database table [SalesOrderDetail]
    /// </summary>
    public class datSalesOrderDetail
    {
        /// <summary>
        /// datSalesOrderDetail Constructor method used while taking an instance of this class.
        /// </summary>
        public datSalesOrderDetail()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Method that selects all data stored in table [SalesOrderDetail]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public DataTable SelectSalesOrderDetail(DbConnector parDbConnector)
        {
            return parDbConnector.ExecuteDataTable("SelectSalesOrderDetail", null);
        }

        /// <summary>
        /// Method that selects one row of data in table [SalesOrderDetail] filtered by the PK field of the inbound entity object
        /// </summary>
        /// <param name="parEntSalesOrderDetail">Entity object as parameter for tableSalesOrderDetail]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void SelectSalesOrderDetailById(entSalesOrderDetail parEntSalesOrderDetail, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntSalesOrderDetail.Id);
            DataTable insDataTable = new DataTable();
            insDataTable = parDbConnector.ExecuteDataTable("SelectSalesOrderDetailById", insDbParamCollection);
            if (insDataTable.Rows.Count > 0)
            {
                if (insDataTable.Rows[0]["Id"] != DBNull.Value)
                {
                    parEntSalesOrderDetail.Id = Convert.ToInt32(insDataTable.Rows[0]["Id"]);
                }
                if (insDataTable.Rows[0]["SalesOrderId"] != DBNull.Value)
                {
                    parEntSalesOrderDetail.SalesOrderId = Convert.ToInt32(insDataTable.Rows[0]["SalesOrderId"]);
                }
                if (insDataTable.Rows[0]["CustomerProductId"] != DBNull.Value)
                {
                    parEntSalesOrderDetail.CustomerProductId = Convert.ToInt32(insDataTable.Rows[0]["CustomerProductId"]);
                }
                if (insDataTable.Rows[0]["Quantity"] != DBNull.Value)
                {
                    parEntSalesOrderDetail.Quantity = Convert.ToInt32(insDataTable.Rows[0]["Quantity"]);
                }
                if (insDataTable.Rows[0]["SalesOrderPackageTypeId"] != DBNull.Value)
                {
                    parEntSalesOrderDetail.SalesOrderPackageTypeId = Convert.ToInt32(insDataTable.Rows[0]["SalesOrderPackageTypeId"]);
                }
                if (insDataTable.Rows[0]["Description"] != DBNull.Value)
                {
                    parEntSalesOrderDetail.Description = Convert.ToString(insDataTable.Rows[0]["Description"]);
                }
                if (insDataTable.Rows[0]["CreatedOn"] != DBNull.Value)
                {
                    parEntSalesOrderDetail.CreatedOn = Convert.ToDateTime(insDataTable.Rows[0]["CreatedOn"]);
                }
                if (insDataTable.Rows[0]["CreatedBy"] != DBNull.Value)
                {
                    parEntSalesOrderDetail.CreatedBy = Convert.ToString(insDataTable.Rows[0]["CreatedBy"]);
                }
            }
        }

        /// <summary>
        /// Method that inserts one row of data into table [SalesOrderDetail] based on the values of input entity object
        /// </summary>
        /// <param name="parEntSalesOrderDetail">Entity object as parameter for tableSalesOrderDetail]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void InsertSalesOrderDetail(entSalesOrderDetail parEntSalesOrderDetail, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.AddOutput("@pId", DbType.Int32);
            insDbParamCollection.Add("@pSalesOrderId", parEntSalesOrderDetail.SalesOrderId);
            insDbParamCollection.Add("@pCustomerProductId", parEntSalesOrderDetail.CustomerProductId);
            insDbParamCollection.Add("@pQuantity", parEntSalesOrderDetail.Quantity);
            insDbParamCollection.Add("@pSalesOrderPackageTypeId", parEntSalesOrderDetail.SalesOrderPackageTypeId);
            insDbParamCollection.Add("@pDescription", parEntSalesOrderDetail.Description);
            insDbParamCollection.Add("@pCreatedOn", parEntSalesOrderDetail.CreatedOn);
            insDbParamCollection.Add("@pCreatedBy", parEntSalesOrderDetail.CreatedBy);
            parDbConnector.ExecuteNonQuery("InsertSalesOrderDetail", insDbParamCollection);
            parEntSalesOrderDetail.Id = Convert.ToInt32(insDbParamCollection.GetOutPutParameter().Value);
        }

        /// <summary>
        /// Method that updates one row of data in table [SalesOrderDetail] based on the values of input entity object
        /// </summary>
        /// <param name="parEntSalesOrderDetail">Entity object as parameter for tableSalesOrderDetail]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void UpdateSalesOrderDetailById(entSalesOrderDetail parEntSalesOrderDetail, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntSalesOrderDetail.Id);
            insDbParamCollection.Add("@pSalesOrderId", parEntSalesOrderDetail.SalesOrderId);
            insDbParamCollection.Add("@pCustomerProductId", parEntSalesOrderDetail.CustomerProductId);
            insDbParamCollection.Add("@pQuantity", parEntSalesOrderDetail.Quantity);
            insDbParamCollection.Add("@pSalesOrderPackageTypeId", parEntSalesOrderDetail.SalesOrderPackageTypeId);
            insDbParamCollection.Add("@pDescription", parEntSalesOrderDetail.Description);
            insDbParamCollection.Add("@pCreatedOn", parEntSalesOrderDetail.CreatedOn);
            insDbParamCollection.Add("@pCreatedBy", parEntSalesOrderDetail.CreatedBy);
            parDbConnector.ExecuteNonQuery("UpdateSalesOrderDetailById", insDbParamCollection);
        }

        /// <summary>
        /// Method that deletes all data in table [SalesOrderDetail]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteSalesOrderDetail(DbConnector parDbConnector)
        {
            parDbConnector.ExecuteNonQuery("DeleteSalesOrderDetail", null);
        }

        /// <summary>
        /// Method that deletes one row of data in table [SalesOrderDetail] based on the passed entity object
        /// </summary>
        /// <param name="parEntSalesOrderDetail">Gets entity object as parameter for table SalesOrderDetail]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteSalesOrderDetailById(entSalesOrderDetail parEntSalesOrderDetail, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntSalesOrderDetail.Id);
            parDbConnector.ExecuteNonQuery("DeleteSalesOrderDetailById", insDbParamCollection);
        }

        #endregion
        #region Custom Methods
        public DataTable SelectSalesOrderDetailBySalesOrderIdGridData(entSalesOrderDetail insEntSalesOrderDetail, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pSalesOrderId", insEntSalesOrderDetail.SalesOrderId);
            return insDbConnector.ExecuteDataTable("SelectSalesOrderDetailBySalesOrderIdGridData", insDbParamCollection);
        }

        public void DeleteSalesOrderDetailByCustomerProductId(entSalesOrderDetail insEntSalesOrderDetail_Delete, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pCustomerProductId", insEntSalesOrderDetail_Delete.CustomerProductId);
            insDbConnector.ExecuteNonQuery("DeleteSalesOrderDetailByCustomerProductId", insDbParamCollection);
        }
        #endregion
    }
}