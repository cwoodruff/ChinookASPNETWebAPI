using System;
using System.Collections.Generic;
using System.Linq;
using Chinook.Domain.ApiModels;
using Chinook.Domain.Extensions;
using Microsoft.Extensions.Caching.Memory;

namespace Chinook.Domain.Supervisor
{
    public partial class ChinookSupervisor
    {
        public IEnumerable<InvoiceApiModel> GetAllInvoice()
        {
            var invoices = _invoiceRepository.GetAll().ConvertAll();
            foreach (var invoice in invoices)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat((object?) "Invoice-", invoice.Id), invoice, cacheEntryOptions);
            }
            return invoices;
        }
        
        public InvoiceApiModel GetInvoiceById(int id)
        {
            var invoiceApiModelCached = _cache.Get<InvoiceApiModel>(string.Concat("Invoice-", id));

            if (invoiceApiModelCached != null)
            {
                return invoiceApiModelCached;
            }
            else
            {
                var invoiceApiModel = (_invoiceRepository.GetById(id)).Convert();
                invoiceApiModel.Customer = GetCustomerById(invoiceApiModel.CustomerId);
                invoiceApiModel.InvoiceLines = (GetInvoiceLineByInvoiceId(invoiceApiModel.Id)).ToList();
                invoiceApiModel.CustomerName =
                    $"{invoiceApiModel.Customer.LastName}, {invoiceApiModel.Customer.FirstName}";

                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat((object?) "Invoice-", invoiceApiModel.Id), invoiceApiModel, cacheEntryOptions);

                return invoiceApiModel;
            }
        }

        public IEnumerable<InvoiceApiModel> GetInvoiceByCustomerId(int id)
        {
            var invoices = _invoiceRepository.GetByCustomerId(id);
            return invoices.ConvertAll();
        }

        public InvoiceApiModel AddInvoice(InvoiceApiModel newInvoiceApiModel)
        {
            var invoice = newInvoiceApiModel.Convert();

            invoice = _invoiceRepository.Add(invoice);
            newInvoiceApiModel.Id = invoice.Id;
            return newInvoiceApiModel;
        }

        public bool UpdateInvoice(InvoiceApiModel invoiceApiModel)
        {
            var invoice = _invoiceRepository.GetById(invoiceApiModel.Id);

            if (invoice == null) return false;
            invoice.Id = invoiceApiModel.Id;
            invoice.CustomerId = invoiceApiModel.CustomerId;
            invoice.InvoiceDate = invoiceApiModel.InvoiceDate;
            invoice.BillingAddress = invoiceApiModel.BillingAddress;
            invoice.BillingCity = invoiceApiModel.BillingCity;
            invoice.BillingState = invoiceApiModel.BillingState;
            invoice.BillingCountry = invoiceApiModel.BillingCountry;
            invoice.BillingPostalCode = invoiceApiModel.BillingPostalCode;
            invoice.Total = invoiceApiModel.Total;

            return _invoiceRepository.Update(invoice);
        }

        public bool DeleteInvoice(int id) 
            => _invoiceRepository.Delete(id);
        
        
        public IEnumerable<InvoiceApiModel> GetInvoiceByEmployeeId(int id)
        {
            var invoices = _invoiceRepository.GetByEmployeeId(id);
            return invoices.ConvertAll();
        }
    }
}