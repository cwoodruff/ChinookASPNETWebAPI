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
    [ResponseCache(Duration = 604800)]
    public class GenreController : ControllerBase
    {
        private readonly IChinookSupervisor _chinookSupervisor;

        public GenreController(IChinookSupervisor chinookSupervisor)
        {
            _chinookSupervisor = chinookSupervisor;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Gets all Genre",
            Description = "Gets all Genre",
            OperationId = "Genre.GetAll",
            Tags = new[] { "GenreEndpoint"})]
        [Produces(typeof(List<GenreApiModel>))]
        public ActionResult<List<GenreApiModel>> Get()
        {
            try
            {
                return new ObjectResult(_chinookSupervisor.GetAllGenre());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Gets a specific Genre",
            Description = "Gets a specific Genre",
            OperationId = "Genre.GetOne",
            Tags = new[] { "GenreEndpoint"})]
        [Produces(typeof(GenreApiModel))]
        public ActionResult<GenreApiModel> Get(int id)
        {
            try
            {
                var genre = _chinookSupervisor.GetGenreById(id);

                return Ok(genre);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Creates a new Genre",
            Description = "Creates a new Genre",
            OperationId = "Genre.Create",
            Tags = new[] { "GenreEndpoint"})]
        public ActionResult<GenreApiModel> Post([FromBody] GenreApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();

                return StatusCode(201, _chinookSupervisor.AddGenre(input));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Update an Genre",
            Description = "Update an Genre",
            OperationId = "Genre.Update",
            Tags = new[] { "GenreEndpoint"})]
        public ActionResult<GenreApiModel> Put(int id, [FromBody] GenreApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();

                if (_chinookSupervisor.UpdateGenre(input))
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
            Summary = "Delete a Genre",
            Description = "Delete a Genre",
            OperationId = "Genre.Delete",
            Tags = new[] { "GenreEndpoint"})]
        public ActionResult Delete(int id)
        {
            try
            {
                if (_chinookSupervisor.DeleteGenre(id))
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