using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using Android.Telephony;
using System.Net.Http;
using System.Net.Http.Headers;
using entMerchPlus;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;
using static Android.Views.ViewGroup;
using System.IO;
using SQLite;
using APP.MerchPlus.Entities;
using System.Collections.Generic;
using System.Net;
using Android.Graphics;
using Android.Provider;
using Android.Graphics.Drawables;
using System.Data;
using Java.Nio;

namespace APP.MerchPlus
{
    [Activity(Label = "Raf Fotoðrafý")]
    public class ShopEntryVitrineCameraActivity : Activity
    {

        private string _imageUri;
        Intent intent = new Intent(MediaStore.ActionImageCapture);

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ShopVitrineCamera);
            ImageButton btn = FindViewById<ImageButton>(Resource.Id.ShopVitrineCamera_btnTakePhoto);
            btn.Click += Btn_Click;

            ImageButton btn_displey = FindViewById<ImageButton>(Resource.Id.DisplayCamera_btnTakePhoto);
            btn_displey.Click += Btn_Click_Display;
        }

        private void Btn_Click_Display(object sender, EventArgs e)
        {
            var promotionTypeActivity = new Intent(this, typeof(ShopCameraActivity));
            promotionTypeActivity.PutExtra("entMemberRouteId", Intent.GetStringExtra("entMemberRouteId"));
            StartActivity(promotionTypeActivity);
        }

        private Boolean isMounted
        {
            get
            {
                return Android.OS.Environment.ExternalStorageState.Equals(Android.OS.Environment.MediaMounted);
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == Result.Ok && requestCode == 1001)
            {
                Android.Net.Uri _currentImageUri = Android.Net.Uri.Parse(_imageUri);
                Bitmap bitmap = BitmapFactory.DecodeStream(ContentResolver.OpenInputStream(_currentImageUri));

                byte[] bitmapData = null;

                using (MemoryStream stream = new MemoryStream())
                {
                    bitmap.Compress(Bitmap.CompressFormat.Jpeg, 70, stream);
                    bitmapData = stream.ToArray();

                    var str = Convert.ToBase64String(bitmapData);
                    var db = new SQLiteConnection(GlobalVariables.DatabasePath);
                    var entity = db.Query<entMemberRoute>("SELECT * FROM entMemberRoute WHERE Id=?", Convert.ToInt32(Intent.GetStringExtra("entMemberRouteId")));
                    entMemberRoutePhoto insEnt = new entMemberRoutePhoto();
                    insEnt.CreatedOn = DateTime.Now;
                    insEnt.IsSentToServer = false;
                    insEnt.MemberId = entity[0].MemberId;
                    insEnt.MemberRouteId = entity[0].Id;
                    insEnt.ProfilePicturePath = str;
                    db.Insert(insEnt);

                    this.Finish();

                }

                bitmap.Dispose();
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            var uri = ContentResolver.Insert(isMounted
                                             ? MediaStore.Images.Media.ExternalContentUri
                                             : MediaStore.Images.Media.InternalContentUri, new ContentValues());
            _imageUri = uri.ToString();
            intent.PutExtra(MediaStore.ExtraOutput, uri);
            StartActivityForResult(intent, 1001);
        }
    }
}