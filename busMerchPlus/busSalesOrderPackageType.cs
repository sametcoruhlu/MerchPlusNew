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
    /// Manages transactions on table [SalesOrderPackageType]
    /// </summary>
    public class busSalesOrderPackageType : BusBase
    {
        /// <summary>
        /// busSalesOrderPackageTypeConstructor method used while taking an instance of this class.
        /// </summary>
        public busSalesOrderPackageType() : base()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Selects all data stored in table [SalesOrderPackageType]
        /// </summary>
        public DataTable SelectSalesOrderPackageType()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datSalesOrderPackageType insDatSalesOrderPackageType = new datSalesOrderPackageType();
                return insDatSalesOrderPackageType.SelectSalesOrderPackageType(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Selects one row of data stored in table [SalesOrderPackageType] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntSalesOrderPackageType">Gets entity object as parameter for table SalesOrderPackageType]</param>
        public void SelectSalesOrderPackageTypeById(entSalesOrderPackageType parEntSalesOrderPackageType)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datSalesOrderPackageType insDatSalesOrderPackageType = new datSalesOrderPackageType();
                insDatSalesOrderPackageType.SelectSalesOrderPackageTypeById(parEntSalesOrderPackageType, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Inserts one row of data into table [SalesOrderPackageType] based on the values of input entity object
        /// </summary>
        /// <param name="parEntSalesOrderPackageType">Gets entity object as parameter for table SalesOrderPackageType]</param>
        public void InsertSalesOrderPackageType(entSalesOrderPackageType parEntSalesOrderPackageType)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datSalesOrderPackageType insDatSalesOrderPackageType = new datSalesOrderPackageType();
                insDatSalesOrderPackageType.InsertSalesOrderPackageType(parEntSalesOrderPackageType, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Updates one row of data in table [SalesOrderPackageType] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntSalesOrderPackageType">Gets entity object as parameter for table SalesOrderPackageType]</param>
        public void UpdateSalesOrderPackageTypeById(entSalesOrderPackageType parEntSalesOrderPackageType)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datSalesOrderPackageType insDatSalesOrderPackageType = new datSalesOrderPackageType();
                insDatSalesOrderPackageType.UpdateSalesOrderPackageTypeById(parEntSalesOrderPackageType, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes all data stored in table [SalesOrderPackageType]
        /// </summary>
        public void DeleteSalesOrderPackageType()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datSalesOrderPackageType insDatSalesOrderPackageType = new datSalesOrderPackageType();
                insDatSalesOrderPackageType.DeleteSalesOrderPackageType(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes data stored in table [SalesOrderPackageType] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntSalesOrderPackageType">Gets entity object as parameter for table SalesOrderPackageType]</param>
        public void DeleteSalesOrderPackageTypeById(entSalesOrderPackageType parEntSalesOrderPackageType)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datSalesOrderPackageType insDatSalesOrderPackageType = new datSalesOrderPackageType();
                insDatSalesOrderPackageType.DeleteSalesOrderPackageTypeById(parEntSalesOrderPackageType, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        #endregion
        #region Custom Methods
        #endregion
    }
}