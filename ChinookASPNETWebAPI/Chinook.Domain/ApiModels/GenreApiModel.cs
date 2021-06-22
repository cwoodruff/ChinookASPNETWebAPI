using System.Collections.Generic;
using Chinook.Domain.Converters;
using Chinook.Domain.Entities;

namespace Chinook.Domain.ApiModels
{
    public class GenreApiModel : IConvertModel<GenreApiModel, Genre>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<TrackApiModel> Tracks { get; set; }
        
        public Genre Convert() =>
            new Genre
            {
                Id = Id,
                Name = Name
            };
    }
}