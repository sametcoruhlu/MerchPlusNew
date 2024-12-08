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
    /// Manages transactions on table [RetailCategory]
    /// </summary>
    public class busRetailCategory : BusBase
    {
        /// <summary>
        /// busRetailCategoryConstructor method used while taking an instance of this class.
        /// </summary>
        public busRetailCategory() : base()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Selects all data stored in table [RetailCategory]
        /// </summary>
        public DataTable SelectRetailCategory()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datRetailCategory insDatRetailCategory = new datRetailCategory();
                return insDatRetailCategory.SelectRetailCategory(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Selects one row of data stored in table [RetailCategory] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntRetailCategory">Gets entity object as parameter for table RetailCategory]</param>
        public void SelectRetailCategoryById(entRetailCategory parEntRetailCategory)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datRetailCategory insDatRetailCategory = new datRetailCategory();
                insDatRetailCategory.SelectRetailCategoryById(parEntRetailCategory, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Inserts one row of data into table [RetailCategory] based on the values of input entity object
        /// </summary>
        /// <param name="parEntRetailCategory">Gets entity object as parameter for table RetailCategory]</param>
        public void InsertRetailCategory(entRetailCategory parEntRetailCategory)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datRetailCategory insDatRetailCategory = new datRetailCategory();
                insDatRetailCategory.InsertRetailCategory(parEntRetailCategory, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Updates one row of data in table [RetailCategory] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntRetailCategory">Gets entity object as parameter for table RetailCategory]</param>
        public void UpdateRetailCategoryById(entRetailCategory parEntRetailCategory)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datRetailCategory insDatRetailCategory = new datRetailCategory();
                insDatRetailCategory.UpdateRetailCategoryById(parEntRetailCategory, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes all data stored in table [RetailCategory]
        /// </summary>
        public void DeleteRetailCategory()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datRetailCategory insDatRetailCategory = new datRetailCategory();
                insDatRetailCategory.DeleteRetailCategory(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes data stored in table [RetailCategory] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntRetailCategory">Gets entity object as parameter for table RetailCategory]</param>
        public void DeleteRetailCategoryById(entRetailCategory parEntRetailCategory)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datRetailCategory insDatRetailCategory = new datRetailCategory();
                insDatRetailCategory.DeleteRetailCategoryById(parEntRetailCategory, insDbConnector);
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