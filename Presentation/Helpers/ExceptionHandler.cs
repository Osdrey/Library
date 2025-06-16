using Library.Application.Exceptions;
using Library.Infraestructure.Exceptions;

namespace Library.Presentation.Helpers
{
    public static class ExceptionHandler
    {
        private static void PrintException(string label, Exception ex)
        {
            Console.WriteLine($"\n[{label}]: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"[Detalle]: {ex.InnerException.Message}");
            }
        }
        public static void DAOHandle(Exception ex)
        {
            switch (ex)
            {
                case DAOException.ConnectionException:
                    PrintException("Error de conexión",ex);
                    break;

                case DAOException.ConfigurationException:
                    PrintException("Error de configuración", ex);
                    break;

                default:
                    PrintException("Error de conexión", ex);
                    break;
            }
        }

        public static void UserHandle(Exception ex)
        {
            switch (ex)
            {
                case UserDAOException.UserInsertException:
                case UserDAOException.UserUpdateException:
                case UserDAOException.UserSearchException:
                case UserDAOException.UserListException:
                case UserDAOException.PasswordChangeException:
                case UserDAOException.UserStatusChangeException:
                    PrintException("Error de conexión", ex);
                    break;
                case UserException.UserListNotFoundException:
                case UserException.UserNotFoundException:
                case UserException.DocumentAlreadyExistException:
                case UserException.DuplicateEmailException:
                case UserException.UserAlreadyExistsException:
                case UserException.UserInactiveException:
                case UserException.IncorrectPasswordException:
                case UserException.ActionErrorException:
                    PrintException("Error de conexión", ex);
                    break;
                default:
                    PrintException("Error de conexión", ex);
                    break;
            }
        }
    }
}
