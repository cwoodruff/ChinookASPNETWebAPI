using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Chinook.Domain.Entities;
using Chinook.Domain.Repositories;
using Microsoft.Data.SqlClient;

namespace Chinook.DataJson.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly SqlConnection _sqlconn;

        public InvoiceRepository(SqlConnection sqlconn)
        {
            _sqlconn = sqlconn;
        }

        public void Dispose()
        {
        }

        private async Task<bool> InvoiceExists(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_CheckInvoice", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcomm.Parameters.Add(new SqlParameter("InvoiceId", id));
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);

            return Convert.ToBoolean(dset.Tables[0].Rows[0][0]);
        }

        public async Task<List<Invoice>> GetAll()
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetInvoice", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            var converted =
                JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(),
                    typeof(List<Invoice>)) as List<Invoice>;
            return converted;
        }

        public async Task<Invoice> GetById(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetInvoiceDetails", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcomm.Parameters.Add(new SqlParameter("InvoiceId", id));
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            var converted =
                JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(),
                    typeof(List<Invoice>)) as List<Invoice>;

            return converted.FirstOrDefault();
        }

        public async Task<List<Invoice>> GetByCustomerId(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetInvoiceByCustomer", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcomm.Parameters.Add(new SqlParameter("CustomerId", id));
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            var converted =
                JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(),
                    typeof(List<Invoice>)) as List<Invoice>;
            return converted;
        }

        public async Task<Invoice> Add(Invoice newInvoice)
        {
            return newInvoice;
        }

        public async Task<bool> Update(Invoice invoice)
        {
            if (!await InvoiceExists(invoice.Id))
                return false;

            try
            {
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Invoice>> GetByEmployeeId(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetInvoiceByEmployee", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcomm.Parameters.Add(new SqlParameter("EmployeeId", id));
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            var converted =
                JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(),
                    typeof(List<Invoice>)) as List<Invoice>;
            return converted;
        }
    }
}