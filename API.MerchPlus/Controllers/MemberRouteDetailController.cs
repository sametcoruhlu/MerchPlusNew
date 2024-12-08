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
    [RoutePrefix("api/MemberRouteDetailController")]
    public class MemberRouteDetailController : ApiController
    {

        [HttpPost]
        [AllowAnonymous]
        [Route("SelectMemberRouteDetailByMemberIdEffectiveDate")]
        public JObject SelectMemberRouteDetailByMemberIdEffectiveDate(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            #region Business Logic
            busMemberRoute insBusMemberRoute = new busMemberRoute();
            entMemberRoute insEntMemberRoute = new entMemberRoute();
            insEntMemberRoute.MemberId = json.MemberId;
            insEntMemberRoute.EffectiveDate = Convert.ToDateTime(json.EffectiveDate);

            DataTable insDt = new DataTable();
            insDt = insBusMemberRoute.SelectMemberRouteDetailByMemberIdEffectiveDate(insEntMemberRoute);
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
        [Route("SelectPromotionHistoryByMemberRouteIdPromotionTypeId")]
        public JObject SelectPromotionHistoryByMemberRouteIdPromotionTypeId(JObject data)
        {

            JObject returnJson;
            dynamic json = data;


            busMemberRouteDetail insBusMemberRouteDetail = new busMemberRouteDetail();
            entMemberRouteDetail insEntMemberRouteDetail = new entMemberRouteDetail();
            insEntMemberRouteDetail.MemberRouteId = Convert.ToInt32(json.MemberRouteId);
            insEntMemberRouteDetail.PromotionTypeId = Convert.ToInt32(json.PromotionTypeId);
            DataTable insDt_MeberRouteDetail = new DataTable();
            insDt_MeberRouteDetail = insBusMemberRouteDetail.SelectMemberRouteDetailByMemberRouteIdPromotionTypeId(insEntMemberRouteDetail);

            insDt_MeberRouteDetail.Columns.Add("History", typeof(DataTable));
            insDt_MeberRouteDetail.Columns.Add("CustomerProduct", typeof(entCustomerProduct));

            #region Get MemberRoute
            entMemberRoute insEntMemberRoute = new entMemberRoute();
            busMemberRoute insBusMemberRoute = new busMemberRoute();
            insEntMemberRoute.Id = Convert.ToInt32(insEntMemberRouteDetail.MemberRouteId);
            insBusMemberRoute.SelectMemberRouteById(insEntMemberRoute);
            #endregion

            foreach (DataRow insDr in insDt_MeberRouteDetail.Rows)
            {
                #region Get Customer Product
                entCustomerProduct insEntCustomerProduct = new entCustomerProduct();
                busCustomerProduct insBusCustomerProduct = new busCustomerProduct();
                insEntCustomerProduct.Id = Convert.ToInt32(insDr["CustomerProductId"]);
                insBusCustomerProduct.SelectCustomerProductById(insEntCustomerProduct);
                insDr["CustomerProduct"] = insEntCustomerProduct;
                #endregion

                #region Get History for this Product in this location for the past 8 weeks
                DataTable insDt_History = new DataTable();
                insDt_History = insBusMemberRouteDetail.SelectMemberRouteDetailByMemberRouteIdStartCustomerProductIdRetailShopIdNumberOfRecords(insEntMemberRoute.Id, Convert.ToInt32(insDr["CustomerProductId"]), Convert.ToInt32(insEntMemberRoute.RetailShopId), 8);
                insDr["History"] = insDt_History;
                #endregion
            }

            if (insBusMemberRouteDetail.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusMemberRouteDetail.ErrorMessage)
                                            );
                return returnJson;
            }
            insDt_MeberRouteDetail.AcceptChanges();
            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", JArray.Parse(JsonConvert.SerializeObject(insDt_MeberRouteDetail)))
                                        );
            return returnJson;
            
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("UpdateFaceCountById")]
        public JObject UpdateFaceCountById(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            #region Business Logic
            entMemberRouteDetail insEnt = new entMemberRouteDetail();
            busMemberRouteDetail insBus = new busMemberRouteDetail();
            insEnt.Id = Convert.ToInt32(json.Id);
            insBus.SelectMemberRouteDetailById(insEnt);

            insEnt.FaceCount = Convert.ToInt32(json.FaceCount);
            insBus.UpdateMemberRouteDetailById(insEnt);

            if (insBus.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBus.ErrorMessage)
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
        [Route("UpdateShelfStockById")]
        public JObject UpdateShelfStockById(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            #region Business Logic
            entMemberRouteDetail insEnt = new entMemberRouteDetail();
            busMemberRouteDetail insBus = new busMemberRouteDetail();
            insEnt.Id = Convert.ToInt32(json.Id);
            insBus.SelectMemberRouteDetailById(insEnt);

            insEnt.ShelfStock = Convert.ToInt32(json.ShelfStock);
            insBus.UpdateMemberRouteDetailById(insEnt);

            if (insBus.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBus.ErrorMessage)
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
        [Route("UpdateShelfPriceById")]
        public JObject UpdateShelfPriceById(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            #region Business Logic
            entMemberRouteDetail insEnt = new entMemberRouteDetail();
            busMemberRouteDetail insBus = new busMemberRouteDetail();
            insEnt.Id = Convert.ToInt32(json.Id);
            insBus.SelectMemberRouteDetailById(insEnt);

            insEnt.ShelfPrice = Convert.ToDecimal(json.ShelfPrice);
            insBus.UpdateMemberRouteDetailById(insEnt);

            if (insBus.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBus.ErrorMessage)
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
        [Route("UpdateShelfPriceByIds")]
        public JObject UpdateShelfPriceByIds(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            #region Business Logic
            entMemberRouteDetail insEnt = new entMemberRouteDetail();
            busMemberRouteDetail insBus = new busMemberRouteDetail();
            List<int> ids = JsonConvert.DeserializeObject<List<int>>(JsonConvert.SerializeObject(json.Ids));
            Decimal price = Convert.ToDecimal(json.ShelfPrice);

            busMemberRouteDetail insBusMemberRouteDetail = new busMemberRouteDetail();
            foreach (int idToUpdate in ids)
            {
                entMemberRouteDetail insEntDetail = new entMemberRouteDetail();
                insEntDetail.Id = idToUpdate;
                insBusMemberRouteDetail.SelectMemberRouteDetailById(insEntDetail);
                insEntDetail.ShelfPrice = price;
                insBusMemberRouteDetail.UpdateMemberRouteDetailById(insEntDetail);
            }

            if (insBus.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBus.ErrorMessage)
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
