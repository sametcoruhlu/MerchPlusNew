using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using SqlHelper;
using System.Data;
using entMerchPlus;
using datMerchPlus;
using Infrastructure;

namespace busMerchPlus
{
    /// <summary>
    /// This layer is responsible for implementing business logic.
    /// Manages transactions on table [SalesOrder]
    /// </summary>
    public class busSalesOrder : BusBase
    {
        /// <summary>
        /// busSalesOrderConstructor method used while taking an instance of this class.
        /// </summary>
        public busSalesOrder() : base()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Selects all data stored in table [SalesOrder]
        /// </summary>
        public DataTable SelectSalesOrder()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datSalesOrder insDatSalesOrder = new datSalesOrder();
                return insDatSalesOrder.SelectSalesOrder(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Selects one row of data stored in table [SalesOrder] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntSalesOrder">Gets entity object as parameter for table SalesOrder]</param>
        public void SelectSalesOrderById(entSalesOrder parEntSalesOrder)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datSalesOrder insDatSalesOrder = new datSalesOrder();
                insDatSalesOrder.SelectSalesOrderById(parEntSalesOrder, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Inserts one row of data into table [SalesOrder] based on the values of input entity object
        /// </summary>
        /// <param name="parEntSalesOrder">Gets entity object as parameter for table SalesOrder]</param>
        public void InsertSalesOrder(entSalesOrder parEntSalesOrder)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datSalesOrder insDatSalesOrder = new datSalesOrder();
                insDatSalesOrder.InsertSalesOrder(parEntSalesOrder, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Updates one row of data in table [SalesOrder] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntSalesOrder">Gets entity object as parameter for table SalesOrder]</param>
        public void UpdateSalesOrderById(entSalesOrder parEntSalesOrder)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datSalesOrder insDatSalesOrder = new datSalesOrder();
                insDatSalesOrder.UpdateSalesOrderById(parEntSalesOrder, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes all data stored in table [SalesOrder]
        /// </summary>
        public void DeleteSalesOrder()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datSalesOrder insDatSalesOrder = new datSalesOrder();
                insDatSalesOrder.DeleteSalesOrder(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes data stored in table [SalesOrder] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntSalesOrder">Gets entity object as parameter for table SalesOrder]</param>
        public void DeleteSalesOrderById(entSalesOrder parEntSalesOrder)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datSalesOrder insDatSalesOrder = new datSalesOrder();
                insDatSalesOrder.DeleteSalesOrderById(parEntSalesOrder, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        #endregion
        #region Custom Methods
        public DataTable SelectSalesOrderByStartDateEndDateCustomerIdGridData(DateTime startDateTime, DateTime endDateTime, int customerId)
        {
            DbConnector insDbConnector = new DbConnector();
            datSalesOrder insDatSalesOrder = new datSalesOrder();
            try
            {
                return insDatSalesOrder.SelectSalesOrderByStartDateEndDateCustomerIdGridData(startDateTime, endDateTime, customerId, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public DataTable SelectSalesOrderByIdGridData(entSalesOrder insEntSalesOrder)
        {
            DbConnector insDbConnector = new DbConnector();
            datSalesOrder insDatSalesOrder = new datSalesOrder();
            try
            {
                return insDatSalesOrder.SelectSalesOrderByIdGridData(insEntSalesOrder, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }
        #endregion
    }
}