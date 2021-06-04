using System;
using Microsoft.Extensions.Configuration.Json;
using d03.Nasa;
using d03.Nasa.Apod;
using d03.Nasa.NeoWs;

namespace d03.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Wrong number of arguments");
                return;
            }
            ApiClientBase nasaClient;
            nasaClient = new ApodClient("DEMO_KEY");
            nasaClient = new NeoWsClient("DEMO_KEY");
        }
    }
}
