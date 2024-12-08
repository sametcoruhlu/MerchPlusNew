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
    [RoutePrefix("api/MemberCustomerController")]
    public class MemberCustomerController : ApiController
    {

        [HttpPost]
        [Route("SelectMemberCustomerByMemberId")]
        public JObject SelectMemberCustomerByMemberId(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            #region Business Logic MemberModule
            busMemberCustomer insBusMemberCustomer = new busMemberCustomer();
            entMemberCustomer insEntMemberCustomer = new entMemberCustomer();
            insEntMemberCustomer.MemberId = Convert.ToString(json.MemberId);
            DataTable insDt = new DataTable();
            insDt = insBusMemberCustomer.SelectMemberCustomerByMemberIdGridData(insEntMemberCustomer);
            if (insBusMemberCustomer.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusMemberCustomer.ErrorMessage)
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
