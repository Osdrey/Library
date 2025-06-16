using Library.Application.DTOs;

namespace Library.Presentation.UI.Printers
{
    internal static class ReservationPrinter
    {
        public static void Print(ReservationDTO reservation)
        {
            Console.WriteLine("\n══════════════════════════════════════════════════\n");
            Console.WriteLine($"ID de la reserva: {reservation.ReservationId}");
            Console.WriteLine($"ID del usuario: {reservation.UserId}");
            Console.WriteLine($"ID del material: {reservation.MaterialId}");
            Console.WriteLine($"Fecha de solicitud: {reservation.RequestDate:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"Fecha de expiración: {reservation.ExpirationDate:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"Estado de la reserva: {reservation.ReservationStatus}");
        }
    }
}
