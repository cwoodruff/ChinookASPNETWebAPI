using Chinook.Domain.ApiModels;
using FluentValidation;

namespace Chinook.Domain.Validation
{
    public class PlaylistTrackValidator : AbstractValidator<PlaylistTrackApiModel>
    {
        public PlaylistTrackValidator()
        {
            
        }
    }
}