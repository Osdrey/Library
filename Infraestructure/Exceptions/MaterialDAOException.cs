namespace Library.Infraestructure.Exceptions
{
    internal static class MaterialDAOException
    {
        public class DataAccessException : Exception
        {
            public DataAccessException(string message, Exception? inner = null)
                : base(message, inner) { }
        }

        public class MaterialInsertException : DataAccessException
        {
            public MaterialInsertException(Exception? inner = null)
                : base("Error al insertar el material en la base de datos.", inner) { }
        }

        public class MaterialUpdateException : DataAccessException
        {
            public MaterialUpdateException(Exception? inner = null)
                : base("Error al actualizar el material en la base de datos.", inner) { }
        }

        public class MaterialDeleteException : DataAccessException
        {
            public MaterialDeleteException(Exception? inner = null)
                : base("Error al eliminar el material de la base de datos.", inner) { }
        }

        public class MaterialSearchException : DataAccessException
        {
            public MaterialSearchException(Exception? inner = null)
                : base("Error al buscar el material en la base de datos.", inner) { }
        }

        public class MaterialListException : DataAccessException
        {
            public MaterialListException(Exception? inner = null)
                : base("Error al obtener la lista de materiales.", inner) { }
        }

        public class MaterialStatusUpdateException : DataAccessException
        {
            public MaterialStatusUpdateException(Exception? inner = null)
                : base("Error al actualizar el estado del material.", inner) { }
        }
        public class MaterialStatusChangeException : DataAccessException
        {
            public MaterialStatusChangeException(Exception? inner = null)
                : base($"Error al cambiar el estado del material.", inner) { }
        }
        public class MaterialAvailabilityException : DataAccessException
        {
            public MaterialAvailabilityException(Exception? inner = null)
                : base("Error al verificar la disponibilidad del material.", inner) { }
        }
        public class MaterialTypeUnknownException : Exception
        {
            public MaterialTypeUnknownException(string type, string materialId)
                : base($"[Advertencia] Tipo de material desconocido: {type} (ID: {materialId})") { }
        }
    }
}
