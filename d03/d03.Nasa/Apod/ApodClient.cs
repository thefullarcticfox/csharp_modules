using d03.Nasa.Apod.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace d03.Nasa.Apod
{
    public class ApodClient : ApiClientBase, INasaClient<int, Task<MediaOfToday[]>>
    {
        public ApodClient(string apiKey) : base(apiKey) {}

        public async Task<MediaOfToday[]> GetAsync(int count)
        {
            var res = await HttpGetAsync<MediaOfToday[]>
                ($"https://api.nasa.gov/planetary/apod?api_key={_apiKey}&count={count}");
            return res;
        }
    }
}
