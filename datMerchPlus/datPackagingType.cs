using System;
using System.Collections.Generic;
using System.Text;
using SqlHelper;
using System.Data;
using entMerchPlus;

namespace datMerchPlus
{
    /// <summary>
    /// The class that is generated hold parameters to manage database table [PackagingType]
    /// </summary>
    public class datPackagingType
    {
        /// <summary>
        /// datPackagingType Constructor method used while taking an instance of this class.
        /// </summary>
        public datPackagingType()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Method that selects all data stored in table [PackagingType]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public DataTable SelectPackagingType(DbConnector parDbConnector)
        {
            return parDbConnector.ExecuteDataTable("SelectPackagingType", null);
        }

        /// <summary>
        /// Method that selects one row of data in table [PackagingType] filtered by the PK field of the inbound entity object
        /// </summary>
        /// <param name="parEntPackagingType">Entity object as parameter for tablePackagingType]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void SelectPackagingTypeById(entPackagingType parEntPackagingType, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntPackagingType.Id);
            DataTable insDataTable = new DataTable();
            insDataTable = parDbConnector.ExecuteDataTable("SelectPackagingTypeById", insDbParamCollection);
            if (insDataTable.Rows.Count > 0)
            {
                if (insDataTable.Rows[0]["Id"] != DBNull.Value)
                {
                    parEntPackagingType.Id = Convert.ToInt32(insDataTable.Rows[0]["Id"]);
                }
                if (insDataTable.Rows[0]["Name"] != DBNull.Value)
                {
                    parEntPackagingType.Name = Convert.ToString(insDataTable.Rows[0]["Name"]);
                }
            }
        }

        /// <summary>
        /// Method that inserts one row of data into table [PackagingType] based on the values of input entity object
        /// </summary>
        /// <param name="parEntPackagingType">Entity object as parameter for tablePackagingType]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void InsertPackagingType(entPackagingType parEntPackagingType, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.AddOutput("@pId", DbType.Int32);
            insDbParamCollection.Add("@pName", parEntPackagingType.Name);
            parDbConnector.ExecuteNonQuery("InsertPackagingType", insDbParamCollection);
            parEntPackagingType.Id = Convert.ToInt32(insDbParamCollection.GetOutPutParameter().Value);
        }

        /// <summary>
        /// Method that updates one row of data in table [PackagingType] based on the values of input entity object
        /// </summary>
        /// <param name="parEntPackagingType">Entity object as parameter for tablePackagingType]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void UpdatePackagingTypeById(entPackagingType parEntPackagingType, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntPackagingType.Id);
            insDbParamCollection.Add("@pName", parEntPackagingType.Name);
            parDbConnector.ExecuteNonQuery("UpdatePackagingTypeById", insDbParamCollection);
        }

        /// <summary>
        /// Method that deletes all data in table [PackagingType]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeletePackagingType(DbConnector parDbConnector)
        {
            parDbConnector.ExecuteNonQuery("DeletePackagingType", null);
        }

        /// <summary>
        /// Method that deletes one row of data in table [PackagingType] based on the passed entity object
        /// </summary>
        /// <param name="parEntPackagingType">Gets entity object as parameter for table PackagingType]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeletePackagingTypeById(entPackagingType parEntPackagingType, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntPackagingType.Id);
            parDbConnector.ExecuteNonQuery("DeletePackagingTypeById", insDbParamCollection);
        }

        #endregion
        #region Custom Methods
        #endregion
    }
}
