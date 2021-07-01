using System.Collections.Generic;
using System.Text.Json.Serialization;
using Chinook.Domain.Converters;
using Chinook.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Chinook.Domain.ApiModels
{
    public class PlaylistApiModel : IConvertModel<PlaylistApiModel, Playlist>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        [ValidateNever]
        [JsonIgnore]
        public IList<TrackApiModel> Tracks { get; set; }
        
        [ValidateNever]
        [JsonIgnore]
        public IList<PlaylistTrackApiModel> PlaylistTracks { get; set; }

        public Playlist Convert() =>
            new Playlist
            {
                Id = Id,
                Name = Name
            };
    }
}