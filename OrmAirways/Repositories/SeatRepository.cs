using Microsoft.EntityFrameworkCore;
using OrmAirways.Data;
using OrmAirways.Interfaces;
using OrmAirways.Models;

namespace OrmAirways.Repositories
{
    public class SeatRepository(AirwaysDbContext context) : ISeatRepository
    {
        public async Task Create(Seat seat)
        {
            await context.Seats.AddAsync(seat);
            await context.SaveChangesAsync();
        }

        public async Task Delete(Seat seat)
        {
            context.Seats.Remove(seat);
            await context.SaveChangesAsync();
        }

        public async Task<List<Seat>?> GetAll()
        {
            return await context.Seats
                .AsNoTracking()
                .Include(s => s.Aircraft)
                .ToListAsync();
        }

        public async Task<Seat?> GetById(Guid id)
        {
            return await context.Seats
                .Include(s => s.Aircraft)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task Update(Seat seat)
        {
            context.Seats.Update(seat);
            await context.SaveChangesAsync();
        }
    }
}