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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly SqlConnection _sqlconn;

        public EmployeeRepository(SqlConnection sqlconn)
        {
            _sqlconn = sqlconn;
        }

        public void Dispose()
        {
        }

        private async Task<bool> EmployeeExists(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_CheckEmployee", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcomm.Parameters.Add(new SqlParameter("EmployeeId", id));
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);

            return Convert.ToBoolean(dset.Tables[0].Rows[0][0]);
        }

        public async Task<List<Employee>> GetAll()
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetEmployee", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            var converted =
                JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(), typeof(List<Employee>)) as List<Employee>;
            return converted;
        }

        public async Task<Employee> GetById(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetEmployeeDetails", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcomm.Parameters.Add(new SqlParameter("EmployeeId", id));
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            var converted =
                JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(), typeof(List<Employee>)) as List<Employee>;

            return converted.FirstOrDefault();
        }

        public async Task<Employee> GetReportsTo(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetEmployeeReportTo", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcomm.Parameters.Add(new SqlParameter("EmployeeId", id));
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            var converted =
                JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(), typeof(List<Employee>)) as List<Employee>;

            return converted.FirstOrDefault();
        }

        public async Task<Employee> Add(Employee newEmployee)
        {
            return newEmployee;
        }

        public async Task<bool> Update(Employee employee)
        {
            if (!await EmployeeExists(employee.Id))
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

        public async Task<List<Employee>> GetDirectReports(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetEmployeeDirectReports", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            var converted =
                JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(), typeof(List<Employee>)) as List<Employee>;
            return converted;
        }
    }
}