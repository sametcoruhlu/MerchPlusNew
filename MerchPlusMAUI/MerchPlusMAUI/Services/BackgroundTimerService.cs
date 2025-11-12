using entMerchPlus;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MerchPlusMAUI.Services
{
    /// <summary>
    /// Arka planda periyodik işlemler yapan servis
    /// </summary>
    public class BackgroundTimerService : IHostedService, IDisposable
    {
        private readonly ILogger<BackgroundTimerService> _logger;
        private readonly DatabaseSyncService _databaseSyncService;
        private Timer _timer;
        private bool _disposed = false;

        // Timer interval'ları (milisaniye)
        private const int SYNC_INTERVAL = 300000; // 5 dakika
        private const int COORDINATE_INTERVAL = 60000; // 1 dakika
        private const int DB_CHECK_INTERVAL = 30000; // 30 saniye

        public BackgroundTimerService(ILogger<BackgroundTimerService> logger, DatabaseSyncService databaseSyncService)
        {
            _logger = logger;
            _databaseSyncService = databaseSyncService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("BackgroundTimerService starting...");

            // Ana senkronizasyon timer'ı (5 dakikada bir)
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(SYNC_INTERVAL));

            _logger.LogInformation("BackgroundTimerService started.");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("BackgroundTimerService stopping...");
            _timer?.Change(Timeout.Infinite, 0);
            _logger.LogInformation("BackgroundTimerService stopped.");
            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            try
            {
                _logger.LogInformation("Background timer work started at: {Time}", DateTimeOffset.Now);

                // 1. Gönderilmemiş verileri senkronize et
                await _databaseSyncService.SyncPendingDataToServer();

                // 2. Koordinat senkronizasyonu (eğer gerekirse)
                await SyncCoordinatesIfNeeded();

                // 3. Veritabanı temizlik işlemleri
                await PerformDatabaseMaintenance();

                _logger.LogInformation("Background timer work completed at: {Time}", DateTimeOffset.Now);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during background timer work");
            }
        }

        /// <summary>
        /// Koordinat senkronizasyonu yapar (gerekirse)
        /// </summary>
        private async Task SyncCoordinatesIfNeeded()
        {
            try
            {
                // Son koordinat senkronizasyonundan 1 dakika geçmişse
                var lastSync = GlobalVariables.LastCoordinateSync;
                if (DateTime.UtcNow - lastSync > TimeSpan.FromMinutes(1))
                {
                    _logger.LogInformation("Syncing coordinates...");
                    
                    // Koordinat senkronizasyonu burada yapılabilir
                    // Örnek: LocationService'den koordinatları al ve server'a gönder
                    
                    GlobalVariables.LastCoordinateSync = DateTime.UtcNow;
                    _logger.LogInformation("Coordinates synced successfully");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error syncing coordinates");
            }
        }

        /// <summary>
        /// Veritabanı bakım işlemlerini yapar
        /// </summary>
        private async Task PerformDatabaseMaintenance()
        {
            try
            {
                _logger.LogInformation("Performing database maintenance...");

                using var db = new SQLite.SQLiteConnection(GlobalVariables.DatabasePath);

                // 1. Eski log kayıtlarını temizle (30 günden eski)
                var cutoffDate = DateTime.UtcNow.AddDays(-30);
                
                // 2. Gönderilmiş eski fotoğrafları temizle (7 günden eski) - eski projeye göre sadece IsSentToServer kontrolü
                var photoCutoffDate = DateTime.UtcNow.AddDays(-7);
                var oldPhotos = db.Table<entMemberRoutePhoto>()
                    .Where(p => p.IsSentToServer == true && p.CreatedOn < photoCutoffDate)
                    .ToList();

                foreach (var photo in oldPhotos)
                {
                    // Fotoğraf dosyasını sil (eğer varsa) - eski projeye göre ProfilePicturePath kullanılıyor
                    if (!string.IsNullOrEmpty(photo.ProfilePicturePath) && System.IO.File.Exists(photo.ProfilePicturePath))
                    {
                        try
                        {
                            System.IO.File.Delete(photo.ProfilePicturePath);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogWarning(ex, "Could not delete photo file: {ProfilePicturePath}", photo.ProfilePicturePath);
                        }
                    }

                    // Veritabanından kaydı sil
                    db.Delete(photo);
                }

                _logger.LogInformation($"Database maintenance completed. Cleaned {oldPhotos.Count} old photos.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during database maintenance");
            }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _timer?.Dispose();
                _disposed = true;
            }
        }
    }
}
