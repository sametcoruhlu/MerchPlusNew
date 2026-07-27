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
using System.Security.Cryptography;

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

            dynamic json = data;
            try
            {
                string physicalRoot = ConfigurationManager.AppSettings["DisplayPhotosPhysicalPath"];
                string baseUrl = ConfigurationManager.AppSettings["DisplayPhotosBaseURL"];
                if (string.IsNullOrWhiteSpace(physicalRoot) || string.IsNullOrWhiteSpace(baseUrl))
                {
                    return new JObject(
                        new JProperty("Result", "NOK"),
                        new JProperty("Reason", "CONFIG_ERROR"),
                        new JProperty("Details", "DisplayPhotosPhysicalPath veya DisplayPhotosBaseURL boş.")
                    );
                }
                // Base64 -> bytes
                byte[] bytes = Convert.FromBase64String(Convert.ToString(json.DisplayPicturePath));
                // Aynı içerik için aynı dosya adı
                string hashHex;
                using (var sha = SHA256.Create())
                {
                    hashHex = BitConverter.ToString(sha.ComputeHash(bytes)).Replace("-", "").ToLowerInvariant();
                }
                string fileName = hashHex + ".jpg";
                string filePath = Path.Combine(physicalRoot, fileName);
                string url = baseUrl.TrimEnd('/') + "/" + fileName;
                // Fiziksel çoğalmayı engelle
                if (!File.Exists(filePath))
                {
                    Directory.CreateDirectory(physicalRoot);
                    File.WriteAllBytes(filePath, bytes);
                }
                // DB kaydı
                busMemberDisplayPhotos insBusMemberDisplayPhotos = new busMemberDisplayPhotos();
                entMemberDisplayPhotos insEntMemberDisplayPhotos = new entMemberDisplayPhotos
                {
                    DisplayPicturePath = url,
                    IsSentToServer = true,
                    MemberId = Convert.ToString(json.MemberId),
                    CreatedOn = Convert.ToDateTime(json.CreatedOn),
                    MemberRouteId = Convert.ToInt32(json.MemberRouteId),
                    CustomerProductId = Convert.ToInt32(json.CustomerProductId),
                    PromotionTypeId = Convert.ToInt32(json.PromotionTypeId)
                };
                insBusMemberDisplayPhotos.InsertMemberDisplayPhotos(insEntMemberDisplayPhotos);
                if (insBusMemberDisplayPhotos.HasErrors)
                {
                    return new JObject(
                        new JProperty("Result", "NOK"),
                        new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                        new JProperty("Details", insBusMemberDisplayPhotos.ErrorMessage)
                    );
                }
                // Eski davranışı korumak için boş DataTable response
                DataTable insDt = new DataTable();
                return new JObject(
                    new JProperty("Result", "OK"),
                    new JProperty("Content", JArray.Parse(JsonConvert.SerializeObject(insDt)))
                );
            }
            catch (FormatException ex)
            {
                return new JObject(
                    new JProperty("Result", "NOK"),
                    new JProperty("Reason", "INVALID_BASE64"),
                    new JProperty("Details", ex.Message)
                );
            }
            catch (Exception ex)
            {
                return new JObject(
                    new JProperty("Result", "NOK"),
                    new JProperty("Reason", "UNEXPECTED_ERROR"),
                    new JProperty("Details", ex.Message)
                );
            }
        }
    }
}
