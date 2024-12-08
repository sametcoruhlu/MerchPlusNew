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
    /// Manages transactions on table [CustomerProductCompetitor]
    /// </summary>
    public class busCustomerProductCompetitor : BusBase
    {
        /// <summary>
        /// busCustomerProductCompetitorConstructor method used while taking an instance of this class.
        /// </summary>
        public busCustomerProductCompetitor() : base()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Selects all data stored in table [CustomerProductCompetitor]
        /// </summary>
        public DataTable SelectCustomerProductCompetitor()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomerProductCompetitor insDatCustomerProductCompetitor = new datCustomerProductCompetitor();
                return insDatCustomerProductCompetitor.SelectCustomerProductCompetitor(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Selects one row of data stored in table [CustomerProductCompetitor] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntCustomerProductCompetitor">Gets entity object as parameter for table CustomerProductCompetitor]</param>
        public void SelectCustomerProductCompetitorById(entCustomerProductCompetitor parEntCustomerProductCompetitor)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomerProductCompetitor insDatCustomerProductCompetitor = new datCustomerProductCompetitor();
                insDatCustomerProductCompetitor.SelectCustomerProductCompetitorById(parEntCustomerProductCompetitor, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Inserts one row of data into table [CustomerProductCompetitor] based on the values of input entity object
        /// </summary>
        /// <param name="parEntCustomerProductCompetitor">Gets entity object as parameter for table CustomerProductCompetitor]</param>
        public void InsertCustomerProductCompetitor(entCustomerProductCompetitor parEntCustomerProductCompetitor)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomerProductCompetitor insDatCustomerProductCompetitor = new datCustomerProductCompetitor();
                insDatCustomerProductCompetitor.InsertCustomerProductCompetitor(parEntCustomerProductCompetitor, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Updates one row of data in table [CustomerProductCompetitor] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntCustomerProductCompetitor">Gets entity object as parameter for table CustomerProductCompetitor]</param>
        public void UpdateCustomerProductCompetitorById(entCustomerProductCompetitor parEntCustomerProductCompetitor)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomerProductCompetitor insDatCustomerProductCompetitor = new datCustomerProductCompetitor();
                insDatCustomerProductCompetitor.UpdateCustomerProductCompetitorById(parEntCustomerProductCompetitor, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes all data stored in table [CustomerProductCompetitor]
        /// </summary>
        public void DeleteCustomerProductCompetitor()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomerProductCompetitor insDatCustomerProductCompetitor = new datCustomerProductCompetitor();
                insDatCustomerProductCompetitor.DeleteCustomerProductCompetitor(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes data stored in table [CustomerProductCompetitor] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntCustomerProductCompetitor">Gets entity object as parameter for table CustomerProductCompetitor]</param>
        public void DeleteCustomerProductCompetitorById(entCustomerProductCompetitor parEntCustomerProductCompetitor)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomerProductCompetitor insDatCustomerProductCompetitor = new datCustomerProductCompetitor();
                insDatCustomerProductCompetitor.DeleteCustomerProductCompetitorById(parEntCustomerProductCompetitor, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        #endregion
        #region Custom Methods
        public DataTable SelectCustomerProductCompetitorByCustomerIdGridData(int customerId)
        {
            DbConnector insDbConnector = new DbConnector();
            datCustomerProductCompetitor insDatCustomerProductComepetitor = new datCustomerProductCompetitor();
            try
            {
                return insDatCustomerProductComepetitor.SelectCustomerProductCompetitorByCustomerIdGridData(customerId, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public DataTable SelectCustomerProductCompetitorByCustomerId(int customerId)
        {
            DbConnector insDbConnector = new DbConnector();
            datCustomerProductCompetitor insDatCustomerProductComepetitor = new datCustomerProductCompetitor();
            try
            {
                return insDatCustomerProductComepetitor.SelectCustomerProductCompetitorByCustomerId(customerId, insDbConnector);
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