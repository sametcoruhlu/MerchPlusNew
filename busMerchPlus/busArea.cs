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
    /// Manages transactions on table [Area]
    /// </summary>
    public class busArea : BusBase
    {
        /// <summary>
        /// busAreaConstructor method used while taking an instance of this class.
        /// </summary>
        public busArea() : base()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Selects all data stored in table [Area]
        /// </summary>
        public DataTable SelectArea()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datArea insDatArea = new datArea();
                return insDatArea.SelectArea(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Selects one row of data stored in table [Area] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntArea">Gets entity object as parameter for table Area]</param>
        public void SelectAreaById(entArea parEntArea)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datArea insDatArea = new datArea();
                insDatArea.SelectAreaById(parEntArea, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Inserts one row of data into table [Area] based on the values of input entity object
        /// </summary>
        /// <param name="parEntArea">Gets entity object as parameter for table Area]</param>
        public void InsertArea(entArea parEntArea)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datArea insDatArea = new datArea();
                insDatArea.InsertArea(parEntArea, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Updates one row of data in table [Area] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntArea">Gets entity object as parameter for table Area]</param>
        public void UpdateAreaById(entArea parEntArea)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datArea insDatArea = new datArea();
                insDatArea.UpdateAreaById(parEntArea, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes all data stored in table [Area]
        /// </summary>
        public void DeleteArea()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datArea insDatArea = new datArea();
                insDatArea.DeleteArea(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes data stored in table [Area] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntArea">Gets entity object as parameter for table Area]</param>
        public void DeleteAreaById(entArea parEntArea)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datArea insDatArea = new datArea();
                insDatArea.DeleteAreaById(parEntArea, insDbConnector);
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