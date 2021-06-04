using System.Threading.Tasks;

namespace d03.Nasa
{
    public abstract class ApiClientBase
    {
        protected readonly string _apiKey;

        protected ApiClientBase(string apiKey)
        {
            _apiKey = apiKey;
        }

        protected Task HttpGetAsync<T>(string url)
        {
            return null;
        }
    }
}
