using rush01.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace rush01.Services
{
    public class WeatherService
    {
        private static readonly string _apiUrl = "http://api.openweathermap.org/data/2.5/weather?";

        public static async Task<WeatherForecast> GetAsync(string apiKey, double latitude, double longitude) =>
            await HttpGetAsync($"lat={latitude}&lon={longitude}&appid={apiKey}");

        public static async Task<WeatherForecast> GetAsync(string apiKey, string city) =>
            await HttpGetAsync($"q={city}&appid={apiKey}");

        private static async Task<WeatherForecast> HttpGetAsync(string query)
        {
            using HttpResponseMessage response = await new HttpClient().GetAsync(_apiUrl + query);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<WeatherForecast>();

            JsonDocument jsonDocument =
                JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            string message = "unknown error";
            message = jsonDocument.RootElement.GetProperty("message").GetString();
            throw new HttpRequestException(message);
        }
    }
}
