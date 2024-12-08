using System;
using System.Collections.Generic;
using System.Text;
using SqlHelper;
using System.Data;
using entMerchPlus;

namespace datMerchPlus
{
    /// <summary>
    /// The class that is generated hold parameters to manage database table [Area]
    /// </summary>
    public class datArea
    {
        /// <summary>
        /// datArea Constructor method used while taking an instance of this class.
        /// </summary>
        public datArea()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Method that selects all data stored in table [Area]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public DataTable SelectArea(DbConnector parDbConnector)
        {
            return parDbConnector.ExecuteDataTable("SelectArea", null);
        }

        /// <summary>
        /// Method that selects one row of data in table [Area] filtered by the PK field of the inbound entity object
        /// </summary>
        /// <param name="parEntArea">Entity object as parameter for tableArea]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void SelectAreaById(entArea parEntArea, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntArea.Id);
            DataTable insDataTable = new DataTable();
            insDataTable = parDbConnector.ExecuteDataTable("SelectAreaById", insDbParamCollection);
            if (insDataTable.Rows.Count > 0)
            {
                if (insDataTable.Rows[0]["Id"] != DBNull.Value)
                {
                    parEntArea.Id = Convert.ToInt32(insDataTable.Rows[0]["Id"]);
                }
                if (insDataTable.Rows[0]["Name"] != DBNull.Value)
                {
                    parEntArea.Name = Convert.ToString(insDataTable.Rows[0]["Name"]);
                }
            }
        }

        /// <summary>
        /// Method that inserts one row of data into table [Area] based on the values of input entity object
        /// </summary>
        /// <param name="parEntArea">Entity object as parameter for tableArea]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void InsertArea(entArea parEntArea, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.AddOutput("@pId", DbType.Int32);
            insDbParamCollection.Add("@pName", parEntArea.Name);
            parDbConnector.ExecuteNonQuery("InsertArea", insDbParamCollection);
            parEntArea.Id = Convert.ToInt32(insDbParamCollection.GetOutPutParameter().Value);
        }

        /// <summary>
        /// Method that updates one row of data in table [Area] based on the values of input entity object
        /// </summary>
        /// <param name="parEntArea">Entity object as parameter for tableArea]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void UpdateAreaById(entArea parEntArea, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntArea.Id);
            insDbParamCollection.Add("@pName", parEntArea.Name);
            parDbConnector.ExecuteNonQuery("UpdateAreaById", insDbParamCollection);
        }

        /// <summary>
        /// Method that deletes all data in table [Area]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteArea(DbConnector parDbConnector)
        {
            parDbConnector.ExecuteNonQuery("DeleteArea", null);
        }

        /// <summary>
        /// Method that deletes one row of data in table [Area] based on the passed entity object
        /// </summary>
        /// <param name="parEntArea">Gets entity object as parameter for table Area]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteAreaById(entArea parEntArea, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntArea.Id);
            parDbConnector.ExecuteNonQuery("DeleteAreaById", insDbParamCollection);
        }

        #endregion
        #region Custom Methods
        #endregion
    }
}