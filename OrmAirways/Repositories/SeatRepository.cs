using Microsoft.AspNetCore.Mvc;
using OrmAirways.Interfaces;
using OrmAirways.Models;

namespace OrmAirways.Repositories
{
	public class SeatRepository : ISeatRepository
	{
		public Task Create(Seat seat)
		{
			throw new NotImplementedException();
		}

		public Task Delete(Seat seat)
		{
			throw new NotImplementedException();
		}

		public Task<List<Seat>?> GetAll()
		{
			throw new NotImplementedException();
		}

		public Task<Seat?> GetById(int id)
		{
			throw new NotImplementedException();
		}
		public Task Update(Seat seat)
		{
			throw new NotImplementedException();
		}
	}
}
