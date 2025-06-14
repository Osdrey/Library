using Library.Application.Interfaces;
using Library.Application.DTOs;

namespace Library.Presentation.UI.Menus
{
    internal class RegularUI
    {
        private readonly IMaterialService _materialService;
        private readonly IReservationService _reservationService;
        private readonly ILoanService _loanService;
        public RegularUI(IMaterialService materialService, ILoanService loanService, IReservationService reservationService)
        {
            _materialService = materialService;
            _loanService = loanService;
            _reservationService = reservationService;
        }

        public void ShowMenu(UserDTO userDTO)
        {
            bool flagMenu = true;
            while (flagMenu)
            {
                Console.WriteLine(
                    $"¡Hola {userDTO.FirstName}! Bienvenido al sistema de prestamos de la universidad\n"+
                    "¿En que podemos ayudarte? Elige una de las opciones disponibles:");
                Console.WriteLine(
                    "1. Consultar materiales disponibles\n" +
                    "2. Reservar material\n" +
                    "3. Ver tus reservas\n" +
                    "4. Extender reserva\n" +
                    "5. Ver tus préstamos\n" +
                    "6. Renovar préstamo\n" +
                    "9. Volver al menú anterior\n" +
                    "0. Salir");

                var input = Console.ReadLine();
                Console.Clear();

                switch (input)
                {
                    case "1":
                        _materialService.ViewAvaraibleMaterials();
                        break;
                    case "2":
                        _reservationService.CreateReservation();
                        break;
                    case "3":
                        _reservationService.SearchReservation();
                        break;
                    case "4":
                        _reservationService.ExtendReservation();
                        break;
                    case "5":
                        _loanService.SearchLoan();
                        break;
                    case "6":
                        _loanService.ExtendLoan();
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
