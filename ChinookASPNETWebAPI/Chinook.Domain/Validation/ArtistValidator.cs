using Chinook.Domain.Entities;
using FluentValidation;

namespace Chinook.Domain.Validation
{
    public class ArtistValidator : AbstractValidator<Artist>
    {
        public ArtistValidator()
        {
            RuleFor(a => a.Name).NotNull();
        }
    }
}