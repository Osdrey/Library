using Library.Application.DTOs;
using Library.Domain.Entities;

namespace Library.Application.Mappers
{
    public static class UserMapper
    {
        public static UserDTO ToDTO(User user)
        {
            return new UserDTO
            {
                Cedula = user.Cedula,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MiddleName = user.MiddleName,
                Age = user.Age,
                Email = user.Email,
                UserName = user.UserName,
                Password = user.Password,
                UserType = user.UserType,
                UserRole = user.UserRole,
                Arrears = user.Arrears,
                IsActive = user.IsActive
            };
        }

        public static User ToEntity(UserDTO dto)
        {
            return new User(
                cedula: dto.Cedula,
                firstName: dto.FirstName,
                lastName: dto.LastName,
                middleName: dto.MiddleName,
                age: dto.Age,
                email: dto.Email,
                userName: dto.UserName,
                password: dto.Password,
                userType: dto.UserType,
                userRole: dto.UserRole,
                arrears: dto.Arrears,
                isActive: dto.IsActive
            );
        }
    }
}
