using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Chinook.Domain.DbInfo;
using Chinook.Domain.Entities;
using Chinook.Domain.Repositories;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

namespace Chinook.DataDapper.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly DbInfo _dbInfo;

        public GenreRepository(DbInfo dbInfo)
        {
            _dbInfo = dbInfo;
        }

        private IDbConnection Connection => new SqlConnection(_dbInfo.ConnectionStrings);

        public void Dispose()
        {
            
        }

        private bool GenreExists(int id) =>
            Connection.ExecuteScalar<bool>("select count(1) from Genre where Id = @id", new {id});

        public List<Genre> GetAll()
        {
            using IDbConnection cn = Connection;
            cn.Open();
            var genres = Connection.Query<Genre>("Select * From Genre");
            return genres.ToList();
        }

        public Genre GetById(int id)
        {
            using var cn = Connection;
            cn.Open();
            return cn.QueryFirstOrDefault<Genre>("Select * From Genre WHERE Id = @Id", new {id});
        }

        public Genre Add(Genre newGenre)
        {
            using var cn = Connection;
            cn.Open();

            newGenre.Id = (int) cn.Insert(new Genre {Name = newGenre.Name});

            return newGenre;
        }

        public bool Update(Genre genre)
        {
            if (!GenreExists(genre.Id))
                return false;

            try
            {
                using var cn = Connection;
                cn.Open();
                return cn.Update(genre);
            }
            catch(Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                using var cn = Connection;
                cn.Open();
                return cn.Delete(new Genre {Id = id});
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}