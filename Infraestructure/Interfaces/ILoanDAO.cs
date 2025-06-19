using Library.Application.DTOs;

namespace Library.Infraestructure.Interfaces
{
    internal interface ILoanDAO
    {
        List<LoanDTO> GetAllLoans();
        List<LoanDTO> GetLoansByUserId(int userId);
        LoanDTO? GetLoanById(int loanId);
        void InsertLoan(LoanDTO loan);
        void UpdateLoan(LoanDTO loan);
        void DeleteLoan(int loanId);
    }
}
