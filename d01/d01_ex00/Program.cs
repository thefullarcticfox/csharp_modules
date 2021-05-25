using System;
using d01_ex00;
using d01_ex00.Models;

if (args.Length < 2)
{
    Console.WriteLine("Input error. Check input data and try again.");
    return;
}

string sum = args[0];
string ratesDirectory = args[1];

var exchangeSum = new ExchangeSum(sum);
Console.WriteLine(exchangeSum.ToString());

Exchanger exchanger;
try
{
    exchanger = new Exchanger(ratesDirectory);
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
    return;
}

foreach (ExchangeRate rate in exchanger.GetExchangeRates())
{
    Console.WriteLine(rate.ToString());
}
