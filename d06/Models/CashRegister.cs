using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;

namespace d06.Models
{
    public class CashRegister
    {
        public int No { get; }
        public ConcurrentQueue<Customer> QueuedCustomers { get; }
        private TimeSpan TimePerItem { get; }
        private TimeSpan TimePerCustomer { get; }
        public TimeSpan TotalTime { get; private set; }
        public int TotalCustomers { get; private set; }
        public TimeSpan MeanWaitTime => TimeSpan.FromSeconds(TotalTime.TotalSeconds / TotalCustomers);
        public Thread Thread { get; }

        private readonly Store _store;

        public CashRegister(Store store, int number, TimeSpan timePerItem, TimeSpan timePerCustomer)
        {
            No = number;
            QueuedCustomers = new ConcurrentQueue<Customer>();
            TimePerItem = timePerItem;
            TimePerCustomer = timePerCustomer;
            TotalTime = new TimeSpan(0, 0, 0);
            _store = store;
            Thread = new Thread(Process) { Name = $"CashRegister#{No}" };

            // bonus
            /*var rnd = new Random();
            TimePerItem = TimeSpan.FromSeconds(rnd.NextDouble() * Math.Abs(timePerItem.TotalSeconds - 1.0) + 1.0);
            TimePerCustomer = TimeSpan.FromSeconds(rnd.NextDouble() * Math.Abs(timePerCustomer.TotalSeconds - 1.0) + 1.0);*/
        }

        private void Process()
        {
            Console.WriteLine($"{this} started in Thread#{Thread.CurrentThread.ManagedThreadId}");
            while (_store.IsOpen)
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                if (!QueuedCustomers.TryDequeue(out Customer customer))
                    continue;

                Thread.Sleep(TimePerItem * customer.ItemsInCart);   // time to process an item

                _store.Storage.ItemsInStorage -= (customer.ItemsInCart <= _store.Storage.ItemsInStorage
                    ? customer.ItemsInCart
                    : _store.Storage.ItemsInStorage);

                Console.WriteLine($"{customer} served by {this} in {stopwatch.Elapsed.TotalSeconds:N2}s");

                if (!_store.IsOpen)
                    Thread.Sleep(TimePerCustomer);                  // delay between customers

                stopwatch.Stop();
                TotalTime += stopwatch.Elapsed;
                TotalCustomers++;
            }
        }

        public override string ToString() =>
            $"Register#{No} ({QueuedCustomers.Count} customers in line)";
    }
}
