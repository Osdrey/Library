using Library.Application.DTOs;
using Library.Application.Mappers;
using Library.Domain.Enumerations;
using Library.Infraestructure.Exceptions;
using Library.Infraestructure.Interfaces;
using Library.Infraestructure.Queries;
using Library.Infrastructure.Config;
using Microsoft.Data.SqlClient;

namespace Library.Infraestructure.DAOs
{
    public class MaterialDAO : IMaterialDAO
    {
        private static readonly string _connectionString = DbConfig.ConnectionString;

        public List<MaterialDTO> GetAvailableMaterials()
        {
            try
            {
                var materials = new List<MaterialDTO>();
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(MaterialQueries.GetAvailableMaterials, connection))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var material = MaterialMapper.MaterialDataReader(reader);
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

        public List<MaterialDTO> GetAllMaterials(string input)
        {
            try
            {
                var materials = new List<MaterialDTO>();
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(MaterialQueries.GetAllMaterials, connection))
                    {
                        cmd.Parameters.AddWithValue("@input", input);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                materials.Add(MaterialMapper.MaterialDataReader(reader));
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

        public MaterialDTO? GetMaterial(string materialId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(MaterialQueries.GetMaterial, connection))
                    {
                        cmd.Parameters.AddWithValue("@materialId", materialId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MaterialMapper.MaterialDataReader(reader);
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
                    using (SqlCommand cmd = new SqlCommand(MaterialQueries.IsMaterialAvailable, connection))
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

        public void InsertMaterial(MaterialDTO material)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(MaterialQueries.InsertMaterial, connection))
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
                    using (SqlCommand cmd = new SqlCommand(MaterialQueries.UpdateMaterial, connection))
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
                    using (SqlCommand cmd = new SqlCommand(MaterialQueries.UpdateMaterialStatus, connection))
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
                    using (SqlCommand cmd = new SqlCommand(MaterialQueries.DeleteMaterial, connection))
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
    }
}