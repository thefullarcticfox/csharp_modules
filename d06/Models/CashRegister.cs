using System;
using System.Collections.Generic;

namespace d06.Models
{
    public class CashRegister
    {
        public int No { get; }
        public Queue<Customer> QueuedCustomers { get; }
        public TimeSpan TimePerItem { get; }
        public TimeSpan Delay { get; }

        public CashRegister(int number, TimeSpan timePerItem, TimeSpan delay)
        {
            No = number;
            QueuedCustomers = new Queue<Customer>();
            TimePerItem = timePerItem;
            Delay = delay;
        }

        public override string ToString() =>
            $"Register#{No} ({QueuedCustomers.Count} customers in line)";
    }
}
