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
                    SalesOrder = db.Query<entSalesOrder>("SELECT * FROM entSalesOrder WHERE MemberRouteId=?", route.Id).ToList(),
                    ProductExpiration = db.Query<entProductExpiration>("SELECT * FROM entProductExpiration WHERE MemberRouteId=?", route.Id).ToList()
                };

                if (syncData.SalesOrder.Any())
                {
                    syncData.SalesOrderDetail = db.Query<entSalesOrderDetail>(
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
    }

    // Helper classes for sync operations
    public class MemberRouteSyncData
    {
        public entMemberRoute MemberRoute { get; set; }
        public List<entMemberRouteDetail> MemberRouteDetail { get; set; } = new List<entMemberRouteDetail>();
        public List<entMemberRoutePhoto> MemberRoutePhoto { get; set; } = new List<entMemberRoutePhoto>();
        public List<entSalesOrder> SalesOrder { get; set; } = new List<entSalesOrder>();
        public List<entSalesOrderDetail> SalesOrderDetail { get; set; } = new List<entSalesOrderDetail>();
        public List<entProductExpiration> ProductExpiration { get; set; } = new List<entProductExpiration>();
    }

    public class MemberDisplayPhotos
    {
        public int Id { get; set; }
        public int CustomerProductId { get; set; }
        public string PhotoBase64 { get; set; } = string.Empty;
        public bool IsSentToServer { get; set; }
        public DateTime CreatedDate { get; set; }
    }
} 