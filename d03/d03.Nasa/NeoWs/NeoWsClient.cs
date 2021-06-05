using d03.Nasa.NeoWs.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace d03.Nasa.NeoWs
{
    public class NeoWsClient : ApiClientBase, INasaClient<AsteroidRequest, Task<AsteroidLookup[]>>
    {
        public NeoWsClient(string apiKey) : base(apiKey) {}

        public async Task<AsteroidLookup[]> GetAsync(AsteroidRequest request)
        {
            var response = await HttpGetAsync<ApiResponse>
                ($"https://api.nasa.gov/neo/rest/v1/feed?api_key={ApiKey}" +
                $"&start_date={request.StartDate:yyyy-MM-dd}&end_date={request.StartDate:yyyy-MM-dd}");
        }
    }

    public class ApiResponse
    {
        [JsonPropertyName("near_earth_objects")]
        public Dictionary<DateTime, List<AsteroidInfo>> NearEarthObjects { get; set; }
    }
}
