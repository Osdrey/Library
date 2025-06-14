using Library.Application.DTOs;

namespace Library.Presentation.UI.Menus
{
    internal class AdministratorUI
    {
        private readonly MaterialUI _materialUI;
        private readonly LoanUI _loanUI;
        private readonly ReservationUI _reservationUI;
        private readonly UserUI _userUI;

        public AdministratorUI(MaterialUI materialUI, LoanUI loanUI, ReservationUI reservationUI, UserUI userUI)
        {
            _materialUI = materialUI;
            _loanUI = loanUI;
            _reservationUI = reservationUI;
            _userUI = userUI;
        }

        public void ShowMenu(UserDTO userDTO)
        {
            bool flagMenu = true;
            while (flagMenu)
            {
                Console.WriteLine(
                    $"¡Hola {userDTO.FirstName}! Bienvenido al sistema de prestamos de la universidad\n" +
                    "¿En que podemos ayudarte? Elige una de las opciones disponibles:");
                Console.WriteLine(
                    "1. Gestionar materiales\n" +
                    "2. Gestionar préstamos\n" +
                    "3. Gestionar reservas\n" +
                    "4. Gestionar usuarios\n" +
                    "9. Volver al menú anterior\n" +
                    "0. Salir");

                var input = Console.ReadLine();
                Console.Clear();

                switch (input)
                {
                    case "1":
                        _materialUI.ShowMenu();
                        break;
                    case "2":
                        _loanUI.ShowMenu();
                        break;
                    case "3":
                        _reservationUI.ShowMenu();
                        break;
                    case "4":
                        _userUI.ShowMenu();
                        break;
                    case "9":
                        Console.WriteLine("Regresando al menu anterior...");
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
