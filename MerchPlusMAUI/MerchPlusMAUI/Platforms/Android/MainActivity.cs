using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Android.App;
using Android.Content.PM;
using Android.Widget;
using entMerchPlus;
using MerchPlusMAUI.Entities;
using MerchPlusMAUI.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SQLite;
using AndroidFileProvider = AndroidX.Core.Content.FileProvider;
using Environment = Android.OS.Environment;
using IOPath = System.IO.Path;
using SystemUri = System.Uri;
using Android.OS;
using Android.Views;
using Microsoft.Maui;
using MauiColor = Microsoft.Maui.Graphics.Color;
using AndroidColor = Android.Graphics.Color;
using MauiProgressBar = Microsoft.Maui.Controls.ProgressBar;
using AndroidProgressBar = Android.Widget.ProgressBar;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
// Android 12+ SplashScreen API için gerekli using direktifleri
using AndroidX.Core.SplashScreen;

namespace MerchPlusMAUI
{
    [Activity(Theme = "@style/MyTheme", 
              MainLauncher = true, 
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        private static MainActivity? CurrentActivity;
        private PowerManager? _powerManager;
        private PowerManager.WakeLock? _wakeLock;
        private LocationAgentService? _locationService;
        private DatabaseSyncService? _databaseService;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            // Android 12 (S) ve üzeri için splash screen davranışını kontrol et
            if (Build.VERSION.SdkInt >= BuildVersionCodes.S)
            {
                try
                {
                    // AndroidX SplashScreen API kullanarak splash screen'i yönet
                    var splashScreen = AndroidX.Core.SplashScreen.SplashScreen.InstallSplashScreen(this);
                    
                    // Splash screen'in ne kadar süre gösterileceğini kontrol et
                    splashScreen.SetKeepOnScreenCondition(new KeepOnScreenCondition());
                    
                    // Splash screen çıkış animasyonunu özelleştir
                    splashScreen.SetOnExitAnimationListener(new SplashScreenExitListener());
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"SplashScreen API hatası: {ex.Message}");
                }
            }
            
            // Varsayılan splash screen'i devre dışı bırakmak için önce temayı ayarla
            SetTheme(Resource.Style.MyTheme);
            
            base.OnCreate(savedInstanceState);
            CurrentActivity = this;

            // Status bar'ı normal haline getir - siyah arka plan, beyaz yazı
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                Window.ClearFlags(WindowManagerFlags.TranslucentStatus | WindowManagerFlags.TranslucentNavigation);
                Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
                Window.SetStatusBarColor(Android.Graphics.Color.Black);
                Window.SetNavigationBarColor(Android.Graphics.Color.Black);
                
                // Status bar'ı normal layout ile göster
                Window.DecorView.SystemUiVisibility = (StatusBarVisibility)(
                    SystemUiFlags.Visible
                );
            }

            // Eski Android sürümleri için normal status bar
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat && Build.VERSION.SdkInt < BuildVersionCodes.Lollipop)
            {
                Window.ClearFlags(WindowManagerFlags.TranslucentStatus);
            }

            // Initialize PowerManager and WakeLock
            _powerManager = (PowerManager?)GetSystemService(Context.PowerService);
            _wakeLock = _powerManager?.NewWakeLock(WakeLockFlags.Full, "MerchPlusMAUI::WakeLockTag");

            // Get services
            var services = IPlatformApplication.Current.Services;
            _locationService = services.GetService<LocationAgentService>();
            _databaseService = services.GetService<DatabaseSyncService>();

            // Request Permissions
            RequestPermissions();

            // Navigate to HomePage - all initialization will be handled there
            _ = Task.Run(async () => 
            {
                await Task.Delay(1000); // Give time for the app to fully load
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    try
                    {
                        await Shell.Current.GoToAsync("///HomePage");
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Navigation error: {ex.Message}");
                    }
                });
            });
        }

        // Splash screen'in ne kadar süre ekranda kalacağını kontrol eden sınıf
        private class KeepOnScreenCondition : Java.Lang.Object, AndroidX.Core.SplashScreen.SplashScreen.IKeepOnScreenCondition
        {
            public bool ShouldKeepOnScreen()
            {
                // Splash screen'i hemen kapat
                return false;
            }
        }

        // Splash Screen'den çıkış animasyonunu yönetecek sınıf
        private class SplashScreenExitListener : Java.Lang.Object, AndroidX.Core.SplashScreen.SplashScreen.IOnExitAnimationListener
        {
            public void OnSplashScreenExit(AndroidX.Core.SplashScreen.SplashScreenViewProvider splashScreenViewProvider)
            {
                try
                {
                    // Splash screen'den hemen çıkış yap, animasyon olmadan
                    var splashScreenView = splashScreenViewProvider.View;
                    splashScreenView?.Animate()
                         ?.Alpha(0.0f)
                         ?.SetDuration(0) // Animasyon süresi 0
                         ?.WithEndAction(new Java.Lang.Runnable(() => {
                             splashScreenViewProvider.Remove();
                         }))
                         ?.Start();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"SplashScreen exit animation hatası: {ex.Message}");
                }
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            try
            {
                if (_wakeLock != null && !_wakeLock.IsHeld)
                {
                    _wakeLock.Acquire();
                    System.Diagnostics.Debug.WriteLine("WakeLock acquired in OnResume");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"WakeLock acquire error: {ex.Message}");
            }
            
            try
            {
                _locationService?.StartAsync(CancellationToken.None);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Location service start error: {ex.Message}");
            }
        }

        protected override void OnPause()
        {
            base.OnPause();
            try
            {
                if (_wakeLock != null && _wakeLock.IsHeld)
                {
                    _wakeLock.Release();
                    System.Diagnostics.Debug.WriteLine("WakeLock released in OnPause");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"WakeLock release error: {ex.Message}");
            }
            
            try
            {
                _locationService?.StopAsync(CancellationToken.None);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Location service stop error: {ex.Message}");
            }
        }

        // Hardware back button'u override et
        public override void OnBackPressed()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Hardware back button pressed");
                
                // Shell navigation ile geri git
                if (Shell.Current != null)
                {
                    var currentPage = Shell.Current.CurrentPage;
                    var pageName = currentPage?.GetType().Name ?? "";
                    
                    System.Diagnostics.Debug.WriteLine($"Current page: {pageName}");
                    
                    // Ana sayfalarda ise uygulamayı minimize et
                    if (pageName == "HomePage" || pageName == "AboutMePage")
                    {
                        System.Diagnostics.Debug.WriteLine("Main page detected, minimizing app");
                        MoveTaskToBack(true);
                        return;
                    }
                    
                    // Diğer sayfalarda Shell navigation ile geri git
                    System.Diagnostics.Debug.WriteLine("Secondary page detected, navigating back");
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        try
                        {
                            await Shell.Current.GoToAsync("..");
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine($"Shell navigation back error: {ex.Message}");
                            // Fallback: Ana sayfaya git
                            try
                            {
                                await Shell.Current.GoToAsync("///AboutMePage");
                            }
                            catch (Exception fallbackEx)
                            {
                                System.Diagnostics.Debug.WriteLine($"Fallback navigation error: {fallbackEx.Message}");
                                base.OnBackPressed();
                            }
                        }
                    });
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Shell.Current is null, using default back pressed");
                    base.OnBackPressed();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"OnBackPressed error: {ex.Message}");
                base.OnBackPressed();
            }
        }

        private void RequestPermissions()
        {
            var permissions = new[]
            {
                Android.Manifest.Permission.Camera,
                Android.Manifest.Permission.ReadExternalStorage,
                Android.Manifest.Permission.WriteExternalStorage,
                Android.Manifest.Permission.Internet,
                Android.Manifest.Permission.AccessCoarseLocation,
                Android.Manifest.Permission.AccessFineLocation,
                Android.Manifest.Permission.WakeLock
            };

            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.M)
            {
                RequestPermissions(permissions, 0);
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            
            bool allPermissionsGranted = true;
            for (int i = 0; i < grantResults.Length; i++)
            {
                if (grantResults[i] != Permission.Granted)
                {
                    allPermissionsGranted = false;
                    break;
                }
            }

            if (!allPermissionsGranted)
            {
                // Handle permission denial
                System.Diagnostics.Debug.WriteLine("Not all permissions were granted");
            }
        }

        protected override void OnDestroy()
        {
            try
            {
                if (_wakeLock != null)
                {
                    if (_wakeLock.IsHeld)
                    {
                        _wakeLock.Release();
                        System.Diagnostics.Debug.WriteLine("WakeLock released in OnDestroy");
                    }
                    _wakeLock.Dispose();
                    _wakeLock = null;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"WakeLock cleanup error: {ex.Message}");
            }
            
            base.OnDestroy();
        }
    }
}

