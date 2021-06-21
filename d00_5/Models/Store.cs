using System;
using System.Collections.Generic;
using d00_5.Extensions;

namespace d00_5.Models
{
    public class Store
    {
        public List<CashRegister> CashRegisters { get; private set; }

        public Storage Storage { get; private set; }

        public bool IsOpen => !Storage.IsEmpty();

        public Store(int cashRegisters, int storageFill)
        {
            CashRegisters = new List<CashRegister>(cashRegisters);
            for (int i = 0; i < cashRegisters; i++)
                CashRegisters.Add(new CashRegister($"Cash register {i + 1}"));
            Storage = new Storage(storageFill);
        }

        public void WorkForCustomers(List<Customer> customers, bool chooseLeastProductsQueue = false)
        {
            int i = 0;
            while (IsOpen)
            {
                Customer customer = customers[i];
                Storage.ProductCount -= customer.FillCart(Storage.ProductCount < 7 ? Storage.ProductCount : 7);

                CashRegister cashRegister;
                if (chooseLeastProductsQueue)
                    cashRegister = customer.GetLeastProductsCashRegister(CashRegisters);
                else
                    cashRegister = customer.GetLeastQueueCashRegister(CashRegisters);

                Console.WriteLine($"{customer} chose {cashRegister}");

                cashRegister
                    .CustomerQueue
                    .Enqueue(customer);

                i = (i + 1) % customers.Count;
            }
        }
    }
}
