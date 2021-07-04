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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DbInfo _dbInfo;

        public CustomerRepository(DbInfo dbInfo)
        {
            _dbInfo = dbInfo;
        }

        private IDbConnection Connection => new SqlConnection(_dbInfo.ConnectionStrings);
        
        public void Dispose()
        {
            
        }

        private async Task<bool> CustomerExists(int id) => 
            await Connection.ExecuteScalarAsync<bool>("select count(1) from Customer where Id = @id", new {id});

        public async Task<List<Customer>> GetAll()
        {
            using IDbConnection cn = Connection;
            cn.Open();
            var customers = await cn.QueryAsync<Customer>("Select * From Customer");
            return customers.ToList();
        }

        public async Task<Customer> GetById(int id)
        {
            using var cn = Connection;
            cn.Open();
            return cn.QueryFirstOrDefault<Customer>("Select * From Customer WHERE Id = @Id", new {id});
        }

        public async Task<List<Customer>> GetBySupportRepId(int id)
        {
            using var cn = Connection;
            cn.Open();
            var customers = await cn.QueryAsync<Customer>("Select * From Customer WHERE ArtistId = @Id", new {id});
            return customers.ToList();
        }

        public async Task<Customer> Add(Customer newCustomer)
        {
            using var cn = Connection;
            cn.Open();

            newCustomer.Id = await cn.InsertAsync(
                new Customer
                {
                    Id = newCustomer.Id,
                    FirstName = newCustomer.FirstName,
                    LastName = newCustomer.LastName,
                    Company = newCustomer.Company,
                    Address = newCustomer.Address,
                    City = newCustomer.City,
                    State = newCustomer.State,
                    Country = newCustomer.Country,
                    PostalCode = newCustomer.PostalCode,
                    Phone = newCustomer.Phone,
                    Fax = newCustomer.Fax,
                    Email = newCustomer.Email,
                    SupportRepId = newCustomer.SupportRepId
                });

            return newCustomer;
        }

        public async Task<bool> Update(Customer customer)
        {
            if (!await CustomerExists(customer.Id))
                return false;

            try
            {
                using var cn = Connection;
                cn.Open();
                return await cn.UpdateAsync(customer);
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
                return await cn.DeleteAsync(new Customer {Id = id});
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}