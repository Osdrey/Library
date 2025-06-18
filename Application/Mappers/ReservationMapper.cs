using Library.Application.DTOs;
using Library.Domain.Entities;

namespace Library.Application.Mappers
{
    public static class ReservationMapper
    {
        public static ReservationDTO ToDTO(Reservation reservation)
        {
            return new ReservationDTO
            {
                ReservationId = reservation.ReservationId,
                UserId = reservation.User.Id,
                MaterialId = reservation.Material.MaterialId,
                RequestDate = reservation.RequestDate,
                ExpirationDate = reservation.ExpirationDate,
                ReservationStatus = (int)reservation.ReservationStatus
            };
        }
    }
}
