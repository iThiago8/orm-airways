using OrmAirways.Models;

namespace OrmAirways.Interfaces
{
	public interface IFlightRepository
	{
		public Task Create (Flight flight);
		public Task Update (Flight flight);
		public Task Delete (Flight flight); 
		public Task<Flight?> GetById (Guid id);
		public Task<List<Flight>?> GetAll ();
		public Task<List<Flight>?> GetByOriginAirportId (Guid originAirportId);
		public Task<List<Flight>?> GetByDestinationAirportId (Guid destinationAirportId);
	}
}
