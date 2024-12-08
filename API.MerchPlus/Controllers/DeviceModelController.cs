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
    [RoutePrefix("api/DeviceModelController")]
    public class DeviceModelController : ApiController
    {

        [HttpPost]
        [Route("SelectDeviceModel")]
        public JObject SelectDeviceModel(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            #region Business Logic MemberModule
            busDeviceModel insBusDeviceModel = new busDeviceModel();
            DataTable insDt = new DataTable();
            insDt = insBusDeviceModel.SelectDeviceModel();
            if (insBusDeviceModel.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusDeviceModel.ErrorMessage)
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
