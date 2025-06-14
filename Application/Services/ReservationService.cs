using Library.Application.Interfaces;

namespace Library.Application.Services
{
    internal class ReservationService : IReservationService
    {
        public void CreateReservation()
        {
            Console.WriteLine("Creando reserva...");
        }
        public void SearchReservation()
        {
            Console.WriteLine("Buscando reserva...");
        }
        public void ExtendReservation()
        {
            Console.WriteLine("Extendiendo reserva...");
        }
        public void CancelReservation()
        {
            Console.WriteLine("Cancelando reserva...");
        }
        public void AcceptReservation()
        {
            Console.WriteLine("Aceptando reserva...");
        }
        public void RejectReservation()
        {
            Console.WriteLine("Rechazando reserva...");
        }
    }
}
