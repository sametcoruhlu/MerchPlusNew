using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.MerchPlus.Models
{
    public class MemberRouteRequest
    {
        public List<MemberRoute> MemberRouteList { get; set; }
    }

    public class MemberRoute
    {
        public int MemberRouteId { get; set; }
        public string MerchNameSurname { get; set; }
        public string RetailShopName { get; set; }
        public string DisplayPicturePath { get; set; }
    }
}