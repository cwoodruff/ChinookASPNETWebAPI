using Chinook.Domain.ApiModels;
using FluentValidation;

namespace Chinook.Domain.Validation
{
    public class EmployeeValidator : AbstractValidator<EmployeeApiModel>
    {
        public EmployeeValidator() {
            RuleFor(e => e.FirstName).NotNull();
            RuleFor(e => e.LastName).NotNull();
            RuleFor(e => e.Email).EmailAddress();
            RuleFor(e => e.Phone).Matches(@"\(?\d{3}\)?[-\.]? *\d{3}[-\.]? *[-\.]?\d{4}");
            RuleFor(e => e.Fax).Matches(@"\(?\d{3}\)?[-\.]? *\d{3}[-\.]? *[-\.]?\d{4}");
        }
    }
}