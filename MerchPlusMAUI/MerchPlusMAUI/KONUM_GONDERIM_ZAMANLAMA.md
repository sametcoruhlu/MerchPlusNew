# Koordinat Bilgilerinin Sunucuya Gönderilmesi - Zamanlama ve Akış

## 📅 Ne Zaman Gönderilir?

### 1. **Uygulama Açılışında Başlatma**

Koordinat senkronizasyon servisi (`DatabaseSyncService`) **uygulama açıldığında otomatik olarak başlatılır**:

**Başlatma Yeri 1: App.xaml.cs - OnStart()**
```csharp
// App.xaml.cs:55-64
var databaseSyncService = _serviceProvider.GetService<DatabaseSyncService>();
if (databaseSyncService != null)
{
    await databaseSyncService.StartAsync(CancellationToken.None);
    GlobalVariables.DatabaseSyncStarted = true;
    _logger?.LogInformation("DatabaseSyncService started");
}
```

**Başlatma Yeri 2: HomePage.xaml.cs - InitializeAppAsync()**
```csharp
// HomePage.xaml.cs:280-287
if (!GlobalVariables.DatabaseSyncStarted)
{
    GlobalVariables.DatabaseSyncStarted = true;
    if (_databaseService != null)
    {
        _ = Task.Run(async () => await _databaseService.StartSync());
        _logger.LogInformation("Database sync service started successfully");
    }
}
```

### 2. **Periyodik Gönderim - Her 5 Dakikada Bir**

Servis başlatıldıktan sonra, **her 5 dakikada bir** otomatik olarak koordinatları sunucuya gönderir:

```csharp
// DatabaseSyncService.cs:86-116
private async Task ExecuteAsync()
{
    _isRunning = true;
    
    try
    {
        while (!_cancellationTokenSource.Token.IsCancellationRequested)
        {
            await SyncData();  // ← Burada koordinatlar gönderiliyor
            await Task.Delay(TimeSpan.FromMinutes(5), _cancellationTokenSource.Token);  // ← 5 dakika bekle
        }
    }
    catch (System.OperationCanceledException)
    {
        _logger.LogInformation("Sync service was cancelled");
    }
    finally
    {
        _isRunning = false;
    }
}
```

**Zamanlama:**
- ⏰ **İlk gönderim:** Servis başlatıldıktan hemen sonra (0. dakika)
- ⏰ **Sonraki gönderimler:** Her 5 dakikada bir (5, 10, 15, 20, 25, 30... dakika)

---

## 🔄 Nasıl Gönderilir?

### Adım 1: Konum Kaydetme (LocationAgentService)

**Her 5 dakikada bir** konum alınır ve `entMemberCoordinate` tablosuna kaydedilir:

```csharp
// LocationAgentService.cs:118-190
private async Task UpdateLocationAsync()
{
    // 1. Konum al
    var location = await _geolocation.GetLocationAsync(...);
    
    // 2. entMemberCoordinate entity oluştur
    var coordinate = new MerchPlusMAUI.Models.entMemberCoordinate
    {
        MemberId = memberId,
        Latitude = location.Latitude,
        Longitude = location.Longitude,
        LastUpdateTime = DateTime.Now
    };
    
    // 3. Veritabanına kaydet
    using var connection = new SQLiteConnection(GlobalVariables.DatabasePath);
    connection.CreateTable<MerchPlusMAUI.Models.entMemberCoordinate>();
    connection.Insert(coordinate);
}
```

**Zamanlama:**
- ⏰ **Her 5 dakikada bir** yeni konum kaydedilir
- 📍 **Tablo:** `entMemberCoordinate`
- 💾 **Kayıt:** Her konum ayrı bir satır olarak kaydedilir

---

### Adım 2: Koordinat Senkronizasyonu (DatabaseSyncService)

**Her 5 dakikada bir** `entMemberCoordinate` tablosundaki **TÜM** koordinatlar sunucuya gönderilir:

```csharp
// DatabaseSyncService.cs:140-188
private async Task SyncCoordinates()
{
    // 1. Veritabanından TÜM koordinatları oku
    using var db = new SQLiteConnection(GlobalVariables.DatabasePath);
    var coordinates = db.Query<MerchPlusMAUI.Models.entMemberCoordinate>(
        "SELECT * from entMemberCoordinate"
    );
    
    if (coordinates.Any())
    {
        // 2. Newtonsoft.Json ile serialize et
        var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(coordinates);
        var request = new StringContent(jsonString, Encoding.UTF8, "application/json");
        
        // 3. Sunucuya POST yap
        var response = await client.PostAsync(
            "MemberCoordinateController/InsertMemberCoordinateFromAndroidArray", 
            request
        );
        
        // 4. Response kontrolü
        JObject returnObject = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(responseString);
        if (Convert.ToString(returnObject["Result"]) != "NOK")
        {
            // 5. Başarılı ise TÜM kayıtları sil
            db.DeleteAll<MerchPlusMAUI.Models.entMemberCoordinate>();
        }
    }
}
```

**Önemli Notlar:**
- ✅ **Tüm koordinatlar** tek seferde gönderilir (array olarak)
- ✅ **Başarılı gönderim** sonrası tüm kayıtlar silinir
- ✅ **Başarısız gönderim** sonrası kayıtlar silinmez (bir sonraki denemede tekrar gönderilir)

---

## 📊 Zaman Çizelgesi Örneği

```
00:00 - Uygulama açıldı
       ├─ LocationAgentService başlatıldı
       ├─ DatabaseSyncService başlatıldı
       └─ İlk konum alındı → entMemberCoordinate tablosuna kaydedildi

00:00 - İlk senkronizasyon (hemen)
       └─ entMemberCoordinate tablosundaki koordinatlar sunucuya gönderildi
          └─ Başarılı → Tablo temizlendi

00:05 - LocationAgentService: Yeni konum alındı
       └─ entMemberCoordinate tablosuna kaydedildi

00:05 - DatabaseSyncService: Senkronizasyon
       └─ entMemberCoordinate tablosundaki koordinatlar sunucuya gönderildi
          └─ Başarılı → Tablo temizlendi

00:10 - LocationAgentService: Yeni konum alındı
       └─ entMemberCoordinate tablosuna kaydedildi

00:10 - DatabaseSyncService: Senkronizasyon
       └─ entMemberCoordinate tablosundaki koordinatlar sunucuya gönderildi
          └─ Başarılı → Tablo temizlendi

... (Her 5 dakikada bir tekrar eder)
```

---

## 🔍 Detaylı Akış Şeması

```
┌─────────────────────────────────────────────────────────┐
│  UYGULAMA AÇILIŞI (App.xaml.cs)                         │
│  OnStart() → DatabaseSyncService.StartAsync()           │
└──────────────────┬──────────────────────────────────────┘
                   │
                   ▼
┌─────────────────────────────────────────────────────────┐
│  DatabaseSyncService.ExecuteAsync() - Background Loop   │
│  ┌──────────────────────────────────────────────────┐  │
│  │  while (!cancelled) {                             │  │
│  │    1. SyncData() çağrılır                         │  │
│  │    2. 5 dakika bekle                               │  │
│  │  }                                                 │  │
│  └──────────────────────────────────────────────────┘  │
└──────────────────┬──────────────────────────────────────┘
                   │
                   ▼
┌─────────────────────────────────────────────────────────┐
│  SyncData()                                             │
│  ┌──────────────────────────────────────────────────┐  │
│  │  1. SyncCoordinates() ← KOORDİNAT GÖNDERİMİ      │  │
│  │  2. SyncMemberRoutes()                            │  │
│  │  3. SyncDisplayPhotos()                           │  │
│  └──────────────────────────────────────────────────┘  │
└──────────────────┬──────────────────────────────────────┘
                   │
                   ▼
┌─────────────────────────────────────────────────────────┐
│  SyncCoordinates() - KOORDİNAT GÖNDERİMİ                │
│  ┌──────────────────────────────────────────────────┐  │
│  │  1. SELECT * FROM entMemberCoordinate            │  │
│  │     → Tüm koordinatları oku                      │  │
│  │                                                    │  │
│  │  2. Newtonsoft.Json.SerializeObject(coordinates) │  │
│  │     → JSON array'e dönüştür                       │  │
│  │                                                    │  │
│  │  3. POST MemberCoordinateController/...          │  │
│  │     → Sunucuya gönder                             │  │
│  │                                                    │  │
│  │  4. Response kontrolü                             │  │
│  │     if (Result != "NOK") {                        │  │
│  │       DELETE FROM entMemberCoordinate            │  │
│  │       → Başarılı ise tabloyu temizle             │  │
│  │     }                                             │  │
│  └──────────────────────────────────────────────────┘  │
└─────────────────────────────────────────────────────────┘
```

---

## ⚙️ Yapılandırma

### Zamanlama Ayarları

**DatabaseSyncService.cs:**
```csharp
// ExecuteAsync() içinde
await Task.Delay(TimeSpan.FromMinutes(5), _cancellationTokenSource.Token);
```

**Değiştirmek İsterseniz:**
- 5 dakika yerine farklı bir süre kullanabilirsiniz
- Örnek: `TimeSpan.FromMinutes(10)` → Her 10 dakikada bir

### API Endpoint

**DatabaseSyncService.cs:140-188**
```csharp
var response = await client.PostAsync(
    "MemberCoordinateController/InsertMemberCoordinateFromAndroidArray", 
    request
);
```

**Endpoint:** `{GlobalVariables.APIPath}/MemberCoordinateController/InsertMemberCoordinateFromAndroidArray`

---

## 📝 Özet

### Ne Zaman?
- ✅ **Uygulama açıldığında** servis başlatılır
- ✅ **Her 5 dakikada bir** otomatik olarak koordinatlar gönderilir
- ✅ **İlk gönderim** servis başlatıldıktan hemen sonra (0. dakika)

### Nasıl?
1. **Konum Kaydetme:** LocationAgentService her 5 dakikada bir konum alır ve `entMemberCoordinate` tablosuna kaydeder
2. **Koordinat Gönderimi:** DatabaseSyncService her 5 dakikada bir `entMemberCoordinate` tablosundaki **TÜM** koordinatları sunucuya gönderir
3. **Temizlik:** Başarılı gönderim sonrası tablodaki tüm kayıtlar silinir

### Önemli Notlar
- ⚠️ **Tüm koordinatlar** tek seferde gönderilir (array olarak)
- ⚠️ **Başarısız gönderim** sonrası kayıtlar silinmez (bir sonraki denemede tekrar gönderilir)
- ⚠️ **Başarılı gönderim** sonrası tablo tamamen temizlenir
- ⚠️ **Sunucu bağlantısı yoksa** koordinatlar tabloda birikir ve bağlantı kurulduğunda gönderilir

---

## 🧪 Test Etmek İçin

1. **Uygulamayı açın**
2. **5 dakika bekleyin**
3. **Logları kontrol edin:**
   ```
   "Synchronizing coordinates with Server > entMemberCoordinate!"
   "Found X coordinates to sync"
   "Successfully synced X coordinates to server"
   ```
4. **Veritabanını kontrol edin:**
   - Gönderim öncesi: `entMemberCoordinate` tablosunda kayıtlar var
   - Gönderim sonrası (başarılı): `entMemberCoordinate` tablosu boş

