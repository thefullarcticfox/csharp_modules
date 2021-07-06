using rush01.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace rush01.Services
{
    public static class WeatherService
    {
        private static readonly string _apiUrl = "http://api.openweathermap.org/data/2.5/weather?";

        public static async Task<WeatherForecast> HttpGetAsync(string query)
        {
            using HttpResponseMessage response = await new HttpClient().GetAsync(_apiUrl + query);
            if (response.IsSuccessStatusCode)
            {
                var weather = await response.Content
                    .ReadFromJsonAsync<OpenWeatherMapResponse>();
                return new WeatherForecast
                {
                    Name = weather.Name,
                    Description = weather.Description,
                    TemperatureK = weather.TempKelvin,
                    Pressure = weather.Pressure,
                    Humidity = weather.Humidity,
                    Wind = weather.WindSpeed
                };
            }

            string content = await response.Content.ReadAsStringAsync();
            JsonDocument jsonDocument = JsonDocument.Parse(content);
            string message = "unknown error";

            try
            {
                message = jsonDocument.RootElement.GetProperty("message").GetString();
            }
            catch
            {
                // can be ignored
            }

            throw new HttpRequestException(message);
        }
    }
}
