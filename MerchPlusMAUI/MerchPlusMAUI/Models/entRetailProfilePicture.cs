using SQLite;

namespace MerchPlusMAUI.Models
{
    [SQLite.Table("entRetailProfilePicture")]
    public class entRetailProfilePicture
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        [Column("RetailId")]
        public int RetailId { get; set; }
        
        [Column("ProfilePictureBase64")]
        public string ProfilePictureBase64 { get; set; } = string.Empty;

        public entRetailProfilePicture()
        {
        }
    }
} 