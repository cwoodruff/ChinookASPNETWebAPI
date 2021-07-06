﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Chinook.Domain.Converters;
using Chinook.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Chinook.Domain.ApiModels
{
    public sealed class TrackApiModel : IConvertModel<TrackApiModel, Track>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? AlbumId { get; set; }
        public string? AlbumName { get; set; }
        public int MediaTypeId { get; set; }
        public string? MediaTypeName { get; set; }
        public int? GenreId { get; set; }
        public string? GenreName { get; set; }
        public string? Composer { get; set; }
        public int Milliseconds { get; set; }
        public int? Bytes { get; set; }
        public decimal UnitPrice { get; set; }
        
        [ValidateNever]
        [JsonIgnore]
        public IList<InvoiceLineApiModel>? InvoiceLines { get; set; }
        
        [ValidateNever]
        [JsonIgnore]
        public IList<PlaylistTrackApiModel>? PlaylistTracks { get; set; }
        
        [ValidateNever]
        [JsonIgnore]
        public AlbumApiModel? Album { get; set; }
        
        [ValidateNever]
        [JsonIgnore]
        public GenreApiModel? Genre { get; set; }
        
        [ValidateNever]
        [JsonIgnore]
        public MediaTypeApiModel? MediaType { get; set; }

        public Track Convert() =>
            new Track
            {
                Id = Id,
                Name = Name,
                AlbumId = AlbumId,
                MediaTypeId = MediaTypeId,
                GenreId = GenreId,
                Composer = Composer,
                Milliseconds = Milliseconds,
                Bytes = Bytes,
                UnitPrice = UnitPrice
            };
        
        public async Task<Track> ConvertAsync() =>
            new Track
            {
                Id = Id,
                Name = Name,
                AlbumId = AlbumId,
                MediaTypeId = MediaTypeId,
                GenreId = GenreId,
                Composer = Composer,
                Milliseconds = Milliseconds,
                Bytes = Bytes,
                UnitPrice = UnitPrice
            };
    }
}