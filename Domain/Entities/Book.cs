using Library.Domain.Enumerations;
using Library.Domain.Structures;

namespace Library.Domain.Entities
{
    public class Book : Material
    {
        private int _pages;

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
    }
}
