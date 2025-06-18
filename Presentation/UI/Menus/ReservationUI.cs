using Library.Application.DTOs;
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

        public void ShowMenu(UserDTO loggedUser)
        {
            bool flagMenu = true;
            while (flagMenu)
            {
                Console.Clear();
                Console.WriteLine(  "╔══════════════════════════════╗\n" +
                                    "║      GESTOR DE RESERVAS      ║\n" +
                                    "╚══════════════════════════════╝\n" +
                "\n¿Qué acción vas a realizar? Ingresa una de las opciones disponibles:\n");
                Console.WriteLine(
                    "1. Crear reserva\n" +
                    "2. Buscar reserva\n" +
                    "3. Extender reserva\n" +
                    "4. Aceptar reserva\n" +
                    "5. Cancelar reserva\n" +
                    "6. Rechazar reserva\n" +
                    "7. Listar reservas\n" +
                    "8. Filtrar reservas\n" +
                    "9. Volver al menú anterior\n" +
                    "0. Salir\n");

                var input = Console.ReadLine();
                Console.Clear();

                switch (input)
                {
                    case "1":
                        _reservationService.CreateReservation(loggedUser);
                        break;
                    case "2":
                        _reservationService.SearchReservation();
                        break;
                    case "3":
                        _reservationService.ExtendReservation();
                        break;
                    case "4":
                        _reservationService.AcceptReservation();
                        break;
                    case "5":
                        _reservationService.CancelReservation();
                        break;
                    case "6":
                        _reservationService.RejectReservation();
                        break;
                    case "7":
                        _reservationService.ListReservation();
                        break;
                    case "8":
                        Console.WriteLine(
                            "¿Deseas buscar por reservas pendientes o por usuario?\n" +
                            "1. Ver reservas pendientes\n" +
                            "2. Buscar por usuario");

                        var option = Console.ReadLine();
                        if (option == "1")
                        {
                            _reservationService.ListPendingReservation();
                        }
                        else if (option == "2")
                        {
                            _reservationService.ListUserReservations(loggedUser);
                        }
                        else
                        {
                            Console.WriteLine("Ingresaste una opción inválida. Intenta de nuevo.");
                        }
                        Console.Clear();
                        break;
                    case "9":
                        Console.WriteLine("Regresando al menú anterior...");
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
