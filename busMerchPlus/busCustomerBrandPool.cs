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
    /// Manages transactions on table [CustomerBrandPool]
    /// </summary>
    public class busCustomerBrandPool : BusBase
    {
        /// <summary>
        /// busCustomerBrandPoolConstructor method used while taking an instance of this class.
        /// </summary>
        public busCustomerBrandPool() : base()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Selects all data stored in table [CustomerBrandPool]
        /// </summary>
        public DataTable SelectCustomerBrandPool()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomerBrandPool insDatCustomerBrandPool = new datCustomerBrandPool();
                return insDatCustomerBrandPool.SelectCustomerBrandPool(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Selects one row of data stored in table [CustomerBrandPool] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntCustomerBrandPool">Gets entity object as parameter for table CustomerBrandPool]</param>
        public void SelectCustomerBrandPoolById(entCustomerBrandPool parEntCustomerBrandPool)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomerBrandPool insDatCustomerBrandPool = new datCustomerBrandPool();
                insDatCustomerBrandPool.SelectCustomerBrandPoolById(parEntCustomerBrandPool, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Inserts one row of data into table [CustomerBrandPool] based on the values of input entity object
        /// </summary>
        /// <param name="parEntCustomerBrandPool">Gets entity object as parameter for table CustomerBrandPool]</param>
        public void InsertCustomerBrandPool(entCustomerBrandPool parEntCustomerBrandPool)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomerBrandPool insDatCustomerBrandPool = new datCustomerBrandPool();
                insDatCustomerBrandPool.InsertCustomerBrandPool(parEntCustomerBrandPool, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Updates one row of data in table [CustomerBrandPool] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntCustomerBrandPool">Gets entity object as parameter for table CustomerBrandPool]</param>
        public void UpdateCustomerBrandPoolById(entCustomerBrandPool parEntCustomerBrandPool)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomerBrandPool insDatCustomerBrandPool = new datCustomerBrandPool();
                insDatCustomerBrandPool.UpdateCustomerBrandPoolById(parEntCustomerBrandPool, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes all data stored in table [CustomerBrandPool]
        /// </summary>
        public void DeleteCustomerBrandPool()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomerBrandPool insDatCustomerBrandPool = new datCustomerBrandPool();
                insDatCustomerBrandPool.DeleteCustomerBrandPool(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes data stored in table [CustomerBrandPool] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntCustomerBrandPool">Gets entity object as parameter for table CustomerBrandPool]</param>
        public void DeleteCustomerBrandPoolById(entCustomerBrandPool parEntCustomerBrandPool)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomerBrandPool insDatCustomerBrandPool = new datCustomerBrandPool();
                insDatCustomerBrandPool.DeleteCustomerBrandPoolById(parEntCustomerBrandPool, insDbConnector);
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