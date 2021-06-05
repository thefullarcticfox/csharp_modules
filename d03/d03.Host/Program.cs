using System;
using System.Globalization;
using Microsoft.Extensions.Configuration;
using d03.Nasa.Apod;
using d03.Nasa.Apod.Models;
using System.Threading.Tasks;

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
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
