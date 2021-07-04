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
    public class PlaylistTrackRepository : IPlaylistTrackRepository
    {
        private readonly DbInfo _dbInfo;

        public PlaylistTrackRepository(DbInfo dbInfo)
        {
            _dbInfo = dbInfo;
        }

        private IDbConnection Connection => new SqlConnection(_dbInfo.ConnectionStrings);
        
        public void Dispose()
        {
            
        }

        public async Task<List<PlaylistTrack>> GetAll()
        {
            using IDbConnection cn = Connection;
            cn.Open();
            var playListTracks = await cn.QueryAsync<PlaylistTrack>("Select * From PlaylistTrack");
            return playListTracks.ToList();
        }

        public async Task<List<PlaylistTrack>> GetByPlaylistId(int id)
        {
            using var cn = Connection;
            cn.Open();
            var playListTracks = await cn.QueryAsync<PlaylistTrack>("Select * From PlaylistTrack WHERE PlaylistId = @Id", new { id });
            return playListTracks.ToList();
        }

        public async Task<List<PlaylistTrack>> GetByTrackId(int id)
        {
            using var cn = Connection;
            cn.Open();
            var playListTracks = await cn.QueryAsync<PlaylistTrack>("Select * From PlaylistTrack WHERE TrackId = @Id", new { id });
            return playListTracks.ToList();
        }

        public async Task<PlaylistTrack> Add(PlaylistTrack newPlaylistTrack)
        {
            using var cn = Connection;
            cn.Open();

            cn.Insert(new PlaylistTrack {PlaylistId = newPlaylistTrack.PlaylistId, TrackId = newPlaylistTrack.TrackId});

            return newPlaylistTrack;
        }

        public async Task<bool> Update(PlaylistTrack playlistTrack)
        {
            try
            {
                using var cn = Connection;
                cn.Open();
                return await cn.UpdateAsync(playlistTrack);
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
                return await cn.DeleteAsync(new PlaylistTrack {PlaylistId = id});
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}