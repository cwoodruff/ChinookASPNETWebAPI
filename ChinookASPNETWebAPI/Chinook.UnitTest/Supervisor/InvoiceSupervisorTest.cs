using System.Linq;
using System.Threading.Tasks;
using Chinook.Domain.Supervisor;
using Xunit;

namespace Chinook.UnitTest.Supervisor
{
    public class InvoiceSupervisorTest
    {
        private readonly IChinookSupervisor _super;

        public InvoiceSupervisorTest(IChinookSupervisor s) => _super = s;

        [Fact]
        public async Task InvoiceGetAll()
        {
            // Act
            var invoices = (await _super.GetAllInvoice()).ToList();

            // Assert
            Assert.True(invoices.Count > 1, "The number of invoices was not greater than 1");
        }
    }
}