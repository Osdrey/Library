using Library.Application.DTOs;
using Library.Application.Mappers;
using Library.Infraestructure.Exceptions;
using Library.Infraestructure.Interfaces;
using Library.Infraestructure.Queries;
using Library.Infrastructure.Config;
using Microsoft.Data.SqlClient;

namespace Library.Infraestructure.DAOs
{

    public class UserDAO : IUserDAO
    {
        private static readonly string _connectionString = DbConfig.ConnectionString;

        public List<UserDTO> GetAllUsers()
        {
            try
            {
                var users = new List<UserDTO>();

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(UserQueries.GetAllActiveUsers, connection))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(UserMapper.UserDataReader(reader));
                        }
                    }
                }
                return users;
            }
            catch (SqlException ex)
            {
                throw new DAOException.ConnectionException(ex);
            }
            catch (Exception ex)
            {
                throw new UserDAOException.UserListException(ex);
            }
        }

        public UserDTO? GetUser(string input)
        {
            try
            {
                UserDTO? user = null;
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand(UserQueries.GetUserByInput, connection))
                    {
                        cmd.Parameters.AddWithValue("@input", input);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                user = UserMapper.UserDataReader(reader);
                            }
                        }
                    }
                }
                return user;
            }
            catch (SqlException ex)
            {
                throw new DAOException.ConnectionException(ex);
            }
            catch (Exception ex)
            {
                throw new UserDAOException.UserSearchException(ex);
            }
        }

        public void InsertUser(UserDTO user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(UserQueries.InsertUser, connection))
                    {
                        cmd.Parameters.AddWithValue("@document", user.Document);
                        cmd.Parameters.AddWithValue("@firstName", user.FirstName);
                        cmd.Parameters.AddWithValue("@lastName", user.LastName);
                        cmd.Parameters.AddWithValue("@middleName", user.MiddleName);
                        cmd.Parameters.AddWithValue("@age", user.Age);
                        cmd.Parameters.AddWithValue("@email", user.Email);
                        cmd.Parameters.AddWithValue("@userName", user.UserName);
                        cmd.Parameters.AddWithValue("@password", user.Password);
                        cmd.Parameters.AddWithValue("@userType", user.UserType);
                        cmd.Parameters.AddWithValue("@userRole", user.UserRole);
                        cmd.Parameters.AddWithValue("@arrears", user.Arrears);
                        cmd.Parameters.AddWithValue("@isActive", user.IsActive);
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
                throw new UserDAOException.UserInsertException(ex);
            }
        }

        public void UpdateUser(UserDTO user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(UserQueries.UpdateUser, connection))
                    {
                        cmd.Parameters.AddWithValue("@document", user.Document);
                        cmd.Parameters.AddWithValue("@firstName", user.FirstName);
                        cmd.Parameters.AddWithValue("@lastName", user.LastName);
                        cmd.Parameters.AddWithValue("@middleName", user.MiddleName);
                        cmd.Parameters.AddWithValue("@age", user.Age);
                        cmd.Parameters.AddWithValue("@email", user.Email);
                        cmd.Parameters.AddWithValue("@userName", user.UserName);
                        cmd.Parameters.AddWithValue("@password", user.Password);
                        cmd.Parameters.AddWithValue("@userType", user.UserType);
                        cmd.Parameters.AddWithValue("@userRole", user.UserRole);
                        cmd.Parameters.AddWithValue("@arrears", user.Arrears);
                        cmd.Parameters.AddWithValue("@isActive", user.IsActive);
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
                throw new UserDAOException.UserUpdateException(ex);
            }
        }

        public void ReactivateUser(int document)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(UserQueries.ReactivateUserByDocument, connection))
                    {
                        cmd.Parameters.AddWithValue("@document", document);
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
                throw new UserDAOException.UserStatusChangeException("activar", ex);
            }
        }

        public void DeactivateUser(int document)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(UserQueries.DeactivateUserByDocument, connection))
                    {
                        cmd.Parameters.AddWithValue("@document", document);
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
                throw new UserDAOException.UserStatusChangeException("desactivar", ex);
            }
        }

        public void ChangePassword(string username, string newPasswordHash)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(UserQueries.UpdateUserPassword, connection))
                    {
                        cmd.Parameters.AddWithValue("@password", newPasswordHash);
                        cmd.Parameters.AddWithValue("@username", username);
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
                throw new UserDAOException.PasswordChangeException(ex);
            }
        }

        public void UpdateUserArrears(UserDTO user)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(UserQueries.UpdateUserArrears, connection))
                    {
                        cmd.Parameters.AddWithValue("@arrears", user.Arrears);
                        cmd.Parameters.AddWithValue("@isActive", user.IsActive);
                        cmd.Parameters.AddWithValue("@userId", user.Id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new UserDAOException.UserUpdateException(ex);
            }
        }
    }
}