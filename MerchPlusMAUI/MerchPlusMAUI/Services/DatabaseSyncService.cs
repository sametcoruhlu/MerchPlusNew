using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Maui.ApplicationModel;
using SQLite;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MerchPlusMAUI.Models;
using MerchPlusMAUI.Services;
using entMerchPlus;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace MerchPlusMAUI.Services
{
    public class DatabaseSyncService : IHostedService, IDatabaseSyncService
    {
        private readonly ILogger<DatabaseSyncService> _logger;
        private readonly HttpClient _httpClient;
        private readonly HttpClient _httpClient2;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private bool _isRunning;
        
        private const int COORDINATE_SYNC_INTERVAL = 300000; // 5 minutes
        private const int ROUTE_SYNC_INTERVAL = 360000; // 6 minutes
        private const int PHOTO_SYNC_INTERVAL = 360000; // 6 minutes
        private const int DB_CHECK_INTERVAL = 15000; // 15 seconds

        public DatabaseSyncService(ILogger<DatabaseSyncService> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient { BaseAddress = new Uri(GlobalVariables.APIPath) };
            _httpClient2 = new HttpClient { BaseAddress = new Uri(GlobalVariables.APIPath2) };
            
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            _httpClient2.DefaultRequestHeaders.Accept.Clear();
            _httpClient2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            _cancellationTokenSource = new CancellationTokenSource();
            _isRunning = false;
        }

        public async Task StartSync()
        {
            await StartAsync(CancellationToken.None);
        }

        public async Task StopSync()
        {
            await StopAsync(CancellationToken.None);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Database sync service is starting.");
            
            // Start the background task without awaiting it
            _ = Task.Run(async () => await ExecuteAsync(), cancellationToken);
            
            // Return immediately so it doesn't block the UI
            await Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Database sync service is stopping.");
            Stop();
            await Task.CompletedTask;
        }

        private async Task ExecuteAsync()
        {
            if (_isRunning)
            {
                _logger.LogInformation("Sync service is already running");
                return;
            }

            _isRunning = true;

            try
            {
                while (!_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    await SyncData();
                    await Task.Delay(TimeSpan.FromMinutes(5), _cancellationTokenSource.Token);
                }
            }
            catch (System.OperationCanceledException)
            {
                _logger.LogInformation("Sync service was cancelled");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred in sync service");
            }
            finally
            {
                _isRunning = false;
            }
        }

        private async Task SyncData()
        {
            try
            {
                using var connection = new SQLiteConnection(GlobalVariables.DatabasePath);
                connection.CreateTable<MemberCoordinate>();

                var fiveMinutesAgo = DateTime.UtcNow.AddMinutes(-5);
                var recentCoordinates = connection.Table<MemberCoordinate>()
                    .Where(c => c.LastUpdateTime >= fiveMinutesAgo)
                    .ToList();

                _logger.LogInformation($"Found {recentCoordinates.Count} coordinates to sync");

                foreach (var coordinate in recentCoordinates)
                {
                    // TODO: Implement sync logic with server
                    _logger.LogInformation($"Syncing coordinate for member {coordinate.MemberId} at {coordinate.LastUpdateTime}");
                    await Task.Delay(100); // Add a small delay to prevent overwhelming the server
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while syncing data");
            }
        }

        public void Stop()
        {
            _cancellationTokenSource.Cancel();
        }

        private async Task SyncCoordinates()
        {
            using var db = new SQLiteConnection(GlobalVariables.DatabasePath);
            var coordinates = db.Query<MemberCoordinate>("SELECT * from entMemberCoordinate");

            if (coordinates.Any())
            {
                var response = await PostToApi(_httpClient, "MemberCoordinateController/InsertMemberCoordinateFromAndroidArray", coordinates);
                
                if (response?.GetProperty("Result").GetString() != "NOK")
                {
                    db.DeleteAll<MemberCoordinate>();
                }
                else
                {
                    _logger.LogWarning("Database Sync Failed > Member Coordinate upload");
                }
            }
        }

        private async Task SyncMemberRoutes()
        {
            _logger.LogInformation("Synchronizing data with Server > MemberRoute!");

            using var db = new SQLiteConnection(GlobalVariables.DatabasePath);
            var routes = db.Query<entMemberRoute>("SELECT * from entMemberRoute WHERE IsSentToServer=0 or IsSentToServer is null");

            foreach (var route in routes)
            {
                var syncData = new MemberRouteSyncData
                {
                    MemberRoute = route,
                    MemberRouteDetail = db.Query<entMemberRouteDetail>("SELECT * from entMemberRouteDetail WHERE MemberRouteId=?", route.Id).ToList(),
                    MemberRoutePhoto = db.Query<entMemberRoutePhoto>("SELECT * from entMemberRoutePhoto WHERE MemberRouteId=?", route.Id).ToList(),
                    SalesOrder = db.Query<SalesOrder>("SELECT * FROM entSalesOrder WHERE MemberRouteId=?", route.Id).ToList(),
                    ProductExpiration = db.Query<ProductExpiration>("SELECT * FROM entProductExpiration WHERE MemberRouteId=?", route.Id).ToList()
                };

                if (syncData.SalesOrder.Any())
                {
                    syncData.SalesOrderDetail = db.Query<SalesOrderDetail>(
                        "SELECT * FROM entSalesOrderDetail WHERE SalesOrderId=?", 
                        syncData.SalesOrder.First().Id).ToList();
                }

                var endpoint = syncData.SalesOrder.Any() 
                    ? "MemberRouteController/CompleteMemberRouteWiselyFromJsonArrayWithSalesOrder"
                    : "MemberRouteController/CompleteMemberRouteWiselyFromJsonArray";

                var response = await PostToApi(_httpClient, endpoint, syncData);

                if (response?.GetProperty("Result").GetString() != "NOK")
                {
                    db.Execute("UPDATE entMemberRoute SET IsSentToServer=? WHERE Id=?", true, route.Id);
                    db.Execute("UPDATE entMemberRouteDetail SET IsSentToServer=? WHERE MemberRouteId=?", true, route.Id);
                    db.Execute("UPDATE entProductExpiration SET IsSentToServer=? WHERE MemberRouteId=?", true, route.Id);
                }
                else
                {
                    _logger.LogWarning("Database Sync Failed > MemberRoute");
                }
            }
        }

        private async Task SyncDisplayPhotos()
        {
            _logger.LogInformation("Synchronizing data with Server > MemberDisplayPhoto!");

            using var db = new SQLiteConnection(GlobalVariables.DatabasePath);
            var photos = db.Query<MemberDisplayPhotos>("SELECT * from entMemberDisplayPhotos WHERE IsSentToServer=false");

            foreach (var photo in photos)
            {
                photo.IsSentToServer = true;
                var response = await PostToApi(_httpClient2, "MemberDisplayPhotosController/InsertMemberDisplayPhotos", photo);

                if (response?.GetProperty("Result").GetString() != "NOK")
                {
                    db.Execute("UPDATE entMemberDisplayPhotos SET IsSentToServer=? WHERE CustomerProductId=?", 
                        true, photo.CustomerProductId);
                }
                else
                {
                    _logger.LogWarning("Database Sync Failed > MemberDisplayPhoto");
                }
            }
        }

        private async Task CheckDatabase()
        {
            using var db = new SQLiteConnection(GlobalVariables.DatabasePath);
            var configs = db.Query<MerchPlusMAUI.Models.entConfiguration>("SELECT * from entConfiguration WHERE Name=?", "RetailDataDateTime");

            if (configs.Any() && DateTime.TryParse(configs[0].Value, out var configDate))
            {
                if (configDate.Date < DateTime.Today)
                {
                    File.Delete(GlobalVariables.DatabasePath);
                    Application.Current.Quit();
                }
            }
        }

        private async Task<JsonElement?> PostToApi(HttpClient client, string endpoint, object data)
        {
            try
            {
                var jsonString = JsonSerializer.Serialize(data, _jsonOptions);
                var content = new StringContent(jsonString);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    
                var response = await client.PostAsync(endpoint, content);
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<JsonElement>(responseString);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error posting to API: {Endpoint}", endpoint);
                return null;
            }
        }

        /// <summary>
        /// Server'a gönderilmemiş verileri senkronize eder
        /// </summary>
        public async Task SyncPendingDataToServer()
        {
            try
            {
                _logger.LogInformation("Starting pending data sync to server...");

                using var db = new SQLiteConnection(GlobalVariables.DatabasePath);

                // Gönderilmemiş MemberRoute'ları al
                var pendingRoutes = db.Table<entMemberRoute>()
                    .Where(r => r.IsSentToServer == false)
                    .ToList();

                foreach (var route in pendingRoutes)
                {
                    await SyncMemberRouteToServer(route, db);
                }

                _logger.LogInformation($"Completed pending data sync. Processed {pendingRoutes.Count} routes.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during pending data sync to server");
            }
        }

        /// <summary>
        /// Tek bir MemberRoute'u server'a senkronize eder
        /// </summary>
        private async Task SyncMemberRouteToServer(entMemberRoute route, SQLiteConnection db)
        {
            try
            {
				// Prepare lists first to avoid referencing the variable inside its own initializer
				var memberRouteDetails = db.Table<entMemberRouteDetail>()
					.Where(d => d.MemberRouteId == route.Id && d.IsSentToServer == false)
					.ToList();

				var memberRoutePhotos = db.Table<entMemberRoutePhoto>()
					.Where(p => p.MemberRouteId == route.Id && p.IsSentToServer == false)
					.ToList();

				var salesOrders = db.Table<SalesOrder>()
					.Where(s => s.MemberRouteId == route.Id && s.IsSentToServer == false)
					.ToList();

				var salesOrderIds = salesOrders.Select(s => s.Id).ToList();
				var salesOrderDetails = db.Table<SalesOrderDetail>()
					.Where(sd => sd.SalesOrderId != 0 && salesOrderIds.Contains(sd.SalesOrderId) && sd.IsSentToServer == false)
					.ToList();

				var productExpirations = db.Table<ProductExpiration>()
					.Where(pe => pe.MemberRouteId == route.Id && pe.IsSentToServer == false)
					.ToList();

				var displayPhotos = db.Table<MemberDisplayPhotos>()
					.Where(mdp => mdp.MemberRouteId == route.Id && mdp.IsSentToServer == false)
					.ToList();

				var syncData = new MemberRouteSyncData
				{
					MemberRoute = route,
					MemberRouteDetail = memberRouteDetails,
					MemberRoutePhoto = memberRoutePhotos,
					SalesOrder = salesOrders,
					SalesOrderDetail = salesOrderDetails,
					ProductExpiration = productExpirations,
					MemberDisplayPhotos = displayPhotos
				};

                // Server'a gönder
                var json = JsonSerializer.Serialize(syncData, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("api/MemberRoute/Sync", content);
                
                if (response.IsSuccessStatusCode)
                {
                    // Başarılı gönderim sonrası flag'leri güncelle
                    await MarkDataAsSentToServer(route.Id, db);
                    _logger.LogInformation($"Successfully synced MemberRoute {route.Id} to server");
                }
                else
                {
                    _logger.LogWarning($"Failed to sync MemberRoute {route.Id} to server. Status: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error syncing MemberRoute {route.Id} to server");
            }
        }

        /// <summary>
        /// Başarılı senkronizasyon sonrası flag'leri günceller
        /// </summary>
        private async Task MarkDataAsSentToServer(int memberRouteId, SQLiteConnection db)
        {
            try
            {
                var now = DateTime.UtcNow;

                // MemberRoute'u güncelle
                var route = db.Table<entMemberRoute>().FirstOrDefault(r => r.Id == memberRouteId);
                if (route != null)
                {
                    route.IsSentToServer = true;
                    route.SentToServerOn = now;
                    db.Update(route);
                }

                // İlgili detayları güncelle
                var details = db.Table<entMemberRouteDetail>()
                    .Where(d => d.MemberRouteId == memberRouteId && d.IsSentToServer == false)
                    .ToList();
                foreach (var detail in details)
                {
                    detail.IsSentToServer = true;
                    detail.SentToServerOn = now;
                    db.Update(detail);
                }

                // Fotoğrafları güncelle
                var photos = db.Table<entMemberRoutePhoto>()
                    .Where(p => p.MemberRouteId == memberRouteId && p.IsSentToServer == false)
                    .ToList();
                foreach (var photo in photos)
                {
                    photo.IsSentToServer = true;
                    // Eski projede entMemberRoutePhoto'da SentToServerOn property'si yok
                    db.Update(photo);
                }

                // Satış siparişlerini güncelle
                var salesOrders = db.Table<SalesOrder>()
                    .Where(s => s.MemberRouteId == memberRouteId && s.IsSentToServer == false)
                    .ToList();
                foreach (var order in salesOrders)
                {
                    order.IsSentToServer = true;
                    order.SentToServerOn = now;
                    db.Update(order);
                }

                // Satış sipariş detaylarını güncelle
                var salesOrderDetails = db.Table<SalesOrderDetail>()
                    .Where(sd => salesOrders.Any(s => s.Id == sd.SalesOrderId) && sd.IsSentToServer == false)
                    .ToList();
                foreach (var detail in salesOrderDetails)
                {
                    detail.IsSentToServer = true;
                    detail.SentToServerOn = now;
                    db.Update(detail);
                }

                // Ürün son kullanma tarihlerini güncelle
                var expirations = db.Table<ProductExpiration>()
                    .Where(pe => pe.MemberRouteId == memberRouteId && pe.IsSentToServer == false)
                    .ToList();
                foreach (var expiration in expirations)
                {
                    expiration.IsSentToServer = true;
                    expiration.SentToServerOn = now;
                    db.Update(expiration);
                }

                // Mağaza fotoğraflarını güncelle
                var displayPhotos = db.Table<MemberDisplayPhotos>()
                    .Where(mdp => mdp.MemberRouteId == memberRouteId && mdp.IsSentToServer == false)
                    .ToList();
                foreach (var photo in displayPhotos)
                {
                    photo.IsSentToServer = true;
                    photo.SentToServerOn = now;
                    db.Update(photo);
                }

                _logger.LogInformation($"Marked all data for MemberRoute {memberRouteId} as sent to server");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error marking data as sent to server for MemberRoute {memberRouteId}");
            }
        }
    }

    // Helper classes for sync operations
    public class MemberRouteSyncData
    {
        public entMemberRoute MemberRoute { get; set; }
        public List<entMemberRouteDetail> MemberRouteDetail { get; set; } = new List<entMemberRouteDetail>();
        public List<entMemberRoutePhoto> MemberRoutePhoto { get; set; } = new List<entMemberRoutePhoto>();
        public List<SalesOrder> SalesOrder { get; set; } = new List<SalesOrder>();
        public List<SalesOrderDetail> SalesOrderDetail { get; set; } = new List<SalesOrderDetail>();
        public List<ProductExpiration> ProductExpiration { get; set; } = new List<ProductExpiration>();
        public List<MemberDisplayPhotos> MemberDisplayPhotos { get; set; } = new List<MemberDisplayPhotos>();
    }

} 