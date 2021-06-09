using d04_ex02.Models;

namespace d04_ex02
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var idUser = new IdentityUser();
            ConsoleSetter.ConsoleSetter.SetValues(idUser);

            // var idRole = new IdentityRole();
            // ConsoleSetter.ConsoleSetter.SetValues(idRole);
        }
    }
}
