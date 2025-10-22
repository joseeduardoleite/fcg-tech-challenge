using Microsoft.AspNetCore.Mvc;

namespace FiapCloudGames.Api.Controllers.v1;

[ApiController]
[Route("v{version:apiVersion}[controller]")]
public class UsuariosController(ILogger<UsuariosController> logger) : ControllerBase
{
    [HttpGet(Name = "")]
    [ProducesResponseType(typeof(int), 200)]
    public async Task<ActionResult<IEnumerable<object>>> GetAsync()
    {
        return Ok();
    }
}
