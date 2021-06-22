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
            Summary = "Gets all Album",
            Description = "Gets all Album",
            OperationId = "Album.GetAll",
            Tags = new[] { "AlbumEndpoint"})]
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
            Summary = "Gets a specific Album",
            Description = "Gets a specific Album",
            OperationId = "Album.GetOne",
            Tags = new[] { "AlbumEndpoint"})]
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
            Summary = "Creates a new Album",
            Description = "Creates a new Album",
            OperationId = "Album.Create",
            Tags = new[] { "AlbumEndpoint"})]
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
            Summary = "Update an Album",
            Description = "Update an Album",
            OperationId = "Album.Update",
            Tags = new[] { "AlbumEndpoint"})]
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
            Summary = "Delete a Album",
            Description = "Delete a Album",
            OperationId = "Album.Delete",
            Tags = new[] { "AlbumEndpoint"})]
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
            Summary = "Gets Albums by Artist",
            Description = "Gets Albums by Artist",
            OperationId = "Album.GetByArtist",
            Tags = new[] { "AlbumEndpoint"})]
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