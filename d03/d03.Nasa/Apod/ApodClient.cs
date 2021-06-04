using d03.Nasa.Apod.Models;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace d03.Nasa.Apod
{
    public class ApodClient : ApiClientBase, INasaClient<int, Task<MediaOfToday[]>>
    {
        public ApodClient(string apiKey) : base(apiKey) {}

        public Task<MediaOfToday[]> GetAsync(int input)
        {
            throw new NotImplementedException();
        }
    }
}
