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
    /// Manages transactions on table [MemberTimeOff]
    /// </summary>
    public class busMemberTimeOff : BusBase
    {
        /// <summary>
        /// busMemberTimeOffConstructor method used while taking an instance of this class.
        /// </summary>
        public busMemberTimeOff() : base()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Selects all data stored in table [MemberTimeOff]
        /// </summary>
        public DataTable SelectMemberTimeOff()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberTimeOff insDatMemberTimeOff = new datMemberTimeOff();
                return insDatMemberTimeOff.SelectMemberTimeOff(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Selects one row of data stored in table [MemberTimeOff] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntMemberTimeOff">Gets entity object as parameter for table MemberTimeOff]</param>
        public void SelectMemberTimeOffById(entMemberTimeOff parEntMemberTimeOff)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberTimeOff insDatMemberTimeOff = new datMemberTimeOff();
                insDatMemberTimeOff.SelectMemberTimeOffById(parEntMemberTimeOff, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Inserts one row of data into table [MemberTimeOff] based on the values of input entity object
        /// </summary>
        /// <param name="parEntMemberTimeOff">Gets entity object as parameter for table MemberTimeOff]</param>
        public void InsertMemberTimeOff(entMemberTimeOff parEntMemberTimeOff)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberTimeOff insDatMemberTimeOff = new datMemberTimeOff();
                insDatMemberTimeOff.InsertMemberTimeOff(parEntMemberTimeOff, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Updates one row of data in table [MemberTimeOff] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntMemberTimeOff">Gets entity object as parameter for table MemberTimeOff]</param>
        public void UpdateMemberTimeOffById(entMemberTimeOff parEntMemberTimeOff)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberTimeOff insDatMemberTimeOff = new datMemberTimeOff();
                insDatMemberTimeOff.UpdateMemberTimeOffById(parEntMemberTimeOff, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes all data stored in table [MemberTimeOff]
        /// </summary>
        public void DeleteMemberTimeOff()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberTimeOff insDatMemberTimeOff = new datMemberTimeOff();
                insDatMemberTimeOff.DeleteMemberTimeOff(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes data stored in table [MemberTimeOff] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntMemberTimeOff">Gets entity object as parameter for table MemberTimeOff]</param>
        public void DeleteMemberTimeOffById(entMemberTimeOff parEntMemberTimeOff)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberTimeOff insDatMemberTimeOff = new datMemberTimeOff();
                insDatMemberTimeOff.DeleteMemberTimeOffById(parEntMemberTimeOff, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        #endregion
        #region Custom Methods
        public DataTable SelectMemberTimeOffByMemberId(entMemberTimeOff insEntMemberTimeOff)
        {
            DbConnector insDbConnector = new DbConnector();
            datMemberTimeOff insDatMemberTimeOff = new datMemberTimeOff();
            try
            {
                return insDatMemberTimeOff.SelectMemberTimeOffByMemberId(insEntMemberTimeOff, insDbConnector);
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
