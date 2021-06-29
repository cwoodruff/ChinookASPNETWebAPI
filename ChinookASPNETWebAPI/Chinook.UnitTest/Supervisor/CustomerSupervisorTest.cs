using System.Linq;
using Chinook.Domain.Supervisor;
using Xunit;

namespace Chinook.UnitTest.Supervisor
{
    public class CustomerSupervisorTest
    {
        private readonly IChinookSupervisor _super;

        public CustomerSupervisorTest(IChinookSupervisor s) => _super = s;

        [Fact]
        public void CustomerGetAll()
        {
            // Act
            var customers = _super.GetAllCustomer().ToList();

            // Assert
            Assert.True(customers.Count > 1, "The number of customers was not greater than 1");
        }
    }
}