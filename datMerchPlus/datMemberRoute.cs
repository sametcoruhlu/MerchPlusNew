using System;
using System.Collections.Generic;
using System.Text;
using SqlHelper;
using System.Data;
using entMerchPlus;

namespace datMerchPlus
{
    /// <summary>
    /// The class that is generated hold parameters to manage database table [MemberRoute]
    /// </summary>
    public class datMemberRoute
    {
        /// <summary>
        /// datMemberRoute Constructor method used while taking an instance of this class.
        /// </summary>
        public datMemberRoute()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Method that selects all data stored in table [MemberRoute]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public DataTable SelectMemberRoute(DbConnector parDbConnector)
        {
            return parDbConnector.ExecuteDataTable("SelectMemberRoute", null);
        }

        /// <summary>
        /// Method that selects one row of data in table [MemberRoute] filtered by the PK field of the inbound entity object
        /// </summary>
        /// <param name="parEntMemberRoute">Entity object as parameter for tableMemberRoute]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void SelectMemberRouteById(entMemberRoute parEntMemberRoute, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntMemberRoute.Id);
            DataTable insDataTable = new DataTable();
            insDataTable = parDbConnector.ExecuteDataTable("SelectMemberRouteById", insDbParamCollection);
            if (insDataTable.Rows.Count > 0)
            {
                if (insDataTable.Rows[0]["Id"] != DBNull.Value)
                {
                    parEntMemberRoute.Id = Convert.ToInt32(insDataTable.Rows[0]["Id"]);
                }
                if (insDataTable.Rows[0]["MemberId"] != DBNull.Value)
                {
                    parEntMemberRoute.MemberId = Convert.ToString(insDataTable.Rows[0]["MemberId"]);
                }
                if (insDataTable.Rows[0]["CustomerId"] != DBNull.Value)
                {
                    parEntMemberRoute.CustomerId = Convert.ToInt32(insDataTable.Rows[0]["CustomerId"]);
                }
                if (insDataTable.Rows[0]["EffectiveDate"] != DBNull.Value)
                {
                    parEntMemberRoute.EffectiveDate = Convert.ToDateTime(insDataTable.Rows[0]["EffectiveDate"]);
                }
                if (insDataTable.Rows[0]["RetailShopId"] != DBNull.Value)
                {
                    parEntMemberRoute.RetailShopId = Convert.ToInt32(insDataTable.Rows[0]["RetailShopId"]);
                }
                if (insDataTable.Rows[0]["EntryOn"] != DBNull.Value)
                {
                    parEntMemberRoute.EntryOn = Convert.ToDateTime(insDataTable.Rows[0]["EntryOn"]);
                }
                if (insDataTable.Rows[0]["ExitOn"] != DBNull.Value)
                {
                    parEntMemberRoute.ExitOn = Convert.ToDateTime(insDataTable.Rows[0]["ExitOn"]);
                }
                if (insDataTable.Rows[0]["EntryCoordinateX"] != DBNull.Value)
                {
                    parEntMemberRoute.EntryCoordinateX = Convert.ToDecimal(insDataTable.Rows[0]["EntryCoordinateX"]);
                }
                if (insDataTable.Rows[0]["EntryCoordinateY"] != DBNull.Value)
                {
                    parEntMemberRoute.EntryCoordinateY = Convert.ToDecimal(insDataTable.Rows[0]["EntryCoordinateY"]);
                }
                if (insDataTable.Rows[0]["ExitCoordinateX"] != DBNull.Value)
                {
                    parEntMemberRoute.ExitCoordinateX = Convert.ToDecimal(insDataTable.Rows[0]["ExitCoordinateX"]);
                }
                if (insDataTable.Rows[0]["ExitCoordinateY"] != DBNull.Value)
                {
                    parEntMemberRoute.ExitCoordinateY = Convert.ToDecimal(insDataTable.Rows[0]["ExitCoordinateY"]);
                }
                if (insDataTable.Rows[0]["EntryImageData"] != DBNull.Value)
                {
                    parEntMemberRoute.EntryImageData = Convert.ToString(insDataTable.Rows[0]["EntryImageData"]);
                }
                if (insDataTable.Rows[0]["TotalTimeSpent"] != DBNull.Value)
                {
                    parEntMemberRoute.TotalTimeSpent = Convert.ToInt32(insDataTable.Rows[0]["TotalTimeSpent"]);
                }
                if (insDataTable.Rows[0]["IsSentToServer"] != DBNull.Value)
                {
                    parEntMemberRoute.IsSentToServer = Convert.ToBoolean(insDataTable.Rows[0]["IsSentToServer"]);
                }
                if (insDataTable.Rows[0]["SentToServerOn"] != DBNull.Value)
                {
                    parEntMemberRoute.SentToServerOn = Convert.ToDateTime(insDataTable.Rows[0]["SentToServerOn"]);
                }
                if (insDataTable.Rows[0]["OrderIndex"] != DBNull.Value)
                {
                    parEntMemberRoute.OrderIndex = Convert.ToInt32(insDataTable.Rows[0]["OrderIndex"]);
                }
            }
        }

        /// <summary>
        /// Method that inserts one row of data into table [MemberRoute] based on the values of input entity object
        /// </summary>
        /// <param name="parEntMemberRoute">Entity object as parameter for tableMemberRoute]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void InsertMemberRoute(entMemberRoute parEntMemberRoute, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.AddOutput("@pId", DbType.Int32);
            insDbParamCollection.Add("@pMemberId", parEntMemberRoute.MemberId);
            insDbParamCollection.Add("@pCustomerId", parEntMemberRoute.CustomerId);
            insDbParamCollection.Add("@pEffectiveDate", parEntMemberRoute.EffectiveDate);
            insDbParamCollection.Add("@pRetailShopId", parEntMemberRoute.RetailShopId);
            insDbParamCollection.Add("@pEntryOn", parEntMemberRoute.EntryOn);
            insDbParamCollection.Add("@pExitOn", parEntMemberRoute.ExitOn);
            insDbParamCollection.Add("@pEntryCoordinateX", parEntMemberRoute.EntryCoordinateX);
            insDbParamCollection.Add("@pEntryCoordinateY", parEntMemberRoute.EntryCoordinateY);
            insDbParamCollection.Add("@pExitCoordinateX", parEntMemberRoute.ExitCoordinateX);
            insDbParamCollection.Add("@pExitCoordinateY", parEntMemberRoute.ExitCoordinateY);
            insDbParamCollection.Add("@pEntryImageData", parEntMemberRoute.EntryImageData);
            insDbParamCollection.Add("@pTotalTimeSpent", parEntMemberRoute.TotalTimeSpent);
            insDbParamCollection.Add("@pIsSentToServer", parEntMemberRoute.IsSentToServer);
            insDbParamCollection.Add("@pSentToServerOn", parEntMemberRoute.SentToServerOn);
            insDbParamCollection.Add("@pOrderIndex", parEntMemberRoute.OrderIndex);
            parDbConnector.ExecuteNonQuery("InsertMemberRoute", insDbParamCollection);
            parEntMemberRoute.Id = Convert.ToInt32(insDbParamCollection.GetOutPutParameter().Value);
        }

        /// <summary>
        /// Method that updates one row of data in table [MemberRoute] based on the values of input entity object
        /// </summary>
        /// <param name="parEntMemberRoute">Entity object as parameter for tableMemberRoute]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void UpdateMemberRouteById(entMemberRoute parEntMemberRoute, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntMemberRoute.Id);
            insDbParamCollection.Add("@pMemberId", parEntMemberRoute.MemberId);
            insDbParamCollection.Add("@pCustomerId", parEntMemberRoute.CustomerId);
            insDbParamCollection.Add("@pEffectiveDate", parEntMemberRoute.EffectiveDate);
            insDbParamCollection.Add("@pRetailShopId", parEntMemberRoute.RetailShopId);
            insDbParamCollection.Add("@pEntryOn", parEntMemberRoute.EntryOn);
            insDbParamCollection.Add("@pExitOn", parEntMemberRoute.ExitOn);
            insDbParamCollection.Add("@pEntryCoordinateX", parEntMemberRoute.EntryCoordinateX);
            insDbParamCollection.Add("@pEntryCoordinateY", parEntMemberRoute.EntryCoordinateY);
            insDbParamCollection.Add("@pExitCoordinateX", parEntMemberRoute.ExitCoordinateX);
            insDbParamCollection.Add("@pExitCoordinateY", parEntMemberRoute.ExitCoordinateY);
            insDbParamCollection.Add("@pEntryImageData", parEntMemberRoute.EntryImageData);
            insDbParamCollection.Add("@pTotalTimeSpent", parEntMemberRoute.TotalTimeSpent);
            insDbParamCollection.Add("@pIsSentToServer", parEntMemberRoute.IsSentToServer);
            insDbParamCollection.Add("@pSentToServerOn", parEntMemberRoute.SentToServerOn);
            insDbParamCollection.Add("@pOrderIndex", parEntMemberRoute.OrderIndex);
            parDbConnector.ExecuteNonQuery("UpdateMemberRouteById", insDbParamCollection);
        }

        /// <summary>
        /// Method that deletes all data in table [MemberRoute]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteMemberRoute(DbConnector parDbConnector)
        {
            parDbConnector.ExecuteNonQuery("DeleteMemberRoute", null);
        }

        /// <summary>
        /// Method that deletes one row of data in table [MemberRoute] based on the passed entity object
        /// </summary>
        /// <param name="parEntMemberRoute">Gets entity object as parameter for table MemberRoute]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteMemberRouteById(entMemberRoute parEntMemberRoute, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntMemberRoute.Id);
            parDbConnector.ExecuteNonQuery("DeleteMemberRouteById", insDbParamCollection);
        }

        #endregion
        #region Custom Methods
        public DataTable SelectMemberRouteByMemberIdEffectiveDate(entMemberRoute insEntMemberRoute, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pMemberId", insEntMemberRoute.MemberId);
            insDbParamCollection.Add("@pEffectiveDate", insEntMemberRoute.EffectiveDate);
            return insDbConnector.ExecuteDataTable("SelectMemberRouteByMemberIdEffectiveDate", insDbParamCollection);
        }

        public DataTable SelectMemberRouteByEffectiveDate(entMemberRoute insEntMemberRoute, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pEffectiveDate", insEntMemberRoute.EffectiveDate);
            return insDbConnector.ExecuteDataTable("SelectMemberRouteByEffectiveDate", insDbParamCollection);
        }

        public DataTable SelectMemberRouteDetailByMemberIdEffectiveDate(entMemberRoute insEntMemberRoute, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pMemberId", insEntMemberRoute.MemberId);
            insDbParamCollection.Add("@pEffectiveDate", insEntMemberRoute.EffectiveDate);
            return insDbConnector.ExecuteDataTable("SelectMemberRouteDetailByMemberIdEffectiveDate", insDbParamCollection);
        }

        public DataTable SelectDashboardWidgetValuesByCustomerIdCurrentDate(int customerId, DateTime insDateTime, DbConnector insDbConnector)
        {
            string query = "SELECT " +
                           "     (SELECT COUNT(DISTINCT MemberId) from MemberRoute WHERE CustomerId = " + Convert.ToString(customerId) + " AND EffectiveDate = '" + insDateTime.ToString("yyyy-MM-dd") + "' AND MemberRoute.MemberId NOT IN (SELECT MemberId from MemberTimeOff WHERE StartDate <= GETDATE() AND EndDate >= GETDATE())) as [NumberOfMembers], " +
                           "     (SELECT COUNT(Id) from MemberRoute WHERE CustomerId = " + Convert.ToString(customerId) + " AND EffectiveDate = '" + insDateTime.ToString("yyyy-MM-dd") + "' AND MemberRoute.MemberId NOT IN (SELECT MemberId from MemberTimeOff WHERE StartDate <= GETDATE() AND EndDate >= GETDATE())) as [PlannedVisitCount], " +
                           "     (SELECT COUNT(Id) from MemberRoute WHERE CustomerId = " + Convert.ToString(customerId) + " AND EffectiveDate = '" + insDateTime.ToString("yyyy-MM-dd") + "' AND EntryOn IS NOT NULL AND MemberRoute.MemberId NOT IN (SELECT MemberId from MemberTimeOff WHERE StartDate <= GETDATE() AND EndDate >= GETDATE())) as [ActualVisitCount], " +
                           "     (" +
                           "         ((SELECT CAST(COUNT(Id) as decimal(18, 4)) from MemberRoute WHERE CustomerId = " + Convert.ToString(customerId) + " AND EffectiveDate = '" + insDateTime.ToString("yyyy-MM-dd") + "' AND EntryOn IS NOT NULL AND MemberRoute.MemberId NOT IN (SELECT MemberId from MemberTimeOff WHERE StartDate <= GETDATE() AND EndDate >= GETDATE())) / (SELECT CAST(COUNT(Id) as decimal(18, 4)) from MemberRoute WHERE CustomerId = " + Convert.ToString(customerId) + " AND EffectiveDate = '" + insDateTime.ToString("yyyy-MM-dd") + "' AND MemberRoute.MemberId NOT IN (SELECT MemberId from MemberTimeOff WHERE StartDate <= GETDATE() AND EndDate >= GETDATE()))) " +
                           "     ) as [ActualPercent], " +
                           "     (SELECT COUNT(DISTINCT MemberId) from MemberRoute WHERE CustomerId = " + Convert.ToString(customerId) + " AND EffectiveDate = '" + insDateTime.ToString("yyyy-MM-dd") + "') as [AllMembers] ";

            return insDbConnector.ExecuteSqlDataTable(query);
        }

        public DataTable GetMapDataByCustomerIdCurrentDate(int customerId, DateTime insDateTime, DbConnector insDbConnector)
        {
            string query = "SELECT * from City WHERE Id IN (" +
                            "SELECT DISTINCT(CityId)from RetailShop WHERE Id IN(" +
                            "SELECT DISTINCT(RetailShopId) from MemberRoute WHERE EffectiveDate = '" + insDateTime.ToString("yyyy-MM-dd") + "' AND CustomerId = " + Convert.ToString(customerId) + "" +
                            " AND MemberRoute.MemberId NOT IN (SELECT MemberId from MemberTimeOff WHERE StartDate <= GETDATE() AND EndDate >= GETDATE()) " +
                            ")) ORDER BY City.Name";
            return insDbConnector.ExecuteSqlDataTable(query);
        }

        public DataTable SelectDistinctMemberIdFromMemberRouteByEffectiveDateCustomerIdCityName(entMemberRoute insEntMemberRoute, string cityName, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pEffectiveDate", insEntMemberRoute.EffectiveDate);
            insDbParamCollection.Add("@pCustomerId", insEntMemberRoute.CustomerId);
            insDbParamCollection.Add("@pCityName", cityName);
            return insDbConnector.ExecuteDataTable("SelectDistinctMemberIdFromMemberRouteByEffectiveDateCustomerIdCityName", insDbParamCollection);
        }

        public DataTable SelectMemberRouteReportByMemberIdEffectiveDate(string memberId, DateTime dateTime, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pMemberId", memberId);
            insDbParamCollection.Add("@pEffectiveDate", dateTime);
            return insDbConnector.ExecuteDataTable("RouteUyumlulukRaporuDetailsByMemberIdEffectiveDate", insDbParamCollection);
        }

        public DataTable SelectPlannedVersusActualGraphDataByCustomerIdEndDate(int customerId, DateTime insDateTime, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pCustomerId", customerId);
            insDbParamCollection.Add("@pEndDate", insDateTime);
            return insDbConnector.ExecuteDataTable("SelectPlannedVersusActualGraphDataByCustomerIdEndDate", insDbParamCollection);
        }

        public DataTable SelectSalesOrderGraphDataByCustomerIdEndDate(int customerId, DateTime insDateTime, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pCustomerId", customerId);
            insDbParamCollection.Add("@pEndDate", insDateTime);
            return insDbConnector.ExecuteDataTable("SelectSalesOrderGraphDataByCustomerIdEndDate", insDbParamCollection);
        }

        public DataTable SelectMemberRouteByRetailShopId(entMemberRoute insEntMemberRoute, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pRetailShopId", insEntMemberRoute.RetailShopId);
            return insDbConnector.ExecuteDataTable("SelectMemberRouteByRetailShopId", insDbParamCollection);
        }

        public DataTable SelectMemberRouteByMemberIdSchedulerView(entMemberRoute insEntMemberRoute, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pMemberId", insEntMemberRoute.MemberId);
            return insDbConnector.ExecuteDataTable("SelectMemberRouteByMemberIdSchedulerView", insDbParamCollection);
        }

        public void DeleteMemberRouteByEffectiveDateMoreThanRetailshopIdMemberId(entMemberRoute insEntMemberRoute, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pMemberId", insEntMemberRoute.MemberId);
            insDbParamCollection.Add("@pEffectiveDate", insEntMemberRoute.EffectiveDate);
            insDbParamCollection.Add("@pRetailShopId", insEntMemberRoute.RetailShopId);
            insDbConnector.ExecuteNonQuery("DeleteMemberRouteByEffectiveDateMoreThanRetailshopIdMemberId", insDbParamCollection);
        }

        public void DeleteMemberRouteByEffectiveDateMoreThanRetailShopIdMemberIdOnlyOriginalWeekday(entMemberRoute insEntMemberRoute_Delete, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pMemberId", insEntMemberRoute_Delete.MemberId);
            insDbParamCollection.Add("@pEffectiveDate", insEntMemberRoute_Delete.EffectiveDate);
            insDbParamCollection.Add("@pRetailShopId", insEntMemberRoute_Delete.RetailShopId);
            insDbConnector.ExecuteNonQuery("DeleteMemberRouteByEffectiveDateMoreThanRetailShopIdMemberIdOnlyOriginalWeekday", insDbParamCollection);
        }

        public DataTable SelectMemberRouteByCustomerIdStartDateEndDate(int customerId, DateTime insStartDate, DateTime insEndDate, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pCustomerId", customerId);
            insDbParamCollection.Add("@pStartDate", insStartDate);
            insDbParamCollection.Add("@pEndDate", insEndDate);
            return insDbConnector.ExecuteDataTable("SelectMemberRouteByCustomerIdStartDateEndDate", insDbParamCollection);
        }
        #endregion
    }
}
