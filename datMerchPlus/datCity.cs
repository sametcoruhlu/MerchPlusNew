using System;
using System.Collections.Generic;
using System.Text;
using SqlHelper;
using System.Data;
using entMerchPlus;

namespace datMerchPlus
{
    /// <summary>
    /// The class that is generated hold parameters to manage database table [City]
    /// </summary>
    public class datCity
    {
        /// <summary>
        /// datCity Constructor method used while taking an instance of this class.
        /// </summary>
        public datCity()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Method that selects all data stored in table [City]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public DataTable SelectCity(DbConnector parDbConnector)
        {
            return parDbConnector.ExecuteDataTable("SelectCity", null);
        }

        /// <summary>
        /// Method that selects one row of data in table [City] filtered by the PK field of the inbound entity object
        /// </summary>
        /// <param name="parEntCity">Entity object as parameter for tableCity]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void SelectCityById(entCity parEntCity, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntCity.Id);
            DataTable insDataTable = new DataTable();
            insDataTable = parDbConnector.ExecuteDataTable("SelectCityById", insDbParamCollection);
            if (insDataTable.Rows.Count > 0)
            {
                if (insDataTable.Rows[0]["Id"] != DBNull.Value)
                {
                    parEntCity.Id = Convert.ToInt32(insDataTable.Rows[0]["Id"]);
                }
                if (insDataTable.Rows[0]["AreaId"] != DBNull.Value)
                {
                    parEntCity.AreaId = Convert.ToInt32(insDataTable.Rows[0]["AreaId"]);
                }
                if (insDataTable.Rows[0]["Name"] != DBNull.Value)
                {
                    parEntCity.Name = Convert.ToString(insDataTable.Rows[0]["Name"]);
                }
            }
        }

        /// <summary>
        /// Method that inserts one row of data into table [City] based on the values of input entity object
        /// </summary>
        /// <param name="parEntCity">Entity object as parameter for tableCity]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void InsertCity(entCity parEntCity, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.AddOutput("@pId", DbType.Int32);
            insDbParamCollection.Add("@pAreaId", parEntCity.AreaId);
            insDbParamCollection.Add("@pName", parEntCity.Name);
            parDbConnector.ExecuteNonQuery("InsertCity", insDbParamCollection);
            parEntCity.Id = Convert.ToInt32(insDbParamCollection.GetOutPutParameter().Value);
        }

        /// <summary>
        /// Method that updates one row of data in table [City] based on the values of input entity object
        /// </summary>
        /// <param name="parEntCity">Entity object as parameter for tableCity]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void UpdateCityById(entCity parEntCity, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntCity.Id);
            insDbParamCollection.Add("@pAreaId", parEntCity.AreaId);
            insDbParamCollection.Add("@pName", parEntCity.Name);
            parDbConnector.ExecuteNonQuery("UpdateCityById", insDbParamCollection);
        }

        /// <summary>
        /// Method that deletes all data in table [City]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteCity(DbConnector parDbConnector)
        {
            parDbConnector.ExecuteNonQuery("DeleteCity", null);
        }

        /// <summary>
        /// Method that deletes one row of data in table [City] based on the passed entity object
        /// </summary>
        /// <param name="parEntCity">Gets entity object as parameter for table City]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteCityById(entCity parEntCity, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntCity.Id);
            parDbConnector.ExecuteNonQuery("DeleteCityById", insDbParamCollection);
        }

        #endregion
        #region Custom Methods
        #endregion
    }
}