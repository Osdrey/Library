using Library.Application.Exceptions;
using Library.Application.Helpers;
using Library.Application.Interfaces;
using Library.Infraestructure.Interfaces;
using Library.Presentation.UI.Inputs;
using Library.Presentation.UI.Printers;

namespace Library.Application.Services
{
    internal class UserService : IUserService
    {
        private readonly IUserDAO _userDao;

        public UserService(IUserDAO userDao)
        {
            _userDao = userDao;
        }

        public void SearchAllUsers()
        {
            var users = _userDao.SearchAllUsers();

            if (users.Count == 0)
            {
                throw new UserException.UserListNotFoundException();
            }

            Console.WriteLine("\nLista de usuarios activos:");

            foreach (var user in users)
            {
                UserPrinter.Print(user);
            }
            Console.WriteLine("\nPresiona una tecla para regresar al menú...");
            Console.ReadKey();
        }

        public void SearchUser()
        {
            Console.WriteLine("Ingrese el valor a buscar (documento, email o username):");
            string input = Console.ReadLine()!;
            var user = _userDao.SearchUser(input);

            if (user is null)
            {
                throw new UserException.UserNotFoundException(input);
            }
            else
            {
                UserPrinter.Print(user);
            }
        }

        public void CreateUser()
        {
            var userDto = UserInput.GetUserFromInput();

            if (_userDao.SearchUser(userDto.Document.ToString()) != null)
            {
                throw new UserException.DocumentAlreadyExistException(userDto.Document.ToString());
            }

            if (_userDao.SearchUser(userDto.Email) != null)
            {
                throw new UserException.DuplicateEmailException(userDto.Email);
            }

            if (_userDao.SearchUser(userDto.UserName) != null)
            {
                throw new UserException.UserAlreadyExistsException(userDto.UserName);
            }

            userDto.Password = PasswordHelper.HashPassword(userDto.Password);
            _userDao.CreateUser(userDto);
            Console.WriteLine("\nUsuario creado exitosamente.");
            Console.WriteLine("Presiona una tecla para continuar...");
            Console.ReadKey();
        }

        public void UpdateUser()
        {
            int document = UserInput.GetDocumentFromInput();
            var user = _userDao.SearchUser(document.ToString());

            if (user is null)
            {
                throw new UserException.UserNotFoundException(document.ToString());
            }

            var updatedUser = UserInput.GetUserUpdateInput(user);
            string newPasswordHash = PasswordHelper.HashPassword(updatedUser.Password);

            if (user.Password != newPasswordHash)
            {
                updatedUser.Password = newPasswordHash;
            }
            else
            {
                updatedUser.Password = user.Password;
            }

            _userDao.UpdateUser(updatedUser);
            Console.WriteLine("\nUsuario actualizado correctamente.");
            Console.WriteLine("Presiona una tecla para continuar...");
            Console.ReadKey();
        }

        public void ReactivateUser()
        {
            int document = UserInput.GetDocumentFromInput();
            var user = _userDao.SearchUser(document.ToString());

            if (user is null)
            {
                throw new UserException.UserNotFoundException(document.ToString());
            }

            if (user.IsActive)
            {
                Console.WriteLine("\nEl usuario ya está activo.");
                return;
            }

            if (UserInput.ConfirmAction(user.FirstName, user.LastName))
            {
                _userDao.ReactivateUser(document);
                Console.WriteLine("\nUsuario reactivado.");
            }
            else
            {
                Console.WriteLine("\nOperación cancelada.");
            }
            Console.WriteLine("Presiona una tecla para continuar...");
            Console.ReadKey();
        }

        public void DeactivateUser()
        {
            int document = UserInput.GetDocumentFromInput();
            var user = _userDao.SearchUser(document.ToString());

            if (user is null)
            {
                throw new UserException.UserNotFoundException(document.ToString());
            }

            if (!user.IsActive)
            {
                Console.WriteLine("\nEl usuario ya está desactivado.");
                return;
            }

            if (UserInput.ConfirmAction(user.FirstName, user.LastName))
            {
                _userDao.DeactivateUser(document);
                Console.WriteLine("\nUsuario desactivado.");
            }
            else
            {
                Console.WriteLine("\nOperación cancelada.");
            }
            Console.WriteLine("Presiona una tecla para continuar...");
            Console.ReadKey();
        }
    }
}
