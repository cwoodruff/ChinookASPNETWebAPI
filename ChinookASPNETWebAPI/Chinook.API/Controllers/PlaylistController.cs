using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Chinook.Domain.Supervisor;
using Chinook.Domain.ApiModels;
using Microsoft.AspNetCore.Cors;
using Swashbuckle.AspNetCore.Annotations;

namespace Chinook.API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        private readonly IChinookSupervisor _chinookSupervisor;

        public PlaylistController(IChinookSupervisor chinookSupervisor)
        {
            _chinookSupervisor = chinookSupervisor;
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
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
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
                return StatusCode(500, ex);
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
                    return BadRequest();

                return StatusCode(201, _chinookSupervisor.AddPlaylist(input));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
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
                    return BadRequest();

                if (_chinookSupervisor.UpdatePlaylist(input))
                {
                    return Ok(input);
                }

                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
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
                return StatusCode(500, ex);
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
                return StatusCode(500, ex);
            }
        }
    }
}