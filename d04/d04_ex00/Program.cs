using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Http;

namespace d04_ex00
{
    class Program
    {
        static void Main()
        {
            Type type = typeof(DefaultHttpContext);
            Console.WriteLine($"Type: {type.FullName}");
            Console.WriteLine($"Assembly: {type.AssemblyQualifiedName}");
            Console.WriteLine($"Based on: {type.BaseType}");
            Console.WriteLine();

            Console.WriteLine($"Fields:");
            var flags =
                BindingFlags.DeclaredOnly |
                BindingFlags.Instance |
                BindingFlags.Static |
                BindingFlags.Public |
                BindingFlags.NonPublic;
            foreach (FieldInfo field in type.GetFields(flags))
                Console.WriteLine($"{field.FieldType} {field.Name}");
            Console.WriteLine();
            
            Console.WriteLine($"Properties:");
            foreach (PropertyInfo property in type.GetProperties())
                Console.WriteLine($"{property.PropertyType} {property.Name}");
            Console.WriteLine();
            
            Console.WriteLine($"Methods:");
            foreach (MethodInfo method in type.GetMethods(flags))
                Console.WriteLine($"{method.ReturnType.Name} {method.Name} " +
                    $"({string.Join(",", method.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}"))})");
        }
    }
}
