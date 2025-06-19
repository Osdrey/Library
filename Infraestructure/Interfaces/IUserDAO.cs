using Library.Application.DTOs;

namespace Library.Infraestructure.Interfaces
{
    public interface IUserDAO
    {
        List<UserDTO> GetAllUsers();
        UserDTO? GetUser(string input);
        void InsertUser(UserDTO user);
        void UpdateUser(UserDTO user);
        void ReactivateUser(int cedula);
        void DeactivateUser(int cedula);
        void ChangePassword(string username, string newPasswordHash);
        void UpdateUserArrears(UserDTO user);
    }
}
