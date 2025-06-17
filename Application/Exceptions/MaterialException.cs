namespace Library.Application.Exceptions
{
    internal static class MaterialException
    {
        public class MaterialListNotFoundException : Exception
        {
            public MaterialListNotFoundException()
                : base("No se encontraron materiales disponibles en el sistema.") { }
        }
        public class MaterialFilterNotFoundException : Exception
        {
            public MaterialFilterNotFoundException()
                : base("No se coincidencias con la búsqueda realizada.") { }
        }

        public class MaterialNotFoundException : Exception
        {
            public MaterialNotFoundException(string id)
                : base($"No se encontró el material con ID '{id}'.") { }

            public MaterialNotFoundException(int id)
            {
            }
        }

        public class MaterialAlreadyExistsException : Exception
        {
            public MaterialAlreadyExistsException(string title)
                : base($"El material con título '{title}' ya está registrado.") { }
        }

        public class MaterialUnavailableException : Exception
        {
            public MaterialUnavailableException(int id)
                : base($"El material con ID '{id}' no está disponible.") { }
        }

        public class InvalidMaterialTopicException : Exception
        {
            public InvalidMaterialTopicException(string topic)
                : base($"El tema '{topic}' no es válido para materiales.") { }
        }

        public class InvalidMaterialDataException : Exception
        {
            public InvalidMaterialDataException(string reason)
                : base($"Datos inválidos del material: {reason}.") { }
        }
    }
}