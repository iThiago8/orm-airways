using Microsoft.AspNetCore.Mvc;
using OrmAirways.Interfaces;
using OrmAirways.Models;

namespace OrmAirways.Repositories
{
	public class FlightRepository : IFlightRepository
	{
		public Task Create(Flight flight)
		{
			throw new NotImplementedException();
		}

		public Task Delete(Flight flight)
		{
			throw new NotImplementedException();
		}

		public Task<List<Flight>?> GetAll()
		{
			throw new NotImplementedException();
		}

		public Task<List<Flight>?> GetByDestinationAirportId(int destinationAirportId)
		{
			throw new NotImplementedException();
		}

		public Task<Flight?> GetById(int id)
		{
			throw new NotImplementedException();
		}

		public Task<List<Flight>?> GetByOriginAirportId(int originAirportId)
		{
			throw new NotImplementedException();
		}
		public Task Update(Flight flight)
		{
			throw new NotImplementedException();
		}
	}
}
