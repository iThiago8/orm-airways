using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrmAirways.Models;

namespace OrmAirways.Data
{
    public class AirwaysDbContext(DbContextOptions<AirwaysDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<Aircraft> Aircrafts { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Airport>().ComplexProperty(a => a.Location);

            modelBuilder.Entity<Aircraft>().ToTable("Aircraft");
            modelBuilder.Entity<Airport>().ToTable("Airport");
            modelBuilder.Entity<Booking>().ToTable("Booking");
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Flight>().ToTable("Flight");
            modelBuilder.Entity<Seat>().ToTable("Seat");

            modelBuilder.Entity<Flight>(entity =>
            {
                entity.HasOne(f => f.Origin)
                      .WithMany()
                      .HasForeignKey(f => f.OriginId)
                      .OnDelete(DeleteBehavior.Restrict); 

                entity.HasOne(f => f.Destination)
                      .WithMany()
                      .HasForeignKey(f => f.DestinationId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(f => f.Aircraft)
                      .WithMany()
                      .HasForeignKey(f => f.AircraftId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(f => f.BasePrice).HasPrecision(18, 2);
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasOne(b => b.Customer)
                      .WithMany(c => c.Bookings)
                      .HasForeignKey(b => b.CustomerId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(b => b.Flight)
                      .WithMany(f => f.Bookings)
                      .HasForeignKey(b => b.FlightId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(b => b.Seat)
                      .WithMany()
                      .HasForeignKey(b => b.SeatId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(b => b.TicketCode).IsUnique();

                entity.HasIndex(b => new { b.FlightId, b.SeatId })
                      .IsUnique()
                      .HasFilter("[SeatId] IS NOT NULL");
            });

            modelBuilder.Entity<Seat>(entity =>
            {
                entity.HasOne(s => s.Aircraft)
                      .WithMany(a => a.Seats)
                      .HasForeignKey(s => s.AircraftId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}