using Library.Application.DTOs;
using Library.Infraestructure.Exceptions;
using Library.Infraestructure.Interfaces;
using Library.Infrastructure.Config;
using Microsoft.Data.SqlClient;

namespace Library.Infraestructure.DAOs
{
    internal class ReservationDAO : IReservationDAO
    {
        private static readonly string _connectionString = DbConfig.ConnectionString;

        public List<ReservationDTO> GetAllReservations()
        {
            try
            {
                var reservations = new List<ReservationDTO>();

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Reservations";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            reservations.Add(MapReservation(reader));
                        }
                    }
                }

                return reservations;
            }
            catch (SqlException ex)
            {
                throw new DAOException.ConnectionException(ex);
            }
            catch (Exception ex)
            {
                throw new ReservationDAOException.ReservationListException(ex);
            }
        }

        public List<ReservationDTO> GetPendingReservations()
        {
            try
            {
                var reservations = new List<ReservationDTO>();

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Reservations WHERE ReservationStatus = 0";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            reservations.Add(MapReservation(reader));
                        }
                    }
                }

                return reservations;
            }
            catch (SqlException ex)
            {
                throw new DAOException.ConnectionException(ex);
            }
            catch (Exception ex)
            {
                throw new ReservationDAOException.ReservationListException(ex);
            }
        }

        public List<ReservationDTO> GetReservationsByUserId(int userId)
        {
            try
            {
                var reservations = new List<ReservationDTO>();
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Reservations WHERE UserId = @userId";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                reservations.Add(MapReservation(reader));
                            }
                        }
                    }
                }

                return reservations;
            }
            catch (SqlException ex)
            {
                throw new DAOException.ConnectionException(ex);
            }
            catch (Exception ex)
            {
                throw new ReservationDAOException.ReservationListException(ex);
            }
        }

        public ReservationDTO? GetReservationById(int reservationId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT * FROM Reservations
                        WHERE ReservationId = @reservationId";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@reservationId", reservationId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapReservation(reader);
                            }
                        }
                    }
                }

                return null;
            }
            catch (SqlException ex)
            {
                throw new DAOException.ConnectionException(ex);
            }
            catch (Exception ex) when (ex is SqlException)
            {
                throw new ReservationDAOException.ReservationListException(ex);
            }
        }

        public void CreateReservation(ReservationDTO r)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"
                        INSERT INTO Reservations (UserId, MaterialId, RequestDate, ExpirationDate, ReservationStatus)
                        VALUES (@userId, @materialId, @requestDate, @expirationDate, @reservationStatus)";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@userId", r.UserId);
                        cmd.Parameters.AddWithValue("@materialId", r.MaterialId);
                        cmd.Parameters.AddWithValue("@requestDate", r.RequestDate);
                        cmd.Parameters.AddWithValue("@expirationDate", r.ExpirationDate);
                        cmd.Parameters.AddWithValue("@reservationStatus", r.ReservationStatus);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DAOException.ConnectionException(ex);
            }
            catch (Exception ex)
            {
                throw new ReservationDAOException.ReservationInsertException(ex);
            }
        }

        public void UpdateReservation(ReservationDTO r)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"
                        UPDATE Reservations
                        SET ExpirationDate = @expirationDate,
                            ReservationStatus = @reservationStatus
                        WHERE ReservationId = @reservationId";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@expirationDate", r.ExpirationDate);
                        cmd.Parameters.AddWithValue("@reservationStatus", r.ReservationStatus);
                        cmd.Parameters.AddWithValue("@reservationId", r.ReservationId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DAOException.ConnectionException(ex);
            }
            catch (Exception ex)
            {
                throw new ReservationDAOException.ReservationUpdateException(ex);
            }
        }

        public void DeleteReservation(int reservationId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    const string query = @"
                        DELETE FROM Reservations
                        WHERE ReservationId = @reservationId";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@reservationId", reservationId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DAOException.ConnectionException(ex);
            }
            catch (Exception ex)
            {
                throw new ReservationDAOException.ReservationDeleteException(ex);
            }
        }

        private ReservationDTO MapReservation(SqlDataReader reader)
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
