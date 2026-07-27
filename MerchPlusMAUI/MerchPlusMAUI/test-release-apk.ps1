# Release APK Test Script
# Bu script Release build yapıp APK formatında oluşturur ve telefona kurar

Write-Host "=== Release APK Test Script ===" -ForegroundColor Cyan

# 1. Release build yap
Write-Host "`n1. Release build yapılıyor..." -ForegroundColor Yellow
dotnet clean -c Release -f net8.0-android
dotnet build -c Release -f net8.0-android

if ($LASTEXITCODE -ne 0) {
    Write-Host "Build başarısız!" -ForegroundColor Red
    exit 1
}

Write-Host "Build başarılı!" -ForegroundColor Green

# 2. APK formatını kontrol et
Write-Host "`n2. APK formatı kontrol ediliyor..." -ForegroundColor Yellow
$csprojFile = "MerchPlusMAUI\MerchPlusMAUI.csproj"
$csprojContent = Get-Content $csprojFile -Raw

# Geçici olarak APK formatına çevir
$tempFile = "$csprojFile.temp"
Copy-Item $csprojFile $tempFile

$csprojContent = $csprojContent -replace 'AndroidPackageFormat Condition="''\$\$\(Configuration\)'' == ''Release'' AND \$\$\(TargetFramework\.Contains\(''-android''\)\)">aab</AndroidPackageFormat>', 'AndroidPackageFormat Condition="''$(Configuration)'' == ''Release'' AND $(TargetFramework.Contains(''-android''))">apk</AndroidPackageFormat>'

Set-Content $csprojFile -Value $csprojContent -NoNewline

# 3. APK oluştur
Write-Host "`n3. APK oluşturuluyor..." -ForegroundColor Yellow
dotnet build -c Release -f net8.0-android

if ($LASTEXITCODE -ne 0) {
    Write-Host "APK oluşturma başarısız!" -ForegroundColor Red
    # Orijinal dosyayı geri yükle
    Move-Item $tempFile $csprojFile -Force
    exit 1
}

# 4. Orijinal dosyayı geri yükle (AAB formatına)
Write-Host "`n4. Orijinal ayarlar geri yükleniyor..." -ForegroundColor Yellow
Move-Item $tempFile $csprojFile -Force

# 5. APK dosyasını bul
Write-Host "`n5. APK dosyası aranıyor..." -ForegroundColor Yellow
$apkPath = Get-ChildItem -Path "bin\Release\net8.0-android" -Filter "*.apk" -Recurse | Select-Object -First 1

if ($apkPath -eq $null) {
    Write-Host "APK dosyası bulunamadı!" -ForegroundColor Red
    exit 1
}

Write-Host "APK bulundu: $($apkPath.FullName)" -ForegroundColor Green
Write-Host "APK boyutu: $([math]::Round($apkPath.Length / 1MB, 2)) MB" -ForegroundColor Cyan

# 6. Telefona kur (opsiyonel)
Write-Host "`n6. Telefona kurulacak mı? (Y/N): " -ForegroundColor Yellow -NoNewline
$install = Read-Host

if ($install -eq "Y" -or $install -eq "y") {
    Write-Host "Telefona kuruluyor..." -ForegroundColor Yellow
    adb install -r $apkPath.FullName
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host "APK başarıyla kuruldu!" -ForegroundColor Green
        Write-Host "`nUygulamayı test edebilirsiniz." -ForegroundColor Cyan
    } else {
        Write-Host "Kurulum başarısız! USB bağlantısını kontrol edin." -ForegroundColor Red
    }
} else {
    Write-Host "`nAPK hazır: $($apkPath.FullName)" -ForegroundColor Cyan
    Write-Host "Manuel olarak kurabilirsiniz: adb install -r $($apkPath.FullName)" -ForegroundColor Yellow
}

Write-Host "`n=== Test Tamamlandı ===" -ForegroundColor Cyan
