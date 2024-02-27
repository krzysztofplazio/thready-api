using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Thready.Application.Constants;
using Thready.Application.Dtos.Projects;
using Thready.Application.Exceptions.Users;
using Thready.Core.Dtos.Paginations;
using Thready.Core.Repositories.Users;

namespace Thready.Application.Queries.GetAssignedProjects;

public class GetAssignedProjectsHandler(IProjectsRepository projectsRepository, IHttpContextAccessor httpContextAccessor) : IRequestHandler<GetAssignedProjectsQuery, PagedItems<ProjectResult>>
{
    private readonly IProjectsRepository _projectsRepository = projectsRepository;
    private readonly HttpContext _httpContext = httpContextAccessor.HttpContext ?? throw new ArgumentNullException(paramName: nameof(httpContextAccessor));

    async Task<PagedItems<ProjectResult>> IRequestHandler<GetAssignedProjectsQuery, PagedItems<ProjectResult>>.Handle(GetAssignedProjectsQuery request, CancellationToken cancellationToken)
    {
        if (!int.TryParse(_httpContext.User.Claims.FirstOrDefault(x => string.Equals(x.Type, ClaimTypes.NameIdentifier, StringComparison.Ordinal))?.Value 
                            ?? throw new UserHasNoIdentityException(UserExceptionErrorCodes.UserHasNoIdentity), CultureInfo.InvariantCulture, out int userId))
        {
            throw new UserNotExistException(UserExceptionErrorCodes.UserNotExist);
        }

        if (request.Search is null)
        {
            return await _projectsRepository.GetAssignedProjects(request.Order, request.Search, userId, request.PageSize, request.PageNumber, cancellationToken).ConfigureAwait(false);
        }

        var searchList = request.Search
            .Split(',')
            .Select(search =>
            {
                var splittedSearch = search.Split(':');
                return $"{splittedSearch[0].Trim()}.Contains(\"{splittedSearch[1].Trim()}\")";
            })
            .ToList();
        var finalSearch = string.Join(" || ", searchList);
        return await _projectsRepository.GetAssignedProjects(request.Order, finalSearch, userId, request.PageSize, request.PageNumber, cancellationToken).ConfigureAwait(false);
    }
}
