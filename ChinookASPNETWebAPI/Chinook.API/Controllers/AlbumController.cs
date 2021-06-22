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
    public class AlbumController : ControllerBase
    {
        private readonly IChinookSupervisor _chinookSupervisor;

        public AlbumController(IChinookSupervisor chinookSupervisor)
        {
            _chinookSupervisor = chinookSupervisor;
        }
        
        [HttpGet]
        [SwaggerOperation(
            Summary = "Gets all Album",
            Description = "Gets all Album",
            OperationId = "Album.GetAll",
            Tags = new[] { "AlbumEndpoint"})]
        [Produces(typeof(List<AlbumApiModel>))]
        public ActionResult<List<AlbumApiModel>> Get()
        {
            try
            {
                return new ObjectResult(_chinookSupervisor.GetAllAlbum());
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
        [Produces(typeof(AlbumApiModel))]
        public ActionResult<AlbumApiModel> Get(int id)
        {
            try
            {
                var album = _chinookSupervisor.GetAlbumById(id);
                if (album == null)
                {
                    return NotFound();
                }

                return Ok(album);
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
        [Produces(typeof(List<AlbumApiModel>))]
        public ActionResult<List<AlbumApiModel>> GetByArtistId(int id)
        {
            try
            {
                var artist = _chinookSupervisor.GetArtistById(id);

                return Ok(_chinookSupervisor.GetAlbumByArtistId(id));
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
        public ActionResult<AlbumApiModel> Post([FromBody] AlbumApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();

                return StatusCode(201, _chinookSupervisor.AddAlbum(input));
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
        public ActionResult<AlbumApiModel> Put(int id, [FromBody] AlbumApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();
                if (_chinookSupervisor.GetAlbumById(id) == null)
                {
                    return NotFound();
                }

                // var errors = JsonConvert.SerializeObject(ModelState.Values
                //     .SelectMany(state => state.Errors)
                //     .Select(error => error.ErrorMessage));
                // Debug.WriteLine(errors);

                if (_chinookSupervisor.UpdateAlbum(input))
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