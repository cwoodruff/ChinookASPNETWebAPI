using Chinook.Domain.Converters;
using Chinook.Domain.ApiModels;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Chinook.Domain.Entities
{
    public class MediaType : IConvertModel<MediaType, MediaTypeApiModel>
    {
        public MediaType()
        {
            Tracks = new HashSet<Track>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Track> Tracks { get; set; }
        
        public MediaTypeApiModel Convert() =>
            new MediaTypeApiModel
            {
                Id = Id,
                Name = Name
            };
        
        public async Task<MediaTypeApiModel> ConvertAsync() =>
            new MediaTypeApiModel
            {
                Id = Id,
                Name = Name
            };
    }
}