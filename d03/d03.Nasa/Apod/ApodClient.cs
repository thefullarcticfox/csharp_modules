using d03.Nasa.Apod.Models;
using System.Threading.Tasks;

namespace d03.Nasa.Apod
{
    public class ApodClient : ApiClientBase, INasaClient<int, Task<MediaOfToday[]>>
    {
        private const string ApiUrl = "https://api.nasa.gov/planetary/apod";

        public ApodClient(string apiKey) : base(apiKey) { }

        public async Task<MediaOfToday[]> GetAsync(int count)
        {
            MediaOfToday[] res = await HttpGetAsync<MediaOfToday[]>(
                $"{ApiUrl}?count={count}&api_key={ApiKey}");
            return res;
        }
    }
}
