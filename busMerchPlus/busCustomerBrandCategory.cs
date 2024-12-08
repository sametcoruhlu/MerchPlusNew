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
    /// Manages transactions on table [CustomerBrandCategory]
    /// </summary>
    public class busCustomerBrandCategory : BusBase
    {
        /// <summary>
        /// busCustomerBrandCategoryConstructor method used while taking an instance of this class.
        /// </summary>
        public busCustomerBrandCategory() : base()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Selects all data stored in table [CustomerBrandCategory]
        /// </summary>
        public DataTable SelectCustomerBrandCategory()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomerBrandCategory insDatCustomerBrandCategory = new datCustomerBrandCategory();
                return insDatCustomerBrandCategory.SelectCustomerBrandCategory(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Selects one row of data stored in table [CustomerBrandCategory] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntCustomerBrandCategory">Gets entity object as parameter for table CustomerBrandCategory]</param>
        public void SelectCustomerBrandCategoryById(entCustomerBrandCategory parEntCustomerBrandCategory)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomerBrandCategory insDatCustomerBrandCategory = new datCustomerBrandCategory();
                insDatCustomerBrandCategory.SelectCustomerBrandCategoryById(parEntCustomerBrandCategory, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Inserts one row of data into table [CustomerBrandCategory] based on the values of input entity object
        /// </summary>
        /// <param name="parEntCustomerBrandCategory">Gets entity object as parameter for table CustomerBrandCategory]</param>
        public void InsertCustomerBrandCategory(entCustomerBrandCategory parEntCustomerBrandCategory)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomerBrandCategory insDatCustomerBrandCategory = new datCustomerBrandCategory();
                insDatCustomerBrandCategory.InsertCustomerBrandCategory(parEntCustomerBrandCategory, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Updates one row of data in table [CustomerBrandCategory] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntCustomerBrandCategory">Gets entity object as parameter for table CustomerBrandCategory]</param>
        public void UpdateCustomerBrandCategoryById(entCustomerBrandCategory parEntCustomerBrandCategory)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomerBrandCategory insDatCustomerBrandCategory = new datCustomerBrandCategory();
                insDatCustomerBrandCategory.UpdateCustomerBrandCategoryById(parEntCustomerBrandCategory, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes all data stored in table [CustomerBrandCategory]
        /// </summary>
        public void DeleteCustomerBrandCategory()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomerBrandCategory insDatCustomerBrandCategory = new datCustomerBrandCategory();
                insDatCustomerBrandCategory.DeleteCustomerBrandCategory(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes data stored in table [CustomerBrandCategory] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntCustomerBrandCategory">Gets entity object as parameter for table CustomerBrandCategory]</param>
        public void DeleteCustomerBrandCategoryById(entCustomerBrandCategory parEntCustomerBrandCategory)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomerBrandCategory insDatCustomerBrandCategory = new datCustomerBrandCategory();
                insDatCustomerBrandCategory.DeleteCustomerBrandCategoryById(parEntCustomerBrandCategory, insDbConnector);
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