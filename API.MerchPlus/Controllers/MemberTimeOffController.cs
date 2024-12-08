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
    [RoutePrefix("api/MemberTimeOffController")]
    public class MemberTimeOffController : ApiController
    {
        [HttpPost]
        [Route("SelectMemberTimeOffByMemberId")]
        public JObject SelectMemberTimeOffByMemberId(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            #region Business Logic MemberModule
            busMemberTimeOff insBusMemberTimeOff = new busMemberTimeOff();
            entMemberTimeOff insEntMemberTimeOff = new entMemberTimeOff();
            insEntMemberTimeOff.MemberId = Convert.ToString(json.MemberId);
            DataTable insDt = new DataTable();
            insDt = insBusMemberTimeOff.SelectMemberTimeOffByMemberId(insEntMemberTimeOff);
            if (insBusMemberTimeOff.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusMemberTimeOff.ErrorMessage)
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
        [Route("DeleteMemberTimeOffById")]
        public JObject DeleteMemberTimeOffById(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            #region Business Logic MemberModule
            busMemberTimeOff insBusMemberTimeOff = new busMemberTimeOff();
            entMemberTimeOff insEntMemberTimeOff = new entMemberTimeOff();
            insEntMemberTimeOff.Id = Convert.ToInt32(json.Id);
            insBusMemberTimeOff.DeleteMemberTimeOffById(insEntMemberTimeOff);
            if (insBusMemberTimeOff.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusMemberTimeOff.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", "OK")
                                        );
            return returnJson;
            #endregion
        }

        [HttpPost]
        [Route("InsertMemberTimeOff")]
        public JObject InsertMemberTimeOff(JObject data)
        {
            JObject returnJson;
            dynamic json = data;

            #region Business Logic MemberModule
            busMemberTimeOff insBusMemberTimeOff = new busMemberTimeOff();
            entMemberTimeOff insEntMemberTimeOff = new entMemberTimeOff();

            Helpers insHelper = new Helpers();
            insEntMemberTimeOff.StartDate = insHelper.ConvertToDateTime(Convert.ToString(json.StartDate));
            insEntMemberTimeOff.EndDate = insHelper.ConvertToDateTime(Convert.ToString(json.EndDate));
            insEntMemberTimeOff.CreatedBy = Convert.ToString(json.CreatedBy);
            insEntMemberTimeOff.IsOffRoute = Convert.ToBoolean(json.IsOffRoute);
            insEntMemberTimeOff.MemberId = Convert.ToString(json.MemberId);
            insEntMemberTimeOff.Description = Convert.ToString(json.Description);
            insEntMemberTimeOff.CreatedOn = DateTime.Now;

            insBusMemberTimeOff.InsertMemberTimeOff(insEntMemberTimeOff);
            if (insBusMemberTimeOff.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusMemberTimeOff.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", "OK")
                                        );
            return returnJson;
            #endregion
        }
    }
}
