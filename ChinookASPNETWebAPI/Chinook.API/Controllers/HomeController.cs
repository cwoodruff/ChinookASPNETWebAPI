using System;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Chinook.API.Controllers
{
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [SwaggerOperation(
            Summary = "Creates a new Album",
            Description = "Creates a new Album",
            OperationId = "Album.Create",
            Tags = new[] { "AlbumEndpoint"})]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Index()
        {
            try
            {
                var uri = new Uri("/swagger", UriKind.Relative);
                return Redirect(uri.ToString());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}