using Library.Application.DTOs;

namespace Library.Infraestructure.Interfaces
{
    internal interface IReservationDAO
    {
        List<ReservationDTO> GetAllReservations();
        List<ReservationDTO> GetPendingReservations();
        List<ReservationDTO> GetReservationsByUserId(int userId);
        ReservationDTO? GetReservationById(int reservationId);
        void InsertReservation(ReservationDTO reservation);
        void UpdateReservation(ReservationDTO reservation);
        void DeleteReservation(int reservationId);
    }
}
