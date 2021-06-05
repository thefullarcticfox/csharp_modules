using System;
using System.Globalization;
using Microsoft.Extensions.Configuration;
using d03.Nasa;
using d03.Nasa.Apod;
using d03.Nasa.Apod.Models;
using d03.Nasa.NeoWs;
using d03.Nasa.NeoWs.Models;
using System.Threading.Tasks;

namespace d03.Host
{
    class Program
    {
        private static readonly string _configFile = "appsettings.json";
        private static IConfiguration _config;

        public static async Task Main(string[] args)
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-GB", false);

            if (args.Length < 2 || (args[0] != "apod" && args[1] != "neows") ||
                !int.TryParse(args[1], out int count))
            {
                Console.WriteLine("Invalid arguments. Expected: {apod|neows} {count}");
                return;
            }

            try
            {
                _config = new ConfigurationBuilder()
                    .AddJsonFile(_configFile)
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
