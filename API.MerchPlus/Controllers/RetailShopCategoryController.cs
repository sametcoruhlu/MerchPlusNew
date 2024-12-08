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
    [RoutePrefix("api/RetailShopCategoryController")]
    public class RetailShopCategoryController : ApiController
    {

        [HttpPost]
        [AllowAnonymous]
        [Route("SelectRetailShopCategoryByRetailId")]
        public JObject SelectRetailShopCategoryByRetailId(JObject data)
        {

            JObject returnJson;
            dynamic json = data;


            #region Business Logic
            busRetailShopCategory insBusRetailShopCategory = new busRetailShopCategory();
            entRetailShopCategory insEntRetailShopCategory = new entRetailShopCategory();
            insEntRetailShopCategory.RetailId = Convert.ToInt32(json.RetailId);

            DataTable insDt = new DataTable();
            insDt = insBusRetailShopCategory.SelectRetailShopCategoryByRetailId(insEntRetailShopCategory);
            if (insBusRetailShopCategory.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusRetailShopCategory.ErrorMessage)
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
        [Route("SelectRetailShopCategoryGridData")]
        public JObject SelectRetailShopCategoryGridData(JObject data)
        {

            JObject returnJson;
            dynamic json = data;


            #region Business Logic
            busRetailShopCategory insBusRetailShopCategory = new busRetailShopCategory();
            DataTable insDt = new DataTable();
            insDt = insBusRetailShopCategory.SelectRetailShopCategoryGridData();
            if (insBusRetailShopCategory.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusRetailShopCategory.ErrorMessage)
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
        [Route("InsertRetailShopCategory")]
        public JObject InsertRetailShopCategory(JObject data)
        {

            JObject returnJson;
            dynamic json = data;


            #region Business Logic
            busRetailShopCategory insBusRetailShopCategory = new busRetailShopCategory();
            entRetailShopCategory insEntRetailShopCategory = new entRetailShopCategory();
            insEntRetailShopCategory.Name = Convert.ToString(json.Name);
            insEntRetailShopCategory.RetailId = Convert.ToInt32(json.RetailId);
            insBusRetailShopCategory.InsertRetailShopCategory(insEntRetailShopCategory);
            
            if (insBusRetailShopCategory.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusRetailShopCategory.ErrorMessage)
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
