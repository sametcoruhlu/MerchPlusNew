using SQLite;

namespace MerchPlusMAUI.Models
{
    [SQLite.Table("entConfiguration")]
    public class entConfiguration
    {
        [PrimaryKey]
        [Column("Name")]
        public string Name { get; set; } = string.Empty;
        
        [Column("Value")]
        public string Value { get; set; } = string.Empty;

        public entConfiguration()
        {
        }
    }
} 