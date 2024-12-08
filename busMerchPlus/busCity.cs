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
    /// Manages transactions on table [City]
    /// </summary>
    public class busCity : BusBase
    {
        /// <summary>
        /// busCityConstructor method used while taking an instance of this class.
        /// </summary>
        public busCity() : base()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Selects all data stored in table [City]
        /// </summary>
        public DataTable SelectCity()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCity insDatCity = new datCity();
                return insDatCity.SelectCity(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Selects one row of data stored in table [City] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntCity">Gets entity object as parameter for table City]</param>
        public void SelectCityById(entCity parEntCity)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCity insDatCity = new datCity();
                insDatCity.SelectCityById(parEntCity, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Inserts one row of data into table [City] based on the values of input entity object
        /// </summary>
        /// <param name="parEntCity">Gets entity object as parameter for table City]</param>
        public void InsertCity(entCity parEntCity)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCity insDatCity = new datCity();
                insDatCity.InsertCity(parEntCity, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Updates one row of data in table [City] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntCity">Gets entity object as parameter for table City]</param>
        public void UpdateCityById(entCity parEntCity)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCity insDatCity = new datCity();
                insDatCity.UpdateCityById(parEntCity, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes all data stored in table [City]
        /// </summary>
        public void DeleteCity()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCity insDatCity = new datCity();
                insDatCity.DeleteCity(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes data stored in table [City] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntCity">Gets entity object as parameter for table City]</param>
        public void DeleteCityById(entCity parEntCity)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCity insDatCity = new datCity();
                insDatCity.DeleteCityById(parEntCity, insDbConnector);
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