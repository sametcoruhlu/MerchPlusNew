using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using SqlHelper;
using System.Data;
using entMerchPlus;
using datMerchPlus;
using Infrastructure;

namespace busMerchPlus
{
    /// <summary>
    /// This layer is responsible for implementing business logic.
    /// Manages transactions on table [MemberRoutePhoto]
    /// </summary>
    public class busMemberRoutePhoto : BusBase
    {
        /// <summary>
        /// busMemberRoutePhotoConstructor method used while taking an instance of this class.
        /// </summary>
        public busMemberRoutePhoto() : base()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Selects all data stored in table [MemberRoutePhoto]
        /// </summary>
        public DataTable SelectMemberRoutePhoto()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberRoutePhoto insDatMemberRoutePhoto = new datMemberRoutePhoto();
                return insDatMemberRoutePhoto.SelectMemberRoutePhoto(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Selects one row of data stored in table [MemberRoutePhoto] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntMemberRoutePhoto">Gets entity object as parameter for table MemberRoutePhoto]</param>
        public void SelectMemberRoutePhotoById(entMemberRoutePhoto parEntMemberRoutePhoto)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberRoutePhoto insDatMemberRoutePhoto = new datMemberRoutePhoto();
                insDatMemberRoutePhoto.SelectMemberRoutePhotoById(parEntMemberRoutePhoto, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Inserts one row of data into table [MemberRoutePhoto] based on the values of input entity object
        /// </summary>
        /// <param name="parEntMemberRoutePhoto">Gets entity object as parameter for table MemberRoutePhoto]</param>
        public void InsertMemberRoutePhoto(entMemberRoutePhoto parEntMemberRoutePhoto)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberRoutePhoto insDatMemberRoutePhoto = new datMemberRoutePhoto();
                insDatMemberRoutePhoto.InsertMemberRoutePhoto(parEntMemberRoutePhoto, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Updates one row of data in table [MemberRoutePhoto] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntMemberRoutePhoto">Gets entity object as parameter for table MemberRoutePhoto]</param>
        public void UpdateMemberRoutePhotoById(entMemberRoutePhoto parEntMemberRoutePhoto)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberRoutePhoto insDatMemberRoutePhoto = new datMemberRoutePhoto();
                insDatMemberRoutePhoto.UpdateMemberRoutePhotoById(parEntMemberRoutePhoto, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes all data stored in table [MemberRoutePhoto]
        /// </summary>
        public void DeleteMemberRoutePhoto()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberRoutePhoto insDatMemberRoutePhoto = new datMemberRoutePhoto();
                insDatMemberRoutePhoto.DeleteMemberRoutePhoto(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes data stored in table [MemberRoutePhoto] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntMemberRoutePhoto">Gets entity object as parameter for table MemberRoutePhoto]</param>
        public void DeleteMemberRoutePhotoById(entMemberRoutePhoto parEntMemberRoutePhoto)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberRoutePhoto insDatMemberRoutePhoto = new datMemberRoutePhoto();
                insDatMemberRoutePhoto.DeleteMemberRoutePhotoById(parEntMemberRoutePhoto, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        #endregion
        #region Custom Methods
        public DataTable SelectMemberRoutePhotoByMemberRouteId(Int32 memberRouteId)
        {
            DbConnector insDbConnector = new DbConnector();
            datMemberRoutePhoto insDatMemberRoutePhoto = new datMemberRoutePhoto();
            try
            {
                entMemberRoutePhoto insEntMemberRoutePhoto = new entMemberRoutePhoto();
                insEntMemberRoutePhoto.MemberRouteId = memberRouteId;
                return insDatMemberRoutePhoto.SelectMemberRoutePhotoByMemberRouteId(insEntMemberRoutePhoto, insDbConnector);
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
