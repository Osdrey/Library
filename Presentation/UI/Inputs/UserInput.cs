using Library.Application.DTOs;
using Library.Domain.Enumerations;
using static Library.Application.Validations.ValidationHelper.Validations;

namespace Library.Presentation.UI.Inputs
{
    internal static class UserInput
    {
        public static UserDTO GetUserFromInput()
        {
            Console.WriteLine(  "╔══════════════════════════════╗\n" +
                                "║     REGISTRO DE USUARIO      ║\n" +
                                "╚══════════════════════════════╝\n");

            UserType typeUser = SelectUserType();
            int document = DocumentValidate(Request("Ingrese la cédula:"), "Cédula");
            string name = TextValidate(Request("Ingrese su nombre:"), "Nombre");
            string lastName = TextValidate(Request("Ingrese primer apellido:"), "Apellido");
            string middleName = TextValidate(Request("Ingrese segundo apellido:"), "Segundo apellido");
            int age = IntegerValidate(Request("Edad:"), "Edad", max: 120);
            string email = EmailValidate(Request("Email:"), "Email");
            string username = TextValidate(Request("Nombre de usuario:"), "Usuario");
            string password = PasswordValidate(Request("Contraseña:"), "Contraseña");

            return new UserDTO
            {
                Document = document,
                FirstName = name,
                LastName = lastName,
                MiddleName = middleName,
                Age = age,
                Email = email,
                UserName = username,
                Password = password,
                UserType = typeUser,
                UserRole = UserRole.Regular,
                Arrears = 0,
                IsActive = true
            };
        }

        public static int GetDocumentFromInput()
        {
            return DocumentValidate(Request("Ingrese el documento del usuario:"), "Documento");
        }

        public static UserDTO GetUserUpdateInput(UserDTO user)
        {
            Console.WriteLine(  "╔═════════════════════════════════╗\n" +
                                "║    ACTUALIZACIÓN DE USUARIO     ║\n" +
                                "╚═════════════════════════════════╝\n");

            Console.WriteLine($"Documento actual: {user.Document}");
            string inputDocument = Request("Nuevo documento (Enter para omitir):");
            int newDocument = string.IsNullOrWhiteSpace(inputDocument)
                ? user.Document
                : IntegerValidate(inputDocument, "Documento");

            Console.WriteLine($"Nombre actual: {user.FirstName}");
            string inputName = Request("Nuevo nombre (Enter para omitir):");
            string newName = string.IsNullOrWhiteSpace(inputName)
                ? user.FirstName
                : TextValidate(inputName, "Nombre");

            Console.WriteLine($"Apellido paterno actual: {user.LastName}");
            string inputLastName = Request("Nuevo apellido paterno (Enter para omitir):");
            string newLastName = string.IsNullOrWhiteSpace(inputLastName)
                ? user.LastName
                : TextValidate(inputLastName, "Apellido");

            Console.WriteLine($"Apellido materno actual: {user.MiddleName}");
            string inputMiddleName = Request("Nuevo apellido materno (Enter para omitir):");
            string newMiddleName = string.IsNullOrWhiteSpace(inputMiddleName)
                ? user.MiddleName
                : TextValidate(inputMiddleName, "Segundo apellido");

            Console.WriteLine($"Edad actual: {user.Age}");
            string inputAge = Request("Nueva edad (Enter para omitir):");
            int newAge = string.IsNullOrWhiteSpace(inputAge)
                ? user.Age
                : IntegerValidate(inputAge, "Edad", min: 0, max: 120);

            Console.WriteLine($"Email actual: {user.Email}");
            string inputEmail = Request("Nuevo email (Enter para omitir):");
            string newEmail = string.IsNullOrWhiteSpace(inputEmail)
                ? user.Email
                : EmailValidate(inputEmail, "Email");

            Console.WriteLine($"Apodo actual: {user.UserName}");
            string inputUserName = Request("Nuevo apodo de usuario (Enter para omitir):");
            string newUserName = string.IsNullOrWhiteSpace(inputUserName)
                ? user.UserName
                : TextValidate(inputUserName, "Apodo");

            Console.WriteLine($"Contraseña actual: (oculta)");
            string inputPassword = Request("Nueva contraseña (Enter para omitir):");
            string newPassword = string.IsNullOrWhiteSpace(inputPassword)
                ? user.Password
                : inputPassword;

            Console.WriteLine($"Tipo de usuario actual: {user.UserType}");
            string inputUserType = Request("Nuevo tipo de usuario (Enter para omitir):");
            UserType newUserType = string.IsNullOrWhiteSpace(inputUserType)
                ? user.UserType
                : Enum.TryParse<UserType>(inputUserType, true, out var parsedType) ? parsedType : user.UserType;

            Console.WriteLine($"Rol actual: {user.UserRole}");
            string inputUserRole = Request("Nuevo rol (Enter para omitir):");
            UserRole newUserRole = string.IsNullOrWhiteSpace(inputUserRole)
                ? user.UserRole
                : Enum.TryParse<UserRole>(inputUserRole, true, out var parsedRole) ? parsedRole : user.UserRole;

            Console.WriteLine($"Minutos de retraso actuales: {user.Arrears}");
            string inputArrears = Request("Nueva cantidad de minutos para retrasos (Enter para omitir):");
            int newArrears = string.IsNullOrWhiteSpace(inputArrears)
                ? user.Arrears
                : IntegerValidate(inputArrears, "Retrasos", min: 0);

            user.Document = newDocument;
            user.FirstName = newName;
            user.LastName = newLastName;
            user.MiddleName = newMiddleName;
            user.Age = newAge;
            user.Email = newEmail;
            user.UserName = newUserName;
            user.Password = newPassword;
            user.UserType = newUserType;
            user.UserRole = newUserRole;
            user.Arrears = newArrears;

            return user;
        }

        private static UserType SelectUserType()
        {
            while (true)
            {
                Console.WriteLine("Seleccione tipo de usuario:");
                Console.WriteLine("1. Estudiante\n2. Maestro\n3. Empleado");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        return UserType.Student;
                    case "2":
                        return UserType.Teacher;
                    case "3":
                        return UserType.Employee;
                    default:
                        Console.WriteLine("Opción inválida. Intente de nuevo.");
                        break;
                }
            }
        }

        public static bool ConfirmAction(string firstname, string lastname)
        {
            Console.Clear();
            Console.WriteLine($"¿Confirma que desea continuar con esta acción para el usuario {firstname} {lastname}? (s/n)");
            var respuesta = Console.ReadLine();
            return respuesta?.ToLower() == "s";
        }

        private static string Request(string prompt)
        {
            Console.WriteLine(prompt);
            return Console.ReadLine()!;
        }
    }
}
