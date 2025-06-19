using Library.Application.DTOs;
using Library.Domain.Entities;
using Microsoft.Data.SqlClient;

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
                LoanStatus = (int)loan.LoanStatus
            };
        }

        public static LoanDTO LoanDataReader(SqlDataReader reader)
        {
            return new LoanDTO
            {
                LoanId = Convert.ToInt32(reader["LoanId"]),
                ReservationId = Convert.ToInt32(reader["ReservationId"]),
                UserId = Convert.ToInt32(reader["UserId"]),
                StartDate = Convert.ToDateTime(reader["StartDate"]),
                DueDate = Convert.ToDateTime(reader["DueDate"]),
                ReturnDate = reader["ReturnDate"] == DBNull.Value
                                ? null
                                : Convert.ToDateTime(reader["ReturnDate"]),
                LoanStatus = Convert.ToInt32(reader["LoanStatus"])
            };
        }
    }
}
