using Library.Application.DTOs;
using Library.Domain.Entities;
using Library.Domain.Enumerations;
using Library.Domain.Structures;
using Library.Infraestructure.Exceptions;
using Microsoft.Data.SqlClient;

namespace Library.Application.Mappers
{
    public static class MaterialMapper
    {
        public static BookDTO ToDTO(Book book)
        {
            return new BookDTO
            {
                MaterialId = book.MaterialId,
                Title = book.Title,
                Author = book.Author,
                PublicationYear = book.PublicationYear,
                MaterialStatus = book.MaterialStatus,
                MaterialCondition = book.MaterialCondition,
                MaterialTopic = book.MaterialTopic,
                Pages = book.Pages
            };
        }

        public static AudioVisualDTO ToDTO(AudioVisual av)
        {
            return new AudioVisualDTO
            {
                MaterialId = av.MaterialId,
                Title = av.Title,
                Author = av.Author,
                PublicationYear = av.PublicationYear,
                MaterialStatus = av.MaterialStatus,
                Format = av.Format,
                Duration = av.Duration
            };
        }

        public static Book ToEntity(BookDTO dto)
        {
            return new Book (
                dto.MaterialId, 
                dto.Title, 
                dto.Author, 
                dto.PublicationYear, 
                dto.MaterialStatus, 
                dto.MaterialCondition, 
                dto.MaterialTopic,
                dto.Pages);
        }

        public static AudioVisual ToEntity(AudioVisualDTO dto)
        {
            return new AudioVisual (
                dto.MaterialId, 
                dto.Title, 
                dto.Author, 
                dto.PublicationYear, 
                dto.MaterialStatus, 
                dto.MaterialCondition, 
                dto.MaterialTopic,
                dto.Format, 
                dto.Duration);
        }

        public static MaterialDTO? MaterialDataReader(SqlDataReader reader)
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
