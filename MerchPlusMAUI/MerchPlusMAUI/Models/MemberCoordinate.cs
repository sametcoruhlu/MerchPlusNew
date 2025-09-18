using SQLite;
using System;

namespace MerchPlusMAUI.Services
{
    public class MemberCoordinate
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int MemberId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Accuracy { get; set; }
        public DateTime LastUpdateTime { get; set; }

        public MemberCoordinate()
        {
            LastUpdateTime = DateTime.UtcNow;
        }
    }
} 