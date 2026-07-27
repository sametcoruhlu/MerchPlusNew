using System;
using System.Collections.Generic;
using System.Text;
using SqlHelper;
using System.Data;
using entMerchPlus;

namespace datMerchPlus
{
    /// <summary>
    /// The class that is generated hold parameters to manage database table [CustomerFormFieldVisibility]
    /// </summary>
    public class datCustomerFormFieldVisibility
    {
        /// <summary>
        /// datCustomerFormFieldVisibility Constructor method used while taking an instance of this class.
        /// </summary>
        public datCustomerFormFieldVisibility()
        {
        }

        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Method that selects all data stored in table [CustomerFormFieldVisibility]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public DataTable SelectAll(DbConnector parDbConnector)
        {
            return parDbConnector.ExecuteDataTable("SelectCustomerFormFieldVisibility", null);
        }

        /// <summary>
        /// Method that selects data in table [CustomerFormFieldVisibility] filtered by CustomerId
        /// </summary>
        /// <param name="parCustomerId">CustomerId to filter by</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public DataTable SelectByCustomerId(int parCustomerId, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pCustomerId", parCustomerId);
            return parDbConnector.ExecuteDataTable("SelectCustomerFormFieldVisibilityByCustomerId", insDbParamCollection);
        }

        /// <summary>
        /// Method that selects one row of data in table [CustomerFormFieldVisibility] filtered by the PK field of the inbound entity object
        /// </summary>
        /// <param name="parEntCustomerFormFieldVisibility">Entity object as parameter for table [CustomerFormFieldVisibility]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void SelectCustomerFormFieldVisibilityById(entCustomerFormFieldVisibility parEntCustomerFormFieldVisibility, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntCustomerFormFieldVisibility.Id);
            DataTable insDataTable = new DataTable();
            insDataTable = parDbConnector.ExecuteDataTable("SelectCustomerFormFieldVisibilityById", insDbParamCollection);
            if (insDataTable.Rows.Count > 0)
            {
                if (insDataTable.Rows[0]["Id"] != DBNull.Value)
                {
                    parEntCustomerFormFieldVisibility.Id = Convert.ToInt32(insDataTable.Rows[0]["Id"]);
                }
                if (insDataTable.Rows[0]["CustomerId"] != DBNull.Value)
                {
                    parEntCustomerFormFieldVisibility.CustomerId = Convert.ToInt32(insDataTable.Rows[0]["CustomerId"]);
                }
                if (insDataTable.Rows[0]["FieldName"] != DBNull.Value)
                {
                    parEntCustomerFormFieldVisibility.FieldName = Convert.ToString(insDataTable.Rows[0]["FieldName"]);
                }
                if (insDataTable.Rows[0]["IsVisible"] != DBNull.Value)
                {
                    parEntCustomerFormFieldVisibility.IsVisible = Convert.ToBoolean(insDataTable.Rows[0]["IsVisible"]);
                }
                if (insDataTable.Rows[0]["IsRequired"] != DBNull.Value)
                {
                    parEntCustomerFormFieldVisibility.IsRequired = Convert.ToBoolean(insDataTable.Rows[0]["IsRequired"]);
                }
                if (insDataTable.Rows[0]["DisplayOrder"] != DBNull.Value)
                {
                    parEntCustomerFormFieldVisibility.DisplayOrder = Convert.ToInt32(insDataTable.Rows[0]["DisplayOrder"]);
                }
                if (insDataTable.Rows[0]["CreatedBy"] != DBNull.Value)
                {
                    parEntCustomerFormFieldVisibility.CreatedBy = Convert.ToString(insDataTable.Rows[0]["CreatedBy"]);
                }
                if (insDataTable.Rows[0]["CreatedOn"] != DBNull.Value)
                {
                    parEntCustomerFormFieldVisibility.CreatedOn = Convert.ToDateTime(insDataTable.Rows[0]["CreatedOn"]);
                }
                if (insDataTable.Rows[0]["ModifiedBy"] != DBNull.Value)
                {
                    parEntCustomerFormFieldVisibility.ModifiedBy = Convert.ToString(insDataTable.Rows[0]["ModifiedBy"]);
                }
                if (insDataTable.Rows[0]["ModifiedOn"] != DBNull.Value)
                {
                    parEntCustomerFormFieldVisibility.ModifiedOn = Convert.ToDateTime(insDataTable.Rows[0]["ModifiedOn"]);
                }
                if (insDataTable.Rows[0]["IsActive"] != DBNull.Value)
                {
                    parEntCustomerFormFieldVisibility.IsActive = Convert.ToBoolean(insDataTable.Rows[0]["IsActive"]);
                }
            }
        }

        /// <summary>
        /// Method that inserts one row of data into table [CustomerFormFieldVisibility] based on the values of input entity object
        /// </summary>
        /// <param name="parEntCustomerFormFieldVisibility">Entity object as parameter for table [CustomerFormFieldVisibility]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void InsertCustomerFormFieldVisibility(entCustomerFormFieldVisibility parEntCustomerFormFieldVisibility, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.AddOutput("@pId", DbType.Int32);
            insDbParamCollection.Add("@pCustomerId", parEntCustomerFormFieldVisibility.CustomerId);
            insDbParamCollection.Add("@pFieldName", parEntCustomerFormFieldVisibility.FieldName);
            insDbParamCollection.Add("@pIsVisible", parEntCustomerFormFieldVisibility.IsVisible);
            insDbParamCollection.Add("@pIsRequired", parEntCustomerFormFieldVisibility.IsRequired);
            insDbParamCollection.Add("@pDisplayOrder", parEntCustomerFormFieldVisibility.DisplayOrder);
            insDbParamCollection.Add("@pCreatedBy", parEntCustomerFormFieldVisibility.CreatedBy);
            insDbParamCollection.Add("@pCreatedOn", parEntCustomerFormFieldVisibility.CreatedOn);
            insDbParamCollection.Add("@pIsActive", parEntCustomerFormFieldVisibility.IsActive);
            parDbConnector.ExecuteNonQuery("InsertCustomerFormFieldVisibility", insDbParamCollection);
            parEntCustomerFormFieldVisibility.Id = Convert.ToInt32(insDbParamCollection.GetOutPutParameter().Value);
        }

        /// <summary>
        /// Method that updates one row of data in table [CustomerFormFieldVisibility] based on the values of input entity object
        /// </summary>
        /// <param name="parEntCustomerFormFieldVisibility">Entity object as parameter for table [CustomerFormFieldVisibility]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void UpdateCustomerFormFieldVisibilityById(entCustomerFormFieldVisibility parEntCustomerFormFieldVisibility, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntCustomerFormFieldVisibility.Id);
            insDbParamCollection.Add("@pCustomerId", parEntCustomerFormFieldVisibility.CustomerId);
            insDbParamCollection.Add("@pFieldName", parEntCustomerFormFieldVisibility.FieldName);
            insDbParamCollection.Add("@pIsVisible", parEntCustomerFormFieldVisibility.IsVisible);
            insDbParamCollection.Add("@pIsRequired", parEntCustomerFormFieldVisibility.IsRequired);
            insDbParamCollection.Add("@pDisplayOrder", parEntCustomerFormFieldVisibility.DisplayOrder);
            insDbParamCollection.Add("@pModifiedBy", parEntCustomerFormFieldVisibility.ModifiedBy);
            insDbParamCollection.Add("@pModifiedOn", parEntCustomerFormFieldVisibility.ModifiedOn);
            insDbParamCollection.Add("@pIsActive", parEntCustomerFormFieldVisibility.IsActive);
            parDbConnector.ExecuteNonQuery("UpdateCustomerFormFieldVisibilityById", insDbParamCollection);
        }

        /// <summary>
        /// Method that deletes one row of data in table [CustomerFormFieldVisibility] based on the PK field of the inbound entity object
        /// </summary>
        /// <param name="parEntCustomerFormFieldVisibility">Entity object as parameter for table [CustomerFormFieldVisibility]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteCustomerFormFieldVisibilityById(entCustomerFormFieldVisibility parEntCustomerFormFieldVisibility, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntCustomerFormFieldVisibility.Id);
            parDbConnector.ExecuteNonQuery("DeleteCustomerFormFieldVisibilityById", insDbParamCollection);
        }

        /// <summary>
        /// Method that deletes all data in table [CustomerFormFieldVisibility]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteCustomerFormFieldVisibility(DbConnector parDbConnector)
        {
            parDbConnector.ExecuteNonQuery("DeleteCustomerFormFieldVisibility", null);
        }
        #endregion
    }
}
