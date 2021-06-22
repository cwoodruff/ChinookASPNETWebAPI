using System;
using System.Collections.Generic;
using Chinook.Domain.ApiModels;
using Chinook.Domain.Extensions;
using Microsoft.Extensions.Caching.Memory;

namespace Chinook.Domain.Supervisor
{
    public partial class ChinookSupervisor
    {
        public IEnumerable<InvoiceLineApiModel> GetAllInvoiceLine()
        {
            var invoiceLines = _invoiceLineRepository.GetAll().ConvertAll();
            foreach (var invoiceLine in invoiceLines)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat((object?) "InvoiceLine-", invoiceLine.Id), invoiceLine, cacheEntryOptions);
            }
            return invoiceLines;
        }

        public InvoiceLineApiModel GetInvoiceLineById(int id)
        {
            var invoiceLineApiModelCached = _cache.Get<InvoiceLineApiModel>(string.Concat("InvoiceLine-", id));

            if (invoiceLineApiModelCached != null)
            {
                return invoiceLineApiModelCached;
            }
            else
            {
                var invoiceLineApiModel = (_invoiceLineRepository.GetById(id)).Convert();
                invoiceLineApiModel.Track = GetTrackById(invoiceLineApiModel.TrackId);
                invoiceLineApiModel.Invoice = GetInvoiceById(invoiceLineApiModel.InvoiceId);
                invoiceLineApiModel.TrackName = invoiceLineApiModel.Track.Name;

                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat((object?) "InvoiceLine-", invoiceLineApiModel.Id), invoiceLineApiModel, cacheEntryOptions);

                return invoiceLineApiModel;
            }
        }

        public IEnumerable<InvoiceLineApiModel> GetInvoiceLineByInvoiceId(int id)
        {
            var invoiceLines = _invoiceLineRepository.GetByInvoiceId(id);
            return invoiceLines.ConvertAll();
        }

        public IEnumerable<InvoiceLineApiModel> GetInvoiceLineByTrackId(int id)
        {
            var invoiceLines = _invoiceLineRepository.GetByTrackId(id);
            return invoiceLines.ConvertAll();
        }

        public InvoiceLineApiModel AddInvoiceLine(InvoiceLineApiModel newInvoiceLineApiModel)
        {
            var invoiceLine = newInvoiceLineApiModel.Convert();

            invoiceLine = _invoiceLineRepository.Add(invoiceLine);
            newInvoiceLineApiModel.Id = invoiceLine.Id;
            return newInvoiceLineApiModel;
        }

        public bool UpdateInvoiceLine(InvoiceLineApiModel invoiceLineApiModel)
        {
            var invoiceLine = _invoiceLineRepository.GetById(invoiceLineApiModel.InvoiceId);

            if (invoiceLine == null) return false;
            invoiceLine.Id = invoiceLineApiModel.Id;
            invoiceLine.InvoiceId = invoiceLineApiModel.InvoiceId;
            invoiceLine.TrackId = invoiceLineApiModel.TrackId;
            invoiceLine.UnitPrice = invoiceLineApiModel.UnitPrice;
            invoiceLine.Quantity = invoiceLineApiModel.Quantity;

            return _invoiceLineRepository.Update(invoiceLine);
        }

        public bool DeleteInvoiceLine(int id) 
            => _invoiceLineRepository.Delete(id);
    }
}