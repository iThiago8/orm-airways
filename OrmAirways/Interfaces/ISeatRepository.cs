using OrmAirways.Models;

namespace OrmAirways.Interfaces
{
	public interface ISeatRepository
	{
		public Task Create (Seat seat);
		public Task Update (Seat seat);
		public Task Delete(Seat seat);
		public Task<Seat?> GetById (Guid id);
		public Task<List<Seat>?> GetAll ();
	}
}
