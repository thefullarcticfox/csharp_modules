using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
            const int registerCount = 4;
            const int storageCapacity = 50;
            const int cartCapacity = 7;
            var customerCount = 10;

            double timePerItem;
            double timePerCustomer;
            try
            {
                IConfiguration config = new ConfigurationBuilder()
                    .AddJsonFile(ConfigFile)
                    .Build();
                timePerItem = double.Parse(config["timePerItem"]);
                timePerCustomer = double.Parse(config["timePerCustomer"]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            Customer[] customers = Enumerable.Range(1, customerCount)
                .Select(x => new Customer(x))
                .ToArray();

            var store = new Store(
                registerCount,
                storageCapacity,
                TimeSpan.FromSeconds(timePerItem),
                TimeSpan.FromSeconds(timePerCustomer));

            Console.WriteLine("Lines by people count:");
            Parallel.ForEach(customers, customer =>
            {
                customer.FillCart(cartCapacity);
                CashRegister register = customer.GetInLineByPeople(store.Registers);
                Console.WriteLine($"{customer} to {register}");
            });

            store.OpenRegisters();

            while (store.IsOpen)
            {
                Thread.Sleep(TimeSpan.FromSeconds(5));
                var customer = new Customer(++customerCount);
                customer.FillCart(cartCapacity);
                CashRegister register = customer.GetInLineByItems(store.Registers);
                Console.WriteLine($"{customer} to {register}");
            }

            // waiting for all threads to finish
            foreach (CashRegister reg in store.Registers)
                reg.Thread.Join();

            // and output the results
            Console.WriteLine("Register |        Load |   Mean Wait");
            foreach (CashRegister reg in store.Registers)
                Console.WriteLine($"#{reg.No,7} | {reg.TotalTime.TotalSeconds,10:N2}s | {reg.MeanWaitTime.TotalSeconds,10:N2}s");
        }
    }
}
