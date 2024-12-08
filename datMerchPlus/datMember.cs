using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using entMerchPlus;
using SqlHelper;

namespace datMerchPlus
{
    /// <summary>
	/// The class that is generated hold parameters to manage database table [Member]
	/// </summary>
	public class datMember
    {
        /// <summary>
        /// datMember Constructor method used while taking an instance of this class.
        /// </summary>
        public datMember()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]


        /// <summary>
        /// Method that selects one row of data in table [Member] filtered by the PK field of the inbound entity object
        /// </summary>
        /// <param name="parEntMember">Entity object as parameter for tableMember]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void SelectMemberById(entMember parEntMember, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntMember.Id);
            DataTable insDataTable = new DataTable();
            insDataTable = parDbConnector.ExecuteDataTable("SelectMemberById", insDbParamCollection);
            if (insDataTable.Rows.Count > 0)
            {
                if (insDataTable.Rows[0]["Id"] != DBNull.Value)
                {
                    parEntMember.Id = Convert.ToString(insDataTable.Rows[0]["Id"]);
                }
                if (insDataTable.Rows[0]["Email"] != DBNull.Value)
                {
                    parEntMember.Email = Convert.ToString(insDataTable.Rows[0]["Email"]);
                }
                if (insDataTable.Rows[0]["PhoneNumber"] != DBNull.Value)
                {
                    parEntMember.PhoneNumber = Convert.ToString(insDataTable.Rows[0]["PhoneNumber"]);
                }
                if (insDataTable.Rows[0]["UserName"] != DBNull.Value)
                {
                    parEntMember.UserName = Convert.ToString(insDataTable.Rows[0]["UserName"]);
                }
                if (insDataTable.Rows[0]["NameSurname"] != DBNull.Value)
                {
                    parEntMember.NameSurname = Convert.ToString(insDataTable.Rows[0]["NameSurname"]);
                }
                if (insDataTable.Rows[0]["SocialSecurityNumber"] != DBNull.Value)
                {
                    parEntMember.SocialSecurityNumber = Convert.ToString(insDataTable.Rows[0]["SocialSecurityNumber"]);
                }
                if (insDataTable.Rows[0]["DataNumber"] != DBNull.Value)
                {
                    parEntMember.DataNumber = Convert.ToString(insDataTable.Rows[0]["DataNumber"]);
                }
                if (insDataTable.Rows[0]["HiringDate"] != DBNull.Value)
                {
                    parEntMember.HiringDate = Convert.ToDateTime(insDataTable.Rows[0]["HiringDate"]);
                }
                if (insDataTable.Rows[0]["LeavingDate"] != DBNull.Value)
                {
                    parEntMember.LeavingDate = Convert.ToDateTime(insDataTable.Rows[0]["LeavingDate"]);
                }
                if (insDataTable.Rows[0]["IsActive"] != DBNull.Value)
                {
                    parEntMember.IsActive = Convert.ToBoolean(insDataTable.Rows[0]["IsActive"]);
                }
                if (insDataTable.Rows[0]["Address"] != DBNull.Value)
                {
                    parEntMember.Address = Convert.ToString(insDataTable.Rows[0]["Address"]);
                }
                if (insDataTable.Rows[0]["DeviceModelId"] != DBNull.Value)
                {
                    parEntMember.DeviceModelId = Convert.ToInt32(insDataTable.Rows[0]["DeviceModelId"]);
                }
                if (insDataTable.Rows[0]["IMEINumber"] != DBNull.Value)
                {
                    parEntMember.IMEINumber = Convert.ToString(insDataTable.Rows[0]["IMEINumber"]);
                }
                if (insDataTable.Rows[0]["ProfilePicturePath"] != DBNull.Value)
                {
                    parEntMember.ProfilePicturePath = Convert.ToString(insDataTable.Rows[0]["ProfilePicturePath"]);
                }
                if (insDataTable.Rows[0]["MemberTitleId"] != DBNull.Value)
                {
                    parEntMember.MemberTitleId = Convert.ToInt32(insDataTable.Rows[0]["MemberTitleId"]);
                }
                if (insDataTable.Rows[0]["AddressCityId"] != DBNull.Value)
                {
                    parEntMember.AddressCityId = Convert.ToInt32(insDataTable.Rows[0]["AddressCityId"]);
                }
                if (insDataTable.Rows[0]["AddressCoordinateX"] != DBNull.Value)
                {
                    parEntMember.AddressCoordinateX = Convert.ToDecimal(insDataTable.Rows[0]["AddressCoordinateX"]);
                }
                if (insDataTable.Rows[0]["AddressCoordinateY"] != DBNull.Value)
                {
                    parEntMember.AddressCoordinateY = Convert.ToDecimal(insDataTable.Rows[0]["AddressCoordinateY"]);
                }
                if (insDataTable.Rows[0]["DirectReportId"] != DBNull.Value)
                {
                    parEntMember.DirectReportId = Convert.ToString(insDataTable.Rows[0]["DirectReportId"]);
                }
            }
        }

        public DataTable SelectMember(DbConnector insDbConnector)
        {
            return insDbConnector.ExecuteDataTable("SelectMember", null);
        }

        public DataTable SelectMemberByCustomerIdGridData(entMemberCustomer insEntMemberCustomer, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pCustomerId", insEntMemberCustomer.CustomerId);
            return insDbConnector.ExecuteDataTable("SelectMemberByCustomerIdGridData", insDbParamCollection);
        }

        public void SelectMemberByUserName(entMember parEntMember, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pUserName", parEntMember.UserName);
            DataTable insDataTable = new DataTable();
            insDataTable = parDbConnector.ExecuteDataTable("SelectMemberByUserName", insDbParamCollection);
            if (insDataTable.Rows.Count > 0)
            {
                if (insDataTable.Rows[0]["Id"] != DBNull.Value)
                {
                    parEntMember.Id = Convert.ToString(insDataTable.Rows[0]["Id"]);
                }
                if (insDataTable.Rows[0]["Email"] != DBNull.Value)
                {
                    parEntMember.Email = Convert.ToString(insDataTable.Rows[0]["Email"]);
                }
                if (insDataTable.Rows[0]["PhoneNumber"] != DBNull.Value)
                {
                    parEntMember.PhoneNumber = Convert.ToString(insDataTable.Rows[0]["PhoneNumber"]);
                }
                if (insDataTable.Rows[0]["UserName"] != DBNull.Value)
                {
                    parEntMember.UserName = Convert.ToString(insDataTable.Rows[0]["UserName"]);
                }
                if (insDataTable.Rows[0]["NameSurname"] != DBNull.Value)
                {
                    parEntMember.NameSurname = Convert.ToString(insDataTable.Rows[0]["NameSurname"]);
                }
                if (insDataTable.Rows[0]["SocialSecurityNumber"] != DBNull.Value)
                {
                    parEntMember.SocialSecurityNumber = Convert.ToString(insDataTable.Rows[0]["SocialSecurityNumber"]);
                }
                if (insDataTable.Rows[0]["DataNumber"] != DBNull.Value)
                {
                    parEntMember.DataNumber = Convert.ToString(insDataTable.Rows[0]["DataNumber"]);
                }
                if (insDataTable.Rows[0]["HiringDate"] != DBNull.Value)
                {
                    parEntMember.HiringDate = Convert.ToDateTime(insDataTable.Rows[0]["HiringDate"]);
                }
                if (insDataTable.Rows[0]["LeavingDate"] != DBNull.Value)
                {
                    parEntMember.LeavingDate = Convert.ToDateTime(insDataTable.Rows[0]["LeavingDate"]);
                }
                if (insDataTable.Rows[0]["IsActive"] != DBNull.Value)
                {
                    parEntMember.IsActive = Convert.ToBoolean(insDataTable.Rows[0]["IsActive"]);
                }
                if (insDataTable.Rows[0]["Address"] != DBNull.Value)
                {
                    parEntMember.Address = Convert.ToString(insDataTable.Rows[0]["Address"]);
                }
                if (insDataTable.Rows[0]["DeviceModelId"] != DBNull.Value)
                {
                    parEntMember.DeviceModelId = Convert.ToInt32(insDataTable.Rows[0]["DeviceModelId"]);
                }
                if (insDataTable.Rows[0]["IMEINumber"] != DBNull.Value)
                {
                    parEntMember.IMEINumber = Convert.ToString(insDataTable.Rows[0]["IMEINumber"]);
                }
                if (insDataTable.Rows[0]["ProfilePicturePath"] != DBNull.Value)
                {
                    parEntMember.ProfilePicturePath = Convert.ToString(insDataTable.Rows[0]["ProfilePicturePath"]);
                }
                if (insDataTable.Rows[0]["MemberTitleId"] != DBNull.Value)
                {
                    parEntMember.MemberTitleId = Convert.ToInt32(insDataTable.Rows[0]["MemberTitleId"]);
                }
                if (insDataTable.Rows[0]["AddressCityId"] != DBNull.Value)
                {
                    parEntMember.AddressCityId = Convert.ToInt32(insDataTable.Rows[0]["AddressCityId"]);
                }
                if (insDataTable.Rows[0]["AddressCoordinateX"] != DBNull.Value)
                {
                    parEntMember.AddressCoordinateX = Convert.ToDecimal(insDataTable.Rows[0]["AddressCoordinateX"]);
                }
                if (insDataTable.Rows[0]["AddressCoordinateY"] != DBNull.Value)
                {
                    parEntMember.AddressCoordinateY = Convert.ToDecimal(insDataTable.Rows[0]["AddressCoordinateY"]);
                }
                if (insDataTable.Rows[0]["DirectReportId"] != DBNull.Value)
                {
                    parEntMember.DirectReportId = Convert.ToString(insDataTable.Rows[0]["DirectReportId"]);
                }
            }
        }

        public void UpdateMemberById(entMember insEntMember_Original, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", insEntMember_Original.Id);
            insDbParamCollection.Add("@pAddress", insEntMember_Original.Address);
            insDbParamCollection.Add("@pAddressCityId", insEntMember_Original.AddressCityId);
            insDbParamCollection.Add("@pAddressCoordinateX", insEntMember_Original.AddressCoordinateX);
            insDbParamCollection.Add("@pAddressCoordinateY", insEntMember_Original.AddressCoordinateY);
            insDbParamCollection.Add("@pDataNumber", insEntMember_Original.DataNumber);
            insDbParamCollection.Add("@pDeviceModelId", insEntMember_Original.DeviceModelId);
            insDbParamCollection.Add("@pDirectReportId", insEntMember_Original.DirectReportId);
            insDbParamCollection.Add("@pEmail", insEntMember_Original.Email);
            insDbParamCollection.Add("@pHiringDate", insEntMember_Original.HiringDate);
            insDbParamCollection.Add("@pIMEINumber", insEntMember_Original.IMEINumber);
            insDbParamCollection.Add("@pIsActive", insEntMember_Original.IsActive);
            insDbParamCollection.Add("@pLeavingDate", insEntMember_Original.LeavingDate);
            insDbParamCollection.Add("@pMemberTitleId", insEntMember_Original.MemberTitleId);
            insDbParamCollection.Add("@pNameSurname", insEntMember_Original.NameSurname);
            insDbParamCollection.Add("@pPhoneNumber", insEntMember_Original.PhoneNumber);
            insDbParamCollection.Add("@pSocialSecurityNumber", insEntMember_Original.SocialSecurityNumber);
            insDbParamCollection.Add("@pUserName", insEntMember_Original.UserName);
            insDbParamCollection.Add("@pProfilePicturePath", insEntMember_Original.ProfilePicturePath);
            insDbConnector.ExecuteNonQuery("UpdateMemberById", insDbParamCollection);
        }

        public DataTable Members_MemberListGridData(entMemberCustomer insEntMemberCustomer, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pCustomerId", insEntMemberCustomer.CustomerId);
            return insDbConnector.ExecuteDataTable("Members_MemberListGridData", insDbParamCollection);
        }

        public void SelectMemberByIMEINumber(entMember parEntMember, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pIMEINumber", parEntMember.IMEINumber);
            DataTable insDataTable = new DataTable();
            insDataTable = parDbConnector.ExecuteDataTable("SelectMemberByIMEINumber", insDbParamCollection);
            if (insDataTable.Rows.Count > 0)
            {
                if (insDataTable.Rows[0]["Id"] != DBNull.Value)
                {
                    parEntMember.Id = Convert.ToString(insDataTable.Rows[0]["Id"]);
                }
                if (insDataTable.Rows[0]["Email"] != DBNull.Value)
                {
                    parEntMember.Email = Convert.ToString(insDataTable.Rows[0]["Email"]);
                }
                if (insDataTable.Rows[0]["PhoneNumber"] != DBNull.Value)
                {
                    parEntMember.PhoneNumber = Convert.ToString(insDataTable.Rows[0]["PhoneNumber"]);
                }
                if (insDataTable.Rows[0]["UserName"] != DBNull.Value)
                {
                    parEntMember.UserName = Convert.ToString(insDataTable.Rows[0]["UserName"]);
                }
                if (insDataTable.Rows[0]["NameSurname"] != DBNull.Value)
                {
                    parEntMember.NameSurname = Convert.ToString(insDataTable.Rows[0]["NameSurname"]);
                }
                if (insDataTable.Rows[0]["SocialSecurityNumber"] != DBNull.Value)
                {
                    parEntMember.SocialSecurityNumber = Convert.ToString(insDataTable.Rows[0]["SocialSecurityNumber"]);
                }
                if (insDataTable.Rows[0]["DataNumber"] != DBNull.Value)
                {
                    parEntMember.DataNumber = Convert.ToString(insDataTable.Rows[0]["DataNumber"]);
                }
                if (insDataTable.Rows[0]["HiringDate"] != DBNull.Value)
                {
                    parEntMember.HiringDate = Convert.ToDateTime(insDataTable.Rows[0]["HiringDate"]);
                }
                if (insDataTable.Rows[0]["LeavingDate"] != DBNull.Value)
                {
                    parEntMember.LeavingDate = Convert.ToDateTime(insDataTable.Rows[0]["LeavingDate"]);
                }
                if (insDataTable.Rows[0]["IsActive"] != DBNull.Value)
                {
                    parEntMember.IsActive = Convert.ToBoolean(insDataTable.Rows[0]["IsActive"]);
                }
                if (insDataTable.Rows[0]["Address"] != DBNull.Value)
                {
                    parEntMember.Address = Convert.ToString(insDataTable.Rows[0]["Address"]);
                }
                if (insDataTable.Rows[0]["DeviceModelId"] != DBNull.Value)
                {
                    parEntMember.DeviceModelId = Convert.ToInt32(insDataTable.Rows[0]["DeviceModelId"]);
                }
                if (insDataTable.Rows[0]["IMEINumber"] != DBNull.Value)
                {
                    parEntMember.IMEINumber = Convert.ToString(insDataTable.Rows[0]["IMEINumber"]);
                }
                if (insDataTable.Rows[0]["ProfilePicturePath"] != DBNull.Value)
                {
                    parEntMember.ProfilePicturePath = Convert.ToString(insDataTable.Rows[0]["ProfilePicturePath"]);
                }
                if (insDataTable.Rows[0]["MemberTitleId"] != DBNull.Value)
                {
                    parEntMember.MemberTitleId = Convert.ToInt32(insDataTable.Rows[0]["MemberTitleId"]);
                }
                if (insDataTable.Rows[0]["AddressCityId"] != DBNull.Value)
                {
                    parEntMember.AddressCityId = Convert.ToInt32(insDataTable.Rows[0]["AddressCityId"]);
                }
                if (insDataTable.Rows[0]["AddressCoordinateX"] != DBNull.Value)
                {
                    parEntMember.AddressCoordinateX = Convert.ToDecimal(insDataTable.Rows[0]["AddressCoordinateX"]);
                }
                if (insDataTable.Rows[0]["AddressCoordinateY"] != DBNull.Value)
                {
                    parEntMember.AddressCoordinateY = Convert.ToDecimal(insDataTable.Rows[0]["AddressCoordinateY"]);
                }
                if (insDataTable.Rows[0]["DirectReportId"] != DBNull.Value)
                {
                    parEntMember.DirectReportId = Convert.ToString(insDataTable.Rows[0]["DirectReportId"]);
                }
            }
        }

        public DataTable SelectMemberByMemberTitleIdCustomerId(int memberTitleId, int customerId, DbConnector insDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pMemberTitleId", memberTitleId);
            insDbParamCollection.Add("@pCustomerId", customerId);
            return insDbConnector.ExecuteDataTable("SelectMemberByMemberTitleIdCustomerId", insDbParamCollection);
        }
        #endregion
    }
}
