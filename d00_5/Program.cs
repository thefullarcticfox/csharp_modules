using System;
using System.Linq;
using System.Collections.Generic;
using d00_5.Models;

namespace d00_5
{
    internal class Program
    {
        private static List<Customer> GetCustomers() =>
            new()
            {
                new Customer("Andrew", 1), new Customer("Axel", 2), new Customer("Bob", 3),
                new Customer("Duke", 4), new Customer("Eugene", 5), new Customer("Makoto", 6),
                new Customer("Otto", 7), new Customer("Spenser", 8), new Customer("Takemi", 9),
                new Customer("Zeke", 10)
            };

        private static void Main()
        {
            {
                var customer1 = new Customer("Andrew", 1);
                var customer2 = new Customer("Andrew", 1);
                Console.WriteLine(customer1 == customer2);
            }

            Console.WriteLine("--------------------");

            var customers = GetCustomers();
            var store = new Store(3, 40);
            store.WorkForCustomers(customers, false);

            Console.WriteLine("--------------------");

            customers = GetCustomers();
            store = new Store(3, 40);
            store.WorkForCustomers(customers, true);
        }
    }
}
