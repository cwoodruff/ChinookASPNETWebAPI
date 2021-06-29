using Chinook.Domain.Repositories;
using Xunit;

namespace Chinook.UnitTest.Repository
{
    public class CustomerRepositoryTest
    {
        private readonly ICustomerRepository _repo;

        public CustomerRepositoryTest(ICustomerRepository c) => _repo = c;

        [Fact]
        public void CustomerGetAll()
        {
            // Act
            var customers = _repo.GetAll();

            // Assert
            Assert.True(customers.Count > 1, "The number of customers was not greater than 1");
        }
    }
}