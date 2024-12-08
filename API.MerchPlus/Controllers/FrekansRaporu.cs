using busMerchPlus;
using entMerchPlus;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using API.MerchPlus.Models;
using API.MerchPlus.Providers;
using API.MerchPlus.Results;
using System.Data;
using System.Configuration;
using System.IO;

namespace API.MerchPlus.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/ReportingController")]
    public class ReportingController : ApiController
    {


        [HttpPost]
        [Route("FrekansRaporu")]
        public JObject FrekansRaporu(JObject data)
        {
            JObject returnJson;
            dynamic json = data;

            busCustomer insBusCustomer = new busCustomer();
            DataTable insDt = new DataTable();
            insDt = insBusCustomer.FrekansRaporu(Convert.ToDateTime(json.StartDate), Convert.ToDateTime(json.EndDate), Convert.ToInt32(json.CustomerId));
            if (insBusCustomer.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusCustomer.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", JArray.Parse(JsonConvert.SerializeObject(insDt)))
                                        );
            return returnJson;
            
        }

        [HttpPost]
        [Route("RafFiyatlariRaporu")]
        public JObject RafFiyatlariRaporu(JObject data)
        {
            JObject returnJson;
            dynamic json = data;

            busCustomer insBusCustomer = new busCustomer();
            DataTable insDt = new DataTable();
            insDt = insBusCustomer.RafFiyatlariRaporu(Convert.ToDateTime(json.StartDate), Convert.ToDateTime(json.EndDate), Convert.ToInt32(json.CustomerId));

            if (insBusCustomer.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusCustomer.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", JArray.Parse(JsonConvert.SerializeObject(insDt)))
                                        );

            GC.Collect();
            GC.Collect();
            GC.Collect();

            return returnJson;

        }

        [HttpPost]
        [Route("RouteUyumlulukRaporu")]
        public JObject RouteUyumlulukRaporu(JObject data)
        {
            JObject returnJson;
            dynamic json = data;

            busCustomer insBusCustomer = new busCustomer();
            DataTable insDt = new DataTable();
            insDt = insBusCustomer.RouteUyumlulukRaporu(Convert.ToDateTime(json.StartDate), Convert.ToDateTime(json.EndDate), Convert.ToInt32(json.CustomerId));
            if (insBusCustomer.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusCustomer.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", JArray.Parse(JsonConvert.SerializeObject(insDt)))
                                        );
            return returnJson;

        }

        [HttpPost]
        [Route("RouteUyumlulukRaporuDetailsByMemberIdEffectiveDate")]
        public JObject RouteUyumlulukRaporuDetailsByMemberIdEffectiveDate(JObject data)
        {
            JObject returnJson;
            dynamic json = data;

            busCustomer insBusCustomer = new busCustomer();
            DataTable insDt = new DataTable();
            insDt = insBusCustomer.RouteUyumlulukRaporuDetailsByMemberIdEffectiveDate(Convert.ToDateTime(json.EffectiveDate), Convert.ToString(json.MemberId));
            if (insBusCustomer.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusCustomer.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", JArray.Parse(JsonConvert.SerializeObject(insDt)))
                                        );
            return returnJson;

        }

        [HttpPost]
        [Route("TeshirRaporu")]
        public JObject TeshirRaporu(JObject data)
        {
            JObject returnJson;
            dynamic json = data;

            busCustomer insBusCustomer = new busCustomer();
            DataTable insDt = new DataTable();
            insDt = insBusCustomer.TeshirRaporu(Convert.ToDateTime(json.StartDate), Convert.ToDateTime(json.EndDate), Convert.ToInt32(json.CustomerId));
            if (insBusCustomer.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusCustomer.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", JArray.Parse(JsonConvert.SerializeObject(insDt)))
                                        );
            return returnJson;

        }

        [HttpPost]
        [Route("TeshirOzelRaporu")]
        public JObject TeshirOzelRaporu(JObject data)
        {
            JObject returnJson;
            dynamic json = data;

            busCustomer insBusCustomer = new busCustomer();
            DataTable insDt = new DataTable();
            insDt = insBusCustomer.TeshirOzelRaporu(Convert.ToDateTime(json.StartDate), Convert.ToDateTime(json.EndDate), Convert.ToInt32(json.CustomerId));
            if (insBusCustomer.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusCustomer.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", JArray.Parse(JsonConvert.SerializeObject(insDt)))
                                        );
            return returnJson;

        }

        [HttpPost]
        [Route("TeshirUrunRaporu")]
        public JObject TeshirUrunRaporu(JObject data)
        {
            JObject returnJson;
            dynamic json = data;

            busCustomer insBusCustomer = new busCustomer();
            DataTable insDt = new DataTable();
            insDt = insBusCustomer.TeshirUrunRaporu(Convert.ToDateTime(json.StartDate), Convert.ToDateTime(json.EndDate), Convert.ToInt32(json.CustomerId));
            if (insBusCustomer.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusCustomer.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", JArray.Parse(JsonConvert.SerializeObject(insDt)))
                                        );
            return returnJson;

        }

        [HttpPost]
        [Route("SelectMemberRoutePhotoByMemberRouteId")]
        public JObject SelectMemberRoutePhotoByMemberRouteId(JObject data)
        {
            JObject returnJson;
            dynamic json = data;

            busMemberRoutePhoto insBusMemberRoutePhoto = new busMemberRoutePhoto();
            DataTable insDt = new DataTable();
            insDt = insBusMemberRoutePhoto.SelectMemberRoutePhotoByMemberRouteId(Convert.ToInt32(json.MemberRouteId));
            if (insBusMemberRoutePhoto.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusMemberRoutePhoto.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", JArray.Parse(JsonConvert.SerializeObject(insDt)))
                                        );
            return returnJson;

        }

        [HttpPost]
        [Route("VarYokRaporu")]
        public JObject VarYokRaporu(JObject data)
        {
            JObject returnJson;
            dynamic json = data;

            busCustomer insBusCustomer = new busCustomer();
            DataTable insDt = new DataTable();
            insDt = insBusCustomer.VarYokRaporu(Convert.ToDateTime(json.StartDate), Convert.ToDateTime(json.EndDate), Convert.ToInt32(json.CustomerId));
            if (insBusCustomer.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusCustomer.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", JArray.Parse(JsonConvert.SerializeObject(insDt)))
                                        );
            return returnJson;

        }

        [HttpPost]
        [Route("SelectProductListingPerRetailShopByCustomerId")]
        public JObject SelectProductListingPerRetailShopByCustomerId(JObject data)
        {
            JObject returnJson;
            dynamic json = data;

            busCustomer insBusCustomer = new busCustomer();
            DataTable insDt = new DataTable();
            insDt = insBusCustomer.SelectProductListingPerRetailShopByCustomerId(Convert.ToInt32(json.CustomerId));
            if (insBusCustomer.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusCustomer.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", JArray.Parse(JsonConvert.SerializeObject(insDt)))
                                        );
            return returnJson;

        }

        [HttpPost]
        [Route("OnyuzRaporu")]
        public JObject OnyuzRaporu(JObject data)
        {
            JObject returnJson;
            dynamic json = data;

            busCustomer insBusCustomer = new busCustomer();
            DataTable insDt = new DataTable();
            insDt = insBusCustomer.OnyuzRaporu(Convert.ToDateTime(json.StartDate), Convert.ToDateTime(json.EndDate), Convert.ToInt32(json.CustomerId));
            if (insBusCustomer.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusCustomer.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", JArray.Parse(JsonConvert.SerializeObject(insDt)))
                                        );
            return returnJson;

        }

        [HttpPost]
        [Route("SelectSalesOrderByStartDateEndDateCustomerIdGridData")]
        public JObject SelectSalesOrderByStartDateEndDateCustomerIdGridData(JObject data)
        {
            JObject returnJson;
            dynamic json = data;

            busSalesOrder insBusSalesOrder = new busSalesOrder();
            DataTable insDt = new DataTable();
            insDt = insBusSalesOrder.SelectSalesOrderByStartDateEndDateCustomerIdGridData(Convert.ToDateTime(json.StartDate), Convert.ToDateTime(json.EndDate), Convert.ToInt32(json.CustomerId));
            if (insBusSalesOrder.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusSalesOrder.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", JArray.Parse(JsonConvert.SerializeObject(insDt)))
                                        );
            return returnJson;

        }

        [HttpPost]
        [Route("SKTRaporu")]
        public JObject SKTRaporu(JObject data)
        {
            JObject returnJson;
            dynamic json = data;

            busCustomer insBusCustomer = new busCustomer();
            DataTable insDt = new DataTable();
            insDt = insBusCustomer.SKTRaporu(Convert.ToDateTime(json.StartDate), Convert.ToDateTime(json.EndDate), Convert.ToInt32(json.CustomerId));
            if (insBusCustomer.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusCustomer.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", JArray.Parse(JsonConvert.SerializeObject(insDt)))
                                        );
            return returnJson;

        }

        [HttpPost]
        [Route("AktiviteRaporu")]
        public JObject AktiviteRaporu(JObject data)
        {
            JObject returnJson;
            dynamic json = data;

            busCustomer insBusCustomer = new busCustomer();
            DataTable insDt = new DataTable();
            insDt = insBusCustomer.AktiviteRaporu(Convert.ToDateTime(json.StartDate), Convert.ToDateTime(json.EndDate), Convert.ToInt32(json.CustomerId));
            if (insBusCustomer.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusCustomer.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", JArray.Parse(JsonConvert.SerializeObject(insDt)))
                                        );
            return returnJson;

        }

        [HttpPost]
        [Route("SelectMemberRouteDetailByCustomerIdStartDateEndDateGridDataForEdit")]
        public JObject SelectMemberRouteDetailByCustomerIdStartDateEndDateGridDataForEdit(JObject data)
        {
            JObject returnJson;
            dynamic json = data;

            busMemberRouteDetail insBus = new busMemberRouteDetail();
            DataTable insDt = new DataTable();
            insDt = insBus.SelectMemberRouteDetailByCustomerIdStartDateEndDateGridDataForEdit(Convert.ToDateTime(json.StartDate), Convert.ToDateTime(json.EndDate), Convert.ToInt32(json.CustomerId));
            if (insBus.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBus.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", JArray.Parse(JsonConvert.SerializeObject(insDt)))
                                        );
            return returnJson;

        }

        [HttpPost]
        [Route("OnyuzRaporuV2")]
        public JObject OnyuzRaporuV2(JObject data)
        {
            JObject returnJson;
            dynamic json = data;

            busCustomer insBusCustomer = new busCustomer();
            DataTable insDt = new DataTable();
            insDt = insBusCustomer.OnyuzRaporuV2(Convert.ToDateTime(json.StartDate), Convert.ToDateTime(json.EndDate), Convert.ToInt32(json.CustomerId));
            if (insBusCustomer.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusCustomer.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", JArray.Parse(JsonConvert.SerializeObject(insDt)))
                                        );
            return returnJson;

        }

        [HttpPost]
        [Route("SelectRouteAnalysisVisitDataByCustomerIdStartDateEndDate")]
        public JObject SelectRouteAnalysisVisitDataByCustomerIdStartDateEndDate(JObject data)
        {
            JObject returnJson;
            dynamic json = data;

            busCustomer insBusCustomer = new busCustomer();
            DataTable insDt = new DataTable();

            DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            while (startDate.DayOfWeek != DayOfWeek.Monday)
            {
                startDate = startDate.AddDays(-1);
            }

            DateTime endDate = startDate.AddDays(14).AddSeconds(-1);

            insDt = insBusCustomer.SelectRouteAnalysisVisitDataByCustomerIdStartDateEndDate(startDate, endDate, Convert.ToInt32(json.CustomerId));
            if (insBusCustomer.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusCustomer.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", JArray.Parse(JsonConvert.SerializeObject(insDt)))
                                        );
            return returnJson;

        }

        [HttpPost]
        [Route("RouteAnalysis_KanalDagilimi")]
        public JObject RouteAnalysis_KanalDagilimi(JObject data)
        {
            JObject returnJson;
            dynamic json = data;

            busCustomer insBusCustomer = new busCustomer();
            DataTable insDt = new DataTable();

            DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            while (startDate.DayOfWeek != DayOfWeek.Monday)
            {
                startDate = startDate.AddDays(-1);
            }

            DateTime endDate = startDate.AddDays(14).AddSeconds(-1);
            insDt = insBusCustomer.RouteAnalysis_KanalDagilimi(startDate, endDate, Convert.ToInt32(json.CustomerId));

            if (insBusCustomer.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusCustomer.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", JArray.Parse(JsonConvert.SerializeObject(insDt)))
                                        );
            return returnJson;

        }

        [HttpPost]
        [Route("RouteAnalysis_IlMagazaSayisi")]
        public JObject RouteAnalysis_IlMagazaSayisi(JObject data)
        {
            JObject returnJson;
            dynamic json = data;

            busCustomer insBusCustomer = new busCustomer();
            DataTable insDt = new DataTable();

            DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            while (startDate.DayOfWeek != DayOfWeek.Monday)
            {
                startDate = startDate.AddDays(-1);
            }

            DateTime endDate = startDate.AddDays(14).AddSeconds(-1);
            insDt = insBusCustomer.RouteAnalysis_IlMagazaSayisi(startDate, endDate, Convert.ToInt32(json.CustomerId));

            if (insBusCustomer.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusCustomer.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", JArray.Parse(JsonConvert.SerializeObject(insDt)))
                                        );
            return returnJson;

        }

        [HttpPost]
        [Route("RouteAnalysis_NatFirmaKirilimi")]
        public JObject RouteAnalysis_NatFirmaKirilimi(JObject data)
        {
            JObject returnJson;
            dynamic json = data;

            busCustomer insBusCustomer = new busCustomer();
            DataTable insDt = new DataTable();

            DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            while (startDate.DayOfWeek != DayOfWeek.Monday)
            {
                startDate = startDate.AddDays(-1);
            }

            DateTime endDate = startDate.AddDays(14).AddSeconds(-1);
            insDt = insBusCustomer.RouteAnalysis_NatFirmaKirilimi(startDate, endDate, Convert.ToInt32(json.CustomerId));

            if (insBusCustomer.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusCustomer.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", JArray.Parse(JsonConvert.SerializeObject(insDt)))
                                        );
            return returnJson;

        }

        [HttpPost]
        [Route("RouteAnalysis_IlKanalSayisi")]
        public JObject RouteAnalysis_IlKanalSayisi(JObject data)
        {
            JObject returnJson;
            dynamic json = data;

            busCustomer insBusCustomer = new busCustomer();
            DataTable insDt = new DataTable();

            DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            while (startDate.DayOfWeek != DayOfWeek.Monday)
            {
                startDate = startDate.AddDays(-1);
            }

            DateTime endDate = startDate.AddDays(14).AddSeconds(-1);
            insDt = insBusCustomer.RouteAnalysis_IlKanalSayisi(startDate, endDate, Convert.ToInt32(json.CustomerId));

            if (insBusCustomer.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusCustomer.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", JArray.Parse(JsonConvert.SerializeObject(insDt)))
                                        );
            return returnJson;

        }

        [HttpPost]
        [Route("RouteAnalysis_NatFormatKirilimi")]
        public JObject RouteAnalysis_NatFormatKirilimi(JObject data)
        {
            JObject returnJson;
            dynamic json = data;

            busCustomer insBusCustomer = new busCustomer();
            DataTable insDt = new DataTable();

            DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            while (startDate.DayOfWeek != DayOfWeek.Monday)
            {
                startDate = startDate.AddDays(-1);
            }

            DateTime endDate = startDate.AddDays(14).AddSeconds(-1);
            insDt = insBusCustomer.RouteAnalysis_NatFormatKirilimi(startDate, endDate, Convert.ToInt32(json.CustomerId));

            if (insBusCustomer.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusCustomer.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", JArray.Parse(JsonConvert.SerializeObject(insDt)))
                                        );
            return returnJson;

        }

        [HttpPost]
        [Route("RouteAnalysis_LokalDagilimi")]
        public JObject RouteAnalysis_LokalDagilimi(JObject data)
        {
            JObject returnJson;
            dynamic json = data;

            busCustomer insBusCustomer = new busCustomer();
            DataTable insDt = new DataTable();

            DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            while (startDate.DayOfWeek != DayOfWeek.Monday)
            {
                startDate = startDate.AddDays(-1);
            }

            DateTime endDate = startDate.AddDays(14).AddSeconds(-1);
            insDt = insBusCustomer.RouteAnalysis_LokalDagilimi(startDate, endDate, Convert.ToInt32(json.CustomerId));

            if (insBusCustomer.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusCustomer.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", JArray.Parse(JsonConvert.SerializeObject(insDt)))
                                        );
            return returnJson;

        }

        [HttpPost]
        [Route("RouteAnalysis_MerchNokta")]
        public JObject RouteAnalysis_MerchNokta(JObject data)
        {
            JObject returnJson;
            dynamic json = data;

            busCustomer insBusCustomer = new busCustomer();
            DataTable insDt = new DataTable();

            DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            while (startDate.DayOfWeek != DayOfWeek.Monday)
            {
                startDate = startDate.AddDays(-1);
            }

            DateTime endDate = startDate.AddDays(14).AddSeconds(-1);
            insDt = insBusCustomer.RouteAnalysis_MerchNokta(startDate, endDate, Convert.ToInt32(json.CustomerId));

            if (insBusCustomer.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusCustomer.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", JArray.Parse(JsonConvert.SerializeObject(insDt)))
                                        );
            return returnJson;

        }

        [HttpPost]
        [Route("RouteAnalysis_KisiBasiAverajNokta")]
        public JObject RouteAnalysis_KisiBasiAverajNokta(JObject data)
        {
            JObject returnJson;
            dynamic json = data;

            busCustomer insBusCustomer = new busCustomer();
            DataTable insDt = new DataTable();

            DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            while (startDate.DayOfWeek != DayOfWeek.Monday)
            {
                startDate = startDate.AddDays(-1);
            }

            DateTime endDate = startDate.AddDays(14).AddSeconds(-1);
            insDt = insBusCustomer.RouteAnalysis_KisiBasiAverajNokta(startDate, endDate, Convert.ToInt32(json.CustomerId));

            if (insBusCustomer.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusCustomer.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", JArray.Parse(JsonConvert.SerializeObject(insDt)))
                                        );
            return returnJson;

        }

        [HttpPost]
        [Route("RouteAnalysis_FrekansKirilimi")]
        public JObject RouteAnalysis_FrekansKirilimi(JObject data)
        {
            JObject returnJson;
            dynamic json = data;

            busCustomer insBusCustomer = new busCustomer();
            DataTable insDt = new DataTable();

            DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            while (startDate.DayOfWeek != DayOfWeek.Monday)
            {
                startDate = startDate.AddDays(-1);
            }

            DateTime endDate = startDate.AddDays(14).AddSeconds(-1);
            insDt = insBusCustomer.RouteAnalysis_FrekansKirilimi(startDate, endDate, Convert.ToInt32(json.CustomerId));

            if (insBusCustomer.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusCustomer.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", JArray.Parse(JsonConvert.SerializeObject(insDt)))
                                        );
            return returnJson;

        }
    }
}
