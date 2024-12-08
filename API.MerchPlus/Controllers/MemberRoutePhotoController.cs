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
    }
}
