using System;
using System.IO;
using System.Collections.Generic;
using d01_ex00.Models;

namespace d01_ex00
{
    public class Exchanger
    {
        private readonly List<string> _knownCurrencies;
        private readonly List<ExchangeRate> _rates;

        public Exchanger(string ratesDirectory)
        {
            _knownCurrencies = new List<string>();
            _rates = new List<ExchangeRate>();
            foreach (string path in Directory.GetFiles(ratesDirectory))
            {
                string fromCurrencyCode = Path.GetFileNameWithoutExtension(path);
                _knownCurrencies.Add(fromCurrencyCode);
                foreach (string rateStr in File.ReadAllLines(path))
                {
                    _rates.Add(new ExchangeRate(fromCurrencyCode, rateStr));
                }
            }
        }

        public IEnumerable<string> GetKnownCurrencies() => _knownCurrencies;
        public IEnumerable<ExchangeRate> GetExchangeRates() => _rates;

        private static ExchangeSum Exchange(ExchangeSum sum, ExchangeRate rate) =>
            new ExchangeSum(sum.Sum * rate.Rate, rate.ToCurrency);

        public IEnumerable<ExchangeSum> Exchange(ExchangeSum sum)
        {
            if (!_knownCurrencies.Contains(sum.Currency))
                throw new ArgumentException("Input error. Check input data and try again.");

            // select by fromCurrency and sort by ToCurrency
            List<ExchangeRate> selectedRates = _rates.FindAll(rate => sum.Currency == rate.FromCurrency);
            selectedRates.Sort((rate1, rate2) =>
                string.Compare(rate1.ToCurrency, rate2.ToCurrency, StringComparison.Ordinal));

            // do exchange
            foreach (ExchangeRate rate in selectedRates)
                yield return Exchange(sum, rate);
        }
    }
}
