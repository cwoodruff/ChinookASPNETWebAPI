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
    public class EmployeeController : ControllerBase
    {
        private readonly IChinookSupervisor _chinookSupervisor;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IChinookSupervisor chinookSupervisor, ILogger<EmployeeController> logger)
        {
            _chinookSupervisor = chinookSupervisor;
            _logger = logger;
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
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "GetEmployeeById")]
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
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
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
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
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
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
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
                {
                    return BadRequest("Employee is null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Employee object");
                }
                
                var employee = _chinookSupervisor.AddEmployee(input);
        
                return CreatedAtRoute("GetEmployeeById", new { id = employee.Id }, employee);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
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
                {
                    return BadRequest("Employee is null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Employee object");
                }

                if (_chinookSupervisor.UpdateEmployee(input))
                {
                    return CreatedAtRoute("GetEmployeeById", new { id = input.Id }, input);
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
                _logger.LogError($"Something went wrong inside the AlbumController Get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}