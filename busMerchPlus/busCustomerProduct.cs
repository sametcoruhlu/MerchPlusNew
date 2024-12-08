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
    /// Manages transactions on table [CustomerProduct]
    /// </summary>
    public class busCustomerProduct : BusBase
    {
        /// <summary>
        /// busCustomerProductConstructor method used while taking an instance of this class.
        /// </summary>
        public busCustomerProduct() : base()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Selects all data stored in table [CustomerProduct]
        /// </summary>
        public DataTable SelectCustomerProduct()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomerProduct insDatCustomerProduct = new datCustomerProduct();
                return insDatCustomerProduct.SelectCustomerProduct(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Selects one row of data stored in table [CustomerProduct] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntCustomerProduct">Gets entity object as parameter for table CustomerProduct]</param>
        public void SelectCustomerProductById(entCustomerProduct parEntCustomerProduct)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomerProduct insDatCustomerProduct = new datCustomerProduct();
                insDatCustomerProduct.SelectCustomerProductById(parEntCustomerProduct, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Inserts one row of data into table [CustomerProduct] based on the values of input entity object
        /// </summary>
        /// <param name="parEntCustomerProduct">Gets entity object as parameter for table CustomerProduct]</param>
        public void InsertCustomerProduct(entCustomerProduct parEntCustomerProduct)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomerProduct insDatCustomerProduct = new datCustomerProduct();
                insDatCustomerProduct.InsertCustomerProduct(parEntCustomerProduct, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Updates one row of data in table [CustomerProduct] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntCustomerProduct">Gets entity object as parameter for table CustomerProduct]</param>
        public void UpdateCustomerProductById(entCustomerProduct parEntCustomerProduct)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomerProduct insDatCustomerProduct = new datCustomerProduct();
                insDatCustomerProduct.UpdateCustomerProductById(parEntCustomerProduct, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes all data stored in table [CustomerProduct]
        /// </summary>
        public void DeleteCustomerProduct()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomerProduct insDatCustomerProduct = new datCustomerProduct();
                insDatCustomerProduct.DeleteCustomerProduct(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes data stored in table [CustomerProduct] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntCustomerProduct">Gets entity object as parameter for table CustomerProduct]</param>
        public void DeleteCustomerProductById(entCustomerProduct parEntCustomerProduct)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomerProduct insDatCustomerProduct = new datCustomerProduct();
                insDatCustomerProduct.DeleteCustomerProductById(parEntCustomerProduct, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        #endregion
        #region Custom Methods
        public DataTable SelectCustomerProductByCustomerId(entCustomerProduct insEntCustomerProduct)
        {
            DbConnector insDbConnector = new DbConnector();
            datCustomerProduct insDatCustomerProduct = new datCustomerProduct();
            try
            {
                return insDatCustomerProduct.SelectCustomerProductByCustomerId(insEntCustomerProduct, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public DataTable SelectCustomerProductByCustomerIdGridData(entCustomerProduct insEntCustomerProduct)
        {
            DbConnector insDbConnector = new DbConnector();
            datCustomerProduct insDatCustomerProduct = new datCustomerProduct();
            try
            {
                return insDatCustomerProduct.SelectCustomerProductByCustomerIdGridData(insEntCustomerProduct, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public void DeleteCustomerProductByIdWisely(entCustomerProduct insEntCustomerProduct)
        {
            DbConnector insDbConnector = new DbConnector();
            datCustomerProduct insDatCustomerProduct = new datCustomerProduct();
            datCustomerProductCompetitor insDatCustomerProductCompetitor = new datCustomerProductCompetitor();
            datMemberRouteDetail insDatMemberRouteDetail = new datMemberRouteDetail();
            datSalesOrderDetail insDatSalesOrderDetail = new datSalesOrderDetail();
            try
            {
                insDbConnector.BeginTransaction();

                #region Get All Competitors
                DataTable insDt_Competitors = new DataTable();
                entCustomerProductCompetitor insEntCustomerProductCompetitor = new entCustomerProductCompetitor();
                insEntCustomerProductCompetitor.CustomerProductId = insEntCustomerProduct.Id;
                insDt_Competitors = insDatCustomerProductCompetitor.SelectCustomerProductCompetitorByCustomerProductId(insEntCustomerProductCompetitor, insDbConnector);
                #endregion

                #region Delete competitor mapping
                foreach (DataRow insDr_Competitor in insDt_Competitors.Rows)
                {
                    entCustomerProductCompetitor insEntCompetitor_Delete = new entCustomerProductCompetitor();
                    insEntCompetitor_Delete.Id = Convert.ToInt32(insDr_Competitor["Id"]);
                    insDatCustomerProductCompetitor.DeleteCustomerProductCompetitorById(insEntCompetitor_Delete, insDbConnector);
                }
                #endregion

                #region Delete CustomerProduct Data
                entMemberRouteDetail insEntMemberRouteDetail_Delete = new entMemberRouteDetail();
                insEntMemberRouteDetail_Delete.CustomerProductId = insEntCustomerProduct.Id;
                insDatMemberRouteDetail.DeleteMemberRouteDetailByCustomerProductId(insEntMemberRouteDetail_Delete, insDbConnector);
                #endregion

                #region Delete SalesOrderDetail
                entSalesOrderDetail insEntSalesOrderDetail_Delete = new entSalesOrderDetail();
                insEntSalesOrderDetail_Delete.CustomerProductId = insEntCustomerProduct.Id;
                insDatSalesOrderDetail.DeleteSalesOrderDetailByCustomerProductId(insEntSalesOrderDetail_Delete, insDbConnector);
                #endregion

                #region Delete CustomerProduct
                insDatCustomerProduct.DeleteCustomerProductById(insEntCustomerProduct, insDbConnector);
                #endregion

                insDbConnector.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                insDbConnector.RollbackTransaction();
            }
        }
        #endregion
    }
}