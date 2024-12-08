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
    [RoutePrefix("api/CustomerProductRetailShopController")]
    public class CustomerProductRetailShopController : ApiController
    {

        [HttpPost]
        [AllowAnonymous]
        [Route("InsertCustomerProductRetailShopWisely_RetailCategoryMode")]
        public JObject InsertCustomerProductRetailShopWisely_RetailCategoryMode(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            #region Business Logic
            busRetailCategory insBusRetailCategory = new busRetailCategory();
            entRetailCategory insEntRetailCategory = new entRetailCategory();
            insEntRetailCategory.Id = Convert.ToInt32(json.RetailCategoryId);
            insBusRetailCategory.SelectRetailCategoryById(insEntRetailCategory);

            #region Get All RetailShop under this Category
            DataTable insDt_RetailShop = new DataTable();
            busRetailShop insBusRetailShop = new busRetailShop();
            insDt_RetailShop = insBusRetailShop.SelectRetailShopByRetailCategoryId(insEntRetailCategory);
            #endregion

            #region Loop each RetailShop and check entry. If doesn't exist, make insert
            busCustomerProductRetailShop insBusCustomerProductRetailShop = new busCustomerProductRetailShop();

            #region Delete existing mappings
            insBusCustomerProductRetailShop.DeleteCustomerProductRetailShopByCustomerIdRetailChannelId(Convert.ToInt32(json.CustomerId), Convert.ToInt32(json.RetailCategoryId));
            #endregion

            foreach (DataRow insDr in insDt_RetailShop.Rows)
            {
                foreach (int customerProductId in ((JArray)json.CustomerProductId).ToObject<int[]>())
                {
                    entCustomerProductRetailShop insEntCustomerProductRetailShop = new entCustomerProductRetailShop();
                    insEntCustomerProductRetailShop.CustomerId = Convert.ToInt32(json.CustomerId);
                    insEntCustomerProductRetailShop.CustomerProductId = customerProductId;
                    insEntCustomerProductRetailShop.RetailShopId = Convert.ToInt32(insDr["Id"]);
                    insBusCustomerProductRetailShop.SelectCustomerProductRetailShopByCustomerIdCustomerProductIdRetailShopId(insEntCustomerProductRetailShop);

                    if (insEntCustomerProductRetailShop.Id == 0)
                    {
                        insEntCustomerProductRetailShop.CreatedBy = Convert.ToString(json.CreatedBy);
                        insEntCustomerProductRetailShop.CreatedOn = DateTime.Now;
                        insBusCustomerProductRetailShop.InsertCustomerProductRetailShop(insEntCustomerProductRetailShop);
                    }
                }
            }
            #endregion

            if (insBusCustomerProductRetailShop.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusCustomerProductRetailShop.ErrorMessage)
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
        [Route("InsertCustomerProductRetailShopWisely_RetailMode")]
        public JObject InsertCustomerProductRetailShopWisely_RetailMode(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            #region Business Logic
            busRetailCategory insBusRetailCategory = new busRetailCategory();
            entRetailCategory insEntRetailCategory = new entRetailCategory();
            insEntRetailCategory.Id = Convert.ToInt32(json.RetailCategoryId);
            insBusRetailCategory.SelectRetailCategoryById(insEntRetailCategory);

            #region Get All RetailShop under this Category
            DataTable insDt_RetailShop = new DataTable();
            entRetailShop insEntRetailShop = new entRetailShop();
            busRetailShop insBusRetailShop = new busRetailShop();
            insEntRetailShop.RetailId = Convert.ToInt32(json.RetailId);
            insDt_RetailShop = insBusRetailShop.SelectRetailShopByRetailId(insEntRetailShop);
            #endregion

            #region Loop each RetailShop and check entry. If doesn't exist, make insert
            busCustomerProductRetailShop insBusCustomerProductRetailShop = new busCustomerProductRetailShop();

            #region Delete Existing Mapping
            insBusCustomerProductRetailShop.DeleteCustomerProductRetailShopByCustomerIdRetailId(Convert.ToInt32(json.CustomerId), Convert.ToInt32(json.RetailId));
            #endregion

            foreach (DataRow insDr in insDt_RetailShop.Rows)
            {
                foreach (int customerProductId in ((JArray)json.CustomerProductId).ToObject<int[]>())
                {
                    entCustomerProductRetailShop insEntCustomerProductRetailShop = new entCustomerProductRetailShop();
                    insEntCustomerProductRetailShop.CustomerId = Convert.ToInt32(json.CustomerId);
                    insEntCustomerProductRetailShop.CustomerProductId = customerProductId;
                    insEntCustomerProductRetailShop.RetailShopId = Convert.ToInt32(insDr["Id"]);
                    insBusCustomerProductRetailShop.SelectCustomerProductRetailShopByCustomerIdCustomerProductIdRetailShopId(insEntCustomerProductRetailShop);

                    if (insEntCustomerProductRetailShop.Id == 0)
                    {
                        insEntCustomerProductRetailShop.CreatedBy = Convert.ToString(json.CreatedBy);
                        insEntCustomerProductRetailShop.CreatedOn = DateTime.Now;
                        insBusCustomerProductRetailShop.InsertCustomerProductRetailShop(insEntCustomerProductRetailShop);
                    }
                }
            }
            #endregion

            if (insBusCustomerProductRetailShop.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusCustomerProductRetailShop.ErrorMessage)
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
        [Route("InsertCustomerProductRetailShopWisely_RetailShopCategoryMode")]
        public JObject InsertCustomerProductRetailShopWisely_RetailShopCategoryMode(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            #region Business Logic
      
            #region Get All RetailShop under this Category
            DataTable insDt_RetailShop = new DataTable();
            entRetailShop insEntRetailShop = new entRetailShop();
            busRetailShop insBusRetailShop = new busRetailShop();
            insEntRetailShop.RetailShopCategoryId = Convert.ToInt32(json.RetailShopCategoryId);
            insDt_RetailShop = insBusRetailShop.SelectRetailShopByRetailShopCategoryId(insEntRetailShop);
            #endregion

            #region Loop each RetailShop and check entry. If doesn't exist, make insert
            busCustomerProductRetailShop insBusCustomerProductRetailShop = new busCustomerProductRetailShop();

            #region Delete all mappings for this customer and retailcategory
            insBusCustomerProductRetailShop.DeleteCustomerProductRetailShopByCustomerIdRetailCategoryId(Convert.ToInt32(json.CustomerId), Convert.ToInt32(json.RetailShopCategoryId));
            #endregion

            foreach (DataRow insDr in insDt_RetailShop.Rows)
            {
                foreach (int customerProductId in ((JArray)json.CustomerProductId).ToObject<int[]>())
                {
                    entCustomerProductRetailShop insEntCustomerProductRetailShop = new entCustomerProductRetailShop();
                    insEntCustomerProductRetailShop.CustomerId = Convert.ToInt32(json.CustomerId);
                    insEntCustomerProductRetailShop.CustomerProductId = customerProductId;
                    insEntCustomerProductRetailShop.RetailShopId = Convert.ToInt32(insDr["Id"]);
                    insBusCustomerProductRetailShop.SelectCustomerProductRetailShopByCustomerIdCustomerProductIdRetailShopId(insEntCustomerProductRetailShop);

                    if (insEntCustomerProductRetailShop.Id == 0)
                    {
                        insEntCustomerProductRetailShop.CreatedBy = Convert.ToString(json.CreatedBy);
                        insEntCustomerProductRetailShop.CreatedOn = DateTime.Now;
                        insBusCustomerProductRetailShop.InsertCustomerProductRetailShop(insEntCustomerProductRetailShop);
                    }
                }
            }
            #endregion

            if (insBusCustomerProductRetailShop.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusCustomerProductRetailShop.ErrorMessage)
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
        [Route("InsertCustomerProductRetailShopWisely_RetailShopMode")]
        public JObject InsertCustomerProductRetailShopWisely_RetailShopMode(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            #region Business Logic

            #region Loop each RetailShop and check entry. If doesn't exist, make insert
            busCustomerProductRetailShop insBusCustomerProductRetailShop = new busCustomerProductRetailShop();

            #region Delete All CustomerProductRetailShop records by CustomerProductRetailShopId
            entCustomerProductRetailShop insEntCustomerProductRetailShop_Delete = new entCustomerProductRetailShop();
            insEntCustomerProductRetailShop_Delete.RetailShopId = Convert.ToInt32(json.RetailShopId);
            insEntCustomerProductRetailShop_Delete.CustomerId = Convert.ToInt32(json.CustomerId);
            insBusCustomerProductRetailShop.DeleteCustomerProductRetailShopByRetailShopIdCustomerId(insEntCustomerProductRetailShop_Delete);
            #endregion

            foreach (int customerProductId in ((JArray)json.CustomerProductId).ToObject<int[]>())
            {
                entCustomerProductRetailShop insEntCustomerProductRetailShop = new entCustomerProductRetailShop();
                insEntCustomerProductRetailShop.CustomerId = Convert.ToInt32(json.CustomerId);
                insEntCustomerProductRetailShop.CustomerProductId = customerProductId;
                insEntCustomerProductRetailShop.RetailShopId = Convert.ToInt32(json.RetailShopId);
                insBusCustomerProductRetailShop.SelectCustomerProductRetailShopByCustomerIdCustomerProductIdRetailShopId(insEntCustomerProductRetailShop);

                if (insEntCustomerProductRetailShop.Id == 0)
                {
                    insEntCustomerProductRetailShop.CreatedBy = Convert.ToString(json.CreatedBy);
                    insEntCustomerProductRetailShop.CreatedOn = DateTime.Now;
                    insBusCustomerProductRetailShop.InsertCustomerProductRetailShop(insEntCustomerProductRetailShop);
                }
            }
            
            #endregion

            if (insBusCustomerProductRetailShop.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusCustomerProductRetailShop.ErrorMessage)
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
        [Route("DeleteCustomerProductRetailShopById")]
        public JObject DeleteCustomerProductRetailShopById(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            #region Business Logic

            entCustomerProductRetailShop insEnt = new entCustomerProductRetailShop();
            busCustomerProductRetailShop insBus = new busCustomerProductRetailShop();
            insEnt.Id = Convert.ToInt32(json.Id);
            insBus.DeleteCustomerProductRetailShopById(insEnt);

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
        [Route("SelectCustomerProductRetailShopByRetailShopId")]
        public JObject SelectCustomerProductRetailShopByRetailShopId(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            #region Business Logic

            DataTable insDt = new DataTable();
            busCustomerProductRetailShop insBusCustomerProductRetailShop = new busCustomerProductRetailShop();
            entCustomerProductRetailShop insEntCustomerProductRetailShop = new entCustomerProductRetailShop();
            insEntCustomerProductRetailShop.RetailShopId = Convert.ToInt32(json.RetailShopId);
            insDt = insBusCustomerProductRetailShop.SelectCustomerProductRetailShopByRetailShopId(insEntCustomerProductRetailShop);

            if (insBusCustomerProductRetailShop.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusCustomerProductRetailShop.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", JArray.FromObject(insDt))
                                        );
            return returnJson;
            #endregion
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("SelectCustomerProductRetailShopByRetailCategoryId")]
        public JObject SelectCustomerProductRetailShopByRetailCategoryId(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            #region Business Logic

            DataTable insDt = new DataTable();
            busCustomerProductRetailShop insBusCustomerProductRetailShop = new busCustomerProductRetailShop();
            insDt = insBusCustomerProductRetailShop.SelectCustomerProductRetailShopByRetailCategoryId(Convert.ToInt32(json.RetailCategoryId));

            if (insBusCustomerProductRetailShop.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusCustomerProductRetailShop.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", JArray.FromObject(insDt))
                                        );
            return returnJson;
            #endregion
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("SelectCustomerProductRetailShopByRetailId")]
        public JObject SelectCustomerProductRetailShopByRetailId(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            #region Business Logic

            DataTable insDt = new DataTable();
            busCustomerProductRetailShop insBusCustomerProductRetailShop = new busCustomerProductRetailShop();
            insDt = insBusCustomerProductRetailShop.SelectCustomerProductRetailShopByRetailId(Convert.ToInt32(json.RetailId));

            if (insBusCustomerProductRetailShop.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusCustomerProductRetailShop.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", JArray.FromObject(insDt))
                                        );
            return returnJson;
            #endregion
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("SelectCustomerProductRetailShopByRetailChannelId")]
        public JObject SelectCustomerProductRetailShopByRetailChannelId(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            #region Business Logic

            DataTable insDt = new DataTable();
            busCustomerProductRetailShop insBusCustomerProductRetailShop = new busCustomerProductRetailShop();
            insDt = insBusCustomerProductRetailShop.SelectCustomerProductRetailShopByRetailChannelId(Convert.ToInt32(json.RetailCategoryId));

            if (insBusCustomerProductRetailShop.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusCustomerProductRetailShop.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", JArray.FromObject(insDt))
                                        );
            return returnJson;
            #endregion
        }
    }
}
