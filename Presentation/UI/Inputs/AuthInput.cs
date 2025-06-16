using static Library.Application.Validations.ValidationHelper;

namespace Library.Presentation.UI.Inputs
{
    internal static class AuthInput
    {
        public static (string username, string password) LoginCredentials()
        {
            Console.WriteLine(  "╔══════════════════════════════╗\n" +
                                "║       INICIO DE SESIÓN       ║\n" +
                                "╚══════════════════════════════╝\n");
            Console.Write("Usuario: ");
            string username = Console.ReadLine()!;
            Console.Write("Contraseña: ");
            string password = Console.ReadLine()!;
            Console.Clear();
            return (username, password);
        }

        public static (string current, string newPass) PasswordChange()
        {
            Console.WriteLine(  "╔══════════════════════════════════╗\n" +
                                "║       CAMBIO DE CONTRASEÑA       ║\n" +
                                "╚══════════════════════════════════╝\n");
            Console.Write("Contraseña actual: ");
            string current = Console.ReadLine()!;
            Console.Write("Nueva contraseña: ");
            string newPass = Validations.PasswordValidate(Console.ReadLine());
            Console.Clear();
            return (current, newPass);
        }
    }
}
