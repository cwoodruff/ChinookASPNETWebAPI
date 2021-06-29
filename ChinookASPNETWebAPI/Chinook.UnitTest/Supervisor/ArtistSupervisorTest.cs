using System.Linq;
using Chinook.Domain.Supervisor;
using Xunit;

namespace Chinook.UnitTest.Supervisor
{
    public class ArtistSupervisorTest
    {
        private readonly IChinookSupervisor _super;

        public ArtistSupervisorTest(IChinookSupervisor s) => _super = s;
        
        [Fact]
        public void ArtistGetAll()
        {
            // Act
            var artists = _super.GetAllArtist().ToList();

            // Assert
            Assert.True(artists.Count > 1, "The number of artists was not greater than 1");
        }
    }
}