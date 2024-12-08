using System;
using System.Collections.Generic;
using System.Text;
using SqlHelper;
using System.Data;
using entMerchPlus;

namespace datMerchPlus
{
    /// <summary>
    /// The class that is generated hold parameters to manage database table [MemberTitle]
    /// </summary>
    public class datMemberTitle
    {
        /// <summary>
        /// datMemberTitle Constructor method used while taking an instance of this class.
        /// </summary>
        public datMemberTitle()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Method that selects all data stored in table [MemberTitle]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public DataTable SelectMemberTitle(DbConnector parDbConnector)
        {
            return parDbConnector.ExecuteDataTable("SelectMemberTitle", null);
        }

        /// <summary>
        /// Method that selects one row of data in table [MemberTitle] filtered by the PK field of the inbound entity object
        /// </summary>
        /// <param name="parEntMemberTitle">Entity object as parameter for tableMemberTitle]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void SelectMemberTitleById(entMemberTitle parEntMemberTitle, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntMemberTitle.Id);
            DataTable insDataTable = new DataTable();
            insDataTable = parDbConnector.ExecuteDataTable("SelectMemberTitleById", insDbParamCollection);
            if (insDataTable.Rows.Count > 0)
            {
                if (insDataTable.Rows[0]["Id"] != DBNull.Value)
                {
                    parEntMemberTitle.Id = Convert.ToInt32(insDataTable.Rows[0]["Id"]);
                }
                if (insDataTable.Rows[0]["Name"] != DBNull.Value)
                {
                    parEntMemberTitle.Name = Convert.ToString(insDataTable.Rows[0]["Name"]);
                }
            }
        }

        /// <summary>
        /// Method that inserts one row of data into table [MemberTitle] based on the values of input entity object
        /// </summary>
        /// <param name="parEntMemberTitle">Entity object as parameter for tableMemberTitle]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void InsertMemberTitle(entMemberTitle parEntMemberTitle, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.AddOutput("@pId", DbType.Int32);
            insDbParamCollection.Add("@pName", parEntMemberTitle.Name);
            parDbConnector.ExecuteNonQuery("InsertMemberTitle", insDbParamCollection);
            parEntMemberTitle.Id = Convert.ToInt32(insDbParamCollection.GetOutPutParameter().Value);
        }

        /// <summary>
        /// Method that updates one row of data in table [MemberTitle] based on the values of input entity object
        /// </summary>
        /// <param name="parEntMemberTitle">Entity object as parameter for tableMemberTitle]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void UpdateMemberTitleById(entMemberTitle parEntMemberTitle, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntMemberTitle.Id);
            insDbParamCollection.Add("@pName", parEntMemberTitle.Name);
            parDbConnector.ExecuteNonQuery("UpdateMemberTitleById", insDbParamCollection);
        }

        /// <summary>
        /// Method that deletes all data in table [MemberTitle]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteMemberTitle(DbConnector parDbConnector)
        {
            parDbConnector.ExecuteNonQuery("DeleteMemberTitle", null);
        }

        /// <summary>
        /// Method that deletes one row of data in table [MemberTitle] based on the passed entity object
        /// </summary>
        /// <param name="parEntMemberTitle">Gets entity object as parameter for table MemberTitle]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteMemberTitleById(entMemberTitle parEntMemberTitle, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntMemberTitle.Id);
            parDbConnector.ExecuteNonQuery("DeleteMemberTitleById", insDbParamCollection);
        }

        #endregion
        #region Custom Methods
        #endregion
    }
}
