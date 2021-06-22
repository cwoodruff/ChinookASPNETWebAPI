using System.Linq;
using Chinook.Domain.Supervisor;
using Xunit;

namespace Chinook.UnitTest.Supervisor
{
    public class EmployeeRepositoryTest
    {
        private readonly IChinookSupervisor _super;

        public EmployeeRepositoryTest()
        {
        }

        [Fact]
        public void EmployeeGetAll()
        {
            // Act
            var employees = _super.GetAllEmployee().ToList();

            // Assert
            Assert.True(employees.Count > 1, "The number of employees was not greater than 1");
        }
    }
}