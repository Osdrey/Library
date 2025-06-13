using Library.Domain.Enumerations;
using Library.Domain.Structures;

namespace Library.Application.DTOs
{
    public class AudioVisualDTO
    {
        public int MaterialId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int PublicationYear { get; set; }
        public MaterialStatus MaterialStatus { get; set; }
        public MaterialCondition MaterialCondition { get; set; }
        public MaterialTopic MaterialTopic { get; set; }
        public string Format { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
    }
}
