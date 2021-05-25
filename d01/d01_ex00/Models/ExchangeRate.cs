using System;

namespace d01_ex00.Models
{
    public readonly struct ExchangeRate
    {
        public readonly string FromCurrency;
        public readonly string ToCurrency;
        public readonly double Rate;

        // data constructor
        public ExchangeRate(string fromCurrency, string toCurrency, double rate)
        {
            FromCurrency = fromCurrency;
            ToCurrency = toCurrency;
            Rate = rate;
        }

        /* parsing constructor:
         * fromCurCode is "EUR", "RUB" or "USD"
         * toCurCodeRatePair is a pair "{toCurrency}:{rate}" where:
         * - toCurrency is "EUR", "RUB" or "USD"
         * - rate is a floating point number */
        public ExchangeRate(string fromCurCode, string toCurCodeRatePair)
        {
            string[] codeRate = toCurCodeRatePair.Split(':');
            if (codeRate.Length < 2 || !double.TryParse(codeRate[1].Replace(',', '.'), out Rate))
                throw new ArgumentException("Input error. Check input data and try again.");
            FromCurrency = fromCurCode.ToUpper();
            ToCurrency = codeRate[0].ToUpper();
        }

        public override string ToString() => $"{FromCurrency} to {ToCurrency} is {Rate}";
    }
}
