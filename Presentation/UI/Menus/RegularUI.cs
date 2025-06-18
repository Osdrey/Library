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
        private readonly UserDTO _loggedUser;
        public RegularUI(IMaterialService materialService, ILoanService loanService, IReservationService reservationService, IAuthService authService, UserDTO loggedUser)
        {
            _materialService = materialService;
            _loanService = loanService;
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
                    $"¡Hola {userDTO.FirstName}! Bienvenido al sistema de prestamos de la universidad\n" +
                    "\n¿En que podemos ayudarte? Elige una de las opciones disponibles:\n");
                Console.WriteLine(
                    "1. Consultar materiales disponibles\n" +
                    "2. Filtrar materiales (Titulo, Autor, Año)\n" +
                    "3. Reservar material\n" +
                    "4. Ver tus reservas\n" +
                    "5. Gestionar reserva\n" +
                    "6. Ver tus préstamos\n" +
                    "7. Gestionar préstamo\n" +
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
                        _reservationService.CreateReservation(_loggedUser);
                        break;
                    case "4":
                        _reservationService.ListUserReservations(_loggedUser);
                        break;
                    case "5":
                        Console.WriteLine(
                            "¿Deseas extender o cancelar una reserva?\n" +
                            "1. Extender\n" +
                            "2. Cancelar");

                        var optionR = Console.ReadLine();
                        if (optionR == "1")
                        {
                            _reservationService.ExtendReservation();
                        }
                        else if (optionR == "2")
                        {
                            _reservationService.CancelReservation();
                        }
                        else
                        {
                            Console.WriteLine("Ingresaste una opción inválida. Intenta de nuevo.");
                        }
                        Console.Clear();
                        break;
                    case "6":
                        _loanService.ListUserLoans(_loggedUser);
                        break;
                    case "7":
                        Console.WriteLine(
                            "¿Deseas extender o cancelar un préstamo?\n" +
                            "1. Extender\n" +
                            "2. Cancelar");

                        var optionL = Console.ReadLine();
                        if (optionL == "1")
                        {
                            _loanService.ExtendLoan();
                        }
                        else if (optionL == "2")
                        {
                            _loanService.CancelLoan();
                        }
                        else
                        {
                            Console.WriteLine("Ingresaste una opción inválida. Intenta de nuevo.");
                        }
                        Console.Clear();
                        break;
                    case "8":
                        var (current, newPass) = AuthInput.PasswordChange();
                        _authService.ChangePassword(_loggedUser, current, newPass);
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