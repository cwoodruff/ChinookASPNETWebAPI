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
            Summary = "Gets all Album",
            Description = "Gets all Album",
            OperationId = "Album.GetAll",
            Tags = new[] { "AlbumEndpoint"})]
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
            Summary = "Gets a specific Album",
            Description = "Gets a specific Album",
            OperationId = "Album.GetOne",
            Tags = new[] { "AlbumEndpoint"})]
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
            Summary = "Gets Albums by Artist",
            Description = "Gets Albums by Artist",
            OperationId = "Album.GetByArtist",
            Tags = new[] { "AlbumEndpoint"})]
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
            Summary = "Gets Albums by Artist",
            Description = "Gets Albums by Artist",
            OperationId = "Album.GetByArtist",
            Tags = new[] { "AlbumEndpoint"})]
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
            Summary = "Creates a new Album",
            Description = "Creates a new Album",
            OperationId = "Album.Create",
            Tags = new[] { "AlbumEndpoint"})]
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
            Summary = "Update an Album",
            Description = "Update an Album",
            OperationId = "Album.Update",
            Tags = new[] { "AlbumEndpoint"})]
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
            Summary = "Delete a Album",
            Description = "Delete a Album",
            OperationId = "Album.Delete",
            Tags = new[] { "AlbumEndpoint"})]
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