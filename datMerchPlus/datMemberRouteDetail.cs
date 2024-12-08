using System;
using System.Collections.Generic;
using System.Text;
using SqlHelper;
using System.Data;
using entMerchPlus;

namespace datMerchPlus
{
    /// <summary>
    /// The class that is generated hold parameters to manage database table [MemberRouteDetail]
    /// </summary>
    public class datMemberRouteDetail
    {
        /// <summary>
        /// datMemberRouteDetail Constructor method used while taking an instance of this class.
        /// </summary>
        public datMemberRouteDetail()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Method that selects all data stored in table [MemberRouteDetail]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public DataTable SelectMemberRouteDetail(DbConnector parDbConnector)
        {
            return parDbConnector.ExecuteDataTable("SelectMemberRouteDetail", null);
        }

        /// <summary>
        /// Method that selects one row of data in table [MemberRouteDetail] filtered by the PK field of the inbound entity object
        /// </summary>
        /// <param name="parEntMemberRouteDetail">Entity object as parameter for tableMemberRouteDetail]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void SelectMemberRouteDetailById(entMemberRouteDetail parEntMemberRouteDetail, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntMemberRouteDetail.Id);
            DataTable insDataTable = new DataTable();
            insDataTable = parDbConnector.ExecuteDataTable("SelectMemberRouteDetailById", insDbParamCollection);
            if (insDataTable.Rows.Count > 0)
            {
                if (insDataTable.Rows[0]["Id"] != DBNull.Value)
                {
                    parEntMemberRouteDetail.Id = Convert.ToInt32(insDataTable.Rows[0]["Id"]);
                }
                if (insDataTable.Rows[0]["MemberRouteId"] != DBNull.Value)
                {
                    parEntMemberRouteDetail.MemberRouteId = Convert.ToInt32(insDataTable.Rows[0]["MemberRouteId"]);
                }
                if (insDataTable.Rows[0]["CustomerProductId"] != DBNull.Value)
                {
                    parEntMemberRouteDetail.CustomerProductId = Convert.ToInt32(insDataTable.Rows[0]["CustomerProductId"]);
                }
                if (insDataTable.Rows[0]["ShelfIndex"] != DBNull.Value)
                {
                    parEntMemberRouteDetail.ShelfIndex = Convert.ToInt32(insDataTable.Rows[0]["ShelfIndex"]);
                }
                if (insDataTable.Rows[0]["ShelfStock"] != DBNull.Value)
                {
                    parEntMemberRouteDetail.ShelfStock = Convert.ToInt32(insDataTable.Rows[0]["ShelfStock"]);
                }
                if (insDataTable.Rows[0]["ShelfPrice"] != DBNull.Value)
                {
                    parEntMemberRouteDetail.ShelfPrice = Convert.ToDecimal(insDataTable.Rows[0]["ShelfPrice"]);
                }
                if (insDataTable.Rows[0]["FaceCount"] != DBNull.Value)
                {
                    parEntMemberRouteDetail.FaceCount = Convert.ToInt32(insDataTable.Rows[0]["FaceCount"]);
                }
                if (insDataTable.Rows[0]["PromotionTypeId"] != DBNull.Value)
                {
                    parEntMemberRouteDetail.PromotionTypeId = Convert.ToInt32(insDataTable.Rows[0]["PromotionTypeId"]);
                }
                if (insDataTable.Rows[0]["WarehouseStock"] != DBNull.Value)
                {
                    parEntMemberRouteDetail.WarehouseStock = Convert.ToInt32(insDataTable.Rows[0]["WarehouseStock"]);
                }
                if (insDataTable.Rows[0]["ExpirationDate"] != DBNull.Value)
                {
                    parEntMemberRouteDetail.ExpirationDate = Convert.ToDateTime(insDataTable.Rows[0]["ExpirationDate"]);
                }
                if (insDataTable.Rows[0]["Notes"] != DBNull.Value)
                {
                    parEntMemberRouteDetail.Notes = Convert.ToString(insDataTable.Rows[0]["Notes"]);
                }
                if (insDataTable.Rows[0]["ProfilePicturePath"] != DBNull.Value)
                {
                    parEntMemberRouteDetail.ProfilePicturePath = Convert.ToString(insDataTable.Rows[0]["ProfilePicturePath"]);
                }
                if (insDataTable.Rows[0]["CreatedOn"] != DBNull.Value)
                {
                    parEntMemberRouteDetail.CreatedOn = Convert.ToDateTime(insDataTable.Rows[0]["CreatedOn"]);
                }
                if (insDataTable.Rows[0]["IsSentToServer"] != DBNull.Value)
                {
                    parEntMemberRouteDetail.IsSentToServer = Convert.ToBoolean(insDataTable.Rows[0]["IsSentToServer"]);
                }
                if (insDataTable.Rows[0]["SentToServerOn"] != DBNull.Value)
                {
                    parEntMemberRouteDetail.SentToServerOn = Convert.ToDateTime(insDataTable.Rows[0]["SentToServerOn"]);
                }
                if (insDataTable.Rows[0]["ActivityTypeName"] != DBNull.Value)
                {
                    parEntMemberRouteDetail.ActivityTypeName = Convert.ToString(insDataTable.Rows[0]["ActivityTypeName"]);
                }
                if (insDataTable.Rows[0]["DiscountedPrice"] != DBNull.Value)
                {
                    parEntMemberRouteDetail.DiscountedPrice = Convert.ToDecimal(insDataTable.Rows[0]["DiscountedPrice"]);
                }
            }
        }

        /// <summary>
        /// Method that inserts one row of data into table [MemberRouteDetail] based on the values of input entity object
        /// </summary>
        /// <param name="parEntMemberRouteDetail">Entity object as parameter for tableMemberRouteDetail]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void InsertMemberRouteDetail(entMemberRouteDetail parEntMemberRouteDetail, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.AddOutput("@pId", DbType.Int32);
            insDbParamCollection.Add("@pMemberRouteId", parEntMemberRouteDetail.MemberRouteId);
            insDbParamCollection.Add("@pCustomerProductId", parEntMemberRouteDetail.CustomerProductId);
            insDbParamCollection.Add("@pShelfIndex", parEntMemberRouteDetail.ShelfIndex);
            insDbParamCollection.Add("@pShelfStock", parEntMemberRouteDetail.ShelfStock);
            insDbParamCollection.Add("@pShelfPrice", parEntMemberRouteDetail.ShelfPrice);
            insDbParamCollection.Add("@pFaceCount", parEntMemberRouteDetail.FaceCount);
            insDbParamCollection.Add("@pPromotionTypeId", parEntMemberRouteDetail.PromotionTypeId);
            insDbParamCollection.Add("@pWarehouseStock", parEntMemberRouteDetail.WarehouseStock);
            insDbParamCollection.Add("@pExpirationDate", parEntMemberRouteDetail.ExpirationDate);
            insDbParamCollection.Add("@pNotes", parEntMemberRouteDetail.Notes);
            insDbParamCollection.Add("@pProfilePicturePath", parEntMemberRouteDetail.ProfilePicturePath);
            insDbParamCollection.Add("@pCreatedOn", parEntMemberRouteDetail.CreatedOn);
            insDbParamCollection.Add("@pIsSentToServer", parEntMemberRouteDetail.IsSentToServer);
            insDbParamCollection.Add("@pSentToServerOn", parEntMemberRouteDetail.SentToServerOn);
            insDbParamCollection.Add("@pActivityTypeName", parEntMemberRouteDetail.ActivityTypeName);
            insDbParamCollection.Add("@pDiscountedPrice", parEntMemberRouteDetail.DiscountedPrice);
            parDbConnector.ExecuteNonQuery("InsertMemberRouteDetail", insDbParamCollection);
            parEntMemberRouteDetail.Id = Convert.ToInt32(insDbParamCollection.GetOutPutParameter().Value);
        }

        /// <summary>
        /// Method that updates one row of data in table [MemberRouteDetail] based on the values of input entity object
        /// </summary>
        /// <param name="parEntMemberRouteDetail">Entity object as parameter for tableMemberRouteDetail]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void UpdateMemberRouteDetailById(entMemberRouteDetail parEntMemberRouteDetail, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntMemberRouteDetail.Id);
            insDbParamCollection.Add("@pMemberRouteId", parEntMemberRouteDetail.MemberRouteId);
            insDbParamCollection.Add("@pCustomerProductId", parEntMemberRouteDetail.CustomerProductId);
            insDbParamCollection.Add("@pShelfIndex", parEntMemberRouteDetail.ShelfIndex);
            insDbParamCollection.Add("@pShelfStock", parEntMemberRouteDetail.ShelfStock);
            insDbParamCollection.Add("@pShelfPrice", parEntMemberRouteDetail.ShelfPrice);
            insDbParamCollection.Add("@pFaceCount", parEntMemberRouteDetail.FaceCount);
            insDbParamCollection.Add("@pPromotionTypeId", parEntMemberRouteDetail.PromotionTypeId);
            insDbParamCollection.Add("@pWarehouseStock", parEntMemberRouteDetail.WarehouseStock);
            insDbParamCollection.Add("@pExpirationDate", parEntMemberRouteDetail.ExpirationDate);
            insDbParamCollection.Add("@pNotes", parEntMemberRouteDetail.Notes);
            insDbParamCollection.Add("@pProfilePicturePath", parEntMemberRouteDetail.ProfilePicturePath);
            insDbParamCollection.Add("@pCreatedOn", parEntMemberRouteDetail.CreatedOn);
            insDbParamCollection.Add("@pIsSentToServer", parEntMemberRouteDetail.IsSentToServer);
            insDbParamCollection.Add("@pSentToServerOn", parEntMemberRouteDetail.SentToServerOn);
            insDbParamCollection.Add("@pActivityTypeName", parEntMemberRouteDetail.ActivityTypeName);
            insDbParamCollection.Add("@pDiscountedPrice", parEntMemberRouteDetail.DiscountedPrice);
            parDbConnector.ExecuteNonQuery("UpdateMemberRouteDetailById", insDbParamCollection);
        }

        /// <summary>
        /// Method that deletes all data in table [MemberRouteDetail]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteMemberRouteDetail(DbConnector parDbConnector)
        {
            parDbConnector.ExecuteNonQuery("DeleteMemberRouteDetail", null);
        }

        /// <summary>
        /// Method that deletes one row of data in table [MemberRouteDetail] based on the passed entity object
        /// </summary>
        /// <param name="parEntMemberRouteDetail">Gets entity object as parameter for table MemberRouteDetail]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteMemberRouteDetailById(entMemberRouteDetail parEntMemberRouteDetail, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntMemberRouteDetail.Id);
            parDbConnector.ExecuteNonQuery("DeleteMemberRouteDetailById", insDbParamCollection);
        }

        #endregion
        #region Custom Methods
        public void SelectMemberRouteDetailByRetailShopIdCustomerProductIdLatest(entMemberRoute insEntMemberRoute_CheckLatest, entMemberRouteDetail parEntMemberRouteDetail, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pRetailShopId", insEntMemberRoute_CheckLatest.RetailShopId);
            insDbParamCollection.Add("@pCustomerProductId", parEntMemberRouteDetail.CustomerProductId);
            DataTable insDataTable = new DataTable();
            insDataTable = insDbConnector.ExecuteDataTable("SelectMemberRouteDetailByRetailShopIdCustomerProductIdLatest", insDbParamCollection);
            if (insDataTable.Rows.Count > 0)
            {
                if (insDataTable.Rows[0]["Id"] != DBNull.Value)
                {
                    parEntMemberRouteDetail.Id = Convert.ToInt32(insDataTable.Rows[0]["Id"]);
                }
                if (insDataTable.Rows[0]["MemberRouteId"] != DBNull.Value)
                {
                    parEntMemberRouteDetail.MemberRouteId = Convert.ToInt32(insDataTable.Rows[0]["MemberRouteId"]);
                }
                if (insDataTable.Rows[0]["CustomerProductId"] != DBNull.Value)
                {
                    parEntMemberRouteDetail.CustomerProductId = Convert.ToInt32(insDataTable.Rows[0]["CustomerProductId"]);
                }
                if (insDataTable.Rows[0]["ShelfIndex"] != DBNull.Value)
                {
                    parEntMemberRouteDetail.ShelfIndex = Convert.ToInt32(insDataTable.Rows[0]["ShelfIndex"]);
                }
                if (insDataTable.Rows[0]["ShelfStock"] != DBNull.Value)
                {
                    parEntMemberRouteDetail.ShelfStock = Convert.ToInt32(insDataTable.Rows[0]["ShelfStock"]);
                }
                if (insDataTable.Rows[0]["ShelfPrice"] != DBNull.Value)
                {
                    parEntMemberRouteDetail.ShelfPrice = Convert.ToDecimal(insDataTable.Rows[0]["ShelfPrice"]);
                }
                if (insDataTable.Rows[0]["FaceCount"] != DBNull.Value)
                {
                    parEntMemberRouteDetail.FaceCount = Convert.ToInt32(insDataTable.Rows[0]["FaceCount"]);
                }
                if (insDataTable.Rows[0]["PromotionTypeId"] != DBNull.Value)
                {
                    parEntMemberRouteDetail.PromotionTypeId = Convert.ToInt32(insDataTable.Rows[0]["PromotionTypeId"]);
                }
                if (insDataTable.Rows[0]["WarehouseStock"] != DBNull.Value)
                {
                    parEntMemberRouteDetail.WarehouseStock = Convert.ToInt32(insDataTable.Rows[0]["WarehouseStock"]);
                }
                if (insDataTable.Rows[0]["ExpirationDate"] != DBNull.Value)
                {
                    parEntMemberRouteDetail.ExpirationDate = Convert.ToDateTime(insDataTable.Rows[0]["ExpirationDate"]);
                }
                if (insDataTable.Rows[0]["Notes"] != DBNull.Value)
                {
                    parEntMemberRouteDetail.Notes = Convert.ToString(insDataTable.Rows[0]["Notes"]);
                }
                if (insDataTable.Rows[0]["ProfilePicturePath"] != DBNull.Value)
                {
                    parEntMemberRouteDetail.ProfilePicturePath = Convert.ToString(insDataTable.Rows[0]["ProfilePicturePath"]);
                }
                if (insDataTable.Rows[0]["CreatedOn"] != DBNull.Value)
                {
                    parEntMemberRouteDetail.CreatedOn = Convert.ToDateTime(insDataTable.Rows[0]["CreatedOn"]);
                }
                if (insDataTable.Rows[0]["IsSentToServer"] != DBNull.Value)
                {
                    parEntMemberRouteDetail.IsSentToServer = Convert.ToBoolean(insDataTable.Rows[0]["IsSentToServer"]);
                }
                if (insDataTable.Rows[0]["SentToServerOn"] != DBNull.Value)
                {
                    parEntMemberRouteDetail.SentToServerOn = Convert.ToDateTime(insDataTable.Rows[0]["SentToServerOn"]);
                }
                if (insDataTable.Rows[0]["ActivityTypeName"] != DBNull.Value)
                {
                    parEntMemberRouteDetail.ActivityTypeName = Convert.ToString(insDataTable.Rows[0]["ActivityTypeName"]);
                }
                if (insDataTable.Rows[0]["DiscountedPrice"] != DBNull.Value)
                {
                    parEntMemberRouteDetail.DiscountedPrice = Convert.ToDecimal(insDataTable.Rows[0]["DiscountedPrice"]);
                }
            }
        }

        public void DeleteMemberRouteDetailByMemberRouteId(entMemberRouteDetail insEntMemberRouteDetail_Delete, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pMemberRouteId", insEntMemberRouteDetail_Delete.MemberRouteId);
            insDbConnector.ExecuteNonQuery("DeleteMemberRouteDetailByMemberRouteId", insDbParamCollection);
        }

        public DataTable SelectMemberRouteDetailByMemberRouteIdPromotionTypeId(entMemberRouteDetail insEntMemberRouteDetail, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pMemberRouteId", insEntMemberRouteDetail.MemberRouteId);
            insDbParamCollection.Add("@pPromotionTypeId", insEntMemberRouteDetail.PromotionTypeId);
            return insDbConnector.ExecuteDataTable("SelectMemberRouteDetailByMemberRouteIdPromotionTypeId", insDbParamCollection);
        }

        public DataTable SelectMemberRouteDetailByMemberRouteIdStartCustomerProductIdRetailShopIdNumberOfRecords(int memberRouteId, int customerProductId, int retailShopId, int numberOfHistoryRecords, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pMemberRouteId", memberRouteId);
            insDbParamCollection.Add("@pCustomerProductId", customerProductId);
            insDbParamCollection.Add("@pRetailShopId", retailShopId);
            insDbParamCollection.Add("@pNumberOfRows", numberOfHistoryRecords);
            return insDbConnector.ExecuteDataTable("SelectMemberRouteDetailByMemberRouteIdStartCustomerProductIdRetailShopIdNumberOfRecords", insDbParamCollection);
        }

        public DataTable SelectMemberRouteDetailByCustomerIdStartDateEndDateGridDataForEdit(DateTime startDate, DateTime endDate, int customerId, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pStartDate", startDate);
            insDbParamCollection.Add("@pEndDate", endDate);
            insDbParamCollection.Add("@pCustomerId", customerId);
            return insDbConnector.ExecuteDataTable("SelectMemberRouteDetailByCustomerIdStartDateEndDateGridDataForEdit", insDbParamCollection);
        }

        public void DeleteMemberRouteDetailByCustomerProductId(entMemberRouteDetail insEntMemberRouteDetail, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pCustomerProductId", insEntMemberRouteDetail.CustomerProductId);
            insDbConnector.ExecuteNonQuery("DeleteMemberRouteDetailByCustomerProductId", insDbParamCollection);
        }
        #endregion
    }
}