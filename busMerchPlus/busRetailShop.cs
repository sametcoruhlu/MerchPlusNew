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
    /// Manages transactions on table [RetailShop]
    /// </summary>
    public class busRetailShop : BusBase
    {
        /// <summary>
        /// busRetailShopConstructor method used while taking an instance of this class.
        /// </summary>
        public busRetailShop() : base()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Selects all data stored in table [RetailShop]
        /// </summary>
        public DataTable SelectRetailShop()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datRetailShop insDatRetailShop = new datRetailShop();
                return insDatRetailShop.SelectRetailShop(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        

        /// <summary>
        /// Selects one row of data stored in table [RetailShop] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntRetailShop">Gets entity object as parameter for table RetailShop]</param>
        public void SelectRetailShopById(entRetailShop parEntRetailShop)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datRetailShop insDatRetailShop = new datRetailShop();
                insDatRetailShop.SelectRetailShopById(parEntRetailShop, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Inserts one row of data into table [RetailShop] based on the values of input entity object
        /// </summary>
        /// <param name="parEntRetailShop">Gets entity object as parameter for table RetailShop]</param>
        public void InsertRetailShop(entRetailShop parEntRetailShop)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datRetailShop insDatRetailShop = new datRetailShop();
                insDatRetailShop.InsertRetailShop(parEntRetailShop, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Updates one row of data in table [RetailShop] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntRetailShop">Gets entity object as parameter for table RetailShop]</param>
        public void UpdateRetailShopById(entRetailShop parEntRetailShop)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datRetailShop insDatRetailShop = new datRetailShop();
                insDatRetailShop.UpdateRetailShopById(parEntRetailShop, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes all data stored in table [RetailShop]
        /// </summary>
        public void DeleteRetailShop()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datRetailShop insDatRetailShop = new datRetailShop();
                insDatRetailShop.DeleteRetailShop(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes data stored in table [RetailShop] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntRetailShop">Gets entity object as parameter for table RetailShop]</param>
        public void DeleteRetailShopById(entRetailShop parEntRetailShop)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datRetailShop insDatRetailShop = new datRetailShop();
                insDatRetailShop.DeleteRetailShopById(parEntRetailShop, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        #endregion
        #region Custom Methods
        public DataTable SelectRetailShopMapView()
        {
            DbConnector insDbConnector = new DbConnector();
            datRetailShop insDatRetailShop = new datRetailShop();
            try
            {
                return insDatRetailShop.SelectRetailShopMapView(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public DataTable SelectRetailShopByMemberIdMemberRouteDate(entMemberRoute parEntMemberRoute)
        {
            DbConnector insDbConnector = new DbConnector();
            datRetailShop insDatRetailShop = new datRetailShop();
            try
            {
                return insDatRetailShop.SelectRetailShopByMemberIdMemberRouteDate(parEntMemberRoute, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public DataTable SelectRetailShopMapViewByMemberIdEffectiveDate(string memberId, DateTime dateTime)
        {
            DbConnector insDbConnector = new DbConnector();
            datRetailShop insDatRetailShop = new datRetailShop();
            try
            {
                return insDatRetailShop.SelectRetailShopMapViewByMemberIdEffectiveDate(memberId, dateTime, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public void DeleteRetailShopByIdWisely(entRetailShop insEntRetailShop)
        {
            DbConnector insDbConnector = new DbConnector();
            datRetailShop insDatRetailShop = new datRetailShop();
            datCustomerProductRetailShop insDatCustomerProductRetailShop = new datCustomerProductRetailShop();
            datMemberRoute insDatMemberRoute = new datMemberRoute();
            datMemberRouteDetail insDatMemberRouteDetail = new datMemberRouteDetail();
            try
            {
                insDbConnector.BeginTransaction();

                #region Get RetailShop
                insDatRetailShop.SelectRetailShopById(insEntRetailShop, insDbConnector);
                #endregion

                #region Get CustomerProductRetailShop
                entCustomerProductRetailShop insEntCustomerProductRetailShop = new entCustomerProductRetailShop();
                insEntCustomerProductRetailShop.RetailShopId = insEntRetailShop.Id;
                DataTable insDt_CustomerProductRetailShop = new DataTable();
                insDt_CustomerProductRetailShop = insDatCustomerProductRetailShop.SelectCustomerProductRetailShopByRetailShopId(insEntCustomerProductRetailShop, insDbConnector);
                #endregion

                #region Delete all CustomerProductRetailShop for found Id's
                foreach (DataRow insDr_CustomerProductRetailShop in insDt_CustomerProductRetailShop.Rows)
                {
                    entCustomerProductRetailShop insEntCustomerProductRetailShop_Delete = new entCustomerProductRetailShop();
                    insEntCustomerProductRetailShop_Delete.Id = Convert.ToInt32(insDr_CustomerProductRetailShop["Id"]);
                    insDatCustomerProductRetailShop.DeleteCustomerProductRetailShopById(insEntCustomerProductRetailShop_Delete, insDbConnector);
                }
                #endregion

                #region Get All MemberRoute for this RetailShopId
                entMemberRoute insEntMemberRoute = new entMemberRoute();
                insEntMemberRoute.RetailShopId = insEntRetailShop.Id;
                DataTable insDt_MemberRoute = new DataTable();
                insDt_MemberRoute = insDatMemberRoute.SelectMemberRouteByRetailShopId(insEntMemberRoute, insDbConnector);
                #endregion

                #region Loop and get each MemberRouteDetail by MemberRouteId, delete them
                foreach (DataRow insDrMemberRoute in insDt_MemberRoute.Rows)
                {
                    entMemberRouteDetail insEntMemberRouteDetail = new entMemberRouteDetail();
                    insEntMemberRouteDetail.MemberRouteId = Convert.ToInt32(insDrMemberRoute["Id"]);
                    insDatMemberRouteDetail.DeleteMemberRouteDetailByMemberRouteId(insEntMemberRouteDetail, insDbConnector);
                }
                #endregion

                #region Loop and delete MemberRoute
                foreach (DataRow insDrMemberRoute in insDt_MemberRoute.Rows)
                {
                    entMemberRoute insEntMemberRoute_Delete = new entMemberRoute();
                    insEntMemberRoute_Delete.Id = Convert.ToInt32(insDrMemberRoute["Id"]);
                    insDatMemberRoute.DeleteMemberRouteById(insEntMemberRoute_Delete, insDbConnector);
                }
                #endregion

                #region Delete RetailShop
                insDatRetailShop.DeleteRetailShopById(insEntRetailShop, insDbConnector);
                #endregion

                insDbConnector.CommitTransaction();
            }
            catch (Exception ex)
            {
                insDbConnector.RollbackTransaction();
                this.ErrorMessage = ex.ToString();
            }
        }

        public DataTable SelectRetailShopByRetailCategoryId(entRetailCategory insEntRetailCategory)
        {
            DbConnector insDbConnector = new DbConnector();
            datRetailShop insDatRetailShop = new datRetailShop();
            try
            {
                return insDatRetailShop.SelectRetailShopByRetailCategoryId(insEntRetailCategory, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public DataTable SelectRetailShopByRetailId(entRetailShop insEntRetailShop)
        {
            DbConnector insDbConnector = new DbConnector();
            datRetailShop insDatRetailShop = new datRetailShop();
            try
            {
                return insDatRetailShop.SelectRetailShopByRetailId(insEntRetailShop, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public DataTable SelectRetailShopByRetailShopCategoryId(entRetailShop insEntRetailShop)
        {
            DbConnector insDbConnector = new DbConnector();
            datRetailShop insDatRetailShop = new datRetailShop();
            try
            {
                return insDatRetailShop.SelectRetailShopByRetailShopCategoryId(insEntRetailShop, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public DataTable SelectRetailShopMapViewByCustomerId(int customerId)
        {
            DbConnector insDbConnector = new DbConnector();
            datRetailShop insDatRetailShop = new datRetailShop();
            try
            {
                return insDatRetailShop.SelectRetailShopMapViewByCustomerId(customerId, insDbConnector);
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