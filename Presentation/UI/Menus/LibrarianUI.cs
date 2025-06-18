using Library.Application.Interfaces;
using Library.Application.DTOs;
using Library.Presentation.UI.Inputs;

namespace Library.Presentation.UI.Menus
{
    internal class LibrarianUI
    {
        private readonly MaterialUI _materialUI;
        private readonly LoanUI _loanUI;
        private readonly IReservationService _reservationService;
        private readonly IAuthService _authService;
        private readonly UserDTO _loggedUser;

        public LibrarianUI(MaterialUI materialUI, LoanUI loanUI, IReservationService reservationService, IAuthService authService, UserDTO loggedUser)
        {
            _materialUI = materialUI;
            _loanUI = loanUI;
            _reservationService = reservationService;
            _authService = authService;
            _loggedUser = loggedUser;
        }

        public void ShowMenu(UserDTO userDTO)
        {
            bool flagMenu = true;
            while (flagMenu)
            {
                Console.Clear();
                Console.WriteLine(
                    $"¡Hola {userDTO.FirstName}! Bienvenido al sistema de préstamos de la universidad\n" +
                    "\n¿En qué podemos ayudarte? Elige una de las opciones disponibles:\n");
                Console.WriteLine(
                    "1. Gestionar materiales\n" +
                    "2. Gestionar préstamos\n" +
                    "3. Ver reservas pendientes\n" +
                    "4. Gestionar reserva\n" +
                    "5. Cambiar contraseña\n" +
                    "9. Cerrar sesión\n" +
                    "0. Salir\n");

                var input = Console.ReadLine();
                Console.Clear();

                switch (input)
                {
                    case "1":
                        _materialUI.ShowMenu();
                        break;
                    case "2":
                        _loanUI.ShowMenu(_loggedUser);
                        break;
                    case "3":
                        _reservationService.SearchReservation();
                        break;
                    case "4":
                        Console.WriteLine(
                            "¿Deseas aceptar o rechazar la reserva?\n" +
                            "1. Aceptar\n" +
                            "2. Rechazar");

                        var option = Console.ReadLine();
                        if (option == "1")
                        {
                            _reservationService.AcceptReservation();
                        }
                        else if (option == "2")
                        {
                            _reservationService.RejectReservation();
                        }
                        else
                        {
                            Console.WriteLine("Ingresaste una opción inválida. Intenta de nuevo.");
                        }
                        Console.Clear();
                        break;
                    case "5":
                        var (current, newPass) = AuthInput.PasswordChange();
                        _authService.ChangePassword(_loggedUser, current, newPass);
                        break;
                    case "9":
                        Console.WriteLine("Cerrando sesión...");
                        Thread.Sleep(1000);
                        Console.Clear();
                        flagMenu = false;
                        break;
                    case "0":
                        Console.WriteLine("Saliendo del sistema...");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Ingresaste una opción inválida. Intenta de nuevo.");
                        break;
                }
            }
        }
    }
}
