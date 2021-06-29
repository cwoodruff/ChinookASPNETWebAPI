using System.Linq;
using Chinook.Domain.Supervisor;
using Xunit;

namespace Chinook.UnitTest.Supervisor
{
    public class EmployeeSupervisorTest
    {
        private readonly IChinookSupervisor _super;

        public EmployeeSupervisorTest(IChinookSupervisor s) => _super = s;

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