using System.Net;
using System.Text;
using System.Text.Json;

namespace BikeRentalClient.BikeUtils
{
    public class BikeService : ApiService
    {
        public BikeService(HttpClient client) : base(client) { }

        public List<Bike> GetAllBikes()
        {
            var response = client.GetAsync("bikes").Result;

            if (response.IsSuccessStatusCode)
            {
                var resultString = response.Content.ReadAsStringAsync().Result;
                var bikes = JsonSerializer.Deserialize<List<Bike>>(resultString, JsonOpts);
                return bikes ?? new List<Bike>();
            }
            return new List<Bike>();
        }

        public List<Bike> GetFilteredBikes(Bike.BikeStatus bikeStatus)
        {
            var bikes = GetAllBikes();
            return bikes.Where(b => b.Status == bikeStatus).ToList();
        }

        // Return status + message; no MessageBox here
        public (bool Success, string Message) AddBike(Bike bike)
        {
            var json = JsonSerializer.Serialize(bike, JsonOpts);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = client.PostAsync("bikes", content).Result;

            if (response.IsSuccessStatusCode)
                return (true, "Bike added successfully!");

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
