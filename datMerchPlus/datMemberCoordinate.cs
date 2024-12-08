using entMerchPlus;
using SqlHelper;
using System;
using System.Data;

namespace datMerchPlus
{
    /// <summary>
    /// The class that is generated hold parameters to manage database table [MemberCoordinate]
    /// </summary>
    public class datMemberCoordinate
    {
        /// <summary>
        /// datMemberCoordinate Constructor method used while taking an instance of this class.
        /// </summary>
        public datMemberCoordinate()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Method that selects all data stored in table [MemberCoordinate]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public DataTable SelectMemberCoordinate(DbConnector parDbConnector)
        {
            return parDbConnector.ExecuteDataTable("SelectMemberCoordinate", null);
        }

        /// <summary>
        /// Method that selects one row of data in table [MemberCoordinate] filtered by the PK field of the inbound entity object
        /// </summary>
        /// <param name="parEntMemberCoordinate">Entity object as parameter for tableMemberCoordinate]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void SelectMemberCoordinateById(entMemberCoordinate parEntMemberCoordinate, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntMemberCoordinate.Id);
            DataTable insDataTable = new DataTable();
            insDataTable = parDbConnector.ExecuteDataTable("SelectMemberCoordinateById", insDbParamCollection);
            if (insDataTable.Rows.Count > 0)
            {
                if (insDataTable.Rows[0]["Id"] != DBNull.Value)
                {
                    parEntMemberCoordinate.Id = Convert.ToInt32(insDataTable.Rows[0]["Id"]);
                }
                if (insDataTable.Rows[0]["MemberId"] != DBNull.Value)
                {
                    parEntMemberCoordinate.MemberId = Convert.ToString(insDataTable.Rows[0]["MemberId"]);
                }
                if (insDataTable.Rows[0]["CoordinateX"] != DBNull.Value)
                {
                    parEntMemberCoordinate.CoordinateX = Convert.ToDecimal(insDataTable.Rows[0]["CoordinateX"]);
                }
                if (insDataTable.Rows[0]["CoordinateY"] != DBNull.Value)
                {
                    parEntMemberCoordinate.CoordinateY = Convert.ToDecimal(insDataTable.Rows[0]["CoordinateY"]);
                }
                if (insDataTable.Rows[0]["CreatedOn"] != DBNull.Value)
                {
                    parEntMemberCoordinate.CreatedOn = Convert.ToDateTime(insDataTable.Rows[0]["CreatedOn"]);
                }
            }
        }

        /// <summary>
        /// Method that inserts one row of data into table [MemberCoordinate] based on the values of input entity object
        /// </summary>
        /// <param name="parEntMemberCoordinate">Entity object as parameter for tableMemberCoordinate]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void InsertMemberCoordinate(entMemberCoordinate parEntMemberCoordinate, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.AddOutput("@pId", DbType.Int32);
            insDbParamCollection.Add("@pMemberId", parEntMemberCoordinate.MemberId);
            insDbParamCollection.Add("@pCoordinateX", parEntMemberCoordinate.CoordinateX);
            insDbParamCollection.Add("@pCoordinateY", parEntMemberCoordinate.CoordinateY);
            insDbParamCollection.Add("@pCreatedOn", parEntMemberCoordinate.CreatedOn);
            parDbConnector.ExecuteNonQuery("InsertMemberCoordinate", insDbParamCollection);
            parEntMemberCoordinate.Id = Convert.ToInt32(insDbParamCollection.GetOutPutParameter().Value);
        }

        /// <summary>
        /// Method that updates one row of data in table [MemberCoordinate] based on the values of input entity object
        /// </summary>
        /// <param name="parEntMemberCoordinate">Entity object as parameter for tableMemberCoordinate]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void UpdateMemberCoordinateById(entMemberCoordinate parEntMemberCoordinate, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntMemberCoordinate.Id);
            insDbParamCollection.Add("@pMemberId", parEntMemberCoordinate.MemberId);
            insDbParamCollection.Add("@pCoordinateX", parEntMemberCoordinate.CoordinateX);
            insDbParamCollection.Add("@pCoordinateY", parEntMemberCoordinate.CoordinateY);
            insDbParamCollection.Add("@pCreatedOn", parEntMemberCoordinate.CreatedOn);
            parDbConnector.ExecuteNonQuery("UpdateMemberCoordinateById", insDbParamCollection);
        }

        /// <summary>
        /// Method that deletes all data in table [MemberCoordinate]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteMemberCoordinate(DbConnector parDbConnector)
        {
            parDbConnector.ExecuteNonQuery("DeleteMemberCoordinate", null);
        }

        /// <summary>
        /// Method that deletes one row of data in table [MemberCoordinate] based on the passed entity object
        /// </summary>
        /// <param name="parEntMemberCoordinate">Gets entity object as parameter for table MemberCoordinate]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteMemberCoordinateById(entMemberCoordinate parEntMemberCoordinate, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntMemberCoordinate.Id);
            parDbConnector.ExecuteNonQuery("DeleteMemberCoordinateById", insDbParamCollection);
        }

        #endregion
        #region Custom Methods
        public DataTable SelectMemberCoordinateByMemberIdToday(entMemberCoordinate insEntMemberCoordinate, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pMemberId", insEntMemberCoordinate.MemberId);
            return insDbConnector.ExecuteDataTable("SelectMemberCoordinateByMemberIdToday", insDbParamCollection);
        }
        #endregion
    }
}
