using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Thready.Application.Queries.GetAssignedProjects;

namespace Thready.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProjectsController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAssignedProjects(string? order, string? search, int pageNumber = 1, int pageSize = 15, CancellationToken cancellationToken = default)
    {
        return Ok(await _mediator.Send(new GetAssignedProjectsQuery(order, search, pageNumber, pageSize), cancellationToken));
    }
}
