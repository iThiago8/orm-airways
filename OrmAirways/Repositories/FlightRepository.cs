using Microsoft.EntityFrameworkCore;
using OrmAirways.Data;
using OrmAirways.Interfaces;
using OrmAirways.Models;

namespace OrmAirways.Repositories
{
    public class FlightRepository : IFlightRepository
    {
        private readonly AirwaysDbContext _context;

        public FlightRepository(AirwaysDbContext context)
        {
            _context = context;
        }

        public async Task Create(Flight flight)
        {
            await _context.Flights.AddAsync(flight);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Flight flight)
        {
            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Flight>?> GetAll()
        {
            return await _context.Flights
                .AsNoTracking()
                .Include(f => f.Origin)
                .Include(f => f.Destination)
                .Include(f => f.Aircraft)
                .ToListAsync();
        }

        public async Task<Flight?> GetById(Guid id)
        {
            return await _context.Flights
                .Include(f => f.Origin)
                .Include(f => f.Destination)
                .Include(f => f.Aircraft)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<List<Flight>?> GetByOriginAirportId(Guid originAirportId)
        {
            return await _context.Flights
                .AsNoTracking()
                .Include(f => f.Origin)
                .Include(f => f.Destination)
                .Include(f => f.Aircraft)
                .Where(f => f.OriginId == originAirportId)
                .ToListAsync();
        }

        public async Task<List<Flight>?> GetByDestinationAirportId(Guid destinationAirportId)
        {
            return await _context.Flights
                .AsNoTracking()
                .Include(f => f.Origin)
                .Include(f => f.Destination)
                .Include(f => f.Aircraft)
                .Where(f => f.DestinationId == destinationAirportId)
                .ToListAsync();
        }

        public async Task Update(Flight flight)
        {
            _context.Flights.Update(flight);
            await _context.SaveChangesAsync();
        }
    }
}