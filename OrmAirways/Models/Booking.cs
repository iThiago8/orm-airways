namespace OrmAirways.Models
{
    public class Booking : BaseEntity
    {
        public string TicketCode { get; set; } = Guid.NewGuid().ToString()[..8].ToUpper();

        public Guid CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public Guid FlightId { get; set; }
        public Flight? Flight { get; set; }

        public Guid? SeatId { get; set; }
        public Seat? Seat { get; set; }

        public DateTime BookingDate { get; set; } = DateTime.UtcNow;
        public bool IsConfirmed { get; set; } = true;
    }
}
