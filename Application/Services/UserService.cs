using Library.Application.Interfaces;

namespace Library.Application.Services
{
    internal class UserService : IUserService
    {
        public void SearchUser()
        {
            Console.WriteLine("Buscando usuario...");
        }
        public void CreateUser()
        {
            Console.WriteLine("Usuario creado exitosamente.");
        }

        public void UpdateUser()
        {
            Console.WriteLine("Actualizando usuario...");
        }

        public void DeleteUser()
        {
            Console.WriteLine("Eliminando usuario...");
        }
    }
}
