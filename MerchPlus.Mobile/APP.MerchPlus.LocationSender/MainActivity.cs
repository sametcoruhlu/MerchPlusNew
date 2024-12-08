using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Locations;
using Android.Util;
using System.Collections.Generic;
using System.Linq;

namespace APP.MerchPlus.LocationSender
{
    [Activity(Label = "MerchPlus | GPS Tester", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, ILocationListener
    {

        LocationManager locationManager;
        string locationProvider = string.Empty;

        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };

            Log.Debug("Tag", locationProvider);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // initialise the location manager 
            locationManager = (LocationManager)GetSystemService(LocationService);

            // define its Criteria
            Criteria criteriaForLocationService = new Criteria
            {
                Accuracy = Accuracy.Fine,
                PowerRequirement = Power.NoRequirement
            };

            // find a location provider (GPS, wi-fi, etc.)
            IList<string> acceptableLocationProviders = locationManager.GetProviders(criteriaForLocationService, true);

            // if we have any, use the first one
            if (acceptableLocationProviders.Any())
                locationProvider = acceptableLocationProviders.First();
            else
                locationProvider = string.Empty;
        }

        protected override void OnResume()
        {
            base.OnResume();
            if (locationProvider != string.Empty) locationManager.RequestLocationUpdates(locationProvider, 0, 0, this);

            Toast.MakeText(this, "Tekrar Hoşgeldiniz", ToastLength.Long).Show();

        }         

        protected override void OnPause()
        {
            base.OnPause();
            //locationManager.RemoveUpdates(this);
        }         

        public void OnLocationChanged(Location location)
        {
            try
            {
                if (location == null)
                    Log.Debug("Location Changed", "Location not found");
                else
                {
                    Toast.MakeText(this, "Location : " + String.Format("{0}   {1}", location.Latitude, location.Longitude), ToastLength.Short).Show();
                    //Log.Debug("Location Changed", String.Format("{0}   {1}", location.Latitude, location.Longitude));
                }
            }
            catch(Exception ex)
            {
                Log.Debug("Location Changed - ERROR", ex.ToString());
            } // end catch
        }

        public void OnProviderDisabled(string provider)
        {
            
        }

        public void OnProviderEnabled(string provider)
        {
            
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            
        }
    }
}

