# Keystore SHA1 kontrolü
# Bu script, keystore dosyasının SHA1 parmak izini gösterir

$keystorePath = "merchplus-release.keystore"
$alias = "merchplus"
$password = "!!19670261Ts61!!"

# Java SDK yolu (kendi sisteminize göre güncelleyin)
$javaHome = "C:\Program Files (x86)\Android\OpenJDK\jdk-17.0.12"
$keytoolPath = Join-Path $javaHome "bin\keytool.exe"

if (-not (Test-Path $keytoolPath)) {
    Write-Host "keytool bulunamadı. Java SDK yolunu kontrol edin: $javaHome"
    Write-Host "Alternatif olarak, Java SDK'nın bin klasörünü PATH'e ekleyin."
    exit 1
}

if (-not (Test-Path $keystorePath)) {
    Write-Host "Keystore dosyası bulunamadı: $keystorePath"
    exit 1
}

Write-Host "Keystore SHA1 kontrol ediliyor..."
Write-Host ""

# SHA1'i al
$output = & $keytoolPath -list -v -keystore $keystorePath -alias $alias -storepass $password 2>&1

if ($LASTEXITCODE -eq 0) {
    $sha1Line = $output | Select-String -Pattern "SHA1:"
    if ($sha1Line) {
        Write-Host "Mevcut Keystore SHA1:"
        Write-Host $sha1Line -ForegroundColor Green
        Write-Host ""
        Write-Host "Google Play'de beklenen SHA1:"
        Write-Host "SHA1: AA:3F:D4:87:F7:A8:FF:EB:19:66:5F:D6:DB:0E:BC:21:4A:88:01:4D" -ForegroundColor Yellow
        Write-Host ""
        
        if ($sha1Line -match "AA:3F:D4:87:F7:A8:FF:EB:19:66:5F:D6:DB:0E:BC:21:4A:88:01:4D") {
            Write-Host "✅ SHA1 eşleşiyor! Doğru keystore kullanılıyor." -ForegroundColor Green
        } else {
            Write-Host "❌ SHA1 eşleşmiyor! Yanlış keystore kullanılıyor." -ForegroundColor Red
            Write-Host ""
            Write-Host "ÇÖZÜM:" -ForegroundColor Yellow
            Write-Host "1. Google Play Console'da beklenen SHA1'e sahip keystore'u bulun"
            Write-Host "2. Bu keystore'u MerchPlusMAUI\Platforms\Android\ klasörüne kopyalayın"
            Write-Host "3. .csproj dosyasındaki keystore bilgilerini güncelleyin"
            Write-Host "4. Yeni AAB dosyası oluşturun"
        }
    } else {
        Write-Host "SHA1 bilgisi bulunamadı. Keystore'u kontrol edin."
    }
} else {
    Write-Host "Hata: Keystore okunamadı. Şifre veya alias yanlış olabilir." -ForegroundColor Red
    Write-Host $output
}

