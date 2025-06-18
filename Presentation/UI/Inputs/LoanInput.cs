namespace Library.Presentation.UI.Inputs
{
    internal static class LoanInput
    {
        public static int GetLoanIdFromInput()
        {
            Console.Write("Ingrese ID del préstamo: ");
            return int.Parse(Console.ReadLine()!);
        }

        public static int GetReservationIdFromInput()
        {
            Console.Write("Ingrese ID de la reserva asociada: ");
            return int.Parse(Console.ReadLine()!);
        }

        public static int GetUserIdFromInput()
        {
            Console.Write("Ingrese ID del usuario: ");
            return int.Parse(Console.ReadLine()!);
        }

        public static int GetMaterialIdFromInput()
        {
            Console.Write("Ingrese ID del material: ");
            return int.Parse(Console.ReadLine()!);
        }
    }
}
