using System;
using System.Collections.Generic;
using Chinook.Domain.ApiModels;
using System.Linq;
using Chinook.Domain.Extensions;
using Microsoft.Extensions.Caching.Memory;

namespace Chinook.Domain.Supervisor
{
    public partial class ChinookSupervisor
    {
        public IEnumerable<PlaylistApiModel> GetAllPlaylist()
        {
            var playlists = _playlistRepository.GetAll().ConvertAll();
            foreach (var playlist in playlists)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat((object?) "Playlist-", playlist.Id), playlist, cacheEntryOptions);
            }
            return playlists;
        }

        public PlaylistApiModel GetPlaylistById(int id)
        {
            var playlistApiModelCached = _cache.Get<PlaylistApiModel>(string.Concat("Playlist-", id));

            if (playlistApiModelCached != null)
            {
                return playlistApiModelCached;
            }
            else
            {
                var playlistApiModel = (_playlistRepository.GetById(id)).Convert();
                playlistApiModel.Tracks = (GetTrackByPlaylistId(playlistApiModel.Id)).ToList();

                var cacheEntryOptions =
                    new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(604800));
                _cache.Set(string.Concat((object?) "Playlist-", playlistApiModel.Id), playlistApiModel, cacheEntryOptions);

                return playlistApiModel;
            }
        }

        public PlaylistApiModel AddPlaylist(PlaylistApiModel newPlaylistApiModel)
        {
            var playlist = newPlaylistApiModel.Convert();

            playlist = _playlistRepository.Add(playlist);
            newPlaylistApiModel.Id = playlist.Id;
            return newPlaylistApiModel;
        }

        public bool UpdatePlaylist(PlaylistApiModel playlistApiModel)
        {
            var playlist = _playlistRepository.GetById(playlistApiModel.Id);

            if (playlist == null) return false;
            playlist.Id = playlistApiModel.Id;
            playlist.Name = playlistApiModel.Name;

            return _playlistRepository.Update(playlist);
        }

        public bool DeletePlaylist(int id) 
            => _playlistRepository.Delete(id);
        
        public IEnumerable<PlaylistApiModel> GetPlaylistByTrackId(int id)
        {
            var playlists = _playlistRepository.GetByTrackId(id);
            return playlists.ConvertAll();
        }
    }
}