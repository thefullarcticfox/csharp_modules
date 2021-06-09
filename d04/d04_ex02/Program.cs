using d04_ex02.Models;
using System;

namespace d04_ex02
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0 && args[0] == "role")
            {
                var idRole = new IdentityUser();
                ConsoleSetter.ConsoleSetter.SetValues(idRole);
                return;
            }

            var idUser = new IdentityUser();
            ConsoleSetter.ConsoleSetter.SetValues(idUser);
        }
    }
}
