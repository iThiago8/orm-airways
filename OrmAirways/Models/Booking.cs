using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrmAirways.Models
{
    public class Booking
    {
        [Key]
        public int ID { get; set; }
        public int ID_Customer { get; set; }

        [ForeignKey(nameof(ID_Customer))]
        Customer customer { get; set; } = new();

        public int Seat { get; set; }
        public int ID_Flight { get; set; }

        [ForeignKey(nameof(ID_Flight))]
        public Flight Flight { get; set; }
    }
}
