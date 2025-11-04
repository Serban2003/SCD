using System.Text.Json;

namespace BikeRentalClient.UserUtils
{
    public class UserService : ApiService
    {
        public UserService(HttpClient client) : base(client) { }

        public List<User> GetAllUsers()
        {
            HttpResponseMessage response = client.GetAsync("users").Result;

            if (response.IsSuccessStatusCode)
            {
                string resultString = response.Content.ReadAsStringAsync().Result;
                List<User>? users = JsonSerializer.Deserialize<List<User>>(resultString, JsonOpts);
                return users ?? new List<User>();
            }

            return new List<User>();
        }
    }
}
