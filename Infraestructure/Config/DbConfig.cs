using Microsoft.Data.SqlClient;
using static Library.Infraestructure.Exceptions.DAOException;

namespace Library.Infrastructure.Config
{
    public static class DbConfig
    {
        public static string ConnectionString
        {
            get
            {
                try
                {
                    string conn = "Server=.;Database=Library;Trusted_Connection=True;TrustServerCertificate=True";

                    if (string.IsNullOrWhiteSpace(conn))
                    {
                        throw new ConfigurationException("La cadena de conexión está vacía.");
                    }

                    return conn;
                }
                catch (ConfigurationException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw new ConfigurationException("No se pudo obtener la cadena de conexión.", ex);
                }
            }
        }

        public static void TestConnection()
        {
            try
            {
                using (var connection = new SqlConnection(DbConfig.ConnectionString))
                {
                    connection.Open();
                    Console.WriteLine("Conexión a la base de datos exitosa.");
                    Thread.Sleep(1000);
                    Console.Clear();
                }
            }
            catch (SqlException ex)
            {
                throw new ConnectionException(ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new ConnectionException(ex);
            }
            catch (Exception ex)
            {
                throw new ConnectionException(ex);
            }
        }
    }
}