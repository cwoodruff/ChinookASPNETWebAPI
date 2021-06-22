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
    public class EmployeeController : ControllerBase
    {
        private readonly IChinookSupervisor _chinookSupervisor;

        public EmployeeController(IChinookSupervisor chinookSupervisor)
        {
            _chinookSupervisor = chinookSupervisor;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Gets all Employee",
            Description = "Gets all Employee",
            OperationId = "Employee.GetAll",
            Tags = new[] { "EmployeeEndpoint"})]
        [Produces(typeof(List<EmployeeApiModel>))]
        public ActionResult<List<EmployeeApiModel>> Get()
        {
            try
            {
                return new ObjectResult(_chinookSupervisor.GetAllEmployee());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Gets a specific Employee",
            Description = "Gets a specific Employee",
            OperationId = "Employee.GetOne",
            Tags = new[] { "EmployeeEndpoint"})]
        [Produces(typeof(EmployeeApiModel))]
        public ActionResult<EmployeeApiModel> Get(int id)
        {
            try
            {
                var employee = _chinookSupervisor.GetEmployeeById(id);
                if ( employee == null)
                {
                    return NotFound();
                }

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("reportsto/{id}")]
        [SwaggerOperation(
            Summary = "Gets Reports to by Employee",
            Description = "Gets Reports to by Employee",
            OperationId = "Employee.GetReportsTo",
            Tags = new[] { "EmployeeEndpoint"})]
        [Produces(typeof(List<EmployeeApiModel>))]
        public ActionResult<List<EmployeeApiModel>> GetReportsTo(int id)
        {
            try
            {
                var employee = _chinookSupervisor.GetEmployeeById(id);
                if ( employee == null)
                {
                    return NotFound();
                }

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("directreports/{id}")]
        [SwaggerOperation(
            Summary = "Gets Employee direct reports",
            Description = "Gets Employee direct reports",
            OperationId = "Employee.GetByArtist",
            Tags = new[] { "EmployeeEndpoint"})]
        [Produces(typeof(EmployeeApiModel))]
        public ActionResult<EmployeeApiModel> GetDirectReports(int id)
        {
            try
            {
                var employee = _chinookSupervisor.GetEmployeeById(id);

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Creates a new Employee",
            Description = "Creates a new Employee",
            OperationId = "Employee.Create",
            Tags = new[] { "EmployeeEndpoint"})]
        public ActionResult<EmployeeApiModel> Post([FromBody] EmployeeApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();

                return StatusCode(201, _chinookSupervisor.AddEmployee(input));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Update an Employee",
            Description = "Update an Employee",
            OperationId = "Employee.Update",
            Tags = new[] { "EmployeeEndpoint"})]
        public ActionResult<EmployeeApiModel> Put(int id, [FromBody] EmployeeApiModel input)
        {
            try
            {
                if (input == null)
                    return BadRequest();

                if (_chinookSupervisor.UpdateEmployee(input))
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
            Summary = "Delete a Employee",
            Description = "Delete a Employee",
            OperationId = "Employee.Delete",
            Tags = new[] { "EmployeeEndpoint"})]
        public ActionResult Delete(int id)
        {
            try
            {
                if (_chinookSupervisor.DeleteEmployee(id))
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