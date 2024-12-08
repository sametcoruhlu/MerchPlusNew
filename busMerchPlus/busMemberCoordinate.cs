using datMerchPlus;
using entMerchPlus;
using SqlHelper;
using System;
using System.Data;

namespace busMerchPlus
{
    /// <summary>
    /// This layer is responsible for implementing business logic.
    /// Manages transactions on table [MemberCoordinate]
    /// </summary>
    public class busMemberCoordinate : BusBase
    {
        /// <summary>
        /// busMemberCoordinateConstructor method used while taking an instance of this class.
        /// </summary>
        public busMemberCoordinate() : base()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Selects all data stored in table [MemberCoordinate]
        /// </summary>
        public DataTable SelectMemberCoordinate()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberCoordinate insDatMemberCoordinate = new datMemberCoordinate();
                return insDatMemberCoordinate.SelectMemberCoordinate(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Selects one row of data stored in table [MemberCoordinate] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntMemberCoordinate">Gets entity object as parameter for table MemberCoordinate]</param>
        public void SelectMemberCoordinateById(entMemberCoordinate parEntMemberCoordinate)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberCoordinate insDatMemberCoordinate = new datMemberCoordinate();
                insDatMemberCoordinate.SelectMemberCoordinateById(parEntMemberCoordinate, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Inserts one row of data into table [MemberCoordinate] based on the values of input entity object
        /// </summary>
        /// <param name="parEntMemberCoordinate">Gets entity object as parameter for table MemberCoordinate]</param>
        public void InsertMemberCoordinate(entMemberCoordinate parEntMemberCoordinate)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberCoordinate insDatMemberCoordinate = new datMemberCoordinate();
                insDatMemberCoordinate.InsertMemberCoordinate(parEntMemberCoordinate, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Updates one row of data in table [MemberCoordinate] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntMemberCoordinate">Gets entity object as parameter for table MemberCoordinate]</param>
        public void UpdateMemberCoordinateById(entMemberCoordinate parEntMemberCoordinate)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberCoordinate insDatMemberCoordinate = new datMemberCoordinate();
                insDatMemberCoordinate.UpdateMemberCoordinateById(parEntMemberCoordinate, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes all data stored in table [MemberCoordinate]
        /// </summary>
        public void DeleteMemberCoordinate()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberCoordinate insDatMemberCoordinate = new datMemberCoordinate();
                insDatMemberCoordinate.DeleteMemberCoordinate(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes data stored in table [MemberCoordinate] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntMemberCoordinate">Gets entity object as parameter for table MemberCoordinate]</param>
        public void DeleteMemberCoordinateById(entMemberCoordinate parEntMemberCoordinate)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datMemberCoordinate insDatMemberCoordinate = new datMemberCoordinate();
                insDatMemberCoordinate.DeleteMemberCoordinateById(parEntMemberCoordinate, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        #endregion
        #region Custom Methods
        public DataTable SelectMemberCoordinateByMemberIdToday(entMemberCoordinate insEntMemberCoordinate)
        {
            DbConnector insDbConnector = new DbConnector();
            datMemberCoordinate insDatMemberCoordinate = new datMemberCoordinate();
            try
            {
                return insDatMemberCoordinate.SelectMemberCoordinateByMemberIdToday(insEntMemberCoordinate, insDbConnector);
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
