using System.Collections.Generic;
using System.Linq;
using Chinook.DataEF;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.DataEFCore.Repositories
{
    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly ChinookContext _context;

        public PlaylistRepository(ChinookContext context)
        {
            _context = context;
        }

        private bool PlaylistExists(int id) =>
            _context.Playlists.Any(i => i.Id == id);

        public void Dispose() => _context.Dispose();

        public List<Playlist> GetAll() =>
            _context.Playlists.ToList();

        public Playlist GetById(int id) =>
            _context.Playlists.Find(id);

        public Playlist Add(Playlist newPlaylist)
        {
            _context.Playlists.Add(newPlaylist);
            _context.SaveChanges();
            return newPlaylist;
        }

        public bool Update(Playlist playlist)
        {
            if (!PlaylistExists(playlist.Id))
                return false;
            _context.Playlists.Update(playlist);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            if (!PlaylistExists(id))
                return false;
            var toRemove = _context.Playlists.Find(id);
            _context.Playlists.Remove(toRemove);
            _context.SaveChanges();
            return true;
        }

        public List<Playlist> GetByTrackId(int id)
        {
            return _context.Playlists
                .Where(c => c.PlaylistTracks.Any(o => o.TrackId == id))
                .ToList();
        }
    }
}