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
    public class TrackRepository : ITrackRepository
    {
        private readonly DbInfo _dbInfo;

        public TrackRepository(DbInfo dbInfo)
        {
            _dbInfo = dbInfo;
        }

        private IDbConnection Connection => new SqlConnection(_dbInfo.ConnectionStrings);

        public void Dispose()
        {
        }

        private async Task<bool> TrackExists(int id) =>
            await Connection.ExecuteScalarAsync<bool>("select count(1) from Track where Id = @id", new { id });

        public async Task<List<Track>> GetAll()
        {
            using IDbConnection cn = Connection;
            cn.Open();
            var tracks = await cn.QueryAsync<Track>("Select * From Track");
            return tracks.ToList();
        }

        public async Task<Track> GetById(int id)
        {
            using var cn = Connection;
            cn.Open();
            return await cn.QueryFirstOrDefaultAsync<Track>("Select * From Track WHERE Id = @Id", new { id });
        }

        public async Task<Track> Add(Track newTrack)
        {
            using var cn = Connection;
            cn.Open();

            newTrack.Id = await cn.InsertAsync(
                new Track
                {
                    Name = newTrack.Name,
                    AlbumId = newTrack.AlbumId,
                    MediaTypeId = newTrack.MediaTypeId,
                    GenreId = newTrack.GenreId,
                    Composer = newTrack.Composer,
                    Milliseconds = newTrack.Milliseconds,
                    Bytes = newTrack.Bytes,
                    UnitPrice = newTrack.UnitPrice
                });

            return newTrack;
        }

        public async Task<bool> Update(Track track)
        {
            if (!await TrackExists(track.Id))
                return false;

            try
            {
                using var cn = Connection;
                cn.Open();
                return await cn.UpdateAsync(track);
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
                return await cn.DeleteAsync(new Track { Id = id });
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Track>> GetByInvoiceId(int id)
        {
            using var cn = Connection;
            cn.Open();
            var tracks = await cn.QueryAsync<Track>(
                "SELECT T.Id, T.Name, T.AlbumId, T.MediaTypeId, T.GenreId, T.Composer, T.Milliseconds, T.Bytes, T.UnitPrice FROM Track AS T INNER JOIN InvoiceLine AS IL ON T.Id = IL.TrackId WHERE IL.InvoiceID = @Id",
                new { id });
            return tracks.ToList();
        }

        public async Task<List<Track>> GetByPlaylistId(int id)
        {
            using var cn = Connection;
            cn.Open();
            var tracks = await cn.QueryAsync<Track>(
                "SELECT T.Id, T.Name, T.AlbumId, T.MediaTypeId, T.GenreId, T.Composer, T.Milliseconds, T.Bytes, T.UnitPrice FROM Track AS T INNER JOIN PlaylistTrack AS PLT ON T.Id = PLT.TrackId WHERE PLT.PlayListId = @Id",
                new { id });
            return tracks.ToList();
        }

        public async Task<List<Track>> GetByArtistId(int id)
        {
            using var cn = Connection;
            cn.Open();
            var tracks = await cn.QueryAsync<Track>(
                "SELECT T.Id, T.Name, T.AlbumId, T.MediaTypeId, T.GenreId, T.Composer, T.Milliseconds, T.Bytes, T.UnitPrice FROM Track AS T INNER JOIN Album AS A ON T.AlbumId = A.Id WHERE A.Id = @Id",
                new { id });
            return tracks.ToList();
        }

        public async Task<List<Track>> GetByAlbumId(int id)
        {
            using var cn = Connection;
            cn.Open();
            var tracks = await cn.QueryAsync<Track>("Select * From Track WHERE AlbumId = @Id", new { id });
            return tracks.ToList();
        }

        public async Task<List<Track>> GetByGenreId(int id)
        {
            using var cn = Connection;
            cn.Open();
            var tracks = await cn.QueryAsync<Track>("Select * From Track WHERE GenreId = @Id", new { id });
            return tracks.ToList();
        }

        public async Task<List<Track>> GetByMediaTypeId(int id)
        {
            using var cn = Connection;
            cn.Open();
            var tracks = await cn.QueryAsync<Track>("Select * From Track WHERE MediaTypeId = @Id", new { id });
            return tracks.ToList();
        }
    }
}