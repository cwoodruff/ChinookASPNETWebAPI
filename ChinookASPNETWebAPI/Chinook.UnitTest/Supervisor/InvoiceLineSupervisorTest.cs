using System.Linq;
using Chinook.Domain.Supervisor;
using Xunit;

namespace Chinook.UnitTest.Supervisor
{
    public class InvoiceLineSupervisorTest
    {
        private readonly IChinookSupervisor _super;

        public InvoiceLineSupervisorTest(IChinookSupervisor s) => _super = s;

        [Fact]
        public void InvoiceLineGetAll()
        {
            // Act
            var invoiceLines = _super.GetAllInvoiceLine().ToList();

            // Assert
            Assert.True(invoiceLines.Count > 1, "The number of invoice lines was not greater than 1");
        }
    }
}