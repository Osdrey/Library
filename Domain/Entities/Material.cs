using Library.Domain.Enumerations;
using Library.Domain.Structures;

namespace Library.Domain.Entities
{
    public abstract class Material
    {
        private int _materialId;
        private string _title;
        private string _author;
        private int _publicationYear;
        private MaterialStatus _materialStatus;
        private MaterialCondition _materialCondition;
        private MaterialTopic _materialTopic;

        public int MaterialId => _materialId;
        public string Title => _title;
        public string Author => _author;
        public int PublicationYear => _publicationYear;
        public MaterialStatus MaterialStatus => _materialStatus;
        public MaterialCondition MaterialCondition => _materialCondition;
        public MaterialTopic MaterialTopic => _materialTopic;

        protected Material () { }

        protected Material (
            int id,
            string title,
            string author,
            int publicationYear,
            MaterialStatus materialStatus,
            MaterialCondition materialCondition,
            MaterialTopic materialTopic )
        {
            _materialId = id;
            _title = title;
            _author = author;
            _publicationYear = publicationYear;
            _materialStatus = materialStatus;
            _materialCondition = materialCondition;
            _materialTopic = materialTopic;
        }

        protected Material (
            int id,
            string title,
            string author,
            int publicationYear,
            MaterialStatus materialStatus,
            MaterialTopic materialTopic )
        {
            _materialId = id;
            _title = title;
            _author = author;
            _publicationYear = publicationYear;
            _materialStatus = materialStatus;
            _materialTopic = materialTopic;
        }
    }
}
