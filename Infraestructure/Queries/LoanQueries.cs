namespace Library.Infraestructure.Queries
{
    public static class LoanQueries
    {
        public const string GetAllLoans = @"
            SELECT * FROM Loans";

        public const string GetLoansByUserId = @"
            SELECT * FROM Loans WHERE UserId = @userId";

        public const string GetLoanById = @"
            SELECT * FROM Loans WHERE LoanId = @loanId";

        public const string InsertLoan = @"
            INSERT INTO Loans (UserId, ReservationId, StartDate, DueDate, ReturnDate, LoanStatus)
            VALUES (@userId, @reservationId, @startDate, @dueDate, @returnDate, @loanStatus)";

        public const string UpdateLoan = @"
            UPDATE Loans
            SET 
                DueDate = @dueDate,
                ReturnDate = @returnDate,
                LoanStatus = @loanStatus
            WHERE LoanId = @loanId";

        public const string DeleteLoan = @"
            DELETE FROM Loans
            WHERE LoanId = @loanId";
    }
}
