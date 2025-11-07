using System.Text;
using System.Text.Json;

namespace BikeRentalClient.BikeUtils
{
    public class BikeService : ApiService
    {
        public BikeService(HttpClient client) : base(client) { }

        // GET /bikes
        public (bool Success, string Message, List<Bike> Bikes) GetAllBikes()
        {
            using var response = client.GetAsync("bikes").Result;

            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                var bikes = JsonSerializer.Deserialize<List<Bike>>(json, JsonOpts) ?? new List<Bike>();
                return (true, "Fetched bikes.", bikes);
            }

            return (false, BuildErrorMessage(response), new List<Bike>());
        }

        public (bool Success, string Message, List<Bike> Bikes) GetFilteredBikes(Bike.BikeStatus bikeStatus)
        {
            var (ok, msg, bikes) = GetAllBikes();
            if (!ok) return (false, msg, new List<Bike>());

            var filtered = bikes.Where(b => b.Status == bikeStatus).ToList();
            return (true, $"Fetched {filtered.Count} bike(s) with status {bikeStatus}.", filtered);
        }

        // POST /bikes
        public (bool Success, string Message) AddBike(Bike bike)
        {
            var json = JsonSerializer.Serialize(bike, JsonOpts);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            using var response = client.PostAsync("bikes", content).Result;

            if (response.IsSuccessStatusCode)
                return (true, "Bike added successfully.");

            return (false, BuildErrorMessage(response));
        }

        // PUT /bikes/{id}
        public (bool Success, string Message) UpdateBike(Bike bike)
        {
            var json = JsonSerializer.Serialize(bike, JsonOpts);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            using var response = client.PutAsync($"bikes/{bike.Bike_id}", content).Result;

            if (response.IsSuccessStatusCode)
                return (true, "Bike updated successfully.");

            return (false, BuildErrorMessage(response));
        }

        // GET /bikes/rented/{userId}
        public (bool Success, string Message, List<Bike> Bikes) GetRentedBikesByUser(int userId)
        {
            using var response = client.GetAsync($"bikes/rented/{userId}").Result;

            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                var bikes = JsonSerializer.Deserialize<List<Bike>>(json, JsonOpts) ?? new List<Bike>();
                return (true, "Fetched rented bikes.", bikes);
            }

            return (false, BuildErrorMessage(response), new List<Bike>());
        }
    }
}
