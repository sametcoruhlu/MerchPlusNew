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
    [RoutePrefix("api/CustomerBrandController")]
    public class CustomerBrandController : ApiController
    {

        [HttpPost]
        [AllowAnonymous]
        [Route("SelectCustomerBrandGridData")]
        public JObject SelectCustomerBrandGridData()
        {

            JObject returnJson;

            #region Business Logic
            busCustomerBrand insBusCustomerBrand = new busCustomerBrand();
            List<CustomerBrandCategoryItem> list = new List<CustomerBrandCategoryItem>();
            list = insBusCustomerBrand.SelectCustomerBrandGridData();

            if (insBusCustomerBrand.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusCustomerBrand.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", JArray.Parse(JsonConvert.SerializeObject(list)))
                                        );
            return returnJson;
            #endregion
        }
    }
}
