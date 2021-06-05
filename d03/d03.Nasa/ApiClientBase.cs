using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace d03.Nasa
{
    public abstract class ApiClientBase
    {
        protected readonly string _apiKey;
        protected readonly HttpClient httpClient = new HttpClient();

        protected ApiClientBase(string apiKey)
        {
            _apiKey = apiKey;
        }

        protected async Task<T> HttpGetAsync<T>(string url)
        {
            var response = await httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            if (response.IsSuccessStatusCode)
            {
                var streamTask = response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<T>(await streamTask);
            }

            var content = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"GET \"{url}\" returned {response.StatusCode}:\n{content}");
        }
    }
}
