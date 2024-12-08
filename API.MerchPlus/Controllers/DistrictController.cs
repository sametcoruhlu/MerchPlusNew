using busMerchPlus;
using entMerchPlus;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.MerchPlus.Controllers
{
    [Authorize]
    [RoutePrefix("api/DistrictController")]
    public class DistrictController : ApiController
    {

        [HttpPost]
        [Route("SelectDistrictGridData")]
        public JObject SelectDistrictGridData(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            #region Business Logic MemberModule
            busDistrict insBusDistrict = new busDistrict();
            busCity insBusCity = new busCity();

            DataTable insDt_City = new DataTable();
            insDt_City = insBusCity.SelectCity();

            DataTable insDt_New = new DataTable();
            insDt_New.Columns.Add("id", typeof(string));
            insDt_New.Columns.Add("parent", typeof(string));
            insDt_New.Columns.Add("text", typeof(string));
            insDt_New.Columns.Add("value", typeof(int));
            insDt_New.Columns.Add("icon", typeof(string));
            insDt_New.Columns.Add("li_attr", typeof(string));

            foreach (DataRow insDr_City in insDt_City.Rows)
            {

                DataRow insDr_Parent = insDt_New.NewRow();
                insDr_Parent["id"] = "city" + Convert.ToString(insDr_City["Id"]);
                insDr_Parent["parent"] = "#";
                insDr_Parent["text"] = Convert.ToString(insDr_City["Name"]);
                insDr_Parent["value"] = Convert.ToInt32(insDr_City["Id"]);
                insDr_Parent["icon"] = Convert.ToString("Plugins/jsTree/parent.png");
                insDr_Parent["li_attr"] = Convert.ToString("{ 'style': 'font-weight: bold;'}");
                insDt_New.Rows.Add(insDr_Parent);
            }

            foreach (DataRow insDr_City in insDt_City.Rows)
            {
                entDistrict insEntDistrict = new entDistrict();
                insEntDistrict.CityId = Convert.ToInt32(insDr_City["Id"]);
                DataTable insDt_District = new DataTable();
                insDt_District = insBusDistrict.SelectDistrictByCityId(insEntDistrict);

                foreach (DataRow insDr_District in insDt_District.Rows)
                {
                    DataRow insDr_Child = insDt_New.NewRow();
                    insDr_Child["id"] = Convert.ToString(insDr_District["Id"]);
                    insDr_Child["parent"] = "city" + Convert.ToString(insDr_City["Id"]);
                    insDr_Child["text"] = Convert.ToString(insDr_District["Name"]);
                    insDr_Child["value"] = Convert.ToInt32(insDr_District["Id"]);
                    insDr_Child["icon"] = Convert.ToString("Plugins/jsTree/child.png");
                    insDt_New.Rows.Add(insDr_Child);
                }
            }
            insDt_New.AcceptChanges();

            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", JArray.Parse(JsonConvert.SerializeObject(insDt_New)))
                                        );
            return returnJson;
            #endregion
        }

        [HttpPost]
        [Route("SelectDistrictByCityId")]
        public JObject SelectDistrictByCityId(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            #region Business Logic MemberModule
            busDistrict insBusDistrict = new busDistrict();
            entDistrict insEntDistrict = new entDistrict();
            insEntDistrict.CityId = Convert.ToInt32(json.CityId);
            DataTable insDt = new DataTable();
            insDt = insBusDistrict.SelectDistrictByCityId(insEntDistrict);

            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("Content", JArray.Parse(JsonConvert.SerializeObject(insDt)))
                                        );
            return returnJson;
            #endregion
        }
    }
}
