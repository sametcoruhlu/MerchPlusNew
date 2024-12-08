using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Data;
using System.Configuration;

namespace Infrastructure
{
    public class ConStr
    {
        public static string DbConStr
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            }
        }

        public static string TrackingDbConStr
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            }
        }
        public static readonly string ApplicationVersion = "Version 1.0";
    }
}
