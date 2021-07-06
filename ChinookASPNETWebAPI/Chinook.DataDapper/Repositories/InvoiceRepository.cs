using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Chinook.Domain.DbInfo;
using Chinook.Domain.Entities;
using Chinook.Domain.Repositories;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

namespace Chinook.DataDapper.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly DbInfo _dbInfo;

        public InvoiceRepository(DbInfo dbInfo)
        {
            _dbInfo = dbInfo;
        }

        private IDbConnection Connection => new SqlConnection(_dbInfo.ConnectionStrings);

        public void Dispose()
        {
            
        }

        private async Task<bool> InvoiceExists(int id) =>
            await Connection.ExecuteScalarAsync<bool>("select count(1) from Invoice where Id = @id", new {id});

        public async Task<List<Invoice>> GetAll()
        {
            using IDbConnection cn = Connection;
            cn.Open();
            var invoices =  Connection.Query<Invoice>("Select * From Invoice");
            return invoices.ToList();
        }

        public async Task<Invoice> GetById(int id)
        {
            using var cn = Connection;
            cn.Open();
            return cn.QueryFirstOrDefault<Invoice>("Select * From Invoice WHERE Id = @Id", new {id});
        }

        public async Task<List<Invoice>> GetByCustomerId(int id)
        {
            using var cn = Connection;
            cn.Open();
            var invoices = await cn.QueryAsync<Invoice>("Select * From Invoice WHERE CustomerId = @Id", new { id });
            return invoices.ToList();
        }

        public async Task<Invoice> Add(Invoice newInvoice)
        {
            using var cn = Connection;
            cn.Open();

            newInvoice.Id = await cn.InsertAsync(
                new Invoice
                {
                    Id = newInvoice.Id,
                    CustomerId = newInvoice.CustomerId,
                    InvoiceDate = newInvoice.InvoiceDate,
                    BillingAddress = newInvoice.BillingAddress,
                    BillingCity = newInvoice.BillingCity,
                    BillingState = newInvoice.BillingState,
                    BillingCountry = newInvoice.BillingCountry,
                    BillingPostalCode = newInvoice.BillingPostalCode,
                    Total = newInvoice.Total
                });

            return newInvoice;
        }

        public async Task<bool> Update(Invoice invoice)
        {
            if (!await InvoiceExists(invoice.Id))
                return false;

            try
            {
                using var cn = Connection;
                cn.Open();
                return await cn.UpdateAsync(invoice);
            }
            catch(Exception)
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                using var cn = Connection;
                cn.Open();
                return await cn.DeleteAsync(new Invoice {Id = id});
            }
            catch(Exception)
            {
                return false;
            }
        }

        public Task<List<Invoice>> GetByEmployeeId(int id)
        {
            throw new NotImplementedException();
        }
    }
}