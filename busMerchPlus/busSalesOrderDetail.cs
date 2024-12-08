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
    /// Manages transactions on table [SalesOrderDetail]
    /// </summary>
    public class busSalesOrderDetail : BusBase
    {
        /// <summary>
        /// busSalesOrderDetailConstructor method used while taking an instance of this class.
        /// </summary>
        public busSalesOrderDetail() : base()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Selects all data stored in table [SalesOrderDetail]
        /// </summary>
        public DataTable SelectSalesOrderDetail()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datSalesOrderDetail insDatSalesOrderDetail = new datSalesOrderDetail();
                return insDatSalesOrderDetail.SelectSalesOrderDetail(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Selects one row of data stored in table [SalesOrderDetail] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntSalesOrderDetail">Gets entity object as parameter for table SalesOrderDetail]</param>
        public void SelectSalesOrderDetailById(entSalesOrderDetail parEntSalesOrderDetail)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datSalesOrderDetail insDatSalesOrderDetail = new datSalesOrderDetail();
                insDatSalesOrderDetail.SelectSalesOrderDetailById(parEntSalesOrderDetail, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Inserts one row of data into table [SalesOrderDetail] based on the values of input entity object
        /// </summary>
        /// <param name="parEntSalesOrderDetail">Gets entity object as parameter for table SalesOrderDetail]</param>
        public void InsertSalesOrderDetail(entSalesOrderDetail parEntSalesOrderDetail)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datSalesOrderDetail insDatSalesOrderDetail = new datSalesOrderDetail();
                insDatSalesOrderDetail.InsertSalesOrderDetail(parEntSalesOrderDetail, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Updates one row of data in table [SalesOrderDetail] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntSalesOrderDetail">Gets entity object as parameter for table SalesOrderDetail]</param>
        public void UpdateSalesOrderDetailById(entSalesOrderDetail parEntSalesOrderDetail)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datSalesOrderDetail insDatSalesOrderDetail = new datSalesOrderDetail();
                insDatSalesOrderDetail.UpdateSalesOrderDetailById(parEntSalesOrderDetail, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }


        /// <summary>
        /// Deletes all data stored in table [SalesOrderDetail]
        /// </summary>
        public void DeleteSalesOrderDetail()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datSalesOrderDetail insDatSalesOrderDetail = new datSalesOrderDetail();
                insDatSalesOrderDetail.DeleteSalesOrderDetail(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes data stored in table [SalesOrderDetail] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntSalesOrderDetail">Gets entity object as parameter for table SalesOrderDetail]</param>
        public void DeleteSalesOrderDetailById(entSalesOrderDetail parEntSalesOrderDetail)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datSalesOrderDetail insDatSalesOrderDetail = new datSalesOrderDetail();
                insDatSalesOrderDetail.DeleteSalesOrderDetailById(parEntSalesOrderDetail, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        #endregion
        #region Custom Methods
        public DataTable SelectSalesOrderDetailBySalesOrderIdGridData(entSalesOrderDetail insEntSalesOrderDetail)
        {
            DbConnector insDbConnector = new DbConnector();
            datSalesOrderDetail insDatSalesOrderDetail = new datSalesOrderDetail();
            try
            {
                return insDatSalesOrderDetail.SelectSalesOrderDetailBySalesOrderIdGridData(insEntSalesOrderDetail, insDbConnector);
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