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
    /// Manages transactions on table [RetailShopCategory]
    /// </summary>
    public class busRetailShopCategory : BusBase
    {
        /// <summary>
        /// busRetailShopCategoryConstructor method used while taking an instance of this class.
        /// </summary>
        public busRetailShopCategory() : base()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Selects all data stored in table [RetailShopCategory]
        /// </summary>
        public DataTable SelectRetailShopCategory()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datRetailShopCategory insDatRetailShopCategory = new datRetailShopCategory();
                return insDatRetailShopCategory.SelectRetailShopCategory(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Selects one row of data stored in table [RetailShopCategory] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntRetailShopCategory">Gets entity object as parameter for table RetailShopCategory]</param>
        public void SelectRetailShopCategoryById(entRetailShopCategory parEntRetailShopCategory)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datRetailShopCategory insDatRetailShopCategory = new datRetailShopCategory();
                insDatRetailShopCategory.SelectRetailShopCategoryById(parEntRetailShopCategory, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Inserts one row of data into table [RetailShopCategory] based on the values of input entity object
        /// </summary>
        /// <param name="parEntRetailShopCategory">Gets entity object as parameter for table RetailShopCategory]</param>
        public void InsertRetailShopCategory(entRetailShopCategory parEntRetailShopCategory)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datRetailShopCategory insDatRetailShopCategory = new datRetailShopCategory();
                insDatRetailShopCategory.InsertRetailShopCategory(parEntRetailShopCategory, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Updates one row of data in table [RetailShopCategory] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntRetailShopCategory">Gets entity object as parameter for table RetailShopCategory]</param>
        public void UpdateRetailShopCategoryById(entRetailShopCategory parEntRetailShopCategory)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datRetailShopCategory insDatRetailShopCategory = new datRetailShopCategory();
                insDatRetailShopCategory.UpdateRetailShopCategoryById(parEntRetailShopCategory, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes all data stored in table [RetailShopCategory]
        /// </summary>
        public void DeleteRetailShopCategory()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datRetailShopCategory insDatRetailShopCategory = new datRetailShopCategory();
                insDatRetailShopCategory.DeleteRetailShopCategory(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes data stored in table [RetailShopCategory] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntRetailShopCategory">Gets entity object as parameter for table RetailShopCategory]</param>
        public void DeleteRetailShopCategoryById(entRetailShopCategory parEntRetailShopCategory)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datRetailShopCategory insDatRetailShopCategory = new datRetailShopCategory();
                insDatRetailShopCategory.DeleteRetailShopCategoryById(parEntRetailShopCategory, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        #endregion
        #region Custom Methods
        public DataTable SelectRetailShopCategoryByRetailId(entRetailShopCategory insEntRetailShopCategory)
        {
            DbConnector insDbConnector = new DbConnector();
            datRetailShopCategory insDatRetailShopCategory = new datRetailShopCategory();
            try
            {
                return insDatRetailShopCategory.SelectRetailShopCategoryByRetailId(insEntRetailShopCategory, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public DataTable SelectRetailShopCategoryGridData()
        {
            DbConnector insDbConnector = new DbConnector();
            datRetailShopCategory insDatRetailShopCategory = new datRetailShopCategory();
            try
            {
                return insDatRetailShopCategory.SelectRetailShopCategoryGridData(insDbConnector);
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
