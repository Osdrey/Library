using Library.Application.Interfaces;

namespace Library.Presentation.UI.Menus
{
    internal class ReservationUI
    {
        private readonly IReservationService _reservationService;

        public ReservationUI(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        public void ShowMenu()
        {
            bool flagMenu = true;
            while (flagMenu)
            {
                Console.WriteLine(
                    "Bienvenido al gestor de reservas.\n" +
                    "¿Qué acción vas a realizar? Ingresa una de las opciones disponibles:");
                Console.WriteLine(
                    "1. Crear reserva\n" +
                    "2. Buscar reserva\n" +
                    "3. Extender reserva\n" +
                    "4. Cancelar reserva\n" +
                    "5. Aceptar reserva\n" +
                    "6. Rechazar reserva\n" +
                    "9. Volver al menú anterior\n" +
                    "0. Salir");

                var input = Console.ReadLine();
                Console.Clear();

                switch (input)
                {
                    case "1":
                        _reservationService.CreateReservation();
                        break;
                    case "2":
                        _reservationService.SearchReservation();
                        break;
                    case "3":
                        _reservationService.ExtendReservation();
                        break;
                    case "4":
                        _reservationService.CancelReservation();
                        break;
                    case "5":
                        _reservationService.AcceptReservation();
                        break;
                    case "6":
                        _reservationService.RejectReservation();
                        break;
                    case "9":
                        Console.WriteLine("Regresando al menú anterior...");
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
