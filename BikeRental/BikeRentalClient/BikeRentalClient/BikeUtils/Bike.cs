using System.Text.Json.Serialization;

namespace BikeRentalClient.BikeUtils
{
    public class Bike
    {
        public int Bike_id { get; set; } = 0;
        public int Manufacturer_id { get; set; } = 0;
        public string Model { get; set; } = string.Empty;

        public int Year { get; set; } = 0;
        public float Price { get; set; } = 0.0f;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public BikeStatus Status {  get; set; }

        public int? CurrentRenter_id { get; set; } = null;
        public DateTime? RentDate { get; set; } = null;
        public int? RentTime { get; set; } = null;
        public enum BikeStatus
        {
            AVAILABLE,
            RENTED,
            MAINTENANCE
        }
    }
}
