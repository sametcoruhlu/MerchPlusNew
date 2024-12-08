using System;
using System.Collections.Generic;
using System.Text;
using SqlHelper;
using System.Data;
using entMerchPlus;

namespace datMerchPlus
{
    /// <summary>
    /// The class that is generated hold parameters to manage database table [MemberCustomer]
    /// </summary>
    public class datMemberCustomer
    {
        /// <summary>
        /// datMemberCustomer Constructor method used while taking an instance of this class.
        /// </summary>
        public datMemberCustomer()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Method that selects all data stored in table [MemberCustomer]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public DataTable SelectMemberCustomer(DbConnector parDbConnector)
        {
            return parDbConnector.ExecuteDataTable("SelectMemberCustomer", null);
        }

        /// <summary>
        /// Method that selects one row of data in table [MemberCustomer] filtered by the PK field of the inbound entity object
        /// </summary>
        /// <param name="parEntMemberCustomer">Entity object as parameter for tableMemberCustomer]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void SelectMemberCustomerById(entMemberCustomer parEntMemberCustomer, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntMemberCustomer.Id);
            DataTable insDataTable = new DataTable();
            insDataTable = parDbConnector.ExecuteDataTable("SelectMemberCustomerById", insDbParamCollection);
            if (insDataTable.Rows.Count > 0)
            {
                if (insDataTable.Rows[0]["Id"] != DBNull.Value)
                {
                    parEntMemberCustomer.Id = Convert.ToInt32(insDataTable.Rows[0]["Id"]);
                }
                if (insDataTable.Rows[0]["MemberId"] != DBNull.Value)
                {
                    parEntMemberCustomer.MemberId = Convert.ToString(insDataTable.Rows[0]["MemberId"]);
                }
                if (insDataTable.Rows[0]["CustomerId"] != DBNull.Value)
                {
                    parEntMemberCustomer.CustomerId = Convert.ToInt32(insDataTable.Rows[0]["CustomerId"]);
                }
            }
        }

        /// <summary>
        /// Method that inserts one row of data into table [MemberCustomer] based on the values of input entity object
        /// </summary>
        /// <param name="parEntMemberCustomer">Entity object as parameter for tableMemberCustomer]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void InsertMemberCustomer(entMemberCustomer parEntMemberCustomer, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.AddOutput("@pId", DbType.Int32);
            insDbParamCollection.Add("@pMemberId", parEntMemberCustomer.MemberId);
            insDbParamCollection.Add("@pCustomerId", parEntMemberCustomer.CustomerId);
            parDbConnector.ExecuteNonQuery("InsertMemberCustomer", insDbParamCollection);
            parEntMemberCustomer.Id = Convert.ToInt32(insDbParamCollection.GetOutPutParameter().Value);
        }

        /// <summary>
        /// Method that updates one row of data in table [MemberCustomer] based on the values of input entity object
        /// </summary>
        /// <param name="parEntMemberCustomer">Entity object as parameter for tableMemberCustomer]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void UpdateMemberCustomerById(entMemberCustomer parEntMemberCustomer, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntMemberCustomer.Id);
            insDbParamCollection.Add("@pMemberId", parEntMemberCustomer.MemberId);
            insDbParamCollection.Add("@pCustomerId", parEntMemberCustomer.CustomerId);
            parDbConnector.ExecuteNonQuery("UpdateMemberCustomerById", insDbParamCollection);
        }

        /// <summary>
        /// Method that deletes all data in table [MemberCustomer]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteMemberCustomer(DbConnector parDbConnector)
        {
            parDbConnector.ExecuteNonQuery("DeleteMemberCustomer", null);
        }

        /// <summary>
        /// Method that deletes one row of data in table [MemberCustomer] based on the passed entity object
        /// </summary>
        /// <param name="parEntMemberCustomer">Gets entity object as parameter for table MemberCustomer]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteMemberCustomerById(entMemberCustomer parEntMemberCustomer, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntMemberCustomer.Id);
            parDbConnector.ExecuteNonQuery("DeleteMemberCustomerById", insDbParamCollection);
        }

        #endregion
        #region Custom Methods
        public DataTable SelectMemberCustomerByMemberIdGridData(entMemberCustomer insEntMemberCustomer, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pMemberId", insEntMemberCustomer.MemberId);
            return insDbConnector.ExecuteDataTable("SelectMemberCustomerByMemberIdGridData", insDbParamCollection);
        }

        public void DeleteMemberCustomerByMemberId(entMemberCustomer insEntMemberCustomer, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pMemberId", insEntMemberCustomer.MemberId);
            insDbConnector.ExecuteNonQuery("DeleteMemberCustomerByMemberId", insDbParamCollection);
        }

        public void SelectMemberCustomerByMemberIdFirstMatch(entMemberCustomer parEntMemberCustomer, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pMemberId", parEntMemberCustomer.MemberId);
            DataTable insDataTable = new DataTable();
            insDataTable = parDbConnector.ExecuteDataTable("SelectMemberCustomerByMemberIdFirstMatch", insDbParamCollection);
            if (insDataTable.Rows.Count > 0)
            {
                if (insDataTable.Rows[0]["Id"] != DBNull.Value)
                {
                    parEntMemberCustomer.Id = Convert.ToInt32(insDataTable.Rows[0]["Id"]);
                }
                if (insDataTable.Rows[0]["MemberId"] != DBNull.Value)
                {
                    parEntMemberCustomer.MemberId = Convert.ToString(insDataTable.Rows[0]["MemberId"]);
                }
                if (insDataTable.Rows[0]["CustomerId"] != DBNull.Value)
                {
                    parEntMemberCustomer.CustomerId = Convert.ToInt32(insDataTable.Rows[0]["CustomerId"]);
                }
            }
        }
        #endregion
    }
}