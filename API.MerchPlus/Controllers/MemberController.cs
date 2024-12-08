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
    [RoutePrefix("api/MemberController")]
    public class MemberController : ApiController
    {

        [Authorize]
        [HttpPost]
        [Route("WelcomeSMS")]
        public JObject WelcomeSMS(JObject data)
        {
            JObject returnJson;
            dynamic json = data;

            entMember insEntMember = new entMember();
            busMember insBusMember = new busMember();

            insEntMember.Id = Convert.ToString(json.Id);
            insBusMember.SelectMemberById(insEntMember);

            if (Convert.ToString(insEntMember.DataNumber) != string.Empty)
            {
                string messageText = ConfigurationManager.AppSettings["WelcomeSMSText"];

                String messageXML = "<request>";
                messageXML += "<authentication>";
                messageXML += "<username>" + ConfigurationManager.AppSettings["SMSGatewayUsername"] + "</username>";
                messageXML += "<password>" + ConfigurationManager.AppSettings["SMSGatewayPassword"] + "</password>";
                messageXML += "</authentication>";
                messageXML += "<order>";
                messageXML += "<sender>" + ConfigurationManager.AppSettings["SMSGatewayCaption"] + "</sender>";
                messageXML += "<sendDateTime></sendDateTime>";
                messageXML += "<message>";
                messageXML += "<text>" + string.Format(messageText, Convert.ToString(insEntMember.NameSurname), Convert.ToString(insEntMember.UserName), ConfigurationManager.AppSettings["DefaultPassword"]) + "</text>";
                messageXML += "<receipents>";
                messageXML += "<number>" + Convert.ToString(insEntMember.DataNumber) + "</number>";
                messageXML += "</receipents>";
                messageXML += "</message>";
                messageXML += "</order>";
                messageXML += "</request>";

                Helpers insHelper = new Helpers();
                insHelper.SendSMSwithXML(ConfigurationManager.AppSettings["SMSGatewayLink"], messageXML);

                returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", "SMS Sent")
                                    );
                return returnJson;
            }
            else
            {
                returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", "Data numarası girilmediği için SMS gönderilmedi.")
                                    );
                return returnJson;
            }

        }

        [Authorize]
        [HttpPost]
        [Route("ChangePasswordSMS")]
        public JObject ChangePasswordSMS(JObject data)
        {
            JObject returnJson;
            dynamic json = data;

            entMember insEntMember = new entMember();
            busMember insBusMember = new busMember();

            insEntMember.Id = Convert.ToString(json.Id);
            insBusMember.SelectMemberById(insEntMember);

            if (Convert.ToString(insEntMember.DataNumber) != string.Empty)
            {
                string messageText = ConfigurationManager.AppSettings["ChangePasswordSMSText"];

                String messageXML = "<request>";
                messageXML += "<authentication>";
                messageXML += "<username>" + ConfigurationManager.AppSettings["SMSGatewayUsername"] + "</username>";
                messageXML += "<password>" + ConfigurationManager.AppSettings["SMSGatewayPassword"] + "</password>";
                messageXML += "</authentication>";
                messageXML += "<order>";
                messageXML += "<sender>" + ConfigurationManager.AppSettings["SMSGatewayCaption"] + "</sender>";
                messageXML += "<sendDateTime></sendDateTime>";
                messageXML += "<message>";
                messageXML += "<text>" + string.Format(messageText, Convert.ToString(insEntMember.NameSurname), ConfigurationManager.AppSettings["DefaultPassword"]) + "</text>";
                messageXML += "<receipents>";
                messageXML += "<number>" + Convert.ToString(insEntMember.DataNumber) + "</number>";
                messageXML += "</receipents>";
                messageXML += "</message>";
                messageXML += "</order>";
                messageXML += "</request>";

                Helpers insHelper = new Helpers();
                insHelper.SendSMSwithXML(ConfigurationManager.AppSettings["SMSGatewayLink"], messageXML);

                returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", "SMS Sent")
                                    );
                return returnJson;
            }
            else
            {
                returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", "Data numarası girilmediği için SMS gönderilmedi.")
                                    );
                return returnJson;
            }

        }

        [AllowAnonymous]
        [Route("RegisterPozitif")]
        public JObject RegisterPozitif(JObject data)
        {
            JObject returnJson;
            dynamic json = data;

            #region Insert/Update Member Wisely
            entMember insEntMember = new entMember();
            busMember insBusMember = new busMember();
            Helpers insHelpers = new Helpers();

            insEntMember.Id = Convert.ToString(json.Id);
            insEntMember.UserName = Convert.ToString(json.UserName);
            insEntMember.NameSurname = Convert.ToString(json.NameSurname);
            insEntMember.HiringDate = insHelpers.ConvertToDateTime(Convert.ToString(json.HiringDate));
            insEntMember.MemberTitleId = Convert.ToInt32(json.MemberTitleId);
            insEntMember.SocialSecurityNumber = Convert.ToString(json.SocialSecurityNumber);
            insEntMember.Email = Convert.ToString(json.Email);
            if (json.LeavingDate != null)
                insEntMember.LeavingDate = insHelpers.ConvertToDateTime(Convert.ToString(json.LeavingDate));
            insEntMember.DirectReportId = Convert.ToString(json.DirectReportId);
            if (Convert.ToString(json.IsActive) == "true")
                insEntMember.IsActive = true;
            else
                insEntMember.IsActive = false;
            insEntMember.PhoneNumber = Convert.ToString(json.PhoneNumber);
            insEntMember.DataNumber = Convert.ToString(json.DataNumber);
            insEntMember.Address = Convert.ToString(json.Address);
            insEntMember.AddressCityId = Convert.ToInt32(json.AddressCityId);
            insEntMember.AddressCoordinateX = Convert.ToDecimal(Convert.ToDouble(json.AddressCoordinateX));
            insEntMember.AddressCoordinateY = Convert.ToDecimal(Convert.ToDouble(json.AddressCoordinateY));
            insEntMember.DeviceModelId = Convert.ToInt32(json.DeviceModelId);
            insEntMember.IMEINumber = Convert.ToString(json.IMEINumber);

            if (Convert.ToString(json.Base64Encoded) != "")
            {
                #region User uploaded Picture
                string fileName = Guid.NewGuid().ToString() + ".jpg";
                string filePath = ConfigurationManager.AppSettings["ProfilePicturePhysicalPath"] + fileName;
                string url = ConfigurationManager.AppSettings["ProfilePictureBaseURL"] + fileName;
                try
                {
                    string[] byteString = Convert.ToString(json.Base64Encoded).Split(',');
                    byte[] bytes = Convert.FromBase64String(byteString[1]);
                    File.WriteAllBytes(filePath, bytes);
                    insEntMember.ProfilePicturePath = url;
                }
                catch(Exception ex)
                {

                }
                #endregion
            }
            #endregion

            string MemberCustomerList = Convert.ToString(json.MemberCustomerId);
            string MemberDistrictList = Convert.ToString(json.MemberDistrictId);

            insBusMember.ManageMemberWisely(insEntMember, MemberCustomerList, MemberDistrictList);

            if (insBusMember.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusMember.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", "OK")
                                    );
            return returnJson;

        }

        [HttpPost]
        [Route("SelectMember")]
        public JObject SelectMember(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            #region Business Logic MemberModule
            busMember insBusMember = new busMember();
            DataTable insDt = new DataTable();
            insDt = insBusMember.SelectMember();
            if (insBusMember.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusMember.ErrorMessage)
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
        [Route("SelectMemberByCustomerIdGridData")]
        public JObject SelectMemberByCustomerIdGridData(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            #region Business Logic MemberModule
            busMember insBusMember = new busMember();
            entMemberCustomer insEntMemberCustomer = new entMemberCustomer();
            insEntMemberCustomer.CustomerId = Convert.ToInt32(json.Data.Id);
            DataTable insDt = new DataTable();
            insDt = insBusMember.SelectMemberByCustomerIdGridData(insEntMemberCustomer);
            if (insBusMember.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusMember.ErrorMessage)
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
        [Route("Members_MemberListGridData")]
        public JObject Members_MemberListGridData(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            #region Business Logic
            busMember insBusMember = new busMember();
            entMemberCustomer insEntMemberCustomer = new entMemberCustomer();
            insEntMemberCustomer.CustomerId = Convert.ToInt32(json.Data.Id);
            DataTable insDt = new DataTable();
            insDt = insBusMember.Members_MemberListGridData(insEntMemberCustomer);
            if (insBusMember.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusMember.ErrorMessage)
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
        [Route("SelectMemberByIdForEdit")]
        public JObject SelectMemberByIdForEdit(JObject data)
        {
            JObject returnJson;
            dynamic json = data;

            #region Business Logic MemberModule
            busMember insBusMember = new busMember();
            entMember insEntMember = new entMember();
            insEntMember.Id = Convert.ToString(json.Id);
            insBusMember.SelectMemberById(insEntMember);

            #region Get MemberCustomer
            DataTable insDt_MemberCustomer = new DataTable();
            busMemberCustomer insBusMemberCustomer = new busMemberCustomer();
            entMemberCustomer insEntMemberCustomer = new entMemberCustomer();
            insEntMemberCustomer.MemberId = insEntMember.Id;
            insDt_MemberCustomer = insBusMemberCustomer.SelectMemberCustomerByMemberIdGridData(insEntMemberCustomer);
            #endregion

            #region Get MemberDistrict
            DataTable insDt_MemberDistrict = new DataTable();
            busMemberDistrict insBusMemberDistrict = new busMemberDistrict();
            entMemberDistrict insEntMemberDistrict = new entMemberDistrict();
            insEntMemberDistrict.MemberId = insEntMember.Id;
            insDt_MemberDistrict = insBusMemberDistrict.SelectMemberDistrictByMemberId(insEntMemberCustomer);
            #endregion

            if (insBusMember.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusMember.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Member", JObject.FromObject(insEntMember)),
                                        new JProperty("MemberCustomer", JArray.Parse(JsonConvert.SerializeObject(insDt_MemberCustomer))),
                                        new JProperty("MemberDistrict", JArray.Parse(JsonConvert.SerializeObject(insDt_MemberDistrict)))
                                        );
            return returnJson;
            #endregion
        }

        [HttpPost]
        [Route("SelectMemberByMemberTitleIdCustomerId")]
        public JObject SelectMemberByMemberTitleIdCustomerId(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            #region Business Logic MemberModule
            busMember insBusMember = new busMember();
            DataTable insDt = new DataTable();
            insDt = insBusMember.SelectMemberByMemberTitleIdCustomerId(Convert.ToInt32(json.MemberTitleId), Convert.ToInt32(json.CustomerId));
            if (insBusMember.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusMember.ErrorMessage)
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

    }
}
