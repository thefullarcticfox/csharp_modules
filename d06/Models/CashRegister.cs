using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace d06.Models
{
    public class CashRegister
    {
        public int No { get; }
        public Queue<Customer> QueuedCustomers { get; }
        public TimeSpan TimePerItem { get; }
        public TimeSpan Delay { get; }
        public TimeSpan TotalTime { get; private set; }

        private Thread _thread;

        public CashRegister(int number, TimeSpan timePerItem, TimeSpan delay)
        {
            No = number;
            QueuedCustomers = new Queue<Customer>();
            TimePerItem = timePerItem;
            Delay = delay;
            _thread = new Thread(new ThreadStart(ThreadedProcess))
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

                Customer current = QueuedCustomers.Dequeue();

                for (var j = 0; j < current.ItemsInCart; j++)
                    Thread.Sleep(TimePerItem);  // time to process item
                Console.WriteLine($"Customer#{current.No} served by CashRegister#{No} in {stopwatch.Elapsed.TotalSeconds:N2}s");

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
