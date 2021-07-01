using System;
using System.Collections.Generic;
using System.Linq;

namespace d06.Models
{
    public class Store
    {
        public List<CashRegister> Registers { get; }
        public Storage Storage { get; }

        public bool IsOpen => !Storage.IsEmpty;

        public Store(int registerCount, int storageCapacity, TimeSpan timePerItem, TimeSpan delay)
        {
            Storage = new Storage(storageCapacity);
            Registers = Enumerable.Range(1, registerCount)
                .Select(i => new CashRegister(this, i, timePerItem, delay))
                .ToList();
        }

        public void OpenRegisters()
        {
            foreach (CashRegister register in Registers)
                register.Thread.Start();
        }
    }
}
