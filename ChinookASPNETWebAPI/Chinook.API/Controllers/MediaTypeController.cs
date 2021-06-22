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
    [ResponseCache(Duration = 604800)] // cache for a week
    [EnableCors("CorsPolicy")]
    [ApiController]
    public class MediaTypeController : ControllerBase
    {
        private readonly IChinookSupervisor _chinookSupervisor;

        public MediaTypeController(IChinookSupervisor chinookSupervisor)
        {
            _chinookSupervisor = chinookSupervisor;
        }

        [HttpGet]
        
        [SwaggerOperation(
            Summary = "Gets all Album",
            Description = "Gets all Album",
            OperationId = "Album.GetAll",
            Tags = new[] { "AlbumEndpoint"})]
        [Produces(typeof(List<MediaTypeApiModel>))]
        [ResponseCache(Duration = 604800)] // cache for a week
        public ActionResult<List<MediaTypeApiModel>> Get()
        {
            try
            {
                return new ObjectResult(_chinookSupervisor.GetAllMediaType());
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
        [Produces(typeof(MediaTypeApiModel))]
        public ActionResult<MediaTypeApiModel> Get(int id)
        {
            try
            {
                var mediaType = _chinookSupervisor.GetMediaTypeById(id);

                return Ok(mediaType);
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
        public ActionResult<MediaTypeApiModel> Post([FromBody] MediaTypeApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();

                return StatusCode(201, _chinookSupervisor.AddMediaType(input));
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
        public ActionResult<MediaTypeApiModel> Put(int id, [FromBody] MediaTypeApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();

                if (_chinookSupervisor.UpdateMediaType(input))
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
                if (_chinookSupervisor.DeleteMediaType(id))
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