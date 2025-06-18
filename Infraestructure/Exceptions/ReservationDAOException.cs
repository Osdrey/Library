namespace Library.Infraestructure.Exceptions
{
    internal static class ReservationDAOException
    {
        public class DataAccessException : Exception
        {
            public DataAccessException(string message, Exception? inner = null)
                : base(message, inner) { }
        }

        public class ReservationNotFoundException : DataAccessException
        {
            public ReservationNotFoundException(int id)
                : base($"No se encontró la reserva con ID {id}.") { }
        }

        public class ReservationInsertException : DataAccessException
        {
            public ReservationInsertException(Exception? inner = null)
                : base("Error al crear la reserva en la base de datos.", inner) { }
        }

        public class ReservationUpdateException : DataAccessException
        {
            public ReservationUpdateException(Exception? inner = null)
                : base("Error al actualizar la reserva en la base de datos.", inner) { }
        }

        public class ReservationDeleteException : DataAccessException
        {
            public ReservationDeleteException(Exception? inner = null)
                : base("Error al eliminar la reserva de la base de datos.", inner) { }
        }

        public class ReservationListException : DataAccessException
        {
            public ReservationListException(Exception? inner = null)
                : base("Error al obtener la lista de reservas.", inner) { }
        }
    }
}
