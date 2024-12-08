using System;
using System.Collections.Generic;
using System.Text;
using SqlHelper;
using System.Data;
using entMerchPlus;

namespace datMerchPlus
{
    /// <summary>
    /// The class that is generated hold parameters to manage database table [MemberDistrict]
    /// </summary>
    public class datMemberDistrict
    {
        /// <summary>
        /// datMemberDistrict Constructor method used while taking an instance of this class.
        /// </summary>
        public datMemberDistrict()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Method that selects all data stored in table [MemberDistrict]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public DataTable SelectMemberDistrict(DbConnector parDbConnector)
        {
            return parDbConnector.ExecuteDataTable("SelectMemberDistrict", null);
        }

        /// <summary>
        /// Method that selects one row of data in table [MemberDistrict] filtered by the PK field of the inbound entity object
        /// </summary>
        /// <param name="parEntMemberDistrict">Entity object as parameter for tableMemberDistrict]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void SelectMemberDistrictById(entMemberDistrict parEntMemberDistrict, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntMemberDistrict.Id);
            DataTable insDataTable = new DataTable();
            insDataTable = parDbConnector.ExecuteDataTable("SelectMemberDistrictById", insDbParamCollection);
            if (insDataTable.Rows.Count > 0)
            {
                if (insDataTable.Rows[0]["Id"] != DBNull.Value)
                {
                    parEntMemberDistrict.Id = Convert.ToInt32(insDataTable.Rows[0]["Id"]);
                }
                if (insDataTable.Rows[0]["MemberId"] != DBNull.Value)
                {
                    parEntMemberDistrict.MemberId = Convert.ToString(insDataTable.Rows[0]["MemberId"]);
                }
                if (insDataTable.Rows[0]["DistrictId"] != DBNull.Value)
                {
                    parEntMemberDistrict.DistrictId = Convert.ToInt32(insDataTable.Rows[0]["DistrictId"]);
                }
            }
        }

        /// <summary>
        /// Method that inserts one row of data into table [MemberDistrict] based on the values of input entity object
        /// </summary>
        /// <param name="parEntMemberDistrict">Entity object as parameter for tableMemberDistrict]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void InsertMemberDistrict(entMemberDistrict parEntMemberDistrict, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.AddOutput("@pId", DbType.Int32);
            insDbParamCollection.Add("@pMemberId", parEntMemberDistrict.MemberId);
            insDbParamCollection.Add("@pDistrictId", parEntMemberDistrict.DistrictId);
            parDbConnector.ExecuteNonQuery("InsertMemberDistrict", insDbParamCollection);
            parEntMemberDistrict.Id = Convert.ToInt32(insDbParamCollection.GetOutPutParameter().Value);
        }

        /// <summary>
        /// Method that updates one row of data in table [MemberDistrict] based on the values of input entity object
        /// </summary>
        /// <param name="parEntMemberDistrict">Entity object as parameter for tableMemberDistrict]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void UpdateMemberDistrictById(entMemberDistrict parEntMemberDistrict, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntMemberDistrict.Id);
            insDbParamCollection.Add("@pMemberId", parEntMemberDistrict.MemberId);
            insDbParamCollection.Add("@pDistrictId", parEntMemberDistrict.DistrictId);
            parDbConnector.ExecuteNonQuery("UpdateMemberDistrictById", insDbParamCollection);
        }

        /// <summary>
        /// Method that deletes all data in table [MemberDistrict]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteMemberDistrict(DbConnector parDbConnector)
        {
            parDbConnector.ExecuteNonQuery("DeleteMemberDistrict", null);
        }

        /// <summary>
        /// Method that deletes one row of data in table [MemberDistrict] based on the passed entity object
        /// </summary>
        /// <param name="parEntMemberDistrict">Gets entity object as parameter for table MemberDistrict]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteMemberDistrictById(entMemberDistrict parEntMemberDistrict, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntMemberDistrict.Id);
            parDbConnector.ExecuteNonQuery("DeleteMemberDistrictById", insDbParamCollection);
        }

        #endregion
        #region Custom Methods
        public void DeleteMemberDistrictByMemberId(entMemberDistrict insEntMemberDistrict, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pMemberId", insEntMemberDistrict.MemberId);
            insDbConnector.ExecuteNonQuery("DeleteMemberDistrictByMemberId", insDbParamCollection);
        }

        public DataTable SelectMemberDistrictByMemberId(entMemberCustomer insEntMemberCustomer, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pMemberId", insEntMemberCustomer.MemberId);
            return insDbConnector.ExecuteDataTable("SelectMemberDistrictByMemberId", insDbParamCollection);
        }
        #endregion
    }
}
