using Library.Application.Interfaces;

namespace Library.Application.Services
{
    internal class LoanService : ILoanService
    {
        public void SearchLoan()
        {
            Console.WriteLine("Buscando préstamo...");
        }
        public void CreateLoan()
        {
            Console.WriteLine("Creando préstamo...");
        }
        public void ExtendLoan()
        {
            Console.WriteLine("Extendiendo préstamo...");
        }
        public void ReturnMaterial()
        {
            Console.WriteLine("Devolviendo material...");
        }
        public void CancelLoan()
        {
            Console.WriteLine("Cancelando préstamo...");
        }
    }
}
