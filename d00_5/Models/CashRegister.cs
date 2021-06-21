using System;
using System.Collections.Generic;
using System.Linq;

namespace d00_5.Models
{
    public class CashRegister
    {
        public string Title { get; private set; }

        public Queue<Customer> CustomerQueue { get; private set; }

        public CashRegister(string title)
        {
            Title = title;
            CustomerQueue = new Queue<Customer>();
        }

        public override string ToString() =>
            $"\"{Title}\" with {CustomerQueue.Count} customers " +
            $"with {CustomerQueue.Sum(c => c.CartProductCount)} products in queue";

        public override bool Equals(object obj) => obj is CashRegister cr && this == cr;

        public override int GetHashCode() => Tuple.Create(Title).GetHashCode();

        public static bool operator ==(CashRegister c1, CashRegister c2) => c1.Title == c2.Title;

        public static bool operator !=(CashRegister c1, CashRegister c2) => !(c1 == c2);
    }
}
