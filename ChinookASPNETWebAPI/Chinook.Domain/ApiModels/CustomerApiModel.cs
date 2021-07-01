using System.Collections.Generic;
using System.Text.Json.Serialization;
using Chinook.Domain.Converters;
using Chinook.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Chinook.Domain.ApiModels
{
    public class CustomerApiModel : IConvertModel<CustomerApiModel, Customer>
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Company { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? PostalCode { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }
        public int? SupportRepId { get; set; }
        
        [ValidateNever]
        [JsonIgnore]
        public string? SupportRepName { get; set; }
        
        [ValidateNever]
        [JsonIgnore]
        public IList<InvoiceApiModel>? Invoices { get; set; }
        
        [ValidateNever]
        [JsonIgnore]
        public EmployeeApiModel? SupportRep { get; set; }


        public Customer Convert() =>
            new Customer
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                Company = Company,
                Address = Address,
                City = City,
                State = State,
                Country = Country,
                PostalCode = PostalCode,
                Phone = Phone,
                Fax = Fax,
                Email = Email,
                SupportRepId = SupportRepId
            };
    }
}