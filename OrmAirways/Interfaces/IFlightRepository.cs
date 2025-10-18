using OrmAirways.Models;

namespace OrmAirways.Interfaces
{
	public interface IFlightRepository
	{
		public Task Create (Flight flight);
		public Task Update (Flight flight);
		public Task Delete (Flight flight); // Tentar colocar função de senha;
		public Task<Flight?> GetById (int id);
		public Task<List<Flight>?> GetAll ();
		public Task<List<Flight>?> GetByOriginAirportId (int originAirportId);
		public Task<List<Flight>?> GetByDestinationAirportId (int destinationAirportId);
	}
}
