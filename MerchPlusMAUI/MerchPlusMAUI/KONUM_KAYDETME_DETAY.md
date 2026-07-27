# Konum Kaydetme İşlemleri - Detaylı Açıklama

## 📍 Konum Kaydetme Nerede ve Nasıl Yapılıyor?

---

## 🎯 Ana Konum Kaydetme Servisleri

Uygulamada **2 farklı servis** konum kaydediyor:

### 1. **LocationAgentService** ⭐ (ANA SERVİS - 5 Dakikada Bir)

#### 📍 Nerede?
- **Dosya:** `MerchPlusMAUI/Services/LocationAgentService.cs`
- **Metod:** `UpdateLocationAsync()` (satır 118-190)
- **Başlatma:** `HomePage.xaml.cs:242` (InitializeAppAsync içinde)

#### 🔄 Nasıl Çalışıyor?

```118:190:MerchPlusMAUI/Services/LocationAgentService.cs
private async Task UpdateLocationAsync()
{
    try
    {
        // 1. Konum iznini kontrol et
        var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
        
        if (status != PermissionStatus.Granted)
        {
            status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
        }
        
        // 2. Konum al (30 saniye timeout)
        var location = await _geolocation.GetLocationAsync(new GeolocationRequest
        {
            DesiredAccuracy = GeolocationAccuracy.Medium,
            Timeout = TimeSpan.FromSeconds(30)
        });

        if (location != null)
        {
            // 3. GlobalVariables'a kaydet
            GlobalVariables.MemberLastCoordinate = new Location
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude
            };
            
            // 4. MemberId belirle
            int memberId = 1; // Varsayılan
            string memberIdString = GlobalVariables.CurrentMember?.Id ?? string.Empty;
            
            if (!string.IsNullOrEmpty(memberIdString))
            {
                if (!int.TryParse(memberIdString, out memberId))
                {
                    // Dönüştürülemezse varsayılan 1 kullan
                }
            }
            
            // 5. Entity oluştur
            var coordinate = new MemberCoordinate
            {
                MemberId = memberId,
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                LastUpdateTime = DateTime.UtcNow
            };

            // 6. Veritabanına kaydet
            using var connection = new SQLiteConnection(GlobalVariables.DatabasePath);
            connection.CreateTable<MemberCoordinate>();
            connection.Insert(coordinate);
        }
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error occurred while updating location");
    }
}
```

#### ⏰ Zamanlama
- **Aralık:** Her 5 dakikada bir
- **Döngü:** `ExecuteAsync()` metodunda (satır 75-116)
- **Çalışma:** Background thread'de sürekli döngü

```75:116:MerchPlusMAUI/Services/LocationAgentService.cs
private async Task ExecuteAsync()
{
    _isRunning = true;
    
    try
    {
        int loopCount = 0;
        while (!_cancellationTokenSource.Token.IsCancellationRequested)
        {
            loopCount++;
            await UpdateLocationAsync(); // Konum al ve kaydet
            await Task.Delay(TimeSpan.FromMinutes(5), _cancellationTokenSource.Token); // 5 dakika bekle
        }
    }
    catch (System.OperationCanceledException)
    {
        // Normal iptal durumu
    }
    finally
    {
        _isRunning = false;
    }
}
```

#### 🗄️ Veritabanı İşlemi
```sql
-- Tablo oluşturma (eğer yoksa)
CREATE TABLE IF NOT EXISTS MemberCoordinate (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    MemberId INTEGER,
    Latitude REAL,
    Longitude REAL,
    LastUpdateTime TEXT
)

-- Konum kaydetme
INSERT INTO MemberCoordinate (MemberId, Latitude, Longitude, LastUpdateTime)
VALUES (?, ?, ?, ?)
```

---

### 2. **LocationService** (OPSİYONEL - 1 Dakikada Bir)

#### 📍 Nerede?
- **Dosya:** `MerchPlusMAUI/Services/LocationService.cs`
- **Metod:** `LocationTrackingTask()` (satır 85-181)
- **Başlatma:** Manuel olarak `StartLocationTracking()` çağrılmalı

#### 🔄 Nasıl Çalışıyor?

```85:181:MerchPlusMAUI/Services/LocationService.cs
private async Task LocationTrackingTask()
{
    try
    {
        while (!_cancellationTokenSource.Token.IsCancellationRequested)
        {
            try
            {
                // 1. Konum iznini kontrol et
                var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
                
                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                }
                
                // 2. Konum al (10 saniye timeout)
                var location = await Geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(10)
                });
                
                if (location != null)
                {
                    // 3. GlobalVariables'a kaydet
                    GlobalVariables.MemberLastCoordinate = new Location
                    {
                        Latitude = location.Latitude,
                        Longitude = location.Longitude
                    };
                    
                    // 4. MemberId belirle
                    int memberId = 1; // Varsayılan
                    if (!string.IsNullOrEmpty(GlobalVariables.CurrentMemberId))
                    {
                        if (!int.TryParse(GlobalVariables.CurrentMemberId, out memberId))
                        {
                            // Dönüştürülemezse varsayılan 1 kullan
                        }
                    }
                    
                    // 5. Entity oluştur (Accuracy field'ı var)
                    var coordinate = new MemberCoordinate
                    {
                        MemberId = memberId,
                        Latitude = location.Latitude,
                        Longitude = location.Longitude,
                        Accuracy = location.Accuracy ?? 0,
                        LastUpdateTime = DateTime.UtcNow
                    };
                    
                    // 6. Veritabanına kaydet
                    using var db = new SQLiteConnection(_dbPath);
                    db.CreateTable<MemberCoordinate>();
                    db.Insert(coordinate);
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
    finally
    {
        _isTracking = false;
    }
}
```

#### ⏰ Zamanlama
- **Aralık:** Her 1 dakikada bir
- **Döngü:** `LocationTrackingTask()` metodunda sürekli döngü
- **Çalışma:** Background thread'de

#### 🗄️ Veritabanı İşlemi
```sql
-- Tablo oluşturma (eğer yoksa)
CREATE TABLE IF NOT EXISTS MemberCoordinate (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    MemberId INTEGER,
    Latitude REAL,
    Longitude REAL,
    Accuracy REAL,  -- LocationService'de var
    LastUpdateTime TEXT
)

-- Konum kaydetme
INSERT INTO MemberCoordinate (MemberId, Latitude, Longitude, Accuracy, LastUpdateTime)
VALUES (?, ?, ?, ?, ?)
```

---

## 🔄 Konum Kaydetme Akış Şeması

```
┌─────────────────────────────────────────────────────────┐
│  UYGULAMA AÇILIŞI                                       │
│  HomePage.InitializeAppAsync()                          │
└──────────────────┬──────────────────────────────────────┘
                   │
                   ▼
┌─────────────────────────────────────────────────────────┐
│  LocationAgentService.StartLocationTracking()           │
│  - StartAsync() → ExecuteAsync()                       │
└──────────────────┬──────────────────────────────────────┘
                   │
                   ▼
┌─────────────────────────────────────────────────────────┐
│  ExecuteAsync() - Background Döngü                     │
│  ┌──────────────────────────────────────────────────┐  │
│  │  while (!cancelled) {                             │  │
│  │    1. UpdateLocationAsync() çağrılır              │  │
│  │    2. 5 dakika bekle                              │  │
│  │  }                                                 │  │
│  └──────────────────────────────────────────────────┘  │
└──────────────────┬──────────────────────────────────────┘
                   │
                   ▼
┌─────────────────────────────────────────────────────────┐
│  UpdateLocationAsync() - Konum Alma ve Kaydetme        │
│  ┌──────────────────────────────────────────────────┐  │
│  │  1. İzin kontrolü                                │  │
│  │  2. Geolocation.GetLocationAsync()               │  │
│  │  3. GlobalVariables.MemberLastCoordinate = ...   │  │
│  │  4. MemberCoordinate entity oluştur              │  │
│  │  5. db.Insert(coordinate)                         │  │
│  └──────────────────────────────────────────────────┘  │
└─────────────────────────────────────────────────────────┘
```

---

## 📊 Konum Kaydetme Detayları

### 1. **Entity Oluşturma**

**LocationAgentService:**
```csharp
var coordinate = new MemberCoordinate
{
    MemberId = memberId,                    // int (GlobalVariables.CurrentMember.Id'den)
    Latitude = location.Latitude,            // double
    Longitude = location.Longitude,          // double
    LastUpdateTime = DateTime.UtcNow        // DateTime (UTC)
    // Accuracy field'ı YOK
};
```

**LocationService:**
```csharp
var coordinate = new MemberCoordinate
{
    MemberId = memberId,                    // int (GlobalVariables.CurrentMemberId'den)
    Latitude = location.Latitude,            // double
    Longitude = location.Longitude,          // double
    Accuracy = location.Accuracy ?? 0,       // double (LocationService'de VAR)
    LastUpdateTime = DateTime.UtcNow        // DateTime (UTC)
};
```

### 2. **Veritabanına Kaydetme**

**Her İki Servis:**
```csharp
using var connection = new SQLiteConnection(GlobalVariables.DatabasePath);
connection.CreateTable<MemberCoordinate>();  // Tablo yoksa oluştur
connection.Insert(coordinate);                 // INSERT INTO MemberCoordinate
```

**SQL Eşdeğeri:**
```sql
INSERT INTO MemberCoordinate (MemberId, Latitude, Longitude, LastUpdateTime)
VALUES (?, ?, ?, ?)
```

### 3. **GlobalVariables Güncelleme**

**Her İki Servis:**
```csharp
GlobalVariables.MemberLastCoordinate = new Location
{
    Latitude = location.Latitude,
    Longitude = location.Longitude
};
```

---

## 🔍 Konum Kaydetme Özellikleri

| Özellik | LocationAgentService | LocationService |
|---------|---------------------|-----------------|
| **Aralık** | 5 dakika | 1 dakika |
| **Timeout** | 30 saniye | 10 saniye |
| **Accuracy** | Medium | Medium |
| **Accuracy Field** | ❌ Yok | ✅ Var |
| **MemberId Kaynağı** | `CurrentMember.Id` | `CurrentMemberId` |
| **Tablo** | `MemberCoordinate` | `MemberCoordinate` |
| **Entity** | `MemberCoordinate` (Services) | `MemberCoordinate` (Services) |
| **Başlatma** | Otomatik (HomePage) | Manuel |
| **Durum** | ⭐ Aktif | ⚠️ Opsiyonel |

---

## ⚠️ Önemli Notlar

### 1. **İki Servis Aynı Tabloya Yazıyor**
- Her iki servis de `MemberCoordinate` tablosuna yazıyor
- Aynı konum iki kez kaydedilebilir (farklı zamanlarda)

### 2. **MemberId Belirleme Farkı**
- **LocationAgentService:** `GlobalVariables.CurrentMember.Id` kullanıyor
- **LocationService:** `GlobalVariables.CurrentMemberId` kullanıyor
- Her ikisi de string'den int'e dönüştürmeye çalışıyor
- Başarısız olursa varsayılan değer `1` kullanılıyor

### 3. **Accuracy Field Farkı**
- **LocationAgentService:** Accuracy field'ı yok
- **LocationService:** Accuracy field'ı var
- Bu, veritabanı şemasında tutarsızlığa neden olabilir

### 4. **GlobalVariables Güncelleme**
- Her iki servis de `GlobalVariables.MemberLastCoordinate` güncelliyor
- Son güncelleyen servis geçerli olur

---

## 🎯 Sonuç

**Konum kaydetme işlemi:**
1. **LocationAgentService** tarafından **her 5 dakikada bir** otomatik olarak yapılıyor
2. **LocationService** tarafından **her 1 dakikada bir** (opsiyonel) yapılıyor
3. Her iki servis de:
   - `Geolocation.GetLocationAsync()` ile konum alıyor
   - `GlobalVariables.MemberLastCoordinate` güncelliyor
   - `MemberCoordinate` entity oluşturuyor
   - `MemberCoordinate` tablosuna `INSERT` yapıyor

**Ana servis:** `LocationAgentService` (5 dakikada bir, otomatik başlatılıyor)

