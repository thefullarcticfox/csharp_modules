using System;

namespace d01_ex00.Models
{
    internal struct ExchangeRate
    {
        Currency FromCurrency;
        Currency ToCurrency;
        double Rate;

        public ExchangeRate(string fromCurCode, string toCurCodeRatePair)
        {
            string[] codeRate = toCurCodeRatePair.Split(':');
            if (codeRate.Length < 2 || !Enum.TryParse(fromCurCode, true, out FromCurrency) ||
                !Enum.TryParse(codeRate[0], true, out ToCurrency) ||
                !double.TryParse(codeRate[1].Replace(',', '.'), out Rate))
                throw new ArgumentException("Input error. Check input data and try again.");
        }

        public ExchangeRate(Currency fromCur, Currency toCurrency, double rate)
        {
            this.FromCurrency = fromCur;
            ToCurrency = toCurrency;
            Rate = rate;
        }

        public override string ToString()
        {
            return $"{FromCurrency.ToString().ToUpper()} to {ToCurrency.ToString().ToUpper()} is {Rate}";
        }
    }
}
