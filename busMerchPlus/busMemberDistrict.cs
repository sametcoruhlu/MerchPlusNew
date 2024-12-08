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
    /// Manages transactions on table [MemberDistrict]
    /// </summary>
    public class busMemberDistrict : BusBase
    {
        /// <summary>
        /// busMemberDistrictConstructor method used while taking an instance of this class.
        /// </summary>
        public busMemberDistrict() : base()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Selects all data stored in table [MemberDistrict]
        /// </summary>
        public DataTable SelectMemberDistrict()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberDistrict insDatMemberDistrict = new datMemberDistrict();
                return insDatMemberDistrict.SelectMemberDistrict(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Selects one row of data stored in table [MemberDistrict] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntMemberDistrict">Gets entity object as parameter for table MemberDistrict]</param>
        public void SelectMemberDistrictById(entMemberDistrict parEntMemberDistrict)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberDistrict insDatMemberDistrict = new datMemberDistrict();
                insDatMemberDistrict.SelectMemberDistrictById(parEntMemberDistrict, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Inserts one row of data into table [MemberDistrict] based on the values of input entity object
        /// </summary>
        /// <param name="parEntMemberDistrict">Gets entity object as parameter for table MemberDistrict]</param>
        public void InsertMemberDistrict(entMemberDistrict parEntMemberDistrict)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberDistrict insDatMemberDistrict = new datMemberDistrict();
                insDatMemberDistrict.InsertMemberDistrict(parEntMemberDistrict, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Updates one row of data in table [MemberDistrict] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntMemberDistrict">Gets entity object as parameter for table MemberDistrict]</param>
        public void UpdateMemberDistrictById(entMemberDistrict parEntMemberDistrict)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberDistrict insDatMemberDistrict = new datMemberDistrict();
                insDatMemberDistrict.UpdateMemberDistrictById(parEntMemberDistrict, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes all data stored in table [MemberDistrict]
        /// </summary>
        public void DeleteMemberDistrict()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberDistrict insDatMemberDistrict = new datMemberDistrict();
                insDatMemberDistrict.DeleteMemberDistrict(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes data stored in table [MemberDistrict] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntMemberDistrict">Gets entity object as parameter for table MemberDistrict]</param>
        public void DeleteMemberDistrictById(entMemberDistrict parEntMemberDistrict)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberDistrict insDatMemberDistrict = new datMemberDistrict();
                insDatMemberDistrict.DeleteMemberDistrictById(parEntMemberDistrict, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        #endregion
        #region Custom Methods
        public DataTable SelectMemberDistrictByMemberId(entMemberCustomer insEntMemberCustomer)
        {
            DbConnector insDbConnector = new DbConnector();
            datMemberDistrict insDatMemberDistrict = new datMemberDistrict();
            try
            {
                return insDatMemberDistrict.SelectMemberDistrictByMemberId(insEntMemberCustomer, insDbConnector);
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