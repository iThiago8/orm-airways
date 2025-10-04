using Microsoft.EntityFrameworkCore;
using OrmAirways.Data;
using OrmAirways.Interfaces;
using OrmAirways.Models;

namespace OrmAirways.Repositories
{
    public class BookingRepository(AirwaysDbContext context) : IBookingRepository
    {
        public async Task Create(Booking booking)
        {
            await context.Bookings.AddAsync(booking);
            await context.SaveChangesAsync();
        }

        public async Task Update(Booking booking)
        {
            context.Bookings.Update(booking);
            await context.SaveChangesAsync();
        }

        public async Task Delete(Booking booking)
        {
            context.Bookings.Remove(booking);
            await context.SaveChangesAsync();
        }

        public async Task<List<Booking>?> GetAll()
        {
            return await context.Bookings.ToListAsync();
        }

        public async Task<Booking?> GetById(int id)
        {
            return await context.Bookings.FindAsync(id);
        }
    }
}
