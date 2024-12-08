using datMerchPlus;
using entMerchPlus;
using SqlHelper;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;

namespace busMerchPlus
{
    /// <summary>
    /// This layer is responsible for implementing business logic.
    /// Manages transactions on table [MemberRoute]
    /// </summary>
    public class busMemberRoute : BusBase
    {
        /// <summary>
        /// busMemberRouteConstructor method used while taking an instance of this class.
        /// </summary>
        public busMemberRoute() : base()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Selects all data stored in table [MemberRoute]
        /// </summary>
        public DataTable SelectMemberRoute()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberRoute insDatMemberRoute = new datMemberRoute();
                return insDatMemberRoute.SelectMemberRoute(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Selects one row of data stored in table [MemberRoute] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntMemberRoute">Gets entity object as parameter for table MemberRoute]</param>
        public void SelectMemberRouteById(entMemberRoute parEntMemberRoute)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberRoute insDatMemberRoute = new datMemberRoute();
                insDatMemberRoute.SelectMemberRouteById(parEntMemberRoute, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Inserts one row of data into table [MemberRoute] based on the values of input entity object
        /// </summary>
        /// <param name="parEntMemberRoute">Gets entity object as parameter for table MemberRoute]</param>
        public void InsertMemberRoute(entMemberRoute parEntMemberRoute)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberRoute insDatMemberRoute = new datMemberRoute();
                insDatMemberRoute.InsertMemberRoute(parEntMemberRoute, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        public DataTable SelectPlannedVersusActualGraphDataByCustomerIdEndDate(int customerId, DateTime insDateTime)
        {
            DbConnector insDbConnector = new DbConnector();
            datMemberRoute insDatMemberRoute = new datMemberRoute();
            try
            {
                return insDatMemberRoute.SelectPlannedVersusActualGraphDataByCustomerIdEndDate(customerId, insDateTime, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public void CompleteMemberRouteWiselyFromJsonArray(dynamic memberRoute, dynamic memberRouteDetail, dynamic memberRoutePhoto, dynamic productExpiration)
        {
            DbConnector insDbConnector = new DbConnector();
            datMemberRoute insDatMemberRoute = new datMemberRoute();
            datMemberRouteDetail insDatMemberRouteDetail = new datMemberRouteDetail();
            datMemberRoutePhoto insDatMemberRoutePhoto = new datMemberRoutePhoto();
            datProductExpiration insDatProductExpiration = new datProductExpiration();
            try
            {
                insDbConnector.BeginTransaction();

                #region Get MemberRoute
                entMemberRoute insEntMemberRoute = new entMemberRoute();
                insEntMemberRoute.Id = Convert.ToInt32(memberRoute.Id);
                insDatMemberRoute.SelectMemberRouteById(insEntMemberRoute, insDbConnector);
                #endregion

                #region Loop each memberRouteDetail record and make entries
                foreach (var memberRouteDetailItem in memberRouteDetail)
                {
                    entMemberRouteDetail insEntMemberRouteDetail = new entMemberRouteDetail();
                    insEntMemberRouteDetail.Id = Convert.ToInt32(memberRouteDetailItem.Id);
                    insDatMemberRouteDetail.SelectMemberRouteDetailById(insEntMemberRouteDetail, insDbConnector);

                    insEntMemberRouteDetail.IsSentToServer = true;
                    insEntMemberRouteDetail.SentToServerOn = DateTime.Now;

                    if (memberRouteDetailItem.CreatedOn != null)
                        insEntMemberRouteDetail.CreatedOn = Convert.ToDateTime(memberRouteDetailItem.CreatedOn);
                    if (memberRouteDetailItem.FaceCount != null)
                        insEntMemberRouteDetail.FaceCount = Convert.ToInt32(memberRouteDetailItem.FaceCount);
                    if (memberRouteDetailItem.Notes != null)
                        insEntMemberRouteDetail.Notes = Convert.ToString(memberRouteDetailItem.Notes);
                    if (memberRouteDetailItem.PromotionTypeId != null)
                        insEntMemberRouteDetail.PromotionTypeId = Convert.ToInt32(memberRouteDetailItem.PromotionTypeId);
                    if (memberRouteDetailItem.ShelfIndex != null)
                        insEntMemberRouteDetail.ShelfIndex = Convert.ToInt32(memberRouteDetailItem.ShelfIndex);
                    if (memberRouteDetailItem.ShelfPrice != null)
                        insEntMemberRouteDetail.ShelfPrice = Convert.ToDecimal(memberRouteDetailItem.ShelfPrice);
                    if (memberRouteDetailItem.ShelfStock != null)
                        insEntMemberRouteDetail.ShelfStock = Convert.ToInt32(memberRouteDetailItem.ShelfStock);
                    if (memberRouteDetailItem.WarehouseStock != null)
                        insEntMemberRouteDetail.WarehouseStock = Convert.ToInt32(memberRouteDetailItem.WarehouseStock);
                    if (memberRouteDetailItem.ActivityTypeName != null)
                        insEntMemberRouteDetail.ActivityTypeName = Convert.ToString(memberRouteDetailItem.ActivityTypeName);
                    if (memberRouteDetailItem.DiscountedPrice != null)
                        insEntMemberRouteDetail.DiscountedPrice = Convert.ToDecimal(memberRouteDetailItem.DiscountedPrice);

                    insDatMemberRouteDetail.UpdateMemberRouteDetailById(insEntMemberRouteDetail, insDbConnector);
                }
                #endregion

                #region Save Photos
                foreach (var memberRoutePhotoItem in memberRoutePhoto)
                {
                    string fileName = Guid.NewGuid().ToString() + ".jpg";
                    string filePath = ConfigurationManager.AppSettings["ShopPhotosPhysicalPath"] + fileName;
                    string url = ConfigurationManager.AppSettings["ShopPhotosBaseURL"] + fileName;
                    entMemberRoutePhoto insEntMemberRoutePhoto = new entMemberRoutePhoto();
                    insEntMemberRoutePhoto.CreatedOn = Convert.ToDateTime(memberRoutePhotoItem.CreatedOn);
                    insEntMemberRoutePhoto.IsSentToServer = true;
                    insEntMemberRoutePhoto.MemberId = Convert.ToString(memberRoutePhotoItem.MemberId);
                    insEntMemberRoutePhoto.MemberRouteId = Convert.ToInt32(memberRoutePhotoItem.MemberRouteId);
                    insEntMemberRoutePhoto.ProfilePicturePath = url;
                    byte[] bytes = Convert.FromBase64String(Convert.ToString(memberRoutePhotoItem.ProfilePicturePath));
                    File.WriteAllBytes(filePath, bytes);
                    //UploadFtpFile(ConfigurationManager.AppSettings["ShopPhotosBaseURL"], fileName, bytes);
                    insDatMemberRoutePhoto.InsertMemberRoutePhoto(insEntMemberRoutePhoto, insDbConnector);
                }
                #endregion

                #region Update values for MemberRoute
                insEntMemberRoute.EntryCoordinateX = Convert.ToDecimal(memberRoute.EntryCoordinateX);
                insEntMemberRoute.EntryCoordinateY = Convert.ToDecimal(memberRoute.EntryCoordinateY);
                insEntMemberRoute.EntryOn = Convert.ToDateTime(memberRoute.EntryOn);
                insEntMemberRoute.ExitOn = Convert.ToDateTime(memberRoute.ExitOn);
                insEntMemberRoute.IsSentToServer = true;
                insEntMemberRoute.SentToServerOn = DateTime.Now;
                insEntMemberRoute.TotalTimeSpent = Convert.ToInt32((insEntMemberRoute.ExitOn.Value - insEntMemberRoute.EntryOn.Value).TotalMinutes);

                string fileName1 = Guid.NewGuid().ToString() + ".jpg";
                string filePath1 = ConfigurationManager.AppSettings["ShopPhotosPhysicalPath"] + fileName1;
                string url1 = ConfigurationManager.AppSettings["ShopPhotosBaseURL"] + fileName1;
                byte[] bytes1 = Convert.FromBase64String(Convert.ToString(memberRoute.EntryImageData));
                File.WriteAllBytes(filePath1, bytes1);
                //UploadFtpFile(ConfigurationManager.AppSettings["ShopPhotosBaseURL"], fileName1, bytes1);
                insDatMemberRoute.UpdateMemberRouteById(insEntMemberRoute, insDbConnector);
                #endregion

                if (productExpiration != null)
                {
                    foreach (var productExpirationItem in productExpiration)
                    {
                        entProductExpiration insEntProductExpiration = new entProductExpiration();
                        insEntProductExpiration.CreatedOn = DateTime.Now;
                        insEntProductExpiration.CustomerId = Convert.ToInt32(productExpirationItem.CustomerId);
                        insEntProductExpiration.CustomerProductId = Convert.ToInt32(productExpirationItem.CustomerProductId);
                        insEntProductExpiration.ExpirationDate = Convert.ToDateTime(productExpirationItem.ExpirationDate);
                        insEntProductExpiration.IsSentToServer = true;
                        insEntProductExpiration.MemberId = Convert.ToString(productExpirationItem.MemberId);
                        insEntProductExpiration.MemberRouteId = Convert.ToInt32(productExpirationItem.MemberRouteId);
                        insEntProductExpiration.Quantity = Convert.ToInt32(productExpirationItem.Quantity);
                        insEntProductExpiration.SentToServerOn = DateTime.Now;
                        insDatProductExpiration.InsertProductExpiration(insEntProductExpiration, insDbConnector);
                    }
                }

                insDbConnector.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                insDbConnector.RollbackTransaction();
            }
        }

        public void UploadFtpFile(string folderName, string fileName, byte[] file)
        {

            FtpWebRequest request;
            request = WebRequest.Create(new Uri("ftp://waws-prod-db3-203.ftp.azurewebsites.windows.net/site/wwwroot/Images/ShopPhotos/" + fileName)) as FtpWebRequest;
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.UseBinary = true;
            request.UsePassive = true;
            request.KeepAlive = true;
            request.Credentials = new NetworkCredential(@"MerchPlusWeb\$MerchPlusWeb", "KDDcbgtHgB2WEHLDxbkuehqjpzeEphlwfBku12PWwNGpGoCfKcCvai8YQj9T");

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(file, 0, file.Length);
            requestStream.Flush();
            requestStream.Close();
        }


        public void CompleteMemberRouteWiselyFromJsonArrayWithSalesOrder(dynamic memberRoute, dynamic memberRouteDetail, dynamic memberRoutePhoto, dynamic salesOrder, dynamic salesOrderDetail, dynamic productExpiration)
        {
            DbConnector insDbConnector = new DbConnector();
            datMemberRoute insDatMemberRoute = new datMemberRoute();
            datMemberRouteDetail insDatMemberRouteDetail = new datMemberRouteDetail();
            datMemberRoutePhoto insDatMemberRoutePhoto = new datMemberRoutePhoto();
            datSalesOrder insDatSalesOrder = new datSalesOrder();
            datSalesOrderDetail insDatSalesOrderDetail = new datSalesOrderDetail();
            datProductExpiration insDatProductExpiration = new datProductExpiration();
            try
            {
                insDbConnector.BeginTransaction();

                #region Get MemberRoute
                entMemberRoute insEntMemberRoute = new entMemberRoute();
                insEntMemberRoute.Id = Convert.ToInt32(memberRoute.Id);
                insDatMemberRoute.SelectMemberRouteById(insEntMemberRoute, insDbConnector);
                #endregion

                #region Loop each memberRouteDetail record and make entries
                foreach (var memberRouteDetailItem in memberRouteDetail)
                {
                    entMemberRouteDetail insEntMemberRouteDetail = new entMemberRouteDetail();
                    insEntMemberRouteDetail.Id = Convert.ToInt32(memberRouteDetailItem.Id);
                    insDatMemberRouteDetail.SelectMemberRouteDetailById(insEntMemberRouteDetail, insDbConnector);

                    insEntMemberRouteDetail.IsSentToServer = true;
                    insEntMemberRouteDetail.SentToServerOn = DateTime.Now;

                    if (memberRouteDetailItem.CreatedOn != null)
                        insEntMemberRouteDetail.CreatedOn = Convert.ToDateTime(memberRouteDetailItem.CreatedOn);
                    if (memberRouteDetailItem.FaceCount != null)
                        insEntMemberRouteDetail.FaceCount = Convert.ToInt32(memberRouteDetailItem.FaceCount);
                    if (memberRouteDetailItem.Notes != null)
                        insEntMemberRouteDetail.Notes = Convert.ToString(memberRouteDetailItem.Notes);
                    if (memberRouteDetailItem.PromotionTypeId != null)
                        insEntMemberRouteDetail.PromotionTypeId = Convert.ToInt32(memberRouteDetailItem.PromotionTypeId);
                    if (memberRouteDetailItem.ShelfIndex != null)
                        insEntMemberRouteDetail.ShelfIndex = Convert.ToInt32(memberRouteDetailItem.ShelfIndex);
                    if (memberRouteDetailItem.ShelfPrice != null)
                        insEntMemberRouteDetail.ShelfPrice = Convert.ToDecimal(memberRouteDetailItem.ShelfPrice);
                    if (memberRouteDetailItem.ShelfStock != null)
                        insEntMemberRouteDetail.ShelfStock = Convert.ToInt32(memberRouteDetailItem.ShelfStock);
                    if (memberRouteDetailItem.WarehouseStock != null)
                        insEntMemberRouteDetail.WarehouseStock = Convert.ToInt32(memberRouteDetailItem.WarehouseStock);
                    if (memberRouteDetailItem.ActivityTypeName != null)
                        insEntMemberRouteDetail.ActivityTypeName = Convert.ToString(memberRouteDetailItem.ActivityTypeName);
                    if (memberRouteDetailItem.DiscountedPrice != null)
                        insEntMemberRouteDetail.DiscountedPrice = Convert.ToDecimal(memberRouteDetailItem.DiscountedPrice);

                    insDatMemberRouteDetail.UpdateMemberRouteDetailById(insEntMemberRouteDetail, insDbConnector);
                }
                #endregion

                #region Save Photos
                foreach (var memberRoutePhotoItem in memberRoutePhoto)
                {
                    string fileName = Guid.NewGuid().ToString() + ".jpg";
                    string filePath = ConfigurationManager.AppSettings["ShopPhotosPhysicalPath"] + fileName;
                    string url = ConfigurationManager.AppSettings["ShopPhotosBaseURL"] + fileName;
                    entMemberRoutePhoto insEntMemberRoutePhoto = new entMemberRoutePhoto();
                    insEntMemberRoutePhoto.CreatedOn = Convert.ToDateTime(memberRoutePhotoItem.CreatedOn);
                    insEntMemberRoutePhoto.IsSentToServer = true;
                    insEntMemberRoutePhoto.MemberId = Convert.ToString(memberRoutePhotoItem.MemberId);
                    insEntMemberRoutePhoto.MemberRouteId = Convert.ToInt32(memberRoutePhotoItem.MemberRouteId);
                    insEntMemberRoutePhoto.ProfilePicturePath = url;
                    byte[] bytes = Convert.FromBase64String(Convert.ToString(memberRoutePhotoItem.ProfilePicturePath));
                    File.WriteAllBytes(filePath, bytes);
                    insDatMemberRoutePhoto.InsertMemberRoutePhoto(insEntMemberRoutePhoto, insDbConnector);
                }
                #endregion

                #region Save Sales Order
                if (salesOrder != null)
                {
                    entSalesOrder insEntSalesOrder = new entSalesOrder();
                    insEntSalesOrder.CreatedBy = Convert.ToString(salesOrder[0].CreatedBy);
                    insEntSalesOrder.CreatedOn = Convert.ToDateTime(salesOrder[0].CreatedOn);
                    insEntSalesOrder.CustomerId = Convert.ToInt32(salesOrder[0].CustomerId);
                    insEntSalesOrder.MemberRouteId = Convert.ToInt32(insEntMemberRoute.Id);
                    insEntSalesOrder.SalesOrderStatusId = Convert.ToInt32(salesOrder[0].SalesOrderStatusId);
                    insDatSalesOrder.InsertSalesOrder(insEntSalesOrder, insDbConnector);

                    #region Sales Order Details
                    foreach (var parEntSalesOrderDetail in salesOrderDetail)
                    {
                        entSalesOrderDetail insEntSalesOrderDetail = new entSalesOrderDetail();
                        insEntSalesOrderDetail.CreatedBy = insEntSalesOrder.CreatedBy;
                        insEntSalesOrderDetail.CreatedOn = insEntSalesOrderDetail.CreatedOn;
                        insEntSalesOrderDetail.CustomerProductId = Convert.ToInt32(parEntSalesOrderDetail.CustomerProductId);
                        insEntSalesOrderDetail.Description = string.Empty;
                        insEntSalesOrderDetail.Quantity = Convert.ToInt32(parEntSalesOrderDetail.Quantity);
                        insEntSalesOrderDetail.SalesOrderId = insEntSalesOrder.Id;
                        insEntSalesOrderDetail.SalesOrderPackageTypeId = Convert.ToInt32(parEntSalesOrderDetail.SalesOrderPackageTypeId);
                        insDatSalesOrderDetail.InsertSalesOrderDetail(insEntSalesOrderDetail, insDbConnector);
                    }
                    #endregion
                }
                #endregion

                #region Update values for MemberRoute
                insEntMemberRoute.EntryCoordinateX = Convert.ToDecimal(memberRoute.EntryCoordinateX);
                insEntMemberRoute.EntryCoordinateY = Convert.ToDecimal(memberRoute.EntryCoordinateY);
                insEntMemberRoute.EntryOn = Convert.ToDateTime(memberRoute.EntryOn);
                insEntMemberRoute.ExitOn = Convert.ToDateTime(memberRoute.ExitOn);
                insEntMemberRoute.IsSentToServer = true;
                insEntMemberRoute.SentToServerOn = DateTime.Now;
                insEntMemberRoute.TotalTimeSpent = Convert.ToInt32((insEntMemberRoute.ExitOn.Value - insEntMemberRoute.EntryOn.Value).TotalMinutes);

                string fileName1 = Guid.NewGuid().ToString() + ".jpg";
                string filePath1 = ConfigurationManager.AppSettings["ShopPhotosPhysicalPath"] + fileName1;
                string url1 = ConfigurationManager.AppSettings["ShopPhotosBaseURL"] + fileName1;
                byte[] bytes1 = Convert.FromBase64String(Convert.ToString(memberRoute.EntryImageData));
                File.WriteAllBytes(filePath1, bytes1);

                insDatMemberRoute.UpdateMemberRouteById(insEntMemberRoute, insDbConnector);
                #endregion

                if (productExpiration != null)
                {
                    foreach (var productExpirationItem in productExpiration)
                    {
                        entProductExpiration insEntProductExpiration = new entProductExpiration();
                        insEntProductExpiration.CreatedOn = DateTime.Now;
                        insEntProductExpiration.CustomerId = Convert.ToInt32(productExpirationItem.CustomerId);
                        insEntProductExpiration.CustomerProductId = Convert.ToInt32(productExpirationItem.CustomerProductId);
                        insEntProductExpiration.ExpirationDate = Convert.ToDateTime(productExpirationItem.ExpirationDate);
                        insEntProductExpiration.IsSentToServer = true;
                        insEntProductExpiration.MemberId = Convert.ToString(productExpirationItem.MemberId);
                        insEntProductExpiration.MemberRouteId = Convert.ToInt32(productExpirationItem.MemberRouteId);
                        insEntProductExpiration.Quantity = Convert.ToInt32(productExpirationItem.Quantity);
                        insEntProductExpiration.SentToServerOn = DateTime.Now;
                        insDatProductExpiration.InsertProductExpiration(insEntProductExpiration, insDbConnector);
                    }
                }

                insDbConnector.CommitTransaction();
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                insDbConnector.RollbackTransaction();
            }
        }

        /// <summary>
        /// Updates one row of data in table [MemberRoute] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntMemberRoute">Gets entity object as parameter for table MemberRoute]</param>
        public void UpdateMemberRouteById(entMemberRoute parEntMemberRoute)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberRoute insDatMemberRoute = new datMemberRoute();
                insDatMemberRoute.UpdateMemberRouteById(parEntMemberRoute, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes all data stored in table [MemberRoute]
        /// </summary>
        public void DeleteMemberRoute()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberRoute insDatMemberRoute = new datMemberRoute();
                insDatMemberRoute.DeleteMemberRoute(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes data stored in table [MemberRoute] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntMemberRoute">Gets entity object as parameter for table MemberRoute]</param>
        public void DeleteMemberRouteById(entMemberRoute parEntMemberRoute)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberRoute insDatMemberRoute = new datMemberRoute();
                insDatMemberRoute.DeleteMemberRouteById(parEntMemberRoute, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        #endregion
        #region Custom Methods
        public DataTable SelectMemberRouteByMemberIdEffectiveDate(entMemberRoute insEntMemberRoute)
        {
            DbConnector insDbConnector = new DbConnector();
            datMemberRoute insDatMemberRoute = new datMemberRoute();
            try
            {
                return insDatMemberRoute.SelectMemberRouteByMemberIdEffectiveDate(insEntMemberRoute, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public DataTable SelectMemberRouteByEffectiveDate(entMemberRoute insEntMemberRoute)
        {
            DbConnector insDbConnector = new DbConnector();
            datMemberRoute insDatMemberRoute = new datMemberRoute();
            try
            {
                return insDatMemberRoute.SelectMemberRouteByEffectiveDate(insEntMemberRoute, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }
        public DataTable SelectMemberRouteDetailByMemberIdEffectiveDate(entMemberRoute insEntMemberRoute)
        {
            DbConnector insDbConnector = new DbConnector();
            datMemberRoute insDatMemberRoute = new datMemberRoute();
            try
            {
                return insDatMemberRoute.SelectMemberRouteDetailByMemberIdEffectiveDate(insEntMemberRoute, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public DataTable SelectDashboardWidgetValuesByCustomerIdCurrentDate(int customerId, DateTime insDateTime)
        {
            DbConnector insDbConnector = new DbConnector();
            datMemberRoute insDatMemberRoute = new datMemberRoute();
            try
            {
                return insDatMemberRoute.SelectDashboardWidgetValuesByCustomerIdCurrentDate(customerId, insDateTime, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public DataTable GetMapDataByCustomerIdCurrentDate(int customerId, DateTime insDateTime)
        {
            DbConnector insDbConnector = new DbConnector();
            datMemberRoute insDatMemberRoute = new datMemberRoute();
            try
            {
                return insDatMemberRoute.GetMapDataByCustomerIdCurrentDate(customerId, insDateTime, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public DataTable SelectDistinctMemberIdFromMemberRouteByEffectiveDateCustomerIdCityName(entMemberRoute insEntMemberRoute, string cityName)
        {
            DbConnector insDbConnector = new DbConnector();
            datMemberRoute insDatMemberRoute = new datMemberRoute();
            try
            {
                return insDatMemberRoute.SelectDistinctMemberIdFromMemberRouteByEffectiveDateCustomerIdCityName(insEntMemberRoute, cityName, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }

        }

        public DataTable SelectMemberRouteReportByMemberIdEffectiveDate(string memberId, string customerId, DateTime dateTime)
        {
            DbConnector insDbConnector = new DbConnector();
            datMemberRoute insDatMemberRoute = new datMemberRoute();
            try
            {
                return insDatMemberRoute.SelectMemberRouteReportByMemberIdEffectiveDate(memberId, dateTime, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public DataTable SelectSalesOrderGraphDataByCustomerIdEndDate(int customerId, DateTime insDateTime)
        {
            DbConnector insDbConnector = new DbConnector();
            datMemberRoute insDatMemberRoute = new datMemberRoute();
            try
            {
                return insDatMemberRoute.SelectSalesOrderGraphDataByCustomerIdEndDate(customerId, insDateTime, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public DataTable SelectMemberRouteByMemberIdSchedulerView(entMemberRoute insEntMemberRoute)
        {
            DbConnector insDbConnector = new DbConnector();
            datMemberRoute insDatMemberRoute = new datMemberRoute();
            try
            {
                DataTable insDt = new DataTable();
                insDt = insDatMemberRoute.SelectMemberRouteByMemberIdSchedulerView(insEntMemberRoute, insDbConnector);
                insDt.Columns.Add("original", typeof(DateTime));
                insDt.Columns.Add("end", typeof(DateTime));
                insDt.Columns.Add("allDay", typeof(bool));
                insDt.AcceptChanges();
                foreach (DataRow insDr in insDt.Rows)
                {
                    insDr["original"] = Convert.ToDateTime(insDr["start"]);

                    insDr["start"] = Convert.ToDateTime(insDr["start"]).AddHours(Convert.ToInt32(insDr["OrderIndex"]) - 1 + 8);
                    insDr["allDay"] = false;
                    insDr["end"] = Convert.ToDateTime(insDr["start"]).AddMinutes(52).AddSeconds(0);
                }
                insDt.AcceptChanges();
                insDt.Columns.Remove("rank");
                insDt.AcceptChanges();

                #region Create Add Button
                DateTime insDateTime = new DateTime();
                DataTable insDt_New = new DataTable();
                insDt_New = insDt.Clone();
                foreach (DataRow insDr in insDt.Rows)
                {
                    if (Convert.ToDateTime(insDr["original"]) != insDateTime)
                    {
                        insDateTime = Convert.ToDateTime(insDr["original"]);
                        DataRow insDr_New = insDt_New.NewRow();
                        insDr_New["id"] = -1;
                        insDr_New["title"] = string.Empty;
                        insDr_New["start"] = Convert.ToDateTime(insDr["original"]).AddHours(7);
                        insDr_New["end"] = Convert.ToDateTime(insDr["original"]).AddHours(7).AddMinutes(59);
                        insDr_New["allday"] = false;
                        insDr_New["MemberId"] = Convert.ToString(insDr["MemberId"]);
                        insDt_New.Rows.Add(insDr_New);
                    }
                }
                #endregion

                insDt.Merge(insDt_New);

                DateTime insDateNow = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                DateTime insDateEnd = new DateTime();
                insDateEnd = insDateNow.AddDays(30);

                for (DateTime date = insDateNow.AddDays(1); date.Date <= insDateEnd.Date; date = date.AddDays(1))
                {
                    DataRow[] insDrFound = insDt.Select("original='" + date.ToString("yyyy-MM-dd") + "'");
                    if (insDrFound.Length == 0)
                    {
                        DataRow insDr_New = insDt.NewRow();
                        insDr_New["id"] = -1;
                        insDr_New["title"] = string.Empty;
                        insDr_New["start"] = Convert.ToDateTime(date).AddHours(7);
                        insDr_New["end"] = Convert.ToDateTime(date).AddHours(7).AddMinutes(59);
                        insDr_New["allday"] = false;
                        insDr_New["MemberId"] = Convert.ToString(insEntMemberRoute.MemberId);
                        insDt.Rows.Add(insDr_New);
                    }
                }
                insDt.AcceptChanges();

                return insDt;
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public void InsertMemberRouteWiselyFromCalendar(string memberId, int orderIndex, DateTime startDate, int retailShopId, int frequecyId, int customerId)
        {
            DbConnector insDbConnector = new DbConnector();
            datMember insDatMember = new datMember();
            datMemberRoute insDatMemberRoute = new datMemberRoute();
            datRetailShop insDatRetailShop = new datRetailShop();
            try
            {
                insDbConnector.BeginTransaction();

                #region Get Member
                entMember insEntMember = new entMember();
                insEntMember.Id = memberId;
                insDatMember.SelectMemberById(insEntMember, insDbConnector);
                #endregion

                #region Get RetailShop
                entRetailShop insEntRetailShop = new entRetailShop();
                insEntRetailShop.Id = retailShopId;
                insDatRetailShop.SelectRetailShopById(insEntRetailShop, insDbConnector);
                #endregion

                DateTime endDate = new DateTime();
                endDate = startDate.AddYears(1);

                DateTime cursorCurrentDate = new DateTime();
                cursorCurrentDate = startDate;

                while (cursorCurrentDate <= endDate)
                {
                    entMemberRoute insEntMemberRoute = new entMemberRoute();
                    insEntMemberRoute.CustomerId = customerId;
                    insEntMemberRoute.EffectiveDate = cursorCurrentDate;
                    insEntMemberRoute.MemberId = memberId;
                    insEntMemberRoute.OrderIndex = orderIndex;
                    insEntMemberRoute.RetailShopId = retailShopId;
                    insDatMemberRoute.InsertMemberRoute(insEntMemberRoute, insDbConnector);

                    if (frequecyId == 0)
                        break;
                    else if (frequecyId == 1)
                        cursorCurrentDate = cursorCurrentDate.AddDays(7);
                    else if (frequecyId == 2)
                        cursorCurrentDate = cursorCurrentDate.AddDays(14);
                    else if (frequecyId == 3)
                        cursorCurrentDate = cursorCurrentDate.AddDays(21);
                    else if (frequecyId == 4)
                        cursorCurrentDate = cursorCurrentDate.AddDays(28);

                }

                insDbConnector.CommitTransaction();
            }
            catch (Exception ex)
            {
                insDbConnector.RollbackTransaction();
                this.ErrorMessage = ex.ToString();
            }
        }

        public void DeleteMemberRouteByIdAllVisits(entMemberRoute insEntMemberRoute)
        {
            DbConnector insDbConnector = new DbConnector();
            datMemberRoute insDatMemberRoute = new datMemberRoute();
            try
            {
                insDbConnector.BeginTransaction();

                insDatMemberRoute.SelectMemberRouteById(insEntMemberRoute, insDbConnector);

                insDatMemberRoute.DeleteMemberRouteByEffectiveDateMoreThanRetailshopIdMemberId(insEntMemberRoute, insDbConnector);

                insDbConnector.CommitTransaction();
            }
            catch (Exception ex)
            {
                insDbConnector.RollbackTransaction();
                this.ErrorMessage = ex.ToString();
            }
        }

        public void DragAndDropHelper(entMemberRoute insEntMemberRoute, DateTime insNewDate)
        {
            DbConnector insDbConnector = new DbConnector();
            datMemberRoute insDatMemberRoute = new datMemberRoute();
            try
            {
                insDbConnector.BeginTransaction();

                int eventOrderIndex = Convert.ToInt32(insEntMemberRoute.OrderIndex);
                string memberId = Convert.ToString(insEntMemberRoute.MemberId);
                DateTime insDateTimeOriginal = Convert.ToDateTime(insEntMemberRoute.EffectiveDate);
                int retailShopId = Convert.ToInt32(insEntMemberRoute.RetailShopId);
                int customerId = Convert.ToInt32(insEntMemberRoute.CustomerId);

                #region Existing Records for this visit on this particular day of week
                entMemberRoute insEntMemberRoute_Delete = new entMemberRoute();
                insEntMemberRoute_Delete.EffectiveDate = insDateTimeOriginal;
                insEntMemberRoute_Delete.MemberId = memberId;
                insEntMemberRoute_Delete.RetailShopId = retailShopId;
                insDatMemberRoute.DeleteMemberRouteByEffectiveDateMoreThanRetailShopIdMemberIdOnlyOriginalWeekday(insEntMemberRoute_Delete, insDbConnector);
                #endregion

                #region Loop Each date and make entry
                DateTime insDateStop = insNewDate.AddYears(1);
                while (insNewDate <= insDateStop)
                {
                    entMemberRoute insEntMemberRoute_New = new entMemberRoute();
                    insEntMemberRoute_New.CustomerId = customerId;
                    insEntMemberRoute_New.EffectiveDate = new DateTime(insNewDate.Year, insNewDate.Month, insNewDate.Day);
                    insEntMemberRoute_New.MemberId = memberId;
                    if (insNewDate.Hour == 8)
                        insEntMemberRoute_New.OrderIndex = 1;
                    else if (insNewDate.Hour == 9)
                        insEntMemberRoute_New.OrderIndex = 2;
                    else if (insNewDate.Hour == 10)
                        insEntMemberRoute_New.OrderIndex = 3;
                    else if (insNewDate.Hour == 11)
                        insEntMemberRoute_New.OrderIndex = 4;
                    else if (insNewDate.Hour == 12)
                        insEntMemberRoute_New.OrderIndex = 5;
                    else if (insNewDate.Hour == 13)
                        insEntMemberRoute_New.OrderIndex = 6;
                    else if (insNewDate.Hour == 14)
                        insEntMemberRoute_New.OrderIndex = 7;
                    else if (insNewDate.Hour == 15)
                        insEntMemberRoute_New.OrderIndex = 8;
                    else if (insNewDate.Hour == 16)
                        insEntMemberRoute_New.OrderIndex = 9;
                    else if (insNewDate.Hour == 17)
                        insEntMemberRoute_New.OrderIndex = 10;
                    else if (insNewDate.Hour == 18)
                        insEntMemberRoute_New.OrderIndex = 11;
                    else if (insNewDate.Hour == 19)
                        insEntMemberRoute_New.OrderIndex = 12;
                    else if (insNewDate.Hour == 20)
                        insEntMemberRoute_New.OrderIndex = 13;
                    else if (insNewDate.Hour == 21)
                        insEntMemberRoute_New.OrderIndex = 14;

                    insEntMemberRoute_New.RetailShopId = retailShopId;
                    insDatMemberRoute.InsertMemberRoute(insEntMemberRoute_New, insDbConnector);

                    insNewDate = insNewDate.AddDays(7);
                }
                #endregion


                insDbConnector.CommitTransaction();
            }
            catch (Exception ex)
            {
                insDbConnector.RollbackTransaction();
                this.ErrorMessage = ex.ToString();
            }

        }

        public DataTable SelectMemberRouteByCustomerIdStartDateEndDate(int customerId, DateTime insStartDate, DateTime insEndDate)
        {
            DbConnector insDbConnector = new DbConnector();
            datMemberRoute insDatMemberRoute = new datMemberRoute();
            try
            {
                return insDatMemberRoute.SelectMemberRouteByCustomerIdStartDateEndDate(customerId, insStartDate, insEndDate, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }
        #endregion
    }
}
