using System;
using System.Collections.Generic;
using System.Text;
using SqlHelper;
using System.Data;
using entMerchPlus;

namespace datMerchPlus
{
    /// <summary>
    /// The class that is generated hold parameters to manage database table [ProductExpiration]
    /// </summary>
    public class datProductExpiration
    {
        /// <summary>
        /// datProductExpiration Constructor method used while taking an instance of this class.
        /// </summary>
        public datProductExpiration()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Method that selects all data stored in table [ProductExpiration]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public DataTable SelectProductExpiration(DbConnector parDbConnector)
        {
            return parDbConnector.ExecuteDataTable("SelectProductExpiration", null);
        }

        /// <summary>
        /// Method that selects one row of data in table [ProductExpiration] filtered by the PK field of the inbound entity object
        /// </summary>
        /// <param name="parEntProductExpiration">Entity object as parameter for tableProductExpiration]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void SelectProductExpirationById(entProductExpiration parEntProductExpiration, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntProductExpiration.Id);
            DataTable insDataTable = new DataTable();
            insDataTable = parDbConnector.ExecuteDataTable("SelectProductExpirationById", insDbParamCollection);
            if (insDataTable.Rows.Count > 0)
            {
                if (insDataTable.Rows[0]["Id"] != DBNull.Value)
                {
                    parEntProductExpiration.Id = Convert.ToInt32(insDataTable.Rows[0]["Id"]);
                }
                if (insDataTable.Rows[0]["MemberId"] != DBNull.Value)
                {
                    parEntProductExpiration.MemberId = Convert.ToString(insDataTable.Rows[0]["MemberId"]);
                }
                if (insDataTable.Rows[0]["CustomerId"] != DBNull.Value)
                {
                    parEntProductExpiration.CustomerId = Convert.ToInt32(insDataTable.Rows[0]["CustomerId"]);
                }
                if (insDataTable.Rows[0]["MemberRouteId"] != DBNull.Value)
                {
                    parEntProductExpiration.MemberRouteId = Convert.ToInt32(insDataTable.Rows[0]["MemberRouteId"]);
                }
                if (insDataTable.Rows[0]["CustomerProductId"] != DBNull.Value)
                {
                    parEntProductExpiration.CustomerProductId = Convert.ToInt32(insDataTable.Rows[0]["CustomerProductId"]);
                }
                if (insDataTable.Rows[0]["Quantity"] != DBNull.Value)
                {
                    parEntProductExpiration.Quantity = Convert.ToInt32(insDataTable.Rows[0]["Quantity"]);
                }
                if (insDataTable.Rows[0]["ExpirationDate"] != DBNull.Value)
                {
                    parEntProductExpiration.ExpirationDate = Convert.ToDateTime(insDataTable.Rows[0]["ExpirationDate"]);
                }
                if (insDataTable.Rows[0]["CreatedOn"] != DBNull.Value)
                {
                    parEntProductExpiration.CreatedOn = Convert.ToDateTime(insDataTable.Rows[0]["CreatedOn"]);
                }
                if (insDataTable.Rows[0]["IsSentToServer"] != DBNull.Value)
                {
                    parEntProductExpiration.IsSentToServer = Convert.ToBoolean(insDataTable.Rows[0]["IsSentToServer"]);
                }
                if (insDataTable.Rows[0]["SentToServerOn"] != DBNull.Value)
                {
                    parEntProductExpiration.SentToServerOn = Convert.ToDateTime(insDataTable.Rows[0]["SentToServerOn"]);
                }
            }
        }

        /// <summary>
        /// Method that inserts one row of data into table [ProductExpiration] based on the values of input entity object
        /// </summary>
        /// <param name="parEntProductExpiration">Entity object as parameter for tableProductExpiration]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void InsertProductExpiration(entProductExpiration parEntProductExpiration, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.AddOutput("@pId", DbType.Int32);
            insDbParamCollection.Add("@pMemberId", parEntProductExpiration.MemberId);
            insDbParamCollection.Add("@pCustomerId", parEntProductExpiration.CustomerId);
            insDbParamCollection.Add("@pMemberRouteId", parEntProductExpiration.MemberRouteId);
            insDbParamCollection.Add("@pCustomerProductId", parEntProductExpiration.CustomerProductId);
            insDbParamCollection.Add("@pQuantity", parEntProductExpiration.Quantity);
            insDbParamCollection.Add("@pExpirationDate", parEntProductExpiration.ExpirationDate);
            insDbParamCollection.Add("@pCreatedOn", parEntProductExpiration.CreatedOn);
            insDbParamCollection.Add("@pIsSentToServer", parEntProductExpiration.IsSentToServer);
            insDbParamCollection.Add("@pSentToServerOn", parEntProductExpiration.SentToServerOn);
            parDbConnector.ExecuteNonQuery("InsertProductExpiration", insDbParamCollection);
            parEntProductExpiration.Id = Convert.ToInt32(insDbParamCollection.GetOutPutParameter().Value);
        }

        /// <summary>
        /// Method that updates one row of data in table [ProductExpiration] based on the values of input entity object
        /// </summary>
        /// <param name="parEntProductExpiration">Entity object as parameter for tableProductExpiration]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void UpdateProductExpirationById(entProductExpiration parEntProductExpiration, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntProductExpiration.Id);
            insDbParamCollection.Add("@pMemberId", parEntProductExpiration.MemberId);
            insDbParamCollection.Add("@pCustomerId", parEntProductExpiration.CustomerId);
            insDbParamCollection.Add("@pMemberRouteId", parEntProductExpiration.MemberRouteId);
            insDbParamCollection.Add("@pCustomerProductId", parEntProductExpiration.CustomerProductId);
            insDbParamCollection.Add("@pQuantity", parEntProductExpiration.Quantity);
            insDbParamCollection.Add("@pExpirationDate", parEntProductExpiration.ExpirationDate);
            insDbParamCollection.Add("@pCreatedOn", parEntProductExpiration.CreatedOn);
            insDbParamCollection.Add("@pIsSentToServer", parEntProductExpiration.IsSentToServer);
            insDbParamCollection.Add("@pSentToServerOn", parEntProductExpiration.SentToServerOn);
            parDbConnector.ExecuteNonQuery("UpdateProductExpirationById", insDbParamCollection);
        }

        /// <summary>
        /// Method that deletes all data in table [ProductExpiration]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteProductExpiration(DbConnector parDbConnector)
        {
            parDbConnector.ExecuteNonQuery("DeleteProductExpiration", null);
        }

        /// <summary>
        /// Method that deletes one row of data in table [ProductExpiration] based on the passed entity object
        /// </summary>
        /// <param name="parEntProductExpiration">Gets entity object as parameter for table ProductExpiration]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteProductExpirationById(entProductExpiration parEntProductExpiration, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntProductExpiration.Id);
            parDbConnector.ExecuteNonQuery("DeleteProductExpirationById", insDbParamCollection);
        }

        #endregion
        #region Custom Methods
        #endregion
    }
}
