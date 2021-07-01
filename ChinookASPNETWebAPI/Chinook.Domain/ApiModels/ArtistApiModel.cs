using System.Collections.Generic;
using System.Text.Json.Serialization;
using Chinook.Domain.Converters;
using Chinook.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Chinook.Domain.ApiModels
{
    public class ArtistApiModel : IConvertModel<ArtistApiModel, Artist>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        
        [ValidateNever]
        [JsonIgnore]
        public IList<AlbumApiModel>? Albums { get; set; }

        public Artist Convert() =>
            new Artist
            {
                Id = Id,
                Name = Name ?? string.Empty
            };
    }
}