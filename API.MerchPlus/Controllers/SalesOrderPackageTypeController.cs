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
    [RoutePrefix("api/SalesOrderPackageTypeController")]
    public class SalesOrderPackageTypeController : ApiController
    {

        [HttpPost]
        [AllowAnonymous()]
        [Route("SelectSalesOrderPackageType")]
        public JObject SelectSalesOrderPackageType(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            #region Business Logic 
            busSalesOrderPackageType insBusSalesOrderPackageType = new busSalesOrderPackageType();
            DataTable insDt = new DataTable();
            insDt = insBusSalesOrderPackageType.SelectSalesOrderPackageType();
            if (insBusSalesOrderPackageType.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusSalesOrderPackageType.ErrorMessage)
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
