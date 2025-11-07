using System.Text;
using System.Text.Json;

namespace BikeRentalClient.UserUtils
{
    public class UserService : ApiService
    {
        public event Action? LoggedUserChanged;

        private User? _loggedUser;
        public User? LoggedUser
        {
            get => _loggedUser;
            set
            {
                _loggedUser = value;
                LoggedUserChanged?.Invoke();
            }
        }

        public UserService(HttpClient client) : base(client) { }

        // GET /users
        public (bool Success, string Message, List<User> Users) GetAllUsers()
        {
            using var response = client.GetAsync("users").Result;

            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                var users = JsonSerializer.Deserialize<List<User>>(json, JsonOpts) ?? new List<User>();
                return (true, "Fetched users.", users);
            }

            return (false, BuildErrorMessage(response), new List<User>());
        }

        // GET /users/{id}
        public (bool Success, string Message, User? User) GetUserById(int id)
        {
            using var response = client.GetAsync($"users/{id}").Result;

            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                var user = JsonSerializer.Deserialize<User>(json, JsonOpts);
                return (true, "Fetched user.", user);
            }

            return (false, BuildErrorMessage(response), null);
        }

        // POST /users
        public (bool Success, string Message, User? User) AddUser(User user)
        {
            var json = JsonSerializer.Serialize(user, JsonOpts);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            using var response = client.PostAsync("users", content).Result;

            if (response.IsSuccessStatusCode)
            {
                var body = response.Content.ReadAsStringAsync().Result;
                var created = JsonSerializer.Deserialize<User>(body, JsonOpts);
                return (true, "User added successfully.", created);
            }

            return (false, BuildErrorMessage(response), null);
        }

        // POST /users/login
        public (bool Success, string Message, User? User) Login(string email, string password)
        {
            var payload = new { email, password };
            var json = JsonSerializer.Serialize(payload, JsonOpts);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            using var response = client.PostAsync("users/login", content).Result;

            if (response.IsSuccessStatusCode)
            {
                var body = response.Content.ReadAsStringAsync().Result;
                var user = JsonSerializer.Deserialize<User>(body, JsonOpts);
                return (true, "Login successful.", user);
            }

            return (false, BuildErrorMessage(response), null);
        }

        // POST /users/register
        public (bool Success, string Message, User? User) Register(User user)
        {
            var json = JsonSerializer.Serialize(user, JsonOpts);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            using var response = client.PostAsync("users/register", content).Result;

            if (response.IsSuccessStatusCode)
            {
                var body = response.Content.ReadAsStringAsync().Result;
                var created = JsonSerializer.Deserialize<User>(body, JsonOpts);
                return (true, "User registered successfully.", created);
            }

            return (false, BuildErrorMessage(response), null);
        }

        // PUT /users/{id} -- not used currently
        public (bool Success, string Message, User? User) UpdateUser(User user)
        {
            var json = JsonSerializer.Serialize(user, JsonOpts);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            using var response = client.PutAsync($"users/{user.User_id}", content).Result;

            if (response.IsSuccessStatusCode)
            {
                var body = response.Content.ReadAsStringAsync().Result;
                var updated = JsonSerializer.Deserialize<User>(body, JsonOpts);
                return (true, "User updated successfully.", updated);
            }

            return (false, BuildErrorMessage(response), null);
        }

        // DELETE /users/{id} -- not used currently
        public (bool Success, string Message) DeleteUser(int id)
        {
            using var response = client.DeleteAsync($"users/{id}").Result;

            if (response.IsSuccessStatusCode)
                return (true, "User deleted successfully.");

            return (false, BuildErrorMessage(response));
        }
    }
}
