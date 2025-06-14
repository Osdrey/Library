using Library.Application.DTOs;
using Library.Application.Interfaces;
using Library.Application.Services;
using Library.Presentation.UI.Menus;

namespace Library.Presentation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IUserService userService = new UserService();
            IMaterialService materialService = new MaterialService();
            ILoanService loanService = new LoanService();
            IReservationService reservationService = new ReservationService();
            var materialUI = new MaterialUI(materialService);
            var loanUI = new LoanUI(loanService);
            var reservationUI = new ReservationUI(reservationService);
            var userUI = new UserUI(userService);

            bool flagMenu = true;
            while (flagMenu)
            {
                Console.WriteLine(
                    "¿Qué rol tienes?\n" +
                    "1. Usuario regular\n" +
                    "2. Bibliotecario\n" +
                    "3. Administrador\n" +
                    "9. Volver al menú anterior\n" +
                    "0. Salir");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        var regularUser = new UserDTO { FirstName = "Carlos", UserRole = Domain.Enumerations.UserRole.Regular };
                        var regularUI = new RegularUI(materialService, loanService, reservationService);
                        regularUI.ShowMenu(regularUser);
                        break;

                    case "2":
                        var librarianUser = new UserDTO { FirstName = "Adriana", UserRole = Domain.Enumerations.UserRole.Librarian };
                        var librarianUI = new LibrarianUI(materialUI, loanUI, reservationService);
                        librarianUI.ShowMenu(librarianUser);
                        break;

                    case "3":
                        var adminUser = new UserDTO { FirstName = "Oscar", UserRole = Domain.Enumerations.UserRole.Administrator };
                        var adminUI = new AdministratorUI(materialUI, loanUI, reservationUI, userUI);
                        adminUI.ShowMenu(adminUser);
                        break;

                    case "9":
                        Console.WriteLine("Regresando al menú anterior...");
                        flagMenu = false;
                        break;

                    case "0":
                        Console.WriteLine("Saliendo del sistema...");
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Opción inválida. Intente de nuevo.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
