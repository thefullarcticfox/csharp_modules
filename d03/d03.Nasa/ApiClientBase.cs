using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace d03.Nasa
{
    public abstract class ApiClientBase
    {
        protected readonly string ApiKey;
        private readonly HttpClient _httpClient;

        protected ApiClientBase(string apiKey)
        {
            ApiKey = apiKey;
            _httpClient = new HttpClient();
        }

        protected async Task<T> HttpGetAsync<T>(string url)
        {
            using HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<T>();

            string content = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"GET \"{url}\" returned {response.StatusCode}:\n{content}");
        }
    }
}
