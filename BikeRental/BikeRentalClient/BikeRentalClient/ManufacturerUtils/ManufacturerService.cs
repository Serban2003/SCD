using System.Net;
using System.Text;
using System.Text.Json;
using BikeRentalClient.BikeUtils;

namespace BikeRentalClient.ManufacturerUtils
{
    public class ManufacturerService : ApiService
    {
        public ManufacturerService(HttpClient client) : base(client) { }

        // GET /manufacturers
        public (bool Success, string Message, List<Manufacturer> Manufacturers) GetAllManufacturers()
        {
            using var response = client.GetAsync("manufacturers").Result;

            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                var manufacturers = JsonSerializer.Deserialize<List<Manufacturer>>(json, JsonOpts) ?? new List<Manufacturer>();
                return (true, "Fetched manufacturers.", manufacturers);
            }

            return (false, BuildErrorMessage(response), new List<Manufacturer>());
        }

        // GET /manufacturers/{id} -- not used currently
        public (bool Success, string Message, Manufacturer? Manufacturer) GetManufacturerById(int id)
        {
            using var response = client.GetAsync($"manufacturers/{id}").Result;

            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                var manu = JsonSerializer.Deserialize<Manufacturer>(json, JsonOpts);
                return (true, "Fetched manufacturer.", manu);
            }

            return (false, BuildErrorMessage(response), null);
        }

        // POST /manufacturers -- not used currently
        public (bool Success, string Message) AddManufacturer(Manufacturer manufacturer)
        {
            var json = JsonSerializer.Serialize(manufacturer, JsonOpts);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            using var response = client.PostAsync("manufacturers", content).Result;

            if (response.IsSuccessStatusCode)
                return (true, "Manufacturer added successfully.");

            return (false, BuildErrorMessage(response));
        }

        // PUT /manufacturers/{id} -- not used currently
        public (bool Success, string Message) UpdateManufacturer(Manufacturer manufacturer)
        {
            var json = JsonSerializer.Serialize(manufacturer, JsonOpts);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            using var response = client.PutAsync($"manufacturers/{manufacturer.Manufacturer_id}", content).Result;

            if (response.IsSuccessStatusCode)
                return (true, "Manufacturer updated successfully.");

            return (false, BuildErrorMessage(response));
        }

        // DELETE /manufacturers/{id} -- not used currently
        public (bool Success, string Message) DeleteManufacturer(int id)
        {
            using var response = client.DeleteAsync($"manufacturers/{id}").Result;

            if (response.IsSuccessStatusCode)
                return (true, "Manufacturer deleted successfully.");

            return (false, BuildErrorMessage(response));
        }
    }
}
