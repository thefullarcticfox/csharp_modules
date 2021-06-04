using d03.Nasa.NeoWs.Models;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace d03.Nasa.NeoWs
{
    public class NeoWsClient : ApiClientBase, INasaClient<NeoWsRequest, Task<AsteroidLookup[]>>
    {
        public NeoWsClient(string apiKey) : base(apiKey) { }

        public Task<AsteroidLookup[]> GetAsync(NeoWsRequest input)
        {
            throw new NotImplementedException();
        }
    }

    public class NeoWsRequest
    {
    }
}
