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
    [RoutePrefix("api/CustomerController")]
    public class CustomerController : ApiController
    {

        [HttpPost]
        [Route("SelectCustomer")]
        public JObject SelectCustomerController(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            #region Business Logic 
            busCustomer insBusCustomer = new busCustomer();
            DataTable insDt = new DataTable();
            insDt = insBusCustomer.SelectCustomer();
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
            #endregion
        }

    }
}
