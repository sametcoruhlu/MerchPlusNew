using System;
using System.Collections.Generic;
using System.Text;
using SqlHelper;
using System.Data;
using entMerchPlus;

namespace datMerchPlus
{
    /// <summary>
    /// The class that is generated hold parameters to manage database table [Customer]
    /// </summary>
    public class datCustomer
    {
        /// <summary>
        /// datCustomer Constructor method used while taking an instance of this class.
        /// </summary>
        public datCustomer()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Method that selects all data stored in table [Customer]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public DataTable SelectCustomer(DbConnector parDbConnector)
        {
            return parDbConnector.ExecuteDataTable("SelectCustomer", null);
        }

        /// <summary>
        /// Method that selects one row of data in table [Customer] filtered by the PK field of the inbound entity object
        /// </summary>
        /// <param name="parEntCustomer">Entity object as parameter for tableCustomer]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void SelectCustomerById(entCustomer parEntCustomer, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntCustomer.Id);
            DataTable insDataTable = new DataTable();
            insDataTable = parDbConnector.ExecuteDataTable("SelectCustomerById", insDbParamCollection);
            if (insDataTable.Rows.Count > 0)
            {
                if (insDataTable.Rows[0]["Id"] != DBNull.Value)
                {
                    parEntCustomer.Id = Convert.ToInt32(insDataTable.Rows[0]["Id"]);
                }
                if (insDataTable.Rows[0]["Name"] != DBNull.Value)
                {
                    parEntCustomer.Name = Convert.ToString(insDataTable.Rows[0]["Name"]);
                }
                if (insDataTable.Rows[0]["ProfilePicturePath"] != DBNull.Value)
                {
                    parEntCustomer.ProfilePicturePath = Convert.ToString(insDataTable.Rows[0]["ProfilePicturePath"]);
                }
                if (insDataTable.Rows[0]["IsActive"] != DBNull.Value)
                {
                    parEntCustomer.IsActive = Convert.ToBoolean(insDataTable.Rows[0]["IsActive"]);
                }
            }
        }

        /// <summary>
        /// Method that inserts one row of data into table [Customer] based on the values of input entity object
        /// </summary>
        /// <param name="parEntCustomer">Entity object as parameter for tableCustomer]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void InsertCustomer(entCustomer parEntCustomer, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.AddOutput("@pId", DbType.Int32);
            insDbParamCollection.Add("@pName", parEntCustomer.Name);
            insDbParamCollection.Add("@pProfilePicturePath", parEntCustomer.ProfilePicturePath);
            insDbParamCollection.Add("@pIsActive", parEntCustomer.IsActive);
            parDbConnector.ExecuteNonQuery("InsertCustomer", insDbParamCollection);
            parEntCustomer.Id = Convert.ToInt32(insDbParamCollection.GetOutPutParameter().Value);
        }

        /// <summary>
        /// Method that updates one row of data in table [Customer] based on the values of input entity object
        /// </summary>
        /// <param name="parEntCustomer">Entity object as parameter for tableCustomer]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void UpdateCustomerById(entCustomer parEntCustomer, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntCustomer.Id);
            insDbParamCollection.Add("@pName", parEntCustomer.Name);
            insDbParamCollection.Add("@pProfilePicturePath", parEntCustomer.ProfilePicturePath);
            insDbParamCollection.Add("@pIsActive", parEntCustomer.IsActive);
            parDbConnector.ExecuteNonQuery("UpdateCustomerById", insDbParamCollection);
        }

        /// <summary>
        /// Method that deletes all data in table [Customer]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteCustomer(DbConnector parDbConnector)
        {
            parDbConnector.ExecuteNonQuery("DeleteCustomer", null);
        }

        /// <summary>
        /// Method that deletes one row of data in table [Customer] based on the passed entity object
        /// </summary>
        /// <param name="parEntCustomer">Gets entity object as parameter for table Customer]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteCustomerById(entCustomer parEntCustomer, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntCustomer.Id);
            parDbConnector.ExecuteNonQuery("DeleteCustomerById", insDbParamCollection);
        }

        #endregion
        #region Custom Methods
        public DataTable FrekansRaporu(string query, DbConnector insDbConnector)
        {
            return insDbConnector.ExecuteSqlDataTable(query);
        }

        public DataTable RouteUyumlulukRaporuDetailsByMemberIdEffectiveDate(DateTime effectiveDate, string memberId, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection
            {
                { "@pMemberId", memberId },
                { "@pEffectiveDate", effectiveDate }
            };
            return insDbConnector.ExecuteDataTable("RouteUyumlulukRaporuDetailsByMemberIdEffectiveDate", insDbParamCollection);
        }

        public DataTable TeshirRaporu(DateTime startDate, DateTime endDate, int customerId, DbConnector insDbConnector)
        {
            DataTable insDt = new DataTable();
            insDt = insDbConnector.ExecuteSqlDataTable("SELECT  		DISTINCT   		Member.NameSurname as [MerchNameSurname], MemberRoute.Id as [MemberRouteId],  		MemberRoute.EffectiveDate as [DateOfVisit],  		CASE(DATENAME(weekday,MemberRoute.EffectiveDate))  			WHEN 'Monday' THEN 'Pazartesi'  			WHEN 'Tuesday' THEN 'Salı'  			WHEN 'Wednesday' THEN 'Çarşamba'  			WHEN 'Thursday' THEN 'Perşembe'  			WHEN 'Friday' THEN 'Cuma'  			WHEN 'Saturday' THEN 'Cumartesi'  			WHEN 'Sunday' THEN 'Pazar'  			ELSE ''  		END as [Day],  		Retail.Name as [RetailName],  		RetailShop.Name as [RetailShopName],  		(SELECT COUNT(Id) FROM MemberRouteDetail MemberRouteDetail2 WITH (NOLOCK) WHERE MemberRouteDetail2.MemberRouteId = MemberRoute.Id AND PromotionTypeId <> 1 AND MemberRouteDetail2.MemberRouteId IN (SELECT Id FROM MemberRoute MemberRoute4 WITH (NOLOCK) WHERE MemberRoute4.MemberId = MemberRoute.MemberId AND MemberRoute4.EntryOn IS NOT NULL) ) as [PromotionCount]  	FROM  		MemberRoute WITH (NOLOCK)  		INNER JOIN Member WITH (NOLOCK) ON Member.Id = MemberRoute.MemberId  		INNER JOIN MemberCustomer WITH (NOLOCK) ON Member.Id = MemberCustomer.MemberId  		INNER JOIN MemberRouteDetail WITH (NOLOCK) ON MemberRoute.Id = MemberRouteDetail.MemberRouteId  		INNER JOIN RetailShop WITH (NOLOCK) ON MemberRoute.RetailShopId = RetailShop.Id  		INNER JOIN Retail WITH (NOLOCK) ON RetailShop.RetailId = Retail.Id  	WHERE  		MemberRoute.EffectiveDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + " 00:00:00' AND '" + endDate.ToString("yyyy-MM-dd") + " 23:59:59' AND  		MemberCustomer.CustomerId = " + Convert.ToString(customerId) + " AND  		Member.MemberTitleId IN (5) AND MemberRoute.EntryOn IS NOT NULL  	ORDER BY  		Member.NameSurname,  		MemberRoute.EffectiveDate");
            insDt = insDt.Select("PromotionCount > 0").CopyToDataTable();
            return insDt;
        }

        public DataTable TeshirOzelRaporu(DateTime startDate, DateTime endDate, int customerId, DbConnector insDbConnector)
        {
            DataTable insDt = new DataTable();
            insDt = insDbConnector.ExecuteSqlDataTable("SELECT  		DISTINCT   		Member.NameSurname as [MerchNameSurname], MemberRoute.Id as [MemberRouteId],  		MemberRoute.EffectiveDate as [DateOfVisit],  		CASE(DATENAME(weekday,MemberRoute.EffectiveDate))  			WHEN 'Monday' THEN 'Pazartesi'  			WHEN 'Tuesday' THEN 'Salı'  			WHEN 'Wednesday' THEN 'Çarşamba'  			WHEN 'Thursday' THEN 'Perşembe'  			WHEN 'Friday' THEN 'Cuma'  			WHEN 'Saturday' THEN 'Cumartesi'  			WHEN 'Sunday' THEN 'Pazar'  			ELSE ''  		END as [Day],  		Retail.Name as [RetailName],  		RetailShop.Name as [RetailShopName], MemberRouteDetail.PromotionTypeId, " +
                                                       "    CASE MemberRouteDetail.PromotionTypeId WHEN 1 THEN 'YOK' " +
                                                       "                                           WHEN 2 THEN 'Alecentral' " +
                                                       "                                           WHEN 3 THEN 'Asalak Stand' " +
                                                       "                                           WHEN 4 THEN 'Gondol Başı' " +
                                                       "                                           WHEN 5 THEN 'Havuz' " +
                                                       "                                           WHEN 6 THEN 'Kasa Önü' " +
                                                       "                                           WHEN 7 THEN 'Palet' " +
                                                       "                                           WHEN 8 THEN 'Sepet' " +
                                                       "                                           WHEN 9 THEN 'Diğer' " +
                                                       "                                           WHEN 10 THEN 'Devam Eden Teşhir' " +
                                                       "                                           WHEN 11 THEN 'Çapraz Teşhir' " +
                                                       "                                           ELSE 'Belirtilmemiş' " +
                                                       "END AS [PromotionTypeName], " +
                                                       "COUNT(MemberRouteDetail.Id) as [PromotionCount] " +
                                                       "FROM  		MemberRoute WITH (NOLOCK)  		INNER JOIN Member WITH (NOLOCK) ON Member.Id = MemberRoute.MemberId  		INNER JOIN MemberCustomer WITH (NOLOCK) ON Member.Id = MemberCustomer.MemberId  		INNER JOIN MemberRouteDetail WITH (NOLOCK) ON MemberRoute.Id = MemberRouteDetail.MemberRouteId  		INNER JOIN RetailShop WITH (NOLOCK) ON MemberRoute.RetailShopId = RetailShop.Id  		INNER JOIN Retail WITH (NOLOCK) ON RetailShop.RetailId = Retail.Id  	WHERE  		MemberRoute.EffectiveDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + " 00:00:00' AND '" + endDate.ToString("yyyy-MM-dd") + " 23:59:59' AND  		MemberCustomer.CustomerId = " + Convert.ToString(customerId) + " AND  		Member.MemberTitleId IN (5) AND MemberRouteDetail.PromotionTypeId IN (2,3,4,5,6,7,8,9,10,11) AND MemberRoute.EntryOn IS NOT NULL " +
                                                       "GROUP BY Member.NameSurname, MemberRoute.Id, MemberRouteDetail.PromotionTypeId, MemberRoute.EffectiveDate, CASE(DATENAME(weekday,MemberRoute.EffectiveDate))  			WHEN 'Monday' THEN 'Pazartesi'  			WHEN 'Tuesday' THEN 'Salı'  			WHEN 'Wednesday' THEN 'Çarşamba'  			WHEN 'Thursday' THEN 'Perşembe'  			WHEN 'Friday' THEN 'Cuma'  			WHEN 'Saturday' THEN 'Cumartesi'  			WHEN 'Sunday' THEN 'Pazar'  			ELSE ''  		END, Retail.Name,  		RetailShop.Name " +
                                                       "ORDER BY  		Member.NameSurname,  		MemberRoute.EffectiveDate");
            return insDt;
        }

        public DataTable TeshirUrunRaporu(DateTime startDate, DateTime endDate, int customerId, DbConnector insDbConnector)
        {
            DataTable insDt = new DataTable();
            insDt = insDbConnector.ExecuteSqlDataTable("SELECT  		DISTINCT   		Member.NameSurname as [MerchNameSurname], MemberRoute.Id as [MemberRouteId],  		MemberRoute.EffectiveDate as [DateOfVisit],  		CASE(DATENAME(weekday,MemberRoute.EffectiveDate))  			WHEN 'Monday' THEN 'Pazartesi'  			WHEN 'Tuesday' THEN 'Salı'  			WHEN 'Wednesday' THEN 'Çarşamba'  			WHEN 'Thursday' THEN 'Perşembe'  			WHEN 'Friday' THEN 'Cuma'  			WHEN 'Saturday' THEN 'Cumartesi'  			WHEN 'Sunday' THEN 'Pazar'  			ELSE ''  		END as [Day],  		Retail.Name as [RetailName],  		RetailShop.Name as [RetailShopName], MemberRouteDetail.PromotionTypeId, " +
                                                       "    CASE MemberRouteDetail.PromotionTypeId WHEN 1 THEN 'YOK' " +
                                                       "                                           WHEN 2 THEN 'Alecentral' " +
                                                       "                                           WHEN 3 THEN 'Asalak Stand' " +
                                                       "                                           WHEN 4 THEN 'Gondol Başı' " +
                                                       "                                           WHEN 5 THEN 'Havuz' " +
                                                       "                                           WHEN 6 THEN 'Kasa Önü' " +
                                                       "                                           WHEN 7 THEN 'Palet' " +
                                                       "                                           WHEN 8 THEN 'Sepet' " +
                                                       "                                           WHEN 9 THEN 'Diğer' " +
                                                       "                                           WHEN 10 THEN 'Devam Eden Teşhir' " +
                                                       "                                           WHEN 11 THEN 'Çapraz Teşhir' " +
                                                       "                                           ELSE 'Belirtilmemiş' " +
                                                       "END AS [PromotionTypeName], " +
                                                       "CustomerProduct.Name as [CustomerProductName] " +
                                                       "FROM  		MemberRoute WITH (NOLOCK)  		" +
                                                       "            INNER JOIN Member WITH (NOLOCK) ON Member.Id = MemberRoute.MemberId  		" +
                                                       "            INNER JOIN MemberCustomer WITH (NOLOCK) ON Member.Id = MemberCustomer.MemberId  		" +
                                                       "            INNER JOIN MemberRouteDetail WITH (NOLOCK) ON MemberRoute.Id = MemberRouteDetail.MemberRouteId  		" +
                                                       "            INNER JOIN RetailShop WITH (NOLOCK) ON MemberRoute.RetailShopId = RetailShop.Id  		" +
                                                       "            INNER JOIN Retail WITH (NOLOCK) ON RetailShop.RetailId = Retail.Id  	" +
                                                       "            INNER JOIN CustomerProduct WITH (NOLOCK) ON MemberRouteDetail.CustomerProductId = CustomerProduct.Id " +
                                                       "WHERE  		MemberRoute.EffectiveDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + " 00:00:00' AND '" + endDate.ToString("yyyy-MM-dd") + " 23:59:59' AND  		MemberCustomer.CustomerId = " + Convert.ToString(customerId) + " AND  		Member.MemberTitleId IN (5) AND MemberRouteDetail.PromotionTypeId IN (2,3,4,5,6,7,8,9,10,11) AND MemberRoute.EntryOn IS NOT NULL " +
                                                       "ORDER BY  		Member.NameSurname,  		MemberRoute.EffectiveDate");
            return insDt;
        }

        public DataTable VarYokRaporu(DateTime startDate, DateTime endDate, int customerId, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pStartDate", startDate);
            insDbParamCollection.Add("@pEndDate", endDate);
            insDbParamCollection.Add("@pCustomerId", customerId);
            return insDbConnector.ExecuteDataTable("SelectVarYokRaporu", insDbParamCollection);
        }

        public DataTable OnyuzRaporu(DateTime startDate, DateTime endDate, int customerId, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pStartDate", startDate);
            insDbParamCollection.Add("@pEndDate", endDate);
            insDbParamCollection.Add("@pCustomerId", customerId);
            return insDbConnector.ExecuteDataTable("SelectOnyuzRaporu", insDbParamCollection);
        }

        public DataTable SelectProductListingPerRetailShopByCustomerId(int customerId, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pCustomerId", customerId);
            return insDbConnector.ExecuteDataTable("SelectProductListingPerRetailShopByCustomerId", insDbParamCollection);
        }

        public DataTable SKTRaporu(DateTime startDate, DateTime endDate, int customerId, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pCustomerId", customerId);
            insDbParamCollection.Add("@pStartDate", startDate);
            insDbParamCollection.Add("@pEndDate", endDate);
            return insDbConnector.ExecuteDataTable("SKTRaporu", insDbParamCollection);
        }

        public DataTable AktiviteRaporu(DateTime startDate, DateTime endDate, int customerId, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pCustomerId", customerId);
            insDbParamCollection.Add("@pStartDate", startDate);
            insDbParamCollection.Add("@pEndDate", endDate);
            return insDbConnector.ExecuteDataTable("AktiviteRaporu", insDbParamCollection);
        }

        public DataTable OnyuzRaporuV2(DateTime startDate, DateTime endDate, int customerId, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pStartDate", startDate);
            insDbParamCollection.Add("@pEndDate", endDate);
            insDbParamCollection.Add("@pCustomerId", customerId);

            return insDbConnector.ExecuteDataTable("SelectOnyuzRaporuV2", insDbParamCollection);
        }

        public DataTable SelectRouteAnalysisVisitDataByCustomerIdStartDateEndDate(DateTime startDate, DateTime endDate, int customerId, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pStartDate", startDate);
            insDbParamCollection.Add("@pEndDate", endDate);
            insDbParamCollection.Add("@pCustomerId", customerId);
            return insDbConnector.ExecuteDataTable("SelectRouteAnalysisVisitDataByCustomerIdStartDateEndDate", insDbParamCollection);
        }

        public DataTable RouteAnalysis_KanalDagilimi(DateTime startDate, DateTime endDate, int customerId, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pStartDate", startDate);
            insDbParamCollection.Add("@pEndDate", endDate);
            insDbParamCollection.Add("@pCustomerId", customerId);
            return insDbConnector.ExecuteDataTable("RouteAnalysis_KanalDagilimi", insDbParamCollection);
        }

        public DataTable RouteAnalysis_IlMagazaSayisi(DateTime startDate, DateTime endDate, int customerId, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pStartDate", startDate);
            insDbParamCollection.Add("@pEndDate", endDate);
            insDbParamCollection.Add("@pCustomerId", customerId);
            return insDbConnector.ExecuteDataTable("RouteAnalysis_IlMagazaSayisi", insDbParamCollection);
        }

        public DataTable RouteAnalysis_IlKanalSayisi(DateTime startDate, DateTime endDate, int customerId, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pStartDate", startDate);
            insDbParamCollection.Add("@pEndDate", endDate);
            insDbParamCollection.Add("@pCustomerId", customerId);
            return insDbConnector.ExecuteDataTable("RouteAnalysis_IlKanalSayisi", insDbParamCollection);
        }

        public DataTable RouteAnalysis_NatFirmaKirilimi(DateTime startDate, DateTime endDate, int customerId, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pStartDate", startDate);
            insDbParamCollection.Add("@pEndDate", endDate);
            insDbParamCollection.Add("@pCustomerId", customerId);
            return insDbConnector.ExecuteDataTable("RouteAnalysis_NatFirmaKirilimi", insDbParamCollection);
        }

        public DataTable RouteAnalysis_NatFormatKirilimi(DateTime startDate, DateTime endDate, int customerId, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pStartDate", startDate);
            insDbParamCollection.Add("@pEndDate", endDate);
            insDbParamCollection.Add("@pCustomerId", customerId);
            return insDbConnector.ExecuteDataTable("RouteAnalysis_NatFormatKirilimi", insDbParamCollection);
        }

        public DataTable RouteAnalysis_LokalDagilimi(DateTime startDate, DateTime endDate, int customerId, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pStartDate", startDate);
            insDbParamCollection.Add("@pEndDate", endDate);
            insDbParamCollection.Add("@pCustomerId", customerId);
            return insDbConnector.ExecuteDataTable("RouteAnalysis_LokalDagilimi", insDbParamCollection);
        }

        public DataTable RouteAnalysis_MerchNokta(DateTime startDate, DateTime endDate, int customerId, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pStartDate", startDate);
            insDbParamCollection.Add("@pEndDate", endDate);
            insDbParamCollection.Add("@pCustomerId", customerId);
            return insDbConnector.ExecuteDataTable("RouteAnalysis_MerchNokta", insDbParamCollection);
        }

        public DataTable RouteAnalysis_KisiBasiAverajNokta(DateTime startDate, DateTime endDate, int customerId, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pStartDate", startDate);
            insDbParamCollection.Add("@pEndDate", endDate);
            insDbParamCollection.Add("@pCustomerId", customerId);
            return insDbConnector.ExecuteDataTable("RouteAnalysis_KisiBasiAverajNokta", insDbParamCollection);
        }

        public DataTable RouteAnalysis_FrekansKirilimi(DateTime startDate, DateTime endDate, int customerId, DbConnector insDbConnector)
        {
            string query = "WITH TempData " +
                           "AS " +
                           "( " +
                           "SELECT " +
                           "RetailShopName, " +
                           "CAST(COUNT(RetailShopId) as decimal(4, 2)) / CAST(2 as decimal(4, 2)) as [Frekans], " +
                           "CASE CAST(COUNT(RetailShopId) as decimal(4, 2)) / CAST(2 as decimal(4, 2)) " +
                           "    WHEN 0.5 THEN '2 Haftada 1' " +
                           "    WHEN 1 THEN 'Haftada 1' " +
                           "    WHEN 2 THEN 'Haftada 2' " +
                           "    WHEN 3 THEN 'Haftada 3' " +
                           "    WHEN 4 THEN 'Haftada 4' " +
                           "    WHEN 5 THEN 'Haftada 5' " +
                           "    WHEN 6 THEN 'Haftada 6' " +
                           "    ELSE 'Hesaplanamayan' " +
                           "END AS FrekansText " +
                           "FROM vw_RouteAnalysis_IlKanalSayisi WHERE EffectiveDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + " 00:00:00' AND '" + endDate.ToString("yyyy-MM-dd") + " 23:59:59' AND CustomerId = " + Convert.ToString(customerId) + " " +
                           "GROUP BY " +
                           "    RetailShopName " +
                           ") " +
                           "SELECT FrekansText, COUNT(Frekans) as [Frekans], 0.00 AS [Total], 0.00 AS [Percent] FROM TempData GROUP BY FrekansText ORDER BY FrekansText";

            DataTable insDt = new DataTable();
            insDt = insDbConnector.ExecuteSqlDataTable(query);
            return insDt;
        }
        #endregion
    }
}