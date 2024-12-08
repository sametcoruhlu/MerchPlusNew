using busMerchPlus;
using entMerchPlus;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.MerchPlus.Controllers.Android
{
    [AllowAnonymous]
    [RoutePrefix("api/AndroidTestController")]
    public class AndroidTestController : ApiController
    {
        [HttpPost]
        [Route("SelectMemberByIMEINumber")]
        public JObject SelectMemberByIMEINumber(JObject data)
        {

            JObject returnJson;
            dynamic hede = data;

            entMember insEntMember = new entMember();
            busMember insBusMember = new busMember();
            insEntMember.IMEINumber = Convert.ToString(hede.IMEINumber);
            insBusMember.SelectMemberByIMEINumber(insEntMember);

            returnJson = new JObject(new JProperty("Result", "OK"),
                                        new JProperty("Content", JObject.FromObject(insEntMember))
                                        );
            return returnJson;

        }

        [HttpPost]
        [Route("GetCurrentApplicationVersion")]
        public JObject GetCurrentApplicationVersion(JObject data)
        {

            JObject returnJson;
            dynamic hede = data;

            string version = ConfigurationManager.AppSettings["CurrentAndroidVersion"];

            returnJson = new JObject(new JProperty("Result", "OK"),
                                        new JProperty("Version", version)
                                        );
            return returnJson;

        }

        [HttpPost]
        [Route("SelectPromotionType")]
        public JObject SelectPromotionType()
        {

            JObject returnJson;

            #region Business Logic
            busPromotion insBusPromotion = new busPromotion();
            DataTable insDt = new DataTable();
            insDt = insBusPromotion.SelectPromotionType();
            if (insBusPromotion.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusPromotion.ErrorMessage)
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
    }
}
