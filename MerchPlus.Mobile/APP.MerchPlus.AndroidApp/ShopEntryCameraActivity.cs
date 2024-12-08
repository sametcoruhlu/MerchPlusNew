using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Widget;
using entMerchPlus;
using SQLite;
using System;
using System.IO;

namespace APP.MerchPlus
{
    [Activity(Label = "Maðaza Fotoðrafý")]
    public class ShopEntryCameraActivity : Activity
    {
        private string _imageUri;
        Intent intent = new Intent(MediaStore.ActionImageCapture);

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ShopEntryCamera);
            ImageButton btn = FindViewById<ImageButton>(Resource.Id.ShopEntryCamera_btnTakePhoto);
            btn.Click += Btn_Click;
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
                    db.Execute("UPDATE entMemberRoute SET EntryImageData=? WHERE Id=?", str, Convert.ToInt32(Intent.GetStringExtra("entMemberRouteId")));


                }

                bitmap.Dispose();
            }

            var customerProductListActivity = new Intent(this, typeof(CustomerProductList));
            customerProductListActivity.PutExtra("entMemberRouteId", Convert.ToString(Intent.GetStringExtra("entMemberRouteId")));
            StartActivity(customerProductListActivity);
            this.Finish();

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