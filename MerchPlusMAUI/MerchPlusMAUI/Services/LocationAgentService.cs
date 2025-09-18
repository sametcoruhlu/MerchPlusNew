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
            _logger.LogInformation("Location service is starting.");
            await ExecuteAsync();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Location service is stopping.");
            Stop();
            await Task.CompletedTask;
        }

        private async Task ExecuteAsync()
        {
            if (_isRunning)
            {
                _logger.LogInformation("Location service is already running");
                return;
            }

            _isRunning = true;

            try
            {
                while (!_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    await UpdateLocationAsync();
                    await Task.Delay(TimeSpan.FromMinutes(5), _cancellationTokenSource.Token);
                }
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
            }
        }

        private async Task UpdateLocationAsync()
        {
            try
            {
                var location = await _geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(30)
                });

                if (location != null)
                {
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

                    _logger.LogInformation($"Updated location for member {coordinate.MemberId}: Lat={coordinate.Latitude}, Lon={coordinate.Longitude}");
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