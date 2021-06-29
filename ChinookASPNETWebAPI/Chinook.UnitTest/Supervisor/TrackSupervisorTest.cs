using System.Linq;
using Chinook.Domain.Supervisor;
using Xunit;

namespace Chinook.UnitTest.Supervisor
{
    public class TrackSupervisorTest
    {
        private readonly IChinookSupervisor _super;

        public TrackSupervisorTest(IChinookSupervisor s) => _super = s;

        [Fact]
        public void TrackGetAll()
        {
            // Act
            var tracks = _super.GetAllTrack().ToList();

            // Assert
            Assert.True(tracks.Count > 1, "The number of tracks was not greater than 1");
        }
    }
}