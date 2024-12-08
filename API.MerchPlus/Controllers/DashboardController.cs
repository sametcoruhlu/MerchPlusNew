using busMerchPlus;
using entMerchPlus;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.MerchPlus.Controllers
{
    [Authorize]
    [RoutePrefix("api/DashboardController")]
    public class DashboardController : ApiController
    {

        [HttpPost]
        [Route("SelectDashboardWidgetValuesByCustomerIdCurrentDate")]
        public JObject SelectDashboardWidgetValuesByCustomerIdCurrentDate(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            DateTime insDateTime = Convert.ToDateTime(json.CurrentDate);
            int CustomerId = Convert.ToInt32(json.CustomerId);

            busMemberRoute insBusMemberRoute = new busMemberRoute();
            DataTable insDt_Widgets = insBusMemberRoute.SelectDashboardWidgetValuesByCustomerIdCurrentDate(CustomerId, insDateTime);
            
            if (insBusMemberRoute.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusMemberRoute.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", JArray.Parse(JsonConvert.SerializeObject(insDt_Widgets)))
                                        );
            return returnJson;
            
        }

        [HttpPost]
        [Route("GetMapDataByCustomerIdCurrentDate")]
        public JObject GetMapDataByCustomerIdCurrentDate(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            DateTime insDateTime = Convert.ToDateTime(json.CurrentDate);
            int CustomerId = Convert.ToInt32(json.CustomerId);

            busMemberRoute insBusMemberRoute = new busMemberRoute();
            DataTable insDt_Widgets = insBusMemberRoute.GetMapDataByCustomerIdCurrentDate(CustomerId, insDateTime);

            if (insBusMemberRoute.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusMemberRoute.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", JArray.Parse(JsonConvert.SerializeObject(insDt_Widgets)))
                                        );
            return returnJson;

        }

        [HttpPost]
        [Route("SelectDistinctMemberIdFromMemberRouteByEffectiveDateCustomerIdCityName")]
        public JObject SelectDistinctMemberIdFromMemberRouteByEffectiveDateCustomerIdCityName(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            DateTime insDateTime = Convert.ToDateTime(json.CurrentDate);
            int CustomerId = Convert.ToInt32(json.CustomerId);
            string cityName = Convert.ToString(json.CityName);

            busMemberRoute insBusMemberRoute = new busMemberRoute();
            entMemberRoute insEntMemberRoute = new entMemberRoute();
            insEntMemberRoute.CustomerId = CustomerId;
            insEntMemberRoute.EffectiveDate = insDateTime;
            DataTable insDt_Members = new DataTable();
            insDt_Members = insBusMemberRoute.SelectDistinctMemberIdFromMemberRouteByEffectiveDateCustomerIdCityName(insEntMemberRoute, cityName);

            DataTable insDt_RouteDetails = new DataTable();
            foreach (DataRow insDr in insDt_Members.Rows)
            {
                DataTable insDt_Route = new DataTable();
                insDt_Route = insBusMemberRoute.SelectMemberRouteReportByMemberIdEffectiveDate(Convert.ToString(insDr["Id"]), Convert.ToString(CustomerId), insDateTime);
                insDt_RouteDetails.Merge(insDt_Route);
            }

            if (insBusMemberRoute.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusMemberRoute.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Members", JArray.Parse(JsonConvert.SerializeObject(insDt_Members))),
                                        new JProperty("RouteDetails", JArray.Parse(JsonConvert.SerializeObject(insDt_RouteDetails)))
                                        );
            return returnJson;

        }

        [HttpPost]
        [Route("SelectPlannedVersusActualGraphDataByCustomerIdEndDate")]
        public JObject SelectPlannedVersusActualGraphDataByCustomerIdEndDate(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            DateTime insDateTime = Convert.ToDateTime(json.EndDate);
            int CustomerId = Convert.ToInt32(json.CustomerId);

            busMemberRoute insBusMemberRoute = new busMemberRoute();
            DataTable insDt_Data = new DataTable();
            insDt_Data = insBusMemberRoute.SelectPlannedVersusActualGraphDataByCustomerIdEndDate(CustomerId, insDateTime);

            if (insBusMemberRoute.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusMemberRoute.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("GraphData", JArray.Parse(JsonConvert.SerializeObject(insDt_Data)))
                                        );
            return returnJson;

        }

        [HttpPost]
        [Route("SelectSalesOrderGraphDataByCustomerIdEndDate")]
        public JObject SelectSalesOrderGraphDataByCustomerIdEndDate(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            DateTime insDateTime = Convert.ToDateTime(json.EndDate);
            int CustomerId = Convert.ToInt32(json.CustomerId);

            busMemberRoute insBusMemberRoute = new busMemberRoute();
            DataTable insDt_Data = new DataTable();
            insDt_Data = insBusMemberRoute.SelectSalesOrderGraphDataByCustomerIdEndDate(CustomerId, insDateTime);

            if (insBusMemberRoute.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusMemberRoute.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("GraphData", JArray.Parse(JsonConvert.SerializeObject(insDt_Data)))
                                        );
            return returnJson;

        }

    }
}
