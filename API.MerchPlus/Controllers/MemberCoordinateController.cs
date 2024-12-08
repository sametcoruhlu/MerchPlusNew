using busMerchPlus;
using entMerchPlus;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Web.Http;

namespace API.MerchPlus.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/MemberCoordinateController")]
    public class MemberCoordinateController : ApiController
    {
        [HttpPost]
        [Route("InsertMemberCoordinateFromAndroidArray")]
        public JObject InsertMemberCoordinateFromAndroidArray(JObject[] data)
        {
            JObject returnJson;
            dynamic json = data;

            busMemberCoordinate insBusMemberCoordinate = new busMemberCoordinate();
            foreach (JObject incoming in data)
            {
                entMemberCoordinate insEntMemberCoordinate = new entMemberCoordinate();
                insEntMemberCoordinate = incoming.ToObject<entMemberCoordinate>();
                insBusMemberCoordinate.InsertMemberCoordinate(insEntMemberCoordinate);
            }

            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", "All inserted")
                                    );
            return returnJson;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("SelectMemberCoordinateByMemberIdToday")]
        public JObject SelectMemberCoordinateByMemberIdToday(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            #region Business Logic
            busMemberCoordinate insBusMemberCoordinate = new busMemberCoordinate();
            entMemberCoordinate insEntMemberCoordinate = new entMemberCoordinate();
            insEntMemberCoordinate.MemberId = Convert.ToString(json.MemberId);

            DataTable insDt = new DataTable();
            insDt = insBusMemberCoordinate.SelectMemberCoordinateByMemberIdToday(insEntMemberCoordinate);

            DataTable insDt_Noktalar = new DataTable();
            busRetailShop insBusRetailShop = new busRetailShop();
            insDt_Noktalar = insBusRetailShop.SelectRetailShopMapViewByMemberIdEffectiveDate(insEntMemberCoordinate.MemberId, new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));



            if (insBusMemberCoordinate.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusMemberCoordinate.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", JArray.Parse(JsonConvert.SerializeObject(insDt))),
                                        new JProperty("RetailShops", JArray.Parse(JsonConvert.SerializeObject(insDt_Noktalar)))
                                        );
            return returnJson;
            #endregion
        }
    }
}
