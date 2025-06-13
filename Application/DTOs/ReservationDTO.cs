namespace Library.Application.DTOs
{
    public class ReservationDTO
    {
        public int ReservationId { get; set; }
        public int UserId { get; set; }
        public int MaterialId { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string ReservationStatus { get; set; }
    }
}
