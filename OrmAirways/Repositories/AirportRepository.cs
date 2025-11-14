using OrmAirways.Models;
using OrmAirways.Data;
using Microsoft.EntityFrameworkCore;

namespace OrmAirways.Repositories
{
    public class AirportRepository : IAirportRepository
    {
        private readonly AirwaysDbContext _context;

        public AirportRepository(AirwaysDbContext context)
        {
            _context = context;
        }

        public async Task Create(Airport airport)
        {
            await _context.Airports.AddAsync(airport);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Airport airport)
        {
            _context.Airports.Remove(airport);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Airport airport)
        {
            _context.Airports.Update(airport);
            await _context.SaveChangesAsync();
        }

        public async Task<Airport?> GetById(int id)
        {
            var airport = await _context.Airports.Where(a => a.ID == id).FirstOrDefaultAsync();

            return airport;
        }
        public async Task<List<Airport>> GetAll()
        {
            var data = await _context.Airports.ToListAsync();
            return data;
        }

    }
}
