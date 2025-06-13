using Library.Domain.Enumerations;
using Library.Domain.Structures;

namespace Library.Domain.Entities
{
    public class AudioVisual : Material
    {
        private string _format;
        private string _duration;

        public string Format => _format;
        public string Duration => _duration;

        public AudioVisual() { }

        public AudioVisual (
            int id,
            string title,
            string author,
            int publicationYear,
            MaterialStatus materialStatus,
            MaterialCondition materialCondition,
            MaterialTopic materialTopic,
            string format,
            string duration
        ) : base(id, title, author, publicationYear, materialStatus, materialCondition, materialTopic)
        {
            _format = format;
            _duration = duration;
        }
    }
}
