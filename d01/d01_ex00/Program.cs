using System;
using System.Collections.Generic;
using System.Globalization;
using d01_ex00;
using d01_ex00.Models;

CultureInfo.CurrentCulture = new CultureInfo("en-US", false);

if (args.Length < 2)
{
    Console.WriteLine("Input error. Check input data and try again.");
    return;
}

string sum = args[0];
string ratesDirectory = args[1];

try
{
    var sumToExchange = new ExchangeSum(sum);
    var exchanger = new Exchanger(ratesDirectory);

    // output
    IEnumerable<ExchangeSum> result = exchanger.Exchange(sumToExchange);
    Console.WriteLine($"Sum in original currency: {sumToExchange.ToString()}");
    foreach (ExchangeSum exchanged in result)
    {
        Console.WriteLine($"Sum in {exchanged.Currency}: {exchanged.ToString()}");
    }
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}
