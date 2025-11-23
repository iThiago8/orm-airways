using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;

namespace OrmAirways.Models
{
    public class Customer : BaseEntity 
    {
        public string FullName { get; set; } = string.Empty;
        public string DocumentNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public bool IsPriority { get; set; }

        public ICollection<Booking> Bookings { get; set; } = [];
    }
}
