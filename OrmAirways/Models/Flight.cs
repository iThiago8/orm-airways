using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;

namespace OrmAirways.Models
{
    public class Flight : BaseEntity
    {
        public string FlightNumber { get; set; } = string.Empty;

        public Guid AircraftId { get; set; }
        public Aircraft? Aircraft { get; set; }

        public Guid OriginId { get; set; }
        public Airport? Origin { get; set; }

        public Guid DestinationId { get; set; }
        public Airport? Destination { get; set; }

        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal BasePrice { get; set; }

        public ICollection<Booking> Bookings { get; set; } = [];
    }
}
