using Library.Application.Interfaces;
using Library.Application.Services;
using Library.Domain.Enumerations;
using Library.Infraestructure.DAOs;
using Library.Infraestructure.Interfaces;
using Library.Infrastructure.Config;
using Library.Presentation.Helpers;
using Library.Presentation.UI.Inputs;
using Library.Presentation.UI.Menus;

namespace Library.Presentation
{
    internal class Program
    {
        static void Main(string[] args)
        {

            try
            {
                DbConfig.TestConnection();
            }
            catch (Exception ex)
            {
                ExceptionHandler.DAOHandle(ex);
                Console.WriteLine("Presiona una tecla para continuar...");
                Console.ReadKey();
            }

            IUserDAO userDao = new UserDAO();
            IAuthService authService = new AuthService(userDao);
            IUserService userService = new UserService(userDao);
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
                Console.Clear();
                Console.WriteLine(  "╔════════════════════════════════════════════════╗\n" +
                                    "║      SISTEMA BIBLIOTECARIO DE UNIVERSIDAD      ║\n" +
                                    "╚════════════════════════════════════════════════╝\n" +
                    "\nElige una de las opciones disponibles:\n");
                Console.WriteLine(
                    "1. Iniciar sesión\n" +
                    "2. Registrarse\n" +
                    "0. Salir\n");

                var opcion = Console.ReadLine();
                Console.Clear();

                try
                {
                    switch (opcion)
                    {
                        case "1":
                            var (username, password) = AuthInput.LoginCredentials();
                            var loggedUser = authService.Login(username, password);

                            if (loggedUser is not null)
                            {
                                switch (loggedUser.UserRole)
                                {
                                    case UserRole.Regular:
                                        var regularUI = new RegularUI(materialService, loanService, reservationService, authService, loggedUser);
                                        regularUI.ShowMenu(loggedUser);
                                        break;
                                    case UserRole.Librarian:
                                        var librarianUI = new LibrarianUI(materialUI, loanUI, reservationService, authService, loggedUser);
                                        librarianUI.ShowMenu(loggedUser);
                                        break;
                                    case UserRole.Administrator:
                                        var adminUI = new AdministratorUI(materialUI, loanUI, reservationUI, userUI, authService, loggedUser);
                                        adminUI.ShowMenu(loggedUser);
                                        break;
                                    default:
                                        Console.WriteLine("Rol no reconocido.");
                                        break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Inicio de sesión fallido.");
                                Console.WriteLine("Presiona una tecla para continuar...");
                                Console.ReadKey();
                            }
                            break;

                        case "2":
                            var newUser = UserInput.GetUserFromInput();
                            authService.Register(newUser);
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
                catch (Exception ex)
                {
                    ExceptionHandler.UserHandle(ex);
                    Console.WriteLine("Presiona una tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }
    }
}