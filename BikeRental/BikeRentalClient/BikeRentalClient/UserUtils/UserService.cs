using System.Net;
using System.Text;
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

        public User GetUserById(int id)
        {
            HttpResponseMessage response = client.GetAsync($"users/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                string resultString = response.Content.ReadAsStringAsync().Result;
                User? user = JsonSerializer.Deserialize<User>(resultString, JsonOpts);
                return user ?? new User();
            }
            return new User();
        }

        public (bool Success, string Message) AddUser(User user)
        {
            var json = JsonSerializer.Serialize(user, JsonOpts);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = client.PostAsync("users", content).Result;

            if (response.IsSuccessStatusCode)
                return (true, "User added successfully!");

            if (response.StatusCode == HttpStatusCode.Conflict)
                return (false, "This email is already registered.");

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var msg = response.Content.ReadAsStringAsync().Result;
                return (false, $"Invalid input: {msg}");
            }

            return (false, $"Unexpected error ({(int)response.StatusCode})");
        }
    }
}
