using System;
using System.Collections.Generic;
using System.Text;
using SqlHelper;
using System.Data;
using entMerchPlus;

namespace datMerchPlus
{
    /// <summary>
    /// The class that is generated hold parameters to manage database table [District]
    /// </summary>
    public class datDistrict
    {
        /// <summary>
        /// datDistrict Constructor method used while taking an instance of this class.
        /// </summary>
        public datDistrict()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Method that selects all data stored in table [District]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public DataTable SelectDistrict(DbConnector parDbConnector)
        {
            return parDbConnector.ExecuteDataTable("SelectDistrict", null);
        }

        /// <summary>
        /// Method that selects one row of data in table [District] filtered by the PK field of the inbound entity object
        /// </summary>
        /// <param name="parEntDistrict">Entity object as parameter for tableDistrict]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void SelectDistrictById(entDistrict parEntDistrict, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntDistrict.Id);
            DataTable insDataTable = new DataTable();
            insDataTable = parDbConnector.ExecuteDataTable("SelectDistrictById", insDbParamCollection);
            if (insDataTable.Rows.Count > 0)
            {
                if (insDataTable.Rows[0]["Id"] != DBNull.Value)
                {
                    parEntDistrict.Id = Convert.ToInt32(insDataTable.Rows[0]["Id"]);
                }
                if (insDataTable.Rows[0]["CityId"] != DBNull.Value)
                {
                    parEntDistrict.CityId = Convert.ToInt32(insDataTable.Rows[0]["CityId"]);
                }
                if (insDataTable.Rows[0]["Name"] != DBNull.Value)
                {
                    parEntDistrict.Name = Convert.ToString(insDataTable.Rows[0]["Name"]);
                }
            }
        }

        /// <summary>
        /// Method that inserts one row of data into table [District] based on the values of input entity object
        /// </summary>
        /// <param name="parEntDistrict">Entity object as parameter for tableDistrict]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void InsertDistrict(entDistrict parEntDistrict, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.AddOutput("@pId", DbType.Int32);
            insDbParamCollection.Add("@pCityId", parEntDistrict.CityId);
            insDbParamCollection.Add("@pName", parEntDistrict.Name);
            parDbConnector.ExecuteNonQuery("InsertDistrict", insDbParamCollection);
            parEntDistrict.Id = Convert.ToInt32(insDbParamCollection.GetOutPutParameter().Value);
        }

        /// <summary>
        /// Method that updates one row of data in table [District] based on the values of input entity object
        /// </summary>
        /// <param name="parEntDistrict">Entity object as parameter for tableDistrict]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void UpdateDistrictById(entDistrict parEntDistrict, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntDistrict.Id);
            insDbParamCollection.Add("@pCityId", parEntDistrict.CityId);
            insDbParamCollection.Add("@pName", parEntDistrict.Name);
            parDbConnector.ExecuteNonQuery("UpdateDistrictById", insDbParamCollection);
        }

        /// <summary>
        /// Method that deletes all data in table [District]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteDistrict(DbConnector parDbConnector)
        {
            parDbConnector.ExecuteNonQuery("DeleteDistrict", null);
        }

        /// <summary>
        /// Method that deletes one row of data in table [District] based on the passed entity object
        /// </summary>
        /// <param name="parEntDistrict">Gets entity object as parameter for table District]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteDistrictById(entDistrict parEntDistrict, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntDistrict.Id);
            parDbConnector.ExecuteNonQuery("DeleteDistrictById", insDbParamCollection);
        }

        #endregion
        #region Custom Methods
        public DataTable SelectDistrictGridData(DbConnector insDbConnector)
        {
            return insDbConnector.ExecuteDataTable("SelectDistrictGridData", null);
        }

        public DataTable SelectDistrictByCityId(entDistrict insEntDistrict, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pCityId", insEntDistrict.CityId);
            return insDbConnector.ExecuteDataTable("SelectDistrictByCityId", insDbParamCollection);
        }
        #endregion
    }
}