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

        private async Task<bool> MediaTypeExists(int id) =>
            await Connection.ExecuteScalarAsync<bool>("select count(1) from MediaType where Id = @id", new {id});

        public async Task<List<MediaType>> GetAll()
        {
            using IDbConnection cn = Connection;
            cn.Open();
            var mediaTypes = await cn.QueryAsync<MediaType>("Select * From MediaType");
            return mediaTypes.ToList();
        }

        public async Task<MediaType> GetById(int id)
        {
            using var cn = Connection;
            cn.Open();
            return cn.QueryFirstOrDefault<MediaType>("Select * From MediaType WHERE Id = @Id", new {id});
        }

        public async Task<MediaType> Add(MediaType newMediaType)
        {
            using var cn = Connection;
            cn.Open();

            newMediaType.Id = await cn.InsertAsync(new MediaType {Name = newMediaType.Name});

            return newMediaType;
        }

        public async Task<bool> Update(MediaType mediaType)
        {
            if (!await MediaTypeExists(mediaType.Id))
                return false;

            try
            {
                using var cn = Connection;
                cn.Open();
                return await cn.UpdateAsync(mediaType);
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
                return await cn.DeleteAsync(new MediaType {Id = id});
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}