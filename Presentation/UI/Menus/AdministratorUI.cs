using Library.Application.DTOs;
using Library.Application.Interfaces;
using Library.Presentation.UI.Inputs;

namespace Library.Presentation.UI.Menus
{
    internal class AdministratorUI
    {
        private readonly MaterialUI _materialUI;
        private readonly LoanUI _loanUI;
        private readonly ReservationUI _reservationUI;
        private readonly UserUI _userUI;
        private readonly IAuthService _authService;
        private readonly UserDTO _loggedInUser;

        public AdministratorUI(MaterialUI materialUI, LoanUI loanUI, ReservationUI reservationUI, UserUI userUI,IAuthService authService, UserDTO loggedInUser)
        {
            _materialUI = materialUI;
            _loanUI = loanUI;
            _reservationUI = reservationUI;
            _userUI = userUI;
            _authService = authService;
            _loggedInUser = loggedInUser;
        }

        public void ShowMenu(UserDTO userDTO)
        {
            bool flagMenu = true;
            while (flagMenu)
            {
                Console.WriteLine(
                    $"¡Hola {userDTO.FirstName}! Bienvenido al sistema de prestamos de la universidad\n" +
                    "\n¿En que podemos ayudarte? Elige una de las opciones disponibles:\n");
                Console.WriteLine(
                    "1. Gestionar materiales\n" +
                    "2. Gestionar préstamos\n" +
                    "3. Gestionar reservas\n" +
                    "4. Gestionar usuarios\n" +
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
                        _loanUI.ShowMenu();
                        break;
                    case "3":
                        _reservationUI.ShowMenu();
                        break;
                    case "4":
                        _userUI.ShowMenu();
                        break;
                    case "5":
                        var (current, newPass) = AuthInput.PasswordChange();
                        _authService.ChangePassword(_loggedInUser, current, newPass);
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
                        Console.WriteLine("Opción inválida. Intente de nuevo.");
                        break;
                }
            }
        }
    }
}
