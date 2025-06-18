namespace Library.Application.Exceptions
{
    internal class ReservationException
    {
        public class ReservationNotFoundException : Exception
        {
            public ReservationNotFoundException() : base("Reserva no encontrada.") { }
        }

        public class ReservationInsertException : Exception
        {
            public ReservationInsertException(Exception? inner = null)
                : base("Error al crear la reserva.", inner) { }
        }

        public class ReservationUpdateException : Exception
        {
            public ReservationUpdateException(Exception? inner = null)
                : base("Error al actualizar la reserva.", inner) { }
        }

        public class ReservationListException : Exception
        {
            public ReservationListException(Exception? inner = null)
                : base("Error al listar las reservas.", inner) { }
        }

        public class ReservationInvalidActionException : Exception
        {
            public ReservationInvalidActionException(string message)
                : base(message) { }
        }
    }
}
