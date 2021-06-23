using System;
using System.Collections.Generic;
using System.Linq;
using Chinook.Domain.ApiModels;
using Chinook.Domain.Extensions;
using Microsoft.Extensions.Caching.Memory;

namespace Chinook.Domain.Supervisor
{
    public partial class ChinookSupervisor
    {
        public IEnumerable<EmployeeApiModel> GetAllEmployee()
        {
            var employees = _employeeRepository.GetAll().ConvertAll();
            foreach (var employee in employees)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat((object?) "Employee-", employee.Id), employee, cacheEntryOptions);
            }
            return employees;
        }

        public EmployeeApiModel GetEmployeeById(int id)
        {
            var employeeApiModelCached = _cache.Get<EmployeeApiModel>(string.Concat("Employee-", id));

            if (employeeApiModelCached != null)
            {
                return employeeApiModelCached;
            }
            else
            {
                var employeeApiModel = (_employeeRepository.GetById(id)).Convert();
                //employeeApiModel.Customers = (GetCustomerBySupportRepId(employeeApiModel.Id)).ToList();
                //employeeApiModel.DirectReports = (GetEmployeeDirectReports(employeeApiModel.EmployeeId)).ToList();
                // employeeApiModel.Manager = employeeApiModel.ReportsTo.HasValue
                //     ? GetEmployeeReportsTo(id)
                //     : null;
                // if (employeeApiModel.Manager != null)
                //     employeeApiModel.ReportsToName = employeeApiModel.ReportsTo.HasValue
                //         ? $"{employeeApiModel.Manager.LastName}, {employeeApiModel.Manager.FirstName}"
                //         : string.Empty;

                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat((object?) "Employee-", employeeApiModel.Id), employeeApiModel, cacheEntryOptions);

                return employeeApiModel;
            }
        }

        public EmployeeApiModel GetEmployeeReportsTo(int id)
        {
            var employee = _employeeRepository.GetReportsTo(id);
            return employee.Convert();
        }

        public EmployeeApiModel AddEmployee(EmployeeApiModel newEmployeeApiModel)
        {
            var employee = newEmployeeApiModel.Convert();

            employee = _employeeRepository.Add(employee);
            newEmployeeApiModel.Id = employee.Id;
            return newEmployeeApiModel;
        }

        public bool UpdateEmployee(EmployeeApiModel employeeApiModel)
        {
            var employee = _employeeRepository.GetById(employeeApiModel.Id);

            if (employee == null) return false;
            employee.Id = employeeApiModel.Id;
            employee.LastName = employeeApiModel.LastName ?? string.Empty;
            employee.FirstName = employeeApiModel.FirstName ?? string.Empty;
            employee.Title = employeeApiModel.Title ?? string.Empty;
            employee.ReportsTo = employeeApiModel.ReportsTo;
            employee.BirthDate = employeeApiModel.BirthDate;
            employee.HireDate = employeeApiModel.HireDate;
            employee.Address = employeeApiModel.Address ?? string.Empty;
            employee.City = employeeApiModel.City ?? string.Empty;
            employee.State = employeeApiModel.State ?? string.Empty;
            employee.Country = employeeApiModel.Country ?? string.Empty;
            employee.PostalCode = employeeApiModel.PostalCode ?? string.Empty;
            employee.Phone = employeeApiModel.Phone ?? string.Empty;
            employee.Fax = employeeApiModel.Fax ?? string.Empty;
            employee.Email = employeeApiModel.Email ?? string.Empty;

            return _employeeRepository.Update(employee);
        }

        public bool DeleteEmployee(int id) 
            => _employeeRepository.Delete(id);

        public IEnumerable<EmployeeApiModel> GetEmployeeDirectReports(int id)
        {
            var employees = _employeeRepository.GetDirectReports(id);
            return employees.ConvertAll();
        }

        public IEnumerable<EmployeeApiModel> GetDirectReports(int id)
        {
            var employees = _employeeRepository.GetDirectReports(id);
            return employees.ConvertAll();
        }
    }
}