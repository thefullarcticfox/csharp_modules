using d03.Nasa.Apod.Models;
using System.Threading.Tasks;

namespace d03.Nasa.Apod
{
    public class ApodClient : ApiClientBase, INasaClient<int, Task<MediaOfToday[]>>
    {
        public ApodClient(string apiKey) : base(apiKey) {}

        public async Task<MediaOfToday[]> GetAsync(int count)
        {
            MediaOfToday[] res = await HttpGetAsync<MediaOfToday[]>
                ($"https://api.nasa.gov/planetary/apod?api_key={ApiKey}&count={count}");
            return res;
        }
    }
}
