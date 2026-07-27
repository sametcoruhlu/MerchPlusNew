# Release Build Test Kılavuzu

## Debug vs Release Build Farkları

### Debug Build Özellikleri:
- **Fast Deployment (Shared Runtime)**: Aktif - APK boyutu küçük, hızlı deploy
- **EmbedAssembliesIntoApk**: `false` - Assembly'ler APK'ya gömülmez
- **AndroidPackageFormat**: `apk` - APK formatı
- **Optimize**: `true` - Kod optimizasyonu aktif (projede ayarlanmış)
- **Debug Symbols**: Aktif
- **Hot Reload**: Mümkün

### Release Build Özellikleri:
- **Fast Deployment**: Devre dışı - Tüm kod APK'ya gömülür
- **EmbedAssembliesIntoApk**: `true` - Tüm assembly'ler APK'ya gömülür
- **AndroidPackageFormat**: `aab` - AAB formatı (Google Play Store için)
- **Optimize**: `true` - Kod optimizasyonu aktif
- **Debug Symbols**: Devre dışı
- **Hot Reload**: Mümkün değil

### Sorunların Nedenleri:

#### 1. **Layout Rendering Timing Farkları**
- **Debug**: Layout daha yavaş render olur, `OnAppearing` sırasında UI hazır olabilir
- **Release**: Layout daha hızlı render olur, `OnAppearing` çağrıldığında UI henüz hazır olmayabilir
- **Çözüm**: `OnAppearing` içinde `Task.Delay()` eklemek

#### 2. **SafeArea/WindowInsets Hesaplamaları**
- **Debug**: System bars yükseklikleri farklı hesaplanabilir
- **Release**: Optimize edilmiş layout, system bars farklı zamanlanabilir
- **Çözüm**: Layout render edildikten sonra SafeArea hesaplamaları yapmak

#### 3. **MainThread Invocation Timing**
- **Debug**: `MainThread.BeginInvokeOnMainThread` anında çalışabilir
- **Release**: Optimize edilmiş execution, timing farklı olabilir
- **Çözüm**: `await Task.Delay()` ile layout'un hazır olmasını beklemek

## En Doğru Test Yöntemleri

### 1. **Release APK Oluşturup USB ile Kurmak (ÖNERİLEN)**
```powershell
# Release build yap
dotnet build -c Release

# APK'yı oluştur (AAB yerine)
# .csproj dosyasında geçici olarak şunu değiştirin:
# <AndroidPackageFormat Condition="'$(Configuration)' == 'Release' AND $(TargetFramework.Contains('-android'))">apk</AndroidPackageFormat>

# APK'yı telefona kur
adb install -r bin\Release\net8.0-android\com.companyname.merchplusmaui.apk
```

**Avantajlar:**
- En gerçekçi test ortamı
- Gerçek cihazda test
- Publish edilmiş versiyon ile aynı

**Dezavantajlar:**
- Her test için rebuild gerekir
- APK boyutu büyük olabilir

### 2. **Release Build ile Visual Studio'dan Çalıştırmak**
```powershell
# Visual Studio'da Configuration'ı "Release" yap
# Run (F5) ile çalıştır
```

**Avantajlar:**
- Hızlı test
- Debug logları görülebilir

**Dezavantajlar:**
- Fast Deployment aktif olabilir (projede kapatılmış)
- Gerçek publish versiyonundan farklı olabilir

### 3. **AAB Oluşturup Internal Testing'e Yüklemek**
```powershell
# Release build yap (AAB oluşturur)
dotnet build -c Release

# AAB dosyası: bin\Release\net8.0-android\com.companyname.merchplusmaui.aab
# Google Play Console > Internal Testing > New Release > Upload AAB
```

**Avantajlar:**
- Tam gerçekçi test
- Gerçek kullanıcı senaryosu
- Farklı cihazlarda test

**Dezavantajlar:**
- Yavaş süreç (upload, review, distribution)
- Her test için yeni versiyon gerekir

### 4. **Release APK'yı Doğrudan Telefona Kurmak**
```powershell
# Release build yap
dotnet build -c Release

# APK formatına geçici olarak çevir (yukarıdaki adım)
# APK'yı telefona kopyala ve kur
```

**Avantajlar:**
- Hızlı test
- AAB'ye gerek yok

**Dezavantajlar:**
- Her test için rebuild gerekir

## Test Senaryoları

### 1. **SafeArea Testi (ProductMultiSelectionPage)**
- ✅ Modal sayfa açıldığında butonlar görünür olmalı
- ✅ Butonlar SafeArea dışına taşmamalı
- ✅ Butonlar tıklanabilir olmalı

### 2. **Layout Rendering Testi**
- ✅ Sayfalar anında render edilmeli
- ✅ UI elementleri doğru konumda olmalı
- ✅ Animasyonlar düzgün çalışmalı

### 3. **Performance Testi**
- ✅ Sayfa geçişleri akıcı olmalı
- ✅ Resim yükleme performansı iyi olmalı
- ✅ Veritabanı işlemleri hızlı olmalı

## Sorun Giderme

### SafeArea Sorunları
**Sorun**: Release build'de butonlar SafeArea dışına taşıyor
**Çözüm**: `OnAppearing` içinde `await Task.Delay(100)` eklemek

```csharp
protected override async void OnAppearing()
{
    base.OnAppearing();
    
    // Layout render edildikten sonra SafeArea ayarları yap
    await Task.Delay(100); // Release build için gerekli
    MainThread.BeginInvokeOnMainThread(() =>
    {
        // SafeArea ayarları
    });
}
```

### Layout Timing Sorunları
**Sorun**: Release build'de UI elementleri null geliyor
**Çözüm**: `FindByName` öncesi delay eklemek

```csharp
await Task.Delay(150); // Layout'un hazır olması için
var frame = this.FindByName<Frame>("BottomButtonsFrame");
```

### Performance Sorunları
**Sorun**: Release build'de performans düşüyor
**Çözüm**: Optimizasyonları kontrol etmek
- `AndroidEnableProguard`: `false` (projede zaten)
- `AndroidLinkMode`: `None` (projede zaten)
- `RunAOTCompilation`: `false` (projede zaten)

## Best Practices

1. **Her Özelliği Release Build'de Test Et**
   - Debug'da çalışan her şey Release'de çalışmayabilir
   - Release build test'i ihmal etme

2. **Timing İssues için Task.Delay Kullan**
   - Layout render edilene kadar bekle
   - Minimum 100ms delay ekle

3. **Logları Kullan**
   - Release build'de de logları aktif tut
   - `System.Diagnostics.Debug.WriteLine` kullan

4. **Farklı Cihazlarda Test Et**
   - Farklı ekran boyutları
   - Farklı Android versiyonları
   - Farklı üreticiler (Samsung, Xiaomi, vb.)

5. **Performance Profiling Yap**
   - Release build'de profil oluştur
   - Memory leak'leri kontrol et

## Önerilen Test Süreci

1. **Geliştirme**: Debug build ile geliştir
2. **İlk Test**: Release build ile Visual Studio'dan test et
3. **APK Test**: Release APK oluşturup USB ile kur
4. **Internal Testing**: AAB oluşturup Google Play'e yükle
5. **Production**: Public release yap

## Komutlar

### Release APK Oluşturma (Geçici)
```xml
<!-- MerchPlusMAUI.csproj içinde -->
<AndroidPackageFormat Condition="'$(Configuration)' == 'Release' AND $(TargetFramework.Contains('-android'))">apk</AndroidPackageFormat>
```

### Release Build
```powershell
dotnet build -c Release -f net8.0-android
```

### APK Kurma
```powershell
adb install -r bin\Release\net8.0-android\com.companyname.merchplusmaui.apk
```

### AAB Oluşturma (Varsayılan)
```powershell
# .csproj'da zaten AAB formatı ayarlı
dotnet build -c Release -f net8.0-android
# AAB: bin\Release\net8.0-android\com.companyname.merchplusmaui.aab
```

## Sonuç

**En Doğru Test Yöntemi**: Release APK oluşturup USB ile telefona kurmak
- Gerçekçi test ortamı
- Hızlı test döngüsü
- Publish versiyonu ile aynı

**İkinci En İyi**: Release build ile Visual Studio'dan çalıştırmak
- Hızlı iterasyon
- Debug logları mümkün
- Biraz farklı olabilir
