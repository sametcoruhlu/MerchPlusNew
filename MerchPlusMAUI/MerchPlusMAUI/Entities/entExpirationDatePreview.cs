using SQLite;

namespace MerchPlusMAUI.Entities
{
    public class entExpirationDatePreview
    {
        public entExpirationDatePreview()
        {
            Id = 0;
            ProductName = string.Empty;
            ProductId = 0;
            Quantity = 0;
            ExpirationDate = null;
            MemberRouteId = 0;
        }

        [SQLite.Column("Id")]
        public int Id { get; set; }
        
        [SQLite.Column("ProductName")]
        public string ProductName { get; set; }
        
        [SQLite.Column("MemberRouteId")]
        public int MemberRouteId { get; set; }
        
        [SQLite.Column("ProductId")]
        public int ProductId { get; set; }
        
        [SQLite.Column("Quantity")]
        public int Quantity { get; set; }
        
        [SQLite.Column("ExpirationDate")]
        public DateTime? ExpirationDate { get; set; }

    }
}