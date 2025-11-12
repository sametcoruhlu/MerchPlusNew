using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Application = Microsoft.Maui.Controls.Application;
using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.ApplicationModel;
using System.Diagnostics;
using MerchPlusMAUI.Views;
using MerchPlusMAUI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui;
using IWindow = Microsoft.Maui.IWindow;
using Window = Microsoft.Maui.Controls.Window;
using IActivationState = Microsoft.Maui.IActivationState;

namespace MerchPlusMAUI
{
    public partial class App : Application
    {
        private readonly ILogger<App> _logger;
        private readonly IServiceProvider _serviceProvider;



        public App(ILogger<App> logger, IServiceProvider serviceProvider)
        {
            try
            {
                InitializeComponent();
                _logger = logger;
                _serviceProvider = serviceProvider;
                MainPage = new AppShell();
                SetupExceptionHandling();
                
                // Servisleri başlat
                _ = Task.Run(async () => await StartServicesAsync());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"InitializeComponent hatası: {ex.Message}");
                _logger?.LogError(ex, "App başlatılırken hata");
            }
        }

        /// <summary>
        /// Arka plan servislerini başlatır
        /// </summary>
        private async Task StartServicesAsync()
        {
            try
            {
                _logger?.LogInformation("Starting background services...");

                // DatabaseSyncService'i başlat
                var databaseSyncService = _serviceProvider.GetService<DatabaseSyncService>();
                if (databaseSyncService != null)
                {
                    await databaseSyncService.StartAsync(CancellationToken.None);
                    GlobalVariables.DatabaseSyncStarted = true;
                    _logger?.LogInformation("DatabaseSyncService started");
                }

                // BackgroundTimerService'i başlat
                var backgroundTimerService = _serviceProvider.GetService<BackgroundTimerService>();
                if (backgroundTimerService != null)
                {
                    await backgroundTimerService.StartAsync(CancellationToken.None);
                    GlobalVariables.BackgroundTimerStarted = true;
                    _logger?.LogInformation("BackgroundTimerService started");
                }

                _logger?.LogInformation("All background services started successfully");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error starting background services");
            }
        }

        // .NET MAUI 9'da önerilen Window oluşturma yöntemi
        protected override Window CreateWindow(IActivationState activationState)
        {
            try
            {
                // Standart window oluşturma yöntemini kullan
                var window = base.CreateWindow(activationState);

                // Window başlığını ayarla
                window.Title = "MerchPlus";

                // Normal safe area kullanımı
                if (window.Page != null)
                {
                    // iOS için safe area'yı normal kullan
                    Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific.Page.SetUseSafeArea(window.Page, true);
                }

                // Android platformunda normal davranış
                if (DeviceInfo.Platform == DevicePlatform.Android)
                {
                    // Android'de normal layout davranışı
                }

                return window;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Window oluşturulurken hata");
                // Hata durumunda basit bir sayfa döndür
                return new Window(CreateErrorPage(ex));
            }
        }

        private Page CreateErrorPage(Exception exception)
        {
            return new ContentPage
            {
                BackgroundColor = Microsoft.Maui.Graphics.Colors.White,
                Content = new VerticalStackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    Children =
                    {
                        new Label 
                        { 
                            Text = "MerchPlus", 
                            FontSize = 32,
                            TextColor = Microsoft.Maui.Graphics.Color.FromArgb("#336699"),
                            HorizontalOptions = LayoutOptions.Center
                        },
                        new Label 
                        { 
                            Text = "Uygulama başlatılamadı. Lütfen tekrar deneyin.", 
                            FontSize = 18,
                            Margin = new Thickness(0, 20, 0, 0),
                            HorizontalOptions = LayoutOptions.Center
                        },
                        new Label 
                        { 
                            Text = exception?.Message ?? "Bilinmeyen hata", 
                            FontSize = 14,
                            TextColor = Microsoft.Maui.Graphics.Colors.Red,
                            Margin = new Thickness(0, 20, 0, 0),
                            HorizontalOptions = LayoutOptions.Center
                        }
                    }
                }
            };
        }

        private void SetupExceptionHandling()
        {
            try
            {
                AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
                {
                    var exception = e.ExceptionObject as Exception;
                    LogUnhandledException(exception);
                };
                TaskScheduler.UnobservedTaskException += (sender, e) =>
                {
                    LogUnhandledException(e.Exception);
                    e.SetObserved();
                };
            }
            catch (Exception ex)
            {
                LogUnhandledException(ex);
            }
        }

        private void LogUnhandledException(Exception exception)
        {
            var innerException = exception;
            var exceptionMessage = exception?.Message ?? "null";
            var stackTrace = exception?.StackTrace ?? "null";
            while (innerException?.InnerException != null)
            {
                innerException = innerException.InnerException;
            }
            var innerExceptionMessage = innerException?.Message ?? "null";
            var innerStackTrace = innerException?.StackTrace ?? "null";
            var errorMessage = $"UNHANDLED EXCEPTION: {exceptionMessage}\nInner: {innerExceptionMessage}\nStack: {stackTrace}\nInner Stack: {innerStackTrace}";
            System.Diagnostics.Debug.WriteLine(errorMessage);
            _logger?.LogError(exception, "Yakalanmamış uygulama hatası");
            Microsoft.Maui.ApplicationModel.MainThread.BeginInvokeOnMainThread(async () =>
            {
                if (Windows.Count > 0)
                {
                    try
                    {
                        Page currentPage = Windows[0].Page;
                        if (currentPage != null)
                        {
                            await currentPage.DisplayAlert("Uygulama Hatası", $"Uygulama bir hata ile karşılaştı: {innerExceptionMessage}", "Tamam");
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Hata gösterme başarısız oldu: {ex.Message}");
                        _logger?.LogError(ex, "Hata gösterme başarısız oldu");
                    }
                }
            });
        }
    }
}