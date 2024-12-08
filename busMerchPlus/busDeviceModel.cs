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
    /// Manages transactions on table [DeviceModel]
    /// </summary>
    public class busDeviceModel : BusBase
    {
        /// <summary>
        /// busDeviceModelConstructor method used while taking an instance of this class.
        /// </summary>
        public busDeviceModel() : base()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Selects all data stored in table [DeviceModel]
        /// </summary>
        public DataTable SelectDeviceModel()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datDeviceModel insDatDeviceModel = new datDeviceModel();
                return insDatDeviceModel.SelectDeviceModel(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Selects one row of data stored in table [DeviceModel] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntDeviceModel">Gets entity object as parameter for table DeviceModel]</param>
        public void SelectDeviceModelById(entDeviceModel parEntDeviceModel)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datDeviceModel insDatDeviceModel = new datDeviceModel();
                insDatDeviceModel.SelectDeviceModelById(parEntDeviceModel, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Inserts one row of data into table [DeviceModel] based on the values of input entity object
        /// </summary>
        /// <param name="parEntDeviceModel">Gets entity object as parameter for table DeviceModel]</param>
        public void InsertDeviceModel(entDeviceModel parEntDeviceModel)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datDeviceModel insDatDeviceModel = new datDeviceModel();
                insDatDeviceModel.InsertDeviceModel(parEntDeviceModel, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Updates one row of data in table [DeviceModel] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntDeviceModel">Gets entity object as parameter for table DeviceModel]</param>
        public void UpdateDeviceModelById(entDeviceModel parEntDeviceModel)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datDeviceModel insDatDeviceModel = new datDeviceModel();
                insDatDeviceModel.UpdateDeviceModelById(parEntDeviceModel, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes all data stored in table [DeviceModel]
        /// </summary>
        public void DeleteDeviceModel()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datDeviceModel insDatDeviceModel = new datDeviceModel();
                insDatDeviceModel.DeleteDeviceModel(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes data stored in table [DeviceModel] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntDeviceModel">Gets entity object as parameter for table DeviceModel]</param>
        public void DeleteDeviceModelById(entDeviceModel parEntDeviceModel)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datDeviceModel insDatDeviceModel = new datDeviceModel();
                insDatDeviceModel.DeleteDeviceModelById(parEntDeviceModel, insDbConnector);
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
