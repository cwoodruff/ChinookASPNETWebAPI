using System.Collections.Generic;
using System.Linq;
using Chinook.DataEF;
using Chinook.Domain.Repositories;
using Chinook.Domain.Entities;

namespace Chinook.DataEFCore.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ChinookContext _context;

        public GenreRepository(ChinookContext context)
        {
            _context = context;
        }

        private bool GenreExists(int id) =>
            _context.Genres.Any(g => g.Id == id);

        public void Dispose() => _context.Dispose();

        public List<Genre> GetAll() =>
            _context.Genres.ToList();

        public Genre GetById(int id)
        {
            var dbGenre = _context.Genres.Find(id);
            return dbGenre;
        }

        public Genre Add(Genre newGenre)
        {
            _context.Genres.Add(newGenre);
            _context.SaveChanges();
            return newGenre;
        }

        public bool Update(Genre genre)
        {
            if (!GenreExists(genre.Id))
                return false;
            _context.Genres.Update(genre);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            if (!GenreExists(id))
                return false;
            var toRemove = _context.Genres.Find(id);
            _context.Genres.Remove(toRemove);
            _context.SaveChanges();
            return true;
        }
    }
}