using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entMerchPlus
{
    [Serializable()]
    [JsonObject(MemberSerialization.OptOut)]
    public class entMember
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string NameSurname { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string DataNumber { get; set; }
        public DateTime? HiringDate { get; set; }
        public DateTime? LeavingDate { get; set; }
        public bool IsActive { get; set; }
        public string Address { get; set; }
        public int DeviceModelId { get; set; }
        public string IMEINumber { get; set; }
        public string ProfilePicturePath { get; set; }
        public int MemberTitleId { get; set; }
        public int AddressCityId { get; set; }
        public decimal AddressCoordinateX { get; set; }
        public decimal AddressCoordinateY { get; set; }
        public string DirectReportId { get; set; }
        public string Password { get; set; }
        public string PictureData { get; set; }
    }
}
