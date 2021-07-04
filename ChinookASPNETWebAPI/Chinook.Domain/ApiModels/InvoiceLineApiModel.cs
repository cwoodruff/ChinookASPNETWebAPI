using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Chinook.Domain.Converters;
using Chinook.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Chinook.Domain.ApiModels
{
    public class InvoiceLineApiModel : IConvertModel<InvoiceLineApiModel, InvoiceLine>
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int TrackId { get; set; }
        public string TrackName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        
        [ValidateNever]
        [JsonIgnore]
        public InvoiceApiModel Invoice { get; set; }
        
        [ValidateNever]
        [JsonIgnore]
        public TrackApiModel Track { get; set; }

        public InvoiceLine Convert() =>
            new InvoiceLine
            {
                Id = Id,
                InvoiceId = InvoiceId,
                TrackId = TrackId,
                UnitPrice = UnitPrice,
                Quantity = Quantity
            };
        
        public async Task<InvoiceLine> ConvertAsync() =>
            new InvoiceLine
            {
                Id = Id,
                InvoiceId = InvoiceId,
                TrackId = TrackId,
                UnitPrice = UnitPrice,
                Quantity = Quantity
            };
    }
}