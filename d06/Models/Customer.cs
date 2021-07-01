using System;

namespace d06.Models
{
    public class Customer
    {
        public int No { get; }
        public int ItemsInCart { get; private set; }

        public Customer(int number) => No = number;

        public void FillCart(int cartCapacity) =>
            ItemsInCart = new Random().Next(1, cartCapacity);

        public override string ToString() =>
            $"Customer#{No} ({ItemsInCart} items in cart)";
    }
}
