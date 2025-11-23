using OrmAirways.Models;

namespace OrmAirways.Interfaces
{
    public interface ICustomerRepository
    {
        public Task Create(Customer customer);
        public Task Update(Customer customer);
        public Task Delete(Customer customer);
        public Task<Customer?> GetById(Guid id);
        public Task<List<Customer>?> GetAll();
    }
}
