using System.Linq;
using Chinook.Domain.Supervisor;
using Xunit;

namespace Chinook.UnitTest.Supervisor
{
    public class MediaTypeRepositoryTest
    {
        private readonly IChinookSupervisor _super;

        public MediaTypeRepositoryTest()
        {
        }

        [Fact]
        public void MediaTypeGetAll()
        {
            // Act
            var mediaTypes = _super.GetAllMediaType().ToList();

            // Assert
            Assert.True(mediaTypes.Count > 1, "The number of media types was not greater than 1");
        }
    }
}