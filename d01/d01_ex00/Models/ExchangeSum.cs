using System;

namespace d01_ex00.Models
{
    internal struct ExchangeSum
    {
        public Currency Curr;
        public double Sum;

        // from string in format SUM CURRENCY_ID
        public ExchangeSum(string sum)
        {
            string[] sumSplit = sum.Split(' ');
            if (sumSplit.Length < 2 || !double.TryParse(sumSplit[0], out Sum) ||
                !Enum.TryParse(sumSplit[1], true, out Curr))
                throw new ArgumentException("Input error. Check input data and try again.");
        }

        public ExchangeSum(double sum, Currency curr)
        {
            Sum = sum;
            Curr = curr;
        }

        public override string ToString()
        {
            return $"{Sum:N2} {Curr.ToString().ToUpper()}";
        }
    }
}
