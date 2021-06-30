using System.Linq;
using Chinook.DataEF;
using Chinook.DataEFCore.Repositories;
using Chinook.Domain.Supervisor;
using Xunit;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;

namespace Chinook.UnitTest.Supervisor
{
    public class AlbumSupervisorTest
    {
        private readonly IChinookSupervisor _super;
        private readonly AlbumRepository _albumRepo;
        private readonly ChinookContext _context;

        public AlbumSupervisorTest()
        {
            var builder = new DbContextOptionsBuilder<ChinookContext>();
            builder.UseInMemoryDatabase("ChinookUnitTests");
            _context = new ChinookContext(builder.Options);
            _albumRepo = new AlbumRepository(_context);
            _super = new ChinookSupervisor(_albumRepo, null, null, null, null, null, null, null, null, null, new MemoryCache(new MemoryCacheOptions()));
        }

        [Fact]
        public void AlbumGetAll()
        {
            var album1 = new Domain.Entities.Album {
                Id = 12,
            };
            var album2 = new Domain.Entities.Album
            {
                Id = 123,
            };

            // Arrange
            _context.Albums.Add(album1);
            _context.Albums.Add(album2);
            _context.SaveChanges();

            // Act
            var albums = _super.GetAllAlbum().ToList();

            // Assert
            albums.Count.Should().Be(2);
            albums.Should().Contain(x => x.Id == 12);
            albums.Should().Contain(x => x.Id == 123);
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