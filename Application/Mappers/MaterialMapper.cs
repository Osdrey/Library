using Library.Application.DTOs;
using Library.Domain.Entities;

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
    }
}
