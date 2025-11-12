using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices.Sensors;
using entMerchPlus;
using SQLite;
using System.Diagnostics; // Debug için tam yolu belirtiyoruz

namespace MerchPlusMAUI.Services
{
    public class LocationService
    {
        private readonly ILogger<LocationService> _logger;
        private readonly string _dbPath;
        private CancellationTokenSource _cancellationTokenSource;
        private bool _isTracking;
        
        public LocationService(ILogger<LocationService> logger)
        {
            _logger = logger;
            _dbPath = GlobalVariables.DatabasePath;
            _isTracking = false;
        }
        
        /// <summary>
        /// Konum izlemeyi başlatır
        /// </summary>
        public async Task StartLocationTracking()
        {
            if (_isTracking)
            {
                _logger.LogInformation("Konum izleme zaten başlatılmış");
                return;
            }
            
            try
            {
                // Konum iznini kontrol et
                var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                    if (status != PermissionStatus.Granted)
                    {
                        _logger.LogWarning("Konum izni reddedildi");
                        return;
                    }
                }
                
                _cancellationTokenSource = new CancellationTokenSource();
                _isTracking = true;
                
                // Konum izleme görevini başlat
                _ = Task.Run(async () => await LocationTrackingTask(), _cancellationTokenSource.Token);
                
                _logger.LogInformation("Konum izleme başlatıldı");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Konum izleme başlatılırken hata oluştu");
                _isTracking = false;
            }
        }
        
        /// <summary>
        /// Konum izlemeyi durdurur
        /// </summary>
        public void StopLocationTracking()
        {
            if (!_isTracking)
            {
                return;
            }
            
            _cancellationTokenSource?.Cancel();
            _isTracking = false;
            _logger.LogInformation("Konum izleme durduruldu");
        }
        
        /// <summary>
        /// Konum izleme görevi
        /// </summary>
        private async Task LocationTrackingTask()
        {
            try
            {
                while (!_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    try
                    {
                        _logger.LogInformation("=== LOCATIONSERVICE KONUM ALMA DENEMESİ ===");
                        
                        // Konum iznini kontrol et
                        var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
                        _logger.LogInformation($"LocationService - Konum izni durumu: {status}");
                        
                        if (status != PermissionStatus.Granted)
                        {
                            _logger.LogWarning("LocationService - Konum izni verilmemiş, izin isteniyor...");
                            status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                            _logger.LogInformation($"LocationService - İzin isteme sonucu: {status}");
                        }
                        
                        _logger.LogInformation("LocationService - Konum alınıyor... (Timeout: 10 saniye)");
                        var location = await Geolocation.GetLocationAsync(new GeolocationRequest
                        {
                            DesiredAccuracy = GeolocationAccuracy.Medium,
                            Timeout = TimeSpan.FromSeconds(10)
                        });
                        
                        if (location != null)
                        {
                            // Konumu GlobalVariables'a kaydedelim
                            GlobalVariables.MemberLastCoordinate = new Location
                            {
                                Latitude = location.Latitude,
                                Longitude = location.Longitude
                            };
                            
                            // Konumu veritabanına kaydet
                            using var db = new SQLiteConnection(_dbPath);
                            db.CreateTable<MemberCoordinate>();
                            
                            int memberId = 1; // Varsayılan değer
                            
                            // Eğer CurrentMemberId boş değilse int'e dönüştürmeyi dene
                            if (!string.IsNullOrEmpty(GlobalVariables.CurrentMemberId))
                            {
                                if (!int.TryParse(GlobalVariables.CurrentMemberId, out memberId))
                                {
                                    // Dönüştürülemezse log kaydı oluştur ve varsayılan değeri kullan
                                    _logger.LogWarning($"CurrentMemberId string değeri ({GlobalVariables.CurrentMemberId}) integer'a dönüştürülemedi. Varsayılan değer kullanılıyor.");
                                    // memberId zaten 1 olarak atanmıştı, bu nedenle burada tekrar atamaya gerek yok
                                }
                            }
                            
                            var coordinate = new MemberCoordinate
                            {
                                MemberId = memberId,
                                Latitude = location.Latitude,
                                Longitude = location.Longitude,
                                Accuracy = location.Accuracy ?? 0,
                                LastUpdateTime = DateTime.UtcNow
                            };
                            
                            db.Insert(coordinate);
                            
                            _logger.LogInformation($"=== LOCATIONSERVICE KONUM ALINDI ===");
                            _logger.LogInformation($"Konum: Lat={location.Latitude}, Lon={location.Longitude}, Accuracy={location.Accuracy}");
                            _logger.LogInformation($"GlobalVariables güncellendi: Lat={GlobalVariables.MemberLastCoordinate.Latitude}, Lon={GlobalVariables.MemberLastCoordinate.Longitude}");
                            _logger.LogInformation($"Veritabanına kaydedildi: MemberId={memberId}, LastUpdateTime={coordinate.LastUpdateTime}");
                        }
                        else
                        {
                            _logger.LogWarning("LocationService: Konum alınamadı (location null)");
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Konum alınırken hata oluştu");
                    }
                    
                    // 1 dakika bekle
                    await Task.Delay(TimeSpan.FromMinutes(1), _cancellationTokenSource.Token);
                }
            }
            catch (TaskCanceledException)
            {
                // Normal iptal durumu
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Konum izleme görevinde hata oluştu");
            }
            finally
            {
                _isTracking = false;
            }
        }
    }
} 