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

        private bool ArtistExists(int id) =>
            Connection.ExecuteScalar<bool>("select count(1) from Artist where Id = @id", new {id});

        public List<Artist> GetAll()
        {
            using IDbConnection cn = Connection;
            cn.Open();
            var artists = Connection.Query<Artist>("Select * From Artist");
            return artists.ToList();
        }

        public Artist GetById(int id)
        {
            using var cn = Connection;
            cn.Open();
            return cn.QueryFirstOrDefault<Artist>("Select * From Artist WHERE Id = @Id", new {id});
        }

        public Artist Add(Artist newArtist)
        {
            using var cn = Connection;
            cn.Open();

            newArtist.Id = (int) cn.Insert(new Artist {Name = newArtist.Name});

            return newArtist;
        }

        public bool Update(Artist artist)
        {
            if (!ArtistExists(artist.Id))
                return false;

            try
            {
                using var cn = Connection;
                cn.Open();
                return cn.Update(artist);
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
                return cn.Delete(new Artist {Id = id});
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}