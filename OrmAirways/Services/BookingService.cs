using Microsoft.EntityFrameworkCore;
using OrmAirways.Data;
using OrmAirways.Interfaces;
using OrmAirways.Models;

namespace OrmAirways.Services
{
    public class BookingService(AirwaysDbContext context) : IBookingService
    {
        public async Task<Booking> ReserveTicketAsync(Guid flightId, Guid customerId, Guid? seatId)
        {
            var flight = await context.Flights.FindAsync(flightId) ?? throw new Exception("Voo não encontrado.");
            if (seatId.HasValue)
            {
                bool seatTaken = await context.Bookings.AnyAsync(t => t.FlightId == flightId && t.SeatId == seatId);
                if (seatTaken) throw new Exception("Assento já ocupado.");
            }

            var ticket = new Booking
            {
                Id = Guid.NewGuid(),
                FlightId = flightId,
                CustomerId = customerId,
                SeatId = seatId,
                IsConfirmed = true
            };

            context.Bookings.Add(ticket);
            await context.SaveChangesAsync();

            return ticket;
        }

        public async Task CancelTicketAsync(Guid ticketId)
        {
            var ticket = await context.Bookings.FindAsync(ticketId);
            if (ticket != null)
            {
                context.Bookings.Remove(ticket);
                await context.SaveChangesAsync();
            }
        }
    }
}
