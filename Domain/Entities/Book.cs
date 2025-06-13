using Library.Domain.Enumerations;
using Library.Domain.Structures;

namespace Library.Domain.Entities
{
    public class Book : Material
    {
        private int _pages;
        private int materialId;
        private string title;
        private string author;
        private int publicationYear;
        private MaterialStatus materialStatus;
        private int pages;

        public int Pages => _pages;

        public Book() { }

        public Book ( 
            int id, 
            string title, 
            string author, 
            int publicationYear, 
            MaterialStatus materialStatus, 
            MaterialCondition materialCondition, 
            MaterialTopic materialTopic, 
            int pages 
        ) : base(id, title, author, publicationYear, materialStatus, materialCondition, materialTopic)
        {
            _pages = pages;
        }

        public Book(int materialId, string title, string author, int publicationYear, MaterialStatus materialStatus, int pages)
        {
            this.materialId = materialId;
            this.title = title;
            this.author = author;
            this.publicationYear = publicationYear;
            this.materialStatus = materialStatus;
            this.pages = pages;
        }
    }
}
