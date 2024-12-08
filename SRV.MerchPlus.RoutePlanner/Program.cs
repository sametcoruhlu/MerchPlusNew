using busMerchPlus;
using entMerchPlus;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRV.MerchPlus.RoutePlanner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("MerchPlus | Daily Route Planner");
            Console.WriteLine("-------------------------------");

            #region Get all routes planned for today
            entMemberRoute insEntMemberRoute = new entMemberRoute();
            busMemberRoute insBusMemberRoute = new busMemberRoute();
            //insEntMemberRoute.EffectiveDate = new DateTime(DateTime.Now.AddDays(1).Year, DateTime.Now.AddDays(1).Month, DateTime.Now.AddDays(1).Day);
            insEntMemberRoute.EffectiveDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1);
            DataTable insDt = new DataTable();
            insDt = insBusMemberRoute.SelectMemberRouteByEffectiveDate(insEntMemberRoute);
            #endregion

            #region Loop each MemberRoute, find products and insert into MemberRouteDetail table
            foreach (DataRow insDr_MemberRoute in insDt.Rows)
            {
                entMember insEntMember = new entMember();
                busMember insBusMember = new busMember();
                insEntMember.Id = Convert.ToString(insDr_MemberRoute["MemberId"]);
                insBusMember.SelectMemberById(insEntMember);

                entRetailShop insEntRetailShop = new entRetailShop();
                busRetailShop insBusRetailShop = new busRetailShop();
                insEntRetailShop.Id = Convert.ToInt32(insDr_MemberRoute["RetailShopId"]);
                insBusRetailShop.SelectRetailShopById(insEntRetailShop);

                Console.WriteLine(insEntMember.NameSurname + " - " + insEntRetailShop.Name);
                entMemberRouteDetail insEntMemberRouteDetail = new entMemberRouteDetail();
                busMemberRouteDetail insBusMemberRouteDetail = new busMemberRouteDetail();
                insEntMemberRouteDetail.MemberRouteId = Convert.ToInt32(insDr_MemberRoute["Id"]);
                insBusMemberRouteDetail.CreateMemberRouteDetailWiselyByMemberRouteId(insEntMemberRouteDetail);
            }
            #endregion
        }
    }
}
