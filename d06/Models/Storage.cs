using System.Threading;

namespace d06.Models
{
    public class Storage
    {
        private int _itemsInStorage;

        public int ItemsInStorage
        {
            get => Interlocked.CompareExchange(ref _itemsInStorage, 0, 0);
            set => Interlocked.Exchange(ref _itemsInStorage, value);
        }

        public bool IsEmpty => ItemsInStorage <= 0;

        public Storage(int totalItemCount) => ItemsInStorage = totalItemCount;

        /*
            private const int Unlocked = 0;
            private const int Locked = 1;
            private int _locked = Unlocked;
            public void Take(int itemCount)
            {
                while (Interlocked.Exchange(ref _locked, Locked) != Unlocked)
                    Console.WriteLine($"Thread#{Thread.CurrentThread.ManagedThreadId} was denied the lock");

                Console.WriteLine($"Thread#{Thread.CurrentThread.ManagedThreadId} acquired the lock");
                ItemsInStorage -= itemCount;

                // Release the lock
                Interlocked.Exchange(ref _locked, Unlocked);
                Console.WriteLine($"Thread#{Thread.CurrentThread.ManagedThreadId} released the lock");
            }
        */
    }
}
