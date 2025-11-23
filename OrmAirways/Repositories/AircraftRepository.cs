using Microsoft.EntityFrameworkCore;
using OrmAirways.Data;
using OrmAirways.Interfaces;
using OrmAirways.Models;

namespace OrmAirways.Repositories
{
    public class AircraftRepository(AirwaysDbContext context) : IAircraftRepository
    {
        public async Task Create(Aircraft aircraft)
        {
            await context.Aircrafts.AddAsync(aircraft);
            await context.SaveChangesAsync();
        }

        public async Task Delete(Aircraft aircraft)
        {
            context.Aircrafts.Remove(aircraft);
            await context.SaveChangesAsync();
        }

        public async Task<List<Aircraft>?> GetAll()
        {
            return await context.Aircrafts
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Aircraft?> GetById(Guid id)
        {
            return await context.Aircrafts.FindAsync(id);
        }

        public async Task<List<Aircraft>?> GetByModel(string model)
        {
            return await context.Aircrafts
                .AsNoTracking()
                .Where(a => a.Model.Contains(model))
                .ToListAsync();
        }

        public async Task Update(Aircraft aircraft)
        {
            context.Aircrafts.Update(aircraft);
            await context.SaveChangesAsync();
        }
    }
}