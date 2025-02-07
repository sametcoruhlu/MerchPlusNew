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
using System.Drawing;
using System.IO.Compression;
using System.Drawing.Imaging;

namespace API.MerchPlus.Controllers
{
    [Authorize]
    [RoutePrefix("api/MemberRoutePhotoController")]
    public class MemberRoutePhotoController : ApiController
    {

        [HttpPost]
        [AllowAnonymous]
        [Route("DownloadMemberRoutePhotoByMemberRouteList")]
        public JObject DownloadMemberRoutePhotoByMemberRouteList(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            List<int> memberRouteList = new List<int>();
            memberRouteList = ((JArray)json.MemberRouteList).ToObject<List<int>>();
            //memberRouteList.Add(29303);

            string downloadLink = "FotografAlbumu_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".zip";
            string tempDirectory = Guid.NewGuid().ToString();

            #region Create Temp Directory for Export
            DirectoryInfo insDirectory = new DirectoryInfo(ConfigurationManager.AppSettings["ShopPhotosPhysicalPath"] + "\\TempExport\\" + tempDirectory);
            if (!insDirectory.Exists)
                insDirectory.Create();
            #endregion

            List<string> filesToIncludeInZip = new List<string>();
            int pictureCounter = 0;

            List<string> errorMessages = new List<string>();

            foreach (int memberRouteId in memberRouteList)
            {
                pictureCounter = 0;
                #region Get Member Route
                entMemberRoute insEntMemberRoute = new entMemberRoute();
                busMemberRoute insBusMemberRoute = new busMemberRoute();
                insEntMemberRoute.Id = memberRouteId;
                insBusMemberRoute.SelectMemberRouteById(insEntMemberRoute);
                #endregion

                #region Get Retail Shop
                entRetailShop insEntRetailShop = new entRetailShop();
                busRetailShop insBusRetailShop = new busRetailShop();
                insEntRetailShop.Id = Convert.ToInt32(insEntMemberRoute.RetailShopId);
                insBusRetailShop.SelectRetailShopById(insEntRetailShop);
                #endregion

                #region Get Member
                entMember insEntMember = new entMember();
                busMember insBusMember = new busMember();
                insEntMember.Id = Convert.ToString(insEntMemberRoute.MemberId);
                insBusMember.SelectMemberById(insEntMember);
                #endregion

                #region Get Photos
                busMemberRoutePhoto insBusMemberRoutePhoto = new busMemberRoutePhoto();
                DataTable insDt_Photos = new DataTable();
                insDt_Photos = insBusMemberRoutePhoto.SelectMemberRoutePhotoByMemberRouteId(memberRouteId);
                #endregion


                #region Loop each Image and Prepare Images with Info Bar
                foreach (DataRow insDr in insDt_Photos.Rows)
                {
                tryAgain:
                    try
                    {
                        pictureCounter += 1;
                        string fileName = insEntMember.NameSurname + "_" + Convert.ToString(pictureCounter).PadLeft(3, '0') + "_" + Convert.ToString(insEntRetailShop.Name) + "_" + Convert.ToString(insDr["Id"]) + ".jpg";
                        string imageFilePath = ConfigurationManager.AppSettings["ShopPhotosPhysicalPath"] + Convert.ToString(insDr["ProfilePicturePath"]).Replace("Images/ShopPhotos/", "");
                        Image insOriginalImage = Image.FromStream(new MemoryStream(File.ReadAllBytes(imageFilePath)));
                        Bitmap bitmap = (Bitmap)insOriginalImage;
                        using (Graphics graphics = Graphics.FromImage(bitmap))
                        {
                            //Find the %90 of the height of the picture
                            int Height_95 = Convert.ToInt32(Convert.ToDecimal(bitmap.Height) * Convert.ToDecimal(0.95F));
                            int Height_05 = Convert.ToInt32(Convert.ToDecimal(bitmap.Height) * Convert.ToDecimal(0.05F));

                            Rectangle backgroundRectangle = new Rectangle(0, Height_95, bitmap.Width, Height_05);

                            using (Font arialFont = new Font("Arial", 20))
                            {
                                Brush backgroundBrush = new SolidBrush(Color.FromArgb(100, 0, 0, 0));
                                graphics.FillRectangle(backgroundBrush, backgroundRectangle);
                                int LabelTop = Convert.ToInt32(Convert.ToDecimal(bitmap.Height) * Convert.ToDecimal(0.96F));
                                graphics.DrawString(Convert.ToString(insEntRetailShop.Name) + " | " + Convert.ToString(insEntMember.NameSurname) + " | " + Convert.ToDateTime(insDr["CreatedOn"]).ToString("dd.MM.yyyy HH:mm"), arialFont, Brushes.White, new PointF(20, LabelTop));
                            }

                            bitmap.Save(insDirectory.ToString() + "\\" + fileName);
                            filesToIncludeInZip.Add(insDirectory.ToString() + "\\" + fileName);
                        }
                    }
                    catch (Exception ex)
                    {
                        errorMessages.Add("MemberRoutePhotoId : " + Convert.ToString(insDr["Id"]) + "\r\n" + ex.ToString() + "\r\n\r\n");
                        goto tryAgain;
                    }
                }
                #endregion
            }

            #region Save ZIP File
            ZipUtility.CreateZipFile(insDirectory.ToString() + "\\" + downloadLink, filesToIncludeInZip);
            #endregion


            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", tempDirectory + "/" + downloadLink),
                                        new JProperty("ErrorMessages", JArray.FromObject(errorMessages))
                                        );
            return returnJson;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("DownloadMemberRoutePhotoByCustomerIdStartDateEndDate")]
        public JObject DownloadMemberRoutePhotoByCustomerIdStartDateEndDate(JObject data)
        {

            JObject returnJson;
            dynamic json = data;


            string downloadLink = "TeshirAlbumu_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".zip";
            string tempDirectory = Guid.NewGuid().ToString();

            #region Create Temp Directory for Export
            DirectoryInfo insDirectory = new DirectoryInfo(ConfigurationManager.AppSettings["ShopPhotosPhysicalPath"] + "\\TempExport\\" + tempDirectory);
            if (!insDirectory.Exists)
                insDirectory.Create();
            #endregion

            List<string> filesToIncludeInZip = new List<string>();
            int pictureCounter = 0;

            List<string> errorMessages = new List<string>();

            busMemberRoute insBusMemberRoute_All = new busMemberRoute();
            DataTable insDt_MemberRouteAll = new DataTable();
            DateTime insStartDate = Convert.ToDateTime(json.StartDate);
            DateTime insEndDate = Convert.ToDateTime(json.EndDate);
            int customerId = Convert.ToInt32(json.CustomerId);
            insDt_MemberRouteAll = insBusMemberRoute_All.SelectMemberRouteByCustomerIdStartDateEndDate(customerId, insStartDate, insEndDate);

            List<int> memberRouteList = new List<int>();
            foreach (DataRow insDr in insDt_MemberRouteAll.Rows)
            {
                memberRouteList.Add(Convert.ToInt32(insDr["Id"]));
            }

            foreach (int memberRouteId in memberRouteList)
            {
                pictureCounter = 0;
                #region Get Member Route
                entMemberRoute insEntMemberRoute = new entMemberRoute();
                busMemberRoute insBusMemberRoute = new busMemberRoute();
                insEntMemberRoute.Id = memberRouteId;
                insBusMemberRoute.SelectMemberRouteById(insEntMemberRoute);
                #endregion

                #region Get Retail Shop
                entRetailShop insEntRetailShop = new entRetailShop();
                busRetailShop insBusRetailShop = new busRetailShop();
                insEntRetailShop.Id = Convert.ToInt32(insEntMemberRoute.RetailShopId);
                insBusRetailShop.SelectRetailShopById(insEntRetailShop);
                #endregion

                #region Get Member
                entMember insEntMember = new entMember();
                busMember insBusMember = new busMember();
                insEntMember.Id = Convert.ToString(insEntMemberRoute.MemberId);
                insBusMember.SelectMemberById(insEntMember);
                #endregion

                #region Get Photos
                busMemberRoutePhoto insBusMemberRoutePhoto = new busMemberRoutePhoto();
                DataTable insDt_Photos = new DataTable();
                insDt_Photos = insBusMemberRoutePhoto.SelectMemberRoutePhotoByMemberRouteId(memberRouteId);
                #endregion


                #region Loop each Image and Prepare Images with Info Bar
                foreach (DataRow insDr in insDt_Photos.Rows)
                {
                tryAgain:
                    try
                    {
                        pictureCounter += 1;
                        string fileName = insEntMember.NameSurname + "_" + Convert.ToString(pictureCounter).PadLeft(3, '0') + "_" + Convert.ToString(insEntRetailShop.Name) + "_" + Convert.ToString(insDr["Id"]) + ".jpg";
                        string imageFilePath = ConfigurationManager.AppSettings["ShopPhotosPhysicalPath"] + Convert.ToString(insDr["ProfilePicturePath"]).Replace("Images/ShopPhotos/", "");
                        Image insOriginalImage = Image.FromStream(new MemoryStream(File.ReadAllBytes(imageFilePath)));
                        Bitmap bitmap = (Bitmap)insOriginalImage;
                        using (Graphics graphics = Graphics.FromImage(bitmap))
                        {
                            //Find the %90 of the height of the picture
                            int Height_95 = Convert.ToInt32(Convert.ToDecimal(bitmap.Height) * Convert.ToDecimal(0.95F));
                            int Height_05 = Convert.ToInt32(Convert.ToDecimal(bitmap.Height) * Convert.ToDecimal(0.05F));

                            Rectangle backgroundRectangle = new Rectangle(0, Height_95, bitmap.Width, Height_05);

                            using (Font arialFont = new Font("Arial", 20))
                            {
                                Brush backgroundBrush = new SolidBrush(Color.FromArgb(100, 0, 0, 0));
                                graphics.FillRectangle(backgroundBrush, backgroundRectangle);
                                int LabelTop = Convert.ToInt32(Convert.ToDecimal(bitmap.Height) * Convert.ToDecimal(0.96F));
                                graphics.DrawString(Convert.ToString(insEntRetailShop.Name) + " | " + Convert.ToString(insEntMember.NameSurname) + " | " + Convert.ToDateTime(insDr["CreatedOn"]).ToString("dd.MM.yyyy HH:mm"), arialFont, Brushes.White, new PointF(20, LabelTop));
                            }

                            bitmap.Save(insDirectory.ToString() + "\\" + fileName);
                            filesToIncludeInZip.Add(insDirectory.ToString() + "\\" + fileName);
                        }
                    }
                    catch (Exception ex)
                    {
                        errorMessages.Add("MemberRoutePhotoId : " + Convert.ToString(insDr["Id"]) + "\r\n" + ex.ToString() + "\r\n\r\n");
                        goto tryAgain;
                    }
                }
                #endregion
            }

            #region Save ZIP File
            ZipUtility.CreateZipFile(insDirectory.ToString() + "\\" + downloadLink, filesToIncludeInZip);
            #endregion


            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", tempDirectory + "/" + downloadLink),
                                        new JProperty("ErrorMessages", JArray.FromObject(errorMessages))
                                        );
            return returnJson;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("DownloadMemberRouteDisplayPhotoByMemberRouteList")]
        public JObject DownloadMemberRouteDisplayPhotoByMemberRouteList([FromBody] MemberRouteRequest request)
        {
            string logFile = Path.Combine(ConfigurationManager.AppSettings["LogPath"], "Logs", "DownloadLog.txt");
            Directory.CreateDirectory(Path.GetDirectoryName(logFile)); // Log klasörü oluştur

            void WriteLog(string message)
            {
                File.AppendAllText(logFile, $"[{DateTime.Now}] {message}\n");
            }

            WriteLog("✅ İşlem başlatıldı.");

            JObject returnJson;

            try
            {
                if (request == null || request.MemberRouteList == null || request.MemberRouteList.Count == 0)
                {
                    WriteLog("❌ Hata: İstek boş veya geçersiz.");
                    return new JObject(
                        new JProperty("Result", "notOK"),
                        new JProperty("Content", ""),
                        new JProperty("ErrorMessages", "Geçersiz istek, liste boş olamaz.")
                    );
                }
                //string basePath = ConfigurationManager.AppSettings["DisplayPhotosPhysicalPath"];
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["DisplayPhotosPhysicalPath"]))
                {
                    return new JObject(
                        new JProperty("Result", "notOK"),
                        new JProperty("Content", ""),
                        new JProperty("ErrorMessages", "DisplayPhotosPhysicalPath ayarı bulunamadı veya boş!")
                    );
                }

                string downloadFileName = $"FotografAlbumu_{DateTime.Now:yyyyMMddHHmmss}.zip";
                string tempDirectory = Path.Combine(ConfigurationManager.AppSettings["DisplayPhotosPhysicalPath"], "TempExport", Guid.NewGuid().ToString());

                try
                {
                    Directory.CreateDirectory(tempDirectory);
                    WriteLog($"📂 Temp dizini oluşturuldu: {tempDirectory}");
                }
                catch (Exception ex)
                {
                    WriteLog($"⛔ Temp dizini oluşturulurken hata oluştu: {ex.Message}");
                    return new JObject(
                        new JProperty("Result", "notOK"),
                        new JProperty("Content", ""),
                        new JProperty("ErrorMessages", "Temp dizini oluşturulurken hata oluştu")
                    );
                }

                List<string> filesToIncludeInZip = new List<string>();
                List<string> errorMessages = new List<string>();

                foreach (var memberRoute in request.MemberRouteList)
                {
                    try
                    {
                        if (string.IsNullOrWhiteSpace(memberRoute.DisplayPicturePath))
                        {
                            WriteLog($"⚠️ Hata: DisplayPicturePath boş. ID: {memberRoute.MemberRouteId}");
                            errorMessages.Add($"DisplayPicturePath boş: {memberRoute.MemberRouteId}");
                            return new JObject(
                                    new JProperty("Result", "notOK"),
                                    new JProperty("Content", ""),
                                    new JProperty("ErrorMessages", "DisplayPicturePath boş olamaz.")
                                );
                        }

                        string fileName = $"{memberRoute.MerchNameSurname}_{memberRoute.RetailShopName}_{Guid.NewGuid():N}.jpg";

                        string basePath = ConfigurationManager.AppSettings["DisplayPhotosPhysicalPath"].TrimEnd('\\', '/');
                        string relativePath = memberRoute.DisplayPicturePath.Replace("Images/DisplayPhotos/", "").TrimStart('\\', '/');
                        string imageFilePath = Path.Combine(basePath, relativePath).Replace(" ", "");

                        if (!File.Exists(imageFilePath))
                        {
                            WriteLog($"❌ Dosya bulunamadı: {imageFilePath}");
                            errorMessages.Add($"Dosya bulunamadı: {imageFilePath}");
                            return new JObject(
                                    new JProperty("Result", "notOK"),
                                    new JProperty("Content", ""),
                                    new JProperty("ErrorMessages", $"Dosya bulunamadı: {imageFilePath}")
                                );
                        }

                        using (Image insOriginalImage = Image.FromFile(imageFilePath))
                        using (Bitmap bitmap = new Bitmap(insOriginalImage))
                        using (Graphics graphics = Graphics.FromImage(bitmap))
                        {
                            int labelTop = Convert.ToInt32(bitmap.Height * 0.96F);
                            using (Font arialFont = new Font("Arial", 20))
                            {
                                Brush backgroundBrush = new SolidBrush(Color.FromArgb(100, 0, 0, 0));
                                graphics.FillRectangle(backgroundBrush, new Rectangle(0, labelTop - 20, bitmap.Width, 30));
                                graphics.DrawString($"{memberRoute.RetailShopName} | {memberRoute.MerchNameSurname}", arialFont, Brushes.White, new PointF(20, labelTop));
                            }

                            string outputFilePath = Path.Combine(tempDirectory, fileName);
                            bitmap.Save(outputFilePath, ImageFormat.Jpeg);
                            filesToIncludeInZip.Add(outputFilePath);
                            WriteLog($"✅ Resim işlendi ve kaydedildi: {outputFilePath}");
                        }
                    }
                    catch (OutOfMemoryException ex)
                    {
                        WriteLog($"🚨 Bellek hatası: {ex.Message}");
                        errorMessages.Add("Bellek yetersiz. Lütfen daha az resim seçin.");
                        return new JObject(
                                    new JProperty("Result", "notOK"),
                                    new JProperty("Content", ""),
                                    new JProperty("ErrorMessages", "Bellek yetersiz.Lütfen daha az resim seçin.")
                                );
                    }
                    catch (Exception ex)
                    {
                        WriteLog($"⚠️ Genel hata: {ex.Message}");
                        errorMessages.Add($"Hata oluştu: {ex.Message}");
                        return new JObject(
                                    new JProperty("Result", "notOK"),
                                    new JProperty("Content", ""),
                                    new JProperty("ErrorMessages", $"Hata oluştu: {ex.Message}")
                                );
                    }
                }

                if (filesToIncludeInZip.Count == 0)
                {
                    WriteLog("❌ ZIP oluşturulamadı, dahil edilecek dosya yok.");
                    return new JObject(
                        new JProperty("Result", "notOK"),
                        new JProperty("Content", ""),
                        new JProperty("ErrorMessages", "ZIP dosyasına eklenecek resim bulunamadı.")
                    );
                }

                string zipFilePath = Path.Combine(tempDirectory, downloadFileName);
                using (ZipArchive zip = ZipFile.Open(zipFilePath, ZipArchiveMode.Create))
                {
                    foreach (string file in filesToIncludeInZip)
                    {
                        zip.CreateEntryFromFile(file, Path.GetFileName(file));
                    }
                }

                WriteLog($"✅ ZIP dosyası başarıyla oluşturuldu: {zipFilePath}");

                returnJson = new JObject(
                    new JProperty("Result", "OK"),
                    new JProperty("Content", $"{Path.GetFileName(tempDirectory)}/{downloadFileName}"),
                    new JProperty("ErrorMessages", JArray.FromObject(errorMessages))
                );

                return returnJson;
            }
            catch (UnauthorizedAccessException ex)
            {
                WriteLog($"🚨 Yetki hatası: {ex.Message}");
                return new JObject(
                    new JProperty("Result", "notOK"),
                    new JProperty("Content", ""),
                    new JProperty("ErrorMessages", "Yetkisiz erişim hatası. Lütfen izinleri kontrol edin.")
                );
            }
            catch (IOException ex)
            {
                WriteLog($"📂 Dosya sistemi hatası: {ex.Message}");
                return new JObject(
                    new JProperty("Result", "notOK"),
                    new JProperty("Content", ""),
                    new JProperty("ErrorMessages", "Dosya sistemi hatası oluştu. Lütfen klasör ve disk kullanımını kontrol edin.")
                );
            }
            catch (Exception ex)
            {
                WriteLog($"🚨 Genel hata: {ex}");
                return new JObject(
                    new JProperty("Result", "notOK"),
                    new JProperty("Content", ""),
                    new JProperty("ErrorMessages", $"Beklenmeyen bir hata oluştu: {ex.Message}")
                );
            }
        }

        // Dosya adlarında yasaklı karakterleri temizleyen fonksiyon
        private string SanitizeFileName(string fileName)
        {
            return string.Join("_", fileName.Split(Path.GetInvalidFileNameChars())).Replace(" ", "_");
        }
    }
}
