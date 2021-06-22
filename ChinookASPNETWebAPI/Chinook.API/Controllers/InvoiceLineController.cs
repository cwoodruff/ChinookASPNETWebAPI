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
    public class InvoiceLineController : ControllerBase
    {
        private readonly IChinookSupervisor _chinookSupervisor;

        public InvoiceLineController(IChinookSupervisor chinookSupervisor)
        {
            _chinookSupervisor = chinookSupervisor;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Gets all Album",
            Description = "Gets all Album",
            OperationId = "Album.GetAll",
            Tags = new[] { "AlbumEndpoint"})]
        [Produces(typeof(List<InvoiceLineApiModel>))]
        public ActionResult<List<InvoiceLineApiModel>> Get()
        {
            try
            {
                return new ObjectResult(_chinookSupervisor.GetAllInvoiceLine());
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
        [Produces(typeof(InvoiceLineApiModel))]
        public ActionResult<InvoiceLineApiModel> Get(int id)
        {
            try
            {
                var invoiceLine = _chinookSupervisor.GetInvoiceLineById(id);
                if ( invoiceLine == null)
                {
                    return NotFound();
                }

                return Ok(invoiceLine);
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
        [Produces(typeof(List<InvoiceLineApiModel>))]
        public ActionResult<InvoiceLineApiModel> GetByInvoiceId(int id)
        {
            try
            {
                var invoiceLines = _chinookSupervisor.GetInvoiceLineByInvoiceId(id);

                return Ok(invoiceLines);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("track/{id}")]
        [SwaggerOperation(
            Summary = "Creates a new Album",
            Description = "Creates a new Album",
            OperationId = "Album.Create",
            Tags = new[] { "AlbumEndpoint"})]
        [Produces(typeof(List<InvoiceLineApiModel>))]
        public ActionResult<InvoiceLineApiModel> GetByTrackId(int id)
        {
            try
            {
                var invoiceLines = _chinookSupervisor.GetInvoiceLineByTrackId(id);

                return Ok(invoiceLines);
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
        public ActionResult<InvoiceLineApiModel> Post([FromBody] InvoiceLineApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();

                return StatusCode(201, _chinookSupervisor.AddInvoiceLine(input));
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
        public ActionResult<InvoiceLineApiModel> Put(int id, [FromBody] InvoiceLineApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();

                if (_chinookSupervisor.UpdateInvoiceLine(input))
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
                if (_chinookSupervisor.DeleteInvoiceLine(id))
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