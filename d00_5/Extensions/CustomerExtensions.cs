using System.Collections.Generic;
using System.Linq;
using d00_5.Models;

namespace d00_5.Extensions
{
    static class CustomerExtensions
    {
        public static CashRegister GetLeastQueueCashRegister(this Customer customer, List<CashRegister> cashRegisters) =>
            cashRegisters
                .OrderBy(c => c.CustomerQueue.Count)
                .FirstOrDefault();

        public static CashRegister GetLeastProductsCashRegister(this Customer customer, List<CashRegister> cashRegisters) =>
            cashRegisters
                .OrderBy(c => c.CustomerQueue.Sum(c => c.CartProductCount))
                .FirstOrDefault();
    }
}
