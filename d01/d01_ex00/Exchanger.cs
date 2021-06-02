using System;
using System.IO;
using System.Collections.Generic;
using d01_ex00.Models;

namespace d01_ex00
{
    public class Exchanger
    {
        private List<string> KnownCurrencies { get; }
        private List<ExchangeRate> Rates { get; }

        public Exchanger(string ratesDirectory)
        {
            KnownCurrencies = new List<string>();
            Rates = new List<ExchangeRate>();
            foreach (string path in Directory.GetFiles(ratesDirectory))
            {
                string fromCurrencyCode = Path.GetFileNameWithoutExtension(path);
                KnownCurrencies.Add(fromCurrencyCode);
                foreach (string rateStr in File.ReadAllLines(path))
                {
                    Rates.Add(new ExchangeRate(fromCurrencyCode, rateStr));
                }
            }
        }

        private static ExchangeSum Exchange(ExchangeSum sum, ExchangeRate rate) =>
            new(sum.Sum * rate.Rate, rate.ToCurrency);

        /* ExchangeIterator is an iterator method (yield return)
         * it won't execute until returned sequence is consumed by foreach or LINQ
         * that's why i return iterator method after checking known currency in Exchange */
        private IEnumerable<ExchangeSum> ExchangeIterator(ExchangeSum sum)
        {
            foreach (ExchangeRate rate in Rates)
                if (rate.FromCurrency == sum.Currency)
                    yield return Exchange(sum, rate);
        }

        public IEnumerable<ExchangeSum> Exchange(ExchangeSum sum)
        {
            if (!KnownCurrencies.Contains(sum.Currency))
                throw new ArgumentException("Unknown currency");
            return ExchangeIterator(sum);
        }
    }
}
