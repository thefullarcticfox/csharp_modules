using System;
using System.Linq;
using d06.Extensions;
using d06.Models;
using Microsoft.Extensions.Configuration;

namespace d06
{
    internal static class Program
    {
        private const string ConfigFile = "appsettings.json";

        private static void Main()
        {
            const int registerCount = 3;
            const int storageCapacity = 40;
            const int cartCapacity = 7;
            const int customerCount = 10;

            int timePerItem;
            int delay;
            try
            {
                IConfiguration config = new ConfigurationBuilder()
                    .AddJsonFile(ConfigFile)
                    .Build();
                timePerItem = int.Parse(config["time_per_item"]);
                delay = int.Parse(config["delay_after"]);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            {
                Customer[] customers = Enumerable.Range(1, customerCount)
                    .Select(x => new Customer(x))
                    .ToArray();

                var shop = new Store(
                    registerCount,
                    storageCapacity,
                    TimeSpan.FromSeconds(timePerItem),
                    TimeSpan.FromSeconds(delay));

                Console.WriteLine("Lines by people count:");

                var i = 0;
                while (shop.IsOpen && i < customerCount)
                {
                    Customer customer = customers[i++];

                    customer.FillCart(cartCapacity);

                    if (customer.ItemsInCart <= shop.Storage.ItemsInStorage)
                        shop.Storage.ItemsInStorage -= customer.ItemsInCart;
                    else
                        shop.Storage.ItemsInStorage = 0;

                    CashRegister register = customer.GetInLineByPeople(shop.Registers);
                    Console.WriteLine($"{customer} to {register}");
                }
            }

            {
                Customer[] customers = Enumerable.Range(1, customerCount)
                    .Select(x => new Customer(x))
                    .ToArray();

                var shop = new Store(
                    registerCount,
                    storageCapacity,
                    TimeSpan.FromSeconds(timePerItem),
                    TimeSpan.FromSeconds(delay));

                Console.WriteLine("Lines by items count:");

                var i = 0;
                while (shop.IsOpen && i < customerCount)
                {
                    Customer customer = customers[i++];

                    customer.FillCart(cartCapacity);

                    if (customer.ItemsInCart <= shop.Storage.ItemsInStorage)
                        shop.Storage.ItemsInStorage -= customer.ItemsInCart;
                    else
                        shop.Storage.ItemsInStorage = 0;

                    CashRegister register = customer.GetInLineByItems(shop.Registers);
                    Console.WriteLine($"{customer} to {register}");
                }
            }
        }
    }
}
