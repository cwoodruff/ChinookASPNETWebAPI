using Chinook.Domain.ApiModels;
using FluentValidation;

namespace Chinook.Domain.Validation
{
    public class ArtistValidator : AbstractValidator<ArtistApiModel>
    {
        public ArtistValidator()
        {
            RuleFor(a => a.Name).NotNull();
        }
    }
}