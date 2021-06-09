using System;
using d04_ex03.Models;

namespace d04_ex03
{
    internal static class Program
    {
        public static void Main()
        {
            Console.WriteLine(typeof(IdentityUser));
            var user1 = TypeFactory.CreateWithConstructor<IdentityUser>();
            var user2 = TypeFactory.CreateWithActivator<IdentityUser>();
            Console.WriteLine($"{(user1 == user2 ? "user1 == user2" : "user1 != user2")}");

            Console.WriteLine(typeof(IdentityRole));
            var role1 = TypeFactory.CreateWithConstructor<IdentityRole>();
            var role2 = TypeFactory.CreateWithActivator<IdentityRole>();
            Console.WriteLine($"{(role1 == role2 ? "role1 == role2" : "role1 != role2")}");

            Console.WriteLine();
            Console.WriteLine(typeof(IdentityUser));
            Console.WriteLine("Set name:");
            object[] args = { Console.ReadLine() };
            var user = TypeFactory.CreateWithParameters<IdentityUser>(args);
            Console.WriteLine($"Username set: {user.UserName}");
        }
    }
}
