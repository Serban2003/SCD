using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BikeRentalClient
{
    internal class Bike
    {
        public int id { get; set; } = 0;
        public Manufacturer manufacturer { get; set; } = new Manufacturer();
        public string model { get; set; } = string.Empty;

        public int year { get; set; } = 0;
        public List<Component> components { get; set; } = new List<Component>();
        public float price { get; set; } = 0.0f;
        public BikeStatus status {  get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]

        internal enum BikeStatus
        {
            Available,
            Rented,
            Maintenance
        }
    }
}
