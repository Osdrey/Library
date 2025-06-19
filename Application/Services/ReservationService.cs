using Library.Application.DTOs;
using Library.Application.Exceptions;
using Library.Application.Interfaces;
using Library.Domain.Enumerations;
using Library.Infraestructure.Interfaces;
using Library.Presentation.UI.Inputs;
using Library.Presentation.UI.Printers;

namespace Library.Application.Services
{
    internal class ReservationService : IReservationService
    {
        private readonly IReservationDAO _reservationDAO;
        private readonly IMaterialDAO _materialDAO;
        private readonly ILoanService _loanService;

        public ReservationService(IReservationDAO reservationDAO, IMaterialDAO materialDAO, ILoanService loanService)
        {
            _reservationDAO = reservationDAO;
            _materialDAO = materialDAO;
            _loanService = loanService;
        }

        public void ListReservation()
        {
            UpdateExpiredReservations();
            var reservations = _reservationDAO.GetAllReservations();

            if (reservations.Count == 0)
            {
                throw new ReservationException.ReservationNotFoundException();
            }

            Console.WriteLine("\nLista de reservas hechas:");

            foreach (var reservation in reservations)
            {
                ReservationPrinter.Print(reservation);
            }
            Console.WriteLine("\nPresiona una tecla para regresar al menú...");
            Console.ReadKey();
        }

        public void ListPendingReservation()
        {
            UpdateExpiredReservations();
            var reservations = _reservationDAO.GetPendingReservations();

            if (reservations.Count == 0)
            {
                Console.WriteLine("No se encontraron reservas pendientes.");
            }

            Console.WriteLine("\nLista de reservas pendientes:");

            foreach (var reservation in reservations)
            {
                ReservationPrinter.Print(reservation);
            }
            Console.WriteLine("\nPresiona una tecla para regresar al menú...");
            Console.ReadKey();
        }

        public void ListUserReservations(UserDTO loggedUser)
        {
            UpdateExpiredReservations();
            int userId;
            List<ReservationDTO> reservations;

            if (loggedUser.UserRole == UserRole.Administrator)
            {
                userId = ReservationInput.GetUserIdFromInput();
            }
            else
            {
                userId = loggedUser.Id;
            }

            reservations = _reservationDAO.GetReservationsByUserId(userId);

            if (reservations.Count == 0)
            {
                Console.WriteLine("Este usuario no tiene reservas registradas.");
                return;
            }

            Console.WriteLine("\nLista de reservas hechas:");

            foreach (var reservation in reservations)
            {
                ReservationPrinter.Print(reservation);
            }
            Console.WriteLine("\nPresiona una tecla para regresar al menú...");
            Console.ReadKey();
        }

        public void SearchReservation()
        {
            UpdateExpiredReservations();
            int id = ReservationInput.GetReservationIdFromInput();
            var reservation = _reservationDAO.GetReservationById(id);
            if (reservation == null)
            {
                throw new ReservationException.ReservationNotFoundException();
            }
            else
            {
                ReservationPrinter.Print(reservation);
            }
            Console.WriteLine("\nPresiona una tecla para regresar al menú...");
            Console.ReadKey();
        }

        public void CreateReservation(UserDTO loggedUser)
        {
            int userId;

            if (loggedUser.UserRole == UserRole.Administrator)
            {
                userId = ReservationInput.GetUserIdFromInput();
            }
            else
            {
                userId = loggedUser.Id;
            }

            int materialId = ReservationInput.GetMaterialIdFromInput();
            var material = _materialDAO.GetMaterial(materialId.ToString());
            if (material?.MaterialStatus != MaterialStatus.Available)
            {
                Console.WriteLine("El material no se encuentra disponible.");
                Console.WriteLine("\nPresiona una tecla para continuar...");
                Console.ReadKey();
                return;
            }

            if (loggedUser.UserRole != UserRole.Administrator)
            {
                var userReservations = _reservationDAO.GetReservationsByUserId(userId);

                bool hasRecentMaterialReservation = userReservations.Any(r =>
                    r.MaterialId == materialId &&
                    r.ReservationStatus == (int)ReservationStatus.Expired &&
                    (DateTime.Now - r.ExpirationDate).TotalDays < 7);

                if (hasRecentMaterialReservation)
                {
                    Console.WriteLine("No puedes reservar este material aún. Debes esperar 7 días después de la última expiración.");
                    Console.WriteLine("\nPresiona una tecla para continuar...");
                    Console.ReadKey();
                    return;
                }
            }

            var now = DateTime.Now;
            var expiration = now.AddDays(3);

            var reservation = new ReservationDTO
            {
                UserId = userId,
                MaterialId = materialId,
                RequestDate = now,
                ExpirationDate = expiration,
                ReservationStatus = (int)ReservationStatus.Pending
            };

            _reservationDAO.InsertReservation(reservation);
            _materialDAO.UpdateMaterialStatus(reservation.MaterialId, MaterialStatus.Reserved);
            Console.WriteLine("Reserva creada exitosamente.");
            Console.WriteLine("\nPresiona una tecla para continuar...");
            Console.ReadKey();
        }

        private bool IsReservationModifiable(ReservationDTO reservation, string action)
        {
            var status = reservation.ReservationStatus;

            switch (status)
            {
                case (int)ReservationStatus.Accepted:
                    Console.WriteLine($"La reserva ya ha sido aceptada, no se puede {action}.");
                    return false;
                case (int)ReservationStatus.Expired:
                    Console.WriteLine($"La reserva ya ha expirado, no se puede {action}.");
                    return false;
                case (int)ReservationStatus.Rejected:
                    Console.WriteLine($"La reserva ya ha sido rechazada, no se puede {action}.");
                    return false;
                case (int)ReservationStatus.Canceled:
                    Console.WriteLine($"La reserva ya ha sido cancelada, no se puede {action}.");
                    return false;
                case (int)ReservationStatus.Pending:
                    return true;
                default:
                    throw new ReservationException.ReservationInvalidActionException("Estado de reserva no reconocido.");
            }
        }

        public void ExtendReservation()
        {
            int reservationId = ReservationInput.GetReservationIdFromInput();
            var reservation = _reservationDAO.GetReservationById(reservationId);

            if (reservation == null)
            {
                throw new ReservationException.ReservationNotFoundException();
            }

            else if (!IsReservationModifiable(reservation, "extender"))
            {
                Console.WriteLine("\nPresiona una tecla para continuar...");
                Console.ReadKey();
                return;
            }

            else if ((reservation.ExpirationDate - reservation.RequestDate).TotalDays > 3)
            {
                Console.WriteLine("Esta reserva ya fue extendida una vez. No se puede extender de nuevo.");
                Console.WriteLine("\nPresiona una tecla para continuar...");
                Console.ReadKey();
                return;
            }

            else
            {
                reservation.ExpirationDate = reservation.ExpirationDate.AddDays(2);
                _reservationDAO.UpdateReservation(reservation);
                Console.WriteLine("Reserva extendida exitosamente.");
                Console.WriteLine("\nPresiona una tecla para continuar...");
                Console.ReadKey();
            }
        }

        public void AcceptReservation()
        {
            int id = ReservationInput.GetReservationIdFromInput();
            var reservation = _reservationDAO.GetReservationById(id);

            if (reservation == null)
            {
                throw new ReservationException.ReservationNotFoundException();
            }

            else if (!IsReservationModifiable(reservation, "aceptar"))
            {
                Console.WriteLine("\nPresiona una tecla para continuar...");
                Console.ReadKey();
                return;
            }

            reservation.ReservationStatus = (int)ReservationStatus.Accepted;
            _reservationDAO.UpdateReservation(reservation);
            _loanService.CreateLoanFromReservation(reservation);
            _materialDAO.UpdateMaterialStatus(reservation.MaterialId, MaterialStatus.Loaned);
            Console.WriteLine("Reserva aceptada exitosamente.");
            Console.WriteLine("\nPresiona una tecla para continuar...");
            Console.ReadKey();
        }

        public void CancelReservation()
        {
            int id = ReservationInput.GetReservationIdFromInput();
            var reservation = _reservationDAO.GetReservationById(id);

            if (reservation == null)
            {
                throw new ReservationException.ReservationNotFoundException();
            }

            else if (!IsReservationModifiable(reservation, "cancelar"))
            {
                Console.WriteLine("\nPresiona una tecla para continuar...");
                Console.ReadKey();
                return;
            }

            reservation.ReservationStatus = (int)ReservationStatus.Canceled;
            _reservationDAO.UpdateReservation(reservation);
            _materialDAO.UpdateMaterialStatus(reservation.MaterialId, MaterialStatus.Available);
            Console.WriteLine("La reserva ha sido cancelada exitosamente.");
            Console.WriteLine("\nPresiona una tecla para continuar...");
            Console.ReadKey();
        }

        public void RejectReservation()
        {
            int id = ReservationInput.GetReservationIdFromInput();
            var reservation = _reservationDAO.GetReservationById(id);

            if (reservation == null)
            {
                throw new ReservationException.ReservationNotFoundException();
            }

            else if (!IsReservationModifiable(reservation, "rechazar"))
            {
                Console.WriteLine("\nPresiona una tecla para continuar...");
                Console.ReadKey();
                return;
            }

            reservation.ReservationStatus = (int)ReservationStatus.Rejected;
            _reservationDAO.UpdateReservation(reservation);
            _materialDAO.UpdateMaterialStatus(reservation.MaterialId, MaterialStatus.Available);
            Console.WriteLine("Reserva rechazada exitosamente.");
            Console.WriteLine("\nPresiona una tecla para continuar...");
            Console.ReadKey();
        }

        private void UpdateExpiredReservations()
        {
            var allReservations = _reservationDAO.GetAllReservations();

            foreach (var reservation in allReservations)
            {
                var status = reservation.ReservationStatus;

                if (reservation.ReservationStatus == (int)ReservationStatus.Pending &&
                    reservation.ExpirationDate < DateTime.Now)
                {
                    reservation.ReservationStatus = (int)ReservationStatus.Expired;
                    _reservationDAO.UpdateReservation(reservation);
                    _materialDAO.UpdateMaterialStatus(reservation.MaterialId, MaterialStatus.Available);
                }
            }
        }
    }
}
