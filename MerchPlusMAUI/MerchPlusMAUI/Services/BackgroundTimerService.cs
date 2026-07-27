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
                
                // 1. Veritabanı temizlik işlemleri
                await PerformDatabaseMaintenance();

                _logger.LogInformation("Background timer work completed at: {Time}", DateTimeOffset.Now);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during background timer work");
            }
        }

        /// <summary>
        /// Veritabanı bakım işlemlerini yapar
        /// </summary>
        private async Task PerformDatabaseMaintenance()
        {
            try
            {
                // Temizlik işlemi DatabaseSyncService başlangıcında tek noktadan yönetiliyor.
                // Aynı silme işlemini burada tekrar çalıştırmıyoruz.
                _logger.LogInformation("Database maintenance skipped: startup cleanup is centralized in DatabaseSyncService.");
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
