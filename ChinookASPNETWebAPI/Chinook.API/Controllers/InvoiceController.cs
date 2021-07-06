using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    public class InvoiceController : ControllerBase
    {
        private readonly IChinookSupervisor _chinookSupervisor;
        private readonly ILogger<InvoiceController> _logger;

        public InvoiceController(IChinookSupervisor chinookSupervisor, ILogger<InvoiceController> logger)
        {
            _chinookSupervisor = chinookSupervisor;
            _logger = logger;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Gets all Invoice",
            Description = "Gets all Invoice",
            OperationId = "Invoice.GetAll",
            Tags = new[] { "InvoiceEndpoint"})]
        [Produces(typeof(List<InvoiceApiModel>))]
        public async Task<ActionResult<List<InvoiceApiModel>>> Get()
        {
            try
            {
                return new ObjectResult(await _chinookSupervisor.GetAllInvoice());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "GetInvoiceById")]
        [SwaggerOperation(
            Summary = "Gets a specific Invoice",
            Description = "Gets a specific Invoice",
            OperationId = "Invoice.GetOne",
            Tags = new[] { "InvoiceEndpoint"})]
        [Produces(typeof(InvoiceApiModel))]
        public async Task<ActionResult<InvoiceApiModel>> Get(int id)
        {
            try
            {
                var invoice = await _chinookSupervisor.GetInvoiceById(id);
                if ( invoice == null)
                {
                    return NotFound();
                }

                return Ok(invoice);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("customer/{id}")]
        [SwaggerOperation(
            Summary = "Gets Invoices by Customer",
            Description = "Gets Invoices by Customer",
            OperationId = "Invoice.GetByArtist",
            Tags = new[] { "InvoiceEndpoint"})]
        [Produces(typeof(List<InvoiceApiModel>))]
        public async Task<ActionResult<InvoiceApiModel>> GetByCustomerId(int id)
        {
            try
            {
                return Ok(await _chinookSupervisor.GetInvoiceByCustomerId(id));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Creates a new Invoice",
            Description = "Creates a new Invoice",
            OperationId = "Invoice.Create",
            Tags = new[] { "InvoiceEndpoint"})]
        public async Task<ActionResult<InvoiceApiModel>> Post([FromBody] InvoiceApiModel input)
        {
            try
            {
                if (input == null)
                {
                    return BadRequest("Invoice is null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Invoice object");
                }
                
                var invoice = await _chinookSupervisor.AddInvoice(input);
        
                return CreatedAtRoute("GetInvoiceById", new { id = invoice.Id }, invoice);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Update an Invoice",
            Description = "Update an Invoice",
            OperationId = "Invoice.Update",
            Tags = new[] { "InvoiceEndpoint"})]
        public async Task<ActionResult<InvoiceApiModel>> Put(int id, [FromBody] InvoiceApiModel input)
        {
            try
            {
                if (input == null)
                {
                    return BadRequest("Invoice is null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Invoice object");
                }

                if (await _chinookSupervisor.UpdateInvoice(input))
                {
                    return CreatedAtRoute("GetInvoiceById", new { id = input.Id }, input);
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
            Summary = "Delete a Invoice",
            Description = "Delete a Invoice",
            OperationId = "Invoice.Delete",
            Tags = new[] { "InvoiceEndpoint"})]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (await _chinookSupervisor.DeleteInvoice(id))
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
        
        [HttpGet("employee/{id}")]
        [SwaggerOperation(
            Summary = "Gets Invoices by Employee",
            Description = "Gets Invoices by Employee",
            OperationId = "Invoice.Create",
            Tags = new[] { "InvoiceEndpoint"})]
        [Produces(typeof(List<InvoiceApiModel>))]
        public async Task<ActionResult<InvoiceApiModel>> GetByEmployeeId(int id)
        {
            try
            {
                return Ok(await _chinookSupervisor.GetInvoiceByEmployeeId(id));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}