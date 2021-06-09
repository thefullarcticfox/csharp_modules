using Microsoft.AspNetCore.Http;
using System;
using System.Reflection;

namespace d04_ex01
{
    internal static class Program
    {
        public static void Main()
        {
            var ctx = new DefaultHttpContext();
            Console.WriteLine($"Old Response value: {ctx.Response}");
            ctx.GetType()
                .GetField("_response", BindingFlags.Instance | BindingFlags.NonPublic)?
                .SetValue(ctx, null);
            Console.WriteLine($"New Response value: {ctx.Response}");
        }
    }
}
