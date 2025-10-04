using OrmAirways.Models;

namespace OrmAirways.Interfaces
{
    public interface IBookingRepository
    {
        public Task Create(Booking booking);
        public Task Update(Booking booking);
        public Task Delete(Booking booking);
        public Task<Booking?> GetById(int id);
        public Task<List<Booking>?> GetAll();
    }
}
