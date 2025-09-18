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

        public int Id { get; set; }
        public string ProductName { get; set; }
        public int MemberRouteId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime? ExpirationDate { get; set; }

    }
}