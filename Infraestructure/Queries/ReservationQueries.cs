namespace Library.Infraestructure.Queries
{
    public static class ReservationQueries
    {
        public const string GetAllReservations = @"
            SELECT * FROM Reservations";

        public const string GetPendingReservations = @"
            SELECT * FROM Reservations WHERE ReservationStatus = 0";

        public const string GetReservationsByUserId = @"
            SELECT * FROM Reservations WHERE UserId = @userId";

        public const string GetReservationById = @"
            SELECT * FROM Reservations WHERE ReservationId = @reservationId";

        public const string InsertReservation = @"
            INSERT INTO Reservations (UserId, MaterialId, RequestDate, ExpirationDate, ReservationStatus)
            VALUES (@userId, @materialId, @requestDate, @expirationDate, @reservationStatus);
            SELECT CAST(SCOPE_IDENTITY() AS INT);";

        public const string UpdateReservation = @"
            UPDATE Reservations
            SET ExpirationDate = @expirationDate,
                ReservationStatus = @reservationStatus
            WHERE ReservationId = @reservationId";

        public const string DeleteReservation = @"
            DELETE FROM Reservations
            WHERE ReservationId = @reservationId";
    }
}
