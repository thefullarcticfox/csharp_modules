using System;
using System.Globalization;
using Microsoft.Extensions.Configuration;
using d03.Nasa.Apod;
using d03.Nasa.Apod.Models;
using System.Threading.Tasks;
using d03.Nasa.NeoWs;
using d03.Nasa.NeoWs.Models;

namespace d03.Host
{
    internal static class Program
    {
        private const string ConfigFile = "appsettings.json";
        private static IConfiguration _config;

        public static async Task Main(string[] args)
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-GB", false);

            if (args.Length < 2 || (args[0] != "apod" && args[0] != "neows") ||
                !int.TryParse(args[1], out int count))
            {
                Console.WriteLine("Invalid arguments. Expected: {API} {count}");
                Console.WriteLine("- API is \"apod\" or \"neows\"\n- count is an integer");
                return;
            }

            try
            {
                _config = new ConfigurationBuilder()
                    .AddJsonFile(ConfigFile)
                    .Build();
                string apiKey = _config["ApiKey"];

                if (args[0] == "apod")
                {
                    var nasaClient = new ApodClient(apiKey);
                    MediaOfToday[] res = await nasaClient.GetAsync(count);
                    foreach (MediaOfToday media in res)
                        Console.WriteLine($"{media}{Environment.NewLine}");
                }
                else
                {
                    if (!DateTime.TryParse(_config["NeoWs:StartDate"], out DateTime startDate) ||
                        !DateTime.TryParse(_config["NeoWs:EndDate"], out DateTime endDate))
                        throw new ArgumentException($"Invalid dates in {ConfigFile}");
                    var request = new AsteroidRequest(startDate, endDate);
                    var nasaClient = new NeoWsClient(apiKey);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
