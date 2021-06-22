using System.Collections.Generic;
using System.Linq;
using Chinook.DataEF;
using Chinook.Domain.Entities;
using Chinook.Domain.Repositories;

namespace Chinook.DataEFCore.Repositories
{
    /// <summary>
    /// The invoice repository.
    /// </summary>
    public class InvoiceRepository : IInvoiceRepository
    {
        /// <summary>
        /// The _context.
        /// </summary>
        private readonly ChinookContext _context;

        public InvoiceRepository(ChinookContext context)
        {
            _context = context;
        }

        private bool InvoiceExists(int id) =>
            _context.Invoices.Any(i => i.Id == id);

        public void Dispose() => _context.Dispose();

        public List<Invoice> GetAll() =>
            _context.Invoices.ToList();

        public Invoice GetById(int id) =>
            _context.Invoices.Find(id);

        public Invoice Add(Invoice newInvoice)
        {
            _context.Invoices.Add(newInvoice);
            _context.SaveChanges();
            return newInvoice;
        }

        public bool Update(Invoice invoice)
        {
            if (!InvoiceExists(invoice.Id))
                return false;
            _context.Invoices.Update(invoice);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            if (!InvoiceExists(id))
                return false;
            var toRemove = _context.Invoices.Find(id);
            _context.Invoices.Remove(toRemove);
            _context.SaveChanges();
            return true;
        }

        public List<Invoice> GetByEmployeeId(int id) =>
            _context.Customers.Where(a => a.SupportRepId == 5).SelectMany(t => t.Invoices).ToList();

        public List<Invoice> GetByCustomerId(int id) =>
            _context.Invoices.Where(i => i.CustomerId == id).ToList();
    }
}