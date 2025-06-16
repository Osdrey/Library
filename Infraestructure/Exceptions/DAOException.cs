namespace Library.Infraestructure.Exceptions
{
    internal class DAOException
    {
        public class DataAccessException : Exception
        {
            public DataAccessException(string message, Exception? inner = null)
                : base(message, inner) { }
        }

        public class ConnectionException : DataAccessException
        {
            public ConnectionException(Exception? inner = null)
                : base("Error al conectar con la base de datos.", inner) { }
        }

        public class ConfigurationException : DataAccessException
        {
            public ConfigurationException(string message, Exception? inner = null)
                : base($"Error de configuración de base de datos: {message}", inner) { }
        }
    }
}
