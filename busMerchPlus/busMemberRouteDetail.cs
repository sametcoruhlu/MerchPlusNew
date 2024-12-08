using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using SqlHelper;
using System.Data;
using entMerchPlus;
using datMerchPlus;
using Infrastructure;

namespace busMerchPlus
{
    /// <summary>
    /// This layer is responsible for implementing business logic.
    /// Manages transactions on table [MemberRouteDetail]
    /// </summary>
    public class busMemberRouteDetail : BusBase
    {
        /// <summary>
        /// busMemberRouteDetailConstructor method used while taking an instance of this class.
        /// </summary>
        public busMemberRouteDetail() : base()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Selects all data stored in table [MemberRouteDetail]
        /// </summary>
        public DataTable SelectMemberRouteDetail()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberRouteDetail insDatMemberRouteDetail = new datMemberRouteDetail();
                return insDatMemberRouteDetail.SelectMemberRouteDetail(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Selects one row of data stored in table [MemberRouteDetail] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntMemberRouteDetail">Gets entity object as parameter for table MemberRouteDetail]</param>
        public void SelectMemberRouteDetailById(entMemberRouteDetail parEntMemberRouteDetail)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberRouteDetail insDatMemberRouteDetail = new datMemberRouteDetail();
                insDatMemberRouteDetail.SelectMemberRouteDetailById(parEntMemberRouteDetail, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Inserts one row of data into table [MemberRouteDetail] based on the values of input entity object
        /// </summary>
        /// <param name="parEntMemberRouteDetail">Gets entity object as parameter for table MemberRouteDetail]</param>
        public void InsertMemberRouteDetail(entMemberRouteDetail parEntMemberRouteDetail)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberRouteDetail insDatMemberRouteDetail = new datMemberRouteDetail();
                insDatMemberRouteDetail.InsertMemberRouteDetail(parEntMemberRouteDetail, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Updates one row of data in table [MemberRouteDetail] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntMemberRouteDetail">Gets entity object as parameter for table MemberRouteDetail]</param>
        public void UpdateMemberRouteDetailById(entMemberRouteDetail parEntMemberRouteDetail)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberRouteDetail insDatMemberRouteDetail = new datMemberRouteDetail();
                insDatMemberRouteDetail.UpdateMemberRouteDetailById(parEntMemberRouteDetail, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes all data stored in table [MemberRouteDetail]
        /// </summary>
        public void DeleteMemberRouteDetail()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberRouteDetail insDatMemberRouteDetail = new datMemberRouteDetail();
                insDatMemberRouteDetail.DeleteMemberRouteDetail(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes data stored in table [MemberRouteDetail] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntMemberRouteDetail">Gets entity object as parameter for table MemberRouteDetail]</param>
        public void DeleteMemberRouteDetailById(entMemberRouteDetail parEntMemberRouteDetail)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberRouteDetail insDatMemberRouteDetail = new datMemberRouteDetail();
                insDatMemberRouteDetail.DeleteMemberRouteDetailById(parEntMemberRouteDetail, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        #endregion
        #region Custom Methods
        public void CreateMemberRouteDetailWiselyByMemberRouteId(entMemberRouteDetail insEntMemberRouteDetail)
        {
            DbConnector insDbConnector = new DbConnector();
            datMember insDatMember = new datMember();
            datMemberRoute insDatMemberRoute = new datMemberRoute();
            datMemberRouteDetail insDatMemberRouteDetail = new datMemberRouteDetail();
            datRetailShop insDatRetailShop = new datRetailShop();
            datCustomerProduct insDatCustomerProduct = new datCustomerProduct();
            datCustomerProductRetailShop insDatCustomerProductRetailShop = new datCustomerProductRetailShop();
            datCustomerProductCompetitor insDatCustomerProductCompetitor = new datCustomerProductCompetitor();
            datMemberCustomer insDatMemberCustomer = new datMemberCustomer();
            try
            {
                insDbConnector.BeginTransaction();

                #region Get MemberRoute
                entMemberRoute insEntMemberRoute = new entMemberRoute();
                insEntMemberRoute.Id = Convert.ToInt32(insEntMemberRouteDetail.MemberRouteId);
                insDatMemberRoute.SelectMemberRouteById(insEntMemberRoute, insDbConnector);
                #endregion

                #region Delete MemberRouteDetail for this MemberRouteId
                entMemberRouteDetail insEntMemberRouteDetail_Delete = new entMemberRouteDetail();
                insEntMemberRouteDetail_Delete.MemberRouteId = Convert.ToInt32(insEntMemberRoute.Id);
                insDatMemberRouteDetail.DeleteMemberRouteDetailByMemberRouteId(insEntMemberRouteDetail_Delete, insDbConnector);
                #endregion

                #region Get MemberCustomer By MemberId
                entMemberCustomer insEntMemberCustomer = new entMemberCustomer();
                insEntMemberCustomer.MemberId = insEntMemberRoute.MemberId;
                insDatMemberCustomer.SelectMemberCustomerByMemberIdFirstMatch(insEntMemberCustomer, insDbConnector);
                #endregion

                #region Get all CustomerProduct for CustomerId in this RetailShop
                entCustomerProductRetailShop insEntCustomerProductRetailShop = new entCustomerProductRetailShop();
                insEntCustomerProductRetailShop.CustomerId = Convert.ToInt32(insEntMemberCustomer.CustomerId);
                insEntCustomerProductRetailShop.RetailShopId = insEntMemberRoute.RetailShopId;
                DataTable insDt_Products = new DataTable();
                insDt_Products = insDatCustomerProductRetailShop.SelectCustomerProductRetailShopByRetailShopIdCustomerId(insEntCustomerProductRetailShop, insDbConnector);
                #endregion

                #region Loop each product and add to MemberRouteDetail table
                foreach (DataRow insDr_Product in insDt_Products.Rows)
                {
                    entMemberRouteDetail insEntMemberRouteDetail_NewMaster = new entMemberRouteDetail();
                    insEntMemberRouteDetail_NewMaster.CustomerProductId = Convert.ToInt32(insDr_Product["CustomerProductId"]);

                    #region Get last input to this product in this shop
                    entMemberRoute insEntMemberRoute_CheckLatest = new entMemberRoute();
                    entMemberRouteDetail insEntMemberRouteDetail_CheckLatest = new entMemberRouteDetail();
                    insEntMemberRoute_CheckLatest.RetailShopId = insEntMemberRoute.RetailShopId;
                    insEntMemberRouteDetail_CheckLatest.CustomerProductId = Convert.ToInt32(insDr_Product["CustomerProductId"]);
                    insDatMemberRouteDetail.SelectMemberRouteDetailByRetailShopIdCustomerProductIdLatest(insEntMemberRoute_CheckLatest, insEntMemberRouteDetail_CheckLatest, insDbConnector);
                    #endregion

                    if (insEntMemberRouteDetail_CheckLatest.Id != 0)
                    {
                        #region Existing Data from past. Copy with new values
                        insEntMemberRouteDetail_NewMaster.CustomerProductId = insEntMemberRouteDetail_CheckLatest.CustomerProductId;
                        insEntMemberRouteDetail_NewMaster.ExpirationDate = insEntMemberRouteDetail_CheckLatest.ExpirationDate;
                        insEntMemberRouteDetail_NewMaster.FaceCount = insEntMemberRouteDetail_CheckLatest.FaceCount;
                        insEntMemberRouteDetail_NewMaster.IsSentToServer = false;
                        insEntMemberRouteDetail_NewMaster.MemberRouteId = insEntMemberRoute.Id;
                        insEntMemberRouteDetail_NewMaster.Notes = insEntMemberRouteDetail_CheckLatest.Notes;
                        insEntMemberRouteDetail_NewMaster.PromotionTypeId = insEntMemberRouteDetail_CheckLatest.PromotionTypeId;
                        insEntMemberRouteDetail_NewMaster.ShelfIndex = insEntMemberRouteDetail_CheckLatest.ShelfIndex;
                        insEntMemberRouteDetail_NewMaster.ShelfPrice = insEntMemberRouteDetail_CheckLatest.ShelfPrice;
                        insEntMemberRouteDetail_NewMaster.ShelfStock = insEntMemberRouteDetail_CheckLatest.ShelfStock;
                        insEntMemberRouteDetail_NewMaster.WarehouseStock = insEntMemberRouteDetail_CheckLatest.WarehouseStock;
                        insDatMemberRouteDetail.InsertMemberRouteDetail(insEntMemberRouteDetail_NewMaster, insDbConnector);
                        #endregion
                    }
                    else
                    {
                        #region No data from past. Create new
                        insEntMemberRouteDetail_NewMaster.IsSentToServer = false;
                        insEntMemberRouteDetail_NewMaster.MemberRouteId = insEntMemberRoute.Id;
                        insDatMemberRouteDetail.InsertMemberRouteDetail(insEntMemberRouteDetail_NewMaster, insDbConnector);
                        #endregion
                    }
                }
                #endregion

                #region Loop each product and find it's competitors and add to MemberRouteDetail table
                foreach (DataRow insDr_Product in insDt_Products.Rows)
                {
                    entCustomerProductCompetitor insEntCustomerProductCompetitor = new entCustomerProductCompetitor();
                    insEntCustomerProductCompetitor.CustomerProductId = Convert.ToInt32(insDr_Product["CustomerProductId"]);
                    DataTable insDt_Competitor = new DataTable();
                    insDt_Competitor = insDatCustomerProductCompetitor.SelectCustomerProductCompetitorByCustomerProductId(insEntCustomerProductCompetitor, insDbConnector);

                    foreach (DataRow insDr_Competitor in insDt_Competitor.Rows)
                    {

                        entMemberRouteDetail insEntMemberRouteDetail_NewMaster = new entMemberRouteDetail();
                        insEntMemberRouteDetail_NewMaster.CustomerProductId = Convert.ToInt32(insDr_Competitor["CompetitorCustomerProductId"]);

                        #region Get last input to this product in this shop
                        entMemberRoute insEntMemberRoute_CheckLatest = new entMemberRoute();
                        entMemberRouteDetail insEntMemberRouteDetail_CheckLatest = new entMemberRouteDetail();
                        insEntMemberRoute_CheckLatest.RetailShopId = insEntMemberRoute.RetailShopId;
                        insEntMemberRouteDetail_CheckLatest.CustomerProductId = Convert.ToInt32(insDr_Competitor["CompetitorCustomerProductId"]);
                        insDatMemberRouteDetail.SelectMemberRouteDetailByRetailShopIdCustomerProductIdLatest(insEntMemberRoute_CheckLatest, insEntMemberRouteDetail_CheckLatest, insDbConnector);
                        #endregion

                        if (insEntMemberRouteDetail_CheckLatest.Id != 0)
                        {
                            #region Existing Data from past. Copy with new values
                            insEntMemberRouteDetail_NewMaster.CustomerProductId = insEntMemberRouteDetail_CheckLatest.CustomerProductId;
                            insEntMemberRouteDetail_NewMaster.ExpirationDate = insEntMemberRouteDetail_CheckLatest.ExpirationDate;
                            insEntMemberRouteDetail_NewMaster.FaceCount = insEntMemberRouteDetail_CheckLatest.FaceCount;
                            insEntMemberRouteDetail_NewMaster.IsSentToServer = false;
                            insEntMemberRouteDetail_NewMaster.MemberRouteId = insEntMemberRoute.Id;
                            insEntMemberRouteDetail_NewMaster.Notes = insEntMemberRouteDetail_CheckLatest.Notes;
                            insEntMemberRouteDetail_NewMaster.PromotionTypeId = insEntMemberRouteDetail_CheckLatest.PromotionTypeId;
                            insEntMemberRouteDetail_NewMaster.ShelfIndex = insEntMemberRouteDetail_CheckLatest.ShelfIndex;
                            insEntMemberRouteDetail_NewMaster.ShelfPrice = insEntMemberRouteDetail_CheckLatest.ShelfPrice;
                            insEntMemberRouteDetail_NewMaster.ShelfStock = insEntMemberRouteDetail_CheckLatest.ShelfStock;
                            insEntMemberRouteDetail_NewMaster.WarehouseStock = insEntMemberRouteDetail_CheckLatest.WarehouseStock;
                            insDatMemberRouteDetail.InsertMemberRouteDetail(insEntMemberRouteDetail_NewMaster, insDbConnector);
                            #endregion
                        }
                        else
                        {
                            #region No data from past. Create new
                            insEntMemberRouteDetail_NewMaster.IsSentToServer = false;
                            insEntMemberRouteDetail_NewMaster.MemberRouteId = insEntMemberRoute.Id;
                            insDatMemberRouteDetail.InsertMemberRouteDetail(insEntMemberRouteDetail_NewMaster, insDbConnector);
                            #endregion
                        }

                    }
                }
                #endregion

                insDbConnector.CommitTransaction();
            }
            catch (Exception ex)
            {
                insDbConnector.RollbackTransaction();
                this.ErrorMessage = ex.ToString();
            }
        }

        public DataTable SelectMemberRouteDetailByMemberRouteIdPromotionTypeId(entMemberRouteDetail insEntMemberRouteDetail)
        {
            DbConnector insDbConnector = new DbConnector();
            datMemberRouteDetail insDatMemberRouteDetail = new datMemberRouteDetail();
            try
            {
                return insDatMemberRouteDetail.SelectMemberRouteDetailByMemberRouteIdPromotionTypeId(insEntMemberRouteDetail, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public DataTable SelectMemberRouteDetailByMemberRouteIdStartCustomerProductIdRetailShopIdNumberOfRecords(int memberRouteId, int customerProductId, int retailShopId, int numberOfHistoryRecords)
        {
            DbConnector insDbConnector = new DbConnector();
            datMemberRouteDetail insDatMemberRouteDetail = new datMemberRouteDetail();
            try
            {
                return insDatMemberRouteDetail.SelectMemberRouteDetailByMemberRouteIdStartCustomerProductIdRetailShopIdNumberOfRecords(memberRouteId, customerProductId, retailShopId, numberOfHistoryRecords, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public DataTable SelectMemberRouteDetailByCustomerIdStartDateEndDateGridDataForEdit(DateTime StartDate, DateTime EndDate, int CustomerId)
        {
            DbConnector insDbConnector = new DbConnector();
            datMemberRouteDetail insDatMemberRouteDetail = new datMemberRouteDetail();
            try
            {
                return insDatMemberRouteDetail.SelectMemberRouteDetailByCustomerIdStartDateEndDateGridDataForEdit(StartDate, EndDate, CustomerId, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }
        #endregion
    }
}
