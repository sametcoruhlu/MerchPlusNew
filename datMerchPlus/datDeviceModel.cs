using System;
using System.Collections.Generic;
using System.Text;
using SqlHelper;
using System.Data;
using entMerchPlus;

namespace datMerchPlus
{
    /// <summary>
    /// The class that is generated hold parameters to manage database table [DeviceModel]
    /// </summary>
    public class datDeviceModel
    {
        /// <summary>
        /// datDeviceModel Constructor method used while taking an instance of this class.
        /// </summary>
        public datDeviceModel()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Method that selects all data stored in table [DeviceModel]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public DataTable SelectDeviceModel(DbConnector parDbConnector)
        {
            return parDbConnector.ExecuteDataTable("SelectDeviceModel", null);
        }

        /// <summary>
        /// Method that selects one row of data in table [DeviceModel] filtered by the PK field of the inbound entity object
        /// </summary>
        /// <param name="parEntDeviceModel">Entity object as parameter for tableDeviceModel]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void SelectDeviceModelById(entDeviceModel parEntDeviceModel, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntDeviceModel.Id);
            DataTable insDataTable = new DataTable();
            insDataTable = parDbConnector.ExecuteDataTable("SelectDeviceModelById", insDbParamCollection);
            if (insDataTable.Rows.Count > 0)
            {
                if (insDataTable.Rows[0]["Id"] != DBNull.Value)
                {
                    parEntDeviceModel.Id = Convert.ToInt32(insDataTable.Rows[0]["Id"]);
                }
                if (insDataTable.Rows[0]["Name"] != DBNull.Value)
                {
                    parEntDeviceModel.Name = Convert.ToString(insDataTable.Rows[0]["Name"]);
                }
            }
        }

        /// <summary>
        /// Method that inserts one row of data into table [DeviceModel] based on the values of input entity object
        /// </summary>
        /// <param name="parEntDeviceModel">Entity object as parameter for tableDeviceModel]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void InsertDeviceModel(entDeviceModel parEntDeviceModel, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.AddOutput("@pId", DbType.Int32);
            insDbParamCollection.Add("@pName", parEntDeviceModel.Name);
            parDbConnector.ExecuteNonQuery("InsertDeviceModel", insDbParamCollection);
            parEntDeviceModel.Id = Convert.ToInt32(insDbParamCollection.GetOutPutParameter().Value);
        }

        /// <summary>
        /// Method that updates one row of data in table [DeviceModel] based on the values of input entity object
        /// </summary>
        /// <param name="parEntDeviceModel">Entity object as parameter for tableDeviceModel]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void UpdateDeviceModelById(entDeviceModel parEntDeviceModel, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntDeviceModel.Id);
            insDbParamCollection.Add("@pName", parEntDeviceModel.Name);
            parDbConnector.ExecuteNonQuery("UpdateDeviceModelById", insDbParamCollection);
        }

        /// <summary>
        /// Method that deletes all data in table [DeviceModel]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteDeviceModel(DbConnector parDbConnector)
        {
            parDbConnector.ExecuteNonQuery("DeleteDeviceModel", null);
        }

        /// <summary>
        /// Method that deletes one row of data in table [DeviceModel] based on the passed entity object
        /// </summary>
        /// <param name="parEntDeviceModel">Gets entity object as parameter for table DeviceModel]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteDeviceModelById(entDeviceModel parEntDeviceModel, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntDeviceModel.Id);
            parDbConnector.ExecuteNonQuery("DeleteDeviceModelById", insDbParamCollection);
        }

        #endregion
        #region Custom Methods
        #endregion
    }
}
