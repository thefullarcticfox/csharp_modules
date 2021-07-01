using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace d06.Models
{
    public class CashRegister
    {
        private int No { get; }
        public Queue<Customer> QueuedCustomers { get; }
        private TimeSpan TimePerItem { get; }
        private TimeSpan Delay { get; }
        private TimeSpan TotalTime { get; set; }

        private readonly Store _store;
        private readonly Thread _thread;

        public CashRegister(Store store, int number, TimeSpan timePerItem, TimeSpan delay)
        {
            No = number;
            QueuedCustomers = new Queue<Customer>();
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
            for (var i = 0; i < QueuedCustomers.Count; i++)
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                Customer customer = QueuedCustomers.Dequeue();

                for (var j = 0; j < customer.ItemsInCart; j++)
                    Thread.Sleep(TimePerItem);  // time to process item

                _store.Storage.ItemsInStorage -= (customer.ItemsInCart <= _store.Storage.ItemsInStorage
                    ? customer.ItemsInCart
                    : _store.Storage.ItemsInStorage);

                Console.WriteLine($"Customer#{customer.No} served by CashRegister#{No} in {stopwatch.Elapsed.TotalSeconds:N2}s");

                if (QueuedCustomers.Count > 0)
                {
                    // Console.WriteLine($"Next customer waits {Delay} in CashRegister#{No}");
                    Thread.Sleep(Delay);        // delay between customers
                }

                TotalTime += stopwatch.Elapsed;
                stopwatch.Stop();
            }

            Console.WriteLine($"Thread#{Thread.CurrentThread.ManagedThreadId} (CashRegister#{No}) finished in {TotalTime.TotalSeconds:N2}s");
        }

        public void Process()
        {
            _thread.Start();
        }

        public override string ToString() =>
            $"Register#{No} ({QueuedCustomers.Count} customers in line)";
    }
}
