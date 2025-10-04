using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrmAirways.Models
{
    public class Booking
    {
        [Key]
        public int ID { get; set; }
        public int CustomerID { get; set; }

        [ForeignKey(nameof(CustomerID))]
        public Customer Customer { get; set; } = new();

        public int SeatID { get; set; }

        [ForeignKey(nameof(SeatID))]
        public Seat Seat { get; set; } = new();
        public int FlightID { get; set; }

        [ForeignKey(nameof(FlightID))]
        public Flight Flight { get; set; } = new();
    }
}
