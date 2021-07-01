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

        private readonly Store _store;
        private readonly Thread _thread;

        public CashRegister(Store store, int number, TimeSpan timePerItem, TimeSpan delay)
        {
            No = number;
            QueuedCustomers = new ConcurrentQueue<Customer>();
            TimePerItem = timePerItem;
            Delay = delay;
            TotalTime = new TimeSpan(0, 0, 0);
            _store = store;
            _thread = new Thread(ThreadedProcess)
            {
                Name = $"CashRegister#{No}"
            };
        }

        private void ThreadedProcess()
        {
            Console.WriteLine($"Thread#{Thread.CurrentThread.ManagedThreadId} (CashRegister#{No}) started");
            while (QueuedCustomers.Count > 0)
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                if (!QueuedCustomers.TryDequeue(out Customer customer))
                    throw new Exception("Could not retrieve customer from queue");

                for (var i = 0; i < customer.ItemsInCart; i++)
                    Thread.Sleep(TimePerItem);  // time to process item

                _store.Storage.ItemsInStorage -= (customer.ItemsInCart <= _store.Storage.ItemsInStorage
                    ? customer.ItemsInCart
                    : _store.Storage.ItemsInStorage);

                Console.WriteLine($"{customer} served by {this} in {stopwatch.Elapsed.TotalSeconds:N2}s");

                if (QueuedCustomers.Count > 0)
                    Thread.Sleep(Delay);        // delay between customers

                TotalTime += stopwatch.Elapsed;
                stopwatch.Stop();
            }

            Console.WriteLine($"Thread#{Thread.CurrentThread.ManagedThreadId} (CashRegister#{No}) finished in {TotalTime.TotalSeconds:N2}s");
            //Console.WriteLine(_store.Storage.ItemsInStorage);
        }

        public void Process()
        {
            _thread.Start();
        }

        public override string ToString() =>
            $"Register#{No} ({QueuedCustomers.Count} customers in line)";
    }
}
