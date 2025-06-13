using Library.Domain.Enumerations;

namespace Library.Domain.Entities
{
    public class Loan
    {
        private int _loanId;
        private User _user;
        private Reservation _reservation;
        private DateTime _startDate;
        private DateTime _dueDate;
        private DateTime? _returnDate;
        private LoanStatus _loanStatus;

        public int LoanId => _loanId;
        public DateTime StartDate => _startDate;
        public DateTime DueDate => _dueDate;
        public DateTime? ReturnDate => _returnDate;
        internal User User => _user;
        internal Reservation Reservation => _reservation;
        public LoanStatus LoanStatus => _loanStatus;

        public Loan() { }

        public Loan(User user, Reservation reservation, DateTime startDate, DateTime dueDate)
        {
            _user = user;
            _reservation = reservation;
            _startDate = startDate;
            _dueDate = dueDate;
            _loanStatus = LoanStatus.Active;
            _returnDate = null;
        }
    }
}
