using System.ComponentModel.DataAnnotations;

namespace OrmAirways.Models
{
    public class Aircraft : BaseEntity
    {
        public string Model { get; set; } = string.Empty;
        public string RegistrationNumber { get; set; } = string.Empty;
        public int Capacity { get; set; }

        public ICollection<Seat> Seats { get; set; } = new List<Seat>();
    }
}
