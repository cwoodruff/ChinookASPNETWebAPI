using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Chinook.Domain.Entities;
using Chinook.Domain.Repositories;
using Microsoft.Data.SqlClient;

namespace Chinook.DataJson.Repositories
{
    public class TrackRepository : ITrackRepository
    {
        private readonly SqlConnection _sqlconn;

        public TrackRepository(SqlConnection sqlconn)
        {
            _sqlconn = sqlconn;
        }

        public void Dispose()
        {
        }

        private async Task<bool> TrackExists(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_CheckTrack", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcomm.Parameters.Add(new SqlParameter("TrackId", id));
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);

            return Convert.ToBoolean(dset.Tables[0].Rows[0][0]);
        }

        public async Task<List<Track>> GetAll()
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetTrack", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            var converted =
                JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(), typeof(List<Track>)) as List<Track>;
            return converted;
        }

        public async Task<Track> GetById(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetTrackDetails", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcomm.Parameters.Add(new SqlParameter("TrackId", id));
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            var converted =
                JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(), typeof(List<Track>)) as List<Track>;

            return converted.FirstOrDefault();
        }

        public async Task<Track> Add(Track newTrack)
        {
            return newTrack;
        }

        public async Task<bool> Update(Track track)
        {
            if (!await TrackExists(track.Id))
                return false;

            try
            {
                return true;
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
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Track>> GetByInvoiceId(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetTrackByInvoice", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcomm.Parameters.Add(new SqlParameter("InvoiceId", id));
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            var converted =
                JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(), typeof(List<Track>)) as List<Track>;
            return converted;
        }

        public async Task<List<Track>> GetByAlbumId(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetTrackByAlbum", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcomm.Parameters.Add(new SqlParameter("AlbumId", id));
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            var converted =
                JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(), typeof(List<Track>)) as List<Track>;
            return converted;
        }

        public async Task<List<Track>> GetByGenreId(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetTrackByGenre", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcomm.Parameters.Add(new SqlParameter("GenreId", id));
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            var converted =
                JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(), typeof(List<Track>)) as List<Track>;
            return converted;
        }

        public async Task<List<Track>> GetByMediaTypeId(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetTrackByMediaType", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcomm.Parameters.Add(new SqlParameter("MediaTypeId", id));
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            var converted =
                JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(), typeof(List<Track>)) as List<Track>;
            return converted;
        }

        public async Task<List<Track>> GetByPlaylistId(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetTrackByPlaylist", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcomm.Parameters.Add(new SqlParameter("PlaylistId", id));
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            var converted =
                JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(), typeof(List<Track>)) as List<Track>;
            return converted;
        }

        public async Task<List<Track>> GetByArtistId(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetTrackByArtist", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcomm.Parameters.Add(new SqlParameter("ArtistId", id));
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            var converted =
                JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(), typeof(List<Track>)) as List<Track>;
            return converted;
        }
    }
}