using Library.Application.Interfaces;

namespace Library.Presentation.UI.Menus
{
    internal class UserUI
    {
        private readonly IUserService _userService;

        public UserUI(IUserService userService)
        {
            _userService = userService;
        }

        public void ShowMenu()
        {
            bool flagMenu = true;
            while (flagMenu)
            {
                Console.WriteLine(
                    "Bienvenido al gestor de usuarios.\n" +
                    "¿Qué acción vas a realizar? Ingresa una de las opciones disponibles:");
                Console.WriteLine(
                    "1. Crear usuario\n" +
                    "2. Buscar usuario\n" +
                    "3. Actualizar usuario\n" +
                    "4. Eliminar usuario\n" +
                    "9. Volver al menú anterior\n" +
                    "0. Salir");

                var input = Console.ReadLine();
                Console.Clear();

                switch (input)
                {
                    case "1":
                        _userService.CreateUser();
                        break;
                    case "2":
                        _userService.SearchUser();
                        break;
                    case "3":
                        _userService.UpdateUser();
                        break;
                    case "4":
                        _userService.DeleteUser();
                        break;
                    case "9":
                        Console.WriteLine("Regresando al menu anterior...");
                        flagMenu = false;
                        break;
                    case "0":
                        Console.WriteLine("Saliendo del sistema...");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Opción inválida. Intente de nuevo.");
                        break;
                }
            }
        }
    }
}
