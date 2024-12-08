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
    [RoutePrefix("api/RetailShopController")]
    public class RetailShopController : ApiController
    {


        [HttpPost]
        [Route("SelectRetailShopMapView")]
        public JObject SelectRetailShopMapView(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            #region Business Logic
            busRetailShop insBusRetailShop = new busRetailShop();
            DataTable insDt = new DataTable();
            insDt = insBusRetailShop.SelectRetailShopMapView();
            if (insBusRetailShop.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusRetailShop.ErrorMessage)
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
        [Route("SelectRetailShopMapViewByCustomerId")]
        public JObject SelectRetailShopMapViewByCustomerId(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            #region Business Logic
            busRetailShop insBusRetailShop = new busRetailShop();
            DataTable insDt = new DataTable();

            int customerId = Convert.ToInt32(json.CustomerId);

            insDt = insBusRetailShop.SelectRetailShopMapViewByCustomerId(customerId);
            if (insBusRetailShop.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusRetailShop.ErrorMessage)
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
        [Route("SelectRetailShopByMemberIdMemberRouteDate")]
        public JObject SelectRetailShopByMemberIdMemberRouteDate(JObject data)
        {

            JObject returnJson;
            dynamic json = data;


            #region Business Logic
            busRetailShop insBusRetailShop = new busRetailShop();
            entMemberRoute insEntMemberRoute = new entMemberRoute();
            insEntMemberRoute.MemberId = json.MemberId;
            insEntMemberRoute.EffectiveDate = Convert.ToDateTime(json.EffectiveDate);

            DataTable insDt = new DataTable();
            insDt = insBusRetailShop.SelectRetailShopByMemberIdMemberRouteDate(insEntMemberRoute);
            if (insBusRetailShop.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusRetailShop.ErrorMessage)
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
        [Route("UpdateRetailShopById")]
        public JObject UpdateRetailShopById(JObject data)
        {
            JObject returnJson;
            dynamic json = data;

            busRetailShop insBusRetailShop = new busRetailShop();
            entRetailShop insEntRetailShop = new entRetailShop();

            #region Get Original Retail Shop
            insEntRetailShop.Id = Convert.ToInt32(json.Id);
            insBusRetailShop.SelectRetailShopById(insEntRetailShop);
            #endregion

            #region Make Changes
            insEntRetailShop.Address = Convert.ToString(json.Address);
            insEntRetailShop.CityId = Convert.ToInt32(json.CityId);
            insEntRetailShop.CoordinateX = Convert.ToDecimal(json.CoordinateX);
            insEntRetailShop.CoordinateY = Convert.ToDecimal(json.CoordinateY);
            insEntRetailShop.CreatedBy = Convert.ToString(json.CreatedBy);
            insEntRetailShop.DistrictId = Convert.ToInt32(json.DistrictId);
            insEntRetailShop.IsActive = true;
            insEntRetailShop.Name = Convert.ToString(json.Name);
            insEntRetailShop.RetailId = Convert.ToInt32(json.RetailId);
            insEntRetailShop.RetailShopCategoryId = Convert.ToInt32(json.RetailShopCategoryId);
            insBusRetailShop.UpdateRetailShopById(insEntRetailShop);
            #endregion

            if (insBusRetailShop.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                        new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                        new JProperty("Details", insBusRetailShop.ErrorMessage)
                                        );
                return returnJson;
            }

            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", JObject.Parse(JsonConvert.SerializeObject(insEntRetailShop)))
                                        );
            return returnJson;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("InsertRetailShop")]
        public JObject InsertRetailShop(JObject data)
        {
            JObject returnJson;
            dynamic json = data;

            busRetailShop insBusRetailShop = new busRetailShop();
            entRetailShop insEntRetailShop = new entRetailShop();


            #region Get Products listed in same format
            busCustomerProductRetailShop insBusCustomerProductRetailShop = new busCustomerProductRetailShop();
            DataTable insDt_CustomerProduct = new DataTable();
            insDt_CustomerProduct = insBusCustomerProductRetailShop.SelectCustomerProductRetailShopByRetailShopCategoryIdOnlyProducts(Convert.ToInt32(json.RetailShopCategoryId));
            #endregion

            #region Make Changes
            insEntRetailShop.Address = Convert.ToString(json.Address);
            insEntRetailShop.CityId = Convert.ToInt32(json.CityId);
            insEntRetailShop.CoordinateX = Convert.ToDecimal(json.CoordinateX);
            insEntRetailShop.CoordinateY = Convert.ToDecimal(json.CoordinateY);
            insEntRetailShop.CreatedBy = Convert.ToString(json.CreatedBy);
            insEntRetailShop.CreatedOn = DateTime.Now;
            insEntRetailShop.DistrictId = Convert.ToInt32(json.DistrictId);
            insEntRetailShop.IsActive = true;
            insEntRetailShop.Name = Convert.ToString(json.Name);
            insEntRetailShop.RetailId = Convert.ToInt32(json.RetailId);
            insEntRetailShop.RetailShopCategoryId = Convert.ToInt32(json.RetailShopCategoryId);
            insBusRetailShop.InsertRetailShop(insEntRetailShop);
            #endregion

            if (insDt_CustomerProduct.Rows.Count > 0)
            {
                foreach (DataRow insDrProduct in insDt_CustomerProduct.Rows)
                {
                    entCustomerProduct insEntCustomerProduct = new entCustomerProduct();
                    insEntCustomerProduct.Id = Convert.ToInt32(insDrProduct["CustomerProductId"]);
                    busCustomerProduct insBusCustomerProduct = new busCustomerProduct();
                    insBusCustomerProduct.SelectCustomerProductById(insEntCustomerProduct);

                    entCustomerProductRetailShop insEntProduct = new entCustomerProductRetailShop();
                    insEntProduct.CreatedBy = Convert.ToString(json.CreatedBy);
                    insEntProduct.CreatedOn = DateTime.Now;
                    insEntProduct.CustomerProductId = insEntCustomerProduct.Id;
                    insEntProduct.CustomerId = insEntCustomerProduct.CustomerId;
                    insEntProduct.RetailShopId = insEntRetailShop.Id;
                    insBusCustomerProductRetailShop.InsertCustomerProductRetailShop(insEntProduct);
                }
            }

            if (insBusRetailShop.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                        new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                        new JProperty("Details", insBusRetailShop.ErrorMessage)
                                        );
                return returnJson;
            }

            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", JObject.Parse(JsonConvert.SerializeObject(insEntRetailShop)))
                                        );
            return returnJson;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("DeleteRetailShopById")]
        public JObject DeleteRetailShopById(JObject data)
        {
            JObject returnJson;
            dynamic json = data;

            busRetailShop insBusRetailShop = new busRetailShop();
            entRetailShop insEntRetailShop = new entRetailShop();

            insEntRetailShop.Id = Convert.ToInt32(json.Id);

            insBusRetailShop.DeleteRetailShopByIdWisely(insEntRetailShop);

            #region Make Changes
            insEntRetailShop.Address = Convert.ToString(json.Address);
            insEntRetailShop.CityId = Convert.ToInt32(json.CityId);
            insEntRetailShop.CoordinateX = Convert.ToDecimal(json.CoordinateX);
            insEntRetailShop.CoordinateY = Convert.ToDecimal(json.CoordinateY);
            insEntRetailShop.CreatedBy = Convert.ToString(json.CreatedBy);
            insEntRetailShop.CreatedOn = DateTime.Now;
            insEntRetailShop.DistrictId = Convert.ToInt32(json.DistrictId);
            insEntRetailShop.IsActive = true;
            insEntRetailShop.Name = Convert.ToString(json.Name);
            insEntRetailShop.RetailId = Convert.ToInt32(json.RetailId);
            insEntRetailShop.RetailShopCategoryId = Convert.ToInt32(json.RetailShopCategoryId);
            insBusRetailShop.InsertRetailShop(insEntRetailShop);
            #endregion

            if (insBusRetailShop.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                        new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                        new JProperty("Details", insBusRetailShop.ErrorMessage)
                                        );
                return returnJson;
            }

            returnJson = new JObject(
                                        new JProperty("Result", "OK")
                                        );
            return returnJson;
        }
    }
}
