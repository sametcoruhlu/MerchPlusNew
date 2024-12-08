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
    [RoutePrefix("api/SalesOrderController")]
    public class SalesOrderController : ApiController
    {

        [HttpPost]
        [AllowAnonymous()]
        [Route("SelectSalesOrderGridDataById")]
        public JObject SelectSalesOrderGridDataById(JObject data)
        {

            JObject returnJson;
            dynamic json = data;

            #region Business Logic 
            entSalesOrder insEntSalesOrder = new entSalesOrder();
            busSalesOrder insBusSalesOrder = new busSalesOrder();
            entSalesOrderDetail insEntSalesOrderDetail = new entSalesOrderDetail();
            busSalesOrderDetail insBusSalesOrderDetail = new busSalesOrderDetail();

            insEntSalesOrder.Id = Convert.ToInt32(json.Id);
            DataTable insDt_SalesOrder = new DataTable();
            insDt_SalesOrder = insBusSalesOrder.SelectSalesOrderByIdGridData(insEntSalesOrder);

            insEntSalesOrderDetail.SalesOrderId = insEntSalesOrder.Id;
            DataTable insDt_SalesOrderDetail = new DataTable();
            insDt_SalesOrderDetail = insBusSalesOrderDetail.SelectSalesOrderDetailBySalesOrderIdGridData(insEntSalesOrderDetail);

            if (insBusSalesOrder.HasErrors || insBusSalesOrderDetail.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusSalesOrder.ErrorMessage + " " + insBusSalesOrderDetail.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK"),
                                        new JProperty("SalesOrder", JArray.Parse(JsonConvert.SerializeObject(insDt_SalesOrder))),
                                        new JProperty("SalesOrderDetail", JArray.Parse(JsonConvert.SerializeObject(insDt_SalesOrderDetail)))
                                        );
            return returnJson;
            #endregion
        }

        [HttpPost]
        [AllowAnonymous()]
        [Route("MarkSalesOrderAsShipped")]
        public JObject MarkSalesOrderAsShipped(JObject data)
        {
            JObject returnJson;
            dynamic json = data;

            busSalesOrder insBusSalesOrder = new busSalesOrder();
            entSalesOrder insEntSalesOrder = new entSalesOrder();
            insEntSalesOrder.Id = Convert.ToInt32(json.Id);
            insBusSalesOrder.SelectSalesOrderById(insEntSalesOrder);

            insEntSalesOrder.ShippedBy = Convert.ToString(json.MemberId);
            insEntSalesOrder.ShippedOn = DateTime.Now;
            insEntSalesOrder.SalesOrderStatusId = 2; //Shipped
            insBusSalesOrder.UpdateSalesOrderById(insEntSalesOrder);

            if (insBusSalesOrder.HasErrors)
            {
                returnJson = new JObject(new JProperty("Result", "NOK"),
                                            new JProperty("Reason", "ERROR OCCURED DURING PROCESS"),
                                            new JProperty("Details", insBusSalesOrder.ErrorMessage)
                                            );
                return returnJson;
            }
            returnJson = new JObject(
                                        new JProperty("Result", "OK")
                                        );
            return returnJson;
        }

    }
}
