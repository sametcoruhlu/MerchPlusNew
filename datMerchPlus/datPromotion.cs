using SqlHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datMerchPlus
{
    public class datPromotion
    {
        public DataTable SelectPromotion(DbConnector parDbConnector)
        {
            return parDbConnector.ExecuteDataTable("SelectPromotionType", null);
        }
    }
}
