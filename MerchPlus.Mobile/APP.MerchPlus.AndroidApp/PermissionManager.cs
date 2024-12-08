using Android.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.MerchPlus
{
    public static class PermissionManager
    {
        private static string preferenceKey = "permissions_granted";

        public static void SetPermissionsGranted(Context context, bool granted)
        {
            var prefs = context.GetSharedPreferences("app_preferences", FileCreationMode.Private);
            var editor = prefs.Edit();
            editor.PutBoolean(preferenceKey, granted);
            editor.Apply();
        }

        public static bool ArePermissionsGranted(Context context)
        {
            var prefs = context.GetSharedPreferences("app_preferences", FileCreationMode.Private);
            return prefs.GetBoolean(preferenceKey, false);
        }
    }
}
