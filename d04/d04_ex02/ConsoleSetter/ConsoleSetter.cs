using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using d04_ex02.Attributes;

namespace d04_ex02.ConsoleSetter
{
    public static class ConsoleSetter
    {
        public static void SetValues<T>(T input) where T : class
        {
            Console.WriteLine($"Let's set {typeof(T).Name}");

            IEnumerable<PropertyInfo> requiredProps = typeof(T)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => !p.IsDefined(typeof(NoDisplayAttribute)));

            foreach (PropertyInfo property in requiredProps)
            {
                var descriptionAttr = (DescriptionAttribute)Attribute.GetCustomAttribute(property, typeof(DescriptionAttribute));

                Console.WriteLine($"Set {descriptionAttr?.Description ?? property.Name}:");
                string value = Console.ReadLine();

                if (string.IsNullOrEmpty(value))
                {
                    var defaultValueAttr = (DefaultValueAttribute)Attribute.GetCustomAttribute(property, typeof(DefaultValueAttribute));
                    property.SetValue(input, defaultValueAttr?.Value ?? string.Empty);
                    continue;
                }

                property.SetValue(input, value);
            }

            Console.WriteLine();
            Console.WriteLine("We've set our instance!");
            Console.WriteLine($"{input}");
        }
    }
}
