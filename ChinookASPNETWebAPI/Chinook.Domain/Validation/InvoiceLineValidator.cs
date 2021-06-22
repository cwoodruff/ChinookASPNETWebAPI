using Chinook.Domain.Entities;
using FluentValidation;

namespace Chinook.Domain.Validation
{
    public class InvoiceLineValidator : AbstractValidator<InvoiceLine>
    {
        public InvoiceLineValidator()
        {
            RuleFor(il => il.Quantity).NotNull();
            RuleFor(il => il.UnitPrice).NotNull();
        }
    }
}