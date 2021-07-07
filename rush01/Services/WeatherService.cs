using Microsoft.Extensions.Options;
using rush01.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace rush01.Services
{
    public class ServiceSettings
    {
        public string ApiKey { get; set; }
    }

    public class WeatherService
    {
        private const string ApiUrl = "https://api.openweathermap.org/data/2.5/weather?";
        private readonly ServiceSettings _settings;

        public WeatherService(IOptions<ServiceSettings> options) => _settings = options.Value;

        public async Task<WeatherForecast> GetAsync(double latitude, double longitude) =>
            await HttpGetAsync($"lat={latitude}&lon={longitude}&appid={_settings.ApiKey}");

        public async Task<WeatherForecast> GetAsync(string city) =>
            await HttpGetAsync($"q={city}&appid={_settings.ApiKey}");

        private static async Task<WeatherForecast> HttpGetAsync(string query)
        {
            using HttpResponseMessage response = await new HttpClient().GetAsync(ApiUrl + query);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<WeatherForecast>();

            JsonDocument jsonDocument =
                JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            string message = jsonDocument.RootElement.GetProperty("message").GetString();
            throw new HttpRequestException(message);
        }
    }
}
