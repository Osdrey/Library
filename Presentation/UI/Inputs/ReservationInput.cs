namespace Library.Presentation.UI.Inputs
{
    internal static class ReservationInput
    {
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

        public static int GetReservationIdFromInput()
        {
            Console.Write("Ingrese ID de la reserva: ");
            return int.Parse(Console.ReadLine()!);
        }
    }
}
