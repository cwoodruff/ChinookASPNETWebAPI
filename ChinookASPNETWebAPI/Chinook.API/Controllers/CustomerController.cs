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
    public class CustomerController : ControllerBase
    {
        private readonly IChinookSupervisor _chinookSupervisor;

        public CustomerController(IChinookSupervisor chinookSupervisor)
        {
            _chinookSupervisor = chinookSupervisor;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Get all Customers",
            Description = "Get all Customers",
            OperationId = "Customer.GetAll",
            Tags = new[] { "CustomerEndpoint"})]
        [Produces(typeof(List<CustomerApiModel>))]
        public ActionResult<List<CustomerApiModel>> Get()
        {
            try
            {
                return new ObjectResult(_chinookSupervisor.GetAllCustomer());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get specific Customers",
            Description = "Creates specific Customer",
            OperationId = "Customer.GetOne",
            Tags = new[] { "CustomerEndpoint"})]
        [Produces(typeof(CustomerApiModel))]
        public ActionResult<CustomerApiModel> Get(int id)
        {
            try
            {
                var customer = _chinookSupervisor.GetCustomerById(id);
                if ( customer == null)
                {
                    return NotFound();
                }

                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("supportrep/{id}")]
        [SwaggerOperation(
            Summary = "Get Customers by Support Rep",
            Description = "Get Customers by Support Rep",
            OperationId = "Customer.GetBySupportRep",
            Tags = new[] { "CustomerEndpoint"})]
        [Produces(typeof(List<CustomerApiModel>))]
        public ActionResult<CustomerApiModel> GetBySupportRepId(int id)
        {
            try
            {
                var rep = _chinookSupervisor.GetEmployeeById(id);

                return Ok(rep);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Creates a new Customer",
            Description = "Creates a new Customer",
            OperationId = "Customer.Create",
            Tags = new[] { "CustomerEndpoint"})]
        public ActionResult<CustomerApiModel> Post([FromBody] CustomerApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();

                return StatusCode(201, _chinookSupervisor.AddCustomer(input));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Update Customer",
            Description = "Update Customer",
            OperationId = "Customer.Update",
            Tags = new[] { "CustomerEndpoint"})]
        public ActionResult<CustomerApiModel> Put(int id, [FromBody] CustomerApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();

                if (_chinookSupervisor.UpdateCustomer(input))
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
            Summary = "Delete Customer",
            Description = "Delete Customer",
            OperationId = "Customer.Delete",
            Tags = new[] { "CustomerEndpoint"})]
        public ActionResult Delete(int id)
        {
            try
            {
                if (_chinookSupervisor.DeleteCustomer(id))
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