using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using SqlHelper;
using System.Data;
using entMerchPlus;
using datMerchPlus;
using Infrastructure;
using System.Linq;

namespace busMerchPlus
{
    /// <summary>
    /// This layer is responsible for implementing business logic.
    /// Manages transactions on table [CustomerFormFieldVisibility]
    /// </summary>
    public class busCustomerFormFieldVisibility : BusBase
    {
        /// <summary>
        /// busCustomerFormFieldVisibility Constructor method used while taking an instance of this class.
        /// </summary>
        public busCustomerFormFieldVisibility() : base()
        {
        }

        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Selects all data stored in table [CustomerFormFieldVisibility]
        /// </summary>
        public DataTable SelectCustomerFormFieldVisibility()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomerFormFieldVisibility insDatCustomerFormFieldVisibility = new datCustomerFormFieldVisibility();
                return insDatCustomerFormFieldVisibility.SelectAll(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Selects data in table [CustomerFormFieldVisibility] filtered by CustomerId
        /// </summary>
        /// <param name="parCustomerId">CustomerId to filter by</param>
        public DataTable SelectCustomerFormFieldVisibilityByCustomerId(int parCustomerId)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomerFormFieldVisibility insDatCustomerFormFieldVisibility = new datCustomerFormFieldVisibility();
                return insDatCustomerFormFieldVisibility.SelectByCustomerId(parCustomerId, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Selects one row of data stored in table [CustomerFormFieldVisibility] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntCustomerFormFieldVisibility">Gets entity object as parameter for table CustomerFormFieldVisibility]</param>
        public void SelectCustomerFormFieldVisibilityById(entCustomerFormFieldVisibility parEntCustomerFormFieldVisibility)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomerFormFieldVisibility insDatCustomerFormFieldVisibility = new datCustomerFormFieldVisibility();
                insDatCustomerFormFieldVisibility.SelectCustomerFormFieldVisibilityById(parEntCustomerFormFieldVisibility, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Inserts one row of data into table [CustomerFormFieldVisibility] based on the values of input entity object
        /// </summary>
        /// <param name="parEntCustomerFormFieldVisibility">Gets entity object as parameter for table CustomerFormFieldVisibility]</param>
        public void InsertCustomerFormFieldVisibility(entCustomerFormFieldVisibility parEntCustomerFormFieldVisibility)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomerFormFieldVisibility insDatCustomerFormFieldVisibility = new datCustomerFormFieldVisibility();
                insDatCustomerFormFieldVisibility.InsertCustomerFormFieldVisibility(parEntCustomerFormFieldVisibility, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Updates one row of data in table [CustomerFormFieldVisibility] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntCustomerFormFieldVisibility">Gets entity object as parameter for table CustomerFormFieldVisibility]</param>
        public void UpdateCustomerFormFieldVisibilityById(entCustomerFormFieldVisibility parEntCustomerFormFieldVisibility)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomerFormFieldVisibility insDatCustomerFormFieldVisibility = new datCustomerFormFieldVisibility();
                insDatCustomerFormFieldVisibility.UpdateCustomerFormFieldVisibilityById(parEntCustomerFormFieldVisibility, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes one row of data stored in table [CustomerFormFieldVisibility] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntCustomerFormFieldVisibility">Gets entity object as parameter for table CustomerFormFieldVisibility]</param>
        public void DeleteCustomerFormFieldVisibilityById(entCustomerFormFieldVisibility parEntCustomerFormFieldVisibility)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomerFormFieldVisibility insDatCustomerFormFieldVisibility = new datCustomerFormFieldVisibility();
                insDatCustomerFormFieldVisibility.DeleteCustomerFormFieldVisibilityById(parEntCustomerFormFieldVisibility, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes all data stored in table [CustomerFormFieldVisibility]
        /// </summary>
        public void DeleteCustomerFormFieldVisibility()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomerFormFieldVisibility insDatCustomerFormFieldVisibility = new datCustomerFormFieldVisibility();
                insDatCustomerFormFieldVisibility.DeleteCustomerFormFieldVisibility(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }
        #endregion
    }
}
