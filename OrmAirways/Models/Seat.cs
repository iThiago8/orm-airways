using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrmAirways.Models
{
    public class Seat : BaseEntity
    {
        public string Code { get; set; } = string.Empty;
        public string ClassType { get; set; } = "Economy"; 

        public Guid AircraftId { get; set; }
        public Aircraft Aircraft { get; set; } = null!;
    }
}
