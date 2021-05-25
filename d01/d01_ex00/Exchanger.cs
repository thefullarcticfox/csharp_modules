using System;
using System.IO;
using System.Collections.Generic;
using d01_ex00.Models;

namespace d01_ex00
{
    internal enum Currency
    {
        Eur,
        Rub,
        Usd
    }

    internal class Exchanger
    {
        private readonly List<ExchangeRate> _rates;

        public Exchanger(string ratesDirectory)
        {
            _rates = new List<ExchangeRate>();
            foreach (string path in Directory.GetFiles(ratesDirectory))
            {
                string fromCurrencyCode = Path.GetFileNameWithoutExtension(path);
                foreach (string rateStr in File.ReadAllLines(path))
                {
                    _rates.Add(new ExchangeRate(fromCurrencyCode, rateStr));
                }
            }
        }

        public IEnumerable<ExchangeRate> GetExchangeRates() => _rates;
    }
}
