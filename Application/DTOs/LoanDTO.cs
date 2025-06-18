namespace Library.Application.DTOs
{
    public class LoanDTO
    {
        public int LoanId { get; set; }
        public int ReservationId { get; set; }
        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int LoanStatus { get; set; }
    }
}
