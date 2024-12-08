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
    [RoutePrefix("api/MemberTitleController")]
    public class MemberTitleController : ApiController
    {

        [HttpPost]
        [Route("SelectMemberTitle")]
        public JObject SelectMemberTitle(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            #region Business Logic MemberModule
            busMemberTitle insBusMemberTitle = new busMemberTitle();
            DataTable insDt = new DataTable();
            insDt = insBusMemberTitle.SelectMemberTitle();
            if (insBusMemberTitle.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusMemberTitle.ErrorMessage)
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
        [Route("SelectMemberTitleById")]
        public entMemberTitle SelectMemberTitleById(JObject data)
        {
            dynamic json = data;

            #region Business Logic
            busMemberTitle insBusMemberTitle = new busMemberTitle();
            entMemberTitle insEntMemberTitle = new entMemberTitle();
            insEntMemberTitle.Id = Convert.ToInt32(json.Id);
            insBusMemberTitle.SelectMemberTitleById(insEntMemberTitle);
            if (insBusMemberTitle.HasErrors)
            {
                return null;
            }

            return insEntMemberTitle;
            #endregion
        }

    }
}
