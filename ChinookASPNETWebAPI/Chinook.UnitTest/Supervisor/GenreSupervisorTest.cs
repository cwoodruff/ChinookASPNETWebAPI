using System.Linq;
using Chinook.Domain.Supervisor;
using Xunit;

namespace Chinook.UnitTest.Supervisor
{
    public class GenreRepositoryTest
    {
        private readonly IChinookSupervisor _super;

        public GenreRepositoryTest()
        {
        }

        [Fact]
        public void GenreGetAll()
        {
            // Act
            var genres = _super.GetAllGenre().ToList();

            // Assert
            Assert.True(genres.Count > 1, "The number of genres was not greater than 1");
        }
    }
}