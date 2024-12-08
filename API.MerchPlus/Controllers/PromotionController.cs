using busMerchPlus;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using entMerchPlus;

namespace API.MerchPlus.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/PromotionController")]
    public class PromotionController : ApiController
    {
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
