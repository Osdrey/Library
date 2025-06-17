using Library.Domain.Enumerations;
using Library.Domain.Structures;

namespace Library.Application.DTOs
{
    public class MaterialDTO
    {
        public int MaterialId { get; set; }
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public int PublicationYear { get; set; }
        public MaterialStatus MaterialStatus { get; set; }
        public MaterialCondition MaterialCondition { get; set; }
        public MaterialTopic MaterialTopic { get; set; }
    }
}
