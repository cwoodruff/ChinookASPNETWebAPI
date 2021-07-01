using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Chinook.Domain.Supervisor;
using Chinook.Domain.ApiModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace Chinook.API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        private readonly IChinookSupervisor _chinookSupervisor;
        private readonly ILogger<PlaylistController> _logger;

        public PlaylistController(IChinookSupervisor chinookSupervisor, ILogger<PlaylistController> logger)
        {
            _chinookSupervisor = chinookSupervisor;
            _logger = logger;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Gets all Playlist",
            Description = "Gets all Playlist",
            OperationId = "Playlist.GetAll",
            Tags = new[] { "PlaylistEndpoint"})]
        [Produces(typeof(List<PlaylistApiModel>))]
        public ActionResult<List<PlaylistApiModel>> Get()
        {
            try
            {
                return new ObjectResult(_chinookSupervisor.GetAllPlaylist());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "GetPlaylistById")]
        [SwaggerOperation(
            Summary = "Gets a specific Playlist",
            Description = "Gets a specific Playlist",
            OperationId = "Playlist.GetOne",
            Tags = new[] { "PlaylistEndpoint"})]
        [Produces(typeof(PlaylistApiModel))]
        public ActionResult<PlaylistApiModel> Get(int id)
        {
            try
            {
                var playList = _chinookSupervisor.GetPlaylistById(id);

                return Ok(playList);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Creates a new Playlist",
            Description = "Creates a new Playlist",
            OperationId = "Playlist.Create",
            Tags = new[] { "PlaylistEndpoint"})]
        public ActionResult<PlaylistApiModel> Post([FromBody] PlaylistApiModel input)
        {
            try
            {
                if (input == null)
                {
                    return BadRequest("Playlist is null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Playlist object");
                }
                
                var playlist = _chinookSupervisor.AddPlaylist(input);
        
                return CreatedAtRoute("GetPlaylistById", new { id = playlist.Id }, playlist);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Update an Playlist",
            Description = "Update an Playlist",
            OperationId = "Playlist.Update",
            Tags = new[] { "PlaylistEndpoint"})]
        public ActionResult<PlaylistApiModel> Put(int id, [FromBody] PlaylistApiModel input)
        {
            try
            {
                if (input == null)
                {
                    return BadRequest("Playlist is null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Playlist object");
                }

                if (_chinookSupervisor.UpdatePlaylist(input))
                {
                    return CreatedAtRoute("GetPlaylistById", new { id = input.Id }, input);
                }

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Delete a Playlist",
            Description = "Delete a Playlist",
            OperationId = "Playlist.Delete",
            Tags = new[] { "PlaylistEndpoint"})]
        public ActionResult Delete(int id)
        {
            try
            {
                if (_chinookSupervisor.DeletePlaylist(id))
                {
                    return Ok();
                }

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
        
        [HttpGet("track/{id}")]
        [SwaggerOperation(
            Summary = "Gets Playlist by Track",
            Description = "Gets Playlist by Track",
            OperationId = "Playlist.GetByTrackId",
            Tags = new[] { "PlaylistEndpoint"})]
        [Produces(typeof(List<TrackApiModel>))]
        public ActionResult<TrackApiModel> GetByTrackId(int id)
        {
            try
            {
                return Ok(_chinookSupervisor.GetPlaylistByTrackId(id));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}