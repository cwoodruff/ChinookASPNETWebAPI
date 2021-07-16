using Chinook.Domain.ApiModels;
using FluentValidation;

namespace Chinook.Domain.Validation
{
    public class AlbumValidator : AbstractValidator<AlbumApiModel>
    {
        public AlbumValidator()
        {
            RuleFor(a => a.Title).NotNull();
        }
    }
}