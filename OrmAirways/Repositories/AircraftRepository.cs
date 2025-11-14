using Microsoft.AspNetCore.Mvc;
using OrmAirways.Interfaces;
using OrmAirways.Models;
using OrmAirways.Data;
using Microsoft.EntityFrameworkCore;

namespace OrmAirways.Repositories
{
	public class AircraftRepository : IAircraftRepository
	{
		private readonly AirwaysDbContext _context;
		public AircraftRepository(AirwaysDbContext context)
		{
			_context = context;
		}
		public async Task Create(Aircraft aircraft)
		{
			await _context.Aircrafts.AddAsync(aircraft);
			await  _context.SaveChangesAsync();
		}

		public async Task Delete(Aircraft aircraft)
		{
			_context.Aircrafts.Remove(aircraft);
			await _context.SaveChangesAsync();
		}

		public async Task<List<Aircraft>?> GetAll()
		{
			return await _context.Aircrafts.ToListAsync();
		}

		public async Task<Aircraft?> GetById(int id)
		{
			return await _context.Aircrafts.FindAsync(id);
		}

		public async Task<List<Aircraft>?> GetByModel(string model)
		{
			return await _context.Aircrafts.ToListAsync();
		}

		public async Task Update(Aircraft aircraft)
		{
			_context.Aircrafts.Update(aircraft);
			await _context.SaveChangesAsync();
		}
	}
}
