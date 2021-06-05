using d03.Nasa.NeoWs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace d03.Nasa.NeoWs
{
    public class NeoWsClient : ApiClientBase, INasaClient<AsteroidRequest, Task<AsteroidLookup[]>>
    {
        private const string ApiUrl = "https://api.nasa.gov/neo/rest/v1";

        public NeoWsClient(string apiKey) : base(apiKey) { }

        public async Task<AsteroidLookup[]> GetAsync(AsteroidRequest request)
        {
            var response = await HttpGetAsync<ApiResponse>(
                $"{ApiUrl}/feed?api_key={ApiKey}" +
                $"&start_date={request.StartDate:yyyy-MM-dd}&end_date={request.EndDate:yyyy-MM-dd}");

            List<AsteroidInfo> asteroids = response.NearEarthObjects
                .SelectMany(asteroid => asteroid.Value)
                .OrderBy(asteroid => asteroid.Kilometers)
                .Take(request.ResultCount)
                .ToList();

            return await Task.WhenAll(asteroids.Select(asteroid =>
                HttpGetAsync<AsteroidLookup>($"{ApiUrl}/neo/{asteroid.Id}?api_key={ApiKey}")));
        }
    }

    public class ApiResponse
    {
        [JsonPropertyName("near_earth_objects")]
        public Dictionary<DateTime, List<AsteroidInfo>> NearEarthObjects { get; set; }
    }
}
