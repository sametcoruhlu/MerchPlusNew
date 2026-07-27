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
using System.Web;
using System.Web.Http.Cors;

namespace API.MerchPlus.Controllers
{
    [AllowAnonymous]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/CustomerFormFieldVisibility")]
    public class CustomerFormFieldVisibilityController : ApiController
    {
        /// <summary>
        /// Get all CustomerFormFieldVisibility records
        /// </summary>
        /// <returns>JSON response with all records</returns>
        [HttpGet]
        [Route("SelectAll")]
        public JObject SelectAll()
        {
            JObject returnJson;
            
            try
            {
                #region Business Logic 
                busCustomerFormFieldVisibility insBusCustomerFormFieldVisibility = new busCustomerFormFieldVisibility();
                DataTable insDt = new DataTable();
                insDt = insBusCustomerFormFieldVisibility.SelectCustomerFormFieldVisibility();
                
                if (insBusCustomerFormFieldVisibility.HasErrors)
                {
                    returnJson = new JObject(
                        new JProperty("Result", "NOK"),
                        new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                        new JProperty("Details", insBusCustomerFormFieldVisibility.ErrorMessage)
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
            catch (Exception ex)
            {
                returnJson = new JObject(
                    new JProperty("Result", "NOK"),
                    new JProperty("Reason", "EXCEPTION OCCURED"),
                    new JProperty("Details", ex.Message)
                );
                return returnJson;
            }
        }

        /// <summary>
        /// Get CustomerFormFieldVisibility records by CustomerId
        /// </summary>
        /// <param name="data">JSON data containing CustomerId</param>
        /// <returns>JSON response with filtered records</returns>
        [HttpPost]
        [Route("SelectByCustomerId")]
        public JObject SelectByCustomerId(JObject data)
        {
            JObject returnJson;
            
            try
            {
                dynamic json = data;
                int customerId = json.CustomerId;

                if (customerId <= 0)
                {
                    returnJson = new JObject(
                        new JProperty("Result", "NOK"),
                        new JProperty("Reason", "INVALID CUSTOMER ID"),
                        new JProperty("Details", "CustomerId must be greater than 0")
                    );
                    return returnJson;
                }

                #region Business Logic 
                busCustomerFormFieldVisibility insBusCustomerFormFieldVisibility = new busCustomerFormFieldVisibility();
                DataTable insDt = new DataTable();
                insDt = insBusCustomerFormFieldVisibility.SelectCustomerFormFieldVisibilityByCustomerId(customerId);
                
                if (insBusCustomerFormFieldVisibility.HasErrors)
                {
                    returnJson = new JObject(
                        new JProperty("Result", "NOK"),
                        new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                        new JProperty("Details", insBusCustomerFormFieldVisibility.ErrorMessage)
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
            catch (Exception ex)
            {
                returnJson = new JObject(
                    new JProperty("Result", "NOK"),
                    new JProperty("Reason", "EXCEPTION OCCURED"),
                    new JProperty("Details", ex.Message)
                );
                return returnJson;
            }
        }

        /// <summary>
        /// Get CustomerFormFieldVisibility by ID
        /// </summary>
        /// <param name="data">JSON data containing ID</param>
        /// <returns>JSON response with specific record</returns>
        [HttpPost]
        [Route("SelectById")]
        public JObject SelectById(JObject data)
        {
            JObject returnJson;
            
            try
            {
                dynamic json = data;
                int id = json.Id;

                if (id <= 0)
                {
                    returnJson = new JObject(
                        new JProperty("Result", "NOK"),
                        new JProperty("Reason", "INVALID ID"),
                        new JProperty("Details", "ID must be greater than 0")
                    );
                    return returnJson;
                }

                #region Business Logic 
                busCustomerFormFieldVisibility insBusCustomerFormFieldVisibility = new busCustomerFormFieldVisibility();
                
                // Create entity object and populate ID
                entCustomerFormFieldVisibility ent = new entCustomerFormFieldVisibility();
                ent.Id = id;
                
                // Call business method to populate entity
                insBusCustomerFormFieldVisibility.SelectCustomerFormFieldVisibilityById(ent);
                
                if (insBusCustomerFormFieldVisibility.HasErrors)
                {
                    returnJson = new JObject(
                        new JProperty("Result", "NOK"),
                        new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                        new JProperty("Details", insBusCustomerFormFieldVisibility.ErrorMessage)
                    );
                    return returnJson;
                }

                if (ent == null)
                {
                    returnJson = new JObject(
                        new JProperty("Result", "NOK"),
                        new JProperty("Reason", "RECORD NOT FOUND"),
                        new JProperty("Details", "No record found with the specified ID")
                    );
                    return returnJson;
                }
                
                returnJson = new JObject(
                    new JProperty("Result", "OK"),
                    new JProperty("Content", JObject.Parse(JsonConvert.SerializeObject(ent)))
                );
                return returnJson;
                #endregion
            }
            catch (Exception ex)
            {
                returnJson = new JObject(
                    new JProperty("Result", "NOK"),
                    new JProperty("Reason", "EXCEPTION OCCURED"),
                    new JProperty("Details", ex.Message)
                );
                return returnJson;
            }
        }

        /// <summary>
        /// Insert new CustomerFormFieldVisibility record
        /// </summary>
        /// <param name="data">JSON data containing CustomerFormFieldVisibility information</param>
        /// <returns>JSON response with operation result</returns>
        [HttpPost]
        [Route("Insert")]
        public JObject Insert(JObject data)
        {
            JObject returnJson;
            
            try
            {
                dynamic json = data;
                
                // Create entity from JSON data
                entCustomerFormFieldVisibility ent = new entCustomerFormFieldVisibility();
                ent.CustomerId = json.CustomerId;
                ent.FieldName = json.FieldName;
                ent.IsVisible = json.IsVisible;
                ent.IsRequired = json.IsRequired;
                ent.DisplayOrder = json.DisplayOrder;
                ent.CreatedBy = json.CreatedBy;
                ent.IsActive = true;

                #region Business Logic 
                busCustomerFormFieldVisibility insBusCustomerFormFieldVisibility = new busCustomerFormFieldVisibility();
                insBusCustomerFormFieldVisibility.InsertCustomerFormFieldVisibility(ent);
                
                if (insBusCustomerFormFieldVisibility.HasErrors)
                {
                    returnJson = new JObject(
                        new JProperty("Result", "NOK"),
                        new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                        new JProperty("Details", insBusCustomerFormFieldVisibility.ErrorMessage)
                    );
                    return returnJson;
                }

                // Check if insertion was successful
                if (!insBusCustomerFormFieldVisibility.HasErrors)
                {
                    returnJson = new JObject(
                        new JProperty("Result", "OK"),
                        new JProperty("Reason", "RECORD INSERTED SUCCESSFULLY"),
                        new JProperty("Details", "New record created with ID: " + ent.Id)
                    );
                }
                else
                {
                    returnJson = new JObject(
                        new JProperty("Result", "NOK"),
                        new JProperty("Reason", "INSERT FAILED"),
                        new JProperty("Details", "Failed to insert new record")
                    );
                }
                
                return returnJson;
                #endregion
            }
            catch (Exception ex)
            {
                returnJson = new JObject(
                    new JProperty("Result", "NOK"),
                    new JProperty("Reason", "EXCEPTION OCCURED"),
                    new JProperty("Details", ex.Message)
                );
                return returnJson;
            }
        }

        /// <summary>
        /// Update existing CustomerFormFieldVisibility record
        /// </summary>
        /// <param name="data">JSON data containing CustomerFormFieldVisibility information</param>
        /// <returns>JSON response with operation result</returns>
        [HttpPost]
        [Route("Update")]
        public JObject Update(JObject data)
        {
            JObject returnJson;
            
            try
            {
                dynamic json = data;
                
                // Create entity from JSON data
                entCustomerFormFieldVisibility ent = new entCustomerFormFieldVisibility();
                ent.Id = json.Id;
                ent.CustomerId = json.CustomerId;
                ent.FieldName = json.FieldName;
                ent.IsVisible = json.IsVisible;
                ent.IsRequired = json.IsRequired;
                ent.DisplayOrder = json.DisplayOrder;
                ent.ModifiedBy = json.ModifiedBy;
                ent.IsActive = json.IsActive;

                #region Business Logic 
                busCustomerFormFieldVisibility insBusCustomerFormFieldVisibility = new busCustomerFormFieldVisibility();
                insBusCustomerFormFieldVisibility.UpdateCustomerFormFieldVisibilityById(ent);
                
                if (insBusCustomerFormFieldVisibility.HasErrors)
                {
                    returnJson = new JObject(
                        new JProperty("Result", "NOK"),
                        new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                        new JProperty("Details", insBusCustomerFormFieldVisibility.ErrorMessage)
                    );
                    return returnJson;
                }

                // Check if update was successful
                if (!insBusCustomerFormFieldVisibility.HasErrors)
                {
                    returnJson = new JObject(
                        new JProperty("Result", "OK"),
                        new JProperty("Reason", "RECORD UPDATED SUCCESSFULLY"),
                        new JProperty("Details", "Record updated successfully")
                    );
                }
                else
                {
                    returnJson = new JObject(
                        new JProperty("Result", "NOK"),
                        new JProperty("Reason", "UPDATE FAILED"),
                        new JProperty("Details", "Failed to update record")
                    );
                }
                
                return returnJson;
                #endregion
            }
            catch (Exception ex)
            {
                returnJson = new JObject(
                    new JProperty("Result", "NOK"),
                    new JProperty("Reason", "EXCEPTION OCCURED"),
                    new JProperty("Details", ex.Message)
                );
                return returnJson;
            }
        }

        /// <summary>
        /// Delete CustomerFormFieldVisibility record
        /// </summary>
        /// <param name="data">JSON data containing ID</param>
        /// <returns>JSON response with operation result</returns>
        [HttpPost]
        [Route("Delete")]
        public JObject Delete(JObject data)
        {
            JObject returnJson;
            
            try
            {
                dynamic json = data;
                int id = json.Id;

                if (id <= 0)
                {
                    returnJson = new JObject(
                        new JProperty("Result", "NOK"),
                        new JProperty("Reason", "INVALID ID"),
                        new JProperty("Details", "ID must be greater than 0")
                    );
                    return returnJson;
                }

                #region Business Logic 
                busCustomerFormFieldVisibility insBusCustomerFormFieldVisibility = new busCustomerFormFieldVisibility();
                
                // Create entity object for deletion
                entCustomerFormFieldVisibility ent = new entCustomerFormFieldVisibility();
                ent.Id = id;
                
                insBusCustomerFormFieldVisibility.DeleteCustomerFormFieldVisibilityById(ent);
                
                if (insBusCustomerFormFieldVisibility.HasErrors)
                {
                    returnJson = new JObject(
                        new JProperty("Result", "NOK"),
                        new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                        new JProperty("Details", insBusCustomerFormFieldVisibility.ErrorMessage)
                    );
                    return returnJson;
                }

                // Check if deletion was successful
                if (!insBusCustomerFormFieldVisibility.HasErrors)
                {
                    returnJson = new JObject(
                        new JProperty("Result", "OK"),
                        new JProperty("Reason", "RECORD DELETED SUCCESSFULLY"),
                        new JProperty("Details", "Record deleted successfully")
                    );
                }
                else
                {
                    returnJson = new JObject(
                        new JProperty("Result", "NOK"),
                        new JProperty("Reason", "DELETE FAILED"),
                        new JProperty("Details", "Failed to delete record")
                    );
                }
                
                return returnJson;
                #endregion
            }
            catch (Exception ex)
            {
                returnJson = new JObject(
                    new JProperty("Result", "NOK"),
                    new JProperty("Reason", "EXCEPTION OCCURED"),
                    new JProperty("Details", ex.Message)
                );
                return returnJson;
            }
        }

        /// <summary>
        /// Handle preflight OPTIONS request
        /// </summary>
        [HttpOptions]
        [Route("GetFieldNames")]
        public void OptionsGetFieldNames()
        {
            // Preflight request handler - CORS will handle the response
        }

        /// <summary>
        /// Get available field names
        /// </summary>
        /// <returns>JSON response with field names</returns>
        [HttpGet]
        [Route("GetFieldNames")]
        public JObject GetFieldNames()
        {
            JObject returnJson;
            
            try
            {
                #region Business Logic 
                // Define available field names directly in controller
                List<string> fieldNames = new List<string>
                {
                    "CustomerName",
                    "CustomerAddress", 
                    "CustomerPhone",
                    "CustomerEmail",
                    "ContactPerson",
                    "TaxNumber",
                    "TaxOffice",
                    "PaymentTerms",
                    "CreditLimit",
                    "Notes"
                };
                
                returnJson = new JObject(
                    new JProperty("Result", "OK"),
                    new JProperty("Content", JArray.Parse(JsonConvert.SerializeObject(fieldNames)))
                );
                return returnJson;
                #endregion
            }
            catch (Exception ex)
            {
                returnJson = new JObject(
                    new JProperty("Result", "NOK"),
                    new JProperty("Reason", "EXCEPTION OCCURED"),
                    new JProperty("Details", ex.Message)
                );
                return returnJson;
            }
        }
    }
}
