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
    /// Manages transactions on table [MemberTitle]
    /// </summary>
    public class busMemberTitle : BusBase
    {
        /// <summary>
        /// busMemberTitleConstructor method used while taking an instance of this class.
        /// </summary>
        public busMemberTitle() : base()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Selects all data stored in table [MemberTitle]
        /// </summary>
        public DataTable SelectMemberTitle()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberTitle insDatMemberTitle = new datMemberTitle();
                return insDatMemberTitle.SelectMemberTitle(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Selects one row of data stored in table [MemberTitle] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntMemberTitle">Gets entity object as parameter for table MemberTitle]</param>
        public void SelectMemberTitleById(entMemberTitle parEntMemberTitle)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberTitle insDatMemberTitle = new datMemberTitle();
                insDatMemberTitle.SelectMemberTitleById(parEntMemberTitle, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Inserts one row of data into table [MemberTitle] based on the values of input entity object
        /// </summary>
        /// <param name="parEntMemberTitle">Gets entity object as parameter for table MemberTitle]</param>
        public void InsertMemberTitle(entMemberTitle parEntMemberTitle)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberTitle insDatMemberTitle = new datMemberTitle();
                insDatMemberTitle.InsertMemberTitle(parEntMemberTitle, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Updates one row of data in table [MemberTitle] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntMemberTitle">Gets entity object as parameter for table MemberTitle]</param>
        public void UpdateMemberTitleById(entMemberTitle parEntMemberTitle)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberTitle insDatMemberTitle = new datMemberTitle();
                insDatMemberTitle.UpdateMemberTitleById(parEntMemberTitle, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes all data stored in table [MemberTitle]
        /// </summary>
        public void DeleteMemberTitle()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberTitle insDatMemberTitle = new datMemberTitle();
                insDatMemberTitle.DeleteMemberTitle(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes data stored in table [MemberTitle] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntMemberTitle">Gets entity object as parameter for table MemberTitle]</param>
        public void DeleteMemberTitleById(entMemberTitle parEntMemberTitle)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberTitle insDatMemberTitle = new datMemberTitle();
                insDatMemberTitle.DeleteMemberTitleById(parEntMemberTitle, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        #endregion
        #region Custom Methods
        #endregion
    }
}
