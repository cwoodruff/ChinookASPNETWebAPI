using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Chinook.Domain.Converters;
using Chinook.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Chinook.Domain.ApiModels
{
    public class MediaTypeApiModel : IConvertModel<MediaTypeApiModel, MediaType>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        [ValidateNever]
        [JsonIgnore]
        public IList<TrackApiModel> Tracks { get; set; }

        public MediaType Convert() =>
            new MediaType
            {
                Id = Id,
                Name = Name
            };
        
        public async Task<MediaType> ConvertAsync() =>
            new MediaType
            {
                Id = Id,
                Name = Name
            };
    }
}