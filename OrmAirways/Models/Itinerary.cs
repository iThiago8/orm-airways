using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrmAirways.Models
{
    public class Itinerary
    {
        [Key]
        public int ID { get; set; }
        public int FlightID { get; set; }

        [ForeignKey(nameof(FlightID))]
        public Flight Flight { get; set; } = new();

        public int AircraftID { get; set; }

        [ForeignKey(nameof(AircraftID))]
        public Aircraft Aircraft { get; set; } = new();

        public int OriginAirportID { get; set; }

        [ForeignKey(nameof(OriginAirportID))]
        public Airport OriginAirport { get; set; } = new();
        
        public int DestinationAirportID { get; set; }

        [ForeignKey(nameof(DestinationAirportID))]
        public Airport DestinationAirport { get; set; } = new();
    }
}
