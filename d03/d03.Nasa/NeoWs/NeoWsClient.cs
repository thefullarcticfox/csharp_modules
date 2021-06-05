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

        public Task<AsteroidLookup[]> GetAsync(AsteroidRequest request)
        {
            throw new NotImplementedException();
        }
    }

    public class ApiResponse
    {
        [JsonPropertyName("near_earth_objects")]
        public Dictionary<DateTime, List<AsteroidInfo>> NearEarthObjects { get; set; }
    }
}
