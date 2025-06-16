using Library.Domain.Enumerations;

namespace Library.Application.DTOs
{
    public class UserDTO
    {
        public int Document { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UserType UserType { get; set; }
        public UserRole UserRole { get; set; }
        public int Arrears { get; set; }
        public bool IsActive { get; set; }
    }
}
