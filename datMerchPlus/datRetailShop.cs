using System;
using System.Collections.Generic;
using System.Text;
using SqlHelper;
using System.Data;
using entMerchPlus;

namespace datMerchPlus
{
    /// <summary>
    /// The class that is generated hold parameters to manage database table [RetailShop]
    /// </summary>
    public class datRetailShop
    {
        /// <summary>
        /// datRetailShop Constructor method used while taking an instance of this class.
        /// </summary>
        public datRetailShop()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Method that selects all data stored in table [RetailShop]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public DataTable SelectRetailShop(DbConnector parDbConnector)
        {
            return parDbConnector.ExecuteDataTable("SelectRetailShop", null);
        }

        /// <summary>
        /// Method that selects one row of data in table [RetailShop] filtered by the PK field of the inbound entity object
        /// </summary>
        /// <param name="parEntRetailShop">Entity object as parameter for tableRetailShop]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void SelectRetailShopById(entRetailShop parEntRetailShop, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntRetailShop.Id);
            DataTable insDataTable = new DataTable();
            insDataTable = parDbConnector.ExecuteDataTable("SelectRetailShopById", insDbParamCollection);
            if (insDataTable.Rows.Count > 0)
            {
                if (insDataTable.Rows[0]["Id"] != DBNull.Value)
                {
                    parEntRetailShop.Id = Convert.ToInt32(insDataTable.Rows[0]["Id"]);
                }
                if (insDataTable.Rows[0]["RetailId"] != DBNull.Value)
                {
                    parEntRetailShop.RetailId = Convert.ToInt32(insDataTable.Rows[0]["RetailId"]);
                }
                if (insDataTable.Rows[0]["RetailShopCategoryId"] != DBNull.Value)
                {
                    parEntRetailShop.RetailShopCategoryId = Convert.ToInt32(insDataTable.Rows[0]["RetailShopCategoryId"]);
                }
                if (insDataTable.Rows[0]["Name"] != DBNull.Value)
                {
                    parEntRetailShop.Name = Convert.ToString(insDataTable.Rows[0]["Name"]);
                }
                if (insDataTable.Rows[0]["Address"] != DBNull.Value)
                {
                    parEntRetailShop.Address = Convert.ToString(insDataTable.Rows[0]["Address"]);
                }
                if (insDataTable.Rows[0]["CityId"] != DBNull.Value)
                {
                    parEntRetailShop.CityId = Convert.ToInt32(insDataTable.Rows[0]["CityId"]);
                }
                if (insDataTable.Rows[0]["DistrictId"] != DBNull.Value)
                {
                    parEntRetailShop.DistrictId = Convert.ToInt32(insDataTable.Rows[0]["DistrictId"]);
                }
                if (insDataTable.Rows[0]["RetailDatabaseId"] != DBNull.Value)
                {
                    parEntRetailShop.RetailDatabaseId = Convert.ToString(insDataTable.Rows[0]["RetailDatabaseId"]);
                }
                if (insDataTable.Rows[0]["CoordinateX"] != DBNull.Value)
                {
                    parEntRetailShop.CoordinateX = Convert.ToDecimal(insDataTable.Rows[0]["CoordinateX"]);
                }
                if (insDataTable.Rows[0]["CoordinateY"] != DBNull.Value)
                {
                    parEntRetailShop.CoordinateY = Convert.ToDecimal(insDataTable.Rows[0]["CoordinateY"]);
                }
                if (insDataTable.Rows[0]["IsActive"] != DBNull.Value)
                {
                    parEntRetailShop.IsActive = Convert.ToBoolean(insDataTable.Rows[0]["IsActive"]);
                }
                if (insDataTable.Rows[0]["CreatedBy"] != DBNull.Value)
                {
                    parEntRetailShop.CreatedBy = Convert.ToString(insDataTable.Rows[0]["CreatedBy"]);
                }
                if (insDataTable.Rows[0]["CreatedOn"] != DBNull.Value)
                {
                    parEntRetailShop.CreatedOn = Convert.ToDateTime(insDataTable.Rows[0]["CreatedOn"]);
                }
            }
        }

        /// <summary>
        /// Method that inserts one row of data into table [RetailShop] based on the values of input entity object
        /// </summary>
        /// <param name="parEntRetailShop">Entity object as parameter for tableRetailShop]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void InsertRetailShop(entRetailShop parEntRetailShop, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.AddOutput("@pId", DbType.Int32);
            insDbParamCollection.Add("@pRetailId", parEntRetailShop.RetailId);
            insDbParamCollection.Add("@pRetailShopCategoryId", parEntRetailShop.RetailShopCategoryId);
            insDbParamCollection.Add("@pName", parEntRetailShop.Name);
            insDbParamCollection.Add("@pAddress", parEntRetailShop.Address);
            insDbParamCollection.Add("@pCityId", parEntRetailShop.CityId);
            insDbParamCollection.Add("@pDistrictId", parEntRetailShop.DistrictId);
            insDbParamCollection.Add("@pRetailDatabaseId", parEntRetailShop.RetailDatabaseId);
            insDbParamCollection.Add("@pCoordinateX", parEntRetailShop.CoordinateX);
            insDbParamCollection.Add("@pCoordinateY", parEntRetailShop.CoordinateY);
            insDbParamCollection.Add("@pIsActive", parEntRetailShop.IsActive);
            insDbParamCollection.Add("@pCreatedBy", parEntRetailShop.CreatedBy);
            insDbParamCollection.Add("@pCreatedOn", parEntRetailShop.CreatedOn);
            parDbConnector.ExecuteNonQuery("InsertRetailShop", insDbParamCollection);
            parEntRetailShop.Id = Convert.ToInt32(insDbParamCollection.GetOutPutParameter().Value);
        }

        /// <summary>
        /// Method that updates one row of data in table [RetailShop] based on the values of input entity object
        /// </summary>
        /// <param name="parEntRetailShop">Entity object as parameter for tableRetailShop]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void UpdateRetailShopById(entRetailShop parEntRetailShop, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntRetailShop.Id);
            insDbParamCollection.Add("@pRetailId", parEntRetailShop.RetailId);
            insDbParamCollection.Add("@pRetailShopCategoryId", parEntRetailShop.RetailShopCategoryId);
            insDbParamCollection.Add("@pName", parEntRetailShop.Name);
            insDbParamCollection.Add("@pAddress", parEntRetailShop.Address);
            insDbParamCollection.Add("@pCityId", parEntRetailShop.CityId);
            insDbParamCollection.Add("@pDistrictId", parEntRetailShop.DistrictId);
            insDbParamCollection.Add("@pRetailDatabaseId", parEntRetailShop.RetailDatabaseId);
            insDbParamCollection.Add("@pCoordinateX", parEntRetailShop.CoordinateX);
            insDbParamCollection.Add("@pCoordinateY", parEntRetailShop.CoordinateY);
            insDbParamCollection.Add("@pIsActive", parEntRetailShop.IsActive);
            insDbParamCollection.Add("@pCreatedBy", parEntRetailShop.CreatedBy);
            insDbParamCollection.Add("@pCreatedOn", parEntRetailShop.CreatedOn);
            parDbConnector.ExecuteNonQuery("UpdateRetailShopById", insDbParamCollection);
        }

        /// <summary>
        /// Method that deletes all data in table [RetailShop]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteRetailShop(DbConnector parDbConnector)
        {
            parDbConnector.ExecuteNonQuery("DeleteRetailShop", null);
        }

        /// <summary>
        /// Method that deletes one row of data in table [RetailShop] based on the passed entity object
        /// </summary>
        /// <param name="parEntRetailShop">Gets entity object as parameter for table RetailShop]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteRetailShopById(entRetailShop parEntRetailShop, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntRetailShop.Id);
            parDbConnector.ExecuteNonQuery("DeleteRetailShopById", insDbParamCollection);
        }

        #endregion
        #region Custom Methods
        public DataTable SelectRetailShopMapView(DbConnector insDbConnector)
        {
            return insDbConnector.ExecuteDataTable("SelectRetailShopMapView", null);
        }

        public DataTable SelectRetailShopByMemberIdMemberRouteDate(entMemberRoute parEntMemberRoute, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pMemberId", parEntMemberRoute.MemberId);
            insDbParamCollection.Add("@pRouteDate", parEntMemberRoute.EffectiveDate);
            return insDbConnector.ExecuteDataTable("SelectRetailShopByMemberIdMemberRouteDate", insDbParamCollection);
        }

        public DataTable SelectRetailShopMapViewByMemberIdEffectiveDate(string memberId, DateTime dateTime, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pMemberId", memberId);
            insDbParamCollection.Add("@pEffectiveDate", dateTime);
            return insDbConnector.ExecuteDataTable("SelectRetailShopMapViewByMemberIdEffectiveDate", insDbParamCollection);
        }

        public DataTable SelectRetailShopByRetailCategoryId(entRetailCategory insEntRetailCategory, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pRetailCategoryId", insEntRetailCategory.Id);
            return insDbConnector.ExecuteDataTable("SelectRetailShopByRetailCategoryId", insDbParamCollection);
        }

        public DataTable SelectRetailShopByRetailId(entRetailShop insEntRetailShop, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pRetailId", insEntRetailShop.RetailId);
            return insDbConnector.ExecuteDataTable("SelectRetailShopByRetailId", insDbParamCollection);
        }

        public DataTable SelectRetailShopByRetailShopCategoryId(entRetailShop insEntRetailShop, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pRetailShopCategoryId", insEntRetailShop.RetailShopCategoryId);
            return insDbConnector.ExecuteDataTable("SelectRetailShopByRetailShopCategoryId", insDbParamCollection);
        }

        public DataTable SelectRetailShopMapViewByCustomerId(int customerId, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pCustomerId", customerId);
            return insDbConnector.ExecuteDataTable("SelectRetailShopMapViewByCustomerId", insDbParamCollection);
        }
        #endregion
    }
}