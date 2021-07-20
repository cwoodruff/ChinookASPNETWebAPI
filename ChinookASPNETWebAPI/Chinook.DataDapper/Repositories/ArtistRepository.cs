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
    public class ArtistRepository : IArtistRepository
    {
        private readonly DbInfo _dbInfo;

        public ArtistRepository(DbInfo dbInfo)
        {
            _dbInfo = dbInfo;
        }

        private IDbConnection Connection => new SqlConnection(_dbInfo.ConnectionStrings);

        public void Dispose()
        {
        }

        private async Task<bool> ArtistExists(int id) =>
            await Connection.ExecuteScalarAsync<bool>("select count(1) from Artist where Id = @id", new { id });

        public async Task<List<Artist>> GetAll()
        {
            using IDbConnection cn = Connection;
            cn.Open();
            var artists = await cn.QueryAsync<Artist>("Select * From Artist");
            return artists.ToList();
        }

        public async Task<Artist> GetById(int id)
        {
            using var cn = Connection;
            cn.Open();
            return cn.QueryFirstOrDefault<Artist>("Select * From Artist WHERE Id = @Id", new { id });
        }

        public async Task<Artist> Add(Artist newArtist)
        {
            using var cn = Connection;
            cn.Open();

            newArtist.Id = await cn.InsertAsync(new Artist { Name = newArtist.Name });

            return newArtist;
        }

        public async Task<bool> Update(Artist artist)
        {
            if (!await ArtistExists(artist.Id))
                return false;

            try
            {
                using var cn = Connection;
                cn.Open();
                return await cn.UpdateAsync(artist);
            }
            catch (Exception)
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
                return await cn.DeleteAsync(new Artist { Id = id });
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}