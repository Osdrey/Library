using Library.Application.Interfaces;
using Library.Application.DTOs;
using Library.Presentation.UI.Inputs;

namespace Library.Presentation.UI.Menus
{
    internal class RegularUI
    {
        private readonly IMaterialService _materialService;
        private readonly ILoanService _loanService;
        private readonly IReservationService _reservationService;
        private readonly IAuthService _authService;
        private readonly UserDTO _loggedInUser;
        public RegularUI(IMaterialService materialService, ILoanService loanService, IReservationService reservationService, IAuthService authService, UserDTO loggedInUser)
        {
            _materialService = materialService;
            _loanService = loanService;
            _reservationService = reservationService;
            _authService = authService;
            _loggedInUser = loggedInUser;
        }

        public void ShowMenu(UserDTO userDTO)
        {
            bool flagMenu = true;
            while (flagMenu)
            {
                Console.WriteLine(
                    $"¡Hola {userDTO.FirstName}! Bienvenido al sistema de prestamos de la universidad\n"+
                    "\n¿En que podemos ayudarte? Elige una de las opciones disponibles:\n");
                Console.WriteLine(
                    "1. Consultar materiales disponibles\n" +
                    "2. Filtrar materiales (Titulo, Autor, Año)\n" +
                    "3. Reservar material\n" +
                    "4. Ver tus reservas\n" +
                    "5. Extender reserva\n" +
                    "6. Ver tus préstamos\n" +
                    "7. Renovar préstamo\n" +
                    "8. Cambiar contraseña\n" +
                    "9. Cerrar sesión\n" +
                    "0. Salir\n");

                var input = Console.ReadLine();
                Console.Clear();

                switch (input)
                {
                    case "1":
                        _materialService.ViewAvailableMaterials();
                        break;
                    case "2":
                        _materialService.SearchAllMaterials();
                        break;
                    case "3":
                        _reservationService.CreateReservation();
                        break;
                    case "4":
                        _reservationService.SearchReservation();
                        break;
                    case "5":
                        _reservationService.ExtendReservation();
                        break;
                    case "6":
                        _loanService.SearchLoan();
                        break;
                    case "7":
                        _loanService.ExtendLoan();
                        break;
                    case "8":
                        var (current, newPass) = AuthInput.PasswordChange();
                        _authService.ChangePassword(_loggedInUser, current, newPass);
                        break;
                    case "9":
                        Console.WriteLine("Cerrando sesión...");
                        Thread.Sleep(3000);
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
