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
    public class entMemberDisplayPhotos
    {
        public int Id { get; set; }

        public string MemberId { get; set; }

        public int MemberRouteId { get; set; }

        public string DisplayPicturePath { get; set; }

        public int PromotionTypeId { get; set; }

        public bool? IsSentToServer { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int CustomerProductId { get; set; }
    }
}
