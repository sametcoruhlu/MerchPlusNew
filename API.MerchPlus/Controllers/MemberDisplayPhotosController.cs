using busMerchPlus;
using entMerchPlus;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Configuration;
using System.IO;

namespace API.MerchPlus.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/MemberDisplayPhotosController")]
    public class MemberDisplayPhotosController : ApiController
    {
        [HttpPost]
        [Route("InsertMemberDisplayPhotos")]
        public JObject InsertMemberDisplayPhotos(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            #region Business Logic
            string fileName = Guid.NewGuid().ToString() + ".jpg";
            string filePath = ConfigurationManager.AppSettings["DisplayPhotosPhysicalPath"] + fileName;
            string url = ConfigurationManager.AppSettings["DisplayPhotosBaseURL"] + fileName;

            busMemberDisplayPhotos insBusMemberDisplayPhotos = new busMemberDisplayPhotos();
            entMemberDisplayPhotos insEntMemberDisplayPhotos = new entMemberDisplayPhotos();

            insEntMemberDisplayPhotos.DisplayPicturePath = url;
            insEntMemberDisplayPhotos.IsSentToServer = true;
            insEntMemberDisplayPhotos.MemberId = Convert.ToString(json.MemberId);
            insEntMemberDisplayPhotos.CreatedOn = Convert.ToDateTime(json.CreatedOn);
            insEntMemberDisplayPhotos.MemberRouteId = Convert.ToInt32(json.MemberRouteId);
            insEntMemberDisplayPhotos.CustomerProductId = Convert.ToInt32(json.CustomerProductId);
            insEntMemberDisplayPhotos.PromotionTypeId = Convert.ToInt32(json.PromotionTypeId);

            byte[] bytes = Convert.FromBase64String(Convert.ToString(json.DisplayPicturePath));
            File.WriteAllBytes(filePath, bytes);

            DataTable insDt = new DataTable();
            insBusMemberDisplayPhotos.InsertMemberDisplayPhotos(insEntMemberDisplayPhotos);
            if (insBusMemberDisplayPhotos.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusMemberDisplayPhotos.ErrorMessage)
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
