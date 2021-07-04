using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Chinook.Domain.Converters;
using Chinook.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Chinook.Domain.ApiModels
{
    public class PlaylistTrackApiModel : IConvertModel<PlaylistTrackApiModel, PlaylistTrack>
    {
        public int PlaylistId { get; set; }
        public int TrackId { get; set; }
        
        [ValidateNever]
        [JsonIgnore]
        public PlaylistApiModel Playlist { get; set; }
        
        [ValidateNever]
        [JsonIgnore]
        public TrackApiModel Track { get; set; }

        public PlaylistTrack Convert() =>
            new PlaylistTrack
            {
                PlaylistId = PlaylistId,
                TrackId = TrackId
            };
        
        public async Task<PlaylistTrack> ConvertAsync() =>
            new PlaylistTrack
            {
                PlaylistId = PlaylistId,
                TrackId = TrackId
            };
    }
}