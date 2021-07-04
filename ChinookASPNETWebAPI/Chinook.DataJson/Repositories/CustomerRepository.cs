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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly SqlConnection _sqlconn;

        public CustomerRepository(SqlConnection sqlconn)
        {
            _sqlconn = sqlconn;
        }

        public void Dispose()
        {
        }

        private async Task<bool> CustomerExists(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_CheckCustomer", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcomm.Parameters.Add(new SqlParameter("CustomerId", id));
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);

            return Convert.ToBoolean(dset.Tables[0].Rows[0][0]);
        }

        public async Task<List<Customer>> GetAll()
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetCustomer", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            var converted =
                JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(), typeof(List<Customer>)) as List<Customer>;
            return converted;
        }

        public async Task<Customer> GetById(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetCustomerDetails", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcomm.Parameters.Add(new SqlParameter("CustomerId", id));
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            var converted =
                JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(), typeof(List<Customer>)) as List<Customer>;

            return converted.FirstOrDefault();
        }

        public async Task<List<Customer>> GetBySupportRepId(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetCustomerBySupportRep", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcomm.Parameters.Add(new SqlParameter("SupportRepId", id));
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            var converted =
                JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(), typeof(List<Customer>)) as List<Customer>;
            return converted;
        }

        public async Task<Customer> Add(Customer newCustomer)
        {
            return newCustomer;
        }

        public async Task<bool> Update(Customer customer)
        {
            if (!await CustomerExists(customer.Id))
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
    }
}