using System.Linq;
using Chinook.Domain.Supervisor;
using Xunit;

namespace Chinook.UnitTest.Supervisor
{
    public class AlbumRepositoryTest
    {
        private readonly IChinookSupervisor _super;

        public AlbumRepositoryTest()
        {
        }

        [Fact]
        public void AlbumGetAll()
        {
            // Arrange

            // Act
            var albums = _super.GetAllAlbum().ToList();

            // Assert
            Assert.True(albums.Count > 1, "The number of albums was not greater than 1");
        }

        [Fact]
        public void AlbumGetOne()
        {
            // Arrange
            var id = 1;

            // Act
            var album = _super.GetAlbumById(id);

            // Assert
            Assert.Equal(id, album.Id);
        }
    }
}