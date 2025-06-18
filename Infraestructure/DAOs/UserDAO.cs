using Library.Application.DTOs;
using Library.Application.Exceptions;
using Library.Domain.Enumerations;
using Library.Infraestructure.Exceptions;
using Library.Infraestructure.Interfaces;
using Library.Infrastructure.Config;
using Microsoft.Data.SqlClient;

namespace Library.Infraestructure.DAOs
{
    public class UserDAO : IUserDAO
    {
        private static readonly string _connectionString = DbConfig.ConnectionString;

        public List<UserDTO> SearchAllUsers()
        {
            try
            {
                var users = new List<UserDTO>();

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Users WHERE isActive = 1";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(MapUser(reader));
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

        public UserDTO? SearchUser(string input)
        {
            try
            {
                UserDTO? user = null;

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"
                    SELECT * FROM Users 
                    WHERE CAST(Id AS NVARCHAR) = @input OR
                          CAST(Document AS NVARCHAR) = @input OR
                          Email = @input OR
                          UserName = @input";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@input", input);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                user = MapUser(reader);
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

        public void CreateUser(UserDTO user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"
                    INSERT INTO Users 
                    (document, firstName, lastName, middleName, age, email, userName, password, userType, userRole, arrears, isActive)
                    VALUES 
                    (@document, @firstName, @lastName, @middleName, @age, @email, @userName, @password, @userType, @userRole, @arrears, @isActive);";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
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

                    string query = @"
                    UPDATE Users SET 
                        firstName = @firstName,
                        lastName = @lastName,
                        middleName = @middleName,
                        age = @age,
                        email = @email,
                        userName = @userName,
                        password = @password,
                        userType = @userType,
                        userRole = @userRole,
                        arrears = @arrears,
                        isActive = @isActive
                    WHERE document = @document";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
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

                    string query = "UPDATE Users SET isActive = 1 WHERE document = @document";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
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

                    string query = "UPDATE Users SET isActive = 0 WHERE document = @document";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
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

                    string query = "UPDATE Users SET password = @password WHERE userName = @username";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
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
                    const string query = @"
                        UPDATE Users
                        SET Arrears = @arrears,
                            IsActive = @isActive
                        WHERE Id = @userId";

                    using (var cmd = new SqlCommand(query, connection))
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

        private UserDTO MapUser(SqlDataReader reader)
        {
            return new UserDTO
            {
                Id = Convert.ToInt32(reader["Id"]), 
                Document = Convert.ToInt32(reader["document"]),
                FirstName = reader["firstName"].ToString() ?? "",
                LastName = reader["lastName"].ToString() ?? "",
                MiddleName = reader["middleName"].ToString() ?? "",
                Age = Convert.ToInt32(reader["age"]),
                Email = reader["email"].ToString() ?? "",
                UserName = reader["userName"].ToString() ?? "",
                Password = reader["password"].ToString() ?? "",
                UserType = Enum.Parse<UserType>(reader["userType"].ToString() ?? "Regular"),
                UserRole = Enum.Parse<UserRole>(reader["userRole"].ToString() ?? "User"),
                Arrears = Convert.ToInt32(reader["arrears"]),
                IsActive = Convert.ToBoolean(reader["isActive"])
            };
        }
    }
}

