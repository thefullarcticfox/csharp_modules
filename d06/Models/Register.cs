using System.Collections.Generic;

namespace d06.Models
{
    public class Register
    {
        public int No { get; }
        public Queue<Customer> QueuedCustomers { get; }

        public Register(int number)
        {
            No = number;
            QueuedCustomers = new Queue<Customer>();
        }

        public override string ToString() =>
            $"Register#{No} ({QueuedCustomers.Count} customers in line)";
    }
}