using Library.Application.DTOs;
using Library.Domain.Entities;

namespace Library.Application.Mappers
{
    public static class LoanMapper
    {
        public static LoanDTO ToDTO(Loan loan)
        {
            return new LoanDTO
            {
                LoanId = loan.LoanId,
                UserId = loan.User.Id,
                ReservationId = loan.Reservation.ReservationId,
                StartDate = loan.StartDate,
                DueDate = loan.DueDate,
                ReturnDate = loan.ReturnDate,
                LoanStatus = loan.LoanStatus.ToString()
            };
        }
    }
}
