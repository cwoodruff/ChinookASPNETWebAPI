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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DbInfo _dbInfo;

        public EmployeeRepository(DbInfo dbInfo)
        {
            _dbInfo = dbInfo;
        }

        private IDbConnection Connection => new SqlConnection(_dbInfo.ConnectionStrings);

        public void Dispose()
        {
        }

        private async Task<bool> EmployeeExists(int id) =>
            await Connection.ExecuteScalarAsync<bool>("select count(1) from Employee where Id = @id", new { id });

        public async Task<List<Employee>> GetAll()
        {
            using IDbConnection cn = Connection;
            cn.Open();
            var employees = await cn.QueryAsync<Employee>("Select * From Employee");
            return employees.ToList();
        }

        public async Task<Employee> GetById(int id)
        {
            using var cn = Connection;
            cn.Open();
            return cn.QueryFirstOrDefault<Employee>("Select * From Employee WHERE Id = @Id", new { id });
        }

        public async Task<Employee> GetReportsTo(int id)
        {
            using var cn = Connection;
            cn.Open();
            return cn.QueryFirstOrDefault<Employee>("Select * From Employee WHERE ReportsTo = @Id", new { id });
        }

        public async Task<Employee> Add(Employee newEmployee)
        {
            using var cn = Connection;
            cn.Open();

            newEmployee.Id = await cn.InsertAsync(
                new Employee
                {
                    LastName = newEmployee.LastName,
                    FirstName = newEmployee.FirstName,
                    Title = newEmployee.Title,
                    ReportsTo = newEmployee.ReportsTo,
                    BirthDate = newEmployee.BirthDate,
                    HireDate = newEmployee.HireDate,
                    Address = newEmployee.Address,
                    City = newEmployee.City,
                    State = newEmployee.State,
                    Country = newEmployee.Country,
                    PostalCode = newEmployee.PostalCode,
                    Phone = newEmployee.Phone,
                    Fax = newEmployee.Fax,
                    Email = newEmployee.Email
                });

            return newEmployee;
        }

        public async Task<bool> Update(Employee employee)
        {
            if (!await EmployeeExists(employee.Id))
                return false;

            try
            {
                using var cn = Connection;
                cn.Open();
                return await cn.UpdateAsync(employee);
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
                using var cn = Connection;
                cn.Open();
                return await cn.DeleteAsync(new Employee { Id = id });
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Employee>> GetDirectReports(int id)
        {
            using var cn = Connection;
            cn.Open();
            var employees = await cn.QueryAsync<Employee>("Select * From Employee WHERE ReportsTo = @Id", new { id });
            return employees.ToList();
        }
    }
}