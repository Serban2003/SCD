using System.Text.Json.Serialization;
using BikeRentalClient.ManufacturerUtils;
using BikeRentalClient.UserUtils;

namespace BikeRentalClient.BikeUtils
{
    public class Bike
    {
        public int Bike_id { get; set; } = 0;
        public Manufacturer Manufacturer { get; set; } = new Manufacturer();
        public string Model { get; set; } = string.Empty;

        public int Year { get; set; } = 0;
        public float Price { get; set; } = 0.0f;
        public BikeStatus Status {  get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]

        public User? CurrentRenter { get; set; } = null;
        public DateTime? RentDate { get; set; } = null;
        public enum BikeStatus
        {
            Available,
            Rented,
            Maintenance
        }
    }
}
