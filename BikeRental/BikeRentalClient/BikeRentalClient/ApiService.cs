using System.Net;
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
        public static string BuildErrorMessage(HttpResponseMessage resp)
        {
            string body = string.Empty;
            try { body = resp.Content.ReadAsStringAsync().Result; } catch { /* ignore */ }

            return resp.StatusCode switch
            {
                HttpStatusCode.BadRequest => string.IsNullOrWhiteSpace(body) ? "Invalid input." : $"Invalid input: {body}",
                HttpStatusCode.Conflict => string.IsNullOrWhiteSpace(body) ? "Conflict: duplicate or constraint violation." : body,
                HttpStatusCode.NotFound => string.IsNullOrWhiteSpace(body) ? "Resource not found." : body,
                HttpStatusCode.Unauthorized => "Not authorized.",
                HttpStatusCode.Forbidden => "Access forbidden.",
                _ => $"Unexpected error {(int)resp.StatusCode} {resp.ReasonPhrase}. " +
                     (string.IsNullOrWhiteSpace(body) ? "" : $"Details: {body}")
            };
        }
    }
}
