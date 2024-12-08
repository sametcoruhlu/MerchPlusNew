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
    /// Manages transactions on table [PackagingType]
    /// </summary>
    public class busPackagingType : BusBase
    {
        /// <summary>
        /// busPackagingTypeConstructor method used while taking an instance of this class.
        /// </summary>
        public busPackagingType() : base()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Selects all data stored in table [PackagingType]
        /// </summary>
        public DataTable SelectPackagingType()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datPackagingType insDatPackagingType = new datPackagingType();
                return insDatPackagingType.SelectPackagingType(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Selects one row of data stored in table [PackagingType] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntPackagingType">Gets entity object as parameter for table PackagingType]</param>
        public void SelectPackagingTypeById(entPackagingType parEntPackagingType)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datPackagingType insDatPackagingType = new datPackagingType();
                insDatPackagingType.SelectPackagingTypeById(parEntPackagingType, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Inserts one row of data into table [PackagingType] based on the values of input entity object
        /// </summary>
        /// <param name="parEntPackagingType">Gets entity object as parameter for table PackagingType]</param>
        public void InsertPackagingType(entPackagingType parEntPackagingType)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datPackagingType insDatPackagingType = new datPackagingType();
                insDatPackagingType.InsertPackagingType(parEntPackagingType, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Updates one row of data in table [PackagingType] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntPackagingType">Gets entity object as parameter for table PackagingType]</param>
        public void UpdatePackagingTypeById(entPackagingType parEntPackagingType)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datPackagingType insDatPackagingType = new datPackagingType();
                insDatPackagingType.UpdatePackagingTypeById(parEntPackagingType, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes all data stored in table [PackagingType]
        /// </summary>
        public void DeletePackagingType()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datPackagingType insDatPackagingType = new datPackagingType();
                insDatPackagingType.DeletePackagingType(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes data stored in table [PackagingType] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntPackagingType">Gets entity object as parameter for table PackagingType]</param>
        public void DeletePackagingTypeById(entPackagingType parEntPackagingType)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datPackagingType insDatPackagingType = new datPackagingType();
                insDatPackagingType.DeletePackagingTypeById(parEntPackagingType, insDbConnector);
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