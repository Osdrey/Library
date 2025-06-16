namespace Library.Domain.Entities
{
    public abstract class Person
    {
        private int _id;
        private int _document;
        private string _firstName;
        private string _lastName;
        private string _middleName;
        private int _age;

        public int Id => _id;
        public int Document => _document;
        public string FirstName => _firstName;
        public string LastName => _lastName;
        public string MiddleName => _middleName;
        public int Age => _age;

        protected Person() { }

        protected Person(int document, string firstName, string lastName, string middleName, int age)
        {
            _document = document;
            _firstName = firstName;
            _lastName = lastName;
            _middleName = middleName;
            _age = age;
        }
    }
}
