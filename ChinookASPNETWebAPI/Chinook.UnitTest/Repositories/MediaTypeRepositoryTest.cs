using System.Threading.Tasks;
using Chinook.Domain.Repositories;
using Xunit;

namespace Chinook.UnitTest.Repository
{
    public class MediaTypeRepositoryTest
    {
        private readonly IMediaTypeRepository _repo;

        public MediaTypeRepositoryTest(IMediaTypeRepository m) => _repo = m;

        [Fact]
        public async Task MediaTypeGetAll()
        {
            // Act
            var mediaTypes = await _repo.GetAll();

            // Assert
            Assert.True(mediaTypes.Count > 1, "The number of media types was not greater than 1");
        }
    }
}