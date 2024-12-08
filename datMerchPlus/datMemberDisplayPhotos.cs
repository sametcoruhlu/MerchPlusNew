using entMerchPlus;
using SqlHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datMerchPlus
{
    public class datMemberDisplayPhotos
    {
        public datMemberDisplayPhotos()
        {
        }

        /// <summary>
        /// Method that inserts one row of data into table [Area] based on the values of input entity object
        /// </summary>
        /// <param name="parEntMemberDisplayPhotos">Entity object as parameter for tableArea]</param>
        /// <param name="parDbConnector">DbConnector instance carried from Business Layer</param>
        public void InsertMemberDisplayPhotos(entMemberDisplayPhotos parEntMemberDisplayPhotos, DbConnector parDbConnector)
        {
            DbParamCollection insDbParamCollection = new DbParamCollection();
            insDbParamCollection.AddOutput("@pId", DbType.Int32);
            insDbParamCollection.Add("@pDisplayPicturePath", parEntMemberDisplayPhotos.DisplayPicturePath);
            insDbParamCollection.Add("@pIsSentToServer", parEntMemberDisplayPhotos.IsSentToServer);
            insDbParamCollection.Add("@pCustomerProductId", parEntMemberDisplayPhotos.CustomerProductId);
            insDbParamCollection.Add("@pCreatedOn", parEntMemberDisplayPhotos.CreatedOn);
            insDbParamCollection.Add("@pMemberId", parEntMemberDisplayPhotos.MemberId);
            insDbParamCollection.Add("@pMemberRouteId", parEntMemberDisplayPhotos.MemberRouteId);
            insDbParamCollection.Add("@pPromotionTypeId", parEntMemberDisplayPhotos.PromotionTypeId);
            parDbConnector.ExecuteNonQuery("InsertMemberDisplayPhotos", insDbParamCollection);
            parEntMemberDisplayPhotos.Id = Convert.ToInt32(insDbParamCollection.GetOutPutParameter().Value);
        }

    }
}
