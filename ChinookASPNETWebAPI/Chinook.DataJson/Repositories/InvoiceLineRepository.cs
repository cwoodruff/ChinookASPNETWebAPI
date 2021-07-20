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
    public class InvoiceLineRepository : IInvoiceLineRepository
    {
        private readonly SqlConnection _sqlconn;

        public InvoiceLineRepository(SqlConnection sqlconn)
        {
            _sqlconn = sqlconn;
        }

        public void Dispose()
        {
        }

        private async Task<bool> InvoiceLineExists(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_CheckInvoiceLine", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcomm.Parameters.Add(new SqlParameter("InvoiceLineId", id));
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);

            return Convert.ToBoolean(dset.Tables[0].Rows[0][0]);
        }

        public async Task<List<InvoiceLine>> GetAll()
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetInvoice", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            var converted =
                JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(), typeof(List<InvoiceLine>)) as
                    List<InvoiceLine>;
            return converted;
        }

        public async Task<InvoiceLine> GetById(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetInvoiceLineDetails", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcomm.Parameters.Add(new SqlParameter("InvoiceLineId", id));
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            var converted =
                JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(), typeof(List<InvoiceLine>)) as
                    List<InvoiceLine>;

            return converted.FirstOrDefault();
        }

        public async Task<List<InvoiceLine>> GetByInvoiceId(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetInvoiceLineByInvoice", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcomm.Parameters.Add(new SqlParameter("InvoiceId", id));
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            var converted =
                JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(), typeof(List<InvoiceLine>)) as
                    List<InvoiceLine>;
            return converted;
        }

        public async Task<List<InvoiceLine>> GetByTrackId(int id)
        {
            var sqlcomm = new SqlCommand("dbo.sproc_GetInvoiceLineByTrack", _sqlconn)
            {
                CommandType = CommandType.StoredProcedure
            };
            sqlcomm.Parameters.Add(new SqlParameter("TrackId", id));
            var dset = new DataSet();
            var adap = new SqlDataAdapter(sqlcomm);
            adap.Fill(dset);
            try
            {
                var converted =
                    JsonSerializer.Deserialize(dset.Tables[0].Rows[0][0].ToString(), typeof(List<InvoiceLine>)) as
                        List<InvoiceLine>;
                return converted;
            }
            catch (JsonException)
            {
                return null;
            }
        }

        public async Task<InvoiceLine> Add(InvoiceLine newInvoiceLine)
        {
            return newInvoiceLine;
        }

        public async Task<bool> Update(InvoiceLine invoiceLine)
        {
            if (!await InvoiceLineExists(invoiceLine.Id))
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