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
    [RoutePrefix("api/CustomerProductController")]
    public class CustomerProductController : ApiController
    {

        [HttpPost]
        [AllowAnonymous]
        [Route("SelectCustomerProduct")]
        public JObject SelectCustomerProduct()
        {

            JObject returnJson;

            #region Business Logic
            busCustomerProduct insBusCustomerProduct = new busCustomerProduct();
            DataTable insDt = new DataTable();
            insDt = insBusCustomerProduct.SelectCustomerProduct();
            if (insBusCustomerProduct.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusCustomerProduct.ErrorMessage)
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
        [Route("SelectCustomerProductByCustomerId")]
        public JObject SelectCustomerProductByCustomerId(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            #region Business Logic
            busCustomerProduct insBusCustomerProduct = new busCustomerProduct();
            entCustomerProduct insEntCustomerProduct = new entCustomerProduct();
            insEntCustomerProduct.CustomerId = Convert.ToInt32(json.CustomerId);

            DataTable insDt = new DataTable();
            insDt = insBusCustomerProduct.SelectCustomerProductByCustomerId(insEntCustomerProduct);
            if (insBusCustomerProduct.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusCustomerProduct.ErrorMessage)
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
        [Route("SelectCustomerProductByCustomerIdGridData")]
        public JObject SelectCustomerProductByCustomerIdGridData(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            #region Business Logic
            busCustomerProduct insBusCustomerProduct = new busCustomerProduct();
            entCustomerProduct insEntCustomerProduct = new entCustomerProduct();
            insEntCustomerProduct.CustomerId = Convert.ToInt32(json.CustomerId);

            DataTable insDt = new DataTable();
            insDt = insBusCustomerProduct.SelectCustomerProductByCustomerIdGridData(insEntCustomerProduct);
            if (insBusCustomerProduct.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusCustomerProduct.ErrorMessage)
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
        [Route("UpdateCustomerProductById")]
        public JObject UpdateCustomerProductById(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            if (Convert.ToInt32(json.Id) == 0)
            {
                #region Business Logic
                busCustomerProduct insBusCustomerProduct = new busCustomerProduct();
                entCustomerProduct insEntCustomerProduct = new entCustomerProduct();

                insEntCustomerProduct.Name = Convert.ToString(json.Name);
                insEntCustomerProduct.ProductBarcode = Convert.ToString(json.ProductBarcode);
                insEntCustomerProduct.ProductCode = Convert.ToString(json.ProductCode);
                insEntCustomerProduct.CaseEach = Convert.ToInt32(json.CaseEach);
                insEntCustomerProduct.SuggestedListPrice = Convert.ToDecimal(json.SuggestedListPrice);
                insEntCustomerProduct.SuggestedActivityPrice = Convert.ToDecimal(json.SuggestedActivityPrice);
                insEntCustomerProduct.PackagingSize = Convert.ToDecimal(json.PackagingSize);
                insEntCustomerProduct.CustomerBrandCategoryId = Convert.ToInt32(json.CustomerBrandCategoryId);

                #region Get CustomerBrand 
                entCustomerBrandCategory insEntCustomerBrandCategory = new entCustomerBrandCategory();
                insEntCustomerBrandCategory.Id = Convert.ToInt32(json.CustomerBrandCategoryId);
                busCustomerBrandCategory insBusCustomerBrandCategory = new busCustomerBrandCategory();
                insBusCustomerBrandCategory.SelectCustomerBrandCategoryById(insEntCustomerBrandCategory);
                #endregion

                #region Get CustomerBrand
                entCustomerBrand insEntCustomerBrand = new entCustomerBrand();
                busCustomerBrand insBusCustomerBrand = new busCustomerBrand();
                insEntCustomerBrand.Id = Convert.ToInt32(insEntCustomerBrandCategory.CustomerBrandId);
                insBusCustomerBrand.SelectCustomerBrandById(insEntCustomerBrand);
                #endregion

                if (insEntCustomerBrand.CustomerId != null)
                    insEntCustomerProduct.CustomerId = Convert.ToInt32(insEntCustomerBrand.CustomerId);

                insEntCustomerProduct.CustomerBrandId = Convert.ToInt32(insEntCustomerBrandCategory.CustomerBrandId);
                insEntCustomerProduct.CreatedBy = Convert.ToString(json.MemberId);
                insEntCustomerProduct.CreatedOn = DateTime.Now;
                insEntCustomerProduct.ProfilePicturePath = "Images/CustomerProductImage/NoPhoto.png";

                insBusCustomerProduct.InsertCustomerProduct(insEntCustomerProduct);

                returnJson = new JObject(new JProperty("Result", "NOK"));
                return returnJson;

                #endregion
            }
            else
            {
                #region Business Logic
                busCustomerProduct insBusCustomerProduct = new busCustomerProduct();
                entCustomerProduct insEntCustomerProduct = new entCustomerProduct();
                insEntCustomerProduct.Id = Convert.ToInt32(json.Id);
                insBusCustomerProduct.SelectCustomerProductById(insEntCustomerProduct);

                insEntCustomerProduct.Name = Convert.ToString(json.Name);
                insEntCustomerProduct.ProductBarcode = Convert.ToString(json.ProductBarcode);
                insEntCustomerProduct.ProductCode = Convert.ToString(json.ProductCode);
                insEntCustomerProduct.CaseEach = Convert.ToInt32(json.CaseEach);
                insEntCustomerProduct.SuggestedListPrice = Convert.ToDecimal(json.SuggestedListPrice);
                insEntCustomerProduct.SuggestedActivityPrice = Convert.ToDecimal(json.SuggestedActivityPrice);
                insEntCustomerProduct.PackagingSize = Convert.ToDecimal(json.PackagingSize);
                insEntCustomerProduct.CustomerBrandCategoryId = Convert.ToInt32(json.CustomerBrandCategoryId);
                insBusCustomerProduct.UpdateCustomerProductById(insEntCustomerProduct);

                returnJson = new JObject(new JProperty("Result", "NOK"));
                return returnJson;

                #endregion
            }


        }

        [HttpPost]
        [AllowAnonymous]
        [Route("SelectCustomerProductById")]
        public JObject SelectCustomerProductById(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            #region Business Logic
            busCustomerProduct insBusCustomerProduct = new busCustomerProduct();
            entCustomerProduct insEntCustomerProduct = new entCustomerProduct();
            insEntCustomerProduct.Id = Convert.ToInt32(json.Id);
            insBusCustomerProduct.SelectCustomerProductById(insEntCustomerProduct);

            returnJson = new JObject(new JProperty("Result", "NOK"),
                                     new JProperty("CustomerProduct", JObject.FromObject(insEntCustomerProduct))
                                    );
            return returnJson;
            #endregion
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("DeleteCustomerProductByIdWisely")]
        public JObject DeleteCustomerProductByIdWisely(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            #region Business Logic
            busCustomerProduct insBusCustomerProduct = new busCustomerProduct();
            entCustomerProduct insEntCustomerProduct = new entCustomerProduct();
            insEntCustomerProduct.Id = Convert.ToInt32(json.Id);
            insBusCustomerProduct.DeleteCustomerProductByIdWisely(insEntCustomerProduct);

            returnJson = new JObject(new JProperty("Result", "OK"));
            return returnJson;
            #endregion
        }
    }
}
