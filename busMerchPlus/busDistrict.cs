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
    /// Manages transactions on table [District]
    /// </summary>
    public class busDistrict : BusBase
    {
        /// <summary>
        /// busDistrictConstructor method used while taking an instance of this class.
        /// </summary>
        public busDistrict() : base()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Selects all data stored in table [District]
        /// </summary>
        public DataTable SelectDistrict()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datDistrict insDatDistrict = new datDistrict();
                return insDatDistrict.SelectDistrict(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Selects one row of data stored in table [District] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntDistrict">Gets entity object as parameter for table District]</param>
        public void SelectDistrictById(entDistrict parEntDistrict)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datDistrict insDatDistrict = new datDistrict();
                insDatDistrict.SelectDistrictById(parEntDistrict, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Inserts one row of data into table [District] based on the values of input entity object
        /// </summary>
        /// <param name="parEntDistrict">Gets entity object as parameter for table District]</param>
        public void InsertDistrict(entDistrict parEntDistrict)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datDistrict insDatDistrict = new datDistrict();
                insDatDistrict.InsertDistrict(parEntDistrict, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Updates one row of data in table [District] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntDistrict">Gets entity object as parameter for table District]</param>
        public void UpdateDistrictById(entDistrict parEntDistrict)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datDistrict insDatDistrict = new datDistrict();
                insDatDistrict.UpdateDistrictById(parEntDistrict, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes all data stored in table [District]
        /// </summary>
        public void DeleteDistrict()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datDistrict insDatDistrict = new datDistrict();
                insDatDistrict.DeleteDistrict(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes data stored in table [District] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntDistrict">Gets entity object as parameter for table District]</param>
        public void DeleteDistrictById(entDistrict parEntDistrict)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datDistrict insDatDistrict = new datDistrict();
                insDatDistrict.DeleteDistrictById(parEntDistrict, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        #endregion
        #region Custom Methods
        public DataTable SelectDistrictGridData()
        {
            DbConnector insDbConnector = new DbConnector();
            datDistrict insDatDistrict = new datDistrict();
            try
            {
                return insDatDistrict.SelectDistrictGridData(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public DataTable SelectDistrictByCityId(entDistrict insEntDistrict)
        {
            DbConnector insDbConnector = new DbConnector();
            datDistrict insDatDistrict = new datDistrict();
            try
            {
                return insDatDistrict.SelectDistrictByCityId(insEntDistrict, insDbConnector);
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