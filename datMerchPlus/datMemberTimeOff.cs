using System;
using System.Collections.Generic;
using System.Text;
using SqlHelper;
using System.Data;
using entMerchPlus;

namespace datMerchPlus
{
    /// <summary>
    /// The class that is generated hold parameters to manage database table [MemberTimeOff]
    /// </summary>
    public class datMemberTimeOff
    {
        /// <summary>
        /// datMemberTimeOff Constructor method used while taking an instance of this class.
        /// </summary>
        public datMemberTimeOff()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Method that selects all data stored in table [MemberTimeOff]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public DataTable SelectMemberTimeOff(DbConnector parDbConnector)
        {
            return parDbConnector.ExecuteDataTable("SelectMemberTimeOff", null);
        }

        /// <summary>
        /// Method that selects one row of data in table [MemberTimeOff] filtered by the PK field of the inbound entity object
        /// </summary>
        /// <param name="parEntMemberTimeOff">Entity object as parameter for tableMemberTimeOff]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void SelectMemberTimeOffById(entMemberTimeOff parEntMemberTimeOff, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntMemberTimeOff.Id);
            DataTable insDataTable = new DataTable();
            insDataTable = parDbConnector.ExecuteDataTable("SelectMemberTimeOffById", insDbParamCollection);
            if (insDataTable.Rows.Count > 0)
            {
                if (insDataTable.Rows[0]["Id"] != DBNull.Value)
                {
                    parEntMemberTimeOff.Id = Convert.ToInt32(insDataTable.Rows[0]["Id"]);
                }
                if (insDataTable.Rows[0]["MemberId"] != DBNull.Value)
                {
                    parEntMemberTimeOff.MemberId = Convert.ToString(insDataTable.Rows[0]["MemberId"]);
                }
                if (insDataTable.Rows[0]["StartDate"] != DBNull.Value)
                {
                    parEntMemberTimeOff.StartDate = Convert.ToDateTime(insDataTable.Rows[0]["StartDate"]);
                }
                if (insDataTable.Rows[0]["EndDate"] != DBNull.Value)
                {
                    parEntMemberTimeOff.EndDate = Convert.ToDateTime(insDataTable.Rows[0]["EndDate"]);
                }
                if (insDataTable.Rows[0]["IsOffRoute"] != DBNull.Value)
                {
                    parEntMemberTimeOff.IsOffRoute = Convert.ToBoolean(insDataTable.Rows[0]["IsOffRoute"]);
                }
                if (insDataTable.Rows[0]["Description"] != DBNull.Value)
                {
                    parEntMemberTimeOff.Description = Convert.ToString(insDataTable.Rows[0]["Description"]);
                }
                if (insDataTable.Rows[0]["CreatedBy"] != DBNull.Value)
                {
                    parEntMemberTimeOff.CreatedBy = Convert.ToString(insDataTable.Rows[0]["CreatedBy"]);
                }
                if (insDataTable.Rows[0]["CreatedOn"] != DBNull.Value)
                {
                    parEntMemberTimeOff.CreatedOn = Convert.ToDateTime(insDataTable.Rows[0]["CreatedOn"]);
                }
            }
        }

        /// <summary>
        /// Method that inserts one row of data into table [MemberTimeOff] based on the values of input entity object
        /// </summary>
        /// <param name="parEntMemberTimeOff">Entity object as parameter for tableMemberTimeOff]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void InsertMemberTimeOff(entMemberTimeOff parEntMemberTimeOff, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.AddOutput("@pId", DbType.Int32);
            insDbParamCollection.Add("@pMemberId", parEntMemberTimeOff.MemberId);
            insDbParamCollection.Add("@pStartDate", parEntMemberTimeOff.StartDate);
            insDbParamCollection.Add("@pEndDate", parEntMemberTimeOff.EndDate);
            insDbParamCollection.Add("@pIsOffRoute", parEntMemberTimeOff.IsOffRoute);
            insDbParamCollection.Add("@pDescription", parEntMemberTimeOff.Description);
            insDbParamCollection.Add("@pCreatedBy", parEntMemberTimeOff.CreatedBy);
            insDbParamCollection.Add("@pCreatedOn", parEntMemberTimeOff.CreatedOn);
            parDbConnector.ExecuteNonQuery("InsertMemberTimeOff", insDbParamCollection);
            parEntMemberTimeOff.Id = Convert.ToInt32(insDbParamCollection.GetOutPutParameter().Value);
        }

        /// <summary>
        /// Method that updates one row of data in table [MemberTimeOff] based on the values of input entity object
        /// </summary>
        /// <param name="parEntMemberTimeOff">Entity object as parameter for tableMemberTimeOff]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void UpdateMemberTimeOffById(entMemberTimeOff parEntMemberTimeOff, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntMemberTimeOff.Id);
            insDbParamCollection.Add("@pMemberId", parEntMemberTimeOff.MemberId);
            insDbParamCollection.Add("@pStartDate", parEntMemberTimeOff.StartDate);
            insDbParamCollection.Add("@pEndDate", parEntMemberTimeOff.EndDate);
            insDbParamCollection.Add("@pIsOffRoute", parEntMemberTimeOff.IsOffRoute);
            insDbParamCollection.Add("@pDescription", parEntMemberTimeOff.Description);
            insDbParamCollection.Add("@pCreatedBy", parEntMemberTimeOff.CreatedBy);
            insDbParamCollection.Add("@pCreatedOn", parEntMemberTimeOff.CreatedOn);
            parDbConnector.ExecuteNonQuery("UpdateMemberTimeOffById", insDbParamCollection);
        }

        /// <summary>
        /// Method that deletes all data in table [MemberTimeOff]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteMemberTimeOff(DbConnector parDbConnector)
        {
            parDbConnector.ExecuteNonQuery("DeleteMemberTimeOff", null);
        }

        /// <summary>
        /// Method that deletes one row of data in table [MemberTimeOff] based on the passed entity object
        /// </summary>
        /// <param name="parEntMemberTimeOff">Gets entity object as parameter for table MemberTimeOff]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteMemberTimeOffById(entMemberTimeOff parEntMemberTimeOff, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntMemberTimeOff.Id);
            parDbConnector.ExecuteNonQuery("DeleteMemberTimeOffById", insDbParamCollection);
        }

        #endregion
        #region Custom Methods
        public DataTable SelectMemberTimeOffByMemberId(entMemberTimeOff insEntMemberTimeOff, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pMemberId", insEntMemberTimeOff.MemberId);
            return insDbConnector.ExecuteDataTable("SelectMemberTimeOffByMemberId", insDbParamCollection);
        }
        #endregion
    }
}
