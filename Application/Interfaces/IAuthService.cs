using Library.Application.DTOs;
using Library.Domain.Entities;

namespace Library.Application.Interfaces
{
    public interface IAuthService
    {
        UserDTO? Login(string username, string password);
        bool Register(UserDTO user);
        bool ChangePassword(UserDTO user, string currentPassword, string newPassword);
    }
}
