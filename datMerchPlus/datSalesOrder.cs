using System;
using System.Collections.Generic;
using System.Text;
using SqlHelper;
using System.Data;
using entMerchPlus;

namespace datMerchPlus
{
    /// <summary>
    /// The class that is generated hold parameters to manage database table [SalesOrder]
    /// </summary>
    public class datSalesOrder
    {
        /// <summary>
        /// datSalesOrder Constructor method used while taking an instance of this class.
        /// </summary>
        public datSalesOrder()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Method that selects all data stored in table [SalesOrder]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public DataTable SelectSalesOrder(DbConnector parDbConnector)
        {
            return parDbConnector.ExecuteDataTable("SelectSalesOrder", null);
        }

        /// <summary>
        /// Method that selects one row of data in table [SalesOrder] filtered by the PK field of the inbound entity object
        /// </summary>
        /// <param name="parEntSalesOrder">Entity object as parameter for tableSalesOrder]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void SelectSalesOrderById(entSalesOrder parEntSalesOrder, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntSalesOrder.Id);
            DataTable insDataTable = new DataTable();
            insDataTable = parDbConnector.ExecuteDataTable("SelectSalesOrderById", insDbParamCollection);
            if (insDataTable.Rows.Count > 0)
            {
                if (insDataTable.Rows[0]["Id"] != DBNull.Value)
                {
                    parEntSalesOrder.Id = Convert.ToInt32(insDataTable.Rows[0]["Id"]);
                }
                if (insDataTable.Rows[0]["CustomerId"] != DBNull.Value)
                {
                    parEntSalesOrder.CustomerId = Convert.ToInt32(insDataTable.Rows[0]["CustomerId"]);
                }
                if (insDataTable.Rows[0]["MemberRouteId"] != DBNull.Value)
                {
                    parEntSalesOrder.MemberRouteId = Convert.ToInt32(insDataTable.Rows[0]["MemberRouteId"]);
                }
                if (insDataTable.Rows[0]["SalesOrderStatusId"] != DBNull.Value)
                {
                    parEntSalesOrder.SalesOrderStatusId = Convert.ToInt32(insDataTable.Rows[0]["SalesOrderStatusId"]);
                }
                if (insDataTable.Rows[0]["CreatedBy"] != DBNull.Value)
                {
                    parEntSalesOrder.CreatedBy = Convert.ToString(insDataTable.Rows[0]["CreatedBy"]);
                }
                if (insDataTable.Rows[0]["CreatedOn"] != DBNull.Value)
                {
                    parEntSalesOrder.CreatedOn = Convert.ToDateTime(insDataTable.Rows[0]["CreatedOn"]);
                }
                if (insDataTable.Rows[0]["ShippedOn"] != DBNull.Value)
                {
                    parEntSalesOrder.ShippedOn = Convert.ToDateTime(insDataTable.Rows[0]["ShippedOn"]);
                }
                if (insDataTable.Rows[0]["ShippedBy"] != DBNull.Value)
                {
                    parEntSalesOrder.ShippedBy = Convert.ToString(insDataTable.Rows[0]["ShippedBy"]);
                }
                if (insDataTable.Rows[0]["WaybillNumber"] != DBNull.Value)
                {
                    parEntSalesOrder.WaybillNumber = Convert.ToString(insDataTable.Rows[0]["WaybillNumber"]);
                }
            }
        }

        /// <summary>
        /// Method that inserts one row of data into table [SalesOrder] based on the values of input entity object
        /// </summary>
        /// <param name="parEntSalesOrder">Entity object as parameter for tableSalesOrder]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void InsertSalesOrder(entSalesOrder parEntSalesOrder, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.AddOutput("@pId", DbType.Int32);
            insDbParamCollection.Add("@pCustomerId", parEntSalesOrder.CustomerId);
            insDbParamCollection.Add("@pMemberRouteId", parEntSalesOrder.MemberRouteId);
            insDbParamCollection.Add("@pSalesOrderStatusId", parEntSalesOrder.SalesOrderStatusId);
            insDbParamCollection.Add("@pCreatedBy", parEntSalesOrder.CreatedBy);
            insDbParamCollection.Add("@pCreatedOn", parEntSalesOrder.CreatedOn);
            insDbParamCollection.Add("@pShippedOn", parEntSalesOrder.ShippedOn);
            insDbParamCollection.Add("@pShippedBy", parEntSalesOrder.ShippedBy);
            insDbParamCollection.Add("@pWaybillNumber", parEntSalesOrder.WaybillNumber);
            parDbConnector.ExecuteNonQuery("InsertSalesOrder", insDbParamCollection);
            parEntSalesOrder.Id = Convert.ToInt32(insDbParamCollection.GetOutPutParameter().Value);
        }

        /// <summary>
        /// Method that updates one row of data in table [SalesOrder] based on the values of input entity object
        /// </summary>
        /// <param name="parEntSalesOrder">Entity object as parameter for tableSalesOrder]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void UpdateSalesOrderById(entSalesOrder parEntSalesOrder, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntSalesOrder.Id);
            insDbParamCollection.Add("@pCustomerId", parEntSalesOrder.CustomerId);
            insDbParamCollection.Add("@pMemberRouteId", parEntSalesOrder.MemberRouteId);
            insDbParamCollection.Add("@pSalesOrderStatusId", parEntSalesOrder.SalesOrderStatusId);
            insDbParamCollection.Add("@pCreatedBy", parEntSalesOrder.CreatedBy);
            insDbParamCollection.Add("@pCreatedOn", parEntSalesOrder.CreatedOn);
            insDbParamCollection.Add("@pShippedOn", parEntSalesOrder.ShippedOn);
            insDbParamCollection.Add("@pShippedBy", parEntSalesOrder.ShippedBy);
            insDbParamCollection.Add("@pWaybillNumber", parEntSalesOrder.WaybillNumber);
            parDbConnector.ExecuteNonQuery("UpdateSalesOrderById", insDbParamCollection);
        }

        /// <summary>
        /// Method that deletes all data in table [SalesOrder]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteSalesOrder(DbConnector parDbConnector)
        {
            parDbConnector.ExecuteNonQuery("DeleteSalesOrder", null);
        }

        /// <summary>
        /// Method that deletes one row of data in table [SalesOrder] based on the passed entity object
        /// </summary>
        /// <param name="parEntSalesOrder">Gets entity object as parameter for table SalesOrder]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteSalesOrderById(entSalesOrder parEntSalesOrder, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntSalesOrder.Id);
            parDbConnector.ExecuteNonQuery("DeleteSalesOrderById", insDbParamCollection);
        }

        #endregion
        #region Custom Methods
        public DataTable SelectSalesOrderByStartDateEndDateCustomerIdGridData(DateTime startDateTime, DateTime endDateTime, int customerId, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pStartDate", startDateTime);
            insDbParamCollection.Add("@pEndDate", endDateTime);
            insDbParamCollection.Add("@pCustomerId", customerId);
            return insDbConnector.ExecuteDataTable("SelectSalesOrderByStartDateEndDateCustomerIdGridData", insDbParamCollection);
        }

        public DataTable SelectSalesOrderByIdGridData(entSalesOrder insEntSalesOrder, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", insEntSalesOrder.Id);
            return insDbConnector.ExecuteDataTable("SelectSalesOrderByIdGridData", insDbParamCollection);
        }
        #endregion
    }
}