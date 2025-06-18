using Library.Application.DTOs;

namespace Library.Application.Interfaces
{
    internal interface ILoanService
    {
        void ListLoan();
        void ListUserLoans(UserDTO loggedUser);
        void SearchLoan();
        void CreateLoanFromReservation(ReservationDTO reservation);
        void CreateLoanManually(UserDTO loggedUser);
        void ExtendLoan();
        void ReturnMaterial();
        void CancelLoan();
    }
}
