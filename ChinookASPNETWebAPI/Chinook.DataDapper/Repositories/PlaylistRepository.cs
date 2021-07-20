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
    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly DbInfo _dbInfo;

        public PlaylistRepository(DbInfo dbInfo)
        {
            _dbInfo = dbInfo;
        }

        private IDbConnection Connection => new SqlConnection(_dbInfo.ConnectionStrings);

        public void Dispose()
        {
        }

        private async Task<bool> PlaylistExists(int id) =>
            await Connection.ExecuteScalarAsync<bool>("select count(1) from Playlist where Id = @id", new { id });

        public async Task<List<Playlist>> GetAll()
        {
            using IDbConnection cn = Connection;
            cn.Open();
            var playlists = await cn.QueryAsync<Playlist>("Select * From Playlist");
            return playlists.ToList();
        }

        public async Task<Playlist> GetById(int id)
        {
            using var cn = Connection;
            cn.Open();
            return cn.QueryFirstOrDefault<Playlist>("Select * From Playlist WHERE Id = @Id", new { id });
        }

        public async Task<Playlist> Add(Playlist newPlaylist)
        {
            using var cn = Connection;
            cn.Open();

            newPlaylist.Id = await cn.InsertAsync(new Playlist { Name = newPlaylist.Name });

            return newPlaylist;
        }

        public async Task<List<Track>> GetTrackByPlaylistId(int id)
        {
            using var cn = Connection;
            cn.Open();
            var tracks = await cn.QueryAsync<Track>(
                "SELECT Track.* FROM Playlist INNER JOIN PlaylistTrack ON Playlist.PlaylistId = PlaylistTrack.PlaylistId INNER JOIN Track ON PlaylistTrack.TrackId = Track.Id WHERE Playlist.PlaylistId = @Id",
                new { id });
            return tracks.ToList();
        }

        public async Task<bool> Update(Playlist playlist)
        {
            if (!await PlaylistExists(playlist.Id))
                return false;

            try
            {
                using var cn = Connection;
                cn.Open();
                return await cn.UpdateAsync(playlist);
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
                return await cn.DeleteAsync(new Playlist { Id = id });
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Playlist>> GetByTrackId(int id)
        {
            //SELECT PL.PlaylistId, PL.Name FROM Playlist AS PL INNER JOIN PlaylistTrack PLT ON PL.PlaylistId = PLT.PlaylistId WHERE PLT.TrackID = 5
            using var cn = Connection;
            cn.Open();
            var playlists = await cn.QueryAsync<Playlist>(
                "SELECT PL.Id, PL.Name FROM Playlist AS PL INNER JOIN PlaylistTrack PLT ON PL.Id = PLT.PlaylistId WHERE PLT.TrackID = @Id",
                new { id });
            return playlists.ToList();
        }
    }
}