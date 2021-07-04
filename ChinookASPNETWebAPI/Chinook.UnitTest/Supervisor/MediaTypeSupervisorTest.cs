using System.Linq;
using System.Threading.Tasks;
using Chinook.Domain.Supervisor;
using Xunit;

namespace Chinook.UnitTest.Supervisor
{
    public class MediaTypeSupervisorTest
    {
        private readonly IChinookSupervisor _super;

        public MediaTypeSupervisorTest(IChinookSupervisor s) => _super = s;

        [Fact]
        public async Task MediaTypeGetAll()
        {
            // Act
            var mediaTypes = (await _super.GetAllMediaType()).ToList();

            // Assert
            Assert.True(mediaTypes.Count > 1, "The number of media types was not greater than 1");
        }
    }
}