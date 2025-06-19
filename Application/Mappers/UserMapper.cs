using Library.Application.DTOs;
using Library.Domain.Entities;
using Library.Domain.Enumerations;
using Microsoft.Data.SqlClient;

namespace Library.Application.Mappers
{
    public static class UserMapper
    {
        public static UserDTO ToDTO(User user)
        {
            return new UserDTO
            {
                Document = user.Document,
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
                document: dto.Document,
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

        public static UserDTO UserDataReader(SqlDataReader reader)
        {
            return new UserDTO
            {
                Id = Convert.ToInt32(reader["Id"]),
                Document = Convert.ToInt32(reader["document"]),
                FirstName = reader["firstName"].ToString() ?? "",
                LastName = reader["lastName"].ToString() ?? "",
                MiddleName = reader["middleName"].ToString() ?? "",
                Age = Convert.ToInt32(reader["age"]),
                Email = reader["email"].ToString() ?? "",
                UserName = reader["userName"].ToString() ?? "",
                Password = reader["password"].ToString() ?? "",
                UserType = Enum.Parse<UserType>(reader["userType"].ToString() ?? "Regular"),
                UserRole = Enum.Parse<UserRole>(reader["userRole"].ToString() ?? "User"),
                Arrears = Convert.ToInt32(reader["arrears"]),
                IsActive = Convert.ToBoolean(reader["isActive"])
            };
        }
    }
}
