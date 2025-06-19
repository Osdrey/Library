using Library.Application.DTOs;
using Library.Application.Mappers;
using Library.Infraestructure.Exceptions;
using Library.Infraestructure.Interfaces;
using Library.Infraestructure.Queries;
using Library.Infrastructure.Config;
using Microsoft.Data.SqlClient;

namespace Library.Infraestructure.DAOs
{
    internal class LoanDAO : ILoanDAO
    {
        private static readonly string _connectionString = DbConfig.ConnectionString;

        public List<LoanDTO> GetAllLoans()
        {
            try
            {
                var loans = new List<LoanDTO>();

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(LoanQueries.GetAllLoans, connection))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            loans.Add(LoanMapper.LoanDataReader(reader));
                        }
                    }
                }
                return loans;
            }
            catch (SqlException ex)
            {
                throw new DAOException.ConnectionException(ex);
            }
            catch (Exception ex)
            {
                throw new LoanDAOException.LoanListException(ex);
            }
        }

        public List<LoanDTO> GetLoansByUserId(int userId)
        {
            try
            {
                var loans = new List<LoanDTO>();
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(LoanQueries.GetLoansByUserId, connection))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                loans.Add(LoanMapper.LoanDataReader(reader));
                            }
                        }
                    }
                }
                return loans;
            }
            catch (SqlException ex)
            {
                throw new DAOException.ConnectionException(ex);
            }
            catch (Exception ex)
            {
                throw new LoanDAOException.LoanListException(ex);
            }
        }

        public LoanDTO? GetLoanById(int loanId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(LoanQueries.GetLoanById, connection))
                    {
                        cmd.Parameters.AddWithValue("@loanId", loanId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return LoanMapper.LoanDataReader(reader);
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
                throw new LoanDAOException.LoanListException(ex);
            }
        }

        public void InsertLoan(LoanDTO loan)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(LoanQueries.InsertLoan, connection))
                    {
                        cmd.Parameters.AddWithValue("@reservationId", loan.ReservationId);
                        cmd.Parameters.AddWithValue("@userId", loan.UserId);
                        cmd.Parameters.AddWithValue("@startDate", loan.StartDate);
                        cmd.Parameters.AddWithValue("@dueDate", loan.DueDate);
                        if (loan.ReturnDate == null)
                        {
                            cmd.Parameters.AddWithValue("@returnDate", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@returnDate", loan.ReturnDate);
                        }
                        cmd.Parameters.AddWithValue("@loanStatus", loan.LoanStatus);
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
                throw new LoanDAOException.LoanInsertException(ex);
            }
        }

        public void UpdateLoan(LoanDTO loan)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(LoanQueries.UpdateLoan, connection))
                    {
                        cmd.Parameters.AddWithValue("@dueDate", loan.DueDate);
                        cmd.Parameters.AddWithValue("@returnDate",
                            loan.ReturnDate == null ? (object)DBNull.Value : loan.ReturnDate);
                        cmd.Parameters.AddWithValue("@loanStatus", loan.LoanStatus);
                        cmd.Parameters.AddWithValue("@loanId", loan.LoanId);
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
                throw new LoanDAOException.LoanUpdateException(ex);
            }
        }

        public void DeleteLoan(int loanId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(LoanQueries.DeleteLoan, connection))
                    {
                        cmd.Parameters.AddWithValue("@loanId", loanId);
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
                throw new LoanDAOException.LoanDeleteException(ex);
            }
        }
    }
}
