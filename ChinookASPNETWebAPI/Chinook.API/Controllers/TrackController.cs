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
    public class TrackController : ControllerBase
    {
        private readonly IChinookSupervisor _chinookSupervisor;

        public TrackController(IChinookSupervisor chinookSupervisor)
        {
            _chinookSupervisor = chinookSupervisor;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Gets all Album",
            Description = "Gets all Album",
            OperationId = "Album.GetAll",
            Tags = new[] { "AlbumEndpoint"})]
        [Produces(typeof(List<TrackApiModel>))]
        public ActionResult<List<TrackApiModel>> Get()
        {
            try
            {
                return new ObjectResult(_chinookSupervisor.GetAllTrack());
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
        [Produces(typeof(TrackApiModel))]
        public ActionResult<TrackApiModel> Get(int id)
        {
            try
            {
                var track = _chinookSupervisor.GetTrackById(id);

                return Ok(track);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("album/{id}")]
        [SwaggerOperation(
            Summary = "Gets Albums by Artist",
            Description = "Gets Albums by Artist",
            OperationId = "Album.GetByArtist",
            Tags = new[] { "AlbumEndpoint"})]
        [Produces(typeof(List<TrackApiModel>))]
        public ActionResult<TrackApiModel> GetByAlbumId(int id)
        {
            try
            {
                return Ok(_chinookSupervisor.GetTrackByAlbumId(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("mediatype/{id}")]
        [SwaggerOperation(
            Summary = "Gets Albums by Artist",
            Description = "Gets Albums by Artist",
            OperationId = "Album.GetByArtist",
            Tags = new[] { "AlbumEndpoint"})]
        [Produces(typeof(List<TrackApiModel>))]
        public ActionResult<TrackApiModel> GetByMediaTypeId(int id)
        {
            try
            {
                return Ok(_chinookSupervisor.GetTrackByMediaTypeId(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("genre/{id}")]
        [SwaggerOperation(
            Summary = "Gets Albums by Artist",
            Description = "Gets Albums by Artist",
            OperationId = "Album.GetByArtist",
            Tags = new[] { "AlbumEndpoint"})]
        [Produces(typeof(List<TrackApiModel>))]
        public ActionResult<TrackApiModel> GetByGenreId(int id)
        {
            try
            {
                return Ok(_chinookSupervisor.GetTrackByGenreId(id));
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
        public ActionResult<TrackApiModel> Post([FromBody] TrackApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();

                return StatusCode(201, _chinookSupervisor.AddTrack(input));
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
        public ActionResult<TrackApiModel> Put(int id, [FromBody] TrackApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();

                if (_chinookSupervisor.UpdateTrack(input))
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
                if (_chinookSupervisor.DeleteTrack(id))
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
        
        [HttpGet("artist/{id}")]
        [SwaggerOperation(
            Summary = "Gets Albums by Artist",
            Description = "Gets Albums by Artist",
            OperationId = "Album.GetByArtist",
            Tags = new[] { "AlbumEndpoint"})]
        [Produces(typeof(List<TrackApiModel>))]
        public ActionResult<TrackApiModel> GetByArtistId(int id)
        {
            try
            {
                return Ok(_chinookSupervisor.GetTrackByArtistId(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        
        [HttpGet("invoice/{id}")]
        [SwaggerOperation(
            Summary = "Gets Albums by Artist",
            Description = "Gets Albums by Artist",
            OperationId = "Album.GetByArtist",
            Tags = new[] { "AlbumEndpoint"})]
        [Produces(typeof(List<TrackApiModel>))]
        public ActionResult<TrackApiModel> GetByInvoiceId(int id)
        {
            try
            {
                return Ok(_chinookSupervisor.GetTrackByInvoiceId(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}