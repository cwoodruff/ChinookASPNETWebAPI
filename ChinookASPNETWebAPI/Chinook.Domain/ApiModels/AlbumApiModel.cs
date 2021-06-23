﻿using System.Collections.Generic;
using Chinook.Domain.Converters;
using Chinook.Domain.Entities;

namespace Chinook.Domain.ApiModels
{
    public class AlbumApiModel : IConvertModel<AlbumApiModel, Album>
    {
        public int Id { get; set; }
        public string Title { get; set; } 
        public int ArtistId { get; set; }
        public string? ArtistName { get; set; }
        public ArtistApiModel? Artist { get; set; }
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