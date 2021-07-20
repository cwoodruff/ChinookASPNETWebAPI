using System.Linq;
using System.Threading.Tasks;
using Chinook.Domain.Supervisor;
using Xunit;

namespace Chinook.UnitTest.Supervisor
{
    public class ArtistSupervisorTest
    {
        private readonly IChinookSupervisor _super;

        public ArtistSupervisorTest(IChinookSupervisor s) => _super = s;

        [Fact]
        public async Task ArtistGetAll()
        {
            // Act
            var artists = (await _super.GetAllArtist()).ToList();

            // Assert
            Assert.True(artists.Count > 1, "The number of artists was not greater than 1");
        }
    }
}