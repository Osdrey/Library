using Library.Application.DTOs;
using Library.Domain.Entities;
using Microsoft.Data.SqlClient;

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
        public static ReservationDTO ReservationDataReader(SqlDataReader reader)
        {
            return new ReservationDTO
            {
                ReservationId = Convert.ToInt32(reader["ReservationId"]),
                UserId = Convert.ToInt32(reader["UserId"]),
                MaterialId = Convert.ToInt32(reader["MaterialId"]),
                RequestDate = Convert.ToDateTime(reader["RequestDate"]),
                ExpirationDate = Convert.ToDateTime(reader["ExpirationDate"]),
                ReservationStatus = Convert.ToInt32(reader["ReservationStatus"])
            };
        }
    }
}
