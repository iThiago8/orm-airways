using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrmAirways.Models
{
    public class Flight
    {
        public int ID { get; set; }
        public int OriginAirportID { get; set; }
        [ForeignKey(nameof(OriginAirportID))]
        public Airport OriginAirport { get; set; } = new();


        public int DestinationAirportID { get; set; }
        [ForeignKey(nameof(DestinationAirportID))]
        public Airport DestinationAirport { get; set; } = new();

        public DateTime InicialHour { get; set; }
        public DateTime FinalHour { get; set; }
        public DateTime PredictedFinalHour { get; set; }
        public DateTime PredictedInicialHour { get; set; }
    }
}
