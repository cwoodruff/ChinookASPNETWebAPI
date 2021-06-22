using System.Collections.Generic;
using System.Linq;
using Chinook.DataEF;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.DataEFCore.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ChinookContext _context;

        public EmployeeRepository(ChinookContext context)
        {
            _context = context;
        }

        private bool EmployeeExists(int id) =>
            _context.Employees.Any(e => e.Id == id);

        public void Dispose() => _context.Dispose();

        public List<Employee> GetAll() =>
            _context.Employees.ToList();

        public Employee GetById(int id) =>
            _context.Employees.Find(id);

        public Employee Add(Employee newEmployee)
        {
            _context.Employees.Add(newEmployee);
            _context.SaveChanges();
            return newEmployee;
        }

        public bool Update(Employee employee)
        {
            if (!EmployeeExists(employee.Id))
                return false;
            _context.Employees.Update(employee);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            if (!EmployeeExists(id))
                return false;
            var toRemove = _context.Employees.Find(id);
            _context.Employees.Remove(toRemove);
            _context.SaveChanges();
            return true;
        }

        public Employee GetReportsTo(int id) =>
            _context.Employees.Find(id);

        public List<Employee> GetDirectReports(int id) => _context.Employees.Where(e => e.ReportsTo == id).ToList();
        
        public Employee GetToReports(int id) => 
            _context.Employees
                .Find(_context.Employees.
                    Where(e => e.Id == id)
                    .Select(p => new {p.ReportsTo})
                    .First());
    }
}