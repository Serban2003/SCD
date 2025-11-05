using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BikeRentalClient
{
    public abstract class ApiService
    {
        protected readonly HttpClient client;
        protected static readonly JsonSerializerOptions JsonOpts = new() {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() },
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        protected ApiService(HttpClient client) { this.client = client ?? throw new ArgumentNullException(nameof(client));}

        public virtual void CreateConnection()
        {
            client.BaseAddress = new Uri("http://localhost:8083/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

    }
}
