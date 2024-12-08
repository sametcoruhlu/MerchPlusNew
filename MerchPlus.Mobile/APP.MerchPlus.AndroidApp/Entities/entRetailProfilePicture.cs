using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Sql;

namespace APP.MerchPlus.Entities
{
    public class entRetailProfilePicture
    {
        public entRetailProfilePicture()
        {

        }

        public int RetailId { get; set; }
        public string ProfilePictureBase64 { get; set; }

    }
}