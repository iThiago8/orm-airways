using Microsoft.EntityFrameworkCore;
using OrmAirways.Data;
using OrmAirways.Interfaces;
using OrmAirways.Models;

namespace OrmAirways.Repositories
{
    public class CustomerRepository(AirwaysDbContext context) : ICustomerRepository
    {
        public async Task Create(Customer customer)
        {
            await context.AddAsync(customer);
            await context.SaveChangesAsync();
        }
        public async Task Update(Customer customer)
        {
            context.Customers.Update(customer);
            await context.SaveChangesAsync();
        }

        public async Task Delete(Customer customer)
        {
            context.Remove(customer);
            await context.SaveChangesAsync();
        }

        public async Task<List<Customer>?> GetAll()
        {
            return await context.Customers.ToListAsync();
        }

        public async Task<Customer?> GetById(int id)
        {
            return await context.Customers.FindAsync(id);
        }

    }
}
