using System.Collections.Generic;
using System.Linq;
using Chinook.DataEF;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.DataEFCore.Repositories
{
    public class TrackRepository : ITrackRepository
    {
        private readonly ChinookContext _context;

        public TrackRepository(ChinookContext context)
        {
            _context = context;
        }

        private bool TrackExists(int id) =>
            _context.Tracks.Any(i => i.Id == id);

        public void Dispose() => _context.Dispose();

        public List<Track> GetAll() =>
            _context.Tracks.ToList();

        public Track GetById(int id) =>
            _context.Tracks.Find(id);

        public Track Add(Track newTrack)
        {
            _context.Tracks.Add(newTrack);
            _context.SaveChanges();
            return newTrack;
        }

        public bool Update(Track track)
        {
            if (!TrackExists(track.Id))
                return false;
            _context.Tracks.Update(track);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            if (!TrackExists(id))
                return false;
            var toRemove = _context.Tracks.Find(id);
            _context.Tracks.Remove(toRemove);
            _context.SaveChanges();
            return true;
        }

        public List<Track> GetByAlbumId(int id) =>
            _context.Tracks.Where(a => a.AlbumId == id).ToList();

        public List<Track> GetByGenreId(int id) =>
            _context.Tracks.Where(a => a.GenreId == id).ToList();

        public List<Track> GetByMediaTypeId(int id) =>
            _context.Tracks.Where(a => a.MediaTypeId == id).ToList();
        
        public List<Track> GetByPlaylistId(int id) =>
            _context.PlaylistTracks.Where(p => p.PlaylistId == id).Select(p => p.Track).ToList();

        public List<Track> GetByArtistId(int id) => 
            _context.Albums.Where(a => a.ArtistId == 5).SelectMany(t => t.Tracks).ToList();

            public List<Track> GetByInvoiceId(int id) =>_context.Tracks
                .Where(c => c.InvoiceLines.Any(o => o.InvoiceId == id))
                .ToList();
        }
}