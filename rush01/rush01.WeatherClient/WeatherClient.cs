using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using rush01.WeatherClient.Models;

namespace rush01.WeatherClient
{
    public class ServiceSettings
    {
        public string ApiKey { get; set; }
    }

    public class WeatherClient
    {
        private const string ApiUrl = "https://api.openweathermap.org/data/2.5/weather?";
        private readonly ServiceSettings _settings;

        public WeatherClient(IOptions<ServiceSettings> settings) => _settings = settings.Value;

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
