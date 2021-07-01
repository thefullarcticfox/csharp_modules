using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;

namespace d06.Models
{
    public class CashRegister
    {
        private int No { get; }
        public ConcurrentQueue<Customer> QueuedCustomers { get; }
        private TimeSpan TimePerItem { get; }
        private TimeSpan Delay { get; }
        private TimeSpan TotalTime { get; set; }
        public Thread Thread { get; }

        private readonly Store _store;

        public CashRegister(Store store, int number, TimeSpan timePerItem, TimeSpan delay)
        {
            No = number;
            QueuedCustomers = new ConcurrentQueue<Customer>();
            TimePerItem = timePerItem;
            Delay = delay;
            TotalTime = new TimeSpan(0, 0, 0);
            _store = store;
            Thread = new Thread(Process) { Name = $"CashRegister#{No}" };
        }

        public void Process()
        {
            Console.WriteLine($"Thread#{Thread.CurrentThread.ManagedThreadId} (CashRegister#{No}) started");
            while (!QueuedCustomers.IsEmpty && _store.IsOpen)
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                if (!QueuedCustomers.TryDequeue(out Customer customer))
                    throw new Exception("Could not retrieve customer from queue");

                Thread.Sleep(TimePerItem * customer.ItemsInCart);  // time to process item

                _store.Storage.ItemsInStorage -= (customer.ItemsInCart <= _store.Storage.ItemsInStorage
                    ? customer.ItemsInCart
                    : _store.Storage.ItemsInStorage);

                Console.WriteLine($"{customer} served by {this} in {stopwatch.Elapsed.TotalSeconds:N2}s");

                if (!QueuedCustomers.IsEmpty || !_store.IsOpen)
                    Thread.Sleep(Delay);        // delay between customers

                stopwatch.Stop();
                TotalTime += stopwatch.Elapsed;
            }

            Console.WriteLine($"Thread#{Thread.CurrentThread.ManagedThreadId} (CashRegister#{No}) finished in {TotalTime.TotalSeconds:N2}s");
            //Console.WriteLine(_store.Storage.ItemsInStorage);
        }

        public override string ToString() =>
            $"Register#{No} ({QueuedCustomers.Count} customers in line)";
    }
}
