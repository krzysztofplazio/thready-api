using Microsoft.AspNetCore.Mvc;

namespace Thready.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    public IActionResult Get()
    {
        return Ok();
    }
}
