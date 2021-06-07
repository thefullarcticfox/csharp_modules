using System.IO;
using System.Net.Http;
using System.Text.Json;
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
            {
                using Task<Stream> streamTask = response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<T>(await streamTask);
            }

            string content = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"GET \"{url}\" returned {response.StatusCode}:\n{content}");
        }
    }
}
