using System;
using System.Collections.Generic;
using System.Text;
using SqlHelper;
using System.Data;
using entMerchPlus;

namespace datMerchPlus
{
    /// <summary>
    /// The class that is generated hold parameters to manage database table [MemberRoutePhoto]
    /// </summary>
    public class datMemberRoutePhoto
    {
        /// <summary>
        /// datMemberRoutePhoto Constructor method used while taking an instance of this class.
        /// </summary>
        public datMemberRoutePhoto()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Method that selects all data stored in table [MemberRoutePhoto]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public DataTable SelectMemberRoutePhoto(DbConnector parDbConnector)
        {
            return parDbConnector.ExecuteDataTable("SelectMemberRoutePhoto", null);
        }

        /// <summary>
        /// Method that selects one row of data in table [MemberRoutePhoto] filtered by the PK field of the inbound entity object
        /// </summary>
        /// <param name="parEntMemberRoutePhoto">Entity object as parameter for tableMemberRoutePhoto]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void SelectMemberRoutePhotoById(entMemberRoutePhoto parEntMemberRoutePhoto, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntMemberRoutePhoto.Id);
            DataTable insDataTable = new DataTable();
            insDataTable = parDbConnector.ExecuteDataTable("SelectMemberRoutePhotoById", insDbParamCollection);
            if (insDataTable.Rows.Count > 0)
            {
                if (insDataTable.Rows[0]["Id"] != DBNull.Value)
                {
                    parEntMemberRoutePhoto.Id = Convert.ToInt32(insDataTable.Rows[0]["Id"]);
                }
                if (insDataTable.Rows[0]["MemberId"] != DBNull.Value)
                {
                    parEntMemberRoutePhoto.MemberId = Convert.ToString(insDataTable.Rows[0]["MemberId"]);
                }
                if (insDataTable.Rows[0]["MemberRouteId"] != DBNull.Value)
                {
                    parEntMemberRoutePhoto.MemberRouteId = Convert.ToInt32(insDataTable.Rows[0]["MemberRouteId"]);
                }
                if (insDataTable.Rows[0]["ProfilePicturePath"] != DBNull.Value)
                {
                    parEntMemberRoutePhoto.ProfilePicturePath = Convert.ToString(insDataTable.Rows[0]["ProfilePicturePath"]);
                }
                if (insDataTable.Rows[0]["IsSentToServer"] != DBNull.Value)
                {
                    parEntMemberRoutePhoto.IsSentToServer = Convert.ToBoolean(insDataTable.Rows[0]["IsSentToServer"]);
                }
                if (insDataTable.Rows[0]["CreatedOn"] != DBNull.Value)
                {
                    parEntMemberRoutePhoto.CreatedOn = Convert.ToDateTime(insDataTable.Rows[0]["CreatedOn"]);
                }
            }
        }

        /// <summary>
        /// Method that inserts one row of data into table [MemberRoutePhoto] based on the values of input entity object
        /// </summary>
        /// <param name="parEntMemberRoutePhoto">Entity object as parameter for tableMemberRoutePhoto]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void InsertMemberRoutePhoto(entMemberRoutePhoto parEntMemberRoutePhoto, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.AddOutput("@pId", DbType.Int32);
            insDbParamCollection.Add("@pMemberId", parEntMemberRoutePhoto.MemberId);
            insDbParamCollection.Add("@pMemberRouteId", parEntMemberRoutePhoto.MemberRouteId);
            insDbParamCollection.Add("@pProfilePicturePath", parEntMemberRoutePhoto.ProfilePicturePath);
            insDbParamCollection.Add("@pIsSentToServer", parEntMemberRoutePhoto.IsSentToServer);
            insDbParamCollection.Add("@pCreatedOn", parEntMemberRoutePhoto.CreatedOn);
            parDbConnector.ExecuteNonQuery("InsertMemberRoutePhoto", insDbParamCollection);
            parEntMemberRoutePhoto.Id = Convert.ToInt32(insDbParamCollection.GetOutPutParameter().Value);
        }

        /// <summary>
        /// Method that updates one row of data in table [MemberRoutePhoto] based on the values of input entity object
        /// </summary>
        /// <param name="parEntMemberRoutePhoto">Entity object as parameter for tableMemberRoutePhoto]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void UpdateMemberRoutePhotoById(entMemberRoutePhoto parEntMemberRoutePhoto, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntMemberRoutePhoto.Id);
            insDbParamCollection.Add("@pMemberId", parEntMemberRoutePhoto.MemberId);
            insDbParamCollection.Add("@pMemberRouteId", parEntMemberRoutePhoto.MemberRouteId);
            insDbParamCollection.Add("@pProfilePicturePath", parEntMemberRoutePhoto.ProfilePicturePath);
            insDbParamCollection.Add("@pIsSentToServer", parEntMemberRoutePhoto.IsSentToServer);
            insDbParamCollection.Add("@pCreatedOn", parEntMemberRoutePhoto.CreatedOn);
            parDbConnector.ExecuteNonQuery("UpdateMemberRoutePhotoById", insDbParamCollection);
        }

        /// <summary>
        /// Method that deletes all data in table [MemberRoutePhoto]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteMemberRoutePhoto(DbConnector parDbConnector)
        {
            parDbConnector.ExecuteNonQuery("DeleteMemberRoutePhoto", null);
        }

        /// <summary>
        /// Method that deletes one row of data in table [MemberRoutePhoto] based on the passed entity object
        /// </summary>
        /// <param name="parEntMemberRoutePhoto">Gets entity object as parameter for table MemberRoutePhoto]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteMemberRoutePhotoById(entMemberRoutePhoto parEntMemberRoutePhoto, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntMemberRoutePhoto.Id);
            parDbConnector.ExecuteNonQuery("DeleteMemberRoutePhotoById", insDbParamCollection);
        }

        #endregion
        #region Custom Methods
        public DataTable SelectMemberRoutePhotoByMemberRouteId(entMemberRoutePhoto insEntMemberRoutePhoto, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pMemberRouteId", insEntMemberRoutePhoto.MemberRouteId);
            return insDbConnector.ExecuteDataTable("SelectMemberRoutePhotoByMemberRouteId", insDbParamCollection);
        }
        #endregion
    }
}
