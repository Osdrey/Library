using System.Text.RegularExpressions;

namespace Library.Application.Validations
{
    internal class ValidationHelper
    {
        public static class Validations
        {

            public static string TextValidate(string? input, string attributeName = "Campo")
            {
                while (string.IsNullOrWhiteSpace(input) || !Regex.IsMatch(input, @"^[a-zA-ZÁÉÍÓÚáéíóúÑñ\s]+$"))
                {
                    Console.WriteLine($"{attributeName} inválido. Solo se permiten letras. Intente nuevamente:");
                    input = Console.ReadLine();
                }
                return input!;
            }

            public static int IntegerValidate(string? input, string attributeName = "Campo", int? min = null, int? max = null)
            {
                int result;

                while (!int.TryParse(input, out result) ||
                       (min.HasValue && result < min) ||
                       (max.HasValue && result > max))
                {
                    string rangoMsg = min.HasValue || max.HasValue
                        ? $" ({min ?? int.MinValue} - {max ?? int.MaxValue})"
                        : "";
                    Console.WriteLine($"{attributeName} inválido{rangoMsg}. Intente nuevamente:");
                    input = Console.ReadLine();
                }

                return result;
            }

            public static string EmailValidate(string? input, string attributeName = "Email")
            {
                var regex = new Regex(@"^[\w\.-]+@[\w\.-]+\.\w{2,}$");

                while (string.IsNullOrWhiteSpace(input) || !regex.IsMatch(input))
                {
                    Console.WriteLine($"{attributeName} inválido. Ingrese un email válido (ej: ejemplo@dominio.com):");
                    input = Console.ReadLine();
                }

                return input!;
            }

            public static string PasswordValidate(string? input, string attributeName = "Contraseña")
            {
                var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&\-_.])[A-Za-z\d@$!%*?&\-_\.+]{8,}$");

                while (string.IsNullOrWhiteSpace(input) || !regex.IsMatch(input))
                {
                    Console.WriteLine($"{attributeName} inválida. Debe tener al menos:\n" +
                                      "- 8 caracteres\n" +
                                      "- Una mayúscula\n" +
                                      "- Una minúscula\n" +
                                      "- Un número\n" +
                                      "- Un carácter especial (@$!%*?&-_.).\n" +
                                      "Intente nuevamente:");
                    input = Console.ReadLine();
                }

                return input!;
            }

            public static int DocumentValidate(string? input, string attributeName = "Documento")
            {
                return IntegerValidate(input, attributeName, min: 1000000);
            }

            public static bool ConfirmAction(string mensaje = "¿Desea continuar? (s/n)")
            {
                Console.WriteLine(mensaje);
                var respuesta = Console.ReadLine();
                return respuesta?.ToLower() == "s";
            }
        }
    }
}