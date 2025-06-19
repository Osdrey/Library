using Library.Application.DTOs;
using Library.Application.Mappers;
using Library.Infraestructure.Exceptions;
using Library.Infraestructure.Interfaces;
using Library.Infraestructure.Queries;
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
                    using (SqlCommand cmd = new SqlCommand(ReservationQueries.GetAllReservations, connection))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            reservations.Add(ReservationMapper.ReservationDataReader(reader));
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
                    using (SqlCommand cmd = new SqlCommand(ReservationQueries.GetPendingReservations, connection))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            reservations.Add(ReservationMapper.ReservationDataReader(reader));
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
                    using (SqlCommand cmd = new SqlCommand(ReservationQueries.GetReservationsByUserId, connection))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                reservations.Add(ReservationMapper.ReservationDataReader(reader));
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
                    using (SqlCommand cmd = new SqlCommand(ReservationQueries.GetReservationById, connection))
                    {
                        cmd.Parameters.AddWithValue("@reservationId", reservationId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return ReservationMapper.ReservationDataReader(reader);
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

        public void InsertReservation(ReservationDTO r)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(ReservationQueries.InsertReservation, connection))
                    {
                        cmd.Parameters.AddWithValue("@userId", r.UserId);
                        cmd.Parameters.AddWithValue("@materialId", r.MaterialId);
                        cmd.Parameters.AddWithValue("@requestDate", r.RequestDate);
                        cmd.Parameters.AddWithValue("@expirationDate", r.ExpirationDate);
                        cmd.Parameters.AddWithValue("@reservationStatus", r.ReservationStatus);
                        cmd.ExecuteNonQuery();
                        r.ReservationId = (int)cmd.ExecuteScalar();
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
                    using (SqlCommand cmd = new SqlCommand(ReservationQueries.UpdateReservation, connection))
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
                    using (SqlCommand cmd = new SqlCommand(ReservationQueries.DeleteReservation, connection))
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
    }
}
