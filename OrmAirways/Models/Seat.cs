using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrmAirways.Models
{
    public class Seat
    {
        [Key]
        public int ID { get; set; }
        public int AircraftID { get; set; }
        [ForeignKey(nameof(AircraftID))]
        public Aircraft Aircraft { get; set; } = new();
        public string Location { get; set; } = string.Empty;

    }
}
