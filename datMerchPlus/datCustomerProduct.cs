using System;
using System.Collections.Generic;
using System.Text;
using SqlHelper;
using System.Data;
using entMerchPlus;

namespace datMerchPlus
{
    /// <summary>
    /// The class that is generated hold parameters to manage database table [CustomerProduct]
    /// </summary>
    public class datCustomerProduct
    {
        /// <summary>
        /// datCustomerProduct Constructor method used while taking an instance of this class.
        /// </summary>
        public datCustomerProduct()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Method that selects all data stored in table [CustomerProduct]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public DataTable SelectCustomerProduct(DbConnector parDbConnector)
        {
            return parDbConnector.ExecuteDataTable("SelectCustomerProduct", null);
        }

        /// <summary>
        /// Method that selects one row of data in table [CustomerProduct] filtered by the PK field of the inbound entity object
        /// </summary>
        /// <param name="parEntCustomerProduct">Entity object as parameter for tableCustomerProduct]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void SelectCustomerProductById(entCustomerProduct parEntCustomerProduct, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntCustomerProduct.Id);
            DataTable insDataTable = new DataTable();
            insDataTable = parDbConnector.ExecuteDataTable("SelectCustomerProductById", insDbParamCollection);
            if (insDataTable.Rows.Count > 0)
            {
                if (insDataTable.Rows[0]["Id"] != DBNull.Value)
                {
                    parEntCustomerProduct.Id = Convert.ToInt32(insDataTable.Rows[0]["Id"]);
                }
                if (insDataTable.Rows[0]["CustomerId"] != DBNull.Value)
                {
                    parEntCustomerProduct.CustomerId = Convert.ToInt32(insDataTable.Rows[0]["CustomerId"]);
                }
                if (insDataTable.Rows[0]["CustomerBrandId"] != DBNull.Value)
                {
                    parEntCustomerProduct.CustomerBrandId = Convert.ToInt32(insDataTable.Rows[0]["CustomerBrandId"]);
                }
                if (insDataTable.Rows[0]["CustomerBrandCategoryId"] != DBNull.Value)
                {
                    parEntCustomerProduct.CustomerBrandCategoryId = Convert.ToInt32(insDataTable.Rows[0]["CustomerBrandCategoryId"]);
                }
                if (insDataTable.Rows[0]["ProductBarcode"] != DBNull.Value)
                {
                    parEntCustomerProduct.ProductBarcode = Convert.ToString(insDataTable.Rows[0]["ProductBarcode"]);
                }
                if (insDataTable.Rows[0]["SuggestedListPrice"] != DBNull.Value)
                {
                    parEntCustomerProduct.SuggestedListPrice = Convert.ToDecimal(insDataTable.Rows[0]["SuggestedListPrice"]);
                }
                if (insDataTable.Rows[0]["SuggestedActivityPrice"] != DBNull.Value)
                {
                    parEntCustomerProduct.SuggestedActivityPrice = Convert.ToDecimal(insDataTable.Rows[0]["SuggestedActivityPrice"]);
                }
                if (insDataTable.Rows[0]["PackagingSize"] != DBNull.Value)
                {
                    parEntCustomerProduct.PackagingSize = Convert.ToDecimal(insDataTable.Rows[0]["PackagingSize"]);
                }
                if (insDataTable.Rows[0]["PackagingTypeId"] != DBNull.Value)
                {
                    parEntCustomerProduct.PackagingTypeId = Convert.ToInt32(insDataTable.Rows[0]["PackagingTypeId"]);
                }
                if (insDataTable.Rows[0]["Name"] != DBNull.Value)
                {
                    parEntCustomerProduct.Name = Convert.ToString(insDataTable.Rows[0]["Name"]);
                }
                if (insDataTable.Rows[0]["ProfilePicturePath"] != DBNull.Value)
                {
                    parEntCustomerProduct.ProfilePicturePath = Convert.ToString(insDataTable.Rows[0]["ProfilePicturePath"]);
                }
                if (insDataTable.Rows[0]["ProductCode"] != DBNull.Value)
                {
                    parEntCustomerProduct.ProductCode = Convert.ToString(insDataTable.Rows[0]["ProductCode"]);
                }
                if (insDataTable.Rows[0]["CaseBarcode"] != DBNull.Value)
                {
                    parEntCustomerProduct.CaseBarcode = Convert.ToString(insDataTable.Rows[0]["CaseBarcode"]);
                }
                if (insDataTable.Rows[0]["BoxBarcode"] != DBNull.Value)
                {
                    parEntCustomerProduct.BoxBarcode = Convert.ToString(insDataTable.Rows[0]["BoxBarcode"]);
                }
                if (insDataTable.Rows[0]["CaseEach"] != DBNull.Value)
                {
                    parEntCustomerProduct.CaseEach = Convert.ToInt32(insDataTable.Rows[0]["CaseEach"]);
                }
                if (insDataTable.Rows[0]["CreatedBy"] != DBNull.Value)
                {
                    parEntCustomerProduct.CreatedBy = Convert.ToString(insDataTable.Rows[0]["CreatedBy"]);
                }
                if (insDataTable.Rows[0]["CreatedOn"] != DBNull.Value)
                {
                    parEntCustomerProduct.CreatedOn = Convert.ToDateTime(insDataTable.Rows[0]["CreatedOn"]);
                }
                if (insDataTable.Rows[0]["UpdatedBy"] != DBNull.Value)
                {
                    parEntCustomerProduct.UpdatedBy = Convert.ToString(insDataTable.Rows[0]["UpdatedBy"]);
                }
                if (insDataTable.Rows[0]["UpdatedOn"] != DBNull.Value)
                {
                    parEntCustomerProduct.UpdatedOn = Convert.ToDateTime(insDataTable.Rows[0]["UpdatedOn"]);
                }
            }
        }

        /// <summary>
        /// Method that inserts one row of data into table [CustomerProduct] based on the values of input entity object
        /// </summary>
        /// <param name="parEntCustomerProduct">Entity object as parameter for tableCustomerProduct]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void InsertCustomerProduct(entCustomerProduct parEntCustomerProduct, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.AddOutput("@pId", DbType.Int32);
            insDbParamCollection.Add("@pCustomerId", parEntCustomerProduct.CustomerId);
            insDbParamCollection.Add("@pCustomerBrandId", parEntCustomerProduct.CustomerBrandId);
            insDbParamCollection.Add("@pCustomerBrandCategoryId", parEntCustomerProduct.CustomerBrandCategoryId);
            insDbParamCollection.Add("@pProductBarcode", parEntCustomerProduct.ProductBarcode);
            insDbParamCollection.Add("@pSuggestedListPrice", parEntCustomerProduct.SuggestedListPrice);
            insDbParamCollection.Add("@pSuggestedActivityPrice", parEntCustomerProduct.SuggestedActivityPrice);
            insDbParamCollection.Add("@pPackagingSize", parEntCustomerProduct.PackagingSize);
            insDbParamCollection.Add("@pPackagingTypeId", parEntCustomerProduct.PackagingTypeId);
            insDbParamCollection.Add("@pName", parEntCustomerProduct.Name);
            insDbParamCollection.Add("@pProfilePicturePath", parEntCustomerProduct.ProfilePicturePath);
            insDbParamCollection.Add("@pProductCode", parEntCustomerProduct.ProductCode);
            insDbParamCollection.Add("@pCaseBarcode", parEntCustomerProduct.CaseBarcode);
            insDbParamCollection.Add("@pBoxBarcode", parEntCustomerProduct.BoxBarcode);
            insDbParamCollection.Add("@pCaseEach", parEntCustomerProduct.CaseEach);
            insDbParamCollection.Add("@pCreatedBy", parEntCustomerProduct.CreatedBy);
            insDbParamCollection.Add("@pCreatedOn", parEntCustomerProduct.CreatedOn);
            insDbParamCollection.Add("@pUpdatedBy", parEntCustomerProduct.UpdatedBy);
            insDbParamCollection.Add("@pUpdatedOn", parEntCustomerProduct.UpdatedOn);
            parDbConnector.ExecuteNonQuery("InsertCustomerProduct", insDbParamCollection);
            parEntCustomerProduct.Id = Convert.ToInt32(insDbParamCollection.GetOutPutParameter().Value);
        }

        /// <summary>
        /// Method that updates one row of data in table [CustomerProduct] based on the values of input entity object
        /// </summary>
        /// <param name="parEntCustomerProduct">Entity object as parameter for tableCustomerProduct]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void UpdateCustomerProductById(entCustomerProduct parEntCustomerProduct, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntCustomerProduct.Id);
            insDbParamCollection.Add("@pCustomerId", parEntCustomerProduct.CustomerId);
            insDbParamCollection.Add("@pCustomerBrandId", parEntCustomerProduct.CustomerBrandId);
            insDbParamCollection.Add("@pCustomerBrandCategoryId", parEntCustomerProduct.CustomerBrandCategoryId);
            insDbParamCollection.Add("@pProductBarcode", parEntCustomerProduct.ProductBarcode);
            insDbParamCollection.Add("@pSuggestedListPrice", parEntCustomerProduct.SuggestedListPrice);
            insDbParamCollection.Add("@pSuggestedActivityPrice", parEntCustomerProduct.SuggestedActivityPrice);
            insDbParamCollection.Add("@pPackagingSize", parEntCustomerProduct.PackagingSize);
            insDbParamCollection.Add("@pPackagingTypeId", parEntCustomerProduct.PackagingTypeId);
            insDbParamCollection.Add("@pName", parEntCustomerProduct.Name);
            insDbParamCollection.Add("@pProfilePicturePath", parEntCustomerProduct.ProfilePicturePath);
            insDbParamCollection.Add("@pProductCode", parEntCustomerProduct.ProductCode);
            insDbParamCollection.Add("@pCaseBarcode", parEntCustomerProduct.CaseBarcode);
            insDbParamCollection.Add("@pBoxBarcode", parEntCustomerProduct.BoxBarcode);
            insDbParamCollection.Add("@pCaseEach", parEntCustomerProduct.CaseEach);
            insDbParamCollection.Add("@pCreatedBy", parEntCustomerProduct.CreatedBy);
            insDbParamCollection.Add("@pCreatedOn", parEntCustomerProduct.CreatedOn);
            insDbParamCollection.Add("@pUpdatedBy", parEntCustomerProduct.UpdatedBy);
            insDbParamCollection.Add("@pUpdatedOn", parEntCustomerProduct.UpdatedOn);
            parDbConnector.ExecuteNonQuery("UpdateCustomerProductById", insDbParamCollection);
        }

        /// <summary>
        /// Method that deletes all data in table [CustomerProduct]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteCustomerProduct(DbConnector parDbConnector)
        {
            parDbConnector.ExecuteNonQuery("DeleteCustomerProduct", null);
        }

        /// <summary>
        /// Method that deletes one row of data in table [CustomerProduct] based on the passed entity object
        /// </summary>
        /// <param name="parEntCustomerProduct">Gets entity object as parameter for table CustomerProduct]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteCustomerProductById(entCustomerProduct parEntCustomerProduct, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntCustomerProduct.Id);
            parDbConnector.ExecuteNonQuery("DeleteCustomerProductById", insDbParamCollection);
        }

        #endregion
        #region Custom Methods
        public DataTable SelectCustomerProductByCustomerId(entCustomerProduct insEntCustomerProduct, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pCustomerId", insEntCustomerProduct.CustomerId);
            return parDbConnector.ExecuteDataTable("SelectCustomerProductByCustomerId", insDbParamCollection);
        }

        public DataTable SelectCustomerProductByCustomerIdGridData(entCustomerProduct insEntCustomerProduct, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pCustomerId", insEntCustomerProduct.CustomerId);
            return insDbConnector.ExecuteDataTable("SelectCustomerProductByCustomerIdGridData", insDbParamCollection);
        }
        #endregion
    }
}