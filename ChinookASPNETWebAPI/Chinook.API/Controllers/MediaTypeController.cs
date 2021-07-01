﻿using System;
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
    [ResponseCache(Duration = 604800)] // cache for a week
    [EnableCors("CorsPolicy")]
    [ApiController]
    public class MediaTypeController : ControllerBase
    {
        private readonly IChinookSupervisor _chinookSupervisor;
        private readonly ILogger<MediaTypeController> _logger;

        public MediaTypeController(IChinookSupervisor chinookSupervisor, ILogger<MediaTypeController> logger)
        {
            _chinookSupervisor = chinookSupervisor;
            _logger = logger;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Gets all MediaType",
            Description = "Gets all MediaType",
            OperationId = "MediaType.GetAll",
            Tags = new[] { "MediaTypeEndpoint"})]
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
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "GetMediaTypeById")]
        [SwaggerOperation(
            Summary = "Gets a specific MediaType",
            Description = "Gets a specific MediaType",
            OperationId = "MediaType.GetOne",
            Tags = new[] { "MediaTypeEndpoint"})]
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
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Creates a new MediaType",
            Description = "Creates a new MediaType",
            OperationId = "MediaType.Create",
            Tags = new[] { "MediaTypeEndpoint"})]
        public ActionResult<MediaTypeApiModel> Post([FromBody] MediaTypeApiModel input)
        {
            try
            {
                if (input == null)
                {
                    return BadRequest("Media Type is null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Media Type object");
                }
                
                var mediaType = _chinookSupervisor.AddMediaType(input);
        
                return CreatedAtRoute("GetMediaTypeById", new { id = mediaType.Id }, mediaType);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Update an MediaType",
            Description = "Update an MediaType",
            OperationId = "MediaType.Update",
            Tags = new[] { "MediaTypeEndpoint"})]
        public ActionResult<MediaTypeApiModel> Put(int id, [FromBody] MediaTypeApiModel input)
        {
            try
            {
                if (input == null)
                {
                    return BadRequest("Media Type is null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Media Type object");
                }

                if (_chinookSupervisor.UpdateMediaType(input))
                {
                    return CreatedAtRoute("GetMediaTypeById", new { id = input.Id }, input);
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
            Summary = "Delete a MediaType",
            Description = "Delete a MediaType",
            OperationId = "MediaType.Delete",
            Tags = new[] { "MediaTypeEndpoint"})]
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
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}