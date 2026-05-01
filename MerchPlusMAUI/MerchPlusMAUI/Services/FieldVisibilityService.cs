using System.Text.Json;
using MerchPlusMAUI.Models;
using System.Text;

namespace MerchPlusMAUI.Services
{
    public interface IFieldVisibilityService
    {
        Task<FieldVisibilityModel?> GetFieldVisibilityAsync(int customerId);
    }

    public class FieldVisibilityService : IFieldVisibilityService
    {
        private readonly HttpClient _httpClient;
        private readonly string _serviceUrl;

        public FieldVisibilityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            // Primary URL: app genelinde kullanılan APIPath2 ile aynı backend.
            _serviceUrl = $"{GlobalVariables.APIPath2.TrimEnd('/')}/CustomerFormFieldVisibility/SelectByCustomerId";
        }

        public async Task<FieldVisibilityModel?> GetFieldVisibilityAsync(int customerId)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"FieldVisibilityService.GetFieldVisibilityAsync: Servis URL (primary): {_serviceUrl}");
                System.Diagnostics.Debug.WriteLine($"FieldVisibilityService.GetFieldVisibilityAsync: CustomerId: {customerId}");
                
                // POST request için JSON body oluştur
                var requestData = new { CustomerId = customerId };
                var jsonContent = JsonSerializer.Serialize(requestData);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                
                System.Diagnostics.Debug.WriteLine($"FieldVisibilityService.GetFieldVisibilityAsync: Request Body: {jsonContent}");
                
                // POST request gönder
                var response = await _httpClient.PostAsync(_serviceUrl, content);
                
                System.Diagnostics.Debug.WriteLine($"FieldVisibilityService.GetFieldVisibilityAsync: HTTP Status: {response.StatusCode}");
                
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    
                    System.Diagnostics.Debug.WriteLine($"FieldVisibilityService.GetFieldVisibilityAsync: Response JSON: {responseContent}");
                    
                    // Eğer boş cevap gelirse null döndür (tüm alanlar gösterilecek)
                    if (string.IsNullOrWhiteSpace(responseContent))
                    {
                        System.Diagnostics.Debug.WriteLine("FieldVisibilityService.GetFieldVisibilityAsync: Boş cevap, null döndürülüyor");
                        return null;
                    }

                    try
                    {
                        // API'den gelen JSON formatını parse et
                        using var jsonDoc = JsonDocument.Parse(responseContent);
                        var root = jsonDoc.RootElement;
                        
                        // Result kontrolü
                        if (root.TryGetProperty("Result", out var resultElement))
                        {
                            var result = resultElement.GetString();
                            System.Diagnostics.Debug.WriteLine($"FieldVisibilityService.GetFieldVisibilityAsync: API Result: {result}");
                            
                            if (result == "OK")
                            {
                                // Content array'ini al
                                if (root.TryGetProperty("Content", out var contentElement) && contentElement.ValueKind == JsonValueKind.Array)
                                {
                                    var contentArray = contentElement.EnumerateArray();
                                    if (contentArray.Any())
                                    {
                                        // Tüm field visibility kayıtlarını işle
                                        var fieldVisibility = new FieldVisibilityModel();
                                        
                                        // Önce tüm alanları false olarak ayarla (gizle)
                                        fieldVisibility.FaceCount = false;
                                        fieldVisibility.ShelfIndex = false;
                                        fieldVisibility.ShelfStock = false;
                                        fieldVisibility.Price = false;
                                        fieldVisibility.WarehouseStock = false;
                                        fieldVisibility.ActivityType = false;
                                        fieldVisibility.DiscountPrice = false;
                                        fieldVisibility.Notes = false;
                                        
                                        // Servisten gelen alanları işle - gelen alanları göster
                                        foreach (var item in contentArray)
                                        {
                                            if (item.TryGetProperty("FieldName", out var fieldNameElement) && 
                                                item.TryGetProperty("IsVisible", out var isVisibleElement))
                                            {
                                                var fieldName = fieldNameElement.GetString();
                                                var isVisible = isVisibleElement.GetBoolean();
                                                
                                                System.Diagnostics.Debug.WriteLine($"FieldVisibilityService.GetFieldVisibilityAsync: Gelen Field: {fieldName}, IsVisible: {isVisible}");
                                                
                                                // Sadece IsVisible=true olan alanları göster
                                                if (isVisible)
                                                {
                                                    switch (fieldName)
                                                    {
                                                        case "FaceCount":
                                                            fieldVisibility.FaceCount = true;
                                                            break;
                                                        case "ShelfIndex":
                                                            fieldVisibility.ShelfIndex = true;
                                                            break;
                                                        case "ShelfStock":
                                                            fieldVisibility.ShelfStock = true;
                                                            break;
                                                        case "Price":
                                                            fieldVisibility.Price = true;
                                                            break;
                                                        case "WarehouseStock":
                                                            fieldVisibility.WarehouseStock = true;
                                                            break;
                                                        case "ActivityType":
                                                            fieldVisibility.ActivityType = true;
                                                            break;
                                                        case "DiscountPrice":
                                                            fieldVisibility.DiscountPrice = true;
                                                            break;
                                                        case "Notes":
                                                            fieldVisibility.Notes = true;
                                                            break;
                                                    }
                                                }
                                            }
                                        }
                                        
                                        System.Diagnostics.Debug.WriteLine($"FieldVisibilityService.GetFieldVisibilityAsync: Field Visibility Detayları:");
                                        System.Diagnostics.Debug.WriteLine($"  - FaceCount: {fieldVisibility.FaceCount}");
                                        System.Diagnostics.Debug.WriteLine($"  - ShelfIndex: {fieldVisibility.ShelfIndex}");
                                        System.Diagnostics.Debug.WriteLine($"  - ShelfStock: {fieldVisibility.ShelfStock}");
                                        System.Diagnostics.Debug.WriteLine($"  - Price: {fieldVisibility.Price}");
                                        System.Diagnostics.Debug.WriteLine($"  - WarehouseStock: {fieldVisibility.WarehouseStock}");
                                        System.Diagnostics.Debug.WriteLine($"  - ActivityType: {fieldVisibility.ActivityType}");
                                        System.Diagnostics.Debug.WriteLine($"  - DiscountPrice: {fieldVisibility.DiscountPrice}");
                                        System.Diagnostics.Debug.WriteLine($"  - Notes: {fieldVisibility.Notes}");
                                        
                                        return fieldVisibility;
                                    }
                                    else
                                    {
                                        System.Diagnostics.Debug.WriteLine("FieldVisibilityService.GetFieldVisibilityAsync: Content array boş - tüm alanlar gösterilecek");
                                        // Hiç data gelmiyorsa tüm alanları göster
                                        return new FieldVisibilityModel
                                        {
                                            FaceCount = true,
                                            ShelfIndex = true,
                                            ShelfStock = true,
                                            Price = true,
                                            WarehouseStock = true,
                                            ActivityType = true,
                                            DiscountPrice = true,
                                            Notes = true
                                        };
                                    }
                                }
                                else
                                {
                                    System.Diagnostics.Debug.WriteLine("FieldVisibilityService.GetFieldVisibilityAsync: Content property bulunamadı veya array değil - tüm alanlar gösterilecek");
                                    // Content property bulunamıyorsa tüm alanları göster
                                    return new FieldVisibilityModel
                                    {
                                        FaceCount = true,
                                        ShelfIndex = true,
                                        ShelfStock = true,
                                        Price = true,
                                        WarehouseStock = true,
                                        ActivityType = true,
                                        DiscountPrice = true,
                                        Notes = true
                                    };
                                }
                            }
                            else
                            {
                                // NOK durumu
                                var reason = root.TryGetProperty("Reason", out var reasonElement) ? reasonElement.GetString() : "Unknown";
                                var details = root.TryGetProperty("Details", out var detailsElement) ? detailsElement.GetString() : "No details";
                                System.Diagnostics.Debug.WriteLine($"FieldVisibilityService.GetFieldVisibilityAsync: API Error - Reason: {reason}, Details: {details}");
                                return null;
                            }
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("FieldVisibilityService.GetFieldVisibilityAsync: Result property bulunamadı");
                            return null;
                        }
                    }
                    catch (JsonException jsonEx)
                    {
                        System.Diagnostics.Debug.WriteLine($"FieldVisibilityService.GetFieldVisibilityAsync: JSON Parse Hatası: {jsonEx.Message}");
                        System.Diagnostics.Debug.WriteLine($"FieldVisibilityService.GetFieldVisibilityAsync: JSON İçeriği: {responseContent}");
                        return null;
                    }
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine($"FieldVisibilityService.GetFieldVisibilityAsync: HTTP Hata - Status: {response.StatusCode}");
                    System.Diagnostics.Debug.WriteLine($"FieldVisibilityService.GetFieldVisibilityAsync: HTTP Hata - Reason: {response.ReasonPhrase}");
                    System.Diagnostics.Debug.WriteLine($"FieldVisibilityService.GetFieldVisibilityAsync: HTTP Hata - Content: {errorContent}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"FieldVisibilityService.GetFieldVisibilityAsync: Exception: {ex.Message}");
                // Herhangi bir hata durumunda null döndür (tüm alanlar gösterilecek)
                return null;
            }
        }
    }
}
