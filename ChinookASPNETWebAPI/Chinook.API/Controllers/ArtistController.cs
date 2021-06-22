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
    public class ArtistController : ControllerBase
    {
        private readonly IChinookSupervisor _chinookSupervisor;

        public ArtistController(IChinookSupervisor chinookSupervisor)
        {
            _chinookSupervisor = chinookSupervisor;
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
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
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
                return StatusCode(500, ex);
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
                    return BadRequest();

                return StatusCode(201, _chinookSupervisor.AddArtist(input));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
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
                    return BadRequest();

                if (_chinookSupervisor.UpdateArtist(input))
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
                return StatusCode(500, ex);
            }
        }
    }
}