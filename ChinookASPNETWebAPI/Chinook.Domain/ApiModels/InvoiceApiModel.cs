﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Chinook.Domain.Converters;
using Chinook.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Chinook.Domain.ApiModels
{
    public class InvoiceApiModel : IConvertModel<InvoiceApiModel, Invoice>
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string BillingAddress { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingCountry { get; set; }
        public string BillingPostalCode { get; set; }
        public decimal Total { get; set; }
        
        [ValidateNever]
        [JsonIgnore]
        public IList<InvoiceLineApiModel> InvoiceLines { get; set; }
        
        [ValidateNever]
        [JsonIgnore]
        public CustomerApiModel Customer { get; set; }

        public Invoice Convert() =>
            new Invoice
            {
                Id = Id,
                CustomerId = CustomerId,
                InvoiceDate = InvoiceDate,
                BillingAddress = BillingAddress,
                BillingCity = BillingCity,
                BillingState = BillingState,
                BillingCountry = BillingCountry,
                BillingPostalCode = BillingPostalCode,
                Total = Total
            };
    }
}