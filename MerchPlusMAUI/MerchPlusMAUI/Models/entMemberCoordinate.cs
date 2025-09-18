using SQLite;
using System;

namespace MerchPlusMAUI.Models
{
    [SQLite.Table("entMemberCoordinate")]
    public class entMemberCoordinate
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        [Column("MemberId")]
        public string MemberId { get; set; } = string.Empty;
        
        [Column("Latitude")]
        public double Latitude { get; set; }
        
        [Column("Longitude")]
        public double Longitude { get; set; }
        
        [Column("LastUpdateTime")]
        public DateTime LastUpdateTime { get; set; }

        public entMemberCoordinate()
        {
            LastUpdateTime = DateTime.UtcNow;
        }
    }
} 