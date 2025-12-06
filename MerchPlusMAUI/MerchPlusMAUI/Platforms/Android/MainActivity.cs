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
                    
                    // Navigation stack kontrolü
                    var navigationStack = Shell.Current.Navigation.NavigationStack;
                    var modalStack = Shell.Current.Navigation.ModalStack;
                    
                    System.Diagnostics.Debug.WriteLine($"NavigationStack count: {navigationStack.Count}, ModalStack count: {modalStack.Count}");
                    
                    // Eğer navigation stack'te birden fazla sayfa varsa geri git
                    if (navigationStack.Count > 1)
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            try
                            {
                                await Shell.Current.Navigation.PopAsync();
                                System.Diagnostics.Debug.WriteLine("PopAsync executed successfully");
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine($"PopAsync error: {ex.Message}");
                                // Fallback: Shell GoToAsync ile geri git
                                try
                                {
                                    await Shell.Current.GoToAsync("..");
                                }
                                catch (Exception fallbackEx)
                                {
                                    System.Diagnostics.Debug.WriteLine($"GoToAsync fallback error: {fallbackEx.Message}");
                                    base.OnBackPressed();
                                }
                            }
                        });
                        return;
                    }
                    
                    // Modal stack kontrolü
                    if (modalStack.Count > 0)
                    {
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            try
                            {
                                await Shell.Current.Navigation.PopModalAsync();
                                System.Diagnostics.Debug.WriteLine("PopModalAsync executed successfully");
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine($"PopModalAsync error: {ex.Message}");
                                base.OnBackPressed();
                            }
                        });
                        return;
                    }
                    
                    // Navigation stack boşsa sayfa tipine göre explicit navigation yap
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        try
                        {
                            // Sayfa tipine göre önceki sayfaya git (parametrelerle birlikte)
                            var navigationResult = await GetPreviousPageNavigationAsync(pageName, currentPage);
                            
                            if (navigationResult.Success)
                            {
                                System.Diagnostics.Debug.WriteLine($"Navigation to {navigationResult.Route} executed successfully");
                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine($"Navigation failed: {navigationResult.Error}");
                                // Fallback: Ana sayfaya git
                                await Shell.Current.GoToAsync("///AboutMePage");
                            }
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

        private async Task<(bool Success, string Route, string Error)> GetPreviousPageNavigationAsync(string currentPageName, Microsoft.Maui.Controls.Page currentPage)
        {
            try
            {
                // Sayfa tipine göre önceki sayfaya git (parametrelerle birlikte)
                switch (currentPageName)
                {
                    case "StoreProductsPage":
                        // StoresPage'e StoreType=All parametresi ile git
                        await Shell.Current.GoToAsync("///StoresPage?StoreType=All");
                        System.Diagnostics.Debug.WriteLine("Navigated to StoresPage with StoreType=All");
                        return (true, "///StoresPage?StoreType=All", null);
                        
                    case "MerchDataEntryPage":
                        // StoreProductsPage'e geri dön - parametrelerle birlikte
                        if (currentPage is MerchPlusMAUI.Views.MerchDataEntryPage merchDataEntryPage)
                        {
                            var entMemberRouteId = merchDataEntryPage.EntMemberRouteId;
                            var storeName = merchDataEntryPage.StoreName;
                            
                            if (!string.IsNullOrEmpty(entMemberRouteId))
                            {
                                var parameters = new Dictionary<string, object>
                                {
                                    ["entMemberRouteId"] = entMemberRouteId,
                                    ["StoreName"] = storeName ?? "Mağaza"
                                };
                                
                                System.Diagnostics.Debug.WriteLine($"MerchDataEntryPage: Navigating to StoreProductsPage with parameters:");
                                System.Diagnostics.Debug.WriteLine($"    entMemberRouteId: {entMemberRouteId}");
                                System.Diagnostics.Debug.WriteLine($"    StoreName: {storeName}");
                                
                                await Shell.Current.GoToAsync("///StoreProductsPage", parameters);
                                return (true, "///StoreProductsPage", null);
                            }
                        }
                        // Fallback: parametre olmadan git
                        await Shell.Current.GoToAsync("///StoreProductsPage");
                        return (true, "///StoreProductsPage", null);
                        
                    case "SalesOrderPage":
                        // StoreProductsPage'e geri dön - parametrelerle birlikte
                        if (currentPage is MerchPlusMAUI.Views.SalesOrderPage salesOrderPage)
                        {
                            var salesEntMemberRouteId = salesOrderPage.EntMemberRouteId;
                            var salesStoreName = salesOrderPage.StoreName;
                            
                            if (!string.IsNullOrEmpty(salesEntMemberRouteId))
                            {
                                var parameters = new Dictionary<string, object>
                                {
                                    ["entMemberRouteId"] = salesEntMemberRouteId,
                                    ["StoreName"] = salesStoreName ?? "Mağaza"
                                };
                                
                                System.Diagnostics.Debug.WriteLine($"SalesOrderPage: Navigating to StoreProductsPage with parameters:");
                                System.Diagnostics.Debug.WriteLine($"    entMemberRouteId: {salesEntMemberRouteId}");
                                System.Diagnostics.Debug.WriteLine($"    StoreName: {salesStoreName}");
                                
                                await Shell.Current.GoToAsync("///StoreProductsPage", parameters);
                                return (true, "///StoreProductsPage", null);
                            }
                        }
                        // Fallback: parametre olmadan git
                        await Shell.Current.GoToAsync("///StoreProductsPage");
                        return (true, "///StoreProductsPage", null);
                    case "ExpirationDatePage":
                        // StoreProductsPage'e geri dön - parametrelerle birlikte
                        if (currentPage is MerchPlusMAUI.Views.ExpirationDatePage expirationDatePage)
                        {
                            var expEntMemberRouteId = expirationDatePage.EntMemberRouteId;
                            var expStoreName = expirationDatePage.StoreName;
                            
                            if (!string.IsNullOrEmpty(expEntMemberRouteId))
                            {
                                var parameters = new Dictionary<string, object>
                                {
                                    ["entMemberRouteId"] = expEntMemberRouteId,
                                    ["StoreName"] = expStoreName ?? "Mağaza"
                                };
                                
                                await Shell.Current.GoToAsync("///StoreProductsPage", parameters);
                                return (true, "///StoreProductsPage", null);
                            }
                        }
                        await Shell.Current.GoToAsync("///StoreProductsPage");
                        return (true, "///StoreProductsPage", null);
                        
                    case "StoreReportPage":
                        await Shell.Current.GoToAsync("///StoreProductsPage");
                        return (true, "///StoreProductsPage", null);
                    case "StoreDisplayPhotoPage":
                        await Shell.Current.GoToAsync("///StoreVitrinePhotoPage");
                        System.Diagnostics.Debug.WriteLine("Navigated to StoreVitrinePhotoPage");
                        return (true, "///StoreVitrinePhotoPage", null);
                    case "StoreVitrinePhotoPage":
                        // StoreVitrinePhotoPage'den StoreProductsPage'e geri dön - parametrelerle
                        if (currentPage is MerchPlusMAUI.Views.StoreVitrinePhotoPage storeVitrinePhotoPage)
                        {
                            var vitrineEntMemberRouteId = storeVitrinePhotoPage.EntMemberRouteId;
                            var vitrineStoreName = storeVitrinePhotoPage.StoreName;
                            
                            if (!string.IsNullOrEmpty(vitrineEntMemberRouteId))
                            {
                                var parameters = new Dictionary<string, object>
                                {
                                    ["entMemberRouteId"] = vitrineEntMemberRouteId,
                                    ["StoreName"] = vitrineStoreName ?? "Mağaza"
                                };
                                
                                System.Diagnostics.Debug.WriteLine($"StoreVitrinePhotoPage: Navigating to StoreProductsPage with parameters:");
                                System.Diagnostics.Debug.WriteLine($"    entMemberRouteId: {vitrineEntMemberRouteId}");
                                System.Diagnostics.Debug.WriteLine($"    StoreName: {vitrineStoreName}");
                                
                                await Shell.Current.GoToAsync("///StoreProductsPage", parameters);
                                System.Diagnostics.Debug.WriteLine("Navigated to StoreProductsPage from StoreVitrinePhotoPage");
                                return (true, "///StoreProductsPage", null);
                            }
                        }
                        // Fallback: parametre olmadan git
                        await Shell.Current.GoToAsync("///StoreProductsPage");
                        return (true, "///StoreProductsPage", null);
                    case "StoreEntryPhotoPage":
                        await Shell.Current.GoToAsync("///StoreProductsPage");
                        return (true, "///StoreProductsPage", null);
                    case "StoresPage":
                        // AboutMePage'e geri dön
                        await Shell.Current.GoToAsync("///AboutMePage");
                        System.Diagnostics.Debug.WriteLine("Navigated to AboutMePage");
                        return (true, "///AboutMePage", null);
                    case "NotificationsPage":
                    case "LocationsPage":
                    case "VisitPage":
                        return (true, "///AboutMePage", null);
                    default:
                        return (true, "///AboutMePage", null);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetPreviousPageNavigationAsync error: {ex.Message}");
                return (false, null, ex.Message);
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

