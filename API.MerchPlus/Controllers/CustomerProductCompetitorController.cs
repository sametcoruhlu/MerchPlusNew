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
    [RoutePrefix("api/CustomerProductCompetitorController")]
    public class CustomerProductCompetitorController : ApiController
    {

        [HttpPost]
        [AllowAnonymous]
        [Route("SelectCustomerProductCompetitorByCustomerIdGridData")]
        public JObject SelectCustomerProductCompetitorByCustomerIdGridData(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            #region Business Logic
            int customerId = Convert.ToInt32(json.CustomerId);
            busCustomerProductCompetitor insBus = new busCustomerProductCompetitor();
            DataTable insDt = new DataTable();
            insDt = insBus.SelectCustomerProductCompetitorByCustomerIdGridData(customerId);
            if (insBus.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBus.ErrorMessage)
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
        [AllowAnonymous]
        [Route("SelectCustomerProductCompetitorByCustomerId")]
        public JObject SelectCustomerProductCompetitorByCustomerId(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            #region Business Logic
            int customerId = Convert.ToInt32(json.CustomerId);
            busCustomerProductCompetitor insBus = new busCustomerProductCompetitor();
            DataTable insDt = new DataTable();
            insDt = insBus.SelectCustomerProductCompetitorByCustomerId(customerId);
            if (insBus.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBus.ErrorMessage)
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
        [AllowAnonymous]
        [Route("InsertCustomerProductCompetitor")]
        public JObject InsertCustomerProductCompetitor(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            #region Business Logic
            int customerId = Convert.ToInt32(json.CustomerId);
            busCustomerProductCompetitor insBus = new busCustomerProductCompetitor();
            entCustomerProductCompetitor insEnt = new entCustomerProductCompetitor();

            insEnt.CustomerProductId = Convert.ToInt32(json.CustomerProductId);
            insEnt.CompetitorCustomerProductId = Convert.ToInt32(json.CustomerProductCompetitorId);
            insBus.InsertCustomerProductCompetitor(insEnt);

            if (insBus.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBus.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK")
                                        );
            return returnJson;
            #endregion
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("DeleteCustomerProductCompetitorById")]
        public JObject DeleteCustomerProductCompetitorById(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            #region Business Logic
            int customerId = Convert.ToInt32(json.CustomerId);
            busCustomerProductCompetitor insBus = new busCustomerProductCompetitor();
            entCustomerProductCompetitor insEnt = new entCustomerProductCompetitor();

            insEnt.Id = Convert.ToInt32(json.Id);
            insBus.DeleteCustomerProductCompetitorById(insEnt);

            if (insBus.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBus.ErrorMessage)
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
