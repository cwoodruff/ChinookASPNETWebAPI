using Chinook.Domain.Entities;
using FluentValidation;

namespace Chinook.Domain.Validation
{
    public class MediaTypeValidator : AbstractValidator<MediaType>
    {
        public MediaTypeValidator()
        {
            RuleFor(m => m.Name).NotNull();
        }
    }
}