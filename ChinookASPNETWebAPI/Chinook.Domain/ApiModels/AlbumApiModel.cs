﻿using System.Collections.Generic;
 using System.Text.Json.Serialization;
 using Chinook.Domain.Converters;
using Chinook.Domain.Entities;
 using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

 namespace Chinook.Domain.ApiModels
{
    public class AlbumApiModel : IConvertModel<AlbumApiModel, Album>
    {
        public int Id { get; set; }
        public string Title { get; set; } 
        public int ArtistId { get; set; }
        
        [ValidateNever]
        [JsonIgnore]
        public string? ArtistName { get; set; }

        [ValidateNever]
        [JsonIgnore]
        public ArtistApiModel? Artist { get; set; }

        [ValidateNever]
        [JsonIgnore]
        public IList<TrackApiModel>? Tracks { get; set; }

        public Album Convert() =>
            new Album
            {
                Id = Id,
                ArtistId = ArtistId,
                Title = Title ?? string.Empty
            };
    }
}