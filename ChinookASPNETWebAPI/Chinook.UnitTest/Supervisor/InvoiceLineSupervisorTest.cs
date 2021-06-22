using System.Linq;
using Chinook.Domain.Supervisor;
using Xunit;

namespace Chinook.UnitTest.Supervisor
{
    public class InvoiceLineRepositoryTest
    {
        private readonly IChinookSupervisor _super;

        public InvoiceLineRepositoryTest()
        {
        }

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