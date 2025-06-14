namespace Library.Application.Interfaces
{
    internal interface ILoanService
    {
        void SearchLoan();
        void CreateLoan();
        void ExtendLoan();
        void ReturnMaterial();
        void CancelLoan();
    }
}
