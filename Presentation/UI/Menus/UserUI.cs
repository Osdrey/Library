using Library.Application.Interfaces;
using Library.Presentation.Helpers;

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
                Console.Clear();
                Console.WriteLine(  "╔══════════════════════════════╗\n" +
                                    "║      GESTOR DE USUARIOS      ║\n" +
                                    "╚══════════════════════════════╝\n" +
                "\n¿Qué acción vas a realizar? Ingresa una de las opciones disponibles:\n");
                Console.WriteLine(
                    "1. Listar usuarios\n" +
                    "2. Buscar usuario\n" +
                    "3. Crear usuario\n" +
                    "4. Actualizar usuario\n" +
                    "5. Cambiar estado de usuario\n" +
                    "9. Volver al menú anterior\n" +
                    "0. Salir");

                var input = Console.ReadLine();
                Console.Clear();

                try
                {
                    switch (input)
                    {
                        case "1":
                            _userService.SearchAllUsers();
                            break;
                        case "2":
                            _userService.SearchUser();
                            break;
                        case "3":
                            _userService.CreateUser();
                            break;
                        case "4":
                            _userService.UpdateUser();
                            break;
                        case "5":
                            Console.WriteLine(
                                "¿Deseas aceptar o rechazar la reserva?\n" +
                                "1. Activar\n" +
                                "2. Desactivar");

                            var option = Console.ReadLine();
                            if (option == "1")
                            {
                                _userService.ReactivateUser();
                            }
                            else if (option == "2")
                            {
                                _userService.DeactivateUser();
                            }
                            else
                            {
                                Console.WriteLine("Ingresaste una opción inválida. Intenta de nuevo.");
                            }
                            break;
                        case "9":
                            Console.WriteLine("Regresando al menu anterior...");
                            Thread.Sleep(1000);
                            Console.Clear();
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
                catch (Exception ex)
                {
                    ExceptionHandler.UserHandle(ex);
                    Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }
    }
}