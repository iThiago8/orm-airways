using OrmAirways.Models;

namespace OrmAirways.Models.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalAircrafts { get; set; }
        public int TotalAirports { get; set; }
        public int TotalCustomers { get; set; }
        public int TotalBookings { get; set; }
        public List<Flight> UpcomingFlights { get; set; } = new();
    }
}