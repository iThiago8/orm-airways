using OrmAirways.Models;

namespace OrmAirways.Interfaces
{
    public interface IAirportRepository
    {
        public Task Create(Airport airport);
        public Task Update(Airport airport);
        public Task Delete(Airport airport);
        public Task<Airport?> GetById(Guid id);
        public Task<List<Airport>>GetAll();
    }
}
