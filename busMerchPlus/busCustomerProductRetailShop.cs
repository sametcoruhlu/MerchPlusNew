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
    /// Manages transactions on table [CustomerProductRetailShop]
    /// </summary>
    public class busCustomerProductRetailShop : BusBase
    {
        /// <summary>
        /// busCustomerProductRetailShopConstructor method used while taking an instance of this class.
        /// </summary>
        public busCustomerProductRetailShop() : base()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Selects all data stored in table [CustomerProductRetailShop]
        /// </summary>
        public DataTable SelectCustomerProductRetailShop()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomerProductRetailShop insDatCustomerProductRetailShop = new datCustomerProductRetailShop();
                return insDatCustomerProductRetailShop.SelectCustomerProductRetailShop(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Selects one row of data stored in table [CustomerProductRetailShop] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntCustomerProductRetailShop">Gets entity object as parameter for table CustomerProductRetailShop]</param>
        public void SelectCustomerProductRetailShopById(entCustomerProductRetailShop parEntCustomerProductRetailShop)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomerProductRetailShop insDatCustomerProductRetailShop = new datCustomerProductRetailShop();
                insDatCustomerProductRetailShop.SelectCustomerProductRetailShopById(parEntCustomerProductRetailShop, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Inserts one row of data into table [CustomerProductRetailShop] based on the values of input entity object
        /// </summary>
        /// <param name="parEntCustomerProductRetailShop">Gets entity object as parameter for table CustomerProductRetailShop]</param>
        public void InsertCustomerProductRetailShop(entCustomerProductRetailShop parEntCustomerProductRetailShop)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomerProductRetailShop insDatCustomerProductRetailShop = new datCustomerProductRetailShop();
                insDatCustomerProductRetailShop.InsertCustomerProductRetailShop(parEntCustomerProductRetailShop, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Updates one row of data in table [CustomerProductRetailShop] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntCustomerProductRetailShop">Gets entity object as parameter for table CustomerProductRetailShop]</param>
        public void UpdateCustomerProductRetailShopById(entCustomerProductRetailShop parEntCustomerProductRetailShop)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomerProductRetailShop insDatCustomerProductRetailShop = new datCustomerProductRetailShop();
                insDatCustomerProductRetailShop.UpdateCustomerProductRetailShopById(parEntCustomerProductRetailShop, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes all data stored in table [CustomerProductRetailShop]
        /// </summary>
        public void DeleteCustomerProductRetailShop()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomerProductRetailShop insDatCustomerProductRetailShop = new datCustomerProductRetailShop();
                insDatCustomerProductRetailShop.DeleteCustomerProductRetailShop(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes data stored in table [CustomerProductRetailShop] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntCustomerProductRetailShop">Gets entity object as parameter for table CustomerProductRetailShop]</param>
        public void DeleteCustomerProductRetailShopById(entCustomerProductRetailShop parEntCustomerProductRetailShop)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomerProductRetailShop insDatCustomerProductRetailShop = new datCustomerProductRetailShop();
                insDatCustomerProductRetailShop.DeleteCustomerProductRetailShopById(parEntCustomerProductRetailShop, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        #endregion
        #region Custom Methods
        public void SelectCustomerProductRetailShopByCustomerIdCustomerProductIdRetailShopId(entCustomerProductRetailShop insEntCustomerProductRetailShop)
        {
            DbConnector insDbConnector = new DbConnector();
            datCustomerProductRetailShop insDatCustomerProductRetailShop = new datCustomerProductRetailShop();
            try
            {
                insDatCustomerProductRetailShop.SelectCustomerProductRetailShopByCustomerIdCustomerProductIdRetailShopId(insEntCustomerProductRetailShop, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        public DataTable SelectCustomerProductRetailShopByRetailShopId(entCustomerProductRetailShop insEntCustomerProductRetailShop)
        {
            DbConnector insDbConnector = new DbConnector();
            datCustomerProductRetailShop insDatCustomerProductRetailShop = new datCustomerProductRetailShop();
            try
            {
                return insDatCustomerProductRetailShop.SelectCustomerProductRetailShopByRetailShopId(insEntCustomerProductRetailShop, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public void DeleteCustomerProductRetailShopByRetailShopIdCustomerId(entCustomerProductRetailShop insEntCustomerProductRetailShop_Delete)
        {
            DbConnector insDbConnector = new DbConnector();
            datCustomerProductRetailShop insDatCustomerProductRetailShop = new datCustomerProductRetailShop();
            try
            {
                insDatCustomerProductRetailShop.DeleteCustomerProductRetailShopByRetailShopIdCustomerId(insEntCustomerProductRetailShop_Delete, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        public DataTable SelectCustomerProductRetailShopByRetailCategoryId(int RetailCategoryId)
        {
            DbConnector insDbConnector = new DbConnector();
            datCustomerProductRetailShop insDat = new datCustomerProductRetailShop();
            try
            {
                return insDat.SelectCustomerProductRetailShopByRetailCategoryId(RetailCategoryId, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public void DeleteCustomerProductRetailShopByCustomerIdRetailCategoryId(int CustomerId, int RetailCategoryId)
        {
            DbConnector insDbConnector = new DbConnector();
            datCustomerProductRetailShop insDat = new datCustomerProductRetailShop();
            try
            {
                insDat.DeleteCustomerProductRetailShopByCustomerIdRetailCategoryId(CustomerId, RetailCategoryId, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        public DataTable SelectCustomerProductRetailShopByRetailId(int RetailId)
        {
            DbConnector insDbConnector = new DbConnector();
            datCustomerProductRetailShop insDat = new datCustomerProductRetailShop();
            try
            {
                return insDat.SelectCustomerProductRetailShopByRetailId(RetailId, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public void DeleteCustomerProductRetailShopByCustomerIdRetailId(int CustomerId, int RetailId)
        {
            DbConnector insDbConnector = new DbConnector();
            datCustomerProductRetailShop insDat = new datCustomerProductRetailShop();
            try
            {
                insDat.DeleteCustomerProductRetailShopByCustomerIdRetailId(CustomerId, RetailId, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        public DataTable SelectCustomerProductRetailShopByRetailChannelId(int RetailCategoryId)
        {
            DbConnector insDbConnector = new DbConnector();
            datCustomerProductRetailShop insDat = new datCustomerProductRetailShop();
            try
            {
                return insDat.SelectCustomerProductRetailShopByRetailChannelId(RetailCategoryId, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public void DeleteCustomerProductRetailShopByCustomerIdRetailChannelId(int CustomerId, int RetailCategoryId)
        {
            DbConnector insDbConnector = new DbConnector();
            datCustomerProductRetailShop insDat = new datCustomerProductRetailShop();
            try
            {
                insDat.DeleteCustomerProductRetailShopByCustomerIdRetailChannelId(CustomerId, RetailCategoryId, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        public DataTable SelectCustomerProductRetailShopByRetailShopCategoryIdOnlyProducts(int RetailCategoryId)
        {
            DbConnector insDbConnector = new DbConnector();
            datCustomerProductRetailShop insDat = new datCustomerProductRetailShop();
            try
            {
                return insDat.SelectCustomerProductRetailShopByRetailShopCategoryIdOnlyProducts(RetailCategoryId, insDbConnector);
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
