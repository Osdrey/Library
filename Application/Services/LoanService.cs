using Library.Application.DTOs;
using Library.Application.Exceptions;
using Library.Application.Interfaces;
using Library.Domain.Enumerations;
using Library.Infraestructure.Interfaces;
using Library.Presentation.UI.Inputs;
using Library.Presentation.UI.Printers;

namespace Library.Application.Services
{
    internal class LoanService : ILoanService
    {
        private readonly ILoanDAO _loanDAO;
        private readonly IReservationDAO _reservationDAO;
        private readonly IUserDAO _userDAO;
        private readonly IMaterialDAO _materialDAO;

        public LoanService(ILoanDAO loanDAO, IReservationDAO reservationDAO, IUserDAO userDAO, IMaterialDAO materialDAO)
        {
            _loanDAO = loanDAO;
            _reservationDAO = reservationDAO;
            _userDAO = userDAO;
            _materialDAO = materialDAO;
        }

        public void ListLoan()
        {
            UpdateExpiredLoans();
            var loans = _loanDAO.GetAllLoans();

            if (loans.Count == 0)
            {
                Console.WriteLine("No hay préstamos registrados.");
            }

            Console.WriteLine("\nLista de préstamos:");
            foreach (var loan in loans)
            {
                LoanPrinter.Print(loan);
            }

            Console.WriteLine("\nPresiona una tecla para continuar...");
            Console.ReadKey();
        }

        public void ListUserLoans(UserDTO loggedUser)
        {
            UpdateExpiredLoans();
            int userId;
            List<LoanDTO> loans;

            if (loggedUser.UserRole == UserRole.Administrator || loggedUser.UserRole == UserRole.Librarian)
            {
                userId = LoanInput.GetUserIdFromInput();
            }
            else
            {
                userId = loggedUser.Id;
            }

            loans = _loanDAO.GetLoansByUserId(userId);

            if (loans.Count == 0)
            {
                Console.WriteLine("El usuario no tiene préstamos registrados.");
            }

            Console.WriteLine("\nLista de préstamos:");
            foreach (var loan in loans)
            {
                LoanPrinter.Print(loan);
            }

            Console.WriteLine("\nPresiona una tecla para continuar...");
            Console.ReadKey();
        }

        public void SearchLoan()
        {
            UpdateExpiredLoans();
            int id = LoanInput.GetLoanIdFromInput();
            var loan = _loanDAO.GetLoanById(id);
            if (loan == null)
            {
                throw new LoanException.LoanNotFoundException();
            }
            else
            {
                LoanPrinter.Print(loan);
            }
            Console.WriteLine("\nPresiona una tecla para continuar...");
            Console.ReadKey();
        }

        public void CreateLoanFromReservation(ReservationDTO reservation)
        {
            var now = DateTime.Now;
            var dueDate = now.AddDays(14);

            var loan = new LoanDTO
            {
                ReservationId = reservation.ReservationId,
                UserId = reservation.UserId,
                StartDate = now,
                DueDate = dueDate,
                ReturnDate = null,
                LoanStatus = (int)LoanStatus.Active
            };
            try
            {
                _loanDAO.CreateLoan(loan);
            }
            catch (Exception ex)
            {
                throw new LoanException.LoanInsertException(ex);
            }
        }

        public void CreateLoanManually(UserDTO loggedUser)
        {
            int userId = LoanInput.GetUserIdFromInput();
            int materialId = LoanInput.GetMaterialIdFromInput();

            try
            {
                ValidateMaterialAvailability(materialId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                return;
            }

            var now = DateTime.Now;
            var expirationDate = now.AddDays(1);
            var reservation = new ReservationDTO
            {
                UserId = userId,
                MaterialId = materialId,
                RequestDate = now,
                ExpirationDate = expirationDate,
                ReservationStatus = (int)ReservationStatus.Accepted
            };

            try
            {
                _reservationDAO.CreateReservation(reservation);
            }
            catch (Exception ex)
            {
                throw new ReservationException.ReservationInsertException(ex);
            }

            var dueDate = now.AddDays(14);

            var loan = new LoanDTO
            {
                ReservationId = reservation.ReservationId,
                UserId = reservation.UserId,
                StartDate = now,
                DueDate = dueDate,
                ReturnDate = null,
                LoanStatus = (int)LoanStatus.Active
            };

            try
            {
                _loanDAO.CreateLoan(loan);
                _materialDAO.UpdateMaterialStatus(reservation.MaterialId, MaterialStatus.Loaned);
                Console.WriteLine("Préstamo creado exitosamente.");
            }
            catch (Exception ex)
            {
                throw new LoanException.LoanInsertException(ex);
            }

            Console.WriteLine("\nPresiona una tecla para continuar...");
            Console.ReadKey();
        }

        public void ExtendLoan()
        {
            int loanId = LoanInput.GetLoanIdFromInput();
            var loan = _loanDAO.GetLoanById(loanId);

            if (loan == null)
            {
                throw new LoanException.LoanNotFoundException();
            }

            if (loan.LoanStatus != (int)LoanStatus.Active)
            {
                Console.WriteLine("El préstamo no está activo. No se puede extender.");
                Console.WriteLine("\nPresiona una tecla para continuar...");
                Console.ReadKey();
                return;
            }

            if ((loan.DueDate - loan.StartDate).TotalDays > 14)
            {
                Console.WriteLine("Este préstamo ya fue extendido una vez. No se puede extender de nuevo.");
                Console.WriteLine("\nPresiona una tecla para continuar...");
                Console.ReadKey();
                return;
            }

            loan.DueDate = loan.DueDate.AddDays(7);

            try
            {
                _loanDAO.UpdateLoan(loan);
                Console.WriteLine("Préstamo extendido exitosamente.");
            }
            catch (Exception ex)
            {
                throw new LoanException.LoanUpdateException(ex);
            }

            Console.WriteLine("\nPresiona una tecla para continuar...");
            Console.ReadKey();
        }

        public void ReturnMaterial()
        {
            int loanId = LoanInput.GetLoanIdFromInput();
            var loan = _loanDAO.GetLoanById(loanId);
            var user = _userDAO.SearchUser(loan.UserId.ToString());

            if (loan == null)
            {
                throw new LoanException.LoanNotFoundException();
            }

            if (loan.LoanStatus != (int)LoanStatus.Active && loan.LoanStatus != (int)LoanStatus.Overdue)
            {
                Console.WriteLine("El préstamo no puede ser devuelto porque se encuentra cancelado o completado.");
                Console.WriteLine("\nPresiona una tecla para continuar...");
                Console.ReadKey();
                return;
            }

            var reservation = _reservationDAO.GetReservationById(loan.ReservationId);
            if (reservation == null)
            {
                throw new ReservationException.ReservationNotFoundException();
            }

            loan.ReturnDate = DateTime.Now;

            if (loan.ReturnDate > loan.DueDate)
            {
                loan.LoanStatus = (int)LoanStatus.Overdue;
                if (user.Arrears >= 5)
                {
                    Console.WriteLine($"El préstamo fue devuelto con atraso. El usuario tiene {user?.Arrears} atrasos, el usuario fue desactivado.");
                }
                else
                {
                    Console.WriteLine($"El préstamo fue devuelto con atraso. El usuario tiene {user?.Arrears} atrasos...");
                }
            }
            else
            {
                loan.LoanStatus = (int)LoanStatus.Completed;
                Console.WriteLine("Préstamo devuelto exitosamente.");
            }

            try
            {
                _loanDAO.UpdateLoan(loan);
                _materialDAO.UpdateMaterialStatus(reservation.MaterialId, MaterialStatus.Available);
            }
            catch (Exception ex)
            {
                throw new LoanException.LoanUpdateException(ex);
            }

            Console.WriteLine("\nPresiona una tecla para continuar...");
            Console.ReadKey();
        }

        public void CancelLoan()
        {
            int loanId = LoanInput.GetLoanIdFromInput();
            var loan = _loanDAO.GetLoanById(loanId);

            if (loan == null)
            {
                throw new LoanException.LoanNotFoundException();
            }

            if (loan.LoanStatus != (int)LoanStatus.Active)
            {
                Console.WriteLine("Solo se pueden cancelar préstamos activos.");
                Console.WriteLine("\nPresiona una tecla para continuar...");
                Console.ReadKey();
                return;
            }

            loan.LoanStatus = (int)LoanStatus.Canceled;

            try
            {
                _loanDAO.UpdateLoan(loan);
                _materialDAO.UpdateMaterialStatus(loan.ReservationId, MaterialStatus.Available);
                Console.WriteLine("Préstamo cancelado exitosamente.");
            }
            catch (Exception ex)
            {
                throw new LoanException.LoanUpdateException(ex);
            }

            Console.WriteLine("\nPresiona una tecla para continuar...");
            Console.ReadKey();
        }

        private void ValidateMaterialAvailability(int materialId)
        {
            var material = _materialDAO.SearchMaterial(materialId.ToString());

            if (material == null)
            {
                throw new MaterialException.MaterialNotFoundException(materialId);
            }

            switch (material.MaterialStatus)
            {
                case MaterialStatus.Reserved:
                    throw new LoanException.MaterialAlreadyReservedException(materialId);
                case MaterialStatus.Loaned:
                    throw new LoanException.MaterialAlreadyLoanedException(materialId);
                case MaterialStatus.Available:
                    return;
                default:
                    throw new Exception($"Estado de material desconocido para el material con ID {materialId}.");
            }
        }

        private void UpdateExpiredLoans()
        {
            var allLoans = _loanDAO.GetAllLoans();

            foreach (var loan in allLoans)
            {
                var status = loan.LoanStatus;

                if (loan.LoanStatus == (int)LoanStatus.Active &&
                    loan.DueDate < DateTime.Now)
                {
                    loan.LoanStatus = (int)LoanStatus.Overdue;
                    _loanDAO.UpdateLoan(loan);
                    var user = _userDAO.SearchUser(loan.UserId.ToString());

                    if (user != null)
                    {
                        user.Arrears += 1;

                        if (user.Arrears >= 5)
                        {
                            user.IsActive = false;
                            Console.WriteLine($"Usuario '{user.UserName}' ha sido desactivado por múltiples atrasos.");
                        }

                        _userDAO.UpdateUserArrears(user);
                    }
                }
            }
        }
    }
}
