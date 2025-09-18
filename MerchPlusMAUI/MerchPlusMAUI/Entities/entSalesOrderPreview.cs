namespace MerchPlusMAUI.Entities
{
    public class entSalesOrderPreview
    {
        public entSalesOrderPreview()
        {
            Id = 0;
            ProductName = string.Empty;
            Quantity = 0;
            PackageTypeName = string.Empty;
        }

        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string PackageTypeName { get; set; }

    }
}