using Library.Application.DTOs;
using Library.Domain.Enumerations;
using Library.Domain.Structures;
using Library.Infraestructure.Exceptions;
using Library.Infraestructure.Interfaces;
using Library.Infrastructure.Config;
using Microsoft.Data.SqlClient;

namespace Library.Infraestructure.DAOs
{
    public class MaterialDAO : IMaterialDAO
    {
        private static readonly string _connectionString = DbConfig.ConnectionString;

        public List<MaterialDTO> ViewAvailableMaterials()
        {
            try
            {
                var materials = new List<MaterialDTO>();

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Materials WHERE MaterialStatus = 0";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var material = MapMaterial(reader);
                            if (material != null)
                                materials.Add(material);
                        }
                    }
                }

                return materials;
            }
            catch (SqlException ex)
            {
                throw new DAOException.ConnectionException(ex);
            }
            catch (Exception ex)
            {
                throw new MaterialDAOException.MaterialListException(ex);
            }
        }

        public List<MaterialDTO> SearchAllMaterials(string input)
        {
            try
            {
                var materials = new List<MaterialDTO>();

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT * FROM Materials
                        WHERE 
                        Title LIKE '%' + @input + '%' OR
                        Author LIKE '%' + @input + '%' OR
                        CAST(PublicationYear AS NVARCHAR) LIKE '%' + @input + '%'";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@input", input);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                materials.Add(MapMaterial(reader));
                            }
                        }
                    }
                }

                return materials;
            }
            catch (SqlException ex)
            {
                throw new DAOException.ConnectionException(ex);
            }
            catch (Exception ex)
            {
                throw new MaterialDAOException.MaterialListException(ex);
            }
        }

        public MaterialDTO? SearchMaterial(string materialId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Materials WHERE MaterialId = @materialId";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@materialId", materialId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapMaterial(reader);
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
            catch (Exception ex)
            {
                throw new MaterialDAOException.MaterialSearchException(ex);
            }
        }

        public bool IsMaterialAvailable(int materialId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "SELECT MaterialStatus FROM Materials WHERE MaterialId = @materialId";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@materialId", materialId);

                        var status = cmd.ExecuteScalar()?.ToString();
                        return status == MaterialStatus.Available.ToString();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new DAOException.ConnectionException(ex);
            }
            catch (Exception ex)
            {
                throw new MaterialDAOException.MaterialAvailabilityException(ex);
            }
        }

        public void CreateMaterial(MaterialDTO material)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"
                        INSERT INTO Materials
                        (Title, Author, PublicationYear, MaterialStatus, MaterialCondition, MaterialTopic, Pages, Format, Duration, MaterialType)
                        VALUES
                        (@Title, @Author, @PublicationYear, @MaterialStatus, @MaterialCondition, @MaterialTopic, @Pages, @Format, @Duration, @MaterialType)";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Title", material.Title);
                        cmd.Parameters.AddWithValue("@Author", material.Author);
                        cmd.Parameters.AddWithValue("@PublicationYear", material.PublicationYear);
                        cmd.Parameters.AddWithValue("@MaterialStatus", (int)material.MaterialStatus);
                        cmd.Parameters.AddWithValue("@MaterialCondition", (int)material.MaterialCondition);
                        cmd.Parameters.AddWithValue("@MaterialTopic", material.MaterialTopic.ToString());

                        if (material is BookDTO book)
                        {
                            cmd.Parameters.AddWithValue("@Pages", book.Pages);
                            cmd.Parameters.AddWithValue("@Format", DBNull.Value);
                            cmd.Parameters.AddWithValue("@Duration", DBNull.Value);
                            cmd.Parameters.AddWithValue("@MaterialType", "Book");
                        }
                        else if (material is AudioVisualDTO av)
                        {
                            cmd.Parameters.AddWithValue("@Pages", DBNull.Value);
                            cmd.Parameters.AddWithValue("@Format", av.Format);
                            cmd.Parameters.AddWithValue("@Duration", av.Duration);
                            cmd.Parameters.AddWithValue("@MaterialType", "AudioVisual");
                        }
                        else
                        {
                            throw new ArgumentException("Tipo de material desconocido.");
                        }

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
                throw new MaterialDAOException.MaterialInsertException(ex);
            }
        }

        public void UpdateMaterial(MaterialDTO material)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"
                        UPDATE Materials SET
                            Title = @Title,
                            Author = @Author,
                            PublicationYear = @PublicationYear,
                            MaterialStatus = @MaterialStatus,
                            MaterialCondition = @MaterialCondition,
                            MaterialTopic = @MaterialTopic,
                            Pages = @Pages,
                            Format = @Format,
                            Duration = @Duration
                        WHERE MaterialId = @MaterialId";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@MaterialId", material.MaterialId);
                        cmd.Parameters.AddWithValue("@Title", material.Title);
                        cmd.Parameters.AddWithValue("@Author", material.Author);
                        cmd.Parameters.AddWithValue("@PublicationYear", material.PublicationYear);
                        cmd.Parameters.AddWithValue("@MaterialStatus", (int)material.MaterialStatus);
                        cmd.Parameters.AddWithValue("@MaterialCondition", (int)material.MaterialCondition);
                        cmd.Parameters.AddWithValue("@MaterialTopic", material.MaterialTopic.ToString());

                        if (material is BookDTO book)
                        {
                            cmd.Parameters.AddWithValue("@Pages", book.Pages);
                            cmd.Parameters.AddWithValue("@Format", DBNull.Value);
                            cmd.Parameters.AddWithValue("@Duration", DBNull.Value);
                        }
                        else if (material is AudioVisualDTO av)
                        {
                            cmd.Parameters.AddWithValue("@Pages", DBNull.Value);
                            cmd.Parameters.AddWithValue("@Format", av.Format);
                            cmd.Parameters.AddWithValue("@Duration", av.Duration);
                        }

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
                throw new MaterialDAOException.MaterialUpdateException(ex);
            }
        }

        public void UpdateMaterialStatus(int materialId, MaterialStatus status)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "UPDATE Materials SET MaterialStatus = @status WHERE MaterialId = @materialId";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@status", (int)status);
                        cmd.Parameters.AddWithValue("@materialId", materialId);
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
                throw new MaterialDAOException.MaterialStatusChangeException(ex);
            }
        }

        public void DeleteMaterial(int materialId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "DELETE FROM Materials WHERE MaterialId = @materialId";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@materialId", materialId);
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
                throw new MaterialDAOException.MaterialDeleteException(ex);
            }
        }

        private MaterialDTO? MapMaterial(SqlDataReader reader)
        {
            string type = reader["MaterialType"].ToString() ?? "";

            var common = new
            {
                MaterialId = Convert.ToInt32(reader["MaterialId"]),
                Title = reader["Title"].ToString() ?? "",
                Author = reader["Author"].ToString() ?? "",
                PublicationYear = Convert.ToInt32(reader["PublicationYear"]),
                MaterialStatus = Enum.Parse<MaterialStatus>(reader["MaterialStatus"].ToString() ?? "Available"),
                MaterialCondition = Enum.Parse<MaterialCondition>(reader["MaterialCondition"].ToString() ?? "New"),
                MaterialTopic = MaterialTopic.FromString(reader["MaterialTopic"].ToString() ?? "")
            };

            switch (type)
            {
                case "Book":
                    return new BookDTO
                    {
                        MaterialId = common.MaterialId,
                        Title = common.Title,
                        Author = common.Author,
                        PublicationYear = common.PublicationYear,
                        MaterialStatus = common.MaterialStatus,
                        MaterialCondition = common.MaterialCondition,
                        MaterialTopic = common.MaterialTopic,
                        Pages = Convert.ToInt32(reader["Pages"])
                    };

                case "Audiovisual":
                    return new AudioVisualDTO
                    {
                        MaterialId = common.MaterialId,
                        Title = common.Title,
                        Author = common.Author,
                        PublicationYear = common.PublicationYear,
                        MaterialStatus = common.MaterialStatus,
                        MaterialCondition = common.MaterialCondition,
                        MaterialTopic = common.MaterialTopic,
                        Format = reader["Format"].ToString() ?? "",
                        Duration = reader["Duration"].ToString() ?? ""
                    };

                default:
                    throw new MaterialDAOException.MaterialTypeUnknownException(type, reader["MaterialId"].ToString() ?? "desconocido");
            }
        }
    }
}