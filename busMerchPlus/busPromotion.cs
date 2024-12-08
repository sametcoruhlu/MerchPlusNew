using datMerchPlus;
using SqlHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace busMerchPlus
{
    public class busPromotion : BusBase
    {
        public busPromotion() : base()
        {
        }

        public DataTable SelectPromotionType()
        {
            DbConnector insDbConnector = new DbConnector();
            try
            {
                datPromotion insDatPromotion = new datPromotion();
                return insDatPromotion.SelectPromotion(insDbConnector);
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.ToString();
                return null;
            }
        }
    }
}
