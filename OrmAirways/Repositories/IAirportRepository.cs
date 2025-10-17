using OrmAirways.Models;

namespace OrmAirways.Repositories
{
    public interface IAirportRepository
    {
        public Task Create(Airport airport);
        public Task Update(Airport airport);
        public Task Delete(Airport airport);
        public Task GetById(int id);
        public Task<List<Airport>>GetAll();
    }
}
