using System;

namespace d01_ex00.Models
{
    public readonly struct ExchangeSum
    {
        public readonly string Currency;
        public readonly double Sum;

        // data constructor
        public ExchangeSum(double sum, string currency)
        {
            Sum = sum;
            Currency = currency;
        }

        /* parsing constructor:
         * sum is a pair "{sum} {currency}" where:
         * - currency is "EUR", "RUB" or "USD"
         * - sum is a floating point number */
        public ExchangeSum(string sum)
        {
            string[] sumSplit = sum.Split(' ');
            if (sumSplit.Length < 2 || !double.TryParse(sumSplit[0].Replace(',', '.'), out Sum))
                throw new ArgumentException("Input error. Check input data and try again.");
            Currency = sumSplit[1].ToUpper();
        }

        public override string ToString() => $"{Sum:N2} {Currency}";
    }
}
