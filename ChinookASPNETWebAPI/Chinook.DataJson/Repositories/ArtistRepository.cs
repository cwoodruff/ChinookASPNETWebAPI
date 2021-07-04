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
    public class ArtistRepository : IArtistRepository
    {
        private readonly SqlConnection _sqlconn;

        public ArtistRepository(SqlConnection sqlconn)
        {
            _sqlconn = sqlconn;
        }
        
        public void Dispose()
        {
            
        }

        private async Task<bool> ArtistExists(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_CheckArtist", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcomm.Parameters.Add(new SqlParameter("ArtistId", id));
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);

            return Convert.ToBoolean(dset.Tables[0].Rows[0][0]);
        }

        public async Task<List<Artist>> GetAll()
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetArtist", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            var converted =
                JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(), typeof(List<Artist>)) as List<Artist>;
            return converted;
        }

        public async Task<Artist> GetById(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetArtistDetails", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcomm.Parameters.Add(new SqlParameter("ArtistId", id));
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            var converted =
                JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(), typeof(List<Artist>)) as List<Artist>;

            return converted.FirstOrDefault();
        }

        public async Task<Artist> Add(Artist newArtist)
        {

            return newArtist;
        }

        public async Task<bool> Update(Artist artist)
        {
            if (!await ArtistExists(artist.Id))
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
    }
}