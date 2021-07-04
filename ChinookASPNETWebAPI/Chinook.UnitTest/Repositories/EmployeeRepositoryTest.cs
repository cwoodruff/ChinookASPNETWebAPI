using System.Threading.Tasks;
using Chinook.Domain.Repositories;
using Xunit;

namespace Chinook.UnitTest.Repository
{
    public class EmployeeRepositoryTest
    {
        private readonly IEmployeeRepository _repo;

        public EmployeeRepositoryTest(IEmployeeRepository e) => _repo = e;

        [Fact]
        public async Task EmployeeGetAll()
        {
            // Act
            var employees = await _repo.GetAll();

            // Assert
            Assert.True(employees.Count > 1, "The number of employees was not greater than 1");
        }
    }
}