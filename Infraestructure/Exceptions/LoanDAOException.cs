namespace Library.Infraestructure.Exceptions
{
    internal static class LoanDAOException
    {
        public class DataAccessException : Exception
        {
            public DataAccessException(string message, Exception? inner = null)
                : base(message, inner) { }
        }

        public class LoanNotFoundException : DataAccessException
        {
            public LoanNotFoundException(int id)
                : base($"No se encontró el préstamo con ID {id}.") { }
        }

        public class LoanInsertException : DataAccessException
        {
            public LoanInsertException(Exception? inner = null)
                : base("Error al crear el préstamo en la base de datos.", inner) { }
        }

        public class LoanUpdateException : DataAccessException
        {
            public LoanUpdateException(Exception? inner = null)
                : base("Error al actualizar el préstamo en la base de datos.", inner) { }
        }

        public class LoanDeleteException : DataAccessException
        {
            public LoanDeleteException(Exception? inner = null)
                : base("Error al eliminar el préstamo de la base de datos.", inner) { }
        }

        public class LoanListException : DataAccessException
        {
            public LoanListException(Exception? inner = null)
                : base("Error al obtener la lista de préstamos.", inner) { }
        }
    }
}
