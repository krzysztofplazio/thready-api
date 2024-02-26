using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thready.Application.Dtos.Projects;
using Thready.Core.Dtos.Paginations;

namespace Thready.Application.Queries.GetAssignedProjects;

public class GetAssignedProjectsQuery(string? order, string? search, int pageNumber = 1, int pageSize = 15) : IRequest<PagedItems<ProjectResult>>
{
    public string? Order { get; } = order;
    public string? Search { get; } = search;
    public int PageNumber { get; } = pageNumber;
    public int PageSize { get; } = pageSize;
}
