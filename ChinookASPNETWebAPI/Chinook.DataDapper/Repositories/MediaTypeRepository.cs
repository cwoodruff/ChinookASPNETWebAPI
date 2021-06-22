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
    public class MediaTypeRepository : IMediaTypeRepository
    {
        private readonly DbInfo _dbInfo;

        public MediaTypeRepository(DbInfo dbInfo)
        {
            _dbInfo = dbInfo;
        }

        private IDbConnection Connection => new SqlConnection(_dbInfo.ConnectionStrings);

        public void Dispose()
        {
            
        }

        private bool MediaTypeExists(int id) =>
            Connection.ExecuteScalar<bool>("select count(1) from MediaType where Id = @id", new {id});

        public List<MediaType> GetAll()
        {
            using IDbConnection cn = Connection;
            cn.Open();
            var mediaTypes = Connection.Query<MediaType>("Select * From MediaType");
            return mediaTypes.ToList();
        }

        public MediaType GetById(int id)
        {
            using var cn = Connection;
            cn.Open();
            return cn.QueryFirstOrDefault<MediaType>("Select * From MediaType WHERE Id = @Id", new {id});
        }

        public MediaType Add(MediaType newMediaType)
        {
            using var cn = Connection;
            cn.Open();

            newMediaType.Id = (int) cn.Insert(new MediaType {Name = newMediaType.Name});

            return newMediaType;
        }

        public bool Update(MediaType mediaType)
        {
            if (!MediaTypeExists(mediaType.Id))
                return false;

            try
            {
                using var cn = Connection;
                cn.Open();
                return cn.Update(mediaType);
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
                return cn.Delete(new MediaType {Id = id});
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}