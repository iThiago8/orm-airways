namespace OrmAirways.Models
{
	public interface IAircraftRepository
	{
		public Task Create (Aircraft aircraft);
		public Task Update (Aircraft aircraft);
		public Task Delete (Aircraft aircraft);
		public Task<Aircraft?> GetById (int id);
		public Task<List<Aircraft>?> GetAll ();
		public Task<Aircraft?> GetByModel (string model);
	}
}
