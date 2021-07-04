using System.Linq;
using System.Threading.Tasks;
using Chinook.Domain.Supervisor;
using Xunit;

namespace Chinook.UnitTest.Supervisor
{
    public class TrackSupervisorTest
    {
        private readonly IChinookSupervisor _super;

        public TrackSupervisorTest(IChinookSupervisor s) => _super = s;

        [Fact]
        public async Task TrackGetAll()
        {
            // Act
            var tracks = (await _super.GetAllTrack()).ToList();

            // Assert
            Assert.True(tracks.Count > 1, "The number of tracks was not greater than 1");
        }
    }
}