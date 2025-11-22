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
        
        // Eski projede CoordinateX ve CoordinateY kullanılıyor (decimal)
        // Sunucu bu alanları bekliyor, bu yüzden hem eski hem yeni formatı destekliyoruz
        [Column("CoordinateX")]
        public decimal? CoordinateX { get; set; }
        
        [Column("CoordinateY")]
        public decimal? CoordinateY { get; set; }
        
        // Yeni format (double) - veritabanında saklamak için
        [Column("Latitude")]
        public double? Latitude { get; set; }
        
        [Column("Longitude")]
        public double? Longitude { get; set; }
        
        // Eski projede CreatedOn kullanılıyor
        [Column("CreatedOn")]
        public DateTime? CreatedOn { get; set; }
        
        // Yeni format - LastUpdateTime
        [Column("LastUpdateTime")]
        public DateTime? LastUpdateTime { get; set; }

        public entMemberCoordinate()
        {
            CreatedOn = DateTime.Now;
            LastUpdateTime = DateTime.Now;
        }
    }
} 