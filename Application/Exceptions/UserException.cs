namespace Library.Application.Exceptions
{
    internal class UserException
    {
        public class UserListNotFoundException : Exception
        {
            public UserListNotFoundException()
                : base("No se encontraron usuarios activos en el sistema.") { }
        }

        public class UserNotFoundException : Exception
        {
            public UserNotFoundException(string identifier)
                : base($"No se encontró un usuario con el identificador '{identifier}'.") { }
        }

        public class DocumentAlreadyExistException : Exception
        {
            public DocumentAlreadyExistException(string document)
                : base($"El documento '{document}' ya está registrado.")
            {
            }
        }

        public class DuplicateEmailException : Exception
        {
            public DuplicateEmailException(string email)
                : base($"El correo electrónico '{email}' ya está registrado.")
            {
            }
        }

        public class UserAlreadyExistsException : Exception
        {
            public UserAlreadyExistsException(string username)
                : base($"El nombre de usuario '{username}' ya está registrado.") { }
        }

        public class UserInactiveException : Exception
        {
            public UserInactiveException(string username)
                : base($"El usuario '{username}' está inactivo. Contacte con el administrador.") { }
        }
        public class IncorrectPasswordException : Exception
        {
            public IncorrectPasswordException()
                : base("La contraseña es incorrecta") { }
        }

        public class ActionErrorException : Exception
        {
            public ActionErrorException()
                : base("No se pudo completar la acción.") { }
        }
    }
}
