using Chinook.Domain.Entities;
using FluentValidation;

namespace Chinook.Domain.Validation
{
    public class InvoiceValidator : AbstractValidator<Invoice>
    {
        public InvoiceValidator() {
            RuleFor(i => i.CustomerId).NotNull();
            RuleFor(i => i.InvoiceDate).NotNull();
            RuleFor(i => i.Total).GreaterThan(0);
            RuleFor(i => i.BillingAddress).NotNull();
            RuleFor(i => i.BillingCity).NotNull();
            RuleFor(i => i.BillingCountry).NotNull();
            RuleFor(i => i.BillingState).NotNull();
            RuleFor(i => i.BillingPostalCode).NotNull();
        }
    }
}