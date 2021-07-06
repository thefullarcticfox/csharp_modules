using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using rush01.Models;

namespace rush01.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly string _apiKey;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IConfiguration config, ILogger<WeatherForecastController> logger)
        {
            _apiKey = config["OpenWeatherMap:ApiKey"];
            _logger = logger;
        }

        /// <summary>
        /// Current weather API provided by OpenWeatherMap
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /weatherforecast?lat=55.7558&amp;lon=37.6173
        ///
        /// </remarks>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns>Current weather for provided latitude and longitude</returns>
        /// <response code="200">Returns current weather</response>
        /// <response code="400">If something went wrong</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WeatherForecast))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAsync(
            [FromQuery(Name = "lat")] double latitude,
            [FromQuery(Name = "lon")] double longitude)
        {
            try
            {
                var forecast = await HttpGetWeatherAsync(
                    "http://api.openweathermap.org/data/2.5/weather?" +
                    $"lat={latitude}&lon={longitude}&appid={_apiKey}");
                return Ok(forecast);
            }
            catch (Exception ex)
            {
                var problem = new ProblemDetails { Detail = ex.Message };
                return BadRequest(problem);
            }
        }

        private async Task<WeatherForecast> HttpGetWeatherAsync(string url)
        {
            using HttpResponseMessage response = await new HttpClient().GetAsync(url);
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
