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

ExchangeSum sumToExchange;
Exchanger exchanger;
try
{
    sumToExchange = new ExchangeSum(sum);
    exchanger = new Exchanger(ratesDirectory);
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
    return;
}

// foreach (ExchangeRate rate in exchanger.GetExchangeRates())
//     Console.WriteLine(rate.ToString());

// output
Console.WriteLine($"Sum in original currency: {sumToExchange.ToString()}");
foreach (ExchangeSum exchanged in exchanger.ExchangeToCurrencies(sumToExchange))
    Console.WriteLine($"Sum in {exchanged.Curr.ToString()}: {exchanged.ToString()}");
