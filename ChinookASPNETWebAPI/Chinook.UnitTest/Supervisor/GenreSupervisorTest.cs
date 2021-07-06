using System.Linq;
using System.Threading.Tasks;
using Chinook.Domain.Supervisor;
using Xunit;

namespace Chinook.UnitTest.Supervisor
{
    public class GenreSupervisorTest
    {
        private readonly IChinookSupervisor _super;

        public GenreSupervisorTest(IChinookSupervisor s) => _super = s;

        [Fact]
        public async Task GenreGetAll()
        {
            // Act
            var genres = (await _super.GetAllGenre()).ToList();

            // Assert
            Assert.True(genres.Count > 1, "The number of genres was not greater than 1");
        }
    }
}