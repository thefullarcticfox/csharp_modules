using System;
using System.Collections.Generic;
using System.Globalization;
using d01_ex00;
using d01_ex00.Models;

CultureInfo.CurrentCulture = new CultureInfo("en-US", false);

try
{
    string sum = args[0];
    string ratesDirectory = args[1];
    var sumToExchange = new ExchangeSum(sum);
    var exchanger = new Exchanger(ratesDirectory);

    IEnumerable<ExchangeSum> result = exchanger.Exchange(sumToExchange);

    // output
    Console.WriteLine($"Sum in original currency: {sumToExchange}");
    foreach (ExchangeSum exchanged in result)
        Console.WriteLine($"Sum in {exchanged.Currency}: {exchanged}");
}
catch
{
    Console.WriteLine("Input error. Check input data and try again.");
}
