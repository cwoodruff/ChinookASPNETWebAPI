using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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

        private async Task<bool> GenreExists(int id) =>
            await Connection.ExecuteScalarAsync<bool>("select count(1) from Genre where Id = @id", new {id});

        public async Task<List<Genre>> GetAll()
        {
            using IDbConnection cn = Connection;
            cn.Open();
            var genres = await cn.QueryAsync<Genre>("Select * From Genre");
            return genres.ToList();
        }

        public async Task<Genre> GetById(int id)
        {
            using var cn = Connection;
            cn.Open();
            return cn.QueryFirstOrDefault<Genre>("Select * From Genre WHERE Id = @Id", new {id});
        }

        public async Task<Genre> Add(Genre newGenre)
        {
            using var cn = Connection;
            cn.Open();

            newGenre.Id = await cn.InsertAsync(new Genre {Name = newGenre.Name});

            return newGenre;
        }

        public async Task<bool> Update(Genre genre)
        {
            if (!await GenreExists(genre.Id))
                return false;

            try
            {
                using var cn = Connection;
                cn.Open();
                return await cn.UpdateAsync(genre);
            }
            catch(Exception)
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                using var cn = Connection;
                cn.Open();
                return await cn.DeleteAsync(new Genre {Id = id});
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}