using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Devices;
using Microsoft.Maui.ApplicationModel;
#if ANDROID
using Android.Content;
using Android.OS;
using Android.Provider;
using AndroidNet = Android.Net;
using JavaIO = Java.IO;
using AndroidEnvironment = Android.OS.Environment;
#endif

namespace MerchPlusMAUI.Services
{
    public interface IPhotoGalleryService
    {
        Task<bool> SavePhotoToGalleryAsync(byte[] photoBytes, string fileName);
    }

    public class PhotoGalleryService : IPhotoGalleryService
    {
        public async Task<bool> SavePhotoToGalleryAsync(byte[] photoBytes, string fileName)
        {
#if ANDROID
            return await SavePhotoToGalleryAndroidAsync(photoBytes, fileName);
#else
            // iOS ve diğer platformlar için şimdilik false döndür
            return await Task.FromResult(false);
#endif
        }

#if ANDROID
        private async Task<bool> SavePhotoToGalleryAndroidAsync(byte[] photoBytes, string fileName)
        {
            try
            {
                // Android.Content.Context al
                var context = Platform.CurrentActivity ?? Android.App.Application.Context;
                if (context == null)
                {
                    System.Diagnostics.Debug.WriteLine("PhotoGalleryService: Android context bulunamadı");
                    return false;
                }

                // Android 10+ (API 29+) için MediaStore API kullan
                if (Build.VERSION.SdkInt >= BuildVersionCodes.Q)
                {
                    // MediaStore API ile kaydet
                    var contentValues = new ContentValues();
                    contentValues.Put(MediaStore.IMediaColumns.DisplayName, fileName);
                    contentValues.Put(MediaStore.IMediaColumns.MimeType, "image/jpeg");
                    contentValues.Put(MediaStore.IMediaColumns.RelativePath, AndroidEnvironment.DirectoryDcim + "/MerchPlus");

                    var resolver = context.ContentResolver;
                    var uri = resolver.Insert(MediaStore.Images.Media.ExternalContentUri, contentValues);

                    if (uri != null)
                    {
                        using (var outputStream = resolver.OpenOutputStream(uri))
                        {
                            if (outputStream != null)
                            {
                                await outputStream.WriteAsync(photoBytes, 0, photoBytes.Length);
                                await outputStream.FlushAsync();
                                System.Diagnostics.Debug.WriteLine($"PhotoGalleryService: Fotoğraf galeriye kaydedildi: {fileName}");
                                return true;
                            }
                        }
                    }
                }
                else
                {
                    // Android 9 ve altı için eski yöntem
                    var picturesDir = AndroidEnvironment.GetExternalStoragePublicDirectory(AndroidEnvironment.DirectoryDcim);
                    var merchPlusDir = new JavaIO.File(picturesDir, "MerchPlus");
                    if (!merchPlusDir.Exists())
                    {
                        merchPlusDir.Mkdirs();
                    }

                    var file = new JavaIO.File(merchPlusDir, fileName);
                    using (var fileStream = new System.IO.FileStream(file.AbsolutePath, System.IO.FileMode.Create))
                    {
                        await fileStream.WriteAsync(photoBytes, 0, photoBytes.Length);
                        await fileStream.FlushAsync();
                    }

                    // MediaStore'a bildir (galeri uygulamalarının görmesi için)
                    var mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
                    var fileUri = AndroidNet.Uri.FromFile(file);
                    mediaScanIntent.SetData(fileUri);
                    context.SendBroadcast(mediaScanIntent);

                    System.Diagnostics.Debug.WriteLine($"PhotoGalleryService: Fotoğraf galeriye kaydedildi (eski yöntem): {fileName}");
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"PhotoGalleryService: Fotoğraf galeriye kaydetme hatası: {ex.Message}");
                return false;
            }
        }
#endif
    }
}

