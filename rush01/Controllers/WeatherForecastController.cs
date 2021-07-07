using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using rush01.Models;
using rush01.Services;

namespace rush01.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly WeatherService _weatherService;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IOptions<ServiceSettings> settings, ILogger<WeatherForecastController> logger)
        {
            _weatherService = new WeatherService(settings);
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
                WeatherForecast forecast = await _weatherService.GetAsync(latitude, longitude);
                return Ok(forecast);
            }
            catch (Exception ex)
            {
                var problem = new ProblemDetails { Detail = ex.Message };
                return BadRequest(problem);
            }
        }

        /// <summary>
        /// Current weather API provided by OpenWeatherMap
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /weatherforecast/Moscow
        ///
        /// </remarks>
        /// <param name="city"></param>
        /// <returns>Current weather for provided city</returns>
        /// <response code="200">Returns current weather</response>
        /// <response code="400">If something went wrong</response>
        [HttpGet]
        [Route("{city}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WeatherForecast))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAsync(string city)
        {
            try
            {
                WeatherForecast forecast = await _weatherService.GetAsync(city);
                return Ok(forecast);
            }
            catch (Exception ex)
            {
                return BadRequest(new ProblemDetails { Detail = ex.Message });
            }
        }
    }
}
