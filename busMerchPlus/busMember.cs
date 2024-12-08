using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using datMerchPlus;
using entMerchPlus;
using SqlHelper;
using System.ServiceProcess;

namespace busMerchPlus
{
    public class busMember : BusBase
    {
        public busMember() : base()
        {
        }

        public void SelectMemberById(entMember parEntMember)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMember insDatMember = new datMember();
                insDatMember.SelectMemberById(parEntMember, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        public DataTable SelectMember()
        {
            DbConnector insDbConnector = new DbConnector();
            datMember insDatMember = new datMember();
            try
            {
                return insDatMember.SelectMember(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public DataTable SelectMemberByCustomerIdGridData(entMemberCustomer insEntMemberCustomer)
        {
            DbConnector insDbConnector = new DbConnector();
            datMember insDatMember = new datMember();
            try
            {
                return insDatMember.SelectMemberByCustomerIdGridData(insEntMemberCustomer, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public DataTable Members_MemberListGridData(entMemberCustomer insEntMemberCustomer)
        {
            DbConnector insDbConnector = new DbConnector();
            datMember insDatMember = new datMember();
            try
            {
                return insDatMember.Members_MemberListGridData(insEntMemberCustomer, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        public void ManageMemberWisely(entMember insEntMember, string memberCustomerList, string memberDistrictList)
        {
            DbConnector insDbConnector = new DbConnector();
            datMember insDatMember = new datMember();
            datMemberCustomer insDatMemberCustomer = new datMemberCustomer();
            datMemberDistrict insDatMemberDistrict = new datMemberDistrict();
            try
            {
                insDbConnector.BeginTransaction();


                #region Existing User

                #region Delete MemberCustomer
                entMemberCustomer insEntMemberCustomer = new entMemberCustomer();
                insEntMemberCustomer.MemberId = Convert.ToString(insEntMember.Id);
                insDatMemberCustomer.DeleteMemberCustomerByMemberId(insEntMemberCustomer, insDbConnector);
                #endregion

                #region Delete MemberDistrict
                entMemberDistrict insEntMemberDistrict = new entMemberDistrict();
                insEntMemberDistrict.MemberId = Convert.ToString(insEntMember.Id);
                insDatMemberDistrict.DeleteMemberDistrictByMemberId(insEntMemberDistrict, insDbConnector);
                #endregion

                #region Get Original Entity
                entMember insEntMember_Original = new entMember();
                insEntMember_Original.Id = insEntMember.Id;
                insDatMember.SelectMemberById(insEntMember_Original, insDbConnector);
                #endregion

                #region Set New Values
                insEntMember_Original.Address = insEntMember.Address;
                insEntMember_Original.AddressCityId = insEntMember.AddressCityId;
                insEntMember_Original.AddressCoordinateX = insEntMember.AddressCoordinateX;
                insEntMember_Original.AddressCoordinateY = insEntMember.AddressCoordinateY;
                insEntMember_Original.DataNumber = Convert.ToString(insEntMember.DataNumber).Replace(" ", string.Empty).Replace("(", "").Replace(")", "").Replace("-", "").Trim();
                insEntMember_Original.DeviceModelId = insEntMember.DeviceModelId;
                insEntMember_Original.DirectReportId = insEntMember.DirectReportId;
                insEntMember_Original.Email = insEntMember.Email;
                insEntMember_Original.HiringDate = insEntMember.HiringDate;
                insEntMember_Original.IMEINumber = insEntMember.IMEINumber;
                insEntMember_Original.IsActive = insEntMember.IsActive;
                insEntMember_Original.LeavingDate = insEntMember.LeavingDate;
                insEntMember_Original.MemberTitleId = insEntMember.MemberTitleId;
                insEntMember_Original.NameSurname = insEntMember.NameSurname;
                insEntMember_Original.PhoneNumber = Convert.ToString(insEntMember.PhoneNumber).Replace(" ", string.Empty).Replace("(", "").Replace(")", "").Replace("-", "").Trim();
                insEntMember_Original.SocialSecurityNumber = insEntMember.SocialSecurityNumber;
                insEntMember_Original.UserName = insEntMember.UserName;
                if (insEntMember.ProfilePicturePath != null)
                    insEntMember_Original.ProfilePicturePath = insEntMember.ProfilePicturePath;
                #endregion

                insDatMember.UpdateMemberById(insEntMember_Original, insDbConnector);

                #endregion

                #region Insert MemberCustomer
                string[] companies = memberCustomerList.Split(',');
                foreach (string companyId in companies)
                {
                    entMemberCustomer insEntMemberCustomer_New = new entMemberCustomer();
                    insEntMemberCustomer_New.CustomerId = Convert.ToInt32(companyId);
                    insEntMemberCustomer_New.MemberId = insEntMember_Original.Id;
                    insDatMemberCustomer.InsertMemberCustomer(insEntMemberCustomer_New, insDbConnector);
                }
                #endregion

                #region Insert MemberDistrict
                if (memberDistrictList != string.Empty)
                {
                    string[] districts = memberDistrictList.Split(',');
                    foreach (string districtId in districts)
                    {
                        entMemberDistrict insEntMemberDistrict_New = new entMemberDistrict();
                        insEntMemberDistrict_New.DistrictId = Convert.ToInt32(districtId);
                        insEntMemberDistrict_New.MemberId = Convert.ToString(insEntMember_Original.Id);
                        insDatMemberDistrict.InsertMemberDistrict(insEntMemberDistrict_New, insDbConnector);
                    }
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

        public void SelectMemberByUserName(entMember insEntMember)
        {
            DbConnector insDbConnector = new DbConnector();
            datMember insDatMember = new datMember();
            try
            {
                insDatMember.SelectMemberByUserName(insEntMember, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        public void SelectMemberByIMEINumber(entMember insEntMember)
        {
            DbConnector insDbConnector = new DbConnector();
            datMember insDatMember = new datMember();
            try
            {
                insDatMember.SelectMemberByIMEINumber(insEntMember, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        public DataTable SelectMemberByMemberTitleIdCustomerId(int MemberTitleId, int CustomerId)
        {
            DbConnector insDbConnector = new DbConnector();
            datMember insDatMember = new datMember();
            try
            {
                return insDatMember.SelectMemberByMemberTitleIdCustomerId(MemberTitleId, CustomerId, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }
    }
}
