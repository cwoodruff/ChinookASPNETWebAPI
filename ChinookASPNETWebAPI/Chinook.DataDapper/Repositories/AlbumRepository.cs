using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Chinook.Domain.DbInfo;
using Chinook.Domain.Entities;
using Chinook.Domain.Repositories;
using Microsoft.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;

namespace Chinook.DataDapper.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly DbInfo _dbInfo;

        public AlbumRepository(DbInfo dbInfo)
        {
            _dbInfo = dbInfo;
        }

        private IDbConnection Connection => new SqlConnection(_dbInfo.ConnectionStrings);

        public void Dispose()
        {
        }

        private async Task<bool> AlbumExists(int id) =>
            await Connection.ExecuteScalarAsync<bool>("select count(1) from Album where Id = @id", new { id });

        public async Task<List<Album>> GetAll()
        {
            using IDbConnection cn = Connection;
            cn.Open();
            var albums = await cn.QueryAsync<Album>("Select * From Album");
            return albums.ToList();
        }

        public async Task<Album> GetById(int? id)
        {
            using var cn = Connection;
            cn.Open();
            var album = await cn.QueryFirstOrDefaultAsync<Album>("Select * From Album WHERE Id = @id", new { id });
            return album;
        }

        public async Task<List<Album>> GetByArtistId(int id)
        {
            using var cn = Connection;
            cn.Open();
            var albums = await cn.QueryAsync<Album>("Select * From Album WHERE ArtistId = @Id", new { id });
            return albums.ToList();
        }

        public async Task<Album> Add(Album newAlbum)
        {
            using var cn = Connection;
            cn.Open();
            var albumId = cn.Insert(newAlbum);
            newAlbum.Id = (int)albumId;

            return newAlbum;
        }

        public async Task<bool> Update(Album album)
        {
            if (!await AlbumExists(album.Id))
                return false;

            try
            {
                using var cn = Connection;
                cn.Open();
                return await cn.UpdateAsync(album);
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
                return await cn.DeleteAsync(new Album { Id = id });
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}