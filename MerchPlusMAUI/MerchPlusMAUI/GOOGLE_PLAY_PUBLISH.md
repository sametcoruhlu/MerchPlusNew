# Google Play Store için Paket Hazırlama Rehberi

## 1. Keystore Oluşturma

Google Play Store'a yüklemek için uygulamanızı imzalamanız gerekiyor. İlk kez bir keystore oluşturmanız gerekiyor:

### Windows PowerShell'de:

```powershell
keytool -genkey -v -keystore merchplus-release.keystore -alias merchplus -keyalg RSA -keysize 2048 -validity 10000
```

Bu komut sizden şunları isteyecek:
- **Keystore password**: Güçlü bir şifre seçin ve GÜVENLİ BİR YERDE SAKLAYIN!
- **Key password**: Genellikle keystore password ile aynı olabilir
- **Adınız, organizasyonunuz, şehir, ülke** vb. bilgiler

**ÖNEMLİ:** Keystore dosyasını ve şifrelerini GÜVENLİ BİR YERDE SAKLAYIN! Kaybederseniz uygulamanızı güncelleyemezsiniz!

### Keystore'u Projeye Ekleme

1. Keystore dosyasını `MerchPlusMAUI\Platforms\Android\` klasörüne kopyalayın
2. `.csproj` dosyasındaki signing ayarlarını düzenleyin (aşağıdaki adımlara bakın)

## 2. .csproj Dosyasında Signing Ayarları

`MerchPlusMAUI.csproj` dosyasında aşağıdaki satırların yorumlarını kaldırın ve kendi bilgilerinizi girin:

```xml
<AndroidSigningKeyStore>merchplus-release.keystore</AndroidSigningKeyStore>
<AndroidSigningStorePass>YOUR_STORE_PASSWORD</AndroidSigningStorePass>
<AndroidSigningKeyAlias>merchplus</AndroidSigningKeyAlias>
<AndroidSigningKeyPass>YOUR_KEY_PASSWORD</AndroidSigningKeyPass>
```

**GÜVENLİK UYARISI:** Şifreleri doğrudan .csproj dosyasına yazmak güvenli değildir. Alternatif olarak:

### Güvenli Yöntem: MSBuild Properties Dosyası

1. `MerchPlusMAUI\Platforms\Android\merchplus-release.properties` dosyası oluşturun:

```properties
AndroidSigningKeyStore=merchplus-release.keystore
AndroidSigningStorePass=YOUR_STORE_PASSWORD
AndroidSigningKeyAlias=merchplus
AndroidSigningKeyPass=YOUR_KEY_PASSWORD
```

2. Bu dosyayı `.gitignore`'a ekleyin (şifrelerinizi git'e yüklemeyin!)

3. `.csproj` dosyasında properties dosyasını import edin:

```xml
<Import Project="Platforms\Android\merchplus-release.properties" Condition="Exists('Platforms\Android\merchplus-release.properties')" />
```

## 3. Version Bilgilerini Güncelleme

Her yeni sürüm için version bilgilerini güncelleyin:

`MerchPlusMAUI.csproj` dosyasında:

```xml
<ApplicationDisplayVersion>1.0.0</ApplicationDisplayVersion>  <!-- Kullanıcıya gösterilen versiyon -->
<ApplicationVersion>1</ApplicationVersion>                    <!-- Google Play version code (her yüklemede artırılmalı) -->
```

**Önemli:** Her Google Play'e yüklemede `ApplicationVersion` (version code) artırılmalıdır (1, 2, 3, ...).

## 4. AAB (Android App Bundle) Oluşturma

### Visual Studio ile:

1. **Configuration**'ı **Release** yapın
2. Projeye sağ tıklayın → **Publish** → **Create Publish Profile**
3. **Android** seçin → **Next**
4. **Release** seçin → **Finish**
5. **Publish** butonuna tıklayın

### Komut Satırı ile:

```powershell
cd D:\GIT\MerchPlusNew\MerchPlusMAUI\MerchPlusMAUI
dotnet publish -f net8.0-android -c Release
```

### AAB Dosyasının Konumu:

```
MerchPlusMAUI\bin\Release\net8.0-android\publish\com.companyname.merchplusmaui-Signed.aab
```

## 5. Google Play Console'a Yükleme

1. [Google Play Console](https://play.google.com/console) hesabınıza giriş yapın
2. Uygulamanızı seçin (veya yeni uygulama oluşturun)
3. **Production** (veya **Internal testing**, **Closed testing**, **Open testing**) → **Create new release**
4. **AAB** dosyasını yükleyin
5. **Release notes** (sürüm notları) ekleyin
6. **Review release** → **Start rollout to Production**

## 6. Önemli Kontroller

### AndroidManifest.xml Kontrolleri:

- ✅ `android:versionCode` ve `android:versionName` doğru mu?
- ✅ Gerekli izinler tanımlı mı?
- ✅ `android:icon` ve `android:roundIcon` ayarlı mı?

### Test Edilmesi Gerekenler:

- ✅ Uygulama Release build'de çalışıyor mu?
- ✅ Tüm özellikler test edildi mi?
- ✅ Crash'ler var mı kontrol edildi mi?
- ✅ Performans testleri yapıldı mı?

## 7. ProGuard/R8 Kuralları (Opsiyonel)

Eğer bazı sınıflar ProGuard/R8 tarafından siliniyorsa, `Platforms\Android\proguard.cfg` dosyası oluşturun:

```
-keep class entMerchPlus.** { *; }
-keep class MerchPlusMAUI.** { *; }
-keep class SQLite.** { *; }
```

## 8. Sorun Giderme

### "Keystore file not found" hatası:
- Keystore dosyasının `Platforms\Android\` klasöründe olduğundan emin olun
- Dosya yolunun doğru olduğunu kontrol edin

### "Wrong password" hatası:
- Keystore şifresini kontrol edin
- Büyük/küçük harf duyarlılığına dikkat edin

### "AAB dosyası çok büyük" uyarısı:
- ProGuard/R8 ile kod küçültme aktif mi kontrol edin
- Gereksiz dosyaları kaldırın
- Resim dosyalarını optimize edin

## 9. Güvenlik Notları

⚠️ **ASLA:**
- Keystore dosyasını git'e yüklemeyin
- Şifreleri kod içine yazmayın
- Keystore'u paylaşmayın
- Keystore şifresini unutmayın (yedeğini alın!)

✅ **MUTLAKA:**
- Keystore'u güvenli bir yerde saklayın
- Şifreleri güvenli bir password manager'da saklayın
- Keystore'un yedeğini alın
- Her yüklemede version code'u artırın

