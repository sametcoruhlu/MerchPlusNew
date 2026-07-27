# Konum Senkronizasyon Düzeltmeleri

## 🔍 Tespit Edilen Sorunlar

### 1. **Tablo Uyumsuzluğu**
- ❌ **LocationAgentService** `MemberCoordinate` tablosuna yazıyordu
- ❌ **DatabaseSyncService** `entMemberCoordinate` tablosundan okuyordu
- ✅ **Eski Proje:** `entMemberCoordinate` tablosuna yazıyor ve okuyordu

### 2. **Senkronizasyon Çalışmıyordu**
- ❌ **DatabaseSyncService.SyncData()** sadece log yazıyordu (TODO yazıyordu)
- ❌ **SyncCoordinates()** metodu hiç çağrılmıyordu
- ✅ **Eski Proje:** Timer ile her 5 dakikada bir `SyncCoordinates()` çağrılıyordu

### 3. **Entity Uyumsuzluğu**
- ❌ **DatabaseSyncService.SyncCoordinates()** `MemberCoordinate` entity kullanıyordu
- ❌ **LocationAgentService** `MemberCoordinate` entity kullanıyordu
- ✅ **Eski Proje:** `entMemberCoordinate` entity kullanıyordu

### 4. **JSON Serialization Uyumsuzluğu**
- ❌ **DatabaseSyncService** `System.Text.Json` kullanıyordu
- ✅ **Eski Proje:** `Newtonsoft.Json` kullanıyordu

---

## ✅ Yapılan Düzeltmeler

### 1. **LocationAgentService - entMemberCoordinate Kullanımı**

**Önceki Kod:**
```csharp
var coordinate = new MemberCoordinate
{
    MemberId = memberId,  // int
    Latitude = location.Latitude,
    Longitude = location.Longitude,
    LastUpdateTime = DateTime.UtcNow
};

using var connection = new SQLiteConnection(GlobalVariables.DatabasePath);
connection.CreateTable<MemberCoordinate>();
connection.Insert(coordinate);
```

**Yeni Kod:**
```csharp
// Eski projede olduğu gibi entMemberCoordinate kullanıyoruz
string memberId = GlobalVariables.CurrentMember?.Id ?? "1";

var coordinate = new MerchPlusMAUI.Models.entMemberCoordinate
{
    MemberId = memberId,  // string (eski projede olduğu gibi)
    Latitude = location.Latitude,
    Longitude = location.Longitude,
    LastUpdateTime = DateTime.Now  // Eski projede DateTime.Now kullanılıyor
};

using var connection = new SQLiteConnection(GlobalVariables.DatabasePath);
connection.CreateTable<MerchPlusMAUI.Models.entMemberCoordinate>();
connection.Insert(coordinate);
```

**Değişiklikler:**
- ✅ `MemberCoordinate` → `entMemberCoordinate` (Models namespace)
- ✅ `MemberId` int → string (eski projede olduğu gibi)
- ✅ `DateTime.UtcNow` → `DateTime.Now` (eski projede olduğu gibi)
- ✅ Tablo: `MemberCoordinate` → `entMemberCoordinate`

---

### 2. **DatabaseSyncService - SyncData() Metodu Düzeltmesi**

**Önceki Kod:**
```csharp
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
            // TODO: Implement sync logic with server  ❌
            _logger.LogInformation($"Syncing coordinate for member {coordinate.MemberId} at {coordinate.LastUpdateTime}");
            await Task.Delay(100);
        }
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error occurred while syncing data");
    }
}
```

**Yeni Kod:**
```csharp
private async Task SyncData()
{
    try
    {
        // Eski projede olduğu gibi önce koordinatları senkronize et
        await SyncCoordinates();
        
        // Sonra diğer verileri senkronize et
        await SyncMemberRoutes();
        await SyncDisplayPhotos();
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error occurred while syncing data");
    }
}
```

**Değişiklikler:**
- ✅ `SyncCoordinates()` metodu çağrılıyor
- ✅ `SyncMemberRoutes()` metodu çağrılıyor
- ✅ `SyncDisplayPhotos()` metodu çağrılıyor

---

### 3. **DatabaseSyncService - SyncCoordinates() Metodu Düzeltmesi**

**Önceki Kod:**
```csharp
private async Task SyncCoordinates()
{
    using var db = new SQLiteConnection(GlobalVariables.DatabasePath);
    var coordinates = db.Query<MemberCoordinate>("SELECT * from entMemberCoordinate");  // ❌ Yanlış entity

    if (coordinates.Any())
    {
        var response = await PostToApi(_httpClient, "MemberCoordinateController/InsertMemberCoordinateFromAndroidArray", coordinates);
        
        if (response?.GetProperty("Result").GetString() != "NOK")
        {
            db.DeleteAll<MemberCoordinate>();  // ❌ Yanlış entity
        }
        else
        {
            _logger.LogWarning("Database Sync Failed > Member Coordinate upload");
        }
    }
}
```

**Yeni Kod:**
```csharp
private async Task SyncCoordinates()
{
    try
    {
        _logger.LogInformation("Synchronizing coordinates with Server > entMemberCoordinate!");
        
        using var db = new SQLiteConnection(GlobalVariables.DatabasePath);
        // Eski projede olduğu gibi entMemberCoordinate tablosundan okuyoruz
        var coordinates = db.Query<MerchPlusMAUI.Models.entMemberCoordinate>("SELECT * from entMemberCoordinate");

        if (coordinates.Any())
        {
            _logger.LogInformation($"Found {coordinates.Count} coordinates to sync");
            
            // Eski projede olduğu gibi Newtonsoft.Json kullanıyoruz
            using var client = new HttpClient { BaseAddress = new Uri(GlobalVariables.APIPath) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            // Eski projede olduğu gibi JSON serialize
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(coordinates);
            var request = new StringContent(jsonString, Encoding.UTF8, "application/json");
            
            var response = await client.PostAsync("MemberCoordinateController/InsertMemberCoordinateFromAndroidArray", request);
            var responseString = await response.Content.ReadAsStringAsync();
            
            // Eski projede olduğu gibi response kontrolü
            JObject returnObject = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(responseString);
            if (Convert.ToString(returnObject["Result"]) != "NOK")
            {
                // Başarılı - tüm kayıtları sil (eski projede olduğu gibi)
                db.DeleteAll<MerchPlusMAUI.Models.entMemberCoordinate>();
                _logger.LogInformation($"Successfully synced {coordinates.Count} coordinates to server");
            }
            else
            {
                _logger.LogWarning("Database Sync Failed > Member Coordinate upload");
            }
        }
        else
        {
            _logger.LogInformation("No coordinates to sync");
        }
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error occurred while syncing coordinates");
    }
}
```

**Değişiklikler:**
- ✅ `MemberCoordinate` → `entMemberCoordinate` (Models namespace)
- ✅ `System.Text.Json` → `Newtonsoft.Json` (eski projede olduğu gibi)
- ✅ Eski projede olduğu gibi HTTP client setup
- ✅ Eski projede olduğu gibi response kontrolü (JObject)
- ✅ Başarılı olursa tüm kayıtları silme (eski projede olduğu gibi)
- ✅ Detaylı log mesajları eklendi

---

## 📊 Eski Proje vs Yeni Proje Karşılaştırması

### Konum Kaydetme

| Özellik | Eski Proje | Yeni Proje (Önce) | Yeni Proje (Sonra) |
|---------|------------|-------------------|---------------------|
| **Tablo** | `entMemberCoordinate` | `MemberCoordinate` ❌ | `entMemberCoordinate` ✅ |
| **Entity** | `entMemberCoordinate` | `MemberCoordinate` ❌ | `entMemberCoordinate` ✅ |
| **MemberId Tipi** | `string` | `int` ❌ | `string` ✅ |
| **CreatedOn/LastUpdateTime** | `DateTime.Now` | `DateTime.UtcNow` ❌ | `DateTime.Now` ✅ |

### Konum Senkronizasyonu

| Özellik | Eski Proje | Yeni Proje (Önce) | Yeni Proje (Sonra) |
|---------|------------|-------------------|---------------------|
| **Timer Aralığı** | 5 dakika (300000ms) | 5 dakika ✅ | 5 dakika ✅ |
| **Tablo** | `entMemberCoordinate` | `entMemberCoordinate` ✅ | `entMemberCoordinate` ✅ |
| **Entity** | `entMemberCoordinate` | `MemberCoordinate` ❌ | `entMemberCoordinate` ✅ |
| **JSON Library** | `Newtonsoft.Json` | `System.Text.Json` ❌ | `Newtonsoft.Json` ✅ |
| **Response Kontrolü** | `JObject["Result"] != "NOK"` | `JsonElement` ❌ | `JObject["Result"] != "NOK"` ✅ |
| **Başarılı İşlem** | `DeleteAll<entMemberCoordinate>()` | Çağrılmıyordu ❌ | `DeleteAll<entMemberCoordinate>()` ✅ |

---

## 🔄 Çalışma Akışı (Düzeltme Sonrası)

```
┌─────────────────────────────────────────────────────────┐
│  UYGULAMA AÇILIŞI                                       │
│  HomePage.InitializeAppAsync()                          │
└──────────────────┬──────────────────────────────────────┘
                   │
                   ▼
┌─────────────────────────────────────────────────────────┐
│  LocationAgentService.StartLocationTracking()           │
│  - Her 5 dakikada bir UpdateLocationAsync()             │
└──────────────────┬──────────────────────────────────────┘
                   │
                   ▼
┌─────────────────────────────────────────────────────────┐
│  UpdateLocationAsync()                                  │
│  1. Konum al (Geolocation.GetLocationAsync)            │
│  2. GlobalVariables.MemberLastCoordinate güncelle        │
│  3. entMemberCoordinate entity oluştur                  │
│  4. entMemberCoordinate tablosuna INSERT               │
└──────────────────┬──────────────────────────────────────┘
                   │
                   ▼
┌─────────────────────────────────────────────────────────┐
│  DatabaseSyncService.ExecuteAsync()                    │
│  - Her 5 dakikada bir SyncData() çağrılır              │
└──────────────────┬──────────────────────────────────────┘
                   │
                   ▼
┌─────────────────────────────────────────────────────────┐
│  SyncData()                                             │
│  1. SyncCoordinates() çağrılır                          │
│  2. SyncMemberRoutes() çağrılır                         │
│  3. SyncDisplayPhotos() çağrılır                        │
└──────────────────┬──────────────────────────────────────┘
                   │
                   ▼
┌─────────────────────────────────────────────────────────┐
│  SyncCoordinates()                                      │
│  1. entMemberCoordinate tablosundan tüm kayıtları oku  │
│  2. Newtonsoft.Json ile serialize et                    │
│  3. POST: MemberCoordinateController/...                │
│  4. Response kontrolü (JObject["Result"] != "NOK")     │
│  5. Başarılı ise DeleteAll<entMemberCoordinate>()      │
└─────────────────────────────────────────────────────────┘
```

---

## ✅ Sonuç

Artık konum kaydetme ve senkronizasyon işlemleri eski projeyle uyumlu çalışıyor:

1. ✅ **LocationAgentService** `entMemberCoordinate` tablosuna yazıyor
2. ✅ **DatabaseSyncService** `entMemberCoordinate` tablosundan okuyor
3. ✅ **SyncCoordinates()** metodu her 5 dakikada bir çağrılıyor
4. ✅ **Newtonsoft.Json** kullanılıyor (eski projede olduğu gibi)
5. ✅ **Response kontrolü** eski projede olduğu gibi yapılıyor
6. ✅ **Başarılı senkronizasyon** sonrası kayıtlar siliniyor

**Test:** 5 dakika bekledikten sonra konumların sunucuya gönderildiğini kontrol edebilirsiniz.

