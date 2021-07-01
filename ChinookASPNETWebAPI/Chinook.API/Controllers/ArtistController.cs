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
    public class ArtistController : ControllerBase
    {
        private readonly IChinookSupervisor _chinookSupervisor;
        private readonly ILogger<ArtistController> _logger;

        public ArtistController(IChinookSupervisor chinookSupervisor, ILogger<ArtistController> logger)
        {
            _chinookSupervisor = chinookSupervisor;
            _logger = logger;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Get all Artists",
            Description = "Get all Artists",
            OperationId = "Artist.GetAll",
            Tags = new[] { "ArtistEndpoint"})]
        [Produces(typeof(List<ArtistApiModel>))]
        public ActionResult<List<ArtistApiModel>> Get()
        {
            try
            {
                return new ObjectResult(_chinookSupervisor.GetAllArtist());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "GetArtistById")]
        [SwaggerOperation(
            Summary = "Get specific Artist",
            Description = "Get specific Artist",
            OperationId = "Artist.GetOne",
            Tags = new[] { "ArtistEndpoint"})]
        [Produces(typeof(ArtistApiModel))]
        public ActionResult<ArtistApiModel> Get(int id)
        {
            try
            {
                var artist = _chinookSupervisor.GetArtistById(id);

                return Ok(artist);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Creates a new Artist",
            Description = "Creates a new Artist",
            OperationId = "Artist.Create",
            Tags = new[] { "ArtistEndpoint"})]
        public ActionResult<ArtistApiModel> Post([FromBody] ArtistApiModel input)
        {
            try
            {
                if (input == null)
                {
                    return BadRequest("Artist is null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Artist object");
                }
                
                var artist = _chinookSupervisor.AddArtist(input);
        
                return CreatedAtRoute("GetArtistById", new { id = artist.Id }, artist);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Update a Artist",
            Description = "Update a Artist",
            OperationId = "Artist.Update",
            Tags = new[] { "ArtistEndpoint"})]
        public ActionResult<ArtistApiModel> Put(int id, [FromBody] ArtistApiModel input)
        {
            try
            {
                if (input == null)
                {
                    return BadRequest("Artist is null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Artist object");
                }

                if (_chinookSupervisor.UpdateArtist(input))
                {
                    return CreatedAtRoute("GetArtistById", new { id = input.Id }, input);
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
            Summary = "Delete an Artist",
            Description = "Delete an Artist",
            OperationId = "Artist.Create",
            Tags = new[] { "ArtistEndpoint"})]
        public ActionResult Delete(int id)
        {
            try
            {
                if (_chinookSupervisor.GetAlbumById(id) == null)
                {
                    return NotFound();
                }

                if (_chinookSupervisor.DeleteAlbum(id))
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
    }
}