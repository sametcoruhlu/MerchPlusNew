# Konum İşlemleri Detaylı Analizi

## 📋 Genel Bakış

Bu doküman, MerchPlusMAUI uygulamasında konum kontrolü, konum kaydetme ve konum senkronizasyonu ile ilgili tüm işlemleri detaylı olarak açıklamaktadır.

---

## 🗂️ Konumla İlgili Entity'ler ve Tablolar

### 1. **MemberCoordinate** (Services Namespace)
- **Konum:** `MerchPlusMAUI/Services/MemberCoordinate.cs`
- **Tablo Adı:** `MemberCoordinate` (SQLite-net otomatik oluşturur)
- **Özellikler:**
  - `Id` (PrimaryKey, AutoIncrement)
  - `MemberId` (int)
  - `Latitude` (double)
  - `Longitude` (double)
  - `Accuracy` (double)
  - `LastUpdateTime` (DateTime)

### 2. **entMemberCoordinate** (Models Namespace)
- **Konum:** `MerchPlusMAUI/Models/entMemberCoordinate.cs`
- **Tablo Adı:** `entMemberCoordinate`
- **Özellikler:**
  - `Id` (PrimaryKey, AutoIncrement)
  - `MemberId` (string)
  - `Latitude` (double)
  - `Longitude` (double)
  - `LastUpdateTime` (DateTime)

### 3. **GlobalVariables.MemberLastCoordinate**
- **Tip:** `Location` (custom class)
- **Özellikler:**
  - `Latitude` (double)
  - `Longitude` (double)
- **Kullanım:** Son bilinen konum bilgisini tutar

---

## 🔄 Konum İzleme Servisleri

### 1. **LocationService** (1 Dakika Aralıkla)

#### 1.1. Servis Tanımı
- **Konum:** `MerchPlusMAUI/Services/LocationService.cs`
- **Tip:** Normal Service (IHostedService değil)
- **Başlatma:** Manuel olarak `StartLocationTracking()` çağrılır

#### 1.2. Konum İzleme Başlatma
**Metod:** `StartLocationTracking()`
- **Konum:** `LocationService.cs:30`
- **İşlemler:**
  1. Konum iznini kontrol eder (`Permissions.LocationWhenInUse`)
  2. İzin yoksa ister
  3. İzin reddedilirse işlemi durdurur
  4. `LocationTrackingTask()` metodunu background thread'de başlatır

#### 1.3. Konum İzleme Döngüsü
**Metod:** `LocationTrackingTask()`
- **Konum:** `LocationService.cs:85`
- **Aralık:** **1 dakika** (`TimeSpan.FromMinutes(1)`)
- **İşlem Akışı:**
  1. Konum iznini kontrol eder
  2. `Geolocation.GetLocationAsync()` çağrılır
     - **DesiredAccuracy:** `GeolocationAccuracy.Medium`
     - **Timeout:** 10 saniye
  3. Konum alınırsa:
     - `GlobalVariables.MemberLastCoordinate` güncellenir
     - `MemberCoordinate` entity oluşturulur
     - Veritabanına kaydedilir (`INSERT INTO MemberCoordinate`)
  4. 1 dakika bekler ve tekrar eder

#### 1.4. Veritabanı İşlemleri
```sql
-- Tablo oluşturma (eğer yoksa)
CREATE TABLE IF NOT EXISTS MemberCoordinate (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    MemberId INTEGER,
    Latitude REAL,
    Longitude REAL,
    Accuracy REAL,
    LastUpdateTime TEXT
)

-- Konum kaydetme
INSERT INTO MemberCoordinate (MemberId, Latitude, Longitude, Accuracy, LastUpdateTime)
VALUES (?, ?, ?, ?, ?)
```

#### 1.5. MemberId Belirleme
- `GlobalVariables.CurrentMemberId` string'den int'e dönüştürülür
- Dönüştürülemezse varsayılan değer `1` kullanılır

---

### 2. **LocationAgentService** (5 Dakika Aralıkla) ⭐ **ANA SERVİS**

#### 2.1. Servis Tanımı
- **Konum:** `MerchPlusMAUI/Services/LocationAgentService.cs`
- **Tip:** `IHostedService` (Background Service)
- **Başlatma:** Uygulama açılışında otomatik başlatılır

#### 2.2. Servis Başlatma
**Metod:** `StartAsync()` → `ExecuteAsync()`
- **Konum:** `LocationAgentService.cs:59`
- **Başlatma Yeri:** `HomePage.xaml.cs:242` (InitializeAppAsync içinde)
- **Kontrol:** `GlobalVariables.MemberLocationAgentStarted` flag'i ile çift başlatma önlenir

#### 2.3. Konum İzleme Döngüsü
**Metod:** `ExecuteAsync()` → `UpdateLocationAsync()`
- **Konum:** `LocationAgentService.cs:75`
- **Aralık:** **5 dakika** (`TimeSpan.FromMinutes(5)`)
- **İşlem Akışı:**
  1. Konum iznini kontrol eder
  2. `IGeolocation.GetLocationAsync()` çağrılır
     - **DesiredAccuracy:** `GeolocationAccuracy.Medium`
     - **Timeout:** 30 saniye
  3. Konum alınırsa:
     - `GlobalVariables.MemberLastCoordinate` güncellenir
     - `MemberCoordinate` entity oluşturulur
     - Veritabanına kaydedilir (`INSERT INTO MemberCoordinate`)

#### 2.4. Veritabanı İşlemleri
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

**Not:** LocationAgentService'de `Accuracy` field'ı yok, sadece LocationService'de var.

#### 2.5. MemberId Belirleme
- `GlobalVariables.CurrentMember.Id` string'den int'e dönüştürülür
- Dönüştürülemezse varsayılan değer `1` kullanılır

---

## 📍 Konum Yükleme İşlemleri

### 1. **HomePage - Son Konum Yükleme**

#### 1.1. Metod: `LoadLastKnownLocation()`
- **Konum:** `HomePage.xaml.cs:2148`
- **Çağrılma:** `HomePage.xaml.cs:229` (InitializeAppAsync içinde, konum servisi başlatılmadan önce)

#### 1.2. Veritabanı Sorgusu
```sql
SELECT * FROM entMemberCoordinate 
ORDER BY LastUpdateTime DESC 
LIMIT 1
```

#### 1.3. İşlem Akışı
1. `entMemberCoordinate` tablosundan son kayıt okunur
2. `GlobalVariables.MemberLastCoordinate` güncellenir
3. Eğer veri yoksa, GlobalVariables sıfır değerlerde kalır

#### 1.4. Entity Kullanımı
- **Entity:** `MemberCoordinate` (Models namespace)
- **Tablo:** `entMemberCoordinate`

---

## 🔄 Konum Senkronizasyonu (Sunucuya Gönderme)

### 1. **CommonFooter - SyncCoordinates()**

#### 1.1. Metod: `SyncCoordinates()`
- **Konum:** `CommonFooter.xaml.cs:359`
- **Çağrılma:** `CommonFooter.xaml.cs:262` (SyncData içinde)

#### 1.2. Veritabanı Sorgusu
```sql
SELECT * FROM entMemberCoordinate
```

#### 1.3. İşlem Akışı
1. `entMemberCoordinate` tablosundan tüm kayıtlar okunur
2. JSON'a serialize edilir (`System.Text.Json`)
3. API'ye POST isteği gönderilir:
   - **Endpoint:** `MemberCoordinateController/InsertMemberCoordinateFromAndroidArray`
   - **BaseAddress:** `GlobalVariables.APIPath`
4. Başarılı olursa (`response.IsSuccessStatusCode`):
   - `entMemberCoordinate` tablosundaki **TÜM** kayıtlar silinir
   - `db.DeleteAll<entMemberCoordinate>()`

#### 1.4. Entity Kullanımı
- **Entity:** `entMemberCoordinate` (Models namespace)
- **Tablo:** `entMemberCoordinate`

---

### 2. **AboutMePage - SyncCoordinates()**

#### 2.1. Metod: `SyncCoordinates()`
- **Konum:** `AboutMePage.xaml.cs:220`
- **Çağrılma:** `AboutMePage.xaml.cs:187` (SyncData içinde)

#### 2.2. İşlem Akışı
- CommonFooter ile **aynı** işlem akışı
- Aynı endpoint ve aynı silme işlemi

---

### 3. **DatabaseSyncService - SyncCoordinates()**

#### 3.1. Metod: `SyncCoordinates()`
- **Konum:** `DatabaseSyncService.cs:148`
- **Çağrılma:** Background service tarafından periyodik olarak

#### 3.2. Veritabanı Sorgusu
```sql
SELECT * FROM entMemberCoordinate
```

#### 3.3. İşlem Akışı
1. `MemberCoordinate` (Services namespace) entity'si kullanılır
2. API'ye POST isteği gönderilir
3. Başarılı olursa (`Result != "NOK"`):
   - `MemberCoordinate` tablosundaki **TÜM** kayıtlar silinir
   - `db.DeleteAll<MemberCoordinate>()`

#### 3.4. Entity Kullanımı
- **Entity:** `MemberCoordinate` (Services namespace)
- **Tablo:** `MemberCoordinate` (SQLite-net otomatik oluşturur)

**⚠️ ÖNEMLİ:** DatabaseSyncService farklı entity kullanıyor (`MemberCoordinate` vs `entMemberCoordinate`), bu potansiyel bir sorun olabilir.

---

## 📊 Konum İşlemleri Akış Şeması

```
┌─────────────────────────────────────────────────────────────┐
│                    UYGULAMA AÇILIŞI                        │
└──────────────────────┬────────────────────────────────────┘
                       │
                       ▼
┌─────────────────────────────────────────────────────────────┐
│  HomePage.InitializeAppAsync()                              │
│  - LoadLastKnownLocation()                                  │
│    • SELECT * FROM entMemberCoordinate ORDER BY ... LIMIT 1 │
│    • GlobalVariables.MemberLastCoordinate güncellenir       │
└──────────────────────┬────────────────────────────────────┘
                       │
                       ▼
┌─────────────────────────────────────────────────────────────┐
│  LocationAgentService.StartLocationTracking()               │
│  - ExecuteAsync() → UpdateLocationAsync()                   │
│    • Her 5 dakikada bir:                                    │
│      1. Geolocation.GetLocationAsync()                      │
│      2. GlobalVariables.MemberLastCoordinate güncellenir    │
│      3. INSERT INTO MemberCoordinate                        │
└──────────────────────┬────────────────────────────────────┘
                       │
                       ▼
┌─────────────────────────────────────────────────────────────┐
│  LocationService.StartLocationTracking() (Opsiyonel)        │
│  - LocationTrackingTask()                                   │
│    • Her 1 dakikada bir:                                    │
│      1. Geolocation.GetLocationAsync()                       │
│      2. GlobalVariables.MemberLastCoordinate güncellenir    │
│      3. INSERT INTO MemberCoordinate                         │
└──────────────────────┬────────────────────────────────────┘
                       │
                       ▼
┌─────────────────────────────────────────────────────────────┐
│  CommonFooter.SyncData() veya AboutMePage.SyncData()         │
│  - SyncCoordinates()                                         │
│    1. SELECT * FROM entMemberCoordinate                      │
│    2. POST → MemberCoordinateController/...                 │
│    3. Başarılı ise: DELETE FROM entMemberCoordinate         │
└─────────────────────────────────────────────────────────────┘
```

---

## 🔍 Detaylı İşlem Analizi

### 1. Konum Alma İşlemleri

| Servis | Aralık | Timeout | Accuracy | Entity | Tablo |
|--------|--------|---------|----------|--------|-------|
| LocationService | 1 dakika | 10 saniye | Medium | MemberCoordinate | MemberCoordinate |
| LocationAgentService | 5 dakika | 30 saniye | Medium | MemberCoordinate | MemberCoordinate |

### 2. Konum Kaydetme İşlemleri

| Servis | Latitude | Longitude | Accuracy | MemberId | LastUpdateTime |
|--------|----------|-----------|----------|----------|----------------|
| LocationService | ✅ | ✅ | ✅ | ✅ (int) | ✅ (UTC) |
| LocationAgentService | ✅ | ✅ | ❌ | ✅ (int) | ✅ (UTC) |

### 3. Konum Yükleme İşlemleri

| Metod | Tablo | Sorgu | Sonuç |
|-------|-------|-------|-------|
| LoadLastKnownLocation() | entMemberCoordinate | ORDER BY LastUpdateTime DESC LIMIT 1 | GlobalVariables güncellenir |

### 4. Konum Senkronizasyonu İşlemleri

| Metod | Tablo | Entity | Endpoint | Başarılı İşlem |
|-------|-------|--------|----------|----------------|
| CommonFooter.SyncCoordinates() | entMemberCoordinate | entMemberCoordinate | InsertMemberCoordinateFromAndroidArray | DELETE ALL |
| AboutMePage.SyncCoordinates() | entMemberCoordinate | entMemberCoordinate | InsertMemberCoordinateFromAndroidArray | DELETE ALL |
| DatabaseSyncService.SyncCoordinates() | MemberCoordinate | MemberCoordinate | InsertMemberCoordinateFromAndroidArray | DELETE ALL |

---

## ⚠️ Önemli Notlar ve Potansiyel Sorunlar

### 1. **İki Farklı Entity Kullanımı**
- **LocationService & LocationAgentService:** `MemberCoordinate` (Services namespace) kullanıyor
- **CommonFooter & AboutMePage:** `entMemberCoordinate` (Models namespace) kullanıyor
- **DatabaseSyncService:** `MemberCoordinate` (Services namespace) kullanıyor

**Sorun:** İki farklı tablo kullanılıyor olabilir:
- `MemberCoordinate` tablosu (LocationService/LocationAgentService tarafından yazılıyor)
- `entMemberCoordinate` tablosu (CommonFooter/AboutMePage tarafından okunuyor)

**Çözüm Önerisi:** Tüm servisler aynı entity'yi kullanmalı.

### 2. **İki Farklı Servis Aynı Anda Çalışıyor**
- **LocationService:** 1 dakikada bir konum alıyor
- **LocationAgentService:** 5 dakikada bir konum alıyor

**Sorun:** İki servis aynı anda çalışırsa, aynı konum iki kez kaydedilebilir.

**Çözüm Önerisi:** Sadece bir servis aktif olmalı (LocationAgentService önerilir).

### 3. **GlobalVariables Güncelleme**
- Her iki servis de `GlobalVariables.MemberLastCoordinate` güncelliyor
- Son güncelleyen servis geçerli olur

### 4. **MemberId Dönüştürme**
- LocationService: `GlobalVariables.CurrentMemberId` (string) kullanıyor
- LocationAgentService: `GlobalVariables.CurrentMember.Id` (string) kullanıyor
- Her ikisi de int'e dönüştürmeye çalışıyor, başarısız olursa `1` kullanıyor

### 5. **Senkronizasyon Sonrası Silme**
- Başarılı senkronizasyon sonrası **TÜM** koordinatlar siliniyor
- Bu, veri kaybı riski oluşturabilir (eğer sunucu kaydı başarısız olursa)

---

## 📝 Öneriler

1. **Tek Entity Kullanımı:** Tüm servisler `entMemberCoordinate` (Models namespace) kullanmalı
2. **Tek Servis:** Sadece `LocationAgentService` aktif olmalı, `LocationService` kaldırılmalı veya devre dışı bırakılmalı
3. **Senkronizasyon İyileştirmesi:** Başarılı gönderilen kayıtların ID'leri saklanmalı ve sadece onlar silinmeli
4. **Hata Yönetimi:** Senkronizasyon başarısız olursa, koordinatlar silinmemeli

---

## 🎯 Sonuç

Uygulamada konum işlemleri için **iki farklı servis** ve **iki farklı entity** kullanılıyor. Bu durum veri tutarsızlığına neden olabilir. Önerilen çözüm: Tüm konum işlemleri için tek bir servis (`LocationAgentService`) ve tek bir entity (`entMemberCoordinate`) kullanılmalıdır.

