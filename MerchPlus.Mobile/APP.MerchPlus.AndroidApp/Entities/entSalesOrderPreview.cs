using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace APP.MerchPlus.Entities
{
    public class entSalesOrderPreview
    {
        public entSalesOrderPreview()
        {
            Id = 0;
            ProductName = string.Empty;
            Quantity = 0;
            PackageTypeName = string.Empty;
        }

        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string PackageTypeName { get; set; }

    }
}