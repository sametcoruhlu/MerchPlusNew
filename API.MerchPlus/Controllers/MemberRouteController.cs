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
    [Authorize]
    [RoutePrefix("api/MemberRouteController")]
    public class MemberRouteController : ApiController
    {

        [HttpPost]
        [AllowAnonymous]
        [Route("SelectMemberRouteByMemberIdEffectiveDate")]
        public JObject SelectMemberRouteByMemberIdEffectiveDate(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            #region Business Logic
            busMemberRoute insBusMemberRoute = new busMemberRoute();
            entMemberRoute insEntMemberRoute = new entMemberRoute();
            insEntMemberRoute.MemberId = json.MemberId;
            insEntMemberRoute.EffectiveDate = Convert.ToDateTime(json.EffectiveDate);

            DataTable insDt = new DataTable();
            insDt = insBusMemberRoute.SelectMemberRouteByMemberIdEffectiveDate(insEntMemberRoute);
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
                                        new JProperty("Content", JArray.Parse(JsonConvert.SerializeObject(insDt)))
                                        );
            return returnJson;
            #endregion
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("CompleteMemberRouteWiselyFromJsonArray")]
        public JObject CompleteMemberRouteWiselyFromJsonArray(JObject data)
        {
            JObject returnJson;
            dynamic json = data;

            var memberRoute = json.MemberRoute;
            var memberRouteDetail = json.MemberRouteDetail;
            var memberRoutePhoto = json.MemberRoutePhoto;
            var productExpiration = json.ProductExpiration;

            busMemberRoute insBusMemberRoute = new busMemberRoute();
            insBusMemberRoute.CompleteMemberRouteWiselyFromJsonArray(memberRoute, memberRouteDetail, memberRoutePhoto, productExpiration);

            if (insBusMemberRoute.HasErrors)
            {
                returnJson = new JObject(
                                        new JProperty("Result", "NOK"),
                                        new JProperty("Content", insBusMemberRoute.ErrorMessage)
                                        );
            }
            else
            {
                returnJson = new JObject(
                                        new JProperty("Result", "OK")
                                        );
            }

            return returnJson;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("CompleteMemberRouteWiselyFromJsonArrayWithSalesOrder")]
        public JObject CompleteMemberRouteWiselyFromJsonArrayWithSalesOrder(JObject data)
        {
            JObject returnJson;
            dynamic json = data;

            var memberRoute = json.MemberRoute;
            var memberRouteDetail = json.MemberRouteDetail;
            var memberRoutePhoto = json.MemberRoutePhoto;
            var salesOrder = json.SalesOrder;
            var salesOrderDetail = json.SalesOrderDetail;
            var productExpiration = json.ProductExpiration;

            busMemberRoute insBusMemberRoute = new busMemberRoute();
            insBusMemberRoute.CompleteMemberRouteWiselyFromJsonArrayWithSalesOrder(memberRoute, memberRouteDetail, memberRoutePhoto, salesOrder, salesOrderDetail, productExpiration);

            if (insBusMemberRoute.HasErrors)
            {
                returnJson = new JObject(
                                        new JProperty("Result", "NOK"),
                                        new JProperty("Content", insBusMemberRoute.ErrorMessage)
                                        );
            }
            else
            {
                returnJson = new JObject(
                                        new JProperty("Result", "OK")
                                        );
            }

            return returnJson;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("SelectMemberRouteByMemberIdSchedulerData")]
        public JObject SelectMemberRouteByMemberIdSchedulerData(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            #region Business Logic
            busMemberRoute insBusMemberRoute = new busMemberRoute();
            entMemberRoute insEntMemberRoute = new entMemberRoute();
            insEntMemberRoute.MemberId = Convert.ToString(json.MemberId);

            DataTable insDt = new DataTable();
            insDt = insBusMemberRoute.SelectMemberRouteByMemberIdSchedulerView(insEntMemberRoute);

            #region Decide if record is selectable
            insDt.Columns.Add("selectable", typeof(bool));
            insDt.Columns.Add("editable", typeof(bool));
            insDt.AcceptChanges();
            foreach (DataRow insDr in insDt.Rows)
            {
                if (Convert.ToDateTime(insDr["start"]) <= DateTime.Now)
                    insDr["selectable"] = false;
                else
                    insDr["selectable"] = true;
                if (Convert.ToDateTime(insDr["start"]) <= DateTime.Now)
                    insDr["editable"] = false;
                else
                    insDr["editable"] = true;
            }
            #endregion

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
                                        new JProperty("Content", JArray.Parse(JsonConvert.SerializeObject(insDt)))
                                        );
            return returnJson;
            #endregion
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("InsertMemberRouteWiselyFromCalendar")]
        public JObject InsertMemberRouteWiselyFromCalendar(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            DateTime startDate = new DateTime();
            startDate = Convert.ToDateTime(json.EffectiveDate);
            int orderIndex = Convert.ToInt32(json.OrderIndex);
            string memberId = Convert.ToString(json.MemberId);
            int retailShopId = Convert.ToInt32(json.RetailShopId);
            int frequecyId = Convert.ToInt32(json.FrequencyId);
            int customerId = Convert.ToInt32(json.CustomerId);
            
            busMemberRoute insBusMemberRoute = new busMemberRoute();
            insBusMemberRoute.InsertMemberRouteWiselyFromCalendar(memberId, orderIndex, startDate, retailShopId, frequecyId, customerId);



            #region Business Logic
            if (insBusMemberRoute.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusMemberRoute.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK")
                                        );
            return returnJson;
            #endregion
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("DeleteMemberRouteWiselyFromCalendar")]
        public JObject DeleteMemberRouteWiselyFromCalendar(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            busMemberRoute insBusMemberRoute = new busMemberRoute();
            entMemberRoute insEntMemberRoute = new entMemberRoute();

            int id = Convert.ToInt32(json.MemberRouteId);
            insEntMemberRoute.Id = id;

            if (Convert.ToInt32(json.DeleteAll) == 0)
            {
                insBusMemberRoute.DeleteMemberRouteById(insEntMemberRoute);
            }
            else
            {
                insBusMemberRoute.DeleteMemberRouteByIdAllVisits(insEntMemberRoute);
            }

            #region Business Logic
            if (insBusMemberRoute.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusMemberRoute.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK")
                                        );
            return returnJson;
            #endregion
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("DragAndDropHelper")]
        public JObject DragAndDropHelper(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            #region Business Logic
            busMemberRoute insBusMemberRoute = new busMemberRoute();
            entMemberRoute insEntMemberRoute = new entMemberRoute();

            insEntMemberRoute.Id = Convert.ToInt32(json.Id);
            insBusMemberRoute.SelectMemberRouteById(insEntMemberRoute);
            DateTime insNewDate = Convert.ToDateTime(json.EffectiveDate);

            insBusMemberRoute.DragAndDropHelper(insEntMemberRoute, insNewDate);

            if (insBusMemberRoute.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusMemberRoute.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK")
                                        );
            return returnJson;
            #endregion
        }
    }
}
