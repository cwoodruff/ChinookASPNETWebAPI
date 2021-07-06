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
    public class InvoiceLineRepository : IInvoiceLineRepository
    {
        private readonly DbInfo _dbInfo;

        public InvoiceLineRepository(DbInfo dbInfo)
        {
            _dbInfo = dbInfo;
        }

        private IDbConnection Connection => new SqlConnection(_dbInfo.ConnectionStrings);

        public void Dispose()
        {
            
        }

        private async Task<bool> InvoiceLineExists(int id) =>
            await Connection.ExecuteScalarAsync<bool>("select count(1) from InvoiceLine where Id = @id", new {id});

        public async Task<List<InvoiceLine>> GetAll()
        {
            using IDbConnection cn = Connection;
            cn.Open();
            var invoiceLines = await cn.QueryAsync<InvoiceLine>("Select * From InvoiceLine");
            return invoiceLines.ToList();
        }

        public async Task<InvoiceLine> GetById(int id)
        {
            using var cn = Connection;
            cn.Open();
            return cn.QueryFirstOrDefault<InvoiceLine>("Select * From InvoiceLine WHERE Id = @Id", new {id});
        }

        public async Task<List<InvoiceLine>> GetByInvoiceId(int id)
        {
            using var cn = Connection;
            cn.Open();
            var invoiceLines = await cn.QueryAsync<InvoiceLine>("Select * From InvoiceLine WHERE InvoiceId = @Id", new { id });
            return invoiceLines.ToList();
        }

        public async Task<List<InvoiceLine>> GetByTrackId(int id)
        {
            using var cn = Connection;
            cn.Open();
            var invoiceLines =  cn.Query<InvoiceLine>("Select * From InvoiceLine WHERE TrackId = @Id", new { id });
            return invoiceLines.ToList();
        }

        public async Task<InvoiceLine> Add(InvoiceLine newInvoiceLine)
        {
            using var cn = Connection;
            cn.Open();

            newInvoiceLine.Id = await cn.InsertAsync(
                new InvoiceLine
                {
                    Id = newInvoiceLine.Id,
                    InvoiceId = newInvoiceLine.InvoiceId,
                    TrackId = newInvoiceLine.TrackId,
                    UnitPrice = newInvoiceLine.UnitPrice,
                    Quantity = newInvoiceLine.Quantity
                });

            return newInvoiceLine;
        }

        public async Task<bool> Update(InvoiceLine invoiceLine)
        {
            if (!await InvoiceLineExists(invoiceLine.Id))
                return false;

            try
            {
                using var cn = Connection;
                cn.Open();
                return await cn.UpdateAsync(invoiceLine);
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
                return await cn.DeleteAsync(new InvoiceLine {Id = id});
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}