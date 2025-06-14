namespace Library.Application.Interfaces
{
    internal interface IReservationService
    {
        void CreateReservation();
        void SearchReservation();
        void ExtendReservation();
        void CancelReservation();
        void AcceptReservation();
        void RejectReservation();
    }
}
