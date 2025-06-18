using Library.Application.DTOs;

namespace Library.Application.Interfaces
{
    internal interface IReservationService
    {
        void ListReservation();
        void ListPendingReservation();
        void ListUserReservations(UserDTO loggedUser);
        void SearchReservation();
        void CreateReservation(UserDTO loggedUser);
        void ExtendReservation();
        void CancelReservation();
        void AcceptReservation();
        void RejectReservation();
    }
}
