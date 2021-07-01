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
    [EnableCors("CorsPolicy")]
    [ApiController]
    public class TrackController : ControllerBase
    {
        private readonly IChinookSupervisor _chinookSupervisor;
        private readonly ILogger<TrackController> _logger;

        public TrackController(IChinookSupervisor chinookSupervisor, ILogger<TrackController> logger)
        {
            _chinookSupervisor = chinookSupervisor;
            _logger = logger;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Gets all Track",
            Description = "Gets all Track",
            OperationId = "Track.GetAll",
            Tags = new[] { "TrackEndpoint"})]
        [Produces(typeof(List<TrackApiModel>))]
        public ActionResult<List<TrackApiModel>> Get()
        {
            try
            {
                return new ObjectResult(_chinookSupervisor.GetAllTrack());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "GetTrackById")]
        [SwaggerOperation(
            Summary = "Gets a specific Track",
            Description = "Gets a specific Track",
            OperationId = "Track.GetOne",
            Tags = new[] { "TrackEndpoint"})]
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
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("album/{id}")]
        [SwaggerOperation(
            Summary = "Gets Track by Album",
            Description = "Gets Track by Album",
            OperationId = "Track.GetByAlbumId",
            Tags = new[] { "TrackEndpoint"})]
        [Produces(typeof(List<TrackApiModel>))]
        public ActionResult<TrackApiModel> GetByAlbumId(int id)
        {
            try
            {
                return Ok(_chinookSupervisor.GetTrackByAlbumId(id));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("mediatype/{id}")]
        [SwaggerOperation(
            Summary = "Gets Track by MediaType",
            Description = "Gets Track by MediaType",
            OperationId = "Track.GetByMediaTypeId",
            Tags = new[] { "TrackEndpoint"})]
        [Produces(typeof(List<TrackApiModel>))]
        public ActionResult<TrackApiModel> GetByMediaTypeId(int id)
        {
            try
            {
                return Ok(_chinookSupervisor.GetTrackByMediaTypeId(id));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("genre/{id}")]
        [SwaggerOperation(
            Summary = "Gets Track by Genre",
            Description = "Gets Track by Genre",
            OperationId = "Track.GetByGenreId",
            Tags = new[] { "TrackEndpoint"})]
        [Produces(typeof(List<TrackApiModel>))]
        public ActionResult<TrackApiModel> GetByGenreId(int id)
        {
            try
            {
                return Ok(_chinookSupervisor.GetTrackByGenreId(id));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Creates a new Track",
            Description = "Creates a new Track",
            OperationId = "Track.Create",
            Tags = new[] { "TrackEndpoint"})]
        public ActionResult<TrackApiModel> Post([FromBody] TrackApiModel input)
        {
            try
            {
                if (input == null)
                {
                    return BadRequest("Track is null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Track object");
                }
                
                var track = _chinookSupervisor.AddTrack(input);
        
                return CreatedAtRoute("GetTrackById", new { id = track.Id }, track);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Update an Track",
            Description = "Update an Track",
            OperationId = "Track.Update",
            Tags = new[] { "TrackEndpoint"})]
        public ActionResult<TrackApiModel> Put(int id, [FromBody] TrackApiModel input)
        {
            try
            {
                if (input == null)
                {
                    return BadRequest("Track is null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Track object");
                }

                if (_chinookSupervisor.UpdateTrack(input))
                {
                    return CreatedAtRoute("GetTrackById", new { id = input.Id }, input);
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
            Summary = "Delete a Track",
            Description = "Delete a Track",
            OperationId = "Track.Delete",
            Tags = new[] { "TrackEndpoint"})]
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
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
        
        [HttpGet("artist/{id}")]
        [SwaggerOperation(
            Summary = "Gets Track by Artist",
            Description = "Gets Track by Artist",
            OperationId = "Track.GetByArtistId",
            Tags = new[] { "TrackEndpoint"})]
        [Produces(typeof(List<TrackApiModel>))]
        public ActionResult<TrackApiModel> GetByArtistId(int id)
        {
            try
            {
                return Ok(_chinookSupervisor.GetTrackByArtistId(id));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
        
        [HttpGet("invoice/{id}")]
        [SwaggerOperation(
            Summary = "Gets Track by Invoice",
            Description = "Gets Track by Invoice",
            OperationId = "Track.GetByInvoiceId",
            Tags = new[] { "TrackEndpoint"})]
        [Produces(typeof(List<TrackApiModel>))]
        public ActionResult<TrackApiModel> GetByInvoiceId(int id)
        {
            try
            {
                return Ok(_chinookSupervisor.GetTrackByInvoiceId(id));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}