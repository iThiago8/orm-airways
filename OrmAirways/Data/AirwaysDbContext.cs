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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Aircraft>().ToTable("Aircraft");


            modelBuilder.Entity<Airport>().ToTable("Airport");


            modelBuilder.Entity<Booking>().ToTable("Booking");
                
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Customer)
                .WithMany()
                .HasForeignKey(b => b.CustomerID);
            
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Seat)
                .WithMany()
                .HasForeignKey(b => b.SeatID);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Flight)
                .WithMany()
                .HasForeignKey(b => b.FlightID);


            modelBuilder.Entity<Customer>().ToTable("Customer");
            

            modelBuilder.Entity<Flight>().ToTable("Flight");
            
            modelBuilder.Entity<Flight>()
                .HasOne(f => f.OriginAirport)
                .WithMany()
                .HasForeignKey(b => b.OriginAirportID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Flight>()
                .HasOne(f => f.DestinationAirport)
                .WithMany()
                .HasForeignKey(b => b.DestinationAirportID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Itinerary>().ToTable("Itinerary");

            modelBuilder.Entity<Itinerary>()
                .HasOne(i => i.Flight)
                .WithMany()
                .HasForeignKey(i => i.FlightID);

            modelBuilder.Entity<Itinerary>()
                .HasOne(i => i.Aircraft)
                .WithMany()
                .HasForeignKey(i => i.AircraftID);

            modelBuilder.Entity<Itinerary>()
                .HasOne(i => i.OriginAirport)
                .WithMany()
                .HasForeignKey(i => i.OriginAirportID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Itinerary>()
                .HasOne(i => i.DestinationAirport)
                .WithMany()
                .HasForeignKey(i => i.DestinationAirportID)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Seat>().ToTable("Seat");

            modelBuilder.Entity<Seat>()
                .HasOne(s => s.Aircraft)
                .WithMany()
                .HasForeignKey(s => s.AircraftID);

        }
    }
}
