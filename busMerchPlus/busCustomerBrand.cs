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
    /// Manages transactions on table [CustomerBrand]
    /// </summary>
    public class busCustomerBrand : BusBase
    {
        /// <summary>
        /// busCustomerBrandConstructor method used while taking an instance of this class.
        /// </summary>
        public busCustomerBrand() : base()
        {
        }
        #region BSM Code Generator Methods [DO NOT PUT CUSTOM METHODS HERE!]
        /// <summary>
        /// Selects all data stored in table [CustomerBrand]
        /// </summary>
        public DataTable SelectCustomerBrand()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomerBrand insDatCustomerBrand = new datCustomerBrand();
                return insDatCustomerBrand.SelectCustomerBrand(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Selects one row of data stored in table [CustomerBrand] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntCustomerBrand">Gets entity object as parameter for table CustomerBrand]</param>
        public void SelectCustomerBrandById(entCustomerBrand parEntCustomerBrand)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomerBrand insDatCustomerBrand = new datCustomerBrand();
                insDatCustomerBrand.SelectCustomerBrandById(parEntCustomerBrand, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Inserts one row of data into table [CustomerBrand] based on the values of input entity object
        /// </summary>
        /// <param name="parEntCustomerBrand">Gets entity object as parameter for table CustomerBrand]</param>
        public void InsertCustomerBrand(entCustomerBrand parEntCustomerBrand)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomerBrand insDatCustomerBrand = new datCustomerBrand();
                insDatCustomerBrand.InsertCustomerBrand(parEntCustomerBrand, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Updates one row of data in table [CustomerBrand] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntCustomerBrand">Gets entity object as parameter for table CustomerBrand]</param>
        public void UpdateCustomerBrandById(entCustomerBrand parEntCustomerBrand)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomerBrand insDatCustomerBrand = new datCustomerBrand();
                insDatCustomerBrand.UpdateCustomerBrandById(parEntCustomerBrand, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes all data stored in table [CustomerBrand]
        /// </summary>
        public void DeleteCustomerBrand()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomerBrand insDatCustomerBrand = new datCustomerBrand();
                insDatCustomerBrand.DeleteCustomerBrand(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        /// <summary>
        /// Deletes data stored in table [CustomerBrand] based on the PK field passed in the input entity object
        /// </summary>
        /// <param name="parEntCustomerBrand">Gets entity object as parameter for table CustomerBrand]</param>
        public void DeleteCustomerBrandById(entCustomerBrand parEntCustomerBrand)
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datCustomerBrand insDatCustomerBrand = new datCustomerBrand();
                insDatCustomerBrand.DeleteCustomerBrandById(parEntCustomerBrand, insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
            }
        }

        #endregion
        #region Custom Methods
        public List<CustomerBrandCategoryItem> SelectCustomerBrandGridData()
        {
            DbConnector insDbConnector = new DbConnector();
            datCustomerBrand insDatCustomerBrand = new datCustomerBrand();
            datCustomerBrandCategory insDatCustomerBrandCategory = new datCustomerBrandCategory();
            try
            {
                List<CustomerBrandCategoryItem> list = new List<CustomerBrandCategoryItem>();

                DataTable insDt = new DataTable();
                insDt = insDatCustomerBrand.SelectCustomerBrand(insDbConnector);

                foreach (DataRow insDr in insDt.Rows)
                {
                    CustomerBrandCategoryItem insItem = new CustomerBrandCategoryItem();
                    insItem.id = Convert.ToInt32(insDr["Id"]);
                    insItem.text = Convert.ToString(insDr["Name"]);

                    DataTable insDt_Items = new DataTable();
                    entCustomerBrandCategory insEntCustomerBrandCategory = new entCustomerBrandCategory();
                    insEntCustomerBrandCategory.CustomerBrandId = insItem.id;
                    insDt_Items = insDatCustomerBrandCategory.SelectCustomerBrandCategoryByCustomerBrandIdParentIdNull(insEntCustomerBrandCategory, insDbConnector);

                    List<CustomerBrandCategoryItem> listSubItems = new List<CustomerBrandCategoryItem>();

                    foreach (DataRow insDr_CategoryItem in insDt_Items.Rows)
                    {
                        CustomerBrandCategoryItem insSubItem = new CustomerBrandCategoryItem();
                        insSubItem.id = Convert.ToInt32(insDr_CategoryItem["Id"]);
                        insSubItem.text = Convert.ToString(insDr_CategoryItem["Name"]);

                        insSubItem.children = SelectCustomerBrandCategoryByParentId(Convert.ToInt32(insSubItem.id), insDbConnector);

                        listSubItems.Add(insSubItem);
                    }

                    insItem.children = listSubItems;

                    list.Add(insItem);
                }

                return list;
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return new List<CustomerBrandCategoryItem>();
            }
        }

        private List<CustomerBrandCategoryItem> SelectCustomerBrandCategoryByParentId(int v, DbConnector insDbConnector)
        {
            List<CustomerBrandCategoryItem> list = new List<CustomerBrandCategoryItem>();
            datCustomerBrandCategory insDat = new datCustomerBrandCategory();
            entCustomerBrandCategory insEnt = new entCustomerBrandCategory();
            insEnt.ParentId = v;
            DataTable insDt = new DataTable();
            insDt = insDat.SelectCustomerBrandCategoryByParentId(insEnt, insDbConnector);

            foreach (DataRow insDr in insDt.Rows)
            {
                CustomerBrandCategoryItem insItem = new CustomerBrandCategoryItem();
                insItem.id = Convert.ToInt32(insDr["Id"]);
                insItem.text = Convert.ToString(insDr["Name"]);

                insItem.children = SelectCustomerBrandCategoryByParentId(insItem.id, insDbConnector);

                list.Add(insItem);
            }

            return list;
        }
        #endregion
    }

    public class CustomerBrandCategoryItem
    {
        public int id { get; set; }
        public string text { get; set; }
        public List<CustomerBrandCategoryItem> children { get; set; }
    }
}