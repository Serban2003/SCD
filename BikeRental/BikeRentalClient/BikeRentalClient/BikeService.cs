using System.Net.Http.Headers;
using System.Text.Json;

namespace BikeRentalClient
{
    internal class BikeService
    {
        static HttpClient client = new HttpClient();

        public void createConnection()
        {
            client.BaseAddress = new Uri("http://localhost:8083");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public List<Bike> GetBike()
        {
            List<Bike> bikes = new List<Bike>();
            HttpResponseMessage response = client.GetAsync("bike/get-bikes").Result;

            if (response.IsSuccessStatusCode)
            {
                string resultString = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(resultString);
                bikes = JsonSerializer.Deserialize<List<Bike>>(resultString);
                return bikes;
            }

            return null;
        }

        public List<Bike> GetAvailableBikes()
        {
            List<Bike> bikes = this.GetBike();
            return bikes.Select(bike => bike.status == Bike.BikeStatus.Available).ToList();
        }
    }
}
