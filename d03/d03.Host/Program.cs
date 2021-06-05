using System;
using System.Globalization;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using d03.Nasa.Apod;
using d03.Nasa.Apod.Models;
using d03.Nasa.NeoWs;
using d03.Nasa.NeoWs.Models;

namespace d03.Host
{
    internal static class Program
    {
        private const string ConfigFile = "appsettings.json";
        private static IConfiguration _config;

        private static void ThrowOnInvalidInput() =>
            throw new ArgumentException(
                "Invalid arguments. Expected: {API} {count}\n" +
                "- API is \"apod\" or \"neows\"\n- count is an integer");

        public static async Task Main(string[] args)
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-GB", false);

            try
            {
                if (args.Length < 1 || (args[0] != "apod" && args[0] != "neows"))
                    ThrowOnInvalidInput();

                _config = new ConfigurationBuilder()
                    .AddJsonFile(ConfigFile)
                    .Build();
                string apiKey = _config["ApiKey"];

                if (args[0] == "apod")
                {
                    if (args.Length < 2)
                        ThrowOnInvalidInput();
                    if (!int.TryParse(args[1], out int count))
                        ThrowOnInvalidInput();
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

                    AsteroidRequest request;
                    if (args.Length < 2 || !int.TryParse(args[1], out int count))
                        request = new AsteroidRequest(startDate, endDate);
                    else
                        request = new AsteroidRequest(startDate, endDate, count);

                    var nasaClient = new NeoWsClient(apiKey);
                    AsteroidLookup[] res = await nasaClient.GetAsync(request);

                    foreach (AsteroidLookup asteroid in res)
                        Console.WriteLine($"- {asteroid}{Environment.NewLine}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
