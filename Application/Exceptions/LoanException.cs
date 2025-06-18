namespace Library.Application.Exceptions
{
    internal class LoanException
    {
        public class LoanNotFoundException : Exception
        {
            public LoanNotFoundException() : base("Préstamo no encontrado.") { }
        }

        public class LoanInsertException : Exception
        {
            public LoanInsertException(Exception? inner = null)
                : base("Error al crear el préstamo.", inner) { }
        }

        public class LoanUpdateException : Exception
        {
            public LoanUpdateException(Exception? inner = null)
                : base("Error al actualizar el préstamo.", inner) { }
        }

        public class LoanListException : Exception
        {
            public LoanListException(Exception? inner = null)
                : base("Error al listar los préstamos.", inner) { }
        }

        public class LoanInvalidActionException : Exception
        {
            public LoanInvalidActionException(string message)
                : base(message) { }
        }

        public class MaterialAlreadyLoanedException : Exception
        {
            public MaterialAlreadyLoanedException(int materialId)
                : base($"El material con ID {materialId} ya está prestado.") { }
        }

        public class MaterialAlreadyReservedException : Exception
        {
            public MaterialAlreadyReservedException(int materialId)
                : base($"El material con ID {materialId} ya está reservado por otro usuario.") { }
        }
    }
}
