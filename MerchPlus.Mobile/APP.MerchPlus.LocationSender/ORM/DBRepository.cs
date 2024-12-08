using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;


using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace APP.MerchPlus.LocationSender.ORM
{
    public class DBRepository
    {
        public string CreateDatabase()
        {
            var output = string.Empty;
            output += "Creating Database\n";
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "MerchPlus.db3");
            var insSqlConnection = new SQLiteConnection(dbPath);
            output += "Database created (MerchPlus.db3)";
            return output;
        }
    }
}