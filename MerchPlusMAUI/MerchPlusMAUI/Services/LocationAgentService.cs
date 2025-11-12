using Microsoft.Maui.ApplicationModel;
using SQLite;
using System.Diagnostics;
using MerchPlusMAUI.Models;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Maui.Devices.Sensors;

namespace MerchPlusMAUI.Services
{
    public interface ILocation
    {
        double Latitude { get; }
        double Longitude { get; }
    }

    public class LocationAdapter : ILocation
    {
        private readonly Microsoft.Maui.Devices.Sensors.Location _location;

        public LocationAdapter(Microsoft.Maui.Devices.Sensors.Location location)
        {
            _location = location;
        }

        public double Latitude => _location.Latitude;
        public double Longitude => _location.Longitude;
    }

    public class LocationAgentService : IHostedService, ILocationAgentService
    {
        private readonly ILogger<LocationAgentService> _logger;
        private readonly IGeolocation _geolocation;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private bool _isRunning;

        public LocationAgentService(ILogger<LocationAgentService> logger, IGeolocation geolocation)
        {
            _logger = logger;
            _geolocation = geolocation;
            _cancellationTokenSource = new CancellationTokenSource();
            _isRunning = false;
        }

        public async Task StartLocationTracking()
        {
            await StartAsync(CancellationToken.None);
        }

        public async Task StopLocationTracking()
        {
            await StopAsync(CancellationToken.None);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("=== LOCATIONAGENTSERVICE STARTASYNC BAŞLADI ===");
            _logger.LogInformation($"CancellationToken.IsCancellationRequested: {cancellationToken.IsCancellationRequested}");
            _logger.LogInformation("Location service is starting.");
            await ExecuteAsync();
            _logger.LogInformation("=== LOCATIONAGENTSERVICE STARTASYNC TAMAMLANDI ===");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Location service is stopping.");
            Stop();
            await Task.CompletedTask;
        }

        private async Task ExecuteAsync()
        {
            _logger.LogInformation("=== EXECUTEASYNC BAŞLADI ===");
            _logger.LogInformation($"_isRunning: {_isRunning}");
            _logger.LogInformation($"_cancellationTokenSource.Token.IsCancellationRequested: {_cancellationTokenSource.Token.IsCancellationRequested}");
            
            if (_isRunning)
            {
                _logger.LogInformation("Location service is already running");
                return;
            }

            _isRunning = true;
            _logger.LogInformation("Location service execution started");

            try
            {
                int loopCount = 0;
                while (!_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    loopCount++;
                    _logger.LogInformation($"=== KONUM ALMA DÖNGÜSÜ #{loopCount} ===");
                    await UpdateLocationAsync();
                    _logger.LogInformation($"Konum alma döngüsü #{loopCount} tamamlandı, 5 dakika bekleniyor...");
                    await Task.Delay(TimeSpan.FromMinutes(5), _cancellationTokenSource.Token);
                }
                _logger.LogInformation("Konum alma döngüsü sonlandırıldı");
            }
            catch (System.OperationCanceledException)
            {
                _logger.LogInformation("Location service was cancelled");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in location service");
            }
            finally
            {
                _isRunning = false;
                _logger.LogInformation("Location service execution ended");
            }
        }

        private async Task UpdateLocationAsync()
        {
            try
            {
                _logger.LogInformation("=== KONUM ALMA DENEMESİ BAŞLADI ===");
                _logger.LogInformation($"Geolocation servisi null mu?: {_geolocation == null}");
                
                // Konum iznini kontrol et
                var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
                _logger.LogInformation($"Konum izni durumu: {status}");
                
                if (status != PermissionStatus.Granted)
                {
                    _logger.LogWarning("Konum izni verilmemiş, izin isteniyor...");
                    status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                    _logger.LogInformation($"İzin isteme sonucu: {status}");
                }
                
                _logger.LogInformation("Konum alınıyor... (Timeout: 30 saniye)");
                var location = await _geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(30)
                });

                if (location != null)
                {
                    // GlobalVariables'a konumu kaydet (bu eksikti!)
                    GlobalVariables.MemberLastCoordinate = new Location
                    {
                        Latitude = location.Latitude,
                        Longitude = location.Longitude
                    };
                    
                    int memberId = 1; // Varsayılan değer
                    string memberIdString = GlobalVariables.CurrentMember?.Id ?? string.Empty;
                    
                    if (!string.IsNullOrEmpty(memberIdString))
                    {
                        if (!int.TryParse(memberIdString, out memberId))
                        {
                            _logger.LogWarning($"CurrentMember.Id değeri ({memberIdString}) integer'a dönüştürülemedi. Varsayılan değer kullanılıyor.");
                            // memberId zaten 1 olarak atanmıştı, tekrar atamaya gerek yok
                        }
                    }
                    
                    var coordinate = new MemberCoordinate
                    {
                        MemberId = memberId,
                        Latitude = location.Latitude,
                        Longitude = location.Longitude,
                        LastUpdateTime = DateTime.UtcNow
                    };

                    using var connection = new SQLiteConnection(GlobalVariables.DatabasePath);
                    connection.CreateTable<MemberCoordinate>();
                    connection.Insert(coordinate);

                    _logger.LogInformation($"=== LOCATIONAGENTSERVICE KONUM ALINDI ===");
                    _logger.LogInformation($"Konum: Lat={location.Latitude}, Lon={location.Longitude}");
                    _logger.LogInformation($"GlobalVariables güncellendi: Lat={GlobalVariables.MemberLastCoordinate.Latitude}, Lon={GlobalVariables.MemberLastCoordinate.Longitude}");
                    _logger.LogInformation($"Veritabanına kaydedildi: MemberId={memberId}, LastUpdateTime={coordinate.LastUpdateTime}");
                }
                else
                {
                    _logger.LogWarning("LocationAgentService: Konum alınamadı (location null)");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating location");
            }
        }

        public void Stop()
        {
            _cancellationTokenSource.Cancel();
        }
    }

    public interface ILocationAgentService
    {
        Task StartLocationTracking();
        Task StopLocationTracking();
    }
} 