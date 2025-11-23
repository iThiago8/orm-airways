using OrmAirways.Models;

namespace OrmAirways.Interfaces
{
	public interface IAircraftRepository
	{
		public Task Create (Aircraft aircraft);
		public Task Update (Aircraft aircraft);
		public Task Delete (Aircraft aircraft);
		public Task<Aircraft?> GetById (Guid id);
		public Task<List<Aircraft>?> GetAll ();
		public Task<List<Aircraft>?> GetByModel (string model);
	}
}
