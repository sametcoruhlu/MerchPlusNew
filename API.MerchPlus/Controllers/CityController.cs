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
    [RoutePrefix("api/CityController")]
    public class CityController : ApiController
    {

        [HttpPost]
        [Route("SelectCity")]
        public JObject SelectCity(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            #region Business Logic 
            busCity insBusCity = new busCity();
            DataTable insDt = new DataTable();
            insDt = insBusCity.SelectCity();
            insDt.DefaultView.Sort = "Name ASC";
            insDt = insDt.DefaultView.ToTable();
            if (insBusCity.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusCity.ErrorMessage)
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
