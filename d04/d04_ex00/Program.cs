using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Http;

namespace d04_ex00
{
    internal static class Program
    {
        public static void Main()
        {
            Type type = typeof(DefaultHttpContext);
            Console.WriteLine($"Type: {type.FullName}");
            Console.WriteLine($"Assembly: {type.AssemblyQualifiedName}");
            Console.WriteLine($"Based on: {type.BaseType}");
            Console.WriteLine();

            const BindingFlags fieldFlags = BindingFlags.Instance |
                                            BindingFlags.Static |
                                            BindingFlags.Public |
                                            BindingFlags.NonPublic;
            const BindingFlags otherFlags = BindingFlags.Instance |
                                            BindingFlags.Static |
                                            BindingFlags.Public;

            Console.WriteLine("Fields:");
            foreach (FieldInfo field in type.GetFields(fieldFlags))
                Console.WriteLine($"{field.FieldType} {field.Name}");
            Console.WriteLine();

            Console.WriteLine("Properties:");
            foreach (PropertyInfo property in type.GetProperties(otherFlags))
                Console.WriteLine($"{property.PropertyType} {property.Name}");
            Console.WriteLine();

            Console.WriteLine("Methods:");
            foreach (MethodInfo method in type.GetMethods(otherFlags))
            {
                string arguments = string.Join(", ",
                    method.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}"));
                Console.WriteLine($"{method.ReturnType.Name} {method.Name} ({arguments})");
            }
        }
    }
}
