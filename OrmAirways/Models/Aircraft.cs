using System.ComponentModel.DataAnnotations;

namespace OrmAirways.Models
{
    public class Aircraft
    {
        [Key]
        public int ID { get; set; }
        public string Model { get; set; } = string.Empty;
        public int SeatNumber { get; set; }
    }
}
