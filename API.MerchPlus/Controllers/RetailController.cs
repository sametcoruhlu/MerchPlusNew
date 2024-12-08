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
    [AllowAnonymous]
    [RoutePrefix("api/RetailController")]
    public class RetailController : ApiController
    {


        [HttpPost]
        [Route("SelectRetail")]
        public JObject SelectRetail()
        {

            JObject returnJson;

            #region Business Logic
            busRetail insBusRetail = new busRetail();
            DataTable insDt = new DataTable();
            insDt = insBusRetail.SelectRetail();
            if (insBusRetail.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusRetail.ErrorMessage)
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
        [Route("SelectRetailByRetailCategoryId")]
        public JObject SelectRetailByRetailCategoryId(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            #region Business Logic
            busRetail insBusRetail = new busRetail();
            entRetail insEntRetail = new entRetail();
            insEntRetail.RetailCategoryId = Convert.ToInt32(json.RetailCategoryId);
            DataTable insDt = new DataTable();
            insDt = insBusRetail.SelectRetailByRetailCategoryId(insEntRetail);
            if (insBusRetail.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusRetail.ErrorMessage)
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
        [Route("SelectRetailGridData")]
        public JObject SelectRetailGridData()
        {

            JObject returnJson;

            #region Business Logic
            busRetail insBusRetail = new busRetail();
            DataTable insDt = new DataTable();
            insDt = insBusRetail.SelectRetailGridData();
            if (insBusRetail.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusRetail.ErrorMessage)
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
        [Route("UpdateRetailWisely")]
        public JObject UpdateRetailWisely(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            #region Business Logic
            busRetail insBusRetail = new busRetail();
            entRetail insEntRetail = new entRetail();
            insEntRetail.Id = Convert.ToInt32(json.Id);

            if (insEntRetail.Id != 0)
                insBusRetail.SelectRetailById(insEntRetail);

            insEntRetail.Name = Convert.ToString(json.Name);
            insEntRetail.RetailCategoryId = Convert.ToInt32(json.RetailCategoryId);
            insEntRetail.ProfilePicturePath = "Images/Retail/noProfile.png";

            if (insEntRetail.Id == 0)
            {
                insBusRetail.InsertRetail(insEntRetail);
            }
            else
            {
                insBusRetail.UpdateRetailById(insEntRetail);
            }

            if (insBusRetail.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusRetail.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK")
                                        );
            return returnJson;
            #endregion
        }
    }
}
