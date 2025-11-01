using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrmAirways.Data;
using OrmAirways.Interfaces;
using OrmAirways.Models;

namespace OrmAirways.Repositories
{
	public class SeatRepository : ISeatRepository
	{
		private readonly AirwaysDbContext _context;
		public SeatRepository(AirwaysDbContext context)
		{
			_context = context;
		}
		public async Task Create(Seat seat)
		{
			await _context.Seats.AddAsync(seat);
			await _context.SaveChangesAsync();
		}

		public async Task Delete(Seat seat)
		{
			_context.Seats.Remove(seat);
			await _context.SaveChangesAsync();
		}

		public async Task<List<Seat>?> GetAll()
		{
			return await _context.Seats.ToListAsync();
		}

		public async Task<Seat?> GetById(int id)
		{
			return await _context.Seats.FindAsync(id);
		}
		public async Task Update(Seat seat)
		{
			_context.Seats.Update(seat);
			await _context.SaveChangesAsync();
		}
	}
}
