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
    public class InvoiceLineController : ControllerBase
    {
        private readonly IChinookSupervisor _chinookSupervisor;
        private readonly ILogger<InvoiceLineController> _logger;

        public InvoiceLineController(IChinookSupervisor chinookSupervisor, ILogger<InvoiceLineController> logger)
        {
            _chinookSupervisor = chinookSupervisor;
            _logger = logger;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Gets all InvoiceLine",
            Description = "Gets all InvoiceLine",
            OperationId = "InvoiceLine.GetAll",
            Tags = new[] { "InvoiceLineEndpoint"})]
        [Produces(typeof(List<InvoiceLineApiModel>))]
        public ActionResult<List<InvoiceLineApiModel>> Get()
        {
            try
            {
                return new ObjectResult(_chinookSupervisor.GetAllInvoiceLine());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "GetInvoiceLineById")]
        [SwaggerOperation(
            Summary = "Gets a specific InvoiceLine",
            Description = "Gets a specific InvoiceLine",
            OperationId = "InvoiceLine.GetOne",
            Tags = new[] { "InvoiceLineEndpoint"})]
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
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("invoice/{id}")]
        [SwaggerOperation(
            Summary = "Gets InvoiceLine by Invoice",
            Description = "Gets InvoiceLine by Invoice",
            OperationId = "InvoiceLine.GetByInvoice",
            Tags = new[] { "InvoiceLineEndpoint"})]
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
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("track/{id}")]
        [SwaggerOperation(
            Summary = "Gets InvoiceLine by Track",
            Description = "Gets InvoiceLine by Track",
            OperationId = "InvoiceLine.GetByTrackId",
            Tags = new[] { "InvoiceLineEndpoint"})]
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
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Creates a new InvoiceLine",
            Description = "Creates a new InvoiceLine",
            OperationId = "InvoiceLine.Create",
            Tags = new[] { "InvoiceLineEndpoint"})]
        public ActionResult<InvoiceLineApiModel> Post([FromBody] InvoiceLineApiModel input)
        {
            try
            {
                if (input == null)
                {
                    return BadRequest("Invoice Line is null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Invoice Line object");
                }
                
                var invoiceLine = _chinookSupervisor.AddInvoiceLine(input);
        
                return CreatedAtRoute("GetInvoiceLineById", new { id = invoiceLine.Id }, invoiceLine);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Update an InvoiceLine",
            Description = "Update an InvoiceLine",
            OperationId = "InvoiceLine.Update",
            Tags = new[] { "InvoiceLineEndpoint"})]
        public ActionResult<InvoiceLineApiModel> Put(int id, [FromBody] InvoiceLineApiModel input)
        {
            try
            {
                if (input == null)
                {
                    return BadRequest("Invoice Line is null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Invoice Line object");
                }

                if (_chinookSupervisor.UpdateInvoiceLine(input))
                {
                    return CreatedAtRoute("GetInvoiceLineById", new { id = input.Id }, input);
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
            Summary = "Delete a InvoiceLine",
            Description = "Delete a InvoiceLine",
            OperationId = "InvoiceLine.Delete",
            Tags = new[] { "InvoiceLineEndpoint"})]
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
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}