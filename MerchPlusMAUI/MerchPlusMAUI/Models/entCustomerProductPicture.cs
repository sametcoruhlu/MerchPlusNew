using SQLite;

namespace entMerchPlus
{
    [SQLite.Table("entCustomerProductPicture")]
    public class entCustomerProductPicture
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        [Column("CustomerProductId")]
        public int CustomerProductId { get; set; }
        
        [Column("CustomerProductPictureBase64")]
        public string CustomerProductPictureBase64 { get; set; } = string.Empty;

        public entCustomerProductPicture()
        {
        }
    }
} 