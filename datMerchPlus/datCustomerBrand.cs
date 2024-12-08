using System;
using System.Collections.Generic;
using System.Text;
using SqlHelper;
using System.Data;
using entMerchPlus;

namespace datMerchPlus
{
    /// <summary>
    /// The class that is generated hold parameters to manage database table [CustomerBrand]
    /// </summary>
    public class datCustomerBrand
    {
        /// <summary>
        /// datCustomerBrand Constructor method used while taking an instance of this class.
        /// </summary>
        public datCustomerBrand()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Method that selects all data stored in table [CustomerBrand]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public DataTable SelectCustomerBrand(DbConnector parDbConnector)
        {
            return parDbConnector.ExecuteDataTable("SelectCustomerBrand", null);
        }

        /// <summary>
        /// Method that selects one row of data in table [CustomerBrand] filtered by the PK field of the inbound entity object
        /// </summary>
        /// <param name="parEntCustomerBrand">Entity object as parameter for tableCustomerBrand]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void SelectCustomerBrandById(entCustomerBrand parEntCustomerBrand, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntCustomerBrand.Id);
            DataTable insDataTable = new DataTable();
            insDataTable = parDbConnector.ExecuteDataTable("SelectCustomerBrandById", insDbParamCollection);
            if (insDataTable.Rows.Count > 0)
            {
                if (insDataTable.Rows[0]["Id"] != DBNull.Value)
                {
                    parEntCustomerBrand.Id = Convert.ToInt32(insDataTable.Rows[0]["Id"]);
                }
                if (insDataTable.Rows[0]["CustomerId"] != DBNull.Value)
                {
                    parEntCustomerBrand.CustomerId = Convert.ToInt32(insDataTable.Rows[0]["CustomerId"]);
                }
                if (insDataTable.Rows[0]["Name"] != DBNull.Value)
                {
                    parEntCustomerBrand.Name = Convert.ToString(insDataTable.Rows[0]["Name"]);
                }
                if (insDataTable.Rows[0]["Description"] != DBNull.Value)
                {
                    parEntCustomerBrand.Description = Convert.ToString(insDataTable.Rows[0]["Description"]);
                }
                if (insDataTable.Rows[0]["ProfilePicturePath"] != DBNull.Value)
                {
                    parEntCustomerBrand.ProfilePicturePath = Convert.ToString(insDataTable.Rows[0]["ProfilePicturePath"]);
                }
            }
        }

        /// <summary>
        /// Method that inserts one row of data into table [CustomerBrand] based on the values of input entity object
        /// </summary>
        /// <param name="parEntCustomerBrand">Entity object as parameter for tableCustomerBrand]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void InsertCustomerBrand(entCustomerBrand parEntCustomerBrand, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.AddOutput("@pId", DbType.Int32);
            insDbParamCollection.Add("@pCustomerId", parEntCustomerBrand.CustomerId);
            insDbParamCollection.Add("@pName", parEntCustomerBrand.Name);
            insDbParamCollection.Add("@pDescription", parEntCustomerBrand.Description);
            insDbParamCollection.Add("@pProfilePicturePath", parEntCustomerBrand.ProfilePicturePath);
            parDbConnector.ExecuteNonQuery("InsertCustomerBrand", insDbParamCollection);
            parEntCustomerBrand.Id = Convert.ToInt32(insDbParamCollection.GetOutPutParameter().Value);
        }

        /// <summary>
        /// Method that updates one row of data in table [CustomerBrand] based on the values of input entity object
        /// </summary>
        /// <param name="parEntCustomerBrand">Entity object as parameter for tableCustomerBrand]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void UpdateCustomerBrandById(entCustomerBrand parEntCustomerBrand, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntCustomerBrand.Id);
            insDbParamCollection.Add("@pCustomerId", parEntCustomerBrand.CustomerId);
            insDbParamCollection.Add("@pName", parEntCustomerBrand.Name);
            insDbParamCollection.Add("@pDescription", parEntCustomerBrand.Description);
            insDbParamCollection.Add("@pProfilePicturePath", parEntCustomerBrand.ProfilePicturePath);
            parDbConnector.ExecuteNonQuery("UpdateCustomerBrandById", insDbParamCollection);
        }

        /// <summary>
        /// Method that deletes all data in table [CustomerBrand]
        /// </summary>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteCustomerBrand(DbConnector parDbConnector)
        {
            parDbConnector.ExecuteNonQuery("DeleteCustomerBrand", null);
        }

        /// <summary>
        /// Method that deletes one row of data in table [CustomerBrand] based on the passed entity object
        /// </summary>
        /// <param name="parEntCustomerBrand">Gets entity object as parameter for table CustomerBrand]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void DeleteCustomerBrandById(entCustomerBrand parEntCustomerBrand, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.Add("@pId", parEntCustomerBrand.Id);
            parDbConnector.ExecuteNonQuery("DeleteCustomerBrandById", insDbParamCollection);
        }

        #endregion
        #region Custom Methods
        #endregion
    }
}