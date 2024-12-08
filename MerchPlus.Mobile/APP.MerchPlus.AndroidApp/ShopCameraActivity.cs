using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Media;
using Android.OS;
using Android.Provider;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.Core.Content;
using APP.MerchPlus.Adapters;
using APP.MerchPlus.Entities;
using entMerchPlus;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace APP.MerchPlus
{
    [Activity(Label = "Raf ve Teşhir Fotoğrafı")]
    public class ShopCameraActivity : Activity
    {
        private TaskCompletionSource<string> _cameraTaskCompletionSource;
        private string _imageUri;
        List<entPromotionType> listPromotionType = new List<entPromotionType>();
        entPromotionType entPromotionTypeSelected = new entPromotionType();
        List<entCustomerProduct> listCustomerProduct = new List<entCustomerProduct>();
        List<entCustomerProduct> selectedProducts = new List<entCustomerProduct>();
        List<DisplayPhotoItem> displayPhotoList = new List<DisplayPhotoItem>();
        ListView photoListView;
        TextView promotionTextView;
        string routeId = string.Empty;
        private List<int> selectedIds;
        private TextView productSelector;
        private TextView promotionSelector;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ShopCamera);

            // Spinner ve diğer UI elemanlarını yükle
            LoadPromotionSpinner();
            LoadProductSelection();

            // ListView yükleme
            photoListView = FindViewById<ListView>(Resource.Id.photo_list_view);

            promotionTextView = FindViewById<TextView>(Resource.Id.promotion_label);

            // Fotoğraf çek butonu
            ImageButton btn = FindViewById<ImageButton>(Resource.Id.DisplayPhotoCamera_btnTakePhoto);
            btn.Click += async (sender, e) => await Btn_ClickAsync();

            // Kaydet butonu
            Button saveButton = FindViewById<Button>(Resource.Id.save_button);
            saveButton.Click += SaveButton_Click;
        }

        private void LoadPromotionSpinner()
        {
            using (var db = new SQLiteConnection(GlobalVariables.DatabasePath))
            {
                var promotionTypes = db.Query<entPromotionType>("SELECT * FROM entPromotionType");
                var placeholderItem = new entPromotionType { Id = -1, Name = "Seçiniz" };
                promotionTypes.Insert(0, placeholderItem);

                listPromotionType = promotionTypes;
                var adapter = new PromotionTypeSpinnerAdapter(this, promotionTypes);

                promotionSelector = FindViewById<TextView>(Resource.Id.PromotionType_wlePromotionTypeId);
                //spinnerPromotionType.Adapter = adapter;
                //spinnerPromotionType.SetSelection(0);
                //spinnerPromotionType.ItemSelected += Spinner_ItemSelected;

                promotionSelector.Click += (s, e) =>
                {
                    ShowPromotionSelectionDialog();
                };
            }
        }

        private void ShowPromotionSelectionDialog()
        {
            // Tüm teşhir türlerinin isimlerini alıyoruz
            string[] promotionNames = listPromotionType.Select(p => p.Name).ToArray();

            // Varsayılan olarak seçili olan öğeyi belirliyoruz
            int selectedIndex = listPromotionType.IndexOf(entPromotionTypeSelected);

            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.SetTitle("Teşhir Tipi Seçiniz");

            // Tek seçim için SetSingleChoiceItems kullanılıyor
            builder.SetSingleChoiceItems(promotionNames, selectedIndex, (sender, args) =>
            {
                // Yeni seçilen öğeyi güncelliyoruz
                entPromotionTypeSelected = listPromotionType[args.Which];
                
            });

            builder.SetPositiveButton("Tamam", (sender, args) =>
            {
                // Kullanıcı seçim yaptıktan sonra seçilen öğeyi ekrana yazdırıyoruz
                if (entPromotionTypeSelected.Id != -1)
                {
                    promotionTextView.Text = $"Seçilen Teşhir Tipi: {entPromotionTypeSelected.Name}";
                }
                else
                {
                    promotionTextView.Text = "Lütfen bir teşhir tipi seçiniz.";
                }
            });

            builder.SetNegativeButton("İptal", (sender, args) =>
            {
                // İptal edilirse herhangi bir işlem yapılmaz
            });

            builder.Show();
        }


        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            entPromotionTypeSelected = listPromotionType[e.Position];
            promotionTextView.Text = entPromotionTypeSelected.Name;
        }

        private void LoadProductSelection()
        {
            using (var db = new SQLiteConnection(GlobalVariables.DatabasePath))
            {
                routeId = Intent.GetStringExtra("entMemberRouteId");
                var currentMemberRoute = db.Query<entMemberRoute>("SELECT * from entMemberRoute WHERE Id=?", routeId);
                var products = db.Query<entCustomerProduct>("SELECT * FROM entCustomerProduct WHERE Id IN (SELECT CustomerProductId FROM entMemberRouteDetail WHERE MemberRouteId=?) AND CustomerId=?", routeId, currentMemberRoute[0].CustomerId);

                listCustomerProduct = products;

                productSelector = FindViewById<TextView>(Resource.Id.CustomerProduct_wleCustomerProductId);
                productSelector.Click += (s, e) =>
                {
                    ShowProductSelectionDialog();
                };
            }
        }

        private void ShowProductSelectionDialog()
        {
            string[] productNames = listCustomerProduct.Select(p => p.Name).ToArray();
            bool[] checkedItems = listCustomerProduct.Select(p => selectedProducts.Contains(p)).ToArray();

            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.SetTitle("Ürün Seçiniz");
            builder.SetMultiChoiceItems(productNames, checkedItems, (sender, args) =>
            {
                if (args.IsChecked)
                {
                    if (!selectedProducts.Contains(listCustomerProduct[args.Which]))
                        selectedProducts.Add(listCustomerProduct[args.Which]);
                }
                else
                {
                    selectedProducts.Remove(listCustomerProduct[args.Which]);
                }
            });

            builder.SetPositiveButton("Tamam", (sender, args) =>
            {
                selectedIds = selectedProducts.Select(p => p.Id).ToList();
                productSelector.Text = $"Seçilen Ürünler: {string.Join(", ", selectedProducts.Select(p => p.Name))}";
            });

            builder.SetNegativeButton("İptal", (sender, args) => { });

            builder.Show();
        }

        private async Task Btn_ClickAsync()
        {
            if (entPromotionTypeSelected.Id == -1)
            {
                ShowAlert("Promosyon", "Lütfen bir promosyon tipi seçiniz!");
                return;
            }

            if (selectedProducts.Count == 0)
            {
                ShowAlert("Ürün", "Lütfen en az bir ürün seçiniz!");
                return;
            }

            Android.Net.Uri photoUri = CreateUniqueUri();
            _imageUri = photoUri.ToString();

            Intent intent = new Intent(MediaStore.ActionImageCapture);
            intent.PutExtra(MediaStore.ExtraOutput, photoUri);

            try
            {
                string result = await StartCameraAsync(intent);
                ProcessCapturedPhoto();
            }
            catch (Exception ex)
            {
                ShowAlert("Hata", $"Fotoğraf çekimi başarısız: {ex.Message}");
            }
        }

        private Task<string> StartCameraAsync(Intent intent)
        {
            _cameraTaskCompletionSource = new TaskCompletionSource<string>();
            StartActivityForResult(intent, 1001);
            return _cameraTaskCompletionSource.Task;
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == 1001)
            {
                if (resultCode == Result.Ok)
                {
                    _cameraTaskCompletionSource?.TrySetResult(_imageUri);
                }
                else
                {
                    _cameraTaskCompletionSource?.TrySetException(new Exception("Kamera iptal edildi."));
                }
            }
        }

        private void ProcessCapturedPhoto()
        {
            Android.Net.Uri currentImageUri = Android.Net.Uri.Parse(_imageUri);
            Bitmap bitmap = BitmapFactory.DecodeStream(ContentResolver.OpenInputStream(currentImageUri));

            foreach (var product in selectedProducts)
            {
                DisplayPhotoItem newItem = new DisplayPhotoItem
                {
                    PromotionTypeId = entPromotionTypeSelected.Id,
                    PromotionType = entPromotionTypeSelected.Name,
                    ProductId = product.Id,
                    ProductName = product.Name,
                    PhotoBitmap = bitmap
                };

                displayPhotoList.Add(newItem);
            }

            UpdatePhotoListView();
        }

        private void UpdatePhotoListView()
        {
            photoListView.Adapter = new PhotoListAdapter(this, displayPhotoList);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            // Verileri kaydet
            using (var db = new SQLiteConnection(GlobalVariables.DatabasePath))
            {
                var entity = db.Query<entMemberRoute>("SELECT * FROM entMemberRoute WHERE Id=?", Convert.ToInt32(Intent.GetStringExtra("entMemberRouteId")));
                foreach (var photoItem in displayPhotoList)
                {
                    byte[] bitmapData = null;
                    using (MemoryStream stream = new MemoryStream())
                    {
                        photoItem.PhotoBitmap.Compress(Bitmap.CompressFormat.Jpeg, 70, stream);
                        bitmapData = stream.ToArray();

                        // Veritabanına kaydetme işlemi
                        var photo = Convert.ToBase64String(bitmapData);
                        var photoEntity = new entMemberDisplayPhotos
                        {
                            PromotionTypeId = photoItem.PromotionTypeId,
                            CustomerProductId = photoItem.ProductId,
                            DisplayPicturePath = photo,
                            IsSentToServer = false,
                            MemberId = entity[0].MemberId,
                            MemberRouteId = entity[0].Id,
                            CreatedOn = DateTime.Now,
                        };

                        db.Insert(photoEntity);
                    }

                }
                //photoItem.PhotoBitmap.Dispose(); // Belleği serbest bırak
            }
            Finish();
        }

        private void ShowAlert(string title, string message)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle(title);
            alert.SetMessage(message);
            alert.SetPositiveButton("Tamam", (s, e) => { });
            alert.Show();
        }

        private Android.Net.Uri CreateUniqueUri()
        {
            string fileName = $"IMG_{DateTime.Now:yyyyMMdd_HHmmss}.jpg";
            Java.IO.File storageDir = GetExternalFilesDir(Android.OS.Environment.DirectoryPictures);
            if (storageDir != null && !storageDir.Exists()) storageDir.Mkdirs();
            Java.IO.File photoFile = new Java.IO.File(storageDir, fileName);
            return FileProvider.GetUriForFile(this, $"{PackageName}.fileprovider", photoFile);
        }

        #region Custom Spinner
        public class PromotionTypeSpinnerAdapter : BaseAdapter<entPromotionType>
        {
            private readonly List<entPromotionType> _promotionType;
            private readonly Activity _activity;

            public PromotionTypeSpinnerAdapter(Activity activity, List<entPromotionType> incomingData)
            {
                _promotionType = incomingData;
                _activity = activity;
            }

            public override int Count => _promotionType.Count;

            public override entPromotionType this[int position] => _promotionType[position];

            public override long GetItemId(int position) => _promotionType[position].Id;

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                var view = convertView ?? _activity.LayoutInflater.Inflate(Resource.Layout.CustomSpinnerItem, parent, false);
                var textView = view.FindViewById<TextView>(Resource.Id.spinnerItemText);
                textView.Text = _promotionType[position].Name;
                return view;
            }

            public override View GetDropDownView(int position, View convertView, ViewGroup parent)
            {
                var view = convertView ?? _activity.LayoutInflater.Inflate(Resource.Layout.CustomSpinnerDropdownItem, parent, false);
                var textView = view.FindViewById<TextView>(Resource.Id.spinnerDropdownText);
                textView.Text = _promotionType[position].Name;
                return view;
            }
        }
        #endregion
    }
}
