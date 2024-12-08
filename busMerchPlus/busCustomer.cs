using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using SqlHelper;
using System.Data;
using entMerchPlus;
using datMerchPlus;
using Infrastructure;
using System.Linq;

namespace busMerchPlus
{
    /// <summary>
    /// This layer is responsible for implementing business logic.
    /// Manages transactions on table [Customer]
    /// </summary>
    public class busCustomer : BusBase
    {
        /// <summary>
        /// busCustomerConstructor method used while taking an instance of this class.
        /// </summary>
        public busCustomer() : base()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Selects all data stored in table [Customer]
        /// </summary>
        public DataTable SelectCustomer()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomer insDatCustomer = new datCustomer();
                return insDatCustomer.SelectCustomer(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Selects one row of data stored in table [Customer] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntCustomer">Gets entity object as parameter for table Customer]</param>
        public void SelectCustomerById(entCustomer parEntCustomer)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomer insDatCustomer = new datCustomer();
                insDatCustomer.SelectCustomerById(parEntCustomer, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Inserts one row of data into table [Customer] based on the values of input entity object
        /// </summary>
        /// <param name="parEntCustomer">Gets entity object as parameter for table Customer]</param>
        public void InsertCustomer(entCustomer parEntCustomer)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomer insDatCustomer = new datCustomer();
                insDatCustomer.InsertCustomer(parEntCustomer, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Updates one row of data in table [Customer] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntCustomer">Gets entity object as parameter for table Customer]</param>
        public void UpdateCustomerById(entCustomer parEntCustomer)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomer insDatCustomer = new datCustomer();
                insDatCustomer.UpdateCustomerById(parEntCustomer, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes all data stored in table [Customer]
        /// </summary>
        public void DeleteCustomer()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomer insDatCustomer = new datCustomer();
                insDatCustomer.DeleteCustomer(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes data stored in table [Customer] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntCustomer">Gets entity object as parameter for table Customer]</param>
        public void DeleteCustomerById(entCustomer parEntCustomer)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomer insDatCustomer = new datCustomer();
                insDatCustomer.DeleteCustomerById(parEntCustomer, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        #endregion
        #region Custom Methods
        public DataTable FrekansRaporu(DateTime startDate, DateTime endDate, int customerId)
        {
            DbConnector insDbConnector = new DbConnector();
            datCustomer insDatCustomer = new datCustomer();
            try
            {
                endDate = endDate.AddHours(23).AddMinutes(59).AddSeconds(59);
                string query = "DECLARE @pStartDate datetime  DECLARE @pEndDate datetime    SET @pStartDate = '" + startDate.ToString("yyyy-MM-dd") + " 00:00:00'  SET @pEndDate = '" + endDate.ToString("yyyy-MM-dd") + " 23:59:59'  SELECT  		DISTINCT   		Member.NameSurname as [MerchNameSurname],  		(SELECT COUNT(MemberRoute2.Id) FROM MemberRoute MemberRoute2 WITH (NOLOCK) WHERE MemberRoute2.MemberId = MemberRoute.MemberId AND MemberRoute2.EffectiveDate BETWEEN @pStartDate AND @pEndDate) as [PlannedLocationCount],  		(SELECT COUNT(MemberRoute3.Id) FROM MemberRoute MemberRoute3 WITH (NOLOCK) WHERE MemberRoute3.MemberId = MemberRoute.MemberId AND EntryOn IS NOT NULL AND MemberRoute3.EffectiveDate BETWEEN @pStartDate AND @pEndDate) as [ActualLocationCount],  		CAST(  			CAST(  				(SELECT COUNT(MemberRoute3.Id) FROM MemberRoute MemberRoute3 WITH (NOLOCK) WHERE MemberRoute3.MemberId = MemberRoute.MemberId AND EntryOn IS NOT NULL AND MemberRoute3.EffectiveDate BETWEEN @pStartDate AND @pEndDate)   				as decimal(18,4)  				)   				/   			CAST(   				(SELECT COUNT(MemberRoute2.Id) FROM MemberRoute MemberRoute2 WITH (NOLOCK) WHERE MemberRoute2.MemberId = MemberRoute.MemberId AND MemberRoute2.EffectiveDate BETWEEN @pStartDate AND @pEndDate)   				as decimal(18,4)  				) * 100  			as int) as [SuccessPercent]  	FROM  		MemberRoute WITH (NOLOCK)  		INNER JOIN Member WITH (NOLOCK) ON Member.Id = MemberRoute.MemberId  		INNER JOIN MemberCustomer WITH (NOLOCK) ON Member.Id = MemberCustomer.MemberId  	WHERE  		MemberRoute.EffectiveDate BETWEEN @pStartDate AND @pEndDate AND  		MemberCustomer.CustomerId = " + Convert.ToString(customerId) + " AND  		Member.MemberTitleId IN (5)  	ORDER BY  		Member.NameSurname";
                return insDatCustomer.FrekansRaporu(query, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public DataTable RafFiyatlariRaporu(DateTime startDate, DateTime endDate, int customerId)
        {
            DbConnector insDbConnector = new DbConnector();
            datCustomer insDatCustomer = new datCustomer();
            try
            {
                endDate = endDate.AddHours(23).AddMinutes(59).AddSeconds(59);
                //string query = "SELECT  		DISTINCT   		Member.NameSurname as [MerchNameSurname],  		MemberRoute.EffectiveDate as [DateOfVisit],  		CASE(DATENAME(weekday,MemberRoute.EffectiveDate))  			WHEN 'Monday' THEN 'Pazartesi'  			WHEN 'Tuesday' THEN 'Salı'  			WHEN 'Wednesday' THEN 'Çarşamba'  			WHEN 'Thursday' THEN 'Perşembe'  			WHEN 'Friday' THEN 'Cuma'  			WHEN 'Saturday' THEN 'Cumartesi'  			WHEN 'Sunday' THEN 'Pazar'  			ELSE ''  		END as [Day],  		Retail.Name as [RetailName],  		RetailShop.Name as [RetailShopName],  		CustomerBrand.Name as [CustomerProductBrand],  		CustomerProduct.Name as [CustomerProduct],  		CustomerProduct.PackagingSize as [PackagingSize],  		PackagingType.Name as [PackagingType],  		MemberRouteDetail.ShelfPrice as [ShelfPrice],  		CustomerBrand2.Name as [CustomerProductBrand],  		CustomerProduct2.Name as [CustomerProduct],  		CustomerProduct2.PackagingSize as [PackagingSize2],  		PackagingType2.Name as [PackagingType2],  		(SELECT TOP 1 ISNULL(ShelfPrice,0) FROM MemberRouteDetail MemberRouteDetail2 WHERE MemberRouteDetail2.CustomerProductId = CustomerProduct2.Id AND MemberRouteDetail2.MemberRouteId = MemberRoute.Id ORDER BY MemberRouteDetail2.Id DESC) as [CompetitorShelfPrice],  		(SELECT TOP 1 (CAST( ISNULL(ShelfPrice,0) as decimal(18,4)) / CAST(CustomerProduct2.PackagingSize as decimal(18,4))) * CAST(CustomerProduct.PackagingSize as decimal(18,4)) FROM MemberRouteDetail MemberRouteDetail2 WHERE MemberRouteDetail2.CustomerProductId = CustomerProduct2.Id AND MemberRouteDetail2.MemberRouteId = MemberRoute.Id ORDER BY MemberRouteDetail2.Id DESC) as [CompetitorShelfPriceAdjusted],  		CASE (SELECT TOP 1 (CAST( ISNULL(ShelfPrice,0) as decimal(18,4)) / CAST(CustomerProduct2.PackagingSize as decimal(18,4))) * CAST(CustomerProduct.PackagingSize as decimal(18,4)) FROM MemberRouteDetail MemberRouteDetail2 WHERE MemberRouteDetail2.CustomerProductId = CustomerProduct2.Id AND MemberRouteDetail2.MemberRouteId = MemberRoute.Id ORDER BY MemberRouteDetail2.Id DESC)  			WHEN 0 THEN NULL  			ELSE CAST(1 as decimal(18,2)) - ( MemberRouteDetail.ShelfPrice / (SELECT TOP 1 (CAST( ISNULL(ShelfPrice,0) as decimal(18,4)) / CAST(CustomerProduct2.PackagingSize as decimal(18,4))) * CAST(CustomerProduct.PackagingSize as decimal(18,4)) FROM MemberRouteDetail MemberRouteDetail2 WHERE MemberRouteDetail2.CustomerProductId = CustomerProduct2.Id AND MemberRouteDetail2.MemberRouteId = MemberRoute.Id ORDER BY MemberRouteDetail2.Id DESC) )  		END as [Ratio]  	FROM  		MemberRoute WITH (NOLOCK)  		INNER JOIN Member WITH (NOLOCK) ON Member.Id = MemberRoute.MemberId  		INNER JOIN MemberCustomer WITH (NOLOCK) ON Member.Id = MemberCustomer.MemberId  		INNER JOIN MemberRouteDetail WITH (NOLOCK) ON MemberRoute.Id = MemberRouteDetail.MemberRouteId  		INNER JOIN RetailShop WITH (NOLOCK) ON MemberRoute.RetailShopId = RetailShop.Id  		INNER JOIN Retail WITH (NOLOCK) ON RetailShop.RetailId = Retail.Id  		INNER JOIN CustomerProduct WITH (NOLOCK) ON MemberRouteDetail.CustomerProductId = CustomerProduct.Id  		INNER JOIN CustomerBrand WITH (NOLOCK) ON CustomerProduct.CustomerBrandId = CustomerBrand.Id  		INNER JOIN CustomerProductCompetitor WITH (NOLOCK) ON CustomerProductCompetitor.CustomerProductId = CustomerProduct.Id  		INNER JOIN CustomerProduct CustomerProduct2 WITH (NOLOCK) ON CustomerProductCompetitor.CompetitorCustomerProductId = CustomerProduct2.Id  		INNER JOIN CustomerBrand CustomerBrand2 WITH (NOLOCK) ON CustomerProduct2.CustomerBrandId = CustomerBrand2.Id  		INNER JOIN PackagingType WITH (NOLOCK) ON PackagingType.Id = CustomerProduct.PackagingTypeId  		INNER JOIN PackagingType PackagingType2 WITH (NOLOCK) ON PackagingType2.Id = CustomerProduct2.PackagingTypeId  	WHERE  		MemberRoute.EffectiveDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + " 00:00:00' AND '" + endDate.ToString("yyyy-MM-dd") + " 23:59:59' AND  		MemberCustomer.CustomerId = " + Convert.ToString(customerId) + " AND  		Member.MemberTitleId IN (5) AND  		MemberRouteDetail.IsSentToServer=1 AND  		MemberRouteDetail.ShelfPrice IS NOT NULL   		AND CustomerBrand.Id IN (SELECT Id FROM CustomerBrand WHERE CustomerId = 1)  	ORDER BY  		Member.NameSurname,  		MemberRoute.EffectiveDate";
                string query = "SELECT  		" +
                                "    DISTINCT " +
                                "       Member.NameSurname as [MerchNameSurname], " +
                                "		MemberRoute.EffectiveDate as [DateOfVisit], " +
                                "		CASE(DATENAME(weekday, MemberRoute.EffectiveDate))           WHEN 'Monday' THEN 'Pazartesi'              WHEN 'Tuesday' THEN 'Salı'              WHEN 'Wednesday' THEN 'Çarşamba'            WHEN 'Thursday' THEN 'Perşembe'             WHEN 'Friday' THEN 'Cuma'           WHEN 'Saturday' THEN 'Cumartesi'            WHEN 'Sunday' THEN 'Pazar'              ELSE ''         END as [Day], " +
                                "		Retail.Name as [RetailName],  		 " +
                                "		RetailShop.Name as [RetailShopName],  	 " +
                                "		CustomerBrand.Name as [CustomerProductBrand], " +
                                "		CustomerProduct.Name as [CustomerProduct],  	 " +
                                "		CustomerProduct.PackagingSize as [PackagingSize],  	 " +
                                "		PackagingType.Name as [PackagingType],  		 " +
                                "		MemberRouteDetail.ShelfPrice as [ShelfPrice],  		 " +
                                "		CustomerBrand2.Name as [CustomerProductBrand2],  		 " +
                                "		CustomerProduct2.Name as [CustomerProduct2],  		 " +
                                "		CustomerProduct2.PackagingSize as [PackagingSize2],  	 " +
                                "		PackagingType2.Name as [PackagingType2],  		 " +
                                "       MemberRouteDetail.DiscountedPrice as [ActivityPrice], " +
                                "       CustomerProduct.SuggestedActivityPrice," +
                                "		( " +
                                "            SELECT TOP 1 ISNULL(ShelfPrice, 0) FROM MemberRouteDetail MemberRouteDetail2 WHERE MemberRouteDetail2.CustomerProductId = CustomerProduct2.Id AND MemberRouteDetail2.MemberRouteId = MemberRoute.Id ORDER BY MemberRouteDetail2.Id DESC " +
                                "		) as [CompetitorShelfPrice],  		 " +
                                "		( " +
                                "            SELECT TOP 1(CAST(ISNULL(ShelfPrice, 0) as decimal(18, 4)) / CAST(CustomerProduct2.PackagingSize as decimal(18, 4))) * CAST(CustomerProduct.PackagingSize as decimal(18, 4)) FROM MemberRouteDetail MemberRouteDetail2 WHERE MemberRouteDetail2.CustomerProductId = CustomerProduct2.Id AND MemberRouteDetail2.MemberRouteId = MemberRoute.Id ORDER BY MemberRouteDetail2.Id DESC " +
                                "		) as [CompetitorShelfPriceAdjusted], " +
                                "		CASE(SELECT TOP 1(CAST(ISNULL(ShelfPrice, 0) as decimal(18, 4)) / CAST(CustomerProduct2.PackagingSize as decimal(18, 4))) * CAST(CustomerProduct.PackagingSize as decimal(18, 4)) FROM MemberRouteDetail MemberRouteDetail2 WHERE MemberRouteDetail2.CustomerProductId = CustomerProduct2.Id AND MemberRouteDetail2.MemberRouteId = MemberRoute.Id ORDER BY MemberRouteDetail2.Id DESC)              WHEN 0 THEN NULL            ELSE CAST(1 as decimal(18, 2)) -(MemberRouteDetail.ShelfPrice / (SELECT TOP 1(CAST(ISNULL(ShelfPrice, 0) as decimal(18, 4)) / CAST(CustomerProduct2.PackagingSize as decimal(18, 4))) * CAST(CustomerProduct.PackagingSize as decimal(18, 4)) FROM MemberRouteDetail MemberRouteDetail2 WHERE MemberRouteDetail2.CustomerProductId = CustomerProduct2.Id AND MemberRouteDetail2.MemberRouteId = MemberRoute.Id ORDER BY MemberRouteDetail2.Id DESC) )  		END as [Ratio], " +
                                "		CASE CustomerProduct.PackagingSize " +
                                 "           WHEN 0 THEN 0 " +
                                "            ELSE CAST(ROUND((CAST(MemberRouteDetail.ShelfPrice as decimal(18, 4)) / CAST(CustomerProduct.PackagingSize as decimal(18, 4))) *CAST(1000 as decimal(18, 4)) ,2) as numeric(36, 2) ) " +
                                "		END AS[CustomerKilogramPrice], " +
                                "       CASE CustomerProduct2.PackagingSize " +
                                "            WHEN 0 THEN 0 " +
                                "            ELSE CAST(ROUND((CAST((SELECT TOP 1 ISNULL(ShelfPrice, 0) FROM MemberRouteDetail MemberRouteDetail2 WHERE MemberRouteDetail2.CustomerProductId = CustomerProduct2.Id AND MemberRouteDetail2.MemberRouteId = MemberRoute.Id ORDER BY MemberRouteDetail2.Id DESC) as decimal(18, 4)) / CAST(CustomerProduct2.PackagingSize as decimal(18, 4))) *CAST(1000 as decimal(18, 4)) ,2) as numeric(36, 2) ) " +
                                "		END AS[CompetitorKilogramPrice], " +
                                "		CustomerProduct.SuggestedListPrice " +
                                "FROM " +
                                "    MemberRoute WITH(NOLOCK) " +
                                "    INNER JOIN Member WITH(NOLOCK) ON Member.Id = MemberRoute.MemberId " +
                                "    INNER JOIN MemberCustomer WITH(NOLOCK) ON Member.Id = MemberCustomer.MemberId " +
                                "    INNER JOIN MemberRouteDetail WITH(NOLOCK) ON MemberRoute.Id = MemberRouteDetail.MemberRouteId " +
                                "    INNER JOIN RetailShop WITH(NOLOCK) ON MemberRoute.RetailShopId = RetailShop.Id " +
                                "    INNER JOIN Retail WITH(NOLOCK) ON RetailShop.RetailId = Retail.Id " +
                                "    INNER JOIN CustomerProduct WITH(NOLOCK) ON MemberRouteDetail.CustomerProductId = CustomerProduct.Id " +
                                "    INNER JOIN CustomerBrand WITH(NOLOCK) ON CustomerProduct.CustomerBrandId = CustomerBrand.Id " +
                                "    LEFT JOIN CustomerProductCompetitor WITH(NOLOCK) ON CustomerProductCompetitor.CustomerProductId = CustomerProduct.Id " +
                                "    LEFT JOIN CustomerProduct CustomerProduct2 WITH(NOLOCK) ON CustomerProductCompetitor.CompetitorCustomerProductId = CustomerProduct2.Id " +
                                "    LEFT JOIN CustomerBrand CustomerBrand2 WITH(NOLOCK) ON CustomerProduct2.CustomerBrandId = CustomerBrand2.Id " +
                                "    LEFT JOIN PackagingType WITH(NOLOCK) ON PackagingType.Id = CustomerProduct.PackagingTypeId " +
                                "    LEFT JOIN PackagingType PackagingType2 WITH(NOLOCK) ON PackagingType2.Id = CustomerProduct2.PackagingTypeId " +
                                "WHERE " +
                                "    MemberRoute.EffectiveDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + " 00:00:00' AND '" + endDate.ToString("yyyy-MM-dd") + " 23:59:59' AND " +
                                "    MemberCustomer.CustomerId = " + Convert.ToString(customerId) + " AND " +
                                "    Member.MemberTitleId IN(5) AND " +
                                "    MemberRouteDetail.IsSentToServer = 1 AND " +
                                "    MemberRouteDetail.ShelfPrice IS NOT NULL AND " +
                                "    CustomerBrand.Id IN " +
                                "    ( " +
                                "        SELECT Id FROM CustomerBrand WHERE CustomerId = " + Convert.ToString(customerId) + " " +
                                "    ) " +
                                "ORDER BY " +
                                "    Member.NameSurname,  		 " +
                                "	MemberRoute.EffectiveDate";
                DataTable insDt = new DataTable();
                insDt = insDatCustomer.FrekansRaporu(query, insDbConnector);

                List<RafFiyatlariItem> target = insDt.AsEnumerable()
                .Select(row => new RafFiyatlariItem
                {
                    // assuming column 0's type is Nullable<long>

                    MerchNameSurname = row.Field<string>(0),
                    DateOfVisit = row.Field<DateTime>(1),
                    Day = row.Field<string>(2),
                    RetailName = row.Field<string>(3),
                    RetailShopName = row.Field<string>(4),
                    CustomerProductBrand = row.Field<string>(5),
                    CustomerProduct = row.Field<string>(6),
                    PackagingSize = row.Field<decimal?>(7),
                    PackagingType = row.Field<string>(8),
                    ShelfPrice = row.Field<decimal?>(9),
                    CustomerProductBrand2 = row.Field<string>(10),
                    CustomerProduct2 = row.Field<string>(11),
                    PackagingSize2 = row.Field<decimal?>(12),
                    PackagingType2 = row.Field<string>(13),
                    ActivityPrice = row.Field<decimal?>(14),
                    SuggestedActivityPrice = row.Field<decimal?>(15),
                    CompetitorShelfPrice = row.Field<decimal?>(16),
                    CompetitorShelfPriceAdjusted = row.Field<decimal?>(17),
                    Ratio = row.Field<decimal?>(18),
                    CustomerKilogramPrice = row.Field<decimal?>(19),
                    CompetitorKilogramPrice = row.Field<decimal?>(20),
                    SuggestedListPrice = row.Field<decimal?>(21),
                    RatioNew = 0,
                    SuggestedListPriceRatio = 0
                }).ToList();

                foreach (var insDr in target)
                {
                    if (insDr.CustomerKilogramPrice != null && insDr.CustomerKilogramPrice != 0 && insDr.CompetitorKilogramPrice != null && insDr.CompetitorKilogramPrice != 0)
                    {
                        //try
                        //{
                        //    insDr.RatioNew = (Convert.ToDecimal(insDr.CustomerKilogramPrice) / Convert.ToDecimal(insDr.CompetitorKilogramPrice)) - Convert.ToDecimal(1);
                        //    insDr.RatioNew = Math.Round(Convert.ToDecimal(insDr.RatioNew), 3);
                        //}
                        //catch (Exception ex)
                        //{
                        //    insDr.RatioNew = Math.Round(Convert.ToDecimal(0), 2);
                        //}

                        insDr.RatioNew = (Convert.ToDecimal(insDr.CustomerKilogramPrice) / Convert.ToDecimal(insDr.CompetitorKilogramPrice)) - Convert.ToDecimal(1);
                        insDr.RatioNew = Math.Round(Convert.ToDecimal(insDr.RatioNew), 3);

                    }
                    else
                    {
                        insDr.RatioNew = Math.Round(Convert.ToDecimal(0), 2);
                    }

                    if (insDr.SuggestedListPrice != null && insDr.ShelfPrice != null && insDr.ShelfPrice != 0 && insDr.SuggestedListPrice != 0)
                    {
                        //try
                        //{
                        //    insDr.SuggestedListPriceRatio = (Convert.ToDecimal(insDr.ShelfPrice) / Convert.ToDecimal(insDr.SuggestedListPrice)) - Convert.ToDecimal(1);
                        //    insDr.SuggestedListPriceRatio = Math.Round(Convert.ToDecimal(insDr.SuggestedListPriceRatio), 3);
                        //}
                        //catch (Exception ex)
                        //{
                        //    insDr.SuggestedListPriceRatio = Math.Round(Convert.ToDecimal(0), 2);
                        //}

                        insDr.SuggestedListPriceRatio = (Convert.ToDecimal(insDr.ShelfPrice) / Convert.ToDecimal(insDr.SuggestedListPrice)) - Convert.ToDecimal(1);
                        insDr.SuggestedListPriceRatio = Math.Round(Convert.ToDecimal(insDr.SuggestedListPriceRatio), 3);
                    }
                }

                GC.Collect();
                GC.Collect();
                GC.Collect();

                var hede = (from item in target
                            where item.ShelfPrice != 0
                            select item).ToList();

                GC.Collect();
                GC.Collect();
                GC.Collect();

                insDt = ToDataTable(hede);
                insDt.AcceptChanges();

                GC.Collect();
                GC.Collect();
                GC.Collect();

                return insDt;

            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public DataTable SelectRouteAnalysisVisitDataByCustomerIdStartDateEndDate(DateTime startDate, DateTime endDate, int customerId)
        {
            DbConnector insDbConnector = new DbConnector();
            datCustomer insDatCustomer = new datCustomer();
            try
            {
                return insDatCustomer.SelectRouteAnalysisVisitDataByCustomerIdStartDateEndDate(startDate, endDate, customerId, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public DataTable RouteAnalysis_KanalDagilimi(DateTime startDate, DateTime endDate, int customerId)
        {
            DbConnector insDbConnector = new DbConnector();
            datCustomer insDatCustomer = new datCustomer();
            try
            {
                DataTable insDt = new DataTable();
                insDt = insDatCustomer.RouteAnalysis_KanalDagilimi(startDate, endDate, customerId, insDbConnector);

                int total = Convert.ToInt32(insDt.Compute("SUM(Frequency)", "1=1"));
                foreach (DataRow insDr in insDt.Rows)
                {
                    insDr["Total"] = total;
                }
                insDt.AcceptChanges();
                return insDt;
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public DataTable RouteAnalysis_IlMagazaSayisi(DateTime startDate, DateTime endDate, int customerId)
        {
            DbConnector insDbConnector = new DbConnector();
            datCustomer insDatCustomer = new datCustomer();
            try
            {
                return insDatCustomer.RouteAnalysis_IlMagazaSayisi(startDate, endDate, customerId, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public DataTable SKTRaporu(DateTime StartDate, DateTime EndDate, int CustomerId)
        {
            DbConnector insDbConnector = new DbConnector();
            datCustomer insDatCustomer = new datCustomer();
            try
            {
                return insDatCustomer.SKTRaporu(StartDate, EndDate, CustomerId, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public DataTable RouteUyumlulukRaporu(DateTime startDate, DateTime endDate, int customerId)
        {
            DbConnector insDbConnector = new DbConnector();
            datCustomer insDatCustomer = new datCustomer();
            try
            {
                endDate = endDate.AddHours(23).AddMinutes(59).AddSeconds(59);
                string query = "SELECT  		DISTINCT   		Member.NameSurname as [MerchNameSurname], Member.Id as [MemberId],  		MemberRoute.EffectiveDate as [DateOfVisit],  		CASE(DATENAME(weekday,MemberRoute.EffectiveDate))  			WHEN 'Monday' THEN 'Pazartesi'  			WHEN 'Tuesday' THEN 'Salı'  			WHEN 'Wednesday' THEN 'Çarşamba'  			WHEN 'Thursday' THEN 'Perşembe'  			WHEN 'Friday' THEN 'Cuma'  			WHEN 'Saturday' THEN 'Cumartesi'  			WHEN 'Sunday' THEN 'Pazar'  			ELSE ''  		END as [Day],  		(SELECT COUNT(MemberRoute2.Id) FROM MemberRoute MemberRoute2 WITH (NOLOCK) WHERE MemberRoute2.EffectiveDate = MemberRoute.EffectiveDate AND MemberRoute2.MemberId = MemberRoute.MemberId) as [PlannedLocationCount],  		(SELECT COUNT(MemberRoute3.Id) FROM MemberRoute MemberRoute3 WITH (NOLOCK) WHERE MemberRoute3.EffectiveDate = MemberRoute.EffectiveDate AND MemberRoute3.MemberId = MemberRoute.MemberId AND EntryOn IS NOT NULL) as [ActualLocationCount],  		CAST(  			CAST(  				(SELECT COUNT(MemberRoute3.Id) FROM MemberRoute MemberRoute3 WITH (NOLOCK) WHERE MemberRoute3.EffectiveDate = MemberRoute.EffectiveDate AND MemberRoute3.MemberId = MemberRoute.MemberId AND EntryOn IS NOT NULL)   				as decimal(18,4)  				)   				/   			CAST(   				(SELECT COUNT(MemberRoute2.Id) FROM MemberRoute MemberRoute2 WITH (NOLOCK) WHERE MemberRoute2.EffectiveDate = MemberRoute.EffectiveDate AND MemberRoute2.MemberId = MemberRoute.MemberId)   				as decimal(18,4)  				) * 100  			as int) as [SuccessPercent], " +
                               "(SELECT TOP 1 MemberTimeOffCreator.NameSurname FROM MemberTimeOff INNER JOIN Member MemberTimeOffCreator ON MemberTimeOff.CreatedBy = MemberTimeOffCreator.Id WHERE MemberTimeOff.MemberId = Member.Id AND MemberTimeOff.StartDate <= MemberRoute.EffectiveDate AND DATEADD(second,-1,MemberTimeOff.EndDate) >= MemberRoute.EffectiveDate) as [OffRouteCreatedBy], " +
                               "(SELECT TOP 1 MemberTimeOff.Description FROM MemberTimeOff INNER JOIN Member MemberTimeOffCreator ON MemberTimeOff.CreatedBy = MemberTimeOffCreator.Id WHERE MemberTimeOff.MemberId = Member.Id AND MemberTimeOff.StartDate <= MemberRoute.EffectiveDate AND DATEADD(second,-1,MemberTimeOff.EndDate) >= MemberRoute.EffectiveDate) as [OffRouteDescription] " +
                               "FROM  		MemberRoute WITH (NOLOCK)  		INNER JOIN Member WITH (NOLOCK) ON Member.Id = MemberRoute.MemberId  		INNER JOIN MemberCustomer WITH (NOLOCK) ON Member.Id = MemberCustomer.MemberId  	WHERE  		MemberRoute.EffectiveDate BETWEEN '" + startDate.ToString("yyyy-MM-dd") + " 00:00:00' AND '" + endDate.ToString("yyyy-MM-dd") + " 23:59:59' AND  		MemberCustomer.CustomerId = " + Convert.ToString(customerId) + " AND  		Member.MemberTitleId IN (5)  	ORDER BY  		Member.NameSurname,  		MemberRoute.EffectiveDate";
                return insDatCustomer.FrekansRaporu(query, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public DataTable RouteAnalysis_NatFormatKirilimi(DateTime startDate, DateTime endDate, int customerId)
        {
            DbConnector insDbConnector = new DbConnector();
            datCustomer insDatCustomer = new datCustomer();
            try
            {
                return insDatCustomer.RouteAnalysis_NatFormatKirilimi(startDate, endDate, customerId, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public DataTable RouteAnalysis_IlKanalSayisi(DateTime startDate, DateTime endDate, int customerId)
        {
            DbConnector insDbConnector = new DbConnector();
            datCustomer insDatCustomer = new datCustomer();
            try
            {
                return insDatCustomer.RouteAnalysis_IlKanalSayisi(startDate, endDate, customerId, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public DataTable RouteAnalysis_LokalDagilimi(DateTime startDate, DateTime endDate, int customerId)
        {
            DbConnector insDbConnector = new DbConnector();
            datCustomer insDatCustomer = new datCustomer();
            try
            {
                return insDatCustomer.RouteAnalysis_LokalDagilimi(startDate, endDate, customerId, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public DataTable RouteAnalysis_NatFirmaKirilimi(DateTime startDate, DateTime endDate, int customerId)
        {
            DbConnector insDbConnector = new DbConnector();
            datCustomer insDatCustomer = new datCustomer();
            try
            {
                return insDatCustomer.RouteAnalysis_NatFirmaKirilimi(startDate, endDate, customerId, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public DataTable RouteAnalysis_MerchNokta(DateTime startDate, DateTime endDate, int customerId)
        {
            DbConnector insDbConnector = new DbConnector();
            datCustomer insDatCustomer = new datCustomer();
            try
            {
                return insDatCustomer.RouteAnalysis_MerchNokta(startDate, endDate, customerId, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public DataTable RouteAnalysis_KisiBasiAverajNokta(DateTime startDate, DateTime endDate, int customerId)
        {
            DbConnector insDbConnector = new DbConnector();
            datCustomer insDatCustomer = new datCustomer();
            try
            {
                DataTable insDt = new DataTable();
                insDt = insDatCustomer.RouteAnalysis_KisiBasiAverajNokta(startDate, endDate, customerId, insDbConnector);

                int total = 0;
                int totalEkipAdedi = 0;
                foreach (DataRow insDr in insDt.Rows)
                {
                    total += Convert.ToInt32(insDr["MagazaSayisi"]);
                    totalEkipAdedi += Convert.ToInt32(insDr["EkipSayisi"]);
                }
                foreach (DataRow insDr in insDt.Rows)
                {
                    insDr["ToplamMagazaSayisi"] = total;
                    insDr["ToplamEkipAdedi"] = totalEkipAdedi;
                    insDr["EkipOrani"] = Math.Round(Convert.ToDecimal(insDr["MagazaSayisi"]) / Convert.ToDecimal(insDr["EkipSayisi"]), 1);
                    insDr["SehirOrani"] = Math.Round(Convert.ToDecimal(insDr["EkipSayisi"]) / Convert.ToDecimal(insDr["ToplamEkipAdedi"]), 2);
                }

                return insDt;
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public DataTable RouteUyumlulukRaporuDetailsByMemberIdEffectiveDate(DateTime effectiveDate, string memberId)
        {
            DbConnector insDbConnector = new DbConnector();
            datCustomer insDatCustomer = new datCustomer();
            try
            {
                return insDatCustomer.RouteUyumlulukRaporuDetailsByMemberIdEffectiveDate(effectiveDate, memberId, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public DataTable RouteAnalysis_FrekansKirilimi(DateTime startDate, DateTime endDate, int customerId)
        {
            DbConnector insDbConnector = new DbConnector();
            datCustomer insDatCustomer = new datCustomer();
            try
            {
                DataTable insDt = new DataTable();
                insDt = insDatCustomer.RouteAnalysis_FrekansKirilimi(startDate, endDate, customerId, insDbConnector);

                decimal total = 0;
                foreach (DataRow insDr in insDt.Rows)
                {
                    total += Convert.ToDecimal(insDr["Frekans"]);
                }
                foreach (DataRow insDr in insDt.Rows)
                {
                    insDr["Total"] = total;
                    insDr["Percent"] = Math.Round(Convert.ToDecimal(insDr["Frekans"]) / Convert.ToDecimal(insDr["Total"]), 2);
                }
                insDt.AcceptChanges();
                return insDt;
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public DataTable TeshirRaporu(DateTime startDate, DateTime endDate, int customerId)
        {
            DbConnector insDbConnector = new DbConnector();
            datCustomer insDatCustomer = new datCustomer();
            try
            {
                return insDatCustomer.TeshirRaporu(startDate, endDate, customerId, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public DataTable TeshirOzelRaporu(DateTime startDate, DateTime endDate, int customerId)
        {
            DbConnector insDbConnector = new DbConnector();
            datCustomer insDatCustomer = new datCustomer();
            try
            {
                return insDatCustomer.TeshirOzelRaporu(startDate, endDate, customerId, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public DataTable TeshirUrunRaporu(DateTime startDate, DateTime endDate, int customerId)
        {
            DbConnector insDbConnector = new DbConnector();
            datCustomer insDatCustomer = new datCustomer();
            try
            {
                return insDatCustomer.TeshirUrunRaporu(startDate, endDate, customerId, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public DataTable VarYokRaporu(DateTime startDate, DateTime endDate, int customerId)
        {
            DbConnector insDbConnector = new DbConnector();
            datCustomer insDatCustomer = new datCustomer();
            try
            {
                endDate = endDate.AddHours(23).AddMinutes(59).AddSeconds(59);
                return insDatCustomer.VarYokRaporu(startDate, endDate, customerId, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public DataTable OnyuzRaporu(DateTime startDate, DateTime endDate, int customerId)
        {
            DbConnector insDbConnector = new DbConnector();
            datCustomer insDatCustomer = new datCustomer();
            try
            {
                endDate = endDate.AddHours(23).AddMinutes(59).AddSeconds(59);
                return insDatCustomer.OnyuzRaporu(startDate, endDate, customerId, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public DataTable OnyuzRaporuV2(DateTime startDate, DateTime endDate, int customerId)
        {
            DbConnector insDbConnector = new DbConnector();
            datCustomer insDatCustomer = new datCustomer();
            try
            {
                endDate = endDate.AddHours(23).AddMinutes(59).AddSeconds(59);

                DataTable insDt = new DataTable();
                insDt = insDatCustomer.OnyuzRaporuV2(startDate, endDate, customerId, insDbConnector);

                #region Add Additiona Columns
                insDt.Columns.Add("RetailShopFaceCountTotal", typeof(int));
                insDt.Columns.Add("Percentage", typeof(decimal));
                insDt.Columns.Add("BrandPercentage", typeof(decimal));
                insDt.AcceptChanges();
                #endregion

                List<OnyuzReportV2Item> target = insDt.AsEnumerable()
                .Select(row => new OnyuzReportV2Item
                {
                    // assuming column 0's type is Nullable<long>

                    NameSurname = row.Field<string>(0),
                    RetailCategoryName = row.Field<string>(1),
                    CityName = row.Field<string>(2),
                    RetailName = row.Field<string>(3),
                    RetailShopName = row.Field<string>(4),
                    CustomerBrandName = row.Field<string>(5),
                    CustomerProductName = row.Field<string>(6),
                    FaceCount = row.Field<int>(7),
                    RetailShopFaceCountTotal = 0,
                    Percentage = 0,
                    BrandPercentage = 0
                }).ToList();

                #region Loop each RetailShop and update total face count

                var distinct_RetailShop = target.Select(x => x.RetailShopName).Distinct();

                foreach (var insDr_RetailShop in distinct_RetailShop)
                {

                    var rowsToUpdate = (from item in target
                                   where item.RetailShopName == insDr_RetailShop
                                   select item).ToList();

                    int retailShopCount = 0;
                    foreach (var insDr_FaceCount in rowsToUpdate)
                    {
                        retailShopCount += Convert.ToInt32(insDr_FaceCount.FaceCount);
                    }

                    foreach (var insDr_FaceCount in rowsToUpdate)
                    {
                        insDr_FaceCount.RetailShopFaceCountTotal = retailShopCount;
                        insDr_FaceCount.Percentage = Math.Round((Convert.ToDecimal(insDr_FaceCount.FaceCount) / (retailShopCount)) * Convert.ToDecimal(100), 2, MidpointRounding.AwayFromZero);

                        #region Calculate Brand Percentage
                        decimal sum = (from item in target
                                       where item.RetailShopName == insDr_RetailShop &&
                                             item.CustomerBrandName == insDr_FaceCount.CustomerBrandName
                                       select item).ToList().Sum(x => x.FaceCount);

                        insDr_FaceCount.BrandPercentage = Math.Round((Convert.ToDecimal(sum) / Convert.ToDecimal(retailShopCount)) * Convert.ToDecimal(100), 2, MidpointRounding.AwayFromZero);
                        #endregion
                    }
                }

                insDt = ToDataTable(target);
                insDt.AcceptChanges();

                #endregion

                #region Loop each row and create RetailShopSummary
                //DataTable insDt_Result = new DataTable();
                //insDt_Result = insDt.Clone();
                //string lastShipTo = string.Empty;
                //foreach (DataRow insDr_Original in insDt.Rows)
                //{
                //    if (Convert.ToString(insDr_Original["RetailShopName"]) != lastShipTo)
                //    {
                //        #region Get Distinct Brands and Make calculations
                //        DataTable insDt_DistinctBrands = new DataTable();
                //        insDt_DistinctBrands = insDt.Select("RetailShopName = '" + Convert.ToString(insDr_Original["RetailShopName"]) + "'").CopyToDataTable();
                //        insDt_DistinctBrands = insDt_DistinctBrands.DefaultView.ToTable(true, "CustomerBrandName");
                //        foreach (DataRow insDr_Brand in insDt_DistinctBrands.Rows)
                //        {
                //            DataRow insDr_Summary = insDt_Result.NewRow();
                //            insDr_Summary["NameSurname"] = Convert.ToString(insDr_Original["NameSurname"]);
                //            insDr_Summary["RetailName"] = Convert.ToString(insDr_Original["RetailName"]);
                //            insDr_Summary["RetailShopName"] = Convert.ToString(insDr_Original["RetailShopName"]);
                //            insDr_Summary["CustomerBrandName"] = Convert.ToString(insDr_Brand[0]) + " PAYI";
                //            insDr_Summary["CustomerProductName"] = string.Empty;
                //            insDr_Summary["FaceCount"] = Convert.ToInt32(insDt.Compute("SUM(FaceCount)", "RetailShopName='" + Convert.ToString(insDr_Original["RetailShopName"]) + "' AND CustomerBrandName = '" + Convert.ToString(insDr_Brand[0]) + "'"));

                //            decimal totalShopCount = Convert.ToDecimal(insDr_Original["RetailShopFaceCountTotal"]);
                //            decimal brandPercent = Math.Round((Convert.ToDecimal(insDr_Summary["FaceCount"]) / totalShopCount) * Convert.ToDecimal(100), 2, MidpointRounding.AwayFromZero);

                //            insDr_Summary["Percentage"] = brandPercent;
                //            insDt_Result.Rows.Add(insDr_Summary);
                //        }

                //        lastShipTo = Convert.ToString(insDr_Original["RetailShopName"]);
                //        #endregion
                //    }

                //    insDt_Result.ImportRow(insDr_Original);
                //}
                //insDt_Result.AcceptChanges();
                #endregion

                return insDt;
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public DataTable SelectProductListingPerRetailShopByCustomerId(int customerId)
        {
            DbConnector insDbConnector = new DbConnector();
            datCustomer insDatCustomer = new datCustomer();
            try
            {
                return insDatCustomer.SelectProductListingPerRetailShopByCustomerId(customerId, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public DataTable AktiviteRaporu(DateTime StartDate, DateTime EndDate, int CustomerId)
        {
            DbConnector insDbConnector = new DbConnector();
            datCustomer insDatCustomer = new datCustomer();
            try
            {
                return insDatCustomer.AktiviteRaporu(StartDate, EndDate, CustomerId, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
        #endregion
    }

    public class OnyuzReportV2Item
    {
        public string NameSurname { get; set; }
        public string RetailCategoryName { get; set; }
        public string CityName { get; set; }
        public string RetailName { get; set; }
        public string RetailShopName { get; set; }
        public string CustomerBrandName { get; set; }
        public string CustomerProductName { get; set; }
        public int FaceCount { get; set; }
        public int RetailShopFaceCountTotal { get; set; }
        public decimal Percentage { get; set; }
        public decimal BrandPercentage { get; set; }
    }

    public class RafFiyatlariItem
    {
        public string MerchNameSurname { get; set; }
        public DateTime DateOfVisit { get; set; }
        public string Day { get; set; }
        public string RetailName { get; set; }
        public string RetailShopName { get; set; }
        public string CustomerProductBrand { get; set; }
        public string CustomerProduct { get; set; }
        public decimal? PackagingSize { get; set; }
        public string PackagingType { get; set; }
        public decimal? ShelfPrice { get; set; }
        public string CustomerProductBrand2 { get; set; }
        public string CustomerProduct2 { get; set; }
        public decimal? PackagingSize2 { get; set; }
        public string PackagingType2 { get; set; }
        public decimal? ActivityPrice { get; set; }
        public decimal? SuggestedActivityPrice { get; set; }
        public decimal? CompetitorShelfPrice { get; set; }
        public decimal? CompetitorShelfPriceAdjusted { get; set; }
        public decimal? Ratio { get; set; }
        public decimal? CustomerKilogramPrice { get; set; }
        public decimal? CompetitorKilogramPrice { get; set; }
        public decimal? SuggestedListPrice { get; set; }
        public decimal? RatioNew { get; set; }
        public decimal? SuggestedListPriceRatio { get; set; }
    }
}