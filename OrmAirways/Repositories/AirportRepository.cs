using OrmAirways.Models;
using OrmAirways.Data;
using Microsoft.EntityFrameworkCore;
using OrmAirways.Interfaces;

namespace OrmAirways.Repositories
{
    public class AirportRepository(AirwaysDbContext context) : IAirportRepository
    {
        public async Task Create(Airport airport)
        {
            await context.Airports.AddAsync(airport);
            await context.SaveChangesAsync();
        }

        public async Task Delete(Airport airport)
        {
            context.Airports.Remove(airport);
            await context.SaveChangesAsync();
        }

        public async Task Update(Airport airport)
        {
            context.Airports.Update(airport);
            await context.SaveChangesAsync();
        }

        public async Task<Airport?> GetById(Guid id)
        {
            return await context.Airports.FindAsync(id);
        }

        public async Task<List<Airport>> GetAll()
        {
            return await context.Airports
                .AsNoTracking()
                .ToListAsync();
        }
    }
}