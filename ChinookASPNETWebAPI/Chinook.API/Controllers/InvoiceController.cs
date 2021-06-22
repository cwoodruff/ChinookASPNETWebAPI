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
    public class InvoiceController : ControllerBase
    {
        private readonly IChinookSupervisor _chinookSupervisor;

        public InvoiceController(IChinookSupervisor chinookSupervisor)
        {
            _chinookSupervisor = chinookSupervisor;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Gets all Invoice",
            Description = "Gets all Invoice",
            OperationId = "Invoice.GetAll",
            Tags = new[] { "InvoiceEndpoint"})]
        [Produces(typeof(List<InvoiceApiModel>))]
        public ActionResult<List<InvoiceApiModel>> Get()
        {
            try
            {
                return new ObjectResult(_chinookSupervisor.GetAllInvoice());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Gets a specific Invoice",
            Description = "Gets a specific Invoice",
            OperationId = "Invoice.GetOne",
            Tags = new[] { "InvoiceEndpoint"})]
        [Produces(typeof(InvoiceApiModel))]
        public ActionResult<InvoiceApiModel> Get(int id)
        {
            try
            {
                var invoice = _chinookSupervisor.GetInvoiceById(id);
                if ( invoice == null)
                {
                    return NotFound();
                }

                return Ok(invoice);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("customer/{id}")]
        [SwaggerOperation(
            Summary = "Gets Invoices by Customer",
            Description = "Gets Invoices by Customer",
            OperationId = "Invoice.GetByArtist",
            Tags = new[] { "InvoiceEndpoint"})]
        [Produces(typeof(List<InvoiceApiModel>))]
        public ActionResult<InvoiceApiModel> GetByCustomerId(int id)
        {
            try
            {
                return Ok(_chinookSupervisor.GetInvoiceByCustomerId(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Creates a new Invoice",
            Description = "Creates a new Invoice",
            OperationId = "Invoice.Create",
            Tags = new[] { "InvoiceEndpoint"})]
        public ActionResult<InvoiceApiModel> Post([FromBody] InvoiceApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();

                return StatusCode(201, _chinookSupervisor.AddInvoice(input));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Update an Invoice",
            Description = "Update an Invoice",
            OperationId = "Invoice.Update",
            Tags = new[] { "InvoiceEndpoint"})]
        public ActionResult<InvoiceApiModel> Put(int id, [FromBody] InvoiceApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();

                if (_chinookSupervisor.UpdateInvoice(input))
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
            Summary = "Delete a Invoice",
            Description = "Delete a Invoice",
            OperationId = "Invoice.Delete",
            Tags = new[] { "InvoiceEndpoint"})]
        public ActionResult Delete(int id)
        {
            try
            {
                if (_chinookSupervisor.DeleteInvoice(id))
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
        
        [HttpGet("employee/{id}")]
        [SwaggerOperation(
            Summary = "Gets Invoices by Employee",
            Description = "Gets Invoices by Employee",
            OperationId = "Invoice.Create",
            Tags = new[] { "InvoiceEndpoint"})]
        [Produces(typeof(List<InvoiceApiModel>))]
        public ActionResult<InvoiceApiModel> GetByEmployeeId(int id)
        {
            try
            {
                return Ok(_chinookSupervisor.GetInvoiceByEmployeeId(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}