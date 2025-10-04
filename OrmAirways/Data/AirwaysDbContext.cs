using Microsoft.EntityFrameworkCore;
using OrmAirways.Models;

namespace OrmAirways.Data
{
    public class AirwaysDbContext(DbContextOptions<AirwaysDbContext> options) : DbContext(options)
    {
        public DbSet<Aircraft> Aircrafts { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Customer> Customers { get; set; } 
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Itinerary> Itineraries { get; set; }
        public DbSet<Seat> Seats { get; set; }
    }
}
