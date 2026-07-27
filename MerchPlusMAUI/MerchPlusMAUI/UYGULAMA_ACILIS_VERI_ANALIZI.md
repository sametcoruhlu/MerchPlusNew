# Uygulama Açılışında Veri Çekme Analizi

## 📋 Genel Bakış

Bu doküman, MerchPlusMAUI uygulamasının açılışında hangi tablolardan hangi verilerin çekildiğini detaylı olarak açıklamaktadır.

---

## 🚀 Uygulama Başlangıç Akışı

### 1. **MauiProgram.cs** - Uygulama Başlatma
- **Konum:** `MerchPlusMAUI/MauiProgram.cs`
- **İşlev:** Servislerin kayıt edilmesi ve bağımlılık enjeksiyonu
- **Veri Çekme:** ❌ Bu aşamada veri çekilmez

### 2. **AppShell.xaml.cs** - Shell Başlatma
- **Konum:** `MerchPlusMAUI/AppShell.xaml.cs`
- **İşlev:** Route'ların kaydedilmesi
- **Veri Çekme:** ❌ Bu aşamada veri çekilmez

### 3. **HomePage.xaml.cs** - Ana Sayfa Başlatma
- **Konum:** `MerchPlusMAUI/Views/HomePage.xaml.cs`
- **Metod:** `OnAppearing()` → `InitializeAppAsync()`
- **Veri Çekme:** ✅ **ANA VERİ ÇEKME NOKTASI**

---

## 📊 HomePage - InitializeAppAsync() Metodu

### Aşama 1: Kullanıcı Kimlik Doğrulama (10%)
**Metod:** `GetUser()`
- **API Endpoint:** `MemberController/SelectMemberByUsernamePassword`
- **Veri:** Kullanıcı bilgileri (entMember)
- **Veritabanı İşlemi:** ❌ Sadece API çağrısı, veritabanına yazılmaz
- **GlobalVariables:** `CurrentMember` set edilir

### Aşama 2: Veritabanı Kontrolü (25%)
**Metod:** `CheckDatabase()`
- **Veritabanı İşlemi:** 
  - Veritabanı dosyasının varlığı kontrol edilir
  - Tabloların varlığı kontrol edilir
- **Veri Çekme:** ❌ Sadece kontrol

### Aşama 3: Tablo Oluşturma (40%)
**Metod:** `CreateTablesForFirstUse()`
- **Veritabanı İşlemi:** 
  - Tüm tablolar oluşturulur (eğer yoksa)
  - Tablolar: `entRetail`, `entRetailShop`, `entMemberRoute`, `entMemberRouteDetail`, `entCustomerProduct`, vb.
- **Veri Çekme:** ❌ Sadece tablo oluşturma

### Aşama 4: Veri Senkronizasyonu (60%) ⭐ **ÖNEMLİ**
**Metod:** `GetDataForToday()`
- **Konum:** `HomePage.xaml.cs:1541`
- **İşlev:** Sunucudan veri çekme ve veritabanına yazma

#### 4.1. Güncellenecek Veri Tiplerinin Belirlenmesi
**Metod:** `GetDataTypesToUpdate()`
- **Konum:** `HomePage.xaml.cs:1655`
- **Veritabanı Sorguları:**
  ```sql
  -- Her veri tipi için kontrol:
  SELECT COUNT(*) FROM entRetail
  SELECT COUNT(*) FROM entRetailShop
  SELECT COUNT(*) FROM entMemberRoute
  SELECT COUNT(*) FROM entMemberRouteDetail
  SELECT COUNT(*) FROM entCustomerBrandCategory
  SELECT COUNT(*) FROM entCustomerProduct
  SELECT COUNT(*) FROM entSalesOrderPackageType
  SELECT COUNT(*) FROM entPromotionType
  
  -- Configuration tablosundan son güncelleme tarihleri:
  SELECT * FROM entConfiguration WHERE Name IN (
    'RetailDataDateTime',
    'RetailShopDataDateTime',
    'MemberRouteDataDateTime',
    'MemberRouteDetailDataDateTime',
    'CustomerBrandCategoryDataDateTime',
    'CustomerProductDataDateTime',
    'SalesOrderPackageTypeDataDateTime',
    'PromotionTypeDataDateTime'
  )
  ```

#### 4.2. Sunucudan Veri Çekme (Paralel)
Aşağıdaki veriler **paralel olarak** API'den çekilir:

##### 4.2.1. Retail (Perakende) Verileri
- **Metod:** `FetchRetailDataAsync()`
- **API Endpoint:** `RetailController/SelectRetail`
- **Veri Tipi:** `List<entRetail>`
- **Veritabanı Tablosu:** `entRetail`
- **İşlem:**
  ```sql
  DELETE FROM entRetail
  INSERT INTO entRetail (...) VALUES (...)
  UPDATE entConfiguration SET Value=? WHERE Name='RetailDataDateTime'
  ```
- **GlobalVariables:** `GlobalVariables.listRetail` güncellenir
- **Resim İndirme:** Retail profil resimleri indirilir → `entRetailProfilePicture` tablosuna yazılır

##### 4.2.2. RetailShop (Mağaza) Verileri
- **Metod:** `FetchRetailShopDataAsync()`
- **API Endpoint:** `RetailShopController/SelectRetailShopByMemberIdMemberRouteDate`
- **Request Parametreleri:**
  - `EffectiveDate`: Bugünün tarihi
  - `MemberId`: `GlobalVariables.CurrentMember.Id`
- **Veri Tipi:** `List<entRetailShop>`
- **Veritabanı Tablosu:** `entRetailShop`
- **İşlem:**
  ```sql
  DELETE FROM entRetailShop
  INSERT INTO entRetailShop (...) VALUES (...)
  UPDATE entConfiguration SET Value=? WHERE Name='RetailShopDataDateTime'
  ```
- **GlobalVariables:** `GlobalVariables.listRetailShop` güncellenir

##### 4.2.3. MemberRoute (Üye Rota) Verileri
- **Metod:** `FetchMemberRouteDataAsync()`
- **API Endpoint:** `MemberRouteController/SelectMemberRouteByMemberIdEffectiveDate`
- **Request Parametreleri:**
  - `EffectiveDate`: Bugünün tarihi
  - `MemberId`: `GlobalVariables.CurrentMember.Id`
- **Veri Tipi:** `List<entMemberRoute>`
- **Veritabanı Tablosu:** `entMemberRoute`
- **İşlem:**
  ```sql
  DELETE FROM entMemberRoute
  INSERT INTO entMemberRoute (...) VALUES (...)
  UPDATE entConfiguration SET Value=? WHERE Name='MemberRouteDataDateTime'
  ```
- **GlobalVariables:** `GlobalVariables.listMemberRoute` güncellenir

##### 4.2.4. MemberRouteDetail (Rota Detay) Verileri
- **Metod:** `FetchMemberRouteDetailDataAsync()`
- **API Endpoint:** `MemberRouteDetailController/SelectMemberRouteDetailByMemberIdEffectiveDate`
- **Request Parametreleri:**
  - `EffectiveDate`: Bugünün tarihi
  - `MemberId`: `GlobalVariables.CurrentMember.Id`
- **Veri Tipi:** `List<entMemberRouteDetail>`
- **Veritabanı Tablosu:** `entMemberRouteDetail`
- **İşlem:**
  ```sql
  DELETE FROM entMemberRouteDetail
  INSERT INTO entMemberRouteDetail (...) VALUES (...)
  DELETE FROM entMemberRouteDetail WHERE CustomerProductId=208
  UPDATE entConfiguration SET Value=? WHERE Name='MemberRouteDetailDataDateTime'
  ```
- **GlobalVariables:** `GlobalVariables.listMemberRouteDetail` güncellenir

##### 4.2.5. CustomerBrandCategory (Müşteri Marka Kategori) Verileri
- **Metod:** `FetchCustomerBrandCategoryDataAsync()`
- **API Endpoint:** `CustomerBrandCategoryController/SelectCustomerBrandCategory`
- **Veri Tipi:** `List<entCustomerBrandCategory>`
- **Veritabanı Tablosu:** `entCustomerBrandCategory`
- **İşlem:**
  ```sql
  DELETE FROM entCustomerBrandCategory
  INSERT INTO entCustomerBrandCategory (...) VALUES (...)
  UPDATE entConfiguration SET Value=? WHERE Name='CustomerBrandCategoryDataDateTime'
  ```
- **GlobalVariables:** `GlobalVariables.listCustomerBrandCategory` güncellenir

##### 4.2.6. CustomerProduct (Müşteri Ürün) Verileri
- **Metod:** `FetchCustomerProductDataAsync()`
- **API Endpoint:** `CustomerProductController/SelectCustomerProduct`
- **Veri Tipi:** `List<entCustomerProduct>`
- **Veritabanı Tablosu:** `entCustomerProduct`
- **İşlem:**
  ```sql
  DELETE FROM entCustomerProduct
  INSERT INTO entCustomerProduct (...) VALUES (...)
  DELETE FROM entCustomerProduct WHERE Id=208
  UPDATE entConfiguration SET Value=? WHERE Name='CustomerProductDataDateTime'
  ```
- **GlobalVariables:** `GlobalVariables.listCustomerProduct` güncellenir
- **Resim İndirme:** Ürün profil resimleri indirilir → `entCustomerProductPicture` tablosuna yazılır

##### 4.2.7. SalesOrderPackageType (Satış Sipariş Paket Tipi) Verileri
- **Metod:** `FetchSalesOrderPackageTypeDataAsync()`
- **API Endpoint:** `SalesOrderPackageTypeController/SelectSalesOrderPackageType`
- **Veri Tipi:** `List<entSalesOrderPackageType>`
- **Veritabanı Tablosu:** `entSalesOrderPackageType`
- **İşlem:**
  ```sql
  DELETE FROM entSalesOrderPackageType
  INSERT INTO entSalesOrderPackageType (...) VALUES (...)
  UPDATE entConfiguration SET Value=? WHERE Name='SalesOrderPackageTypeDataDateTime'
  ```
- **GlobalVariables:** `GlobalVariables.listSalesOrderPackageType` güncellenir

##### 4.2.8. PromotionType (Promosyon Tipi) Verileri
- **Metod:** `FetchPromotionTypeDataAsync()`
- **API Endpoint:** `PromotionController/SelectPromotionType` (APIPath2 kullanılır)
- **Veri Tipi:** `List<entPromotionType>`
- **Veritabanı Tablosu:** `entPromotionType`
- **İşlem:**
  ```sql
  DELETE FROM entPromotionType
  INSERT INTO entPromotionType (...) VALUES (...)
  UPDATE entConfiguration SET Value=? WHERE Name='PromotionTypeDataDateTime'
  ```
- **GlobalVariables:** `GlobalVariables.listPromotionType` güncellenir

### Aşama 5: Son Konum Bilgisi Yükleme (75%)
**Metod:** `LoadLastKnownLocation()`
- **Konum:** `HomePage.xaml.cs:2148`
- **Veritabanı Sorgusu:**
  ```sql
  SELECT * FROM entMemberCoordinate 
  ORDER BY LastUpdateTime DESC 
  LIMIT 1
  ```
- **Veri Tipi:** `MemberCoordinate` (Models namespace)
- **GlobalVariables:** `GlobalVariables.MemberLastCoordinate` set edilir
- **Not:** Eğer veri yoksa, GlobalVariables sıfır değerlerde kalır

### Aşama 6: İstatistiklerin Hesaplanması (95%)
**Metod:** `CalculateStatistics()`
- **Konum:** `HomePage.xaml.cs:2061`
- **Veritabanı Sorguları:**
  ```sql
  -- Toplam planlanan mağaza sayısı
  SELECT COUNT(*) FROM entMemberRoute
  
  -- Tamamlanan mağaza sayısı (ExitOn IS NOT NULL)
  SELECT COUNT(*) FROM entMemberRoute WHERE ExitOn IS NOT NULL
  
  -- Devam eden mağaza sayısı (EntryOn IS NOT NULL AND ExitOn IS NULL)
  SELECT COUNT(*) FROM entMemberRoute WHERE EntryOn IS NOT NULL AND ExitOn IS NULL
  
  -- Henüz başlanmamış mağaza sayısı (EntryOn IS NULL AND ExitOn IS NULL)
  SELECT COUNT(*) FROM entMemberRoute WHERE EntryOn IS NULL AND ExitOn IS NULL
  ```
- **GlobalVariables:** 
  - `PlannedStoresCount`
  - `CompletedStoresCount`
  - `PendingStoresCount`

---

## 📄 AboutMePage - OnAppearing() Metodu

### 1. İstatistiklerin Hesaplanması
**Metod:** `CalculateAndUpdateStatistics()`
- **Konum:** `AboutMePage.xaml.cs:72`
- **Veritabanı Sorguları:**
  ```sql
  SELECT COUNT(*) FROM entMemberRoute
  SELECT COUNT(*) FROM entMemberRoute WHERE ExitOn IS NOT NULL
  SELECT COUNT(*) FROM entMemberRoute WHERE EntryOn IS NOT NULL AND ExitOn IS NULL
  SELECT COUNT(*) FROM entMemberRoute WHERE EntryOn IS NULL AND ExitOn IS NULL
  ```

### 2. Mağaza Listesinin Yüklenmesi
**Metod:** `LoadStoreList()`
- **Konum:** `AboutMePage.xaml.cs:411`
- **Veritabanı Sorguları:**
  ```sql
  -- Tüm mağaza rotalarını al
  SELECT * FROM entMemberRoute
  
  -- Her mağaza için RetailShop bilgisi (eğer RetailShopId varsa)
  SELECT * FROM entRetailShop WHERE Id=?
  
  -- Her mağaza için tamamlanma yüzdesi hesaplama
  SELECT * FROM entMemberRouteDetail WHERE MemberRouteId=?
  ```

---

## 📋 Özet Tablo

| Aşama | Metod | Tablo | Veri Tipi | Kaynak | İşlem |
|-------|-------|-------|-----------|--------|-------|
| 1 | GetUser() | - | entMember | API | GlobalVariables'a yazılır |
| 2 | CheckDatabase() | - | - | - | Kontrol |
| 3 | CreateTablesForFirstUse() | Tüm tablolar | - | - | Tablo oluşturma |
| 4.1 | GetDataTypesToUpdate() | entConfiguration, entRetail, entRetailShop, entMemberRoute, entMemberRouteDetail, entCustomerBrandCategory, entCustomerProduct, entSalesOrderPackageType, entPromotionType | - | DB | Kontrol |
| 4.2.1 | FetchRetailDataAsync() | entRetail, entRetailProfilePicture, entConfiguration | List<entRetail> | API | DELETE + INSERT + UPDATE |
| 4.2.2 | FetchRetailShopDataAsync() | entRetailShop, entConfiguration | List<entRetailShop> | API | DELETE + INSERT + UPDATE |
| 4.2.3 | FetchMemberRouteDataAsync() | entMemberRoute, entConfiguration | List<entMemberRoute> | API | DELETE + INSERT + UPDATE |
| 4.2.4 | FetchMemberRouteDetailDataAsync() | entMemberRouteDetail, entConfiguration | List<entMemberRouteDetail> | API | DELETE + INSERT + DELETE (Id=208) + UPDATE |
| 4.2.5 | FetchCustomerBrandCategoryDataAsync() | entCustomerBrandCategory, entConfiguration | List<entCustomerBrandCategory> | API | DELETE + INSERT + UPDATE |
| 4.2.6 | FetchCustomerProductDataAsync() | entCustomerProduct, entCustomerProductPicture, entConfiguration | List<entCustomerProduct> | API | DELETE + INSERT + DELETE (Id=208) + UPDATE |
| 4.2.7 | FetchSalesOrderPackageTypeDataAsync() | entSalesOrderPackageType, entConfiguration | List<entSalesOrderPackageType> | API | DELETE + INSERT + UPDATE |
| 4.2.8 | FetchPromotionTypeDataAsync() | entPromotionType, entConfiguration | List<entPromotionType> | API | DELETE + INSERT + UPDATE |
| 5 | LoadLastKnownLocation() | entMemberCoordinate | MemberCoordinate | DB | SELECT (ORDER BY LastUpdateTime DESC LIMIT 1) |
| 6 | CalculateStatistics() | entMemberRoute | - | DB | COUNT(*) sorguları |

---

## 🔍 Önemli Notlar

1. **Paralel İşlem:** Tüm API çağrıları paralel olarak yapılır (`Task.WhenAll`)
2. **Transaction:** Tüm veritabanı yazma işlemleri tek bir transaction içinde yapılır
3. **Resim İndirme:** Retail ve CustomerProduct resimleri paralel batch'ler halinde indirilir (10'ar 10'ar)
4. **Güncelleme Kontrolü:** Her veri tipi için son güncelleme tarihi kontrol edilir, sadece güncel olmayan veriler çekilir
5. **GlobalVariables:** Tüm veriler hem veritabanına yazılır hem de GlobalVariables static listelerine yüklenir
6. **Özel Silme İşlemleri:** 
   - `entMemberRouteDetail` tablosundan `CustomerProductId=208` olan kayıtlar silinir
   - `entCustomerProduct` tablosundan `Id=208` olan kayıt silinir

---

## 📝 Veritabanı Optimizasyonları

Uygulama açılışında aşağıdaki PRAGMA ayarları yapılır:
```sql
PRAGMA journal_mode=WAL
PRAGMA synchronous=NORMAL
PRAGMA cache_size=10000
PRAGMA temp_store=MEMORY
```

---

## 🎯 Sonuç

Uygulama açılışında toplam **8 farklı veri tipi** sunucudan çekilir ve veritabanına yazılır. Ayrıca son konum bilgisi ve istatistikler veritabanından okunur. Tüm işlemler optimize edilmiş şekilde paralel olarak gerçekleştirilir.

