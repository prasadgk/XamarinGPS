using Android.App;
using Android.OS;
using GpsServices.ViewModels;
using MvvmCross.Platforms.Android.Views;
using Android.Locations;
using Android.Runtime;
using Android.Widget;
using System.Collections.Generic;
using Android.Util;
using System.Linq;

namespace GPSTracker.UI.Droid.Views
{
    [Activity(Label = "Tip Calculator App", MainLauncher = true)]
    public class TipView : MvxActivity<TipViewModel>, ILocationListener
    {
        TextView txtlatitu;
        TextView txtlong;
        Location currentLocation;
        LocationManager locationManager;
        string locationProvider;
        public string TAG
        {
            get;
            private set;
        }

        public void OnLocationChanged(Location location)
        {
            //throw new System.NotImplementedException();
            currentLocation = location;
            if (currentLocation == null)
            {
                //Error Message  
            }
            else
            {
                txtlatitu.Text = currentLocation.Latitude.ToString();
                txtlong.Text = currentLocation.Longitude.ToString();
            }
        }

        public void OnProviderDisabled(string provider)
        {
            //throw new System.NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
            //throw new System.NotImplementedException();
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            //throw new System.NotImplementedException();
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.TipView);

            txtlatitu = FindViewById<TextView>(Resource.Id.txtlatitude);
            txtlong = FindViewById<TextView>(Resource.Id.txtlong);
            InitializeLocationManager();
        }
        private void InitializeLocationManager()
        {
            locationManager = (LocationManager)GetSystemService(LocationService);
            Criteria criteriaForLocationService = new Criteria
            {
                Accuracy = Accuracy.Fine
            };
            IList<string> acceptableLocationProviders = locationManager.GetProviders(criteriaForLocationService, true);
            if (acceptableLocationProviders.Any())
            {
                locationProvider = acceptableLocationProviders.First();
            }
            else
            {
                locationProvider = string.Empty;
            }
            Log.Debug(TAG, "Using " + locationProvider + ".");
        }

        protected override void OnResume()
        {
            base.OnResume();
            locationManager.RequestLocationUpdates(locationProvider, 0, 0, this);
        }
        protected override void OnPause()
        {
            base.OnPause();
            locationManager.RemoveUpdates(this);
        }
    }
}