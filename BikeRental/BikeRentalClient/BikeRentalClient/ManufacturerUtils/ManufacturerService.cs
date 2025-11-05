using BikeRentalClient.BikeUtils;
using System.Net;
using System.Text.Json;

namespace BikeRentalClient.ManufacturerUtils
{
    public class ManufacturerService : ApiService
    {
        public ManufacturerService(HttpClient client) : base(client) { }

        public List<Manufacturer> GetAllManufacturers()
        {
            HttpResponseMessage response = client.GetAsync("manufacturers").Result;

            if (response.IsSuccessStatusCode)
            {
                string resultString = response.Content.ReadAsStringAsync().Result;
                List<Manufacturer>? manufacturers = JsonSerializer.Deserialize<List<Manufacturer>>(resultString, JsonOpts);
                return manufacturers ?? new List<Manufacturer>();
            }

            return new List<Manufacturer>();
        }

        public Manufacturer GetManufacturerById(int id)
        {
            HttpResponseMessage response = client.GetAsync($"manufacturers/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                string resultString = response.Content.ReadAsStringAsync().Result;
                Manufacturer? manufacturer = JsonSerializer.Deserialize<Manufacturer>(resultString, JsonOpts);
                return manufacturer ?? new Manufacturer();
            }
            return new Manufacturer();
        }
        public (bool Success, string Message) AddManufacturer(Manufacturer manufacturer)
        {
            var json = JsonSerializer.Serialize(manufacturer, JsonOpts);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = client.PostAsync("manufacturers", content).Result;
            
            if (response.IsSuccessStatusCode)
                return (true, "Manufacturer added successfully!");

            if (response.StatusCode == HttpStatusCode.Conflict)
                return (false, "Conflict: duplicate or constraint violation.");

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var msg = response.Content.ReadAsStringAsync().Result;
                return (false, $"Invalid input: {msg}");
            }

            return (false, $"Unexpected error ({(int)response.StatusCode})");
        }

    }
}
