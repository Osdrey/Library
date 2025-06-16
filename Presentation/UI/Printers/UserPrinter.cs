using Library.Application.DTOs;

namespace Library.Presentation.UI.Printers
{
    public static class UserPrinter
    {
        public static void Print(UserDTO user)
        {
            Console.WriteLine("\n══════════════════════════════════════════════════\n");
            Console.WriteLine($"Cédula: {user.Document}");
            Console.WriteLine($"Nombre completo: {user.FirstName} {user.MiddleName} {user.LastName}");
            Console.WriteLine($"Edad: {user.Age}");
            Console.WriteLine($"Correo electrónico: {user.Email}");
            Console.WriteLine($"Nombre de usuario: {user.UserName}");
            Console.WriteLine($"Tipo de usuario: {user.UserType}");
            Console.WriteLine($"Rol del usuario: {user.UserRole}");
            Console.WriteLine($"Moras: {user.Arrears}");
            Console.WriteLine($"Activo: {(user.IsActive ? "Sí" : "No")}");
        }
    }
}
