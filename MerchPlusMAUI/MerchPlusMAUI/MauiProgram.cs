using Microsoft.Extensions.Logging;
using MerchPlusMAUI.Controls;
using MerchPlusMAUI.Views;
using MerchPlusMAUI.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using CommunityToolkit.Maui;
using Microsoft.Maui;
using Microsoft.Maui.Devices.Sensors;

namespace MerchPlusMAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("MaterialIcons-Regular.ttf", "MaterialIcons");
                    fonts.AddFont("MaterialIcons-Regular.ttf", "MaterialIconsRegular");
                    fonts.AddFont("MaterialIcons-Regular.ttf", "Material Icons");
                });

            // Platform servisleri
            builder.Services.AddSingleton(Connectivity.Current);
            builder.Services.AddSingleton(DeviceInfo.Current);
            builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);
            
            // Uygulama servisleri
            builder.Services.AddSingleton<DatabaseService>();
            builder.Services.AddSingleton<LocationService>();
            builder.Services.AddSingleton<LocationAgentService>();
            builder.Services.AddSingleton<DatabaseSyncService>();
            builder.Services.AddSingleton<BackgroundTimerService>();
            builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();
            
            // HTTP Client ve Field Visibility Service
            builder.Services.AddHttpClient();
            builder.Services.AddSingleton<IFieldVisibilityService, FieldVisibilityService>();
            
            // Görünümler (Views) - ancak AppShell doğrudan App.xaml.cs'de oluşturulduğu için SplashScreen'i kaldırıyoruz
            builder.Services.AddTransient<HomePage>();
            builder.Services.AddTransient<AboutMePage>();
            builder.Services.AddTransient<SettingsPage>();
            builder.Services.AddTransient<LocationsPage>();
            builder.Services.AddTransient<VisitPage>();
            builder.Services.AddTransient<StoresPage>();
            builder.Services.AddTransient<StoreProductsPage>();
            builder.Services.AddTransient<SalesOrderPage>();
            builder.Services.AddTransient<StoreReportPage>();
            builder.Services.AddTransient<ErrorPage>();
            builder.Services.AddTransient<MerchDataEntryPage>();
            builder.Services.AddTransient<StoreEntryPhotoPage>();
            builder.Services.AddTransient<StoreVitrinePhotoPage>();
            builder.Services.AddTransient<StoreDisplayPhotoPage>();
            builder.Services.AddTransient<ProductMultiSelectionPage>();
            builder.Services.AddTransient<ExpirationDatePage>();
            
            // Kontroller (Controls)
            builder.Services.AddTransient<CommonHeader>();
            builder.Services.AddTransient<CommonFooter>();

            // Singleton servisler
            builder.Services.AddSingleton<NotificationService>(provider => NotificationService.Instance);
            builder.Services.AddSingleton<UserProfileService>(provider => UserProfileService.Instance);

            // Loglama
            builder.Logging.AddDebug()
                .SetMinimumLevel(LogLevel.Debug);

            return builder.Build();
        }
    }
}
