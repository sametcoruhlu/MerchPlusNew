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
    /// Manages transactions on table [ProductExpiration]
    /// </summary>
    public class busProductExpiration : BusBase
    {
        /// <summary>
        /// busProductExpirationConstructor method used while taking an instance of this class.
        /// </summary>
        public busProductExpiration() : base()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Selects all data stored in table [ProductExpiration]
        /// </summary>
        public DataTable SelectProductExpiration()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datProductExpiration insDatProductExpiration = new datProductExpiration();
                return insDatProductExpiration.SelectProductExpiration(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Selects one row of data stored in table [ProductExpiration] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntProductExpiration">Gets entity object as parameter for table ProductExpiration]</param>
        public void SelectProductExpirationById(entProductExpiration parEntProductExpiration)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datProductExpiration insDatProductExpiration = new datProductExpiration();
                insDatProductExpiration.SelectProductExpirationById(parEntProductExpiration, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Inserts one row of data into table [ProductExpiration] based on the values of input entity object
        /// </summary>
        /// <param name="parEntProductExpiration">Gets entity object as parameter for table ProductExpiration]</param>
        public void InsertProductExpiration(entProductExpiration parEntProductExpiration)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datProductExpiration insDatProductExpiration = new datProductExpiration();
                insDatProductExpiration.InsertProductExpiration(parEntProductExpiration, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Updates one row of data in table [ProductExpiration] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntProductExpiration">Gets entity object as parameter for table ProductExpiration]</param>
        public void UpdateProductExpirationById(entProductExpiration parEntProductExpiration)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datProductExpiration insDatProductExpiration = new datProductExpiration();
                insDatProductExpiration.UpdateProductExpirationById(parEntProductExpiration, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes all data stored in table [ProductExpiration]
        /// </summary>
        public void DeleteProductExpiration()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datProductExpiration insDatProductExpiration = new datProductExpiration();
                insDatProductExpiration.DeleteProductExpiration(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes data stored in table [ProductExpiration] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntProductExpiration">Gets entity object as parameter for table ProductExpiration]</param>
        public void DeleteProductExpirationById(entProductExpiration parEntProductExpiration)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datProductExpiration insDatProductExpiration = new datProductExpiration();
                insDatProductExpiration.DeleteProductExpirationById(parEntProductExpiration, insDbConnector);
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