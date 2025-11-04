using System.Text.Json;

namespace BikeRentalClient.BikeUtils
{
    public class BikeService : ApiService
    {
        public BikeService(HttpClient client) : base(client) { }

        public List<Bike> GetAllBikes()
        {
            HttpResponseMessage response = client.GetAsync("bikes").Result;

            if (response.IsSuccessStatusCode)
            {
                string resultString = response.Content.ReadAsStringAsync().Result;
                List<Bike>? bikes = JsonSerializer.Deserialize<List<Bike>>(resultString, JsonOpts);
                return bikes ?? new List<Bike>();
            }

            return new List<Bike>();
        }

        public List<Bike> GetAvailableBikes()
        {
            List<Bike> bikes = GetAllBikes();
            return bikes.Where(bike => bike.Status == Bike.BikeStatus.Available).ToList();
        }
    }
}
