using Microsoft.AspNetCore.Mvc;
using OrmAirways.Models;
using OrmAirways.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrmAirways.Interfaces;

namespace OrmAirways.Repositories
{
	public class ItineraryRepository : IItineraryRepository
	{
		private readonly AirwaysDbContext _context;
		public ItineraryRepository(AirwaysDbContext context)
		{
			_context = context;
		}

		public async Task Create(Itinerary itinerary)
		{
			await _context.Itineraries.AddAsync(itinerary);
			await _context.SaveChangesAsync();
		}
		public async Task Delete(Itinerary itinerary)
		{
			_context.Itineraries.Remove(itinerary);
			await _context.SaveChangesAsync();
		}

		public async Task Update(Itinerary itinerary)
		{
			_context.Itineraries.Update(itinerary);
			await _context.SaveChangesAsync();
		}

		public async Task<Itinerary?> GetById(int id)
		{
			return await _context.Itineraries.FindAsync(id);
		}

		public async Task<List<Itinerary>?> GetAll()
		{
			return await _context.Itineraries.ToListAsync();
		}

		public async Task
	}
}
