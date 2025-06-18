using Library.Application.DTOs;
using Library.Application.Interfaces;

namespace Library.Presentation.UI.Menus
{
    internal class LoanUI
    {
        private readonly ILoanService _loanService;

        public LoanUI(ILoanService loanService)
        {
            _loanService = loanService;
        }

        public void ShowMenu(UserDTO loggedUser)
        {
            bool flagMenu = true;
            while (flagMenu)
            {
                Console.Clear();
                Console.WriteLine(  "╔════════════════════════════════╗\n" +
                                    "║      GESTOR DE PRÉSTAMOS       ║\n" +
                                    "╚════════════════════════════════╝\n" +
                "\n¿Qué acción vas a realizar? Ingresa una de las opciones disponibles:\n");
                Console.WriteLine(
                    "1. Crear préstamo\n" +
                    "2. Listar préstamos\n" +
                    "3. Buscar préstamos de un usuario\n" +
                    "4. Buscar préstamo\n" +
                    "5. Extender préstamo\n" +
                    "6. Devolver material\n" +
                    "7. Cancelar préstamo\n" +
                    "9. Volver al menú anterior\n" +
                    "0. Salir\n");

                var input = Console.ReadLine();
                Console.Clear();

                switch (input)
                {
                    case "1":
                        _loanService.CreateLoanManually(loggedUser);
                        break;
                    case "2":
                        _loanService.ListLoan();
                        break;
                    case "3":
                        _loanService.ListUserLoans(loggedUser);
                        break;
                    case "4":
                        _loanService.SearchLoan();
                        break;
                    case "5":
                        _loanService.ExtendLoan();
                        break;
                    case "6":
                        _loanService.ReturnMaterial();
                        break;
                    case "7":
                        _loanService.CancelLoan();
                        break;
                    case "9":
                        Console.WriteLine("Regresando al menú anterior...");
                        Thread.Sleep(1000);
                        Console.Clear();
                        flagMenu = false;
                        break;
                    case "0":
                        Console.WriteLine("Saliendo del sistema...");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Opción inválida. Intente de nuevo.");
                        break;
                }
            }
        }
    }
}
