using Chinook.Domain.ApiModels;
using FluentValidation;

namespace Chinook.Domain.Validation
{
    public class InvoiceLineValidator : AbstractValidator<InvoiceLineApiModel>
    {
        public InvoiceLineValidator()
        {
            RuleFor(il => il.Quantity).NotNull();
            RuleFor(il => il.UnitPrice).NotNull();
        }
    }
}