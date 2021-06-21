using System;
using System.Collections.Generic;

namespace d00_5.Models
{
    public class Customer
    {
        public string Name { get; private set; }

        public int Id { get; private set; }

        public int CartProductCount { get; private set; }

        public Customer(string name, int id)
        {
            Name = name;
            Id = id;
            CartProductCount = 0;
        }

        public int FillCart(int maxCartProducts)
        {
            var rnd = new Random();
            CartProductCount = rnd.Next(1, maxCartProducts + 1);
            return CartProductCount;
        }

        public override string ToString() => $"{Id} {Name} has {CartProductCount} products in cart";

        public override bool Equals(object obj) => obj is Customer customer && this == customer;

        public override int GetHashCode() => Tuple.Create(Name, Id).GetHashCode();

        public static bool operator==(Customer c1, Customer c2) => c1.Name == c2.Name && c1.Id == c2.Id;

        public static bool operator!=(Customer c1, Customer c2) => !(c1 == c2);
    }
}
