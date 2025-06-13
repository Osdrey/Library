using Library.Domain.Enumerations;
using Library.Domain.Structures;

namespace Library.Domain.Entities
{
    public class Reservation
    {
        private int _reservationId;
        private User _user;
        private Material _material;
        private DateTime _requestDate;
        private DateTime _expirationDate;
        private ReservationStatus _reservationStatus;

        public int ReservationId => _reservationId;
        public DateTime RequestDate => _requestDate;
        public DateTime ExpirationDate => _expirationDate;
        internal User User => _user;
        internal Material Material => _material;
        internal ReservationStatus ReservationStatus => _reservationStatus;

        public Reservation (
            User user, 
            Material material, 
            DateTime requestDate, 
            DateTime expirationDate, 
            ReservationStatus reservationStatus )
        {
            _user = user;
            _material = material;
            _requestDate = requestDate;
            _expirationDate = expirationDate;
            _reservationStatus = reservationStatus;
        }
    }
}
