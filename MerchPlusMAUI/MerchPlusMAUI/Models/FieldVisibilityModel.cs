using System.Text.Json.Serialization;

namespace MerchPlusMAUI.Models
{
    public class FieldVisibilityModel
    {
        [JsonPropertyName("faceCount")]
        public bool FaceCount { get; set; } = true;

        [JsonPropertyName("shelfIndex")]
        public bool ShelfIndex { get; set; } = true;

        [JsonPropertyName("shelfStock")]
        public bool ShelfStock { get; set; } = true;

        [JsonPropertyName("price")]
        public bool Price { get; set; } = true;

        [JsonPropertyName("warehouseStock")]
        public bool WarehouseStock { get; set; } = true;

        [JsonPropertyName("activityType")]
        public bool ActivityType { get; set; } = true;

        [JsonPropertyName("discountPrice")]
        public bool DiscountPrice { get; set; } = true;

        [JsonPropertyName("notes")]
        public bool Notes { get; set; } = true;
    }
}
