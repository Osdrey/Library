using Library.Domain.Enumerations;

namespace Library.Domain.Entities
{
    public class User : Person
    {
        private string _email;
        private string _userName;
        private string _password;
        private UserType _userType;
        private UserRole _userRole;
        private int _arrears;
        private bool _isActive;
        private readonly List<Reservation> _reservations;
        private readonly List<Loan> _loans;

        public string Email => _email;
        public string UserName => _userName;
        public string Password => _password;
        public UserType UserType => _userType;
        public UserRole UserRole => _userRole;
        public int Arrears => _arrears;
        public bool IsActive => _isActive;

        internal IReadOnlyCollection<Reservation> Reservations => _reservations.AsReadOnly();
        internal IReadOnlyCollection<Loan> Loans => _loans.AsReadOnly();

        public User ( 
            int document,
            string firstName,
            string lastName,
            string middleName,
            int age,
            string email,
            string userName,
            string password,
            UserType userType,
            UserRole userRole,
            int arrears,
            bool isActive
        ) : base(document, firstName, lastName, middleName, age)
        {
            _email = email;
            _userName = userName;
            _password = password;
            _userType = userType;
            _userRole = userRole;
            _arrears = arrears;
            _isActive = isActive;
            _reservations = new List<Reservation>();
            _loans = new List<Loan>();
        }
    }
}