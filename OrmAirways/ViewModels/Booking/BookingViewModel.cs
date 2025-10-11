namespace OrmAirways.ViewModels.Booking
{
    public class BookingViewModel
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }

        public Models.Customer Customer { get; set; } = new();

        public int SeatID { get; set; }

        public Models.Seat Seat { get; set; } = new();
        public int FlightID { get; set; }

        public Models.Flight Flight { get; set; } = new();
    }
}
