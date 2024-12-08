using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.MerchPlus
{
    public class PermissionsHelper
    {
        private static string[] permissions = {
        Manifest.Permission.Camera,
        Manifest.Permission.ReadExternalStorage,
        Manifest.Permission.WriteExternalStorage,
        Manifest.Permission.AccessFineLocation,
        Manifest.Permission.AccessCoarseLocation,
        Manifest.Permission.RecordAudio,
        Manifest.Permission.CallPhone,
        Manifest.Permission.ReadPhoneState
    };

        public static void RequestPermissionsIfNecessary(Activity activity)
        {
            List<string> permissionsToRequest = new List<string>();

            foreach (var permission in permissions)
            {
                if (ContextCompat.CheckSelfPermission(activity, permission) != (int)Permission.Granted)
                {
                    permissionsToRequest.Add(permission);
                }
            }

            if (permissionsToRequest.Count > 0)
            {
                ActivityCompat.RequestPermissions(activity, permissionsToRequest.ToArray(), 1);
            }
        }

        public static bool ArePermissionsGranted(Context context)
        {
            foreach (var permission in permissions)
            {
                if (ContextCompat.CheckSelfPermission(context, permission) != (int)Permission.Granted)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
