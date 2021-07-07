using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using rush01.WeatherClient;
using rush01.WeatherClient.Models;

namespace rush01.WeatherApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly WeatherClient.WeatherClient _weatherClient;
        private readonly IMemoryCache _memoryCache;

        public WeatherForecastController(IOptions<ServiceSettings> settings, IMemoryCache memoryCache)
        {
            _weatherClient = new WeatherClient.WeatherClient(settings);
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// Gets current weather provided by OpenWeatherMap
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /weatherforecast/coords?lat=55.7558&amp;lon=37.6173
        ///
        /// </remarks>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns>Current weather for provided latitude and longitude</returns>
        /// <response code="200">Returns current weather</response>
        /// <response code="400">If something went wrong</response>
        [HttpGet]
        [Route("coords")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WeatherForecast))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAsync(
            [FromQuery(Name = "lat")] double latitude,
            [FromQuery(Name = "lon")] double longitude)
        {
            try
            {
                return Ok(await _weatherClient.GetAsync(latitude, longitude));
            }
            catch (Exception ex)
            {
                var problem = new ProblemDetails { Detail = ex.Message };
                return BadRequest(problem);
            }
        }

        /// <summary>
        /// Gets current weather provided by OpenWeatherMap
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /weatherforecast/Moscow
        ///
        /// </remarks>
        /// <param name="city">Optional if default city is set</param>
        /// <returns>Current weather for provided city</returns>
        /// <response code="200">Returns current weather</response>
        /// <response code="400">If something went wrong</response>
        [HttpGet]
        [Route("{city?}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WeatherForecast))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAsync(string city = null)
        {
            if (string.IsNullOrWhiteSpace(city) && !_memoryCache.TryGetValue("default_city", out city))
                return NotFound("City not provided");
            try
            {
                return Ok(await _weatherClient.GetAsync(city));
            }
            catch (Exception ex)
            {
                return BadRequest(new ProblemDetails { Detail = ex.Message });
            }
        }

        /// <summary>
        /// Sets default city for OpenWeatherMap API
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /weatherforecast/Moscow
        ///
        /// </remarks>
        /// <param name="city"></param>
        /// <response code="200">If successfully set</response>
        /// <response code="400">If city is not provided</response>
        [HttpPost]
        [Route("{city}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
                return NotFound("City not provided");

            _memoryCache.Set("default_city", city);
            return Ok($"{city} set as default");
        }
    }
}
