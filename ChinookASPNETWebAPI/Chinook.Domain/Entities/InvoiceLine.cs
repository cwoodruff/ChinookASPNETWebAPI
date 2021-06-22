using System.Text.Json.Serialization;
using Chinook.Domain.Converters;
using Chinook.Domain.ApiModels;

namespace Chinook.Domain.Entities
{
    public class InvoiceLine : IConvertModel<InvoiceLine, InvoiceLineApiModel>
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int TrackId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }

        [JsonIgnore]
        public virtual Invoice Invoice { get; set; }
        [JsonIgnore]
        public virtual Track Track { get; set; }

        public InvoiceLineApiModel Convert() =>
            new InvoiceLineApiModel
            {
                Id = Id,
                InvoiceId = InvoiceId,
                TrackId = TrackId,
                UnitPrice = UnitPrice,
                Quantity = Quantity
            };
    }
}