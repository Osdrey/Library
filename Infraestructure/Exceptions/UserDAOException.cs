namespace Library.Infraestructure.Exceptions
{
    internal class UserDAOException
    {
        public class DataAccessException : Exception
        {
            public DataAccessException(string message, Exception? inner = null)
                : base(message, inner) { }
        }

        public class UserInsertException : DataAccessException
        {
            public UserInsertException(Exception? inner = null)
                : base("Error al insertar el usuario en la base de datos.", inner) { }
        }

        public class UserUpdateException : DataAccessException
        {
            public UserUpdateException(Exception? inner = null)
                : base("Error al actualizar el usuario en la base de datos.", inner) { }
        }

        public class UserSearchException : DataAccessException
        {
            public UserSearchException(Exception? inner = null)
                : base("Error al buscar el usuario en la base de datos.", inner) { }
        }

        public class UserListException : DataAccessException
        {
            public UserListException(Exception? inner = null)
                : base("Error al obtener la lista de usuarios.", inner) { }
        }

        public class PasswordChangeException : DataAccessException
        {
            public PasswordChangeException(Exception? inner = null)
                : base("Error al cambiar la contraseña del usuario.", inner) { }
        }

        public class UserStatusChangeException : DataAccessException
        {
            public UserStatusChangeException(string action, Exception? inner = null)
                : base($"Error al {action} el estado del usuario.", inner) { }
        }
    }
}
