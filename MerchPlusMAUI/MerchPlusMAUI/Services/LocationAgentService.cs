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
using System.Reflection;
using entMerchPlus;

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
                Console.WriteLine("========================================");
                Console.WriteLine("[LocationAgentService] === KONUM ALMA DENEMESİ BAŞLADI ===");
                Console.WriteLine($"[LocationAgentService] Zaman: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                Console.WriteLine($"[LocationAgentService] GlobalVariables.CurrentMemberId: '{GlobalVariables.CurrentMemberId}'");
                Console.WriteLine($"[LocationAgentService] GlobalVariables.CurrentMember null mu?: {GlobalVariables.CurrentMember == null}");
                _logger.LogInformation("=== KONUM ALMA DENEMESİ BAŞLADI ===");
                _logger.LogInformation($"Geolocation servisi null mu?: {_geolocation == null}");
                
                // Konum iznini kontrol et
                var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
                _logger.LogInformation($"Konum izni durumu: {status}");
                Console.WriteLine($"[LocationAgentService] Konum izni durumu: {status}");
                
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
                    Console.WriteLine($"[LocationAgentService] ✅ Konum alındı: Lat={location.Latitude}, Lon={location.Longitude}");
                    
                    // GlobalVariables'a konumu kaydet (bu eksikti!)
                    GlobalVariables.MemberLastCoordinate = new Location
                    {
                        Latitude = location.Latitude,
                        Longitude = location.Longitude
                    };
                    
                    // Eski projede olduğu gibi entMemberCoordinate kullanıyoruz
                    // Eski projede: MemberId (string), CreatedOn (DateTime), CoordinateX (decimal), CoordinateY (decimal)
                    // Sunucu bu formatı bekliyor, bu yüzden hem eski hem yeni formatı dolduruyoruz
                    
                    // MemberId'yi belirle - önce CurrentMemberId'yi kontrol et (string), sonra CurrentMember.Id'yi (int -> string)
                    string memberId = GlobalVariables.CurrentMemberId;
                    Console.WriteLine($"[LocationAgentService] İlk MemberId kontrolü: '{memberId}'");
                    
                    if (string.IsNullOrEmpty(memberId))
                    {
                        // CurrentMemberId boşsa, CurrentMember.Id'yi string'e çevir
                        if (GlobalVariables.CurrentMember != null)
                        {
                            // Reflection ile Id property'sini kontrol et (int, string, veya başka bir tip olabilir)
                            var memberType = GlobalVariables.CurrentMember.GetType();
                            var idProperty = memberType.GetProperty("Id");
                            
                            if (idProperty != null)
                            {
                                var idValue = idProperty.GetValue(GlobalVariables.CurrentMember);
                                if (idValue != null)
                                {
                                    memberId = idValue.ToString();
                                    _logger.LogInformation($"CurrentMemberId boş, CurrentMember.Id (reflection) kullanıldı: {memberId}");
                                    Console.WriteLine($"[LocationAgentService] CurrentMemberId boş, CurrentMember.Id (reflection) kullanıldı: '{memberId}'");
                                }
                            }
                            
                            // Hala boşsa, diğer property'leri kontrol et
                            if (string.IsNullOrEmpty(memberId))
                            {
                                var memberIdProperty = memberType.GetProperty("MemberId");
                                if (memberIdProperty != null)
                                {
                                    var memberIdValue = memberIdProperty.GetValue(GlobalVariables.CurrentMember);
                                    if (memberIdValue != null)
                                    {
                                        memberId = memberIdValue.ToString();
                                        _logger.LogInformation($"CurrentMemberId boş, CurrentMember.MemberId (reflection) kullanıldı: {memberId}");
                                        Console.WriteLine($"[LocationAgentService] CurrentMemberId boş, CurrentMember.MemberId (reflection) kullanıldı: '{memberId}'");
                                    }
                                }
                            }
                        }
                        
                        // Hala boşsa, veritabanından entMemberRoute tablosundan MemberId'yi al
                        if (string.IsNullOrEmpty(memberId))
                        {
                            try
                            {
                                using var db = new SQLiteConnection(GlobalVariables.DatabasePath);
                                // Son route'dan MemberId'yi al (CreatedOn kolonu yok, Id kullanıyoruz)
                                var lastRoute = db.Query<entMerchPlus.entMemberRoute>(
                                    "SELECT * FROM entMemberRoute ORDER BY Id DESC LIMIT 1");
                                
                                if (lastRoute != null && lastRoute.Count > 0 && !string.IsNullOrEmpty(lastRoute[0].MemberId))
                                {
                                    memberId = lastRoute[0].MemberId;
                                    _logger.LogInformation($"CurrentMemberId boş, veritabanından entMemberRoute.MemberId kullanıldı: {memberId}");
                                    Console.WriteLine($"[LocationAgentService] CurrentMemberId boş, veritabanından entMemberRoute.MemberId kullanıldı: '{memberId}'");
                                }
                            }
                            catch (Exception dbEx)
                            {
                                _logger.LogWarning(dbEx, "Veritabanından MemberId alınamadı");
                                Console.WriteLine($"[LocationAgentService] Veritabanından MemberId alınamadı: {dbEx.Message}");
                            }
                        }
                        
                        // Son çare: varsayılan değer kullanma, konum kaydını atla
                        if (string.IsNullOrEmpty(memberId))
                        {
                            _logger.LogWarning("GlobalVariables.CurrentMemberId, CurrentMember.Id ve veritabanından MemberId alınamadı, konum kaydı atlanıyor");
                            Console.WriteLine("========================================");
                            Console.WriteLine($"[LocationAgentService] ❌ UYARI: Tüm kaynaklardan MemberId alınamadı!");
                            Console.WriteLine($"[LocationAgentService] CurrentMember null mu?: {GlobalVariables.CurrentMember == null}");
                            if (GlobalVariables.CurrentMember != null)
                            {
                                var memberType = GlobalVariables.CurrentMember.GetType();
                                var props = memberType.GetProperties();
                                Console.WriteLine($"[LocationAgentService] CurrentMember property'leri:");
                                foreach (var prop in props)
                                {
                                    var val = prop.GetValue(GlobalVariables.CurrentMember);
                                    Console.WriteLine($"[LocationAgentService]   {prop.Name} ({prop.PropertyType.Name}): '{val}'");
                                }
                            }
                            Console.WriteLine($"[LocationAgentService] GlobalVariables.CurrentMemberId: '{GlobalVariables.CurrentMemberId}'");
                            Console.WriteLine($"[LocationAgentService] ❌ MemberId bulunamadı, konum kaydı ATLANIYOR. Bir sonraki döngüde tekrar denenecek.");
                            Console.WriteLine("========================================");
                            return; // MemberId bulunamadı, konum kaydını yapma
                        }
                    }
                    
                    Console.WriteLine($"[LocationAgentService] Kullanılacak MemberId: '{memberId}'");
                    
                    var coordinate = new MerchPlusMAUI.Models.entMemberCoordinate
                    {
                        MemberId = memberId,
                        // Eski format (sunucu bunları bekliyor)
                        CoordinateX = Convert.ToDecimal(location.Latitude),
                        CoordinateY = Convert.ToDecimal(location.Longitude),
                        CreatedOn = DateTime.Now,
                        // Yeni format (veritabanında saklamak için)
                        Latitude = location.Latitude,
                        Longitude = location.Longitude,
                        LastUpdateTime = DateTime.Now
                    };

                    using var connection = new SQLiteConnection(GlobalVariables.DatabasePath);
                    connection.CreateTable<MerchPlusMAUI.Models.entMemberCoordinate>();
                    
                    // INSERT öncesi tablodaki kayıt sayısını kontrol et
                    var countBefore = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM entMemberCoordinate");
                    Console.WriteLine($"[LocationAgentService] INSERT öncesi tabloda {countBefore} adet koordinat var");
                    
                    connection.Insert(coordinate);
                    
                    // INSERT sonrası tablodaki kayıt sayısını kontrol et
                    var countAfter = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM entMemberCoordinate");
                    Console.WriteLine($"[LocationAgentService] INSERT sonrası tabloda {countAfter} adet koordinat var (eklenen: {countAfter - countBefore})");
                    
                    // Son kaydedilen koordinatı kontrol et
                    var lastCoord = connection.Query<MerchPlusMAUI.Models.entMemberCoordinate>(
                        "SELECT * FROM entMemberCoordinate ORDER BY Id DESC LIMIT 1").FirstOrDefault();
                    if (lastCoord != null)
                    {
                        Console.WriteLine($"[LocationAgentService] Son kayıt - Id: {lastCoord.Id}, MemberId: '{lastCoord.MemberId}', Lat: {lastCoord.Latitude}, Lon: {lastCoord.Longitude}");
                    }

                    _logger.LogInformation($"=== LOCATIONAGENTSERVICE KONUM ALINDI ===");
                    _logger.LogInformation($"Konum: Lat={location.Latitude}, Lon={location.Longitude}");
                    _logger.LogInformation($"GlobalVariables güncellendi: Lat={GlobalVariables.MemberLastCoordinate.Latitude}, Lon={GlobalVariables.MemberLastCoordinate.Longitude}");
                    _logger.LogInformation($"Veritabanına kaydedildi: MemberId={memberId}, LastUpdateTime={coordinate.LastUpdateTime}");
                    _logger.LogInformation($"entMemberCoordinate tablosuna INSERT yapıldı: Lat={coordinate.Latitude}, Lon={coordinate.Longitude}");
                    
                    Console.WriteLine("========================================");
                    Console.WriteLine("[LOCATIONAGENTSERVICE] Konum kaydedildi:");
                    Console.WriteLine($"  MemberId: '{coordinate.MemberId}'");
                    Console.WriteLine($"  CoordinateX (decimal): {coordinate.CoordinateX}");
                    Console.WriteLine($"  CoordinateY (decimal): {coordinate.CoordinateY}");
                    Console.WriteLine($"  Latitude (double): {coordinate.Latitude}");
                    Console.WriteLine($"  Longitude (double): {coordinate.Longitude}");
                    Console.WriteLine($"  CreatedOn: {coordinate.CreatedOn:yyyy-MM-dd HH:mm:ss}");
                    Console.WriteLine($"  LastUpdateTime: {coordinate.LastUpdateTime:yyyy-MM-dd HH:mm:ss}");
                    Console.WriteLine("========================================");
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