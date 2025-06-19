using Library.Application.DTOs;
using Library.Application.Exceptions;
using Library.Application.Helpers;
using Library.Application.Interfaces;
using Library.Infraestructure.Exceptions;
using Library.Infraestructure.Interfaces;

namespace Library.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserDAO _userDao;

        public AuthService(IUserDAO userDao)
        {
            _userDao = userDao;
        }

        public UserDTO? Login(string username, string password)
        {
            try
            {
                var hashedPassword = PasswordHelper.HashPassword(password);
                var user = _userDao.GetUser(username);

                if (user is null)
                {
                    throw new UserException.UserNotFoundException(username);
                }

                if (!user.IsActive)
                {
                    throw new UserException.UserInactiveException(username);
                }

                if (user.Password != hashedPassword)
                {
                    throw new UserException.IncorrectPasswordException();
                }

                return user;
            }
            catch (Exception ex)
            {
                throw new UserDAOException.UserSearchException(ex);
            }
        }


        public bool Register(UserDTO user)
        {
            try
            {
                if (_userDao.GetUser(user.UserName) is not null)
                {
                    Console.WriteLine("Nombre de usuario ya registrado.");
                    return false;
                }

                user.Password = PasswordHelper.HashPassword(user.Password);
                _userDao.InsertUser(user);
                Console.WriteLine("Usuario registrado correctamente.");
                Console.WriteLine("Presiona una tecla para continuar...");
                Console.ReadKey();
                return true;
            }
            catch (Exception ex)
            {
                throw new UserDAOException.UserInsertException(ex);
            }
        }

        public bool ChangePassword(UserDTO user, string currentPassword, string newPassword)
        {
            try
            {
                var currentPasswordHash = PasswordHelper.HashPassword(currentPassword);
                if (user.Password != currentPasswordHash)
                {
                    Console.WriteLine("Contraseña actual incorrecta.");
                    return false;
                }

                var newPasswordHash = PasswordHelper.HashPassword(newPassword);
                _userDao.ChangePassword(user.UserName, newPasswordHash);
                user.Password = newPasswordHash;
                Console.WriteLine("Contraseña actualizada correctamente.");
                Console.WriteLine("Presiona una tecla para continuar...");
                Console.ReadKey();
                return true;
            }
            catch (Exception ex)
            {
                throw new UserDAOException.PasswordChangeException(ex);
            }
        }
    }
}