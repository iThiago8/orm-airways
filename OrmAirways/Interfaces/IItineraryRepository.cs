using Microsoft.AspNetCore.Mvc;
using OrmAirways.Models;

namespace OrmAirways.Interfaces
{
	public interface IItineraryRepository
	{
		public Task Create(Itinerary itinerary);
		public Task Update(Itinerary itinerary);
		public Task Delete(Itinerary itinerary);
		public Task<Itinerary?> GetById (int id);
		public Task<List<Itinerary>?> GetAll ();
	}
}