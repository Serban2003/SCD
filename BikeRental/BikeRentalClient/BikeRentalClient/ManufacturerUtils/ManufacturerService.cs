using System.Text.Json;

namespace BikeRentalClient.ManufacturerUtils
{
    public class ManufacturerService : ApiService
    {
        public ManufacturerService(HttpClient client) : base(client) { }

        public List<Manufacturer> GetAllUsers()
        {
            HttpResponseMessage response = client.GetAsync("manufacturers").Result;

            if (response.IsSuccessStatusCode)
            {
                string resultString = response.Content.ReadAsStringAsync().Result;
                List<Manufacturer>? users = JsonSerializer.Deserialize<List<Manufacturer>>(resultString, JsonOpts);
                return users ?? new List<Manufacturer>();
            }

            return new List<Manufacturer>();
        }
    }
}
