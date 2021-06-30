using System;
using System.Linq;
using d06.Extensions;
using d06.Models;

namespace d06
{
    internal static class Program
    {
        private static void Main()
        {
            const int registerCount = 3;
            const int storageCapacity = 40;
            const int cartCapacity = 7;
            const int customerCount = 10;

            {
                Customer[] customers = Enumerable.Range(1, customerCount)
                    .Select(x => new Customer(x))
                    .ToArray();

                var shop = new Store(registerCount, storageCapacity);

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

                    Register register = customer.GetInLineByPeople(shop.Registers);
                    Console.WriteLine($"{customer} to {register}");
                }
            }

            {
                Customer[] customers = Enumerable.Range(1, customerCount)
                    .Select(x => new Customer(x))
                    .ToArray();

                var shop = new Store(registerCount, storageCapacity);

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

                    Register register = customer.GetInLineByItems(shop.Registers);
                    Console.WriteLine($"{customer} to {register}");
                }
            }
        }
    }
}
