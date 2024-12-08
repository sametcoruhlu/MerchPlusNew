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
    /// Manages transactions on table [Retail]
    /// </summary>
    public class busRetail : BusBase
    {
        /// <summary>
        /// busRetailConstructor method used while taking an instance of this class.
        /// </summary>
        public busRetail() : base()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Selects all data stored in table [Retail]
        /// </summary>
        public DataTable SelectRetail()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datRetail insDatRetail = new datRetail();
                return insDatRetail.SelectRetail(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Selects one row of data stored in table [Retail] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntRetail">Gets entity object as parameter for table Retail]</param>
        public void SelectRetailById(entRetail parEntRetail)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datRetail insDatRetail = new datRetail();
                insDatRetail.SelectRetailById(parEntRetail, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Inserts one row of data into table [Retail] based on the values of input entity object
        /// </summary>
        /// <param name="parEntRetail">Gets entity object as parameter for table Retail]</param>
        public void InsertRetail(entRetail parEntRetail)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datRetail insDatRetail = new datRetail();
                insDatRetail.InsertRetail(parEntRetail, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Updates one row of data in table [Retail] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntRetail">Gets entity object as parameter for table Retail]</param>
        public void UpdateRetailById(entRetail parEntRetail)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datRetail insDatRetail = new datRetail();
                insDatRetail.UpdateRetailById(parEntRetail, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes all data stored in table [Retail]
        /// </summary>
        public void DeleteRetail()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datRetail insDatRetail = new datRetail();
                insDatRetail.DeleteRetail(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes data stored in table [Retail] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntRetail">Gets entity object as parameter for table Retail]</param>
        public void DeleteRetailById(entRetail parEntRetail)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datRetail insDatRetail = new datRetail();
                insDatRetail.DeleteRetailById(parEntRetail, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        #endregion
        #region Custom Methods
        public DataTable SelectRetailByRetailCategoryId(entRetail insEntRetail)
        {
            DbConnector insDbConnector = new DbConnector();
            datRetail insDatRetail = new datRetail();
            try
            {
                return insDatRetail.SelectRetailByRetailCategoryId(insEntRetail, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public DataTable SelectRetailGridData()
        {
            DbConnector insDbConnector = new DbConnector();
            datRetail insDatRetail = new datRetail();
            try
            {
                return insDatRetail.SelectRetailGridData(insDbConnector);
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