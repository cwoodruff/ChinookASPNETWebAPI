using System.Collections.Generic;
using System.Linq;
using Chinook.DataEF;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.DataEFCore.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ChinookContext _context;

        public CustomerRepository(ChinookContext context)
        {
            _context = context;
        }

        private bool CustomerExists(int id) =>
            _context.Customers.Any(c => c.Id == id);

        public void Dispose() => _context.Dispose();

        public List<Customer> GetAll() =>
            _context.Customers.ToList();

        public Customer GetById(int id) =>
            _context.Customers.Find(id);

        public Customer Add(Customer newCustomer)
        {
            _context.Customers.Add(newCustomer);
            _context.SaveChanges();
            return newCustomer;
        }

        public bool Update(Customer customer)
        {
            if (!CustomerExists(customer.Id))
                return false;
            _context.Customers.Update(customer);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            if (!CustomerExists(id))
                return false;
            var toRemove = _context.Customers.Find(id);
            _context.Customers.Remove(toRemove);
            _context.SaveChanges();
            return true;
        }

        public List<Customer> GetBySupportRepId(int id) => _context.Customers.Where(a => a.SupportRepId == id).ToList();
    }
}