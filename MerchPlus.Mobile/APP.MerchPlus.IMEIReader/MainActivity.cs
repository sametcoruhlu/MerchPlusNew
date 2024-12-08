using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Telephony;
using System.Net.Http;
using System.Net.Http.Headers;
using entMerchPlus;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;
using Android;

namespace APP.MerchPlus.IMEIReader
{
    [Activity(Label = "IMEIReader", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += Button_Click;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            //var telephonyManager = (TelephonyManager)GetSystemService(TelephonyService);
            //var id = telephonyManager.DeviceId;
            //TextView txtIMEI = FindViewById<TextView>(Resource.Id.txtIMEI);
            //txtIMEI.Text = id;

            //TextView txtData = FindViewById<TextView>(Resource.Id.txtData);
            //txtData.Text = string.Empty;

            //using (var client = new HttpClient())
            //{
            //    //setup client
            //    //client.BaseAddress = new Uri("http://mp.bsmyazilim.com/api/api/AndroidTestController/");
            //    client.BaseAddress = new Uri("http://192.168.1.130/API.MerchPlus/api/AndroidTestController/");
            //    client.DefaultRequestHeaders.Accept.Clear();
            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //    //make request
            //    entMember insEntMember = new entMember();
            //    insEntMember.IMEINumber = id;
            //    var request = new StringContent(JsonConvert.SerializeObject(insEntMember), Encoding.UTF8, "application/json");
            //    HttpResponseMessage response = client.PostAsync("SelectMemberByIMEINumber", request).Result;
            //    var responseString = response.Content.ReadAsStringAsync().Result;

            //    txtData.Text = responseString;

            //}
        }
    }
}

