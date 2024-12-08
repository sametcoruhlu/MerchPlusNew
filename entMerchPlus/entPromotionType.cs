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
    public class entPromotionType
    {
        public int Id { get; set; }

        public string Name   { get; set; }
    }
}
