using System.Collections.Generic;
using System.Linq;
using Chinook.DataEF;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.DataEFCore.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly ChinookContext _context;

        public AlbumRepository(ChinookContext context)
        {
            _context = context;
        }

        private bool AlbumExists(int id) =>
            _context.Albums.Any(a => a.Id == id);

        public void Dispose() => _context.Dispose();

        public List<Album> GetAll() => _context.Albums.ToList();

        public Album GetById(int? id)
        {
            var dbAlbum = _context.Albums.Find(id);
            return dbAlbum;
        }

        public Album Add(Album newAlbum)
        {
            _context.Albums.Add(newAlbum);
            _context.SaveChanges();
            return newAlbum;
        }

        public bool Update(Album album)
        {
            if (!AlbumExists(album.Id))
                return false;
            _context.Albums.Update(album);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            if (!AlbumExists(id))
                return false;
            var toRemove = _context.Albums.Find(id);
            _context.Albums.Remove(toRemove);
            _context.SaveChanges();
            return true;
        }

        public List<Album> GetByArtistId(int id) =>
            _context.Albums.Where(a => a.ArtistId == id).ToList();
    }
}