using Library.Application.DTOs;
using Library.Infraestructure.Exceptions;
using Library.Infraestructure.Interfaces;
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
                    string query = "SELECT * FROM Loans";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            loans.Add(MapLoan(reader));
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

                    string query = "SELECT * FROM Loans WHERE UserId = @userId";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                loans.Add(MapLoan(reader));
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

                    string query = @"
                        SELECT * FROM Loans
                        WHERE LoanId = @loanId";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@loanId", loanId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapLoan(reader);
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

        public void CreateLoan(LoanDTO loan)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"
                        INSERT INTO Loans (UserId, ReservationId, StartDate, DueDate, ReturnDate, LoanStatus)
                        VALUES (@userId, @reservationId, @startDate, @dueDate, @returnDate, @loanStatus)";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@reservationId", loan.ReservationId);
                        cmd.Parameters.AddWithValue("@userId", loan.UserId);
                        cmd.Parameters.AddWithValue("@startDate", loan.StartDate);
                        cmd.Parameters.AddWithValue("@dueDate", loan.DueDate);

                        if (loan.ReturnDate == null)
                            cmd.Parameters.AddWithValue("@returnDate", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@returnDate", loan.ReturnDate);

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

                    string query = @"
                        UPDATE Loans
                        SET 
                            DueDate = @dueDate,
                            ReturnDate = @returnDate,
                            LoanStatus = @loanStatus
                        WHERE LoanId = @loanId";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
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

                    const string query = @"
                        DELETE FROM Loans
                        WHERE LoanId = @loanId";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
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

        private LoanDTO MapLoan(SqlDataReader reader)
        {
            return new LoanDTO
            {
                LoanId = Convert.ToInt32(reader["LoanId"]),
                ReservationId = Convert.ToInt32(reader["ReservationId"]),
                UserId = Convert.ToInt32(reader["UserId"]),
                StartDate = Convert.ToDateTime(reader["StartDate"]),
                DueDate = Convert.ToDateTime(reader["DueDate"]),
                ReturnDate = reader["ReturnDate"] == DBNull.Value
                                ? null
                                : Convert.ToDateTime(reader["ReturnDate"]),
                LoanStatus = Convert.ToInt32(reader["LoanStatus"])
            };
        }
    }
}
