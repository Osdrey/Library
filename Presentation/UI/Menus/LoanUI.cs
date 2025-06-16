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

        public void ShowMenu()
        {
            bool flagMenu = true;
            while (flagMenu)
            {
                Console.WriteLine(  "╔════════════════════════════════╗\n" +
                                    "║      GESTOR DE PRÉSTAMOS       ║\n" +
                                    "╚════════════════════════════════╝\n" +
                "\n¿Qué acción vas a realizar? Ingresa una de las opciones disponibles:\n");
                Console.WriteLine(
                    "1. Crear préstamo\n" +
                    "2. Buscar préstamo\n" +
                    "3. Extender préstamo\n" +
                    "4. Devolver material\n" +
                    "5. Cancelar préstamo\n" +
                    "9. Volver al menú anterior\n" +
                    "0. Salir\n");

                var input = Console.ReadLine();
                Console.Clear();

                switch (input)
                {
                    case "1":
                        _loanService.CreateLoan();
                        break;
                    case "2":
                        _loanService.SearchLoan();
                        break;
                    case "3":
                        _loanService.ExtendLoan();
                        break;
                    case "4":
                        _loanService.ReturnMaterial();
                        break;
                    case "5":
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
