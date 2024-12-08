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
    /// Manages transactions on table [MemberCustomer]
    /// </summary>
    public class busMemberCustomer : BusBase
    {
        /// <summary>
        /// busMemberCustomerConstructor method used while taking an instance of this class.
        /// </summary>
        public busMemberCustomer() : base()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Selects all data stored in table [MemberCustomer]
        /// </summary>
        public DataTable SelectMemberCustomer()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberCustomer insDatMemberCustomer = new datMemberCustomer();
                return insDatMemberCustomer.SelectMemberCustomer(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Selects one row of data stored in table [MemberCustomer] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntMemberCustomer">Gets entity object as parameter for table MemberCustomer]</param>
        public void SelectMemberCustomerById(entMemberCustomer parEntMemberCustomer)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberCustomer insDatMemberCustomer = new datMemberCustomer();
                insDatMemberCustomer.SelectMemberCustomerById(parEntMemberCustomer, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Inserts one row of data into table [MemberCustomer] based on the values of input entity object
        /// </summary>
        /// <param name="parEntMemberCustomer">Gets entity object as parameter for table MemberCustomer]</param>
        public void InsertMemberCustomer(entMemberCustomer parEntMemberCustomer)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberCustomer insDatMemberCustomer = new datMemberCustomer();
                insDatMemberCustomer.InsertMemberCustomer(parEntMemberCustomer, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Updates one row of data in table [MemberCustomer] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntMemberCustomer">Gets entity object as parameter for table MemberCustomer]</param>
        public void UpdateMemberCustomerById(entMemberCustomer parEntMemberCustomer)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberCustomer insDatMemberCustomer = new datMemberCustomer();
                insDatMemberCustomer.UpdateMemberCustomerById(parEntMemberCustomer, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes all data stored in table [MemberCustomer]
        /// </summary>
        public void DeleteMemberCustomer()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberCustomer insDatMemberCustomer = new datMemberCustomer();
                insDatMemberCustomer.DeleteMemberCustomer(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes data stored in table [MemberCustomer] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntMemberCustomer">Gets entity object as parameter for table MemberCustomer]</param>
        public void DeleteMemberCustomerById(entMemberCustomer parEntMemberCustomer)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberCustomer insDatMemberCustomer = new datMemberCustomer();
                insDatMemberCustomer.DeleteMemberCustomerById(parEntMemberCustomer, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        #endregion
        #region Custom Methods
        public DataTable SelectMemberCustomerByMemberIdGridData(entMemberCustomer insEntMemberCustomer)
        {
            DbConnector insDbConnector = new DbConnector();
            datMemberCustomer insDatMemberCustomer = new datMemberCustomer();
            try
            {
                return insDatMemberCustomer.SelectMemberCustomerByMemberIdGridData(insEntMemberCustomer, insDbConnector);
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