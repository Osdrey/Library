using Library.Application.DTOs;

namespace Library.Presentation.UI.Printers
{
    internal static class LoanPrinter
    {
        public static void Print(LoanDTO loan)
        {
            Console.WriteLine("\n----- Detalles del Préstamo -----\n");
            Console.WriteLine($"ID del préstamo: {loan.LoanId}");
            Console.WriteLine($"ID de la reserva: {loan.ReservationId}");
            Console.WriteLine($"ID del usuario: {loan.UserId}");
            Console.WriteLine($"Fecha de inicio: {loan.StartDate:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"Fecha de vencimiento: {loan.DueDate:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"Fecha de devolución: {(loan.ReturnDate == null ? "No devuelto" : loan.ReturnDate.Value.ToString("yyyy-MM-dd HH:mm:ss"))}");
            Console.WriteLine($"Estado del préstamo: {loan.LoanStatus}");
        }
    }
}