using System;
using System.Collections.Generic;
using System.Linq;
using Chinook.Domain.ApiModels;
using Chinook.Domain.Extensions;
using Microsoft.Extensions.Caching.Memory;

namespace Chinook.Domain.Supervisor
{
    public partial class ChinookSupervisor
    {
        public IEnumerable<ArtistApiModel> GetAllArtist()
        {
            var artists = _artistRepository.GetAll().ConvertAll();
            foreach (var artist in artists)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat((object?) "Artist-", artist.Id), artist, cacheEntryOptions);
            }
            return artists;
        }

        public ArtistApiModel GetArtistById(int id)
        {
            var artistApiModelCached = _cache.Get<ArtistApiModel>(string.Concat("Artist-", id));

            if (artistApiModelCached != null)
            {
                return artistApiModelCached;
            }
            else
            {
                var artistApiModel = (_artistRepository.GetById(id)).Convert();
                artistApiModel.Albums = (GetAlbumByArtistId(artistApiModel.Id)).ToList();

                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat((object?) "Artist-", artistApiModel.Id), artistApiModel, cacheEntryOptions);

                return artistApiModel;
            }
        }

        public ArtistApiModel AddArtist(ArtistApiModel newArtistApiModel)
        {
            var artist = newArtistApiModel.Convert();

            artist = _artistRepository.Add(artist);
            newArtistApiModel.Id = artist.Id;
            return newArtistApiModel;
        }

        public bool UpdateArtist(ArtistApiModel artistApiModel)
        {
            var artist = _artistRepository.GetById(artistApiModel.Id);

            if (artist == null) return false;
            artist.Id = artistApiModel.Id;
            artist.Name = artistApiModel.Name;

            return _artistRepository.Update(artist);
        }

        public bool DeleteArtist(int id) 
            => _artistRepository.Delete(id);
    }
}